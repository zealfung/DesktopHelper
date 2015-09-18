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
    public partial class iNotepad : UserControl
    {
        string string_CreateTime;
        string string_Title;
        public delegate void DeleteEventHandler(string createTime);
        public event DeleteEventHandler DeleteEvent;
        public delegate void EditEventHandler(string createTime);
        public event EditEventHandler EditEvent;

        public iNotepad(string createTime,string title)
        {
            InitializeComponent();
            string_CreateTime = createTime;
            labelTitle.Text = string_Title = title;
        }

        private void iNotepad_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
        }

        private void iNotepad_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void BtnDelete_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.DodgerBlue;
                BtnDelete.BackColor = Color.Red;
            }
            catch
            {
            }
        }

        private void BtnDelete_MouseLeave(object sender, EventArgs e)
        {
            BtnDelete.BackColor = Color.Transparent;
        }

        private void BtnEdit_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.DodgerBlue;
                BtnEdit.BackColor = Color.DarkOrange;
            }
            catch
            {
            }
        }

        private void BtnEdit_MouseLeave(object sender, EventArgs e)
        {
            BtnEdit.BackColor = Color.Transparent;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteEvent(string_CreateTime);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditEvent(string_CreateTime);
        }
    }
}
