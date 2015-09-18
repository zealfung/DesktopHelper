#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/21 星期五 上午 10:49:30
 * 文件名：UpdateCityListXML
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using Andwho.Logger;

namespace DesktopHelper.Util
{
    public class UpdateCityListXML
    {
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        private string path = Application.StartupPath + @"\XML\CityList.xml";
        Log log = new Log(true);

        /// <summary>
        /// 下载WebsiteList.xml
        /// </summary>
        /// <returns></returns>
        public bool Download()
        {
            bool success = false;
            try
            {
                if (!IsConnectedInternet()) return success;

                if (!Directory.Exists(Application.StartupPath + @"\XML"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\XML");
                }
                string url = "http://www.andwho.com/AndwhoUpdate/DeskHelper/CityList.xml";
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
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return success;
        }

        // 检测本机是否联网
        private bool IsConnectedInternet()
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
    }
}
