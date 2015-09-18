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
    public partial class AddWebsiteForm : Form
    {
        private static AddWebsiteForm form = null;
        private static object synLock = new object();
        public delegate void AddWebsiteHandler(string name, string url);
        public event AddWebsiteHandler AddWebsiteHandlor;

        public static AddWebsiteForm GetInstance()
        {
            if (form == null)
            {
                lock (synLock)
                {
                    form = new AddWebsiteForm();
                }
            }
            return form;
        }

        private AddWebsiteForm()
        {
            InitializeComponent();
        }

        #region
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("请输入名称");
                    return;
                }
                if (string.IsNullOrEmpty(txtURL.Text))
                {
                    MessageBox.Show("请输入网址");
                    return;
                }
                AddWebsiteHandlor(txtName.Text, txtURL.Text);
                this.Close();
            }
            catch
            {
            }
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }

        private void BtnAdd_MouseEnter(object sender, EventArgs e)
        {
            BtnAdd.BackColor = Color.DodgerBlue;
        }

        private void BtnAdd_MouseLeave(object sender, EventArgs e)
        {
            BtnAdd.BackColor = Color.Transparent;
        }
        #endregion

        private void AddWebsiteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form != null)
            {
                form.Dispose();
                form = null;
            }
        }
    }
}
