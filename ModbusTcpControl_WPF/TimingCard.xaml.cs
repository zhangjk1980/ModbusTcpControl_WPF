using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModbusTcpControl_WPF
{
    /// <summary>
    /// TimingCard.xaml 的交互逻辑
    /// </summary>
    public partial class TimingCard : UserControl
    {
        public TimingCard()
        {
            InitializeComponent();
        }
        #region[颜色]
        private string titleColor
        {
            get
            {
                return (string)GetValue(TitleColorProperty);
            }
            set
            {
                SetValue(TitleColorProperty, value);
            }
        }
        public static readonly DependencyProperty TitleColorProperty =
            DependencyProperty.Register("titleColor", typeof(string), typeof(TimingCard), new UIPropertyMetadata("Red"));

        private string timeColor
        {
            get
            {
                return (string)GetValue(MenuColorProperty);
            }
            set
            {
                SetValue(MenuColorProperty, value);
            }
        }
        public static readonly DependencyProperty MenuColorProperty =
            DependencyProperty.Register("menuColor", typeof(string), typeof(TimingCard), new UIPropertyMetadata("Black"));
        #endregion

    }
}
