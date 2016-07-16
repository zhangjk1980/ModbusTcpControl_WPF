using GalaSoft.MvvmLight.Command;
using ModbusTcpControl_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModbusTcpControl_WPF.ViewModel
{
    public class TemplateViewModel
    {
        public AirCondition acMode = new AirCondition(); 

        #region Command without parameters
        private RelayCommand showText;
        public RelayCommand ShowText
        {
            get
            {
                if (showText == null)
                    showText = new RelayCommand(ShowTextFunc);
                return showText;
            }
        }
        public RelayCommand PassEvent { get; set; }

        private void ShowTextFunc()
        {
            MessageBox.Show("I am RealyCommand!");
        }
        #endregion

        #region Command with a parameter 
        private RelayCommand<int> showValue;
        public RelayCommand<int> ShowValue
        {
            get
            {
                if (showValue == null)
                    showValue = new RelayCommand<int>(x => ShowValueFunc(x));
                return showValue;
            }
        }

        private int ShowValueFunc(int a)
        {
            int c = a + 10;
            MessageBox.Show(c.ToString());
            return c;
        }
        #endregion

        #region Command with multiple parameters
        private RelayCommand<BookItem> showBooks;
        public RelayCommand<BookItem> ShowBooks
        {
            get
            {
                if (showBooks == null)
                    showBooks = new RelayCommand<BookItem>(x => ShowBooksFunc(x));
                return showBooks;
            }
        }

        private void ShowBooksFunc(BookItem bookItem)
        {
            MessageBox.Show(bookItem.BName + "|" + bookItem.BAuthor);
        }
        #endregion

        #region[温度]
        /// <summary>
        /// 温度调节按钮
        /// </summary>
        private RelayCommand<bool> addMinusTemperature;
        public RelayCommand<bool> AddMinusTemperature
        {
            get
            {
                if (addMinusTemperature == null)
                    addMinusTemperature = new RelayCommand<bool>(x => AddMinusTemperatureFunc(x));
                return addMinusTemperature;
            }
        }

        private void AddMinusTemperatureFunc(bool isAdd)
        {
            //获取温度并写入
        }
        #endregion

        #region
        /// <summary>
        /// 湿度调节按钮
        /// </summary>
        private RelayCommand<bool> addMinusHumidity;
        public RelayCommand<bool> AddMinusHumidity
        {
            get
            {
                if (addMinusHumidity == null)
                    addMinusHumidity = new RelayCommand<bool>(x => AddMinusHumidityFunc(x));
                return addMinusTemperature;
            }
        }

        private void AddMinusHumidityFunc(bool isAdd)
        {
            //获取湿度并写入
        }
        #endregion

        #region[空调机组状态]
        //状态显示
        //状态控制
        #endregion

        //刷新
        private void refresh()
        {
        }

    }
}
