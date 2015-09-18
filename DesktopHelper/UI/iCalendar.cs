using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopHelper.UI
{
    public partial class iCalendar : Form
    {
        public iCalendar()
        {
            InitializeComponent();
        }

        private string _yangLi;
        /// <summary>
        /// 阳历
        /// </summary>
        public string YangLi
        {
            get { return _yangLi; }
            set { _yangLi = value; }
        }

        private string _week;
        /// <summary>
        /// 星期
        /// </summary>
        public string Week
        {
            get { return _week; }
            set { _week = value; }
        }

        private string _nongLi;
        /// <summary>
        /// 农历
        /// </summary>
        public string NongLi
        {
            get { return _nongLi; }
            set { _nongLi = value; }
        }

        private string _jieQi;
        /// <summary>
        /// 节气
        /// </summary>
        public string JieQi
        {
            get { return _jieQi; }
            set { _jieQi = value; }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        public void ShowMessage()
        {
            labelDayYang.Text = _yangLi;
            labelWeek.Text = _week;
            labelDayNong.Text = _nongLi;
            labelJieQi.Text = _jieQi;
        }
    }
}
