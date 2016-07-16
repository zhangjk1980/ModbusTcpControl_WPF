using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlcDev
{
    public enum LedGroup:int
    {
        LAMP1 = 0 ,             //照明灯1开关
        LAMP2 ,            //照明灯2开关
        ORLED1 ,           //无影灯1开关
        ORLED2 ,          //无影灯2开关
        Viewbox          //观片灯开关
        ,LampBack1         //备用1开关
        ,LampBack2         //备用2开关
        //,LedGroupCount
    }

    public enum AirGroup:int
    {
        AIR = 0, NP, AR, N2, CO2, AIRCOUT
    }

    public enum TimeGroup 
    {
        BeijingTime,
        ORTime,
        MazuiTime
    }

    public enum AC
    {
        Temp,
        Hemp
    }
}
