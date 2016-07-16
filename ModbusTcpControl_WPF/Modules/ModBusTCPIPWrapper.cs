using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows;

namespace CSharpModBusExample
{
    public enum FunctionCode : byte
    {
        /*
            4个基本表：
         * 离散量输入     位       只读      I/O系统可提供这种类型数据
         * 线圈           位       读写      通过应用程序可改变这种类型数据
         * 输入寄存器    16位      只读      I/O系统可提供这种类型数据
         * 保持寄存器    16位      读写      通过应用程序可改变这种类型数据
         */

        /// <summary>
        /// 读取线圈状态，取得一组逻辑线圈的当前状态（ON/OFF)
        /// </summary>
        Read8RW = 1,

        /// <summary>
        /// 读取输入状态，取得一组开关输入的当前状态（ON/OFF)
        /// </summary>
        Read8R = 2,

        /// <summary>
        /// 读取保持寄存器，在一个或多个保持寄存器中取得当前的二进制值
        /// </summary>
        Read16RWM = 3,

        /// <summary>
        /// 读取输入寄存器 在一个或多个输入寄存器中取得当前的二进制值
        /// </summary>
        Read16RM = 4,

        /// <summary>
        /// 强置单线圈 强置一个逻辑线圈的通断状态
        /// </summary>
        Write8RZW = 5,

        /// <summary>
        /// 预置单寄存器 把具体二进值装入一个保持寄存器
        /// </summary>
        Write16S = 6,

/*
        /// <summary>
        /// 读取异常状态 取得 8 个内部线圈的通断状态，这 8 个线圈的地址由控制器决定，用户逻辑可以将这些线圈定义，以说明从机状态，短报文适宜于迅速读取状态
        /// </summary>
        Read = 7,

        /// <summary>
        /// 回送诊断校验 把诊断校验报文送从机，以对通信处理进行评鉴
        /// </summary>
        Read = 8,

        /// <summary>
        /// 编程（只用于 484 ）使主机模拟编程器作用，修改 PC 从机逻辑
        /// </summary>
        Read = 9,

        /// <summary>
        /// 控询（只用于 484 ）　　 可使主机与一台正在执行长程序任务从机通信，探询该从机是否已完成其操作任务，仅在含有功能码 9 的报文发送后，本功能码才发送
        /// </summary>
        Read = 10,

        /// <summary>
        /// 读取事件计数    可使主机发出单询问，并随即判定操作是否成功，尤其是该命令或其他应答产生通信错误时
        /// </summary>
        Read = 11,

        /// <summary>
        /// 读取通信事件记录  可是主机检索每台从机的 ModBus 事务处理通信事件记录。如果某项事务处理完成，记录会给出有关错误
        /// </summary>
        Read = 12,

        /// <summary>
        /// 编程（ 184/384 484 584 ） 可使主机模拟编程器功能修改 PC 从机逻辑
        /// </summary>
        Read = 13,

        /// <summary>
        /// 探询（ 184/384 484 584 ） 可使主机与正在执行任务的从机通信，定期控询该从机是否已完成其程序操作，仅在含有功能 13 的报文发送后，本功能码才得发送
        /// </summary>
        Read = 14,

        /// <summary>
        /// 强置多线圈 强置一串连续逻辑线圈的通断
        /// </summary>
        Read = 15,
*/
        /// <summary>
        /// 预置多寄存器 把具体的二进制值装入一串连续的保持寄存器
        /// </summary>
        Write16M = 16,
/*
        /// <summary>
        /// 报告从机标识 可使主机判断编址从机的类型及该从机运行指示灯的状态
        /// </summary>
        Read = 17,

        /// <summary>
        ///  884 和 MICRO 84 ） 可使主机模拟编程功能，修改 PC 状态逻辑
        /// </summary>
        Read = 18,

        /// <summary>
        /// 重置通信链路 发生非可修改错误后，是从机复位于已知状态，可重置顺序字节
        /// </summary>
        Read = 19,
*/
        /// <summary>
        /// 读取通用参数（ 584L ） 显示扩展存储器文件中的数据信息
        /// </summary>
        ReadFile = 20,

        /// <summary>
        /// 写入通用参数（ 584L ） 把通用参数写入扩展存储文件，或修改之
        /// </summary>
        WriteFile = 21,

        /// <summary>
        /// 屏蔽写寄存器
        /// </summary>
        ForbidWrite16 = 22,

        /// <summary>
        ///  读写多个寄存器
        /// </summary>
        ReadWrite16M = 23
    }

    internal class ModBusTCPIPWrapper : ModBusWrapper, IDisposable
    {
        public static ModBusTCPIPWrapper Instance = new ModBusTCPIPWrapper();
        private SocketWrapper socketWrapper = new SocketWrapper();
        bool connected = false;

        public override void Connect()
        {
            if (!connected)
            {
                this.socketWrapper.Logger = this.Logger;
                this.socketWrapper.Connect();
                this.connected = true;
            }
        }

        #region[线圈]

