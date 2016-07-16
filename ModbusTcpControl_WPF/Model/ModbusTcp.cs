using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class ModbusTcp
    {
        /// <summary>
        /// key
        /// </summary>
        private string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// IP
        /// </summary>
        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 端口
        /// </summary>
        private ushort port;
        public ushort Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// 设备地址
        /// </summary>
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
