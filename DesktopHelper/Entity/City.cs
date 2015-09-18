#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/21 星期五 上午 11:07:03
 * 文件名：City
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion

namespace DesktopHelper.Entity
{
    public class City
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _shengParent;
        /// <summary>
        /// 省父级
        /// </summary>
        public string ShengParent
        {
            get { return _shengParent; }
            set { _shengParent = value; }
        }
        private string _shiParent;
        /// <summary>
        /// 市父级
        /// </summary>
        public string ShiParent
        {
            get { return _shiParent; }
            set { _shiParent = value; }
        }
        private string _code;
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
    }
}
