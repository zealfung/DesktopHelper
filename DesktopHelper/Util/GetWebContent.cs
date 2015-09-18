#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/21 星期五 下午 04:16:46
 * 文件名：GetWebContent
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace DesktopHelper.Util
{
    public class GetWebContent
    {
        //根据Url地址得到网页的html源码
        public string GetWebContentByUrl(string Url, Encoding encoding)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //声明一个HttpWebRequest请求
                //request.Timeout = 60000;
                if (request.Timeout < 60000)
                {
                    return "链接超时";
                }
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                // request.Headers.Set("KeepAlive", "true");
                request.CookieContainer = new CookieContainer();
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Referer = Url;

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReceive.Close();
                streamReader.Close();
                streamReceive = null;
                streamReader = null;
            }
            catch
            {
                return "链接异常";
            }
            return strResult;
        }

        /// <summary>
        /// 删除HTML标识
        /// </summary>
        public string DropHTMLTag(string htmlString)
        {
            htmlString = htmlString.Replace("=<=%", "");
            htmlString = htmlString.Replace("=>=%", "");
            htmlString = htmlString.Replace(">>", "");
            htmlString = Regex.Replace(htmlString, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"-->", "", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<!--.*", "", RegexOptions.IgnoreCase);

            htmlString = Regex.Replace(htmlString, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            htmlString = htmlString.Replace("<", "");
            htmlString = htmlString.Replace(">", "");
            htmlString = htmlString.Replace("\r\n", "");


            return htmlString;
        }
    }
}
