using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ModbusTcpControl_WPF.Model;
using Song.XML.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using plc = PlcDev;

namespace ModbusTcpControl_WPF.ViewModel
{
    /// <summary>
    /// 主ViewModel
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string title = "智发数字化手术室集中控制系统";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public  static plc.DevInterface devI = new plc.DevInterface();

        private bool modbusExceptionFlag;
        public bool ModbusExceptionFlag
        {
            get { return modbusExceptionFlag; }
            set { modbusExceptionFlag = value; }
        }

        private string message;
        /// <summary>
        /// 显示信息
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //private plc.RoomDev rd = null;
        //private plc.AirCondition ar = null;

        private DispatcherTimer timeTimer = new DispatcherTimer();
        private DispatcherTimer ledTimer = new DispatcherTimer();
        private DispatcherTimer gasTimer = new DispatcherTimer();
        private DispatcherTimer acTimer = new DispatcherTimer();

        //public MainControlPanel mcpModel = new MainControlPanel();

        private List<ModbusTcp> ls = new List<ModbusTcp>();
        private Dictionary<string,ModbusTcp> dmt = new Dictionary<string, ModbusTcp>(2);

        private ObservableCollection<LEDModel> warmingList = new ObservableCollection<LEDModel>();//报警显示
        public ObservableCollection<LEDModel> WarmingList
        {
            get { return warmingList; }
            set { warmingList = value; }
        }

        private ObservableCollection<LEDModel> ledList = new ObservableCollection<LEDModel>();//手术室灯列表
        public ObservableCollection<LEDModel> LedList
        {
            get { return ledList; }
            set { ledList = value; }
        }
        private ObservableCollection<GasPressureModel> gaspList = new ObservableCollection<GasPressureModel>();//手术室灯列表
        public ObservableCollection<GasPressureModel> GaspList
        {
            get { return gaspList; }
            set { gaspList = value; }
        }

        private AirCondition ac = new AirCondition();//空调机组
        public AirCondition Ac
        {
            get { return ac; }
            set { ac = value; }
        }

        private Dictionary<string, ModbusBaseModel> coilD = null;
        private Dictionary<string, ModbusBaseModel> registerD = null;

        //public DateTime OperationPD = new DateTime();
        private TimOperation operationPTime = new TimOperation();//手术正计时
        public TimOperation OperationPTime
        {
            get { return operationPTime; }
            set { operationPTime = value; }
        }

        //public DateTime AnesthesiologyND = new DateTime();
        private TimOperation anesthesiologyNTime = new TimOperation();//麻醉倒计时
        public TimOperation AnesthesiologyNTime
        {
            get { return anesthesiologyNTime; }
            set { anesthesiologyNTime = value; }
        }
        
        private DateoperationModel date = new DateoperationModel();//时间显示--取设备内部时间
        public DateoperationModel Date
        {
            get { return date; }
            set { date = value; }
        }
        
        /// <summary>
        /// 读取xml配置信息
        /// </summary>
        private void readConfig()
        {
            //时间 LedMenuList
            ConfigHelper helper = new ConfigHelper();

            //读取初始化信息
            //ls = helper.GetAllDeviceConfigValue();
            dmt = helper.GetNodeConfigValue<ModbusTcp>("DeviceList", "Device");

            coilD = helper.GetNodeConfigValue<ModbusBaseModel>("CoilList", "Coil");
            registerD = helper.GetNodeConfigValue<ModbusBaseModel>("RegisterList", "Register");

            helper.GetNodeConfigValue<LEDModel, ModbusBaseModel>("WarmingList", "Menu", WarmingList, coilD);
            helper.GetNodeConfigValue<LEDModel,ModbusBaseModel>("LedMenuList", "Menu", LedList,coilD);
            //this.RaisePropertyChanged("LedList");
            helper.GetNodeConfigValue<GasPressureModel, ModbusBaseModel>("GasList", "Gas", GaspList, coilD);

            //手术时间
            operationPTime.Hour = registerD["OHour"];
            operationPTime.Minute = registerD["OMinute"];
            operationPTime.Second = registerD["OSecond"];
            //麻醉时间
            anesthesiologyNTime.Hour = registerD["AHour"];
            anesthesiologyNTime.Minute = registerD["AMinute"];
            anesthesiologyNTime.Second = registerD["ASecond"];

            //北京时间
            date.Year = registerD["BeiJingYear"];
            date.Month = registerD["BeiJingMonth"];
            date.Day = registerD["BeiJingDay"];
            date.Hour = registerD["BeiJingHour"];
            date.Minute = registerD["BeiJingMinute"];
            date.Second = registerD["BeiJingSecond"];
            date.Week = registerD["BeiJingWeek"];
        }

