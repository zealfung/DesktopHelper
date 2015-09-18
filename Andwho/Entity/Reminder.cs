#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/13 星期四 上午 09:39:18
 * 文件名：Reminder
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

namespace Andwho.Entity
{
    public class Reminder
    {
        private int _reminderId;
        /// <summary>
        /// 提醒编号
        /// </summary>
        public int ReminderId
        {
            get { return _reminderId; }
            set { _reminderId = value; }
        }
        private DateTime _reminderTime;
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime ReminderTime
        {
            get { return _reminderTime; }
            set { _reminderTime = value; }
        }
        private string _reminderInfo;
        /// <summary>
        /// 提醒信息
        /// </summary>
        public string ReminderInfo
        {
            get { return _reminderInfo; }
            set { _reminderInfo = value; }
        }
    }
}
