#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 下午 01:08:11
 * 文件名：Rili
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion

namespace DataCollection.Entity
{
    class Rili
    {
        private string _yangli;
        /// <summary>
        /// 阳历日期 格式：yyyy年M月d日
        /// </summary>
        public string Yangli
        {
            get { return _yangli; }
            set { _yangli = value; }
        }
        private string _nongli;
        /// <summary>
        /// 农历日期 格式：农历蛇年 腊月二十
        /// </summary>
        public string Nongli
        {
            get { return _nongli; }
            set { _nongli = value; }
        }
        private string _zhigan;
        /// <summary>
        /// 农历支干 格式：癸巳年 乙丑月 辛卯日
        /// </summary>
        public string Zhigan
        {
            get { return _zhigan; }
            set { _zhigan = value; }
        }
        private string _xingzuo;
        /// <summary>
        /// 星座 格式：双鱼座
        /// </summary>
        public string Xingzuo
        {
            get { return _xingzuo; }
            set { _xingzuo = value; }
        }
        private string _xingqi;
        /// <summary>
        /// 星期 格式：星期三
        /// </summary>
        public string Xingqi
        {
            get { return _xingqi; }
            set { _xingqi = value; }
        }
        private string _yi;
        /// <summary>
        /// 宜
        /// </summary>
        public string Yi
        {
            get { return _yi; }
            set { _yi = value; }
        }
        private string _ji;
        /// <summary>
        /// 忌
        /// </summary>
        public string Ji
        {
            get { return _ji; }
            set { _ji = value; }
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += string.Format("\n阳历：{0}", _yangli);
            result += string.Format("\n农历：{0}", _nongli);
            result += string.Format("\n支干：{0}", _zhigan);
            result += string.Format("\n星座：{0}", _xingzuo);
            result += string.Format("\n星期：{0}", _xingqi);
            result += string.Format("\n宜：{0}", _yi);
            result += string.Format("\n忌：{0}", _ji);
            return result;
        }
    }
}
