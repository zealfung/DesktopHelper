#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 上午 09:18:01
 * 文件名：Log
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

namespace DataCollection.Logger
{
    public static class Log
    {
        private static readonly log4net.ILog myLog = log4net.LogManager.GetLogger("AndwhoLog");

        public static void Info(string message)
        {
            myLog.Info(message);
        }

        public static void Debug(string message)
        {
            myLog.Debug(message);
        }

        public static void Error(string message)
        {
            myLog.Error(message);
        }

        public static void Fatal(string message)
        {
            myLog.Fatal(message);
        }

        public static void Warm(string message)
        {
            myLog.Warn(message);
        }
    }
}
