using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modbus.Device;

namespace PlcDev
{
    public enum ACVariable : ushort
    {
        Alam = 40009-1,
        AirRun = 40011 - 1,//开机 1：启动0：停止
        AirDutyRun = 40013 - 1,//值班 1：启动0：停止
        AirRunSign = 40026-1,
        AirDutyRunSign = 40029-1,
        TemSet = 40038-1,
        HempSet = 40040-1
    }

    public struct ACAlarm
    {
        public bool Error, Stop, Lessfan, Highlevel, Heat;
    }

    public struct ACState
    {
        public bool bAirRun, BAirDutyRun;
        public int TemSet, HemSet;
        public ACAlarm AcAlarm;
    }
    public class AirCondition
    {
        private string _ip;
        private ushort _port;
        private ModbusIpMaster _master;
        
        public AirCondition(string ip, ushort port)
        {
            _ip = ip;
            _port = port;
            _master = ModbusIpMaster.CreateIp(ip, port);
        }

        public void ACSwitch(bool bOpen)
        {
            if (bOpen)
            {
                _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.AirRun)-40000), 1);
            }
            else{
                _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.AirRun) - 40000), 0);
            }
        }
        public void ACDutySwitch(bool bOpen)
        {
            if (bOpen)
            {
                _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.AirDutyRun) - 40000), 1);
            }
            {
                _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.AirDutyRun) - 40000), 0);
            }
        }

        /// <summary>
        ///  天气
        /// </summary>
        /// <param name="temp"></param>
        public void SetRoomTemp(ushort temp)
        {
            _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.TemSet) - 40000), temp);
        }

        public void SetRoomHemp(ushort hemp)
        {
            _master.WriteSingleRegister((ushort)(Convert.ToUInt16(ACVariable.HempSet) - 40000), hemp);
        }
        public ACState GetACState()
        {
            ACState acState = new ACState();
            ushort[] b = _master.ReadHoldingRegisters((ushort)(0), 54);
            if (b.Length >= 54)
            {
                ushort bAlarm = b[8];

                acState.AcAlarm.Error = Convert.ToBoolean(bAlarm & 1);
                acState.AcAlarm.Stop = Convert.ToBoolean(bAlarm & (1 << 1));
                acState.AcAlarm.Lessfan = Convert.ToBoolean(bAlarm & (1 << 2));
                acState.AcAlarm.Highlevel = Convert.ToBoolean(bAlarm & (1 << 3));
                acState.AcAlarm.Heat = Convert.ToBoolean(bAlarm & (1 << 4));

                acState.bAirRun = Convert.ToBoolean(b[25]);
                acState.BAirDutyRun = Convert.ToBoolean(b[28]);
                acState.TemSet = b[37];
                acState.HemSet = b[39];
            }

            return acState;
        }
    }
}
