using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class TimOperation:ViewModelBase
    {

        public void SetTime(DateTime dt)
        {
            if (hour == null)
                return;
            this.Hour.Data = (UInt16)dt.Hour;
            this.Minute.Data = (UInt16)dt.Minute;
            this.Second.Data = (UInt16)dt.Second;
        }

        /// <summary>
        /// 时
        /// </summary>
        private ModbusBaseModel hour = new ModbusBaseModel(12);
        public ModbusBaseModel Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        /// <summary>
        /// 分
        /// </summary>
        private ModbusBaseModel minute = new ModbusBaseModel(10);
        public ModbusBaseModel Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        /// <summary>
        /// 秒
        /// </summary>
        private ModbusBaseModel second = new ModbusBaseModel(20);
        public ModbusBaseModel Second
        {
            get { return second; }
            set { second = value; }
        }

        public bool Start()
        {
            bool flag = false;
            try
            {
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine("启动失败。"+ex.Message);
            }
            return flag;
        }

        public bool Pause()
        {
            bool flag = false;
            try
            {
                flag = true;
            }
            catch
            {
                flag = false;
                Console.WriteLine("启动失败。");
            }
            return flag;
        }

        public bool Stop()
        {
            bool flag = false;
            try
            {
                flag = true;
            }
            catch
            {
                flag = false;
                Console.WriteLine("启动失败。");
            }
            return flag;
        }

        #region Command with multiple parameters
        private RelayCommand<ModbusBaseModel> startCommand;
        /// <summary>
        /// 启动
        /// </summary>
        public RelayCommand<ModbusBaseModel> StartCommand
        {
            get
            {
                if (startCommand == null)
                    startCommand = new RelayCommand<ModbusBaseModel>(m => StartCommandFunc(m));
                return startCommand;
            }
        }
        private void StartCommandFunc(ModbusBaseModel m)
        {
        }

        private RelayCommand<ModbusBaseModel> pauseCommand;
        /// <summary>
        /// 暂停
        /// </summary>
        public RelayCommand<ModbusBaseModel> PauseCommand
        {
            get
            {
                if (pauseCommand == null)
                    pauseCommand = new RelayCommand<ModbusBaseModel>(m => PauseCommandFunc(m));
                return pauseCommand;
            }
        }
        private void PauseCommandFunc(ModbusBaseModel m)
        {
        }

        private RelayCommand<ModbusBaseModel> stopCommand;
        /// <summary>
        /// 停止
        /// </summary>
        public RelayCommand<ModbusBaseModel> StopCommand
        {
            get
            {
                if (stopCommand == null)
                    stopCommand = new RelayCommand<ModbusBaseModel>(m => StopCommandFunc(m));
                return stopCommand;
            }
        }
        private void StopCommandFunc(ModbusBaseModel m)
        {
        }

        private RelayCommand<ModbusBaseModel> resetCommand;
        /// <summary>
        /// 复位
        /// </summary>
        public RelayCommand<ModbusBaseModel> ResetCommand
        {
            get
            {
                if (resetCommand == null)
                    resetCommand = new RelayCommand<ModbusBaseModel>(m => ResetCommandFunc(m));
                return resetCommand;
            }
        }
        private void ResetCommandFunc(ModbusBaseModel m)
        {
        }
        #endregion
    }
}
