using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Runtime.InteropServices;

namespace aUpdate
{
    public partial class UpdateForm : Form
    {
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的区域
        private static int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        private static int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏 
        private WebClient downWebClient = new WebClient();
        private static long totalFilesSize;//所有文件大小 
        private static int totalFilesCount;//文件总数 
        private static List<string> fileNameList = new List<string>();
        private static int finishFilesCount;//已更新文件数 
        private static long finishFileSize;//已更新文件大小 
        private static string currentFileName;//当前文件名 
        private static long currentFileSize;//当前文件大小 

        #region 声明API函数
        [DllImportAttribute("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int dwTime, int dwFlags);
        #endregion

        public UpdateForm()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            try
            {
                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
                this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height);//为实例化的对象创建工作区域

                this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);//设置当前窗体的边界
                ShowWindow(this.Handle, 4, AW_SLIDE + AW_VER_NEGATIVE);//动态显示本窗体
            }
            catch
            { }
        }

        // 开始更新 
        private void UpdaterStart()
        {
            try
            {
                float tempf;
                //委托下载数据时事件 
                this.downWebClient.DownloadProgressChanged += delegate(object sender, System.Net.DownloadProgressChangedEventArgs e)
                {
                    this.labelCurrent.Text = String.Format(
                        CultureInfo.InvariantCulture,
                        "正在下载:{0}  [ {1}/{2} ]",
                        currentFileName,
                        ConvertSize(e.BytesReceived),
                        ConvertSize(e.TotalBytesToReceive));

                    currentFileSize = e.TotalBytesToReceive;
                    tempf = ((float)(finishFileSize + e.BytesReceived) / totalFilesSize);
                    this.progressBarTotal.Value = Convert.ToInt32(tempf * 100);
                    this.progressBarCurrent.Value = e.ProgressPercentage;
                };
                //委托下载完成时事件 
                this.downWebClient.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                {
                    if (e.Error != null)
                    {
                        UpdaterClose();
                    }
                    else
                    {                        
                        finishFileSize += currentFileSize;
                        if (fileNameList.Count > finishFilesCount)
                        {
                            DownloadFile(finishFilesCount);
                        }
                        else
                        {
                            MoveFiles();
                            UpdaterClose();
                        }
                    }
                };

                UpdateList();
                if (totalFilesSize == 0)
                {
                    UpdaterClose();
                }
                finishFilesCount = 0;
                finishFileSize = 0;
                if (fileNameList.Count > 0)
                    DownloadFile(0);
            }
            catch
            {
            }
        }

        // 获取文件列表并下载 
        private static void UpdateList()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Application.StartupPath + @"\Update\DeskHelper.xml");
                XmlNodeList clientNodes;
                clientNodes = xDoc.SelectNodes("//client");
                if (clientNodes.Count > 0)
                {
                    foreach (XmlNode xNode in clientNodes)
                    {
                        try
                        {
                            //获取需要更新的文件总大小
                            totalFilesSize = long.Parse(xNode.Attributes["size"].Value);
                        }
                        catch
                        {
                        }
                        //获取需要更新的文件名称
                        foreach (XmlNode cNode in xNode.ChildNodes)
                        {
                            if (cNode.Name.Equals("file"))
                            {
                                fileNameList.Add(cNode.InnerText);
                            }
                        }
                        totalFilesCount = fileNameList.Count;
                    }
                }
            }
            catch
            {
                UpdaterClose();
            }
        }

        // 下载文件
        private void DownloadFile(int arry)
        {
            try
            {
                finishFilesCount++;
                currentFileName = fileNameList[arry];
                this.labelTotal.Text = String.Format(
                    CultureInfo.InvariantCulture,
                    "升级进度 {0}/{1}  [ {2} ]",
                    finishFilesCount,
                    totalFilesCount,
                    ConvertSize(totalFilesSize));

                this.progressBarCurrent.Value = 0;
                this.downWebClient.DownloadFileAsync(
                    new Uri("http://www.andwho.com/AndwhoUpdate/DeskHelper/" + currentFileName),
                    Application.StartupPath + @"\Update\" + currentFileName);
            }
            catch
            {
                UpdaterClose();
            }
        }

        // 将更新文件移至启动目录
        private void MoveFiles()
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\Update\DeskHelper.xml"))
                {
                    File.Delete(Application.StartupPath + @"\Update\DeskHelper.xml");
                }
                foreach (string fileName in fileNameList)
                {
                    if (fileName.Contains("aUpdate"))
                    {
                        continue;
                    }
                    else if (fileName.Contains(".xml"))
                    {
                        if (File.Exists(Application.StartupPath + @"\XML\" + fileName))
                        {
                            File.Delete(Application.StartupPath + @"\XML\" + fileName);
                        }
                        File.Move(Application.StartupPath + @"\Update\" + fileName, Application.StartupPath + @"\XML\" + fileName);
                    }
                    else
                    {
                        if (File.Exists(Application.StartupPath + @"\" + fileName))
                        {
                            File.Delete(Application.StartupPath + @"\" + fileName);
                        }
                        File.Move(Application.StartupPath + @"\Update\" + fileName, Application.StartupPath + @"\" + fileName);
                    }
                }
            }
            catch
            {
                UpdaterClose();
            }
        }

        // 关闭程序
        private static void UpdaterClose()
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\DesktopHelper.exe");
            }
            catch
            {
            }
            Environment.Exit(0);
        }

        // 转换字节大小
        private static string ConvertSize(long byteSize)
        {
            string str = string.Empty;
            float tempf = (float)byteSize;
            if (tempf / 1024 > 1)
            {
                if ((tempf / 1024) / 1024 > 1)
                {
                    str = ((tempf / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "MB";
                }
                else
                {
                    str = (tempf / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "KB";
                }
            }
            else
            {
                str = tempf.ToString(CultureInfo.InvariantCulture) + "B";
            }
            return str;
        }

        private void UpdateForm_Activated(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Hide();
            //    System.Threading.Thread.Sleep(1000);
            //    new DownloadFiles().MoveFile();
            //    //CopyFiles.MoveFile();
            //    Process.Start(Application.StartupPath + "\\客户端.exe", "Update");
            //    Application.Exit();
            //    Process.GetCurrentProcess().Kill();
            //}
            //catch (Exception ex)
            //{
                
            //    MessageBox.Show("更新程序出错:" + ex.Message);
            //    Application.Exit();
            //    Process.GetCurrentProcess().Kill();
            //}
        }

        private void UpdateForm_Resize(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            ShowForm();
            UpdaterStart(); 
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.notifyIcon.Visible = true;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }
    }
}
