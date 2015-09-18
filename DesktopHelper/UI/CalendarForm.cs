using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using DesktopHelper.Util;
using DesktopHelper.DataAccess;
using DesktopHelper.Entity;
using System.Collections.Generic;
using System.IO;
using Andwho.Entity;
using System.Diagnostics;
using Andwho.Logger;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace DesktopHelper.UI
{
    public partial class CalendarForm : Form
    {
        #region 变量
        Rectangle rectangle_Screen;
        Rectangle rectangle_This;
        int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏
        int int_Year;
        int int_Month;
        int int_Day;
        int int_WeekOfFirstDay;
        int int_DayOfMonth;
        bool bool_Flag = true;
        iDay[,] iDays = new iDay[6, 7];        
        CalendarData _data = new CalendarData();
        DataTable table_Reminder = new DataTable();
        DataTable table_TimedEvents = new DataTable();
        DataTable table_Notepad = new DataTable();
        Reminder newReminder = new Reminder();
        Dictionary<DateTime, Reminder> dic_Reminder = new Dictionary<DateTime, Reminder>();        
        TimedEvent newTimerEvent = new TimedEvent();
        List<TimedEvent> list_TimedEvent = new List<TimedEvent>();
        DateTime time_TimedEventReload = DateTime.Today;
        string string_CityCode = string.Empty;
        System.Windows.Forms.Timer weatherTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer bianqianTimer = new System.Windows.Forms.Timer();
        string string_NotepadCreateTime = string.Empty;
        Log log = new Log(true);
        #endregion

        public CalendarForm()
        {
            InitializeComponent();
            Init();
        }

        #region 声明API函数
        [DllImportAttribute("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int dwTime, int dwFlags);
        #endregion

        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowForm(AnchorStyles StopAanhor, int x, int y)
        {
            try
            {
                tabControl.SelectedIndex = 0;
                btnToday_Click(null, null);

                #region
                switch (StopAanhor)
                {
                    case AnchorStyles.Top:
                        if (x > this.Width / 2)
                        {
                            if ((rectangle_Screen.Right - x) > this.Width / 2)
                            {
                                this.rectangle_This = new Rectangle(x + 28 - this.Width / 2, y + 58, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_This = new Rectangle(x - this.Width + 56, y + 58, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_This = new Rectangle(x, y + 58, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Left:
                        if (y > this.Height / 2)
                        {
                            if ((rectangle_Screen.Bottom - y) > this.Height / 2)
                            {
                                this.rectangle_This = new Rectangle(x + 58, y + 28 - this.Height / 2, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_This = new Rectangle(x + 58, y - this.Height + 56, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_This = new Rectangle(x + 58, y, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Right:
                        if (y > this.Height / 2)
                        {
                            if ((rectangle_Screen.Bottom - y) > this.Height / 2)
                            {
                                this.rectangle_This = new Rectangle(x - this.Width, y + 28 - this.Height / 2, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_This = new Rectangle(x - this.Width, y - this.Height + 56, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_This = new Rectangle(x - this.Width, y, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Bottom:
                        if (x > this.Width / 2)
                        {
                            if ((rectangle_Screen.Right - x) > this.Width / 2)
                            {
                                this.rectangle_This = new Rectangle(x + 28 - this.Width / 2, y - this.Height, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_This = new Rectangle(x - this.Width + 56, y - this.Height, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_This = new Rectangle(x, y - this.Height, this.Width, this.Height);
                        }
                        break;
                }
                #endregion
                
                this.SetBounds(rectangle_This.X, rectangle_This.Y, rectangle_This.Width, rectangle_This.Height);//设置当前窗体的边界 
                ShowWindow(this.Handle, 4, AW_SLIDE + AW_VER_NEGATIVE);//动态显示本窗体                
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        #region Init
        private void CalendarForm_Load(object sender, EventArgs e)
        {
        }

        private void Init()
        {
            try
            {
                tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
                rectangle_Screen = System.Windows.Forms.Screen.GetWorkingArea(this);

                tabControl.Controls.Remove(tabTianqi);

                LoadConfig();
                InitCondition();
                CurrentDate();
                LoadReminder();
                LoadTimedEvents();
                LoadNotepad();

                Thread tIsNextDay = new Thread(new ThreadStart(IsNextDay));
                tIsNextDay.IsBackground = true;
                tIsNextDay.Start();

                AppTimerEvent();
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void LoadConfig()
        {
            try
            {
                DataTable dtConfig = _data.GetConfig();
                if (dtConfig != null && dtConfig.Rows.Count > 0)
                {
                    string name = string.Empty;
                    string value = string.Empty;
                    foreach (DataRow dr in dtConfig.Rows)
                    {
                        name = dr["Name"].ToString();
                        value = dr["Value"].ToString();
                        InitConfig(name, value);
                    }
                }
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitConfig(string name, string value)
        {
            try
            {
                switch (name)
                {
                    case "FirstStart":
                        #region 首次运行设置开机启动
                        if (value.Equals("true"))
                        {
                            //string strName = Application.ExecutablePath;
                            //string strnewName = strName.Substring(strName.LastIndexOf(@"\") + 1);
                            //if (File.Exists(strName))//指定文件是否存在  
                            //{
                            //    Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                            //    if (Rkey == null)
                            //        Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                            //    Rkey.SetValue(strnewName, strName);//修改注册表，使程序开机时自动执行。
                            //}
                            _data.UpdateConfig("FirstStart", "false");
                        }
                        #endregion
                        break;
                    case "IsUseMusic":
                        #region 是否启用提示音乐
                        if (value.Equals("true"))
                        {
                            checkBoxUseMusic.Checked = true;
                        }
                        else
                        {
                            checkBoxUseMusic.Checked = false;
                        }
                        #endregion
                        break;
                    case "MusicPath":
                        labelMusicPath.Text = value;
                        break;
                    case "CityCode":
                        string_CityCode = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        #region TimerEvent
        private void AppTimerEvent()
        {
            try
            {
                System.Windows.Forms.Timer appTimer = new System.Windows.Forms.Timer();
                appTimer.Interval = 1000;
                appTimer.Tick += new EventHandler(AppTimerTick);
                appTimer.Start();

                System.Windows.Forms.Timer tixingTimer = new System.Windows.Forms.Timer();
                tixingTimer.Interval = 10 * 1000;
                tixingTimer.Tick += new EventHandler(TixingTimerTick);
                tixingTimer.Start();

                System.Windows.Forms.Timer dingshiTimer = new System.Windows.Forms.Timer();
                dingshiTimer.Interval = 10 * 1000;
                dingshiTimer.Tick += new EventHandler(DingshiTimerTick);
                dingshiTimer.Start();

                System.Windows.Forms.Timer mouseTimer = new System.Windows.Forms.Timer();
                mouseTimer.Interval = 100;
                mouseTimer.Tick += new EventHandler(MouseTick);
                mouseTimer.Start();

                //System.Windows.Forms.Timer weatherTimer = new System.Windows.Forms.Timer();
                //weatherTimer.Interval = 10*000;
                //weatherTimer.Tick += new EventHandler(WeatherTick);
                //weatherTimer.Start();

                bianqianTimer.Interval = 300 * 1000;
                bianqianTimer.Tick += new EventHandler(BianqianTick);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AppTimerTick(object sender, EventArgs e)
        {
            try
            {
                DateTime timenow = DateTime.Now;
                labelTime.Text = timenow.ToString("HH:mm:ss");
                labelShichen.Text = ChineseDate.GetHour(timenow);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void TixingTimerTick(object sender, EventArgs e)
        {
            try
            {
                if (dic_Reminder == null || dic_Reminder.Keys.Count < 1) return;

                DateTime timenow = DateTime.Now;                
                foreach (DateTime time in dic_Reminder.Keys)
                {
                    if (time.Year == timenow.Year
                        && time.Month == timenow.Month
                        && time.Day == timenow.Day
                        && time.Hour == timenow.Hour
                        && time.Minute == timenow.Minute)
                    {
                        Thread remindThread = new Thread(ShowRemind);
                        remindThread.SetApartmentState(ApartmentState.STA);
                        remindThread.IsBackground = true;
                        remindThread.Start((object)dic_Reminder[time]);
                        DeleteReminder(dic_Reminder[time]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void DingshiTimerTick(object sender, EventArgs e)
        {
            try
            {
                if (list_TimedEvent == null || list_TimedEvent.Count < 1) return;

                DateTime timenow = DateTime.Now;
                foreach (TimedEvent item in list_TimedEvent)
                {
                    switch (item.Frequency)
                    {
                        case "仅一次":
                            if (timenow.Year == item.Time.Year
                                && timenow.Month == item.Time.Month
                                && timenow.Day == item.Time.Day
                                && IsTimeNow(timenow, item))
                                ExecEvent(item);
                            break;
                        case "每天":
                            if (IsTimeNow(timenow, item))
                                ExecEvent(item);
                            break;
                        case "每周":
                            if (timenow.DayOfWeek == item.Time.DayOfWeek
                                && IsTimeNow(timenow, item))
                                ExecEvent(item);
                            break;
                        case "每月":
                            if (timenow.Day == item.Time.Day
                                && IsTimeNow(timenow, item))
                                ExecEvent(item);
                            break;
                    }
                }
                if (timenow.Day != time_TimedEventReload.Day)
                {
                    LoadTimedEvents();
                    time_TimedEventReload = timenow;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private bool IsTimeNow(DateTime time, TimedEvent te)
        {
            bool result = false;
            try
            {
                if (time.Hour == te.Time.Hour
                    && time.Minute == te.Time.Minute)
                    result = true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        private void ExecEvent(TimedEvent te)
        {
            try
            {
                switch (te.ExecEvents)
                {
                    case "关机":
                        Process p = new Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.StandardInput.WriteLine("shutdown -s -f");
                        p.Close();
                        break;
                    case "打开软件":
                        if (File.Exists(te.FilePath))
                            System.Diagnostics.Process.Start(te.FilePath);
                        if(list_TimedEvent.Contains(te)) list_TimedEvent.Remove(te);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void MouseTick(object sender, EventArgs e)
        {
            try
            {
                if (!this.Bounds.Contains(Control.MousePosition)
                    && !this.Owner.Bounds.Contains(Control.MousePosition)
                    && Control.MouseButtons == MouseButtons.Left)
                {
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void WeatherTick(object sender, EventArgs e)
        {
            RefreshWeather();
            weatherTimer.Interval = 30 * 60 * 1000;
        }

        private void BianqianTick(object sender, EventArgs e)
        {
            BtnSaveNotepad_Click(null, null);
        }
        #endregion

        #region TabRili
        private void InitCondition()
        {
            try
            {
                string[] strYear = { "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020",
                                 "2021","2022","2023","2024","2025","2026","2027","2028","2029","2030"};
                string[] strMonth = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                comboBoxYear.Items.AddRange(strYear);
                comboBoxMonth.Items.AddRange(strMonth);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        
        private void CurrentDate()
        {
            int_Year = DateTime.Today.Year;
            int_Month = DateTime.Today.Month;
            int_Day = DateTime.Today.Day;
            labelTitle.Text = "星期日  星期一  星期二  星期三  星期四  星期五  星期六";
            DefualtDataTime(int_Year, int_Month);
        }

        private void SetDateDetail(iDay date)
        {
            try
            {
                labelYangli.Text = date.Yangli;
                labelNongli.Text = date.Nongli;
                labelZhigan.Text = date.Zhigan;
                labelXingzuo.Text = date.Xingzuo;
                labelYi.Text = date.Yi;
                labelJi.Text = date.Ji;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void DefualtDataTime(int year, int month)
        {
            try
            {
                this.comboBoxYear.SelectedIndex = year - 2013;
                this.comboBoxMonth.SelectedIndex = month - 1;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void IsNextDay()
        {
            while (true)
            {
                DateTime dtNow = DateTime.Today;//DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime dtOld = new DateTime(int_Year, int_Month, int_Day);// DateTime.Parse(g_year + "-" + g_month + "-" + g_day);
                TimeSpan tsIsNextDay = dtNow - dtOld;

                try
                {
                    if (tsIsNextDay.Days != 0)
                    {
                        CurrentDate();
                        ResetPanel();
                        GetWeekInfo(ref int_WeekOfFirstDay, ref int_DayOfMonth, int_Year, int_Month);
                        FillPanelMonth(int_WeekOfFirstDay, int_DayOfMonth, int_Year, int_Month);
                    }
                }
                catch (Exception ex)
                {
                    log.WriteLog(ex.ToString());
                }
                Thread.Sleep(1000 * 3600);
            }
        }

        private void GetWeekInfo(ref int weekOfFirstDay, ref int dayOfMonth, int year, int month)
        {
            try
            {
                DateTime dt = DateTime.Parse(year.ToString(CultureInfo.InvariantCulture) + "-" +
                    month.ToString(CultureInfo.InvariantCulture));
                weekOfFirstDay = (int)dt.DayOfWeek;
                dayOfMonth = (int)DateTime.DaysInMonth(year, month);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void FillPanelMonth(int firstDayInWeek, int endMontyDay, int year, int month)
        {
            try
            {
                DateTime today = DateTime.Today;
                Color cWeekdays = Color.Navy; //周一到周五
                Color cWeekend = Color.Red;       //周六周日    
                Color cNotHoliday = Color.DarkGray; //农历非节日
                Color cIsHoliday = Color.Red; //农历节日
                Color cSolarTerm = Color.Blue; //农历二十四节气
                Color cTodayBG = Color.LightSalmon;//当天背景色   
                DataTable dtMonth = _data.GetRiliDataByMonth(year, month);

                int num = 1;

                string strMonth = string.Empty;
                string strDay = string.Empty;
                string strHoliday = string.Empty;

                for (int j = 0; j < 6; j++)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (j == 0 && i < firstDayInWeek) //找到一号是星期几
                        {
                            continue;
                        }
                        else
                        {
                            if (num > endMontyDay) //超过这个月的最大天数，退出
                            {
                                break;
                            }

                            DateTime date = DateTime.Parse(year + "-" + month + "-" + num);
                            TimeSpan ts = date - today;

                            strMonth = ChineseDate.GetMonth(date);
                            strDay = ChineseDate.GetDay(date);
                            string strJieqi = ChineseDate.GetSolarTerm(date);
                            string strCHoliday = ChineseDate.GetChinaHoliday(date);
                            strHoliday = strJieqi + (string.IsNullOrEmpty(strJieqi) ? "" : " ")
                                + strCHoliday + (string.IsNullOrEmpty(strCHoliday) ? "" : " ") 
                                + ChineseDate.GetHoliday(date);                 
                            
                            if (i > 0 && i < 6) //周一到周五
                            {
                                //if (strHoliday.Length > 3)
                                if (!string.IsNullOrEmpty(strJieqi))
                                {
                                    iDays[j, i].iOldText = strHoliday;
                                    if (string.IsNullOrEmpty(strJieqi))
                                    {
                                        iDays[j, i].iOldColor = cIsHoliday;
                                    }
                                    else
                                    {
                                        iDays[j, i].iOldColor = cSolarTerm;
                                    }
                                }
                                else
                                {
                                    iDays[j, i].iOldText = strDay == "初一" ? strMonth : strDay;
                                    iDays[j, i].iOldColor = cNotHoliday;
                                }
                                iDays[j, i].iNewText = num.ToString(CultureInfo.InvariantCulture);
                                iDays[j, i].iNewColor = cWeekdays;
                            }
                            else
                            {
                                //if (strHoliday.Length > 3)
                                if (!string.IsNullOrEmpty(strJieqi))
                                {
                                    iDays[j, i].iOldText = strHoliday;
                                    if (string.IsNullOrEmpty(strJieqi))
                                    {
                                        iDays[j, i].iOldColor = cIsHoliday;
                                    }
                                    else
                                    {
                                        iDays[j, i].iOldColor = cSolarTerm;
                                    }
                                }
                                else
                                {
                                    iDays[j, i].iOldText = strDay == "初一" ? strMonth : strDay;
                                    iDays[j, i].iOldColor = cNotHoliday;
                                }
                                iDays[j, i].iNewText = num.ToString(CultureInfo.InvariantCulture);
                                iDays[j, i].iNewColor = cWeekend;
                            }
                            iDays[j, i].Jieri = strHoliday;
                            DataRow[] dr = dtMonth.Select(string.Format("Yangli = '{0}'", date.ToString("yyyy年M月d日")));
                            if (dr != null && dr.Length == 1)
                            {
                                iDays[j, i].Yangli = dr[0]["Yangli"].ToString() + " " + dr[0]["Xingqi"].ToString();
                                iDays[j, i].Nongli = dr[0]["Nongli"].ToString();
                                iDays[j, i].Zhigan = dr[0]["Zhigan"].ToString();
                                iDays[j, i].Xingzuo = dr[0]["Xingzuo"].ToString();
                                iDays[j, i].Yi = dr[0]["Yi"].ToString();
                                iDays[j, i].Ji = dr[0]["Ji"].ToString();
                            }
                            if (ts.Days == 0)
                            {
                                iDays[j, i].BackColor = cTodayBG;
                                SetDateDetail(iDays[j, i]);
                            }
                            iDays[j, i].Visible = true;

                            num++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            try
            {
                if (int_Month < 12)
                {
                    int_Month += 1;
                    DefualtDataTime(int_Year, int_Month);
                }
                else
                {
                    int_Month = 1;
                    int_Year += 1;
                    DefualtDataTime(int_Year, int_Month);
                }
                ResetPanel();
                GetWeekInfo(ref int_WeekOfFirstDay, ref int_DayOfMonth, int_Year, int_Month);
                FillPanelMonth(int_WeekOfFirstDay, int_DayOfMonth, int_Year, int_Month);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (int_Month >= 2)
                {
                    int_Month -= 1;
                    DefualtDataTime(int_Year, int_Month);
                }
                else
                {
                    int_Month = 12;
                    int_Year -= 1;
                    DefualtDataTime(int_Year, int_Month);
                }
                ResetPanel();
                GetWeekInfo(ref int_WeekOfFirstDay, ref int_DayOfMonth, int_Year, int_Month);
                FillPanelMonth(int_WeekOfFirstDay, int_DayOfMonth, int_Year, int_Month);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            CurrentDate();
            ResetPanel();
            GetWeekInfo(ref int_WeekOfFirstDay, ref int_DayOfMonth, int_Year, int_Month);
            FillPanelMonth(int_WeekOfFirstDay, int_DayOfMonth, int_Year, int_Month);
        }

        private void lab_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }

        private void lab_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Black;

        }

        private void ResetPanel()
        {
            try
            {
                if (bool_Flag)
                {
                    InitiDays();
                    bool_Flag = false;
                }

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (iDays[i, j] != null)
                        {
                            iDays[i, j].BackColor = Color.Transparent;
                            iDays[i, j].iNewText = "";
                            iDays[i, j].iOldText = "";
                            iDays[i, j].Yangli = "";
                            iDays[i, j].Nongli = "";
                            iDays[i, j].Zhigan = "";
                            iDays[i, j].Xingzuo = "";
                            iDays[i, j].Yi = "";
                            iDays[i, j].Ji = "";
                            iDays[i, j].Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitiDays()
        {
            try
            {
                int i = 0; int j = 0;
                for (int p = 1; p < 39; p++)
                {
                    foreach (Control con in panelMonth.Controls)
                    {
                        iDay day = con as iDay;                        
                        if (day.TabIndex == p)
                        {
                            day.Clicked += new iDay.BeClicked(day_Clicked);
                            iDays[i, j] = day;
                            j++;
                            if (j == 7)
                            {
                                i++;
                                j = 0;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void ResetiDaysColor()
        {
            try
            {
                if (bool_Flag)
                {
                    InitiDays();
                    bool_Flag = false;
                }

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (iDays[i, j] != null)
                        {
                            iDays[i, j].BackColor = Color.Transparent;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void day_Clicked(object sender, EventArgs e)
        {
            try
            {
                ResetiDaysColor();
                iDay day = sender as iDay;
                day.BackColor = Color.LightSalmon;
                SetDateDetail(day);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int_Year = this.comboBoxYear.SelectedIndex + 2013;
                int_Month = this.comboBoxMonth.SelectedIndex + 1;

                ResetPanel();
                if (int_Year != 0 && int_Month != 0)
                {
                    GetWeekInfo(ref int_WeekOfFirstDay, ref int_DayOfMonth, int_Year, int_Month);
                    FillPanelMonth(int_WeekOfFirstDay, int_DayOfMonth, int_Year, int_Month);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
                
        private void iDay_MouseEnter(object sender, EventArgs e)
        {

        }

        private void iDay_MouseLeave(object sender, EventArgs e)
        {
        }
        #endregion

        #region TabTixing
        private void BtnAddReminder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimeBeginTime.Value <= DateTime.Now)
                {
                    MessageBox.Show("开始时间必须大于当前时间");
                    return;
                }
                if (checkBoxUseMusic.Checked && string.IsNullOrEmpty(labelMusicPath.Text))
                {
                    MessageBox.Show("请选择提醒音乐");
                    return;
                }
                if (dic_Reminder.ContainsKey(dateTimeBeginTime.Value))
                {
                    MessageBox.Show("该时间已经存在待提醒事件，请勿重复添加");
                    return;
                }
                newReminder = new Reminder();
                for (int i = 0; i < 10000; i++)
                {
                    DataRow[] drs = table_Reminder.Select(string.Format("Id = {0}", i));
                    if (drs.Length == 0)
                    {
                        newReminder.ReminderId = i;
                        break;
                    }
                }
                newReminder.ReminderInfo = txtInfo.Text;
                newReminder.ReminderTime = dateTimeBeginTime.Value;

                int result = _data.AddReminder(newReminder);
                if (result == 1)
                {
                    LoadReminder();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void LoadReminder()
        {
            try
            {                
                DateTime now = DateTime.Now;
                dateTimeBeginTime.Value = now.AddMinutes(5);
                txtInfo.Text = "";

                _data.DeleteReminderExpired(now);//删除过期提醒
                table_Reminder = _data.GetReminders(now);                
                if (table_Reminder != null && table_Reminder.Rows.Count > 0)
                {
                    panelReminderList.Controls.Clear();
                    foreach (DataRow dr in table_Reminder.Rows)
                    {
                        Reminder rem = new Reminder();
                        rem.ReminderId = int.Parse(dr["Id"].ToString());
                        rem.ReminderTime = DateTime.Parse(dr["Time"].ToString());;
                        rem.ReminderInfo = dr["Info"].ToString();
                        iReminder ireminder = new iReminder(rem);
                        ireminder.Dock = DockStyle.Top;
                        ireminder.Reminderhandler += new iReminder.ReminderHandler(DeleteReminder);
                        panelReminderList.Controls.Add(ireminder);
                        if (!dic_Reminder.ContainsKey(rem.ReminderTime))
                        {
                            dic_Reminder.Add(rem.ReminderTime, rem);                            
                        }

                    }
                }
                else
                {
                    panelReminderList.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void DeleteReminder(Reminder dReminder)
        {
            try
            {
                _data.DeleteReminderById(dReminder.ReminderId);
                if (dic_Reminder.ContainsKey(dReminder.ReminderTime))
                {
                    dic_Reminder.Remove(dReminder.ReminderTime);
                }
                if (dic_Reminder.Count > 0)
                {
                    panelReminderList.Controls.Clear();
                    foreach (Reminder item in dic_Reminder.Values)
                    {
                        iReminder ireminder = new iReminder(item);
                        ireminder.Dock = DockStyle.Top;
                        ireminder.Reminderhandler += new iReminder.ReminderHandler(DeleteReminder);
                        panelReminderList.Controls.Add(ireminder);
                    }
                }
                else
                {
                    panelReminderList.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void ShowRemind(object reminder)
        {
            try
            {
                string musicPath = string.Empty;
                if (checkBoxUseMusic.Checked)
                {
                    musicPath = labelMusicPath.Text;
                }
                Thread.Sleep(3 * 1000);
                Reminder iReminder = reminder as Reminder;

                Application.Run(new Andwho.UI.ReminderForm(iReminder, musicPath));
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void checkBoxUseMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseMusic.Checked)
            {
                BtnSelectMusic.Enabled = true;
                _data.UpdateConfig("IsUseMusic", "true");
            }
            else
            {
                BtnSelectMusic.Enabled = false;
                _data.UpdateConfig("IsUseMusic", "false");
            }
        }
        
        private void BtnSelectMusic_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = string.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "D:\\";//注意这里写路径时要用c:\\而不是c:\
                openFileDialog.Filter = "MP3|*.mp3|WAV|*.wav|所有文件 (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fName = openFileDialog.FileName;
                }
                labelMusicPath.Text = fName;
                _data.UpdateConfig("MusicPath", fName);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                this.Visible = true;
                tabControl.SelectedIndex = 1;
            }
        }

        private void BtnSelectMusic_MouseEnter(object sender, EventArgs e)
        {
            if (checkBoxUseMusic.Checked)
            {
                BtnSelectMusic.BackColor = Color.DodgerBlue;
            }
        }

        private void BtnSelectMusic_MouseLeave(object sender, EventArgs e)
        {
            BtnSelectMusic.BackColor = Color.Transparent;
        }
        #endregion

        #region TabDingshi
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = string.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "D:\\";//注意这里写路径时要用c:\\而不是c:\
                openFileDialog.Filter = "EXE文件|*.exe|MSI文件|*.msi";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fName = openFileDialog.FileName;
                }
                labelFilepath.Text = fName;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                this.Visible = true;
                tabControl.SelectedIndex = 2;
            }
        }

        private void BtnAddTimedEvents_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelFilepath.Visible && string.IsNullOrEmpty(labelFilepath.Text))
                {
                    MessageBox.Show("请选择需要打开的软件");
                    this.Visible = true;
                    tabControl.SelectedIndex = 2;
                    return;
                }
                newTimerEvent = new TimedEvent();
                for (int i = 0; i < 10000; i++)
                {
                    DataRow[] drs = table_TimedEvents.Select(string.Format("Id = {0}", i));
                    if (drs.Length == 0)
                    {
                        newTimerEvent.Id = i;
                        break;
                    }
                }
                newTimerEvent.Frequency = cbbFrequency.Text;
                newTimerEvent.Time = dateTimeEvent.Value;
                newTimerEvent.ExecEvents = cbbEvent.Text;
                newTimerEvent.FilePath = labelFilepath.Text;

                int result = _data.AddTimedEvent(newTimerEvent);
                if (result == 1)
                {
                    LoadTimedEvents();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void ResetTimedEventCondition()
        {
            try
            {
                dateTimeEvent.Value = DateTime.Now.AddMinutes(5);
                cbbFrequency.SelectedIndex = 0;
                cbbEvent.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void LoadTimedEvents()
        {
            try
            {
                ResetTimedEventCondition();
                _data.DeleteTimedEvent(-1);//删除仅执行一次的过期定时事件
                table_TimedEvents = new DataTable();
                table_TimedEvents = _data.GetTimedEvents();
                panelTimedList.Controls.Clear();
                list_TimedEvent.Clear();
                if (table_TimedEvents != null && table_TimedEvents.Rows.Count > 0)
                {                    
                    foreach (DataRow dr in table_TimedEvents.Rows)
                    {
                        TimedEvent te = new TimedEvent();
                        te.Id = int.Parse(dr["Id"].ToString());
                        te.Frequency = dr["Frequency"].ToString();
                        te.Time = DateTime.Parse(dr["Time"].ToString());;
                        te.ExecEvents = dr["ExecEvents"].ToString();
                        te.FilePath = dr["FilePath"].ToString();

                        iTimer itimer = new iTimer(te);
                        itimer.Dock = DockStyle.Top;
                        itimer.iTimerhandler += new iTimer.iTimerHandler(DeleteTimedEvent);
                        panelTimedList.Controls.Add(itimer);
                        if (!list_TimedEvent.Contains(te)) list_TimedEvent.Add(te);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private int DeleteTimedEvent(TimedEvent te)
        {
            int result = 0;
            try
            {
                result = _data.DeleteTimedEvent(te.Id);
                if (result == 0)
                {
                    MessageBox.Show("删除定时事件失败");
                    return result;
                }

                if (list_TimedEvent.Contains(te))
                {
                    list_TimedEvent.Remove(te);
                }
                panelTimedList.Controls.Clear();
                if (list_TimedEvent.Count > 0)
                {                    
                    foreach (TimedEvent item in list_TimedEvent)
                    {
                        iTimer itimer = new iTimer(item);
                        itimer.Dock = DockStyle.Top;
                        itimer.iTimerhandler += new iTimer.iTimerHandler(DeleteTimedEvent);
                        panelTimedList.Controls.Add(itimer);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        private void cbbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbEvent.SelectedIndex == 1)
                {
                    BtnSelectFile.Visible = true;
                    labelFilepath.Visible = true;
                }
                else
                {
                    BtnSelectFile.Visible = false;
                    labelFilepath.Text = "";
                    labelFilepath.Visible = false;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        #region TabTianqi
        private void RefreshWeather()
        {
            try
            {
                string url = "http://m.weather.com.cn/data/" + string_CityCode + ".html";
                Encoding encoding = Encoding.UTF8;
                GetWebContent gwc = new GetWebContent();
                string result = gwc.GetWebContentByUrl(url, encoding);
                if (result.Contains("链接超时") || result.Contains("链接异常")) return;
                result = GetJsonContent(result);
                if (string.IsNullOrEmpty(result)) return;
                WeatherInfo wi = JSON.Json2Object<WeatherInfo>(result);

                weatherCity.Text = wi.city;//城市
                weatherDate.Text = wi.date_y;//日期
                weatherWeek.Text = wi.week;//星期
                weatherTemp.Text = "温度  " + wi.temp1;//温度
                weatherHumidity.Text = "";//湿度
                weatherWeather.Text = wi.weather1;//天气
                weatherWind.Text = wi.wind1;//风向
                if (File.Exists(Application.StartupPath + @"\Image\" + wi.img1 + ".png"))
                {
                    weatherImg.Image = Image.FromFile(Application.StartupPath + @"\Image\" + wi.img1 + ".png");
                }
                weatherComplex.Text = "紫外线：" + wi.index_uv + "\r\n" +
                                      "洗车：" + wi.index_xc + "\r\n" +
                                      "旅游：" + wi.index_tr + "\r\n" +
                                      "舒适指数：" + wi.index_co + "\r\n" +
                                      "晨练：" + wi.index_cl + "\r\n" +
                                      "晾晒：" + wi.index_ls + "\r\n" +
                                      "过敏：" + wi.index_ag + "\r\n" +
                                      "今日穿衣指数：" + wi.index_d;
                weatherTime.Text = "更新时间：" + DateTime.Now.ToString("HH:mm:ss"); 
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private String GetJsonContent(String weatherQueryResult)
        {
            string pattern = "{\"weatherinfo\":(.*)}";
            var result = Regex.Match(weatherQueryResult, pattern, RegexOptions.IgnoreCase).Groups;
            if (result.Count > 1)
            {
                return result[1].Value;
            }
            return string.Empty;
        }

        #endregion

        #region +TabBianqian
        private void BtnAddNotepad_Click(object sender, EventArgs e)
        {
            txtNotepadTitle.Text = "";
            txtNotepadContent.Text = "";
            string_NotepadCreateTime = "";
            BtnSaveNotepad.Enabled = false;
            panelEditNotepad.Visible = true;
        }

        private void BtnSaveNotepad_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNotepadTitle.Text.Trim()))
                {
                    MessageBox.Show("请输入标题");
                    return;
                }
                SaveNotepad();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void BtnCloseEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnSaveNotepad.Enabled)
                {
                    DialogResult dr = MessageBox.Show("关闭前是否保存最后更改的内容？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (string.IsNullOrEmpty(txtNotepadTitle.Text.Trim()))
                        {
                            MessageBox.Show("请输入标题");
                            return;
                        }
                        SaveNotepad();
                    }
                }
                panelEditNotepad.Visible = false;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                if (bianqianTimer.Enabled) bianqianTimer.Stop();
            }
        }
                
        private void LoadNotepad()
        {
            try
            {
                table_Notepad = new DataTable();
                table_Notepad = _data.GetNotepad();
                panelNotepadList.Controls.Clear();
                if (table_Notepad != null && table_Notepad.Rows.Count > 0)
                {                    
                    foreach (DataRow dr in table_Notepad.Rows)
                    {
                        DateTime dt = DateTime.Parse(dr["CreateTime"].ToString());
                        iNotepad notepad = new iNotepad(dt.ToString("yyyy-MM-dd HH:mm:ss"), dr["Title"].ToString());
                        notepad.Dock = DockStyle.Top;
                        notepad.DeleteEvent += new iNotepad.DeleteEventHandler(DeleteNotepad);
                        notepad.EditEvent += new iNotepad.EditEventHandler(EditNotepad);
                        panelNotepadList.Controls.Add(notepad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void SaveNotepad()
        {
            try
            {                
                if (string.IsNullOrEmpty(string_NotepadCreateTime))
                {
                    int result = _data.AddNotepad(txtNotepadTitle.Text, txtNotepadContent.Text);
                    if (result == 1)
                    {
                        BtnSaveNotepad.Enabled = false;
                        if (bianqianTimer.Enabled) bianqianTimer.Stop();
                        LoadNotepad();
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
                else
                {
                    int result = _data.UpdateNotepad(string_NotepadCreateTime, txtNotepadTitle.Text, txtNotepadContent.Text);
                    if (result == 1)
                    {
                        BtnSaveNotepad.Enabled = false;
                        if (bianqianTimer.Enabled) bianqianTimer.Stop();
                        LoadNotepad();
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DeleteNotepad(string createTime)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否要删除该便签？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) return;
                int result = _data.DeleteNotepad(createTime);
                if (result == 1)
                {
                    LoadNotepad();
                    if (string_NotepadCreateTime == createTime)
                    {
                        txtNotepadTitle.Text = "";
                        txtNotepadContent.Text = "";
                        string_NotepadCreateTime = "";
                        BtnSaveNotepad.Enabled = false;
                        panelEditNotepad.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void EditNotepad(string createTime)
        {
            try
            {
                if (BtnSaveNotepad.Enabled)
                {
                    DialogResult dr = MessageBox.Show("是否保存当前文档最后更改的内容？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (string.IsNullOrEmpty(txtNotepadTitle.Text.Trim()))
                        {
                            MessageBox.Show("请输入标题");
                            return;
                        }
                        SaveNotepad();
                    }
                }
                string_NotepadCreateTime = createTime;
                DataTable table_NotepadEdit = _data.GetNotepadByCreateTime(createTime);
                
                if (table_NotepadEdit != null && table_NotepadEdit.Rows.Count > 0)
                {
                    panelEditNotepad.Visible = true;
                    txtNotepadTitle.Text = table_NotepadEdit.Rows[0]["Title"].ToString();
                    txtNotepadContent.Clear();
                    txtNotepadContent.Focus();
                    txtNotepadContent.AppendText(table_NotepadEdit.Rows[0]["Content"].ToString());
                }
                BtnSaveNotepad.Enabled = false;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void Notepad_TextChanged(object sender, EventArgs e)
        {
            BtnSaveNotepad.Enabled = true;
            if (!bianqianTimer.Enabled) bianqianTimer.Start();
        }

        private void txtNotepadContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
        #endregion

        #region 事件
        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                btn.BackColor = Color.DodgerBlue;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                btn.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
                
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimeBeginTime.Value = DateTime.Now.AddMinutes(5);
                dateTimeEvent.Value = DateTime.Now.AddMinutes(5);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            
        }

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(((TabControl)sender).TabPages[e.Index].Text,
                System.Windows.Forms.SystemInformation.MenuFont,
                new SolidBrush(Color.Black),
                e.Bounds,
                sf);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        #region 处理Label控件闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion
    }
}
