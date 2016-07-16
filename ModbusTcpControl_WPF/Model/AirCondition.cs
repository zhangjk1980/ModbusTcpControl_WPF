using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class AirCondition
    {
        /// <summary>
        /// 温度
        /// </summary>
        private ModbusBaseModel template = new ModbusBaseModel();
        public ModbusBaseModel Template
        {
            get { return template; }
            set { template = value; }
        }

        /// <summary>
        /// 湿度
        /// </summary>
        private ModbusBaseModel humidity = new ModbusBaseModel();
        public ModbusBaseModel Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        #region[空调系统状态]
        /// <summary>
        /// 系统正常
        /// </summary>
        private ModbusBaseModel sysNormal = new ModbusBaseModel();
        public ModbusBaseModel SysNormal
        {
            get { return sysNormal; }
            set { sysNormal = value; }
        }
        /// <summary>
        /// 值机
        /// </summary>
        private ModbusBaseModel onDuty = new ModbusBaseModel();
        public ModbusBaseModel OnDuty
        {
            get { return onDuty; }
            set { onDuty = value; }
        }
        /// <summary>
        /// 系统故障
        /// </summary>
        private ModbusBaseModel sysException = new ModbusBaseModel();
        public ModbusBaseModel SysException
        {
            get { return sysException; }
            set { sysException = value; }
        }
        /// <summary>
        /// 高效堵塞
        /// </summary>
        private ModbusBaseModel hepaBlock = new ModbusBaseModel();
        public ModbusBaseModel HepaBlock
        {
            get { return hepaBlock; }
            set { hepaBlock = value; }
        }
        /// <summary>
        /// 负压运行
        /// </summary>
        private ModbusBaseModel negativePressure = new ModbusBaseModel();
        public ModbusBaseModel NegativePressure
        {
            get { return negativePressure; }
            set { negativePressure = value; }
        }
        #endregion

        #region Command with multiple parameters

        #region[启停]
        private RelayCommand<ModbusBaseModel> startStopCommand;
        /// <summary>
        /// 命令通信
        /// </summary>
        public RelayCommand<ModbusBaseModel> StartStopCommand
        {
            get
            {
                if (startStopCommand == null)
                    startStopCommand = new RelayCommand<ModbusBaseModel>(m => StartStopCommandFunc(m));
                return startStopCommand;
            }
        }

        /// <summary>
        /// 通信方法
        /// </summary>
        /// <param name="m"></param>
        private void StartStopCommandFunc(ModbusBaseModel m)
        {
        }
        #endregion

        #region[值机]
        private RelayCommand<ModbusBaseModel> dutyCommand;
        /// <summary>
        /// 命令通信
        /// </summary>
        public RelayCommand<ModbusBaseModel> DutyCommand
        {
            get
            {
                if (dutyCommand == null)
                    dutyCommand = new RelayCommand<ModbusBaseModel>(m => DutyCommandFunc(m));
                return dutyCommand;
            }
        }

        /// <summary>
        /// 通信方法
        /// </summary>
        /// <param name="m"></param>
        private void DutyCommandFunc(ModbusBaseModel m)
        {
        }
        #endregion

        #region[正负压 Positive and negative pressure]
        private RelayCommand<ModbusBaseModel> pnPressureCommand;
        /// <summary>
        /// 命令通信
        /// </summary>
        public RelayCommand<ModbusBaseModel> PNPressureCommand
        {
            get
            {
                if (pnPressureCommand == null)
                    pnPressureCommand = new RelayCommand<ModbusBaseModel>(m => PnPressureCommandFunc(m));
                return pnPressureCommand;
            }
        }

        /// <summary>
        /// 通信方法
        /// </summary>
        /// <param name="m"></param>
        private void PnPressureCommandFunc(ModbusBaseModel m)
        {
        }
        #endregion

        #endregion
    }
}
