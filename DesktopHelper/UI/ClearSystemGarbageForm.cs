using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andwho.Windows.Forms;
using Andwho.Windows.Forms.Metro;
using System.IO;
using Andwho.Logger;
using System.Threading;
using System.Globalization;
using DesktopHelper.Util.WinShellApi;

namespace DesktopHelper.UI
{
    public partial class ClearSystemGarbageForm : QQForm
    {
        public static ClearSystemGarbageForm form = null;
        private static object synLock = new object();
        FileInfo fi = null;
        long long_TotalSize = 0;
        long long_TotalItems = 0;
        Log log = new Log(true);

        public static ClearSystemGarbageForm GetInstance()
        {
            if (form == null)
            {
                lock (synLock)
                {
                    if (form == null)
                        form = new ClearSystemGarbageForm();
                }
            }
            return form;
        }

        private ClearSystemGarbageForm()
        {
            InitializeComponent();
        }

        private void ClearSystemGarbageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form != null)
            {
                form.Dispose();
                form = null;
            }
        }

        private void BtnStartClear_Click(object sender, EventArgs e)
        {
            labelTip.Visible = true;
            labelTotalSize.Text = "";
            long_TotalSize = 0;
            Thread thread_ClearGarbage = new Thread(ClearSystemGarbage);
            //thread_ClearGarbage.SetApartmentState(ApartmentState.STA);
            thread_ClearGarbage.IsBackground = true;
            thread_ClearGarbage.Start();
        }

        private void ClearSystemGarbage()
        {
            ClearRecycle();//清空回收站
            ClearTemp();//清空临时文件
        }

        //清空回收站
        private void ClearRecycle()
        {
            try
            {
                SetControlText(labelDetail, "开始清理系统垃圾......\r\n");
                RecycleBinInfo rInfo = new RecycleBinInfo();
                long long_RecycleSize = 0;
                long long_RecycleItems = 0;
                uint result = rInfo.QuerySizeRecycleBin(out long_RecycleSize, out long_RecycleItems);
                if (result == 0)
                {
                    long_TotalSize += long_RecycleSize;
                    long_TotalItems += long_RecycleItems;
                }
                SetControlText(labelDetail, "正在清空回收站......\r\n");
                result = rInfo.EmptyRecycleBin(IntPtr.Zero, null, SHERB.SHERB_SILENT);
                SetControlText(labelTotalSize, ConvertSize(long_TotalSize));
            }
            catch
            { 
            }
            #region
            //string[] disks = Environment.GetLogicalDrives();//获取硬盘上面的逻辑驱动器
            //foreach (string disk in disks)
            //{
            //    string path = disk + "";
            //    if (Directory.Exists(path))
            //    {
            //        try
            //        {
            //            deleteFiles(path, "*.*");
            //        }
            //        catch (Exception ex)
            //        {
            //            log.WriteLog(ex.ToString());
            //        }
            //        finally
            //        {
            //            SetControlText(labelDetail, "清空回收站完成!\r\n");
            //        }
            //    }
            //}  
            #endregion
        }

        //清理临时文件
        private void ClearTemp()
        {
            try
            {
                string cookices = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
                string history = Environment.GetFolderPath(Environment.SpecialFolder.History);
                string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                string internetCach = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                string userName = Environment.UserName;
                string temp = @"c:\documents and settings\" + userName + @"\local settings\temp";
                string windows = Environment.GetEnvironmentVariable("WinDir");
                string windowsTemp = Environment.GetEnvironmentVariable("TEMP");
                string[] all ={history ,recent ,internetCach ,temp,windows + "\\Temp",windowsTemp,
                windows+ "\\servicepackfiles",windows+"\\driver cache\\i386",windows+"\\softwaredistribution\\download",
                windows+"\\help"};
                foreach (string d in all)
                {
                    if (Directory.Exists(d))
                    {
                        foreach (string file in Directory.GetFiles(d, "*.*"))
                        {
                            DeleteFile(file);
                        }
                    }
                }
                foreach (string file in Directory.GetFiles(cookices, "*.txt"))
                {
                    DeleteFile(file);
                }
                SetControlText(labelDetail, "清理完成！");
            }
            catch
            {
            }
        }

        private void DeleteFile(string file)
        {
            try
            {
                fi = new FileInfo(file);
                long size = fi.Length;

                SetControlText(labelDetail, "正在清除文件：" + file + "\r\n");
                File.Delete(file);

                long_TotalSize += size;
                long_TotalItems++;
                SetControlText(labelTotalSize, ConvertSize(long_TotalSize));
            }
            catch
            {
            }
        }

        delegate void SetControlTextHandler(Control control, string text);

        private void SetControlText(Control control, string text)
        {
            try
            {
                if (control.InvokeRequired == true)
                {
                    SetControlTextHandler set = new SetControlTextHandler(SetControlText);
                    control.Invoke(set, new object[] { control, text });
                }
                else
                {
                    control.Text = text;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private string ConvertSize(long byteSize)
        {
            string str = string.Empty;
            try
            {
                float tempf = (float)byteSize;
                if (tempf / 1024 > 1)
                {
                    if (((tempf / 1024) / 1024) / 1024 > 1)
                    {
                        str = (((tempf / 1024) / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "GB";
                    }
                    else if ((tempf / 1024) / 1024 > 1)
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
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return str;
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
