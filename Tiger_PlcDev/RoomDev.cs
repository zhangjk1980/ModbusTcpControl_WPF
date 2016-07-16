using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modbus.Device;

namespace PlcDev
{
   /// <summary>
   /// 设备变量枚举
   /// </summary>
    public enum DevVariable : ushort
    {
        BeijingAdjust = 1 - 1,       // 北京时间校准按钮（触摸屏时间校准用）		
                                                   //触摸屏上长按3s进入校准状态，进入校准状态后，每按一下，
                                                    //  对应要设置的时间闪烁。5s不操作退出校准状态。
        BeijingAdd = 2 - 1,         //北京时间设置-增按钮（触摸屏时间校准用）			进入校准状态后，按此按钮数值增加
        BeijingDel = 3 - 1,         //北京时间设置-减按钮（触摸屏时间校准用）	
        BeijingSubmit = 4 - 1,  //时间校准（上位机用）上位机校准时间输入后，按此按钮将校准时间写入
        MazuiReset = 5 - 1,     //麻醉时间重置
        MazuiPTime = 6 - 1,   //麻醉时间正计时开关
        MazuiNTime = 7 - 1,     //麻醉时间倒计时开关
        ORTimeReset = 11 - 1,   //手术时间重置
        ORPTime = 12 - 1,           //手术时间正计时开关
        ORNTime = 13 - 1,           //手术时间倒计时开关
        LAMP1 = 22 - 1,             //照明灯1开关
        LAMP2 = 23 - 1,            //照明灯2开关
        ORLED1 = 24 - 1,           //无影灯1开关
        ORLED2 = 25 - 1,          //无影灯2开关
        Viewbox = 26 - 1,         //观片灯开关
        LampBack1 = 27 - 1,         //备用1开关
        LampBack2 = 28 - 1,         //备用2开关
        Fans = 29 - 1,                   //排风机开关
        Waste = 30 - 1,              //排废弃开关
        FansBack = 31 - 1,           //通风备用开关
        AirAlarm = 56 - 1 ,          //气体报警开关

        BeijingTime = 40002 - 1,
        MazuiTime = 40009 - 1,
        ORTime = 40013 - 1,
        OPAIR = 00032 - 1,
        LAMP1Bright = 40021 - 1,
        LAMP2Bright = 40022 - 1,
        ORLed1Bright = 40023 - 1,
        ORLed2Bright = 40024 - 1,
        BackLED1Bright = 40025 - 1,
        BackLED2Bright = 40026 - 1,
        MazuiTimeSec = 40027 - 1, //
        MazuiTimeMin = 40028 - 1,
        MazuiTimeHour = 40029 - 1,
        OrTimeSec = 40030 - 1,
        OrTimeMin = 40031 - 1,
        OrTimeHour = 40032 - 1,
    }

  

    public enum AIR : int
    {
        O2 = 0, AIR, NP, AR, N2, CO2, AIRCOUT
    }

    public enum AIRSTATE : int
    {
        HIGH = 1 << 2,
        LOW = 1 << 1,
        NORMAL = 1
    }
    public class RoomDev
    {
        private string _ip;
        private ushort _port;
        private ModbusIpMaster _master;

        public RoomDev(string ip, ushort port)
        {
            _ip = ip;
            _port = port;
            _master = ModbusIpMaster.CreateIp(ip, port);
        }

