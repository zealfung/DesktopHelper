using System;
using System.Drawing;
using System.Windows.Forms;
using DesktopHelper.Entity;

namespace DesktopHelper.UI
{
    public partial class iTimer : UserControl
    {
        TimedEvent _timerEvent = new TimedEvent();
        public delegate int iTimerHandler(TimedEvent timerEvent);
        public event iTimerHandler iTimerhandler;

        public iTimer(TimedEvent timerEvent)
        {
            InitializeComponent();
            _timerEvent = timerEvent;
            Init();
        }

        private void Init()
        {
            try
            {
                labelFrequency.Text = string.Format("执行频率：{0}", _timerEvent.Frequency);

                string time = string.Empty;
                switch (_timerEvent.Frequency)
                {
                    case "仅一次":
                        time = string.Format("执行时间：{0}", _timerEvent.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                        break;
                    case "每天":
                        time = string.Format("执行时间：{0}", _timerEvent.Time.ToString("HH:mm:ss"));
                        break;
                    case "每周":
                        time = string.Format("执行时间：{0} {1}", DayOfWeek(_timerEvent.Time), _timerEvent.Time.ToString("HH:mm:ss"));
                        break;
                    case "每月":
                        time = string.Format("执行时间：{0}号 {1}", _timerEvent.Time.Day, _timerEvent.Time.ToString("HH:mm:ss"));
                        break;
                }
                labelTime.Text = time;

                labelEvent.Text = string.Format("执行事件：{0}", _timerEvent.ExecEvents);
                if (string.IsNullOrEmpty(_timerEvent.FilePath)) labelFilepath.Visible = false;
                else
                {
                    labelFilepath.Visible = true;
                    labelFilepath.Text = string.Format("软件路径：{0}", _timerEvent.FilePath);
                }
            }
            catch
            {
            }
        }

        private string DayOfWeek(DateTime time)
        {
            string result = string.Empty;
            switch (time.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    result = "星期一";
                    break;
                case System.DayOfWeek.Tuesday:
                    result = "星期二";
                    break;
                case System.DayOfWeek.Wednesday:
                    result = "星期三";
                    break;
                case System.DayOfWeek.Thursday:
                    result = "星期四";
                    break;
                case System.DayOfWeek.Friday:
                    result = "星期五";
                    break;
                case System.DayOfWeek.Saturday:
                    result = "星期六";
                    break;
                case System.DayOfWeek.Sunday:
                    result = "星期日";
                    break;
            }
            return result;
        }

        private void iTimer_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.DodgerBlue;
        }

        private void iTimer_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            iTimerhandler(_timerEvent);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.DodgerBlue;
                BtnClose.BackColor = Color.Red;
            }
            catch
            {
            }
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }
    }
}
