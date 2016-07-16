using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

using Song.Txt.Files;
using ModbusTcpControl_WPF.Model;
using Song.Extensions;

namespace Song.XML.Files
{
    public class ConfigHelper
    {
        #region [ 方法: 获得配置文件中的值 ]

        private string bootPath = AppDomain.CurrentDomain.BaseDirectory;
        private string logPath = null;
        private TxtOperation log = new TxtOperation();

        private string file = null;
        public string FilePath
        {
            get { return file; }
            set { file = value; }
        }

        public ConfigHelper()
        {
            logPath = bootPath + "Log.txt";

            file = bootPath + @"Config\ModbusTcpConfig.xml";
        }

        /// <summary>
        /// 读取modbus设备信息
        /// </summary>
        /// <returns></returns>
        public List<ModbusTcp> GetAllDeviceConfigValue()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                List<ModbusTcp> list = new List<ModbusTcp>(2);

                XmlNodeList nodeList = xDoc.SelectSingleNode("//Modbus//DeviceList").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = xn as XmlElement;
                    if (xe == null)
                        continue;
                    if (xe.Name.Equals("Device"))   //当前有效的
                    {
                        try
                        {
                            ModbusTcp m = new ModbusTcp();
                            m.Key = xe.GetAttribute(nameof(m.Key));
                            m.Text = xe.GetAttribute(nameof(m.Text));
                            m.IP = xe.GetAttribute(nameof(m.IP));
                            m.Address = xe.GetAttribute(nameof(m.Address));
                            m.Port = ushort.Parse(xe.GetAttribute(nameof(m.Port)));
                            list.Add(m);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("【Error】-" + ex.Message);
                        }
                    }
                }
                return list;
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// 读取LED灯信息
        /// </summary>
        /// <returns></returns>
        public List<ModbusTcp> GetAllLEDConfigValue()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                List<ModbusTcp> list = new List<ModbusTcp>(2);

                XmlNodeList nodeList = xDoc.SelectSingleNode("//Modbus//LedMenuList").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = xn as XmlElement;
                    if (xe == null)
                        continue;
                    if (xe.Name.Equals("Menu"))   //当前有效的
                    {
                        try
                        {
                            ModbusTcp m = new ModbusTcp();
                            m.Key = xe.GetAttribute(nameof(m.Key));
                            m.Text = xe.GetAttribute(nameof(m.Text));
                            m.IP = xe.GetAttribute(nameof(m.IP));
                            m.Address = xe.GetAttribute(nameof(m.Address));
                            m.Port = ushort.Parse(xe.GetAttribute(nameof(m.Port)));
                            list.Add(m);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("【Error】-" + ex.Message);
                        }
                    }
                }
                return list;
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// 读取寄存器、线圈先关指令信息
        /// </summary>
        /// <returns></returns>
        public void GetNodeConfigValue<T,D>(string nodeName, string elementName,IList<T> list, Dictionary<string, D> d)
            where T : new()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                if (list == null)
                    list = new List<T>();

                XmlNodeList nodeList = xDoc.SelectSingleNode("//Modbus//" + nodeName).ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = xn as XmlElement;
                    if (xe == null)
                        continue;
                    if (xe.Name.Equals(elementName))   //当前有效的
                    {
                        try
                        {
                            T t = new T();
                            string key =string.Empty;
                            foreach (XmlAttribute a in xe.Attributes)
                            {
                                //遍历所有参数
                                try
                                {
                                    t.SetProperty(a.Name, a.Value);
                                }
                                catch (Exception e1)
                                {
                                    Console.WriteLine(e1.Message);
                                }
                            }
                            key = xe.GetAttribute("Key");
                            if (!string.IsNullOrEmpty(key) && d.ContainsKey(key))
                                t.SetProperty("Data",d[key]);
                            list.Add(t);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("【Error】-" + ex.Message);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 读取寄存器、线圈先关指令信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,T> GetNodeConfigValue<T>(string nodeName,string elementName)
            where T:new()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                Dictionary<string, T> d = new Dictionary<string, T>(5);

                XmlNodeList nodeList = xDoc.SelectSingleNode("//Modbus//"+nodeName).ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = xn as XmlElement;
                    if (xe == null)
                        continue;
                    if (xe.Name.Equals(elementName))   //当前有效的
                    {
                        try
                        {
                            T t = new T();
                            foreach (XmlAttribute a in xe.Attributes)
                            {
                                //遍历所有参数
                                try
                                {
                                    t.SetProperty(a.Name, a.Value);
                                }
                                catch(Exception e1)
                                {
                                    Console.WriteLine(e1.Message);
                                }
                            }
                            d.Add(xe.Attributes[0].Value,t);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("【Error】-" + ex.Message);
                        }
                    }
                }
                return d;
            }
            catch
            {
            }
            return null;
        }
        #endregion

        #region [ 方法: 修改配置文件中的值 ]
        /// <summary>
        /// 设置Device单个参数
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="Attribute"></param>
        /// <param name="Value"></param>
        public void SetDeviceValue(string key, string Attribute,string Value)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称
                xDoc.Load(file);
                
                XmlElement xElem1;

                xElem1 = xDoc.SelectSingleNode("//Modbus//DeviceList//Device[@Key='" + key + "']") as XmlElement;

                if (xElem1 != null)
                    xElem1.SetAttribute(Attribute, Value);

                xDoc.Save(file);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 设置modbus Device所有参数
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="Attribute"></param>
        /// <param name="Value"></param>
        public void SetDeviceValue(ModbusTcp o)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称
                xDoc.Load(file);

                XmlNode xNode;
                XmlElement xElem1, xElem2;
                xNode = xDoc.SelectSingleNode("//DeviceList");

                xElem1 = (XmlElement)xNode.SelectSingleNode("//Modbus//Device[@Key='" + o.Key + "']");

                if (xElem1 != null)
                {
                    xElem1.SetAttribute(nameof(o.IP), o.IP);
                    xElem1.SetAttribute(nameof(o.Address), o.Address);
                    xElem1.SetAttribute(nameof(o.Port), o.Port.ToString());
                    xElem1.SetAttribute(nameof(o.Text), o.Text);
                }
                else
                {
                    xElem2 = xDoc.CreateElement("Device");
                    xElem2.SetAttribute(nameof(o.IP), o.IP);
                    xElem2.SetAttribute(nameof(o.Text), o.Text);
                    xElem2.SetAttribute(nameof(o.Address), o.Address);
                    xElem2.SetAttribute(nameof(o.Port), o.Port.ToString());
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(file);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
