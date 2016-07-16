using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public enum GasPressureStatus
    {
        Normal,
        Lower,
        Heigher
    }

    public class GasPressureModel
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
        /// 数据
        /// </summary>
        private ModbusBaseModel data = new ModbusBaseModel();
        public ModbusBaseModel Data
        {
            get { return data; }
            set { data = value; }
        }
    }

    public class GasPressure
    {
        /// <summary>
        /// 氧气
        /// </summary>
        private ushort myOxygen;
        public ushort MyOxygen
        {
            get { return myOxygen; }
            set { myOxygen = value; }
        }

        /// <summary>
        /// 氮气
        /// </summary>
        private ushort myNitrogen;
        public ushort MyNitrogen
        {
            get { return myNitrogen; }
            set { myNitrogen = value; }
        }

        /// <summary>
        /// 笑气
        /// </summary>
        private ushort myNitrousOxide;
        public ushort MyNitrousOxide
        {
            get { return myNitrousOxide; }
            set { myNitrousOxide = value; }
        }

        /// <summary>
        /// 氩气
        /// </summary>
        private ushort myArgon;
        public ushort MyArgon
        {
            get { return myArgon; }
            set { myArgon = value; }
        }

        /// <summary>
        /// 压缩空气
        /// </summary>
        private ushort myCompressedAir;
        public ushort MyCompressedAir
        {
            get { return myCompressedAir; }
            set { myCompressedAir = value; }
        }

        /// <summary>
        /// 二氧化碳
        /// </summary>
        private ushort myCarbonDioxide;
        public ushort MyCarbonDioxide
        {
            get { return myCarbonDioxide; }
            set { myCarbonDioxide = value; }
        }

        /// <summary>
        /// 负压吸引
        /// </summary>
        private ushort myNegativePressure;
        public ushort MyNegativePressure
        {
            get { return myNegativePressure; }
            set { myNegativePressure = value; }
        }
    }
}
