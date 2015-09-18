using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DesktopHelper.DataAccess;
using Andwho.Logger;
using DesktopHelper.Util;
using DesktopHelper.Entity;

namespace DesktopHelper.UI
{
    public partial class ConfigForm : Form
    {
        private static ConfigForm form_Config = null;
        private static object synLock = new object();
        AddWebsiteForm form_AddWebsite = null;
        private int int_WebsiteCount = 0;
        private iWebsite[] _iWebsite = new iWebsite[16];
        private ConfigData _data = new ConfigData();
        private bool bool_IsStarting = true;
        public delegate void ReloadConfigHandler();
        public event ReloadConfigHandler ReloadConfigHandlor;
        Log log = new Log(true);

        public static ConfigForm GetInstance()
        {
            if (form_Config == null)
            {
                lock (synLock)
                {
                    if (form_Config == null) form_Config = new ConfigForm();
                }
            }
            return form_Config;
        }

        private ConfigForm()
        {
            InitializeComponent();
            Init();
            bool_IsStarting = false;
        }

        private void Init()
        {
            ////InitCityData();
            //InitCiyCondition();
            InitTabCommon();
            InitTabWebsite();            
        }

        private void InitCityData()
        {
            try
            {
                UpdateCityListXML update = new UpdateCityListXML();
                bool result = update.Download();
                if (result)
                {
                    DataMigration dm = new DataMigration();
                    dm.LoadCityXML();
                    //刷新城市数据
                    InitCiyCondition();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitCiyCondition()
        {
            try
            {
                DataTable table_CitySheng = _data.GetCityData(ConfigData.CityType.Sheng, "");
                if (table_CitySheng != null && table_CitySheng.Rows.Count > 0)
                {
                    foreach (DataRow dr in table_CitySheng.Rows)
                    {
                        string sheng = dr["Name"].ToString();
                        comboBoxSheng.Items.Add(new ComboBoxItem(sheng, sheng));
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitTabCommon()
        {
            try
            {
                DataTable dtConfig = _data.GetConfig();
                string name = string.Empty;
                string value = string.Empty;
                if (dtConfig != null && dtConfig.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtConfig.Rows)
                    {
                        name = dr["Name"].ToString();
                        value = dr["Value"].ToString();
                        SetDefaultConfig(name, value);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void SetDefaultConfig(string name, string value)
        {
            try
            {
                switch (name)
                {
                    case "IsAutoStart":
                        if (value.Equals("true")) AutoStart.Checked = true;
                        else AutoStart.Checked = false;
                        break;
                    case "DefaultSearch":
                        SetDefaultSearch(value);
                        break;
                    case "CityCode":
                        SetDefaultCity(value);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void SetDefaultSearch(string value)
        {
            try
            {
                switch (value)
                {
                    case "1":
                        RbtnBaidu.Checked = true;
                        break;
                    case "2":
                        RbtnGoogle.Checked = true;
                        break;
                    case "3":
                        RbtnYahoo.Checked = true;
                        break;
                    case "4":
                        RbtnSoso.Checked = true;
                        break;
                    case "5":
                        RbtnBing.Checked = true;
                        break;
                    case "6":
                        Rbtn360.Checked = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        string str_SHI = string.Empty;
        string str_XIAN = string.Empty;
        private void SetDefaultCity(string code)
        {
            try
            {
                DataTable table_CityInfo = _data.GetCityInfoByCode(code);
                if (table_CityInfo != null && table_CityInfo.Rows.Count == 1)
                {
                    string sheng = table_CityInfo.Rows[0]["ShengParent"].ToString();
                    str_SHI = table_CityInfo.Rows[0]["ShiParent"].ToString();
                    str_XIAN = table_CityInfo.Rows[0]["Code"].ToString();
                    for (int i = 0; i < comboBoxSheng.Items.Count; i++)
                    {
                        ComboBoxItem item = comboBoxSheng.Items[i] as ComboBoxItem;
                        if (item.Value.Equals(sheng))
                        {
                            comboBoxSheng.SelectedIndex = i;
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

        private void InitTabWebsite()
        {
            try
            {
                InitiWebsite();
                DataTable dtWebiteConfig = _data.GetConfigWebsite();
                if (dtWebiteConfig != null && dtWebiteConfig.Rows.Count > 0)
                {
                    for (int i = 0; i < dtWebiteConfig.Rows.Count; i++)
                    {
                        _iWebsite[i].WebsiteName = dtWebiteConfig.Rows[i]["Name"].ToString();
                        _iWebsite[i].WebsiteUrl = dtWebiteConfig.Rows[i]["URL"].ToString();
                        _iWebsite[i].Visible = true;
                        int_WebsiteCount++;
                    }
                    if (int_WebsiteCount < 16)
                    {
                        CreateAddWebsiteButton(_iWebsite[int_WebsiteCount].Location.X, _iWebsite[int_WebsiteCount].Location.Y);
                    }
                }
                else
                {
                    CreateAddWebsiteButton(_iWebsite[0].Location.X, _iWebsite[0].Location.Y);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void InitiWebsite()
        {
            try
            {
                for (int i = 0; i < 16; i++)
                {
                    foreach (Control con in panelWebsite.Controls)
                    {
                        iWebsite website = con as iWebsite;
                        if (website.TabIndex == (i+1))
                        {
                            website.DeleteWebsiteHandlor +=new iWebsite.DeleteWebsiteHandler(DeleteWebsite);
                            _iWebsite[i] = website;
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

        private void CreateAddWebsiteButton(int x, int y)
        {
            try
            {
                if (panelWebsite.Controls.ContainsKey("BtnAddWebsite"))
                {
                    panelWebsite.Controls.RemoveByKey("BtnAddWebsite");
                }
                Button btn = new Button();
                btn.Name = "BtnAddWebsite";
                btn.BackColor = Color.Transparent;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Font = new Font("宋体", 13, FontStyle.Bold);
                btn.Size = new Size(24, 24);
                btn.Text = "+";
                btn.TabIndex = 0;
                btn.TabStop = false;
                btn.Location = new Point(x, y);
                btn.Click += new EventHandler(BtnAddWebsite_Click);
                btn.MouseEnter += new EventHandler(Btn_MouseEnter);
                btn.MouseLeave += new EventHandler(Btn_MouseLeave);
                
                panelWebsite.Controls.Add(btn);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (bool_IsStarting) return;
                string strName = Application.ExecutablePath;
                string strnewName = strName.Substring(strName.LastIndexOf(@"\") + 1);
                if (AutoStart.Checked)
                {
                    if (!File.Exists(strName))//指定文件是否存在  
                        return;
                    Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (Rkey == null)
                        Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    Rkey.SetValue(strnewName, strName);//修改注册表，使程序开机时自动执行。            
                }
                else
                {
                    Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    Rkey.DeleteValue(strnewName, false);
                }
                BtnApply.Enabled = true;
            }
            catch
            {
                MessageBox.Show("系统管理权限不足，请先退出软件，右键单击以“管理员身份运行”，然后再进行设置");
                AutoStart.CheckState = CheckState.Unchecked;
            }
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form_Config != null)
            {
                form_Config.Dispose();
                form_Config = null;
            }
        }

        private void Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton Rbtn = sender as RadioButton;
                if (Rbtn.Checked)
                {
                    Rbtn.ForeColor = Color.Red;
                }
                else
                {
                    Rbtn.ForeColor = Color.Black;
                }
                if (bool_IsStarting) return;
                BtnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            SaveConfig();
            BtnApply.Enabled = false;
        }

        private void SaveConfig()
        {
            #region -IsAutoStart-
            try
            {
                if (AutoStart.Checked) _data.UpdateConfig("IsAutoStart", "true");
                else _data.UpdateConfig("IsAutoStart", "false");
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            #endregion

            #region -DefaultSearch-
            try
            {
                if (RbtnBaidu.Checked) _data.UpdateConfig("DefaultSearch", "1");
                else if (RbtnGoogle.Checked) _data.UpdateConfig("DefaultSearch", "2");
                else if (RbtnYahoo.Checked) _data.UpdateConfig("DefaultSearch", "3");
                else if (RbtnSoso.Checked) _data.UpdateConfig("DefaultSearch", "4");
                else if (RbtnBing.Checked) _data.UpdateConfig("DefaultSearch", "5");
                else if (Rbtn360.Checked) _data.UpdateConfig("DefaultSearch", "6");

            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            #endregion

            #region -Website-
            try
            {
                _data.ClearConfigWebsite();

                if (int_WebsiteCount > 0)
                {
                    int k = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        if (string.IsNullOrEmpty(_iWebsite[i].WebsiteUrl)) continue;
                        else
                        {
                            _data.AddConfigWebsite(k.ToString(), _iWebsite[i].WebsiteName, _iWebsite[i].WebsiteUrl);
                            k++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            #endregion

            #region -CityCode-
            try
            {
                if (comboBoxXian.SelectedItem != null)
                {
                    ComboBoxItem xian = (ComboBoxItem)comboBoxXian.SelectedItem;
                    _data.UpdateConfig("CityCode", xian.Value);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            #endregion

            ReloadConfigHandlor();
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Button Btn = sender as Button;
                Btn.BackColor = Color.DodgerBlue;
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
                Button Btn = sender as Button;
                Btn.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void BtnAddWebsite_Click(object sender, EventArgs e)
        {
            try
            {
                form_AddWebsite = AddWebsiteForm.GetInstance();
                form_AddWebsite.AddWebsiteHandlor +=new AddWebsiteForm.AddWebsiteHandler(AddWebsite);
                form_AddWebsite.ShowDialog();
                form_AddWebsite.Activate();
                BtnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void AddWebsite(string name, string url)
        {
            try
            {
                for (int i = 0; i < 16; i++)
                {
                    if (!_iWebsite[i].Visible)
                    {
                        _iWebsite[i].WebsiteName = name;
                        _iWebsite[i].WebsiteUrl = url;
                        _iWebsite[i].Visible = true;
                        int_WebsiteCount++;
                        break;
                    }
                }
                
                if (int_WebsiteCount < 16)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        if (!_iWebsite[k].Visible)
                        {
                            int x = _iWebsite[k].Location.X;
                            int y = _iWebsite[k].Location.Y;
                            CreateAddWebsiteButton(x, y);
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

        private void DeleteWebsite(int tabIndex)
        {
            try
            {
                _iWebsite[tabIndex - 1].WebsiteName = "";
                _iWebsite[tabIndex - 1].WebsiteUrl = "";
                _iWebsite[tabIndex - 1].Visible = false;
                if (int_WebsiteCount > 0) int_WebsiteCount--;
                for (int i = 0; i < 16; i++)
                {
                    if (!_iWebsite[i].Visible)
                    {
                        int x = _iWebsite[i].Location.X;
                        int y = _iWebsite[i].Location.Y;
                        CreateAddWebsiteButton(x, y);
                        break;
                    }
                }
                BtnApply.Enabled = true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void comboBoxSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSheng.SelectedItem != null)
                {
                    ComboBoxItem sheng = (ComboBoxItem)comboBoxSheng.SelectedItem;
                    DataTable table_CityShi = _data.GetCityData(ConfigData.CityType.Shi, sheng.Value);
                    if (table_CityShi != null && table_CityShi.Rows.Count > 0)
                    {
                        comboBoxShi.Items.Clear();
                        foreach (DataRow dr in table_CityShi.Rows)
                        {
                            string shi = dr["Name"].ToString();
                            comboBoxShi.Items.Add(new ComboBoxItem(shi, shi));
                        }
                    }
                }
                if (bool_IsStarting)
                {
                    for (int i = 0; i < comboBoxShi.Items.Count; i++)
                    {
                        ComboBoxItem item = comboBoxShi.Items[i] as ComboBoxItem;
                        if (item.Value.Equals(str_SHI))
                        {
                            comboBoxShi.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    BtnApply.Enabled = true;
                    comboBoxShi.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        private void comboBoxShi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxShi.SelectedItem != null)
                {
                    ComboBoxItem shi = (ComboBoxItem)comboBoxShi.SelectedItem;
                    DataTable table_CityXian = _data.GetCityData(ConfigData.CityType.Xian, shi.Value);
                    if (table_CityXian != null && table_CityXian.Rows.Count > 0)
                    {
                        comboBoxXian.Items.Clear();
                        foreach (DataRow dr in table_CityXian.Rows)
                        {
                            comboBoxXian.Items.Add(new ComboBoxItem(dr["Name"].ToString(), dr["Code"].ToString()));
                        }
                    }
                }
                if (bool_IsStarting)
                {
                    for (int i = 0; i < comboBoxXian.Items.Count; i++)
                    {
                        ComboBoxItem item = comboBoxXian.Items[i] as ComboBoxItem;
                        if (item.Value.Equals(str_XIAN))
                        {
                            comboBoxXian.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    BtnApply.Enabled = true;
                    comboBoxXian.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
    }
}
