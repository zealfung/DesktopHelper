#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/1 星期六 下午 03:32:21
 * 文件名：Website
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion

namespace DesktopHelper.Entity
{
    public class Website
    {
        private string _name;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _type;
        /// <summary>
        /// 分类编码
        /// </summary>
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _index;
        /// <summary>
        /// 顺序
        /// </summary>
        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private string _url;
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _iconPath;
        /// <summary>
        /// 图标路径
        /// </summary>
        public string IconPath
        {
            get { return _iconPath; }
            set { _iconPath = value; }
        }
    }
}
