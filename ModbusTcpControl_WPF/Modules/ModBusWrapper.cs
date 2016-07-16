using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpModBusExample
{
    public enum Protocol
    {
        TCPIP,
        SerialPort
    }

    public abstract class ModBusWrapper : IDisposable
    {
        private static ModBusWrapper _Instance;

        public static ModBusWrapper CreateInstance(Protocol protocol)
        {
            if (_Instance == null)
            {
                switch (protocol)
                {
                    case Protocol.TCPIP:
                        _Instance = new ModBusTCPIPWrapper();
                        break;
                    case Protocol.SerialPort:
                        _Instance = new ModBusSerialPortWrapper();
                        break;
                    default:
                        break;
                }
            }
            return _Instance;
        }


        #region Transaction Identifier
        /// <summary>
        /// 数据序号标识
        /// </summary>
        private byte dataIndex = 0;

        protected byte CurrentDataIndex
        {
            get { return this.dataIndex; }
        }

        protected byte NextDataIndex()
        {
            return ++this.dataIndex;
        }
        #endregion

        public ILog Logger { get; set; }

        public abstract void Connect();

        //public abstract byte[] Receive();

        //public abstract void Send(byte[] data);


        public abstract bool CoilSend(short registerAdd);

        public abstract bool CoilWriteMulit(short registerAdd, byte num, string data);

        public abstract bool CoilWrite(short registerAdd, bool open);


        public abstract bool HoldRegisterRead(short registerAdd, short num);

        public abstract bool HoldRegisterWrite(short registerAdd, short data);

        public abstract bool HoldRegisterWriteMulit(short registerAdd, byte num, short data);

        #region IDisposable 成员
        public virtual void Dispose()
        {
        }
        #endregion
    }
}
