using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    //地址类型
    public enum ModbusAddressType
    {
        Coil,//线圈
        Register,//寄存器
        others
    }

    public class ModbusBaseModel:ViewModelBase
    {
        public ModbusBaseModel()
        {
        }

        public ModbusBaseModel(ushort data)
        {
            this.data = data;
        }

        private string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 设备地址
        /// </summary>
        private ushort deviceAddress;
        public ushort DeviceAddress
        {
            get { return deviceAddress; }
            set { deviceAddress = value; }
        }
        
        /// <summary>
        /// 地址
        /// </summary>
        private ushort address;
        public ushort Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// 备用地址
        /// </summary>
        private ushort reserve;
        public ushort Reserve
        {
            get { return reserve; }
            set { reserve = value; }
        }
        
        /// <summary>
        /// 地址类型
        /// </summary>
        private ModbusAddressType aType;
        public ModbusAddressType AType
        {
            get { return aType; }
            set { aType = value; }
        }
        
        /// <summary>
        /// 数据
        /// </summary>
        private ushort data;
        public ushort Data
        {
            get { return data; }
            set { data = value;
                this.RaisePropertyChanged("Data"); }
        }
        
        /// <summary>
        /// 文本
        /// </summary>
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        
    }

    public class GasPressureBaseModel : ViewModelBase
    {
        private string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 设备地址
        /// </summary>
        private ushort deviceAddress;
        public ushort DeviceAddress
        {
            get { return deviceAddress; }
            set { deviceAddress = value; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        private ushort address;
        public ushort Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// 数据
        /// </summary>
        private GasPressureStatus data = GasPressureStatus.Normal;
        public GasPressureStatus Data
        {
            get { return data; }
            set { data = (GasPressureStatus)value; this.RaisePropertyChanged("Data"); }
        }

        /// <summary>
        /// 文本
        /// </summary>
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

    }
}
