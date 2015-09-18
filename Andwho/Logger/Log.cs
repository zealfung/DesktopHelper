#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/19 星期三 下午 01:34:03
 * 文件名：Log
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Andwho.Logger
{
    public class Log
    {
        private bool bool_IsWrite = false;
        private FileStream fs;
        private StreamWriter sw;
        private string folderName = Application.StartupPath + "\\Log";
        private string fileName = string.Empty;

        /// <summary>
        /// 运行日志
        /// </summary>
        /// <param name="isWrite">是否写日志</param>
        public Log(bool isWrite)
        {
            bool_IsWrite = isWrite;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void WriteLog(string msg)
        {
            if (!bool_IsWrite) return;
            try
            {
                fileName = "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                if (!File.Exists(fileName))
                {
                    fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                }
                sw = new StreamWriter(fs, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                sw.WriteLine(ex.Message);
            }
            sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + msg);
            sw.Flush();
            sw.Close();
            fs.Close();
        } 
    }
}
