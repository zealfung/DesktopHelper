using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopHelper.UI
{
    public partial class iDay : UserControl
    {
        public iDay()
        {
            InitializeComponent();
        }
        #region
        private string _iNewText;
        /// <summary>
        /// 阳历日
        /// </summary>
        public string iNewText
        {
            get { return _iNewText; }
            set { _iNewText = value; ShowMessage(); }
        }

        private Color _iNewColor;
        /// <summary>
        /// 阳历颜色
        /// </summary>
        public Color iNewColor
        {
            get { return _iNewColor; }
            set { _iNewColor = value; ShowMessage(); }
        }
        private string _iOldText;
        /// <summary>
        /// 农历日
        /// </summary>
        public string iOldText
        {
            get { return _iOldText; }
            set { _iOldText = value; ShowMessage(); }
        }
        private Color _iOldColor;
        /// <summary>
        /// 农历颜色
        /// </summary>
        public Color iOldColor
        {
            get { return _iOldColor; }
            set { _iOldColor = value; ShowMessage(); }            
        }
        #endregion

        private string _yangli;
        /// <summary>
        /// 阳历 格式：“2014年11月20日 星期四”
        /// </summary>
        public string Yangli
        {
            get { return _yangli; }
            set { _yangli = value; }
        }
        private string _nongli;
        /// <summary>
        /// 农历 格式：“农历蛇年 腊月二十”
        /// </summary>
        public string Nongli
        {
            get { return _nongli; }
            set { _nongli = value; }
        }
        private string _zhigan;
        /// <summary>
        /// 支干 格式：“癸巳年 乙丑月 辛卯日”
        /// </summary>
        public string Zhigan
        {
            get { return _zhigan; }
            set { _zhigan = value; }
        }
        private string _jieri;
        /// <summary>
        /// 节日
        /// </summary>
        public string Jieri
        {
            get { return _jieri; }
            set { _jieri = value; }
        }
        private string _xingzuo;
        /// <summary>
        /// 星座
        /// </summary>
        public string Xingzuo
        {
            get { return _xingzuo; }
            set { _xingzuo = value; }
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

        private void ShowMessage()
        {
            iNew.Text = _iNewText;
            iNew.ForeColor = _iNewColor;
            iOld.Text = _iOldText;
            iOld.ForeColor = _iOldColor;
        }

        #region 点击事件
        public delegate void BeClicked(object sender, EventArgs e);
        public event BeClicked Clicked;

        private void iDay_Click(object sender, EventArgs e)
        {
            Clicked(this, e);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            iDay_Click(sender, e);
        }
        #endregion
    }
}
