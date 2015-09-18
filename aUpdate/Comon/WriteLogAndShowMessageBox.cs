#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2013/12/9 星期一 22:27:04
 * 文件名：WriteLogAndShowMessageBox
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
using System.Windows.Forms;

namespace aUpdate.Comon
{
    public class WriteLogAndShowMessageBox
    {
        /// <summary>
        /// 提示信息写日志同时弹出信息提示框
        /// </summary>
        /// <param name="boxMessage">弹出框提示语</param>
        /// <param name="logMessage">运行日志记录，请详细具体到函数</param>
        public static void Info(string boxMessage, string logMessage)
        {
            Log.Info(logMessage);
            MessageBox.Show(boxMessage, "信息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        /// <summary>
        /// 错误信息写日志同时弹出错误提示框
        /// </summary>
        /// <param name="boxMessage">弹出框提示语，异常部分可以用ex.Message</param>
        /// <param name="logMessage">运行日志记录，请详细具体到函数，异常部分用ex.ToString()</param>
        public static void Error(string boxMessage, string logMessage)
        {
            Log.Error(logMessage);
            MessageBox.Show(boxMessage, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
        /// <summary>
        /// 警告信息写日志同时弹出警告框
        /// </summary>
        /// <param name="boxMessage">弹出框提示语，异常部分可以用ex.Message</param>
        /// <param name="logMessage">运行日志记录，请详细具体到函数，异常部分用ex.ToString()</param>
        public static void Warm(string boxMessage, string logMessage)
        {
            Log.Warm(logMessage);
            MessageBox.Show(boxMessage, "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }
    }
}
