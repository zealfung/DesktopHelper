#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/14 星期五 上午 10:56:10
 * 文件名：iTimer
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;

namespace DesktopHelper.Entity
{
    public class TimedEvent
    {
        private int _id;
        /// <summary>
        /// 序号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _frequency;
        /// <summary>
        /// 执行频率
        /// </summary>
        public string Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private DateTime _time;
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private string _execEvents;
        /// <summary>
        /// 执行事件
        /// </summary>
        public string ExecEvents
        {
            get { return _execEvents; }
            set { _execEvents = value; }
        }

        private string _filePath;
        /// <summary>
        /// 软件路径
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
    }
}
