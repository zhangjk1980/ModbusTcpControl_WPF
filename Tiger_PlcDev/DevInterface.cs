using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PlcDev
{
    public class DevInterface
    {

        public delegate void DevValue<T1, T2>(T1 t1, T2 t2);

        public event DevValue<LedGroup, bool> OnLedGroup = null;
        public event DevValue<AirGroup, AIRSTATE> OnAirGroup = null;
        public event DevValue<TimeGroup, DateTime> OnTimeGroup = null;
        public event DevValue<ACState, ACState> OnAcState = null;

        public delegate void ModbusDele(bool flag,Exception msg);
        public event ModbusDele OnModbusException = null;

        #region

        private RoomDev _roomDev = null;
        private AirCondition  _airCondition = null;
        private Thread _workThread = null;
        private bool _bExit = false;
        #endregion


        public void Initial()
        {
            _workThread = new Thread(WordThread);
        }
        public void SetRoomPlcParam(string ip, ushort port)
        {
            try
            {
                _roomDev = new RoomDev(ip, port);
            }
            catch
            {
                OnModbusException(false,new Exception("手术室辅助控制模块连接异常。"));
            }
        }
        public void SetAcPlcParam(string ip, ushort port)
        {
            try
            {
                _airCondition = new AirCondition(ip, port);
            }
            catch
            {
                OnModbusException(false, new Exception("空调机组模块连接异常。"));
            }
        }
        public void StartWork()
        {
           _bExit =false;
            _workThread.Start();
        }

        public void StopWork()
        {
            _bExit = true;
        }

        void WordThread()
        {
            while (!_bExit)
            {
                CheckLedGroup();
                //CheckAirGroup();
                CheckTimeGroup();
                //CheckAcState();
                Thread.Sleep(1000);
            }

        }

        void CheckLedGroup()
        {
            if (OnLedGroup != null)
            {
                bool b = _roomDev.GetDeveiceSwitchState(DevVariable.LAMP1);
                OnLedGroup(LedGroup.LAMP1, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.LAMP2);
                OnLedGroup(LedGroup.LAMP2, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.ORLED1);
                OnLedGroup(LedGroup.ORLED1, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.ORLED2);
                OnLedGroup(LedGroup.ORLED2, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.Viewbox);
                OnLedGroup(LedGroup.Viewbox, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.LampBack1);
                OnLedGroup(LedGroup.LampBack1, b);
                b = _roomDev.GetDeveiceSwitchState(DevVariable.LampBack2);
                OnLedGroup(LedGroup.LampBack2, b);
            }

        }

        void CheckAirGroup()
        {
            if (OnAirGroup != null)
            {
                AIRSTATE[] arAirstates= _roomDev.GetAirState();
                for (int i = 0; i < arAirstates.Count(); i++)
                {
                    AirGroup airGroup = (AirGroup)i;
                    OnAirGroup(airGroup, arAirstates[i]);
                }
             
            }
        }

        void CheckTimeGroup()
        {
            if (OnTimeGroup != null)
            {
                DateTime beijing = _roomDev.GetBeijingTime();
                OnTimeGroup(TimeGroup.BeijingTime,beijing);

                DateTime mazui = _roomDev.GetMazuiTime();
                OnTimeGroup(TimeGroup.MazuiTime, mazui);

                DateTime or = _roomDev.GetORTime();
                OnTimeGroup(TimeGroup.ORTime, or);
            }
        }

        void CheckAcState()
        {
            if (OnAcState !=null )
            {
                ACState acState = _airCondition.GetACState();
                OnAcState(acState, acState);
            }
        }

        public void OpenCloseAC(bool bopen)
        {
            _airCondition.ACSwitch(bopen);
        }
        public void OpenCloseACDuty(bool bopen)
        {
            _airCondition.ACDutySwitch(bopen);
        }
        public void SetRoomTemp(ushort temp)
        {
            _airCondition.SetRoomTemp(temp);
        }

        public void SetRoomHemp(ushort hemp)
        {
            _airCondition.SetRoomHemp(hemp);
        }

        public bool Switch(DevVariable devVariable, bool bOpen)
        {
            try
            {
                _roomDev.Switch(devVariable, bOpen);
            }
            catch (Exception)
            {
                OnModbusException(false,new Exception("设备["+devVariable.ToString()+"]切换失败。"));
                return false;
            }
            return true;
        }

        public void OpenCloseLed(LedGroup led,bool open)
        {
            DevVariable _devVariable;
            switch (led)
            {
                case LedGroup.LAMP1:
                    _devVariable = DevVariable.LAMP1;
                    break;
                case LedGroup.LAMP2:
                    _devVariable = DevVariable.LAMP2;
                    break;
                case LedGroup.ORLED1:
                    _devVariable = DevVariable.ORLED1;
                    break;
                case LedGroup.ORLED2:
                    _devVariable = DevVariable.ORLED2;
                    break;
                case LedGroup.Viewbox:
                    _devVariable = DevVariable.Viewbox;
                    break;
                case LedGroup.LampBack1:
                     _devVariable = DevVariable.LampBack1;
                    break;
                case LedGroup.LampBack2:
                     _devVariable = DevVariable.LampBack2;
                    break;
                default:
                    return;
            }

            _roomDev.Switch(_devVariable, open);
        }

        public void MazuiPositveCout(bool bOpen)
        {
            //_roomDev.Switch(DevVariable.MazuiNTime, false);
            _roomDev.Switch(DevVariable.MazuiPTime, bOpen);
            
          
            
        }
        public void MazuiNagitiveCout(bool bOpen)
        {
            //_roomDev.Switch(DevVariable.MazuiPTime, false);
            _roomDev.Switch(DevVariable.MazuiNTime, bOpen);
        }

        public void MazuiReset()
        {
            _roomDev.Switch(DevVariable.MazuiReset, true);
            _roomDev.Switch(DevVariable.MazuiReset, false);
        }
        /// <summary>
        /// 设置麻醉时间 -1表示设置
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        public void MazuiTimeSet(int hour, int min, int sec)
        {
            if (hour >=0)
            {
                _roomDev.SetDeveiceStateValue(DevVariable.MazuiTimeHour, (ushort)hour);
            }
            if (min >= 0)
            {
                _roomDev.SetDeveiceStateValue(DevVariable.MazuiTimeMin, (ushort)min);
            }
            if (sec >= 0)
            {
                _roomDev.SetDeveiceStateValue(DevVariable.MazuiTimeSec, (ushort)sec);
            }
        }
    }
}
