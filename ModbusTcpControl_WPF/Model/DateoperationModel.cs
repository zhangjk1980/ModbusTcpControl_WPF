using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTcpControl_WPF.Model
{
    public class DateoperationModel:ViewModelBase
    {
        public void SetDate(DateTime dt)
        {
            if (hour == null)
                return;
            this.Year.Data = (UInt16)dt.Year;
            this.Month.Data = (UInt16)dt.Month;
            this.Day.Data = (UInt16)dt.Day;

            this.Week.Data = (UInt16)dt.DayOfWeek;//0-6

            this.Hour.Data = (UInt16)dt.Hour;
            this.Minute.Data = (UInt16)dt.Minute;
            this.Second.Data = (UInt16)dt.Second;
        }

        /// <summary>
        /// 年
        /// </summary>
        private ModbusBaseModel year = new ModbusBaseModel(2016);
        public ModbusBaseModel Year
        {
            get { return year; }
            set { year = value; }
        }

        /// <summary>
        /// 月
        /// </summary>
        private ModbusBaseModel month = new ModbusBaseModel(7);
        public ModbusBaseModel Month
        {
            get { return month; }
            set { month = value; }
        }

        /// <summary>
        /// 日
        /// </summary>
        private ModbusBaseModel day = new ModbusBaseModel(15);
        public ModbusBaseModel Day
        {
            get { return day; }
            set { day = value; }
        }

        /// <summary>
        /// 星期
        /// </summary>
        private ModbusBaseModel week = new ModbusBaseModel(4);
        public ModbusBaseModel Week
        {
            get { return week; }
            set { week = value; }
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
        private ModbusBaseModel minute = new ModbusBaseModel(6);
        public ModbusBaseModel Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        /// <summary>
        /// 秒
        /// </summary>
        private ModbusBaseModel second = new ModbusBaseModel(1);
        public ModbusBaseModel Second
        {
            get { return second; }
            set { second = value; }
        }

        /// <summary>
        /// 计数
        /// </summary>
        private ModbusBaseModel count = new ModbusBaseModel();
        public ModbusBaseModel Count
        {
            get { return count; }
            set { count = value; }
        }
        
        /// <summary>
        /// 校时
        /// </summary>
        /// <returns></returns>
        public bool Adjust()
        {
            bool flag = false;
            try
            {
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                Console.WriteLine(ex.Message);
            }
            return flag;
        }
    }
}
