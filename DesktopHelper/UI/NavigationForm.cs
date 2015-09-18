using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DesktopHelper.Util;
using System.Data;
using DesktopHelper.DataAccess;
using System.Runtime.InteropServices;
using Andwho.Logger;

namespace DesktopHelper.UI
{
    public partial class NavigationForm : Form
    {
        private System.Drawing.Rectangle rectangle_Screen;
        private System.Drawing.Rectangle rectangle_Show;//定义一个存储矩形框的区域
        private static int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        private static int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏
        private DataTable table_Website = new DataTable();
        private NavigationData _data = new NavigationData();
        private Color color_Website = Color.Blue;
        private string string_Search = "http://www.baidu.com/s?wd=";
        private Log log = new Log(true);

        public NavigationForm()
        {
            InitializeComponent();
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
                #region
                switch (StopAanhor)
                {
                    case AnchorStyles.Top:
                        if (x > this.Width / 2)
                        {
                            if ((rectangle_Screen.Right - x) > this.Width / 2)
                            {
                                this.rectangle_Show = new Rectangle(x + 28 - this.Width / 2, y + 58, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_Show = new Rectangle(x - this.Width + 56, y + 58, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_Show = new Rectangle(x, y + 58, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Left:
                        if (y > this.Height / 2)
                        {
                            if ((rectangle_Screen.Bottom - y) > this.Height / 2)
                            {
                                this.rectangle_Show = new Rectangle(x + 58, y + 28 - this.Height / 2, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_Show = new Rectangle(x + 58, y - this.Height + 56, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_Show = new Rectangle(x + 58, y, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Right:
                        if (y > this.Height / 2)
                        {
                            if ((rectangle_Screen.Bottom - y) > this.Height / 2)
                            {
                                this.rectangle_Show = new Rectangle(x - this.Width, y + 28 - this.Height / 2, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_Show = new Rectangle(x - this.Width, y - this.Height + 56, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_Show = new Rectangle(x - this.Width, y, this.Width, this.Height);
                        }
                        break;
                    case AnchorStyles.Bottom:
                        if (x > this.Width / 2)
                        {
                            if ((rectangle_Screen.Right - x) > this.Width / 2)
                            {
                                this.rectangle_Show = new Rectangle(x + 28 - this.Width / 2, y - this.Height, this.Width, this.Height);
                            }
                            else
                            {
                                this.rectangle_Show = new Rectangle(x - this.Width + 56, y - this.Height, this.Width, this.Height);
                            }
                        }
                        else
                        {
                            this.rectangle_Show = new Rectangle(x, y - this.Height, this.Width, this.Height);
                        }
                        break;
                }
                #endregion
                this.SetBounds(rectangle_Show.X, rectangle_Show.Y, rectangle_Show.Width, rectangle_Show.Height);//设置当前窗体的边界  
                ShowWindow(this.Handle, 4, AW_SLIDE + AW_VER_NEGATIVE);//动态显示本窗体
            }
            catch
            { }
        }

        private void NavigationForm_Load(object sender, EventArgs e)
        {
            rectangle_Screen = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
            GetFixedWebsiteData();
            InitSearchAndConfigWebsite();
            MouseDetection();
            AppTimerEvent();
        }

        private void AppTimerEvent()
        {
            System.Windows.Forms.Timer appTimer = new System.Windows.Forms.Timer();
            appTimer.Interval = 3600 * 1000;
            appTimer.Tick += new EventHandler(TimerTick);
            appTimer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateWebsiteData();
        }

        private void UpdateWebsiteData()
        {
            try
            {
                UpdateWebsiteXML update = new UpdateWebsiteXML();
                bool result = update.Download();
                if (result)
                {
                    DataMigration dm = new DataMigration();
                    dm.LoadWebsiteXML();
                    //刷新网址数据
                    GetFixedWebsiteData();
                    InitConfigWebsite();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void GetFixedWebsiteData()
        {
            try
            {
                table_Website = new DataTable();
                table_Website = _data.GetFixedWebsite();
                if (table_Website != null && table_Website.Rows.Count > 0) InitFixedWebsitePanel();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitFixedWebsitePanel()
        {
            try
            {
                DataRow[] drTabChangyong = table_Website.Select("Type = '0'");
                if (drTabChangyong != null && drTabChangyong.Length > 0) InitTabLabels(drTabChangyong, tabChangyong);

                DataRow[] drTabXinwen = table_Website.Select("Type = '1'");
                if (drTabXinwen != null && drTabXinwen.Length > 0) InitTabLabels(drTabXinwen, tabXinwen);

                DataRow[] drTabShipin = table_Website.Select("Type = '2'");
                if (drTabShipin != null && drTabShipin.Length > 0) InitTabLabels(drTabShipin, tabShipin);

                DataRow[] drTabGouwu = table_Website.Select("Type = '3'");
                if (drTabGouwu != null && drTabGouwu.Length > 0) InitTabLabels(drTabGouwu, tabGouwu);

                DataRow[] drTabShenghuo = table_Website.Select("Type = '4'");
                if (drTabShenghuo != null && drTabShenghuo.Length > 0) InitTabLabels(drTabShenghuo, tabShenghuo);

                DataRow[] drTabShequ = table_Website.Select("Type = '5'");
                if (drTabShequ != null && drTabShequ.Length > 0) InitTabLabels(drTabShequ, tabShequ);

                DataRow[] drTabYouxi = table_Website.Select("Type = '6'");
                if (drTabYouxi != null && drTabYouxi.Length > 0) InitTabLabels(drTabYouxi, tabYouxi);

                DataRow[] drTabYouqu = table_Website.Select("Type = '7'");
                if (drTabYouqu != null && drTabYouqu.Length > 0) InitTabLabels(drTabYouqu, tabYouqu);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitTabLabels(DataRow[] dr, TabPage tabpage)
        {
            try
            {
                Label[] labels = GetTabpageLabels(tabpage);

                if (labels != null && labels.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        labels[i].Text = dr[i]["Name"].ToString();
                        labels[i].Tag = dr[i]["URL"];
                        labels[i].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private Label[] GetTabpageLabels(TabPage tabpage)
        {
            Label[] labels = new Label[36];
            try
            {
                foreach (Control con in tabpage.Controls)
                {
                    Label label = con as Label;
                    if (label != null && label.TabIndex != 0)
                    {
                        labels[label.TabIndex - 1] = label;
                        if (label.TabIndex - 1 > 16)
                        {
                            labels[label.TabIndex - 1].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return labels;
        }

        private void InitSearchAndConfigWebsite()
        {
            InitSearch();
            InitConfigWebsite();
        }

        private void InitSearch()
        {
            try
            {
                string search = _data.GetDefaultSearch();
                switch (search)
                {
                    case "1"://百度
                        labelSearch.Text = "百度";
                        string_Search = "http://www.baidu.com/s?wd=";
                        break;
                    case "2"://谷歌
                        labelSearch.Text = "谷歌";
                        string_Search = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=";
                        break;
                    case "3"://雅虎
                        labelSearch.Text = "雅虎";
                        string_Search = "http://sg.search.yahoo.com/search;_ylt=AwrSbjMLkPlSsSgA7Eci4gt.?p=";
                        break;
                    case "4"://SOSO
                        labelSearch.Text = "SOSO";
                        string_Search = "http://www.soso.com/q?w=";
                        break;
                    case "5"://必应
                        labelSearch.Text = "必应";
                        string_Search = "http://cn.bing.com/search?q=";
                        break;
                    case "6"://360
                        labelSearch.Text = "360";
                        string_Search = "http://www.so.com/s?q=";
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitConfigWebsite()
        {
            try
            {
                DataTable dtConfigWebsite = _data.GetConfigWebsite();
                if (dtConfigWebsite != null && dtConfigWebsite.Rows.Count > 0)
                {
                    Label[] labels = GetTabpageLabels(tabChangyong);

                    if (labels != null && labels.Length > 16)
                    {
                        for (int i = 0; i < dtConfigWebsite.Rows.Count; i++)
                        {
                            labels[i + 16].Text = dtConfigWebsite.Rows[i]["Name"].ToString();
                            labels[i + 16].Tag = dtConfigWebsite.Rows[i]["URL"];
                            labels[i + 16].Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void MouseDetection()
        {
            System.Windows.Forms.Timer mouseTimer = new System.Windows.Forms.Timer();
            mouseTimer.Interval = 100;
            mouseTimer.Tick += new EventHandler(MouseTick);
            mouseTimer.Start();
        }

        private void MouseTick(object sender, EventArgs e)
        {
            try
            {
                if (!this.Bounds.Contains(Control.MousePosition)                    
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

        private string URLKeywordConvert(string keyword)
        {
            keyword = keyword.Replace("+", "%2B");
            keyword = keyword.Replace(" ", "+");
            keyword = keyword.Replace("/", "%2F");
            keyword = keyword.Replace("?", "%3F");
            keyword = keyword.Replace("？", "%3F");
            keyword = keyword.Replace("%","%25");
            keyword = keyword.Replace("#", "%23");
            keyword = keyword.Replace("&", "%26");
            keyword = keyword.Replace("=", "%3D");

            return keyword;
        }
        #region
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text;
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = URLKeywordConvert(keyword);
                    string target = string_Search + keyword;

                    System.Diagnostics.Process.Start(target);
                }
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    log.WriteLog(noBrowser.ToString());
                    MessageBox.Show(noBrowser.Message);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                txtSearch.Text = "";
                this.Hide();
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigForm form = ConfigForm.GetInstance();
                form.ReloadConfigHandlor += new ConfigForm.ReloadConfigHandler(InitSearchAndConfigWebsite);
                form.Show();
                form.Activate();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private void Website_Click(object sender, EventArgs e)
        {
            string target = "http://www.andwho.com/";
            try
            {
                Label label = sender as Label;
                if (label.Tag != null)
                {
                    target = (string)label.Tag;
                }
                System.Diagnostics.Process.Start(target);
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    log.WriteLog(noBrowser.ToString());
                    MessageBox.Show(noBrowser.Message);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            finally
            {
                this.Hide();
            }
        }

        private void Website_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label label = sender as Label;
                color_Website = label.ForeColor;
                label.ForeColor = Color.DodgerBlue;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void Website_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label label = sender as Label;
                if (color_Website == Color.Blue)
                    return;
                label.ForeColor = color_Website;
                color_Website = Color.Blue;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
        #endregion

        private void NavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void BtnSet_MouseEnter(object sender, EventArgs e)
        {
            BtnSet.BackColor = Color.DodgerBlue;
        }

        private void BtnSet_MouseLeave(object sender, EventArgs e)
        {
            BtnSet.BackColor = Color.Transparent;
        }

        private void BtnSearch_MouseEnter(object sender, EventArgs e)
        {
            BtnSearch.BackColor = Color.DodgerBlue;
        }

        private void BtnSearch_MouseLeave(object sender, EventArgs e)
        {
            BtnSearch.BackColor = Color.Transparent;
        }

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
