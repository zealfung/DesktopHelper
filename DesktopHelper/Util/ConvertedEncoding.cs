#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/21 星期二 上午 11:24:21
 * 文件名：ConvertedEncoding
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopHelper.Util
{
    class ConvertedEncoding
    {
        public string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] uByte = utf8.GetBytes(str);
                byte[] gbByte = Encoding.Convert(utf8, gb2312, uByte);
                string result = gb2312.GetString(gbByte);
                return result;
            }
            catch
            {
                return str;
            }
        }

        public string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] gbByte = gb2312.GetBytes(str);
                byte[] uByte = Encoding.Convert(gb2312, uft8, gbByte);
                string result = uft8.GetString(uByte);
                return result;
            }
            catch
            {
                return str;
            }
        }

    }
}
