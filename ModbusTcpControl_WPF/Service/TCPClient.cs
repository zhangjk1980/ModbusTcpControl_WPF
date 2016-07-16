using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPSocketCS;
using Song.XML.Files;
using ModbusTcpControl_WPF.Model;

namespace ModbusTcpControl_WPF.Service
{
    public enum AppState
    {
        Starting, Started, Stoping, Stoped, Error
    }

    public class TCPClient
    {
        //创建Client
        //获取连接信息
        private ConfigHelper chelper = new ConfigHelper();
        private List<ModbusTcp> ls = new List<ModbusTcp>();
        private List<TcpClient> clientLs = new List<TcpClient>();

        private AppState appState = AppState.Stoped;

        public TCPClient()
        {
            try
            {
                //读取初始化信息
                ls = chelper.GetAllDeviceConfigValue();
                start();                

                // 设置包头标识,与对端设置保证一致性
                //client.PackHeaderFlag = 0;
                // 设置最大封包大小
                // client.MaxPackSize = 0x1000;

                SetAppState(AppState.Stoped);
            }
            catch (Exception ex)
            {
                SetAppState(AppState.Error);
                throw ex;
            }
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <param name="deviceIndex"></param>
        private void send(string data,byte id,byte deviceIndex)
        {
            try
            {
                string send = data;
                if (string.IsNullOrEmpty(send))
                {
                    return;
                }

                byte[] bytes = Encoding.Default.GetBytes(send);
                IntPtr connId = clientLs[deviceIndex].ConnectionId;

                // 发送
                if (clientLs[deviceIndex].Send(bytes, bytes.Length))
                {
                    
                }
                else
                {
                    SetAppState(AppState.Error);
                }

            }
            catch (Exception ex)
            {
            }

        }


        HandleResult OnPrepareConnect(TcpClient sender, uint socket)
        {
            return HandleResult.Ok;
        }

        HandleResult OnConnect(TcpClient sender)
        {
            // 已连接 到达一次
            SetAppState(AppState.Started);

            return HandleResult.Ok;
        }

        HandleResult OnSend(TcpClient sender, byte[] bytes)
        {
            return HandleResult.Ok;
        }

        HandleResult OnReceive(TcpClient sender, byte[] bytes)
        {
            return HandleResult.Ok;
        }

        HandleResult OnClose(TcpClient sender, SocketOperation enOperation, int errorCode)
        {
            SetAppState(AppState.Stoped);

            return HandleResult.Ok;
        }

        /// <summary>
        /// 设置程序状态
        /// </summary>
        /// <param name="state"></param>
        void SetAppState(AppState state)
        {
            appState = state;
        }

        private void destroyClients()
        {
            foreach(var item in clientLs)
                item.Destroy();
        }
    }
}
