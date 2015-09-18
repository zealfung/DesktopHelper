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
    public partial class iWebsite : UserControl
    {
        public delegate void DeleteWebsiteHandler(int i);
        public event DeleteWebsiteHandler DeleteWebsiteHandlor;
        public iWebsite()
        {
            InitializeComponent();
        }

        private string _websiteName;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebsiteName
        {
            get { return _websiteName; }
            set { _websiteName = value; Website.Text = _websiteName; }
        }

        private string _websiteUrl;
        /// <summary>
        /// 网址
        /// </summary>
        public string WebsiteUrl
        {
            get { return _websiteUrl; }
            set { _websiteUrl = value; }
        }

        #region
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteWebsiteHandlor(this.TabIndex);
        }

        private void iWebsite_MouseEnter(object sender, EventArgs e)
        {
            //BtnDelete.Visible = true;
        }

        private void iWebsite_MouseLeave(object sender, EventArgs e)
        {
            //BtnDelete.Visible = false;
        }
        
        private void BtnDelete_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Red;
        }

        private void BtnDelete_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.DodgerBlue;
        }
        #endregion
    }
}
