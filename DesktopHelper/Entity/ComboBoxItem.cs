#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/21 星期五 下午 12:07:01
 * 文件名：ComboBoxItem
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion

namespace DesktopHelper.Entity
{
    public class ComboBoxItem
    {
        public ComboBoxItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 实际值
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        //重写Tostring
        public override string ToString()
        {
            return this.Name;
        }
    }
}
