using System;
using System.Drawing;
using System.Windows.Forms;
using Andwho.Entity;

namespace DesktopHelper.UI
{
    public partial class iReminder : UserControl
    {
        Reminder _reminder = new Reminder();
        public delegate void ReminderHandler(Reminder reminder);
        public event ReminderHandler Reminderhandler;

        public iReminder(Reminder reminder)
        {
            InitializeComponent();
            _reminder = reminder;
            Init();
        }

        private void Init()
        {
            labelBeginTime.Text = _reminder.ReminderTime.ToString("yyyy-MM-dd HH:mm:ss");
            labelInfo.Text = _reminder.ReminderInfo;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Reminderhandler(_reminder);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }

        private void iReminder_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
        }

        private void iReminder_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }
    }
}
