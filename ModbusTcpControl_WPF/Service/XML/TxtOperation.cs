using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Song.Txt.Files
{
    class TxtOperation
    {
        #region[将信息写入文件]

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns>成功返回true失败返回false</returns>
        public bool ApplictionErrorWrite(string message)
        {
            try
            {
                if (!Directory.Exists("Error"))
                {
                    Directory.CreateDirectory("Error");
                }
                appendToFile(message, @"Error/ApplictionErrors.txt");
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>成功返回true失败返回false</returns>
        public bool MonthErrorWriter(string message)
        {
            try
            {
                if (!Directory.Exists(@"Error/" + DateTime.Now.ToString("yyyy-MM")))
                {
                    Directory.CreateDirectory(@"Error/" + DateTime.Now.ToString("yyyy-MM"));
                }
                appendToFile(message, @"Error/" + DateTime.Now.ToString("yyyy-MM") + @"/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将信息写入文件
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="path">文件路径</param>
        public void appendToFile(string message, string path)
        {
            try
            {
                if (!File.Exists(path))
                    File.Create(path);
                FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter streamwriter = new StreamWriter(filestream);
                streamwriter.WriteLine(message);
                streamwriter.Flush();
                streamwriter.Close();
                streamwriter.Dispose();
                filestream.Close();
                filestream.Dispose();
            }
            catch 
            {
            }
        }

        public void appendToFile2(string message, string path)
        {
            try
            {
                if (!File.Exists(path))
                    File.Create(path).Dispose();
                FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter streamwriter = new StreamWriter(filestream,Encoding.Default);
                streamwriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss\t") + message);
                streamwriter.Flush();
                streamwriter.Close();
                streamwriter.Dispose();
                filestream.Close();
                filestream.Dispose();
            }
            catch
            {
            }
        }
        #endregion

        #region[读取文件]
        public bool readFile(string path,ref string buf)
        {
            try
            {
                if (!File.Exists(path))
                {
                    //MessageBox.Show("抱歉，所请求的文件不存在.\n"+path);
                    Console.WriteLine("抱歉，所请求的文件不存在.");
                    return false;
                }
                FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(filestream,System.Text.Encoding.Default);
                buf = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                filestream.Close();
                filestream.Dispose();
                return true;
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
                Console.WriteLine("[读取文件]:" + e.Message);
                return false;
            }
        }
        #endregion
    }
}