        public override bool CoilSend(short registerAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 单线圈读取 01
        /// </summary>
        /// <returns></returns>
        public bool CoilSend(short registerAdd,byte deviceAdd)
        {
            bool flag = false;
            try
            {
                //[0]:填充0，清掉剩余的寄存器
                
                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                values.AddRange(new Byte[] { 0, 0, 0, 6 });
                values.Add(deviceAdd);//设备地址
                values.Add(1);//功能码
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));
                values.AddRange(new Byte[]{0,1});

                ////[2].增加数据
                //values.AddRange(data);//14~End:需要发送的数据

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                //[5].读取Response: 写完后会返回10个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(10);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 写线圈 05
        /// </summary>
        /// <returns></returns>
        public override bool CoilWrite(short registerAdd,bool open)
        {
            bool flag = false;
            try
            {
                //[0]:填充0，清掉剩余的寄存器

                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                values.AddRange(new Byte[] { 0, 0, 0, 6, 1, 5 });
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));

                //[2].增加数据
                if(open)
                    values.AddRange(new Byte[]{255,0});//开
                else
                    values.AddRange(new Byte[] { 0, 0 });//关

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                //[5].读取Response: 写完后会返回10个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 写多个线圈 15
        /// </summary>
        /// <param name="registerAdd">qishi</param>
        /// <param name="num"></param>
        /// <param name="data">数据，高位在前</param>
        /// <returns></returns>
        public override bool CoilWriteMulit(short registerAdd,byte num,string data)
        {
            bool flag = false;
            try
            {
                //[0]:填充0，清掉剩余的寄存器

                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                values.AddRange(new Byte[] { 0, 0,0,6});//协议码
                values.AddRange(new Byte[]{ 1, 15 });//丛站标识符、功能码
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));//起始规约地址

                byte dbytes = (byte)(num / 8) ;
                if( num % 8 != 0 )
                    dbytes ++;
                values.Add(dbytes);//字节数
                
                //[2].增加数据
                //位数整理
                var m =data.Length % 8;

                int t = data.Length / 8;
                for (int i = 0; i < t; i++)
                {
                    var temp = Convert.ToByte(data.Substring(8 * t, 8), 2);
                    values.Add(temp);
                }
                if ( m != 0)
                {
                    var temp = Convert.ToByte(data.Substring(8 * t, m), 2);
                    values.Add(temp);
                }

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                ////[4].防止连续读写引起前台UI线程阻塞
                //Application.DoEvents();

                //[5].读取Response: 写完后会返回10个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(9);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 线圈接收
        /// </summary>
        /// <returns></returns>
        public bool CoilReceive()
        {
            bool flag = false;
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }
#endregion

#region[寄存器]

        /// <summary>
        /// 设置单个保持寄存器 6
        /// </summary>
        /// <returns></returns>
        public override bool HoldRegisterWrite(short registerAdd, short data)
        {
            bool flag = false;
            try
            {
                //[0]:填充0，清掉剩余的寄存器

                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                //values.AddRange(new Byte[] { 0, 0 });//3~4:Protocol Identifier,0 = MODBUS protocol
                //values.AddRange(ValueHelper.Instance.GetBytes((byte)6));//5~6:后续的Byte数量
                //values.Add(0);//7:Unit Identifier:This field is used for intra-system routing purpose.
                //values.Add((byte)FunctionCode.Read8RW);//读线圈
                //values.AddRange(ValueHelper.Instance.GetBytes(StartingAddress));//9~10.起始地址
                //values.AddRange(ValueHelper.Instance.GetBytes((short)1));//11~12.寄存器数量
                //values.Add((byte)1);//13.数据的Byte数量
                values.AddRange(new Byte[] { 0, 0, 0, 6, 1, 6 });
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));

                //[2].增加数据
                values.AddRange(ValueHelper.Instance.GetBytes(data));//14~End:需要发送的数据

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                //[5].读取Response: 写完后会返回13个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(12);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 写多个保持寄存器 16
        /// </summary>
        /// <returns></returns>
        public override bool HoldRegisterWriteMulit(short registerAdd, byte num, short data)
        {
            bool flag = false;
            try
            {
                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                values.AddRange(new Byte[] { 0, 0, 0, 9, 1, 16 });
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));
                values.AddRange(ValueHelper.Instance.GetBytes(num));
                byte dbytes = (byte)(num << 1);
                values.Add(dbytes);//1个字节

                //[2].增加数据
                values.AddRange(ValueHelper.Instance.GetBytes(data));//14~End:需要发送的数据

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                ////[4].防止连续读写引起前台UI线程阻塞
                //Application.DoEvents();

                //[5].读取Response: 写完后会返回13个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(12);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 读取一个或多个保持寄存器 3
        /// </summary>
        /// <returns></returns>
        public override bool HoldRegisterRead(short registerAdd, short num)
        {
            bool flag = false;
            try
            {
                //[0]:填充0，清掉剩余的寄存器

                this.Connect();
                List<byte> values = new List<byte>(255);

                //[1].Write Header：MODBUS Application Protocol header
                values.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
                values.AddRange(new Byte[] { 0, 0, 0, 6, 1, 3 });
                values.AddRange(ValueHelper.Instance.GetBytes(registerAdd));
                values.AddRange(ValueHelper.Instance.GetBytes(num));

                //[3].写数据
                this.socketWrapper.Write(values.ToArray());

                ////[4].防止连续读写引起前台UI线程阻塞
                //Application.DoEvents();

                //[5].读取Response: 写完后会返回13个byte的结果
                byte[] responseHeader = this.socketWrapper.Read(9 + 2 * num);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 线圈接收
        /// </summary>
        /// <returns></returns>
        public bool RegisterReceive()
        {
            bool flag = false;
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }
#endregion


        #region IDisposable 成员
        public override void Dispose()
        {
            socketWrapper.Dispose();
        }

        
        #endregion
    }
}