        public DateTime GetBeijingTime()
        {
            DateTime t = new DateTime(2000, 1, 1, 0, 0, 0);
            try
            {
                ushort startAddress = 1;
                ushort numInputs = 6;
                ushort[] inputs = _master.ReadHoldingRegisters(startAddress, numInputs);
                if (inputs != null && inputs.Count() >= numInputs)
                {

                    t = new DateTime(inputs[5],
                        inputs[4], inputs[3], inputs[2], inputs[1], inputs[0]);

                }
            }
            catch (Exception)
            {


            }
            return t;
        }
        public DateTime GetMazuiTime()
        {
            DateTime t = new DateTime(2000, 1, 1, 0, 0, 0);
            try
            {
                ushort startAddress = 8;
                ushort numInputs = 3;
                ushort[] inputs = _master.ReadHoldingRegisters(startAddress, numInputs);
                if (inputs != null && inputs.Count() >= numInputs)
                {

                    t = new DateTime(2000,
                      1, 1, inputs[2], inputs[1], inputs[0]);

                }
            }
            catch (Exception)
            {

            }
            return t;
        }
        public DateTime GetORTime()
        {
            DateTime t = new DateTime(2000, 1, 1, 0, 0, 0);
            try
            {
                ushort startAddress = 8;
                ushort numInputs = 3;
                ushort[] inputs = _master.ReadHoldingRegisters(startAddress, numInputs);
                if (inputs != null && inputs.Count() >= numInputs)
                {

                    t = new DateTime(2000,
                      1, 1, inputs[2], inputs[1], inputs[0]);

                }
            }
            catch (Exception)
            {


            }
            return t;
        }
        /// <summary>
        ///  设备开关
        /// </summary>
        public bool Switch(DevVariable devVariable, bool bOpen)
        {
            try
            {
                _master.WriteSingleCoil(Convert.ToUInt16(devVariable), bOpen);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool GetDeveiceSwitchState(DevVariable devVariable)
        {
            bool bstate = false;
            try
            {
                ushort numInputs = 1;
                bool[] inputs = _master.ReadCoils(Convert.ToUInt16(devVariable), numInputs);
                if (inputs != null && inputs.Count() >= numInputs)
                {
                    bstate = inputs[0];

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return bstate;
        }

        public ushort GetDeveiceStateValue(DevVariable deveiceSwitch)
        {
            ushort value = 0;
            try
            {
                ushort numInputs = 1;
                ushort[] inputs = null;
                if (Convert.ToUInt16(deveiceSwitch) > 40000)
                {
                    inputs = _master.ReadHoldingRegisters((ushort)(Convert.ToUInt16(deveiceSwitch) - 40000), numInputs);
                }
                else
                {
                    inputs = _master.ReadInputRegisters(Convert.ToUInt16(deveiceSwitch), numInputs);

                }
                if (inputs != null && inputs.Count() >= numInputs)
                {
                    value = inputs[0];

                }
            }
            catch (Exception)
            {

            }
            return value;
        }
        public bool SetDeveiceStateValue(DevVariable deveiceSwitch, ushort value)
        {

            try
            {


                if (Convert.ToUInt16(deveiceSwitch)>40000)
                {
                    _master.WriteSingleRegister((ushort)(Convert.ToUInt16(deveiceSwitch)-40000), value);
                }
                else
                {
                    _master.WriteSingleRegister(Convert.ToUInt16(deveiceSwitch), value);
                }
              


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public AIRSTATE[] GetAirState()
        {
            AIRSTATE[] airstates = null;
            try
            {
                ushort numInputs = (ushort)(3*Convert.ToUInt16(AIR.AIRCOUT));
                bool[] inputs = null;
                if (Convert.ToUInt16(DevVariable.OPAIR) > 40000)
                {
                    inputs = _master.ReadInputs(Convert.ToUInt16(DevVariable.OPAIR), numInputs);
                }
                else
                {
                    inputs = _master.ReadCoils(Convert.ToUInt16(DevVariable.OPAIR), numInputs);
                }
                if (inputs != null && inputs.Count() >= numInputs)
                {
                    airstates = new AIRSTATE[Convert.ToUInt16(AIR.AIRCOUT)];
                    for (int i = 0; i < Convert.ToUInt16(AIR.AIRCOUT); i++)
                    {
                        airstates[i] = inputs[i * 3] ? AIRSTATE.HIGH : (inputs[i * 3 + 1] ? AIRSTATE.LOW : AIRSTATE.NORMAL);
                    }
                }
            }
            catch (Exception)
            {

            }
            return airstates;
        }
    }
}