        private void DevI_ModbusException(bool flag, Exception msg)
        {
            ModbusExceptionFlag = true;
            Message = msg.Message;
        }

        private void DevI_OnTimeGroup(plc.TimeGroup t1, DateTime t2)
        {
            switch (t1)
            {
                case plc.TimeGroup.BeijingTime:
                    Date.SetDate(t2);
                    break;
                case PlcDev.TimeGroup.MazuiTime:
                    AnesthesiologyNTime.SetTime(t2);
                    break;
                case PlcDev.TimeGroup.ORTime:
                    OperationPTime.SetTime(t2);
                    break;
            }
        }

        private void DevI_OnLedGroup(plc.LedGroup t1, bool t2)
        {
            try
            {
                var key = Enum.GetName(typeof(plc.LedGroup), t1);
                var m = coilD[key];
                m.Data = t2 ? (ushort)1 : (ushort)0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 气体
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        private void DevI_OnAirGroup(plc.AirGroup t1, plc.AIRSTATE t2)
        {
            ushort status = 0;
            switch (t2)
            {
                case PlcDev.AIRSTATE.HIGH:
                    status = 2;
                    break;
                case PlcDev.AIRSTATE.NORMAL:
                    status = 1;
                    break;
                case PlcDev.AIRSTATE.LOW:
                    status = 0;
                    break;
            }

            try
            {
                var key = Enum.GetName(typeof(plc.AirGroup), t1);
                var m = coilD[key];
                m.Data = status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void DevI_OnAcState(plc.ACState t1, plc.ACState t2)
        {
            try
            {
                //t2.AcAlarm.
                var key = Enum.GetName(typeof(plc.ACState), t1);
                var m = coilD[key];
                //m.Data = t2 ? (ushort)0 : (ushort)1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region 定时器
        private void TimeInit()
        {
            timeTimer.Tick += new EventHandler(timeTimer_Tick);
            timeTimer.Interval = TimeSpan.FromMilliseconds(500);

            ledTimer.Tick += new EventHandler(ledTimer_Tick);
            ledTimer.Interval = TimeSpan.FromMilliseconds(500);

            gasTimer.Tick += new EventHandler(gasTimer_Tick);
            gasTimer.Interval = TimeSpan.FromMilliseconds(500);

            acTimer.Tick += new EventHandler(acTimer_Tick);
            acTimer.Interval = TimeSpan.FromSeconds(1);
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void ledTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void gasTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void acTimer_Tick(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }
        #region 事件
        #region 加载
        private RelayCommand loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                if (loadedCommand == null)
                    loadedCommand = new RelayCommand(LoadedCommandFunc);
                return loadedCommand;
            }
        }
        private void LoadedCommandFunc()
        {
            readConfig();
            
            //#region[设备初始化]
            //devI.Initial();

            ////rd = new PlcDev.RoomDev(dmt["RoomDev"].IP, dmt["RoomDev"].Port);
            //devI.SetRoomPlcParam(dmt["RoomDev"].IP, dmt["RoomDev"].Port);
            ////ar = new PlcDev.AirCondition(dmt["AirCondition"].IP, dmt["AirCondition"].Port);
            ////devI.SetAcPlcParam(dmt["AirCondition"].IP, dmt["AirCondition"].Port);

            //devI.OnAcState += DevI_OnAcState;
            //devI.OnAirGroup += DevI_OnAirGroup;
            //devI.OnLedGroup += DevI_OnLedGroup;
            //devI.OnTimeGroup += DevI_OnTimeGroup;
            //devI.OnModbusException += DevI_ModbusException;
            //#endregion
            
            //if (devI == null)
            //    return;
            //devI.StartWork();
        }
        #endregion

        #region 关闭
        private RelayCommand closedCommand;
        public RelayCommand ClosedCommand
        {
            get
            {
                if (closedCommand == null)
                    closedCommand = new RelayCommand(ClosedCommandFunc);
                return loadedCommand;
            }
        }
        private void ClosedCommandFunc()
        {
            if (devI == null)
                return;
            devI.StopWork();
        }
        #endregion

        #endregion
    }
}