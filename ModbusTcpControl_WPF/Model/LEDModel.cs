using GalaSoft.MvvmLight.Command;
using ModbusTcpControl_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class LEDModel
    {
        /// <summary>
        /// 文本
        /// </summary>
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        private string ico;
        public string ICO
        {
            get { return "Pictures/" + ico; }
            set { ico = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        private ModbusBaseModel data = null;
        public ModbusBaseModel Data
        {
            get { return data; }
            set { data = value; }
        }

        #region Command with multiple parameters
        private RelayCommand sendCommand;
        /// <summary>
        /// 命令通信
        /// </summary>
        public RelayCommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                    sendCommand = new RelayCommand(SendCommandFunc);
                return sendCommand;
            }
        }

        /// <summary>
        /// 通信方法
        /// </summary>
        /// <param name="m"></param>
        private void SendCommandFunc()
        {
           var value = this.Data.Data == 0 ? true : false;
            MainViewModel.devI.Switch((PlcDev.DevVariable)Data.Address,value);
        }
        #endregion
    }
}
