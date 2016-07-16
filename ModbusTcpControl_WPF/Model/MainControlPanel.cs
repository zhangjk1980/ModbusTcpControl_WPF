using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class MainControlPanel
    {
        /// <summary>
        /// 手术正计时
        /// </summary>
        private ushort myOperationTiming;
        public ushort MyOperationTiming
        {
            get { return myOperationTiming; }
            set { myOperationTiming = value; }
        }

        /// <summary>
        /// 麻醉倒计时
        /// </summary>
        private ushort myAnesthesiaCountDown;
        public ushort MyAnesthesiaCountDown
        {
            get { return myAnesthesiaCountDown; }
            set { myAnesthesiaCountDown = value; }
        }

        /// <summary>
        /// 时间
        /// </summary>
        private DateoperationModel myDate;
        public DateoperationModel MyDate
        {
            get { return myDate; }
            set { myDate = value; }
        }

        /// <summary>
        /// 医用气体气压状态
        /// </summary>
        private ushort myTime;
        public ushort MyTime
        {
            get { return myTime; }
            set { myTime = value; }
        }

        /// <summary>
        /// 空调系统状态
        /// </summary>
        private AirCondition myAirCondition;
        public AirCondition MyCondition
        {
            get { return myAirCondition; }
            set { myAirCondition = value; }
        }

        /// <summary>
        /// 酒精报警
        /// </summary>
        private bool myAlcoholWarming;
        public bool MyAlcoholWarming
        {
            get { return myAlcoholWarming; }
            set { myAlcoholWarming = value; }
        }

        /// <summary>
        /// IT电源正常
        /// </summary>
        private bool myITPowerNormal;
        public bool MyITPowerNormal
        {
            get { return myITPowerNormal; }
            set { myITPowerNormal = value; }
        }

        #region[]
        /// <summary>
        /// 照明1
        /// </summary>
        private LEDModel myFloodlight_1;
        public LEDModel MyFloodlight_1
        {
            get { return myFloodlight_1; }
            set { myFloodlight_1 = value; }
        }

        /// <summary>
        /// 照明2
        /// </summary>
        private LEDModel myFloodlight_2;
        public LEDModel MyFloodlight_2
        {
            get { return myFloodlight_2; }
            set { myFloodlight_2 = value; }
        }

        /// <summary>
        /// 排风机
        /// </summary>
        private LEDModel myExhaustFan;
        public LEDModel MyExhaustFan
        {
            get { return myExhaustFan; }
            set { myExhaustFan = value; }
        }

        /// <summary>
        /// 排排废气
        /// </summary>
        private LEDModel myExhaustGas;
        public LEDModel MyExhaustGas
        {
            get { return myExhaustGas; }
            set { myExhaustGas = value; }
        }

        /// <summary>
        /// 观片灯
        /// </summary>
        private LEDModel myFilmViewer;
        public LEDModel MyFilmViewer
        {
            get { return myFilmViewer; }
            set { myFilmViewer = value; }
        }

        /// <summary>
        /// 无影灯
        /// </summary>
        private LEDModel myShadowlessLamp;
        public LEDModel MyShadowlessLamp
        {
            get { return myShadowlessLamp; }
            set { myShadowlessLamp = value; }
        }

        /// <summary>
        /// 手术中
        /// </summary>
        private LEDModel myDUTOperation;
        public LEDModel MyDUTOperation
        {
            get { return myDUTOperation; }
            set { myDUTOperation = value; }
        }
        #endregion
    }
}
