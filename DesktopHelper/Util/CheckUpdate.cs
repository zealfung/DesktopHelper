#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/10 星期一 下午 08:42:15
 * 文件名：CheckUpdate
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;

namespace DesktopHelper.Util
{
    public class CheckUpdate
    {
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        private string path = Application.StartupPath + @"\Update\DeskHelper.xml";
        private string _newVersion;
        /// <summary>
        /// 新版本号
        /// </summary>
        public string NewVersion
        {
            get { return _newVersion; }
            set { _newVersion = value; }
        }
        /// <summary>
        /// 检测本机是否联网
        /// </summary>
        /// <returns></returns>
        public bool IsConnectedInternet()
        {
            int i = 0;
            if (InternetGetConnectedState(out i, 0))
            {
                //已联网
                return true;
            }
            else
            {
                //未联网
                return false;
            }
        }
        /// <summary>
        /// 获取最新版本信息
        /// </summary>
        /// <returns></returns>
        public bool Download()
        {
            bool success = false;
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\Update"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Update");
                }
                string url = "http://www.andwho.com/AndwhoUpdate/DeskHelper/DeskHelper.xml";
                Uri uri = new Uri(url);
                HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(uri);
                mRequest.Method = "GET";
                mRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse mResponse = (HttpWebResponse)mRequest.GetResponse();
                Stream sIn = mResponse.GetResponseStream();
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                long length = mResponse.ContentLength;
                long i = 0;

                while (i < length)
                {
                    byte[] buffer = new byte[length];
                    int read = sIn.Read(buffer, 0, buffer.Length);
                    i += read;

                    string strTemp = System.Text.Encoding.UTF8.GetString(buffer, 0, read);
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strTemp);

                    fs.Write(bytes, 0, bytes.Length);
                }
                sIn.Close();
                mResponse.Close();
                fs.Close();
                success = true;
            }
            catch
            {
            }
            return success;
        }
        /// <summary>
        /// 检测是否有新版本
        /// </summary>
        /// <returns></returns>
        public bool HasNewVersion()
        {
            bool hasNew = false;
            try
            {
                string newVersion = GetNewVersion();
                if (Application.ProductVersion.CompareTo(newVersion) < 0)
                {
                    hasNew = true;
                }
            }
            catch
            {
            }
            return hasNew;
        }

        private string GetNewVersion()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlNodeList clientNodes;
                clientNodes = xDoc.SelectNodes("//client");
                if (clientNodes.Count > 0)
                {
                    foreach (XmlNode xNode in clientNodes)
                    {
                        _newVersion = xNode.FirstChild.InnerText;
                    }
                }
            }
            catch
            {
            }
            return _newVersion;
        }

    }
}
