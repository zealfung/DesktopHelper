namespace DesktopHelper.UI
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.qqTabControl = new Andwho.Windows.Forms.QQTabControl();
            this.tabCommon = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxXian = new System.Windows.Forms.ComboBox();
            this.comboBoxShi = new System.Windows.Forms.ComboBox();
            this.comboBoxSheng = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RbtnBaidu = new System.Windows.Forms.RadioButton();
            this.RbtnYahoo = new System.Windows.Forms.RadioButton();
            this.Rbtn360 = new System.Windows.Forms.RadioButton();
            this.RbtnGoogle = new System.Windows.Forms.RadioButton();
            this.RbtnBing = new System.Windows.Forms.RadioButton();
            this.RbtnSoso = new System.Windows.Forms.RadioButton();
            this.AutoStart = new System.Windows.Forms.CheckBox();
            this.tabWebsite = new System.Windows.Forms.TabPage();
            this.panelWebsite = new System.Windows.Forms.Panel();
            this.iWebsite16 = new DesktopHelper.UI.iWebsite();
            this.iWebsite1 = new DesktopHelper.UI.iWebsite();
            this.iWebsite12 = new DesktopHelper.UI.iWebsite();
            this.iWebsite5 = new DesktopHelper.UI.iWebsite();
            this.iWebsite8 = new DesktopHelper.UI.iWebsite();
            this.iWebsite9 = new DesktopHelper.UI.iWebsite();
            this.iWebsite4 = new DesktopHelper.UI.iWebsite();
            this.iWebsite13 = new DesktopHelper.UI.iWebsite();
            this.iWebsite15 = new DesktopHelper.UI.iWebsite();
            this.iWebsite2 = new DesktopHelper.UI.iWebsite();
            this.iWebsite11 = new DesktopHelper.UI.iWebsite();
            this.iWebsite6 = new DesktopHelper.UI.iWebsite();
            this.iWebsite7 = new DesktopHelper.UI.iWebsite();
            this.iWebsite10 = new DesktopHelper.UI.iWebsite();
            this.iWebsite3 = new DesktopHelper.UI.iWebsite();
            this.iWebsite14 = new DesktopHelper.UI.iWebsite();
            this.BtnApply = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.qqTabControl.SuspendLayout();
            this.tabCommon.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabWebsite.SuspendLayout();
            this.panelWebsite.SuspendLayout();
            this.SuspendLayout();
            // 
            // qqTabControl
            // 
            this.qqTabControl.BackColor = System.Drawing.Color.Transparent;
            this.qqTabControl.BaseColor = System.Drawing.Color.White;
            this.qqTabControl.BorderColor = System.Drawing.Color.White;
            this.qqTabControl.Controls.Add(this.tabCommon);
            this.qqTabControl.Controls.Add(this.tabWebsite);
            this.qqTabControl.ItemSize = new System.Drawing.Size(80, 32);
            this.qqTabControl.Location = new System.Drawing.Point(2, -3);
            this.qqTabControl.Name = "qqTabControl";
            this.qqTabControl.PageColor = System.Drawing.Color.White;
            this.qqTabControl.SelectedIndex = 0;
            this.qqTabControl.Size = new System.Drawing.Size(487, 355);
            this.qqTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.qqTabControl.TabIndex = 0;
            // 
            // tabCommon
            // 
            this.tabCommon.BackColor = System.Drawing.Color.White;
            this.tabCommon.Controls.Add(this.groupBox2);
            this.tabCommon.Controls.Add(this.groupBox1);
            this.tabCommon.Controls.Add(this.AutoStart);
            this.tabCommon.Location = new System.Drawing.Point(4, 36);
            this.tabCommon.Margin = new System.Windows.Forms.Padding(0);
            this.tabCommon.Name = "tabCommon";
            this.tabCommon.Size = new System.Drawing.Size(479, 315);
            this.tabCommon.TabIndex = 0;
            this.tabCommon.Text = "常规设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxXian);
            this.groupBox2.Controls.Add(this.comboBoxShi);
            this.groupBox2.Controls.Add(this.comboBoxSheng);
            this.groupBox2.Location = new System.Drawing.Point(8, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 63);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "天气城市";
            this.groupBox2.Visible = false;
            // 
            // comboBoxXian
            // 
            this.comboBoxXian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxXian.FormattingEnabled = true;
            this.comboBoxXian.Location = new System.Drawing.Point(268, 25);
            this.comboBoxXian.Name = "comboBoxXian";
            this.comboBoxXian.Size = new System.Drawing.Size(121, 20);
            this.comboBoxXian.TabIndex = 0;
            // 
            // comboBoxShi
            // 
            this.comboBoxShi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShi.FormattingEnabled = true;
            this.comboBoxShi.Location = new System.Drawing.Point(141, 25);
            this.comboBoxShi.Name = "comboBoxShi";
            this.comboBoxShi.Size = new System.Drawing.Size(121, 20);
            this.comboBoxShi.TabIndex = 0;
            this.comboBoxShi.SelectedIndexChanged += new System.EventHandler(this.comboBoxShi_SelectedIndexChanged);
            // 
            // comboBoxSheng
            // 
            this.comboBoxSheng.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSheng.FormattingEnabled = true;
            this.comboBoxSheng.Location = new System.Drawing.Point(14, 25);
            this.comboBoxSheng.Name = "comboBoxSheng";
            this.comboBoxSheng.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSheng.TabIndex = 0;
            this.comboBoxSheng.SelectedIndexChanged += new System.EventHandler(this.comboBoxSheng_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbtnBaidu);
            this.groupBox1.Controls.Add(this.RbtnYahoo);
            this.groupBox1.Controls.Add(this.Rbtn360);
            this.groupBox1.Controls.Add(this.RbtnGoogle);
            this.groupBox1.Controls.Add(this.RbtnBing);
            this.groupBox1.Controls.Add(this.RbtnSoso);
            this.groupBox1.Location = new System.Drawing.Point(8, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "默认搜索";
            // 
            // RbtnBaidu
            // 
            this.RbtnBaidu.AutoSize = true;
            this.RbtnBaidu.Checked = true;
            this.RbtnBaidu.ForeColor = System.Drawing.Color.Red;
            this.RbtnBaidu.Location = new System.Drawing.Point(14, 23);
            this.RbtnBaidu.Name = "RbtnBaidu";
            this.RbtnBaidu.Size = new System.Drawing.Size(47, 16);
            this.RbtnBaidu.TabIndex = 1;
            this.RbtnBaidu.TabStop = true;
            this.RbtnBaidu.Text = "百度";
            this.RbtnBaidu.UseVisualStyleBackColor = true;
            this.RbtnBaidu.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // RbtnYahoo
            // 
            this.RbtnYahoo.AutoSize = true;
            this.RbtnYahoo.Location = new System.Drawing.Point(174, 23);
            this.RbtnYahoo.Name = "RbtnYahoo";
            this.RbtnYahoo.Size = new System.Drawing.Size(47, 16);
            this.RbtnYahoo.TabIndex = 1;
            this.RbtnYahoo.TabStop = true;
            this.RbtnYahoo.Text = "雅虎";
            this.RbtnYahoo.UseVisualStyleBackColor = true;
            this.RbtnYahoo.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // Rbtn360
            // 
            this.Rbtn360.AutoSize = true;
            this.Rbtn360.Location = new System.Drawing.Point(414, 23);
            this.Rbtn360.Name = "Rbtn360";
            this.Rbtn360.Size = new System.Drawing.Size(41, 16);
            this.Rbtn360.TabIndex = 1;
            this.Rbtn360.TabStop = true;
            this.Rbtn360.Text = "360";
            this.Rbtn360.UseVisualStyleBackColor = true;
            this.Rbtn360.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // RbtnGoogle
            // 
            this.RbtnGoogle.AutoSize = true;
            this.RbtnGoogle.Location = new System.Drawing.Point(94, 23);
            this.RbtnGoogle.Name = "RbtnGoogle";
            this.RbtnGoogle.Size = new System.Drawing.Size(47, 16);
            this.RbtnGoogle.TabIndex = 1;
            this.RbtnGoogle.TabStop = true;
            this.RbtnGoogle.Text = "谷歌";
            this.RbtnGoogle.UseVisualStyleBackColor = true;
            this.RbtnGoogle.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // RbtnBing
            // 
            this.RbtnBing.AutoSize = true;
            this.RbtnBing.Location = new System.Drawing.Point(334, 23);
            this.RbtnBing.Name = "RbtnBing";
            this.RbtnBing.Size = new System.Drawing.Size(47, 16);
            this.RbtnBing.TabIndex = 1;
            this.RbtnBing.TabStop = true;
            this.RbtnBing.Text = "必应";
            this.RbtnBing.UseVisualStyleBackColor = true;
            this.RbtnBing.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // RbtnSoso
            // 
            this.RbtnSoso.AutoSize = true;
            this.RbtnSoso.Location = new System.Drawing.Point(254, 23);
            this.RbtnSoso.Name = "RbtnSoso";
            this.RbtnSoso.Size = new System.Drawing.Size(47, 16);
            this.RbtnSoso.TabIndex = 1;
            this.RbtnSoso.TabStop = true;
            this.RbtnSoso.Text = "SOSO";
            this.RbtnSoso.UseVisualStyleBackColor = true;
            this.RbtnSoso.CheckedChanged += new System.EventHandler(this.Rbtn_CheckedChanged);
            // 
            // AutoStart
            // 
            this.AutoStart.AutoSize = true;
            this.AutoStart.Location = new System.Drawing.Point(20, 17);
            this.AutoStart.Name = "AutoStart";
            this.AutoStart.Size = new System.Drawing.Size(120, 16);
            this.AutoStart.TabIndex = 0;
            this.AutoStart.Text = "桌面助手开机启动";
            this.AutoStart.UseVisualStyleBackColor = true;
            this.AutoStart.CheckedChanged += new System.EventHandler(this.AutoStart_CheckedChanged);
            // 
            // tabWebsite
            // 
            this.tabWebsite.BackColor = System.Drawing.Color.White;
            this.tabWebsite.Controls.Add(this.panelWebsite);
            this.tabWebsite.Location = new System.Drawing.Point(4, 36);
            this.tabWebsite.Name = "tabWebsite";
            this.tabWebsite.Padding = new System.Windows.Forms.Padding(3);
            this.tabWebsite.Size = new System.Drawing.Size(479, 315);
            this.tabWebsite.TabIndex = 1;
            this.tabWebsite.Text = "常用网址";
            // 
            // panelWebsite
            // 
            this.panelWebsite.BackColor = System.Drawing.Color.Transparent;
            this.panelWebsite.Controls.Add(this.iWebsite16);
            this.panelWebsite.Controls.Add(this.iWebsite1);
            this.panelWebsite.Controls.Add(this.iWebsite12);
            this.panelWebsite.Controls.Add(this.iWebsite5);
            this.panelWebsite.Controls.Add(this.iWebsite8);
            this.panelWebsite.Controls.Add(this.iWebsite9);
            this.panelWebsite.Controls.Add(this.iWebsite4);
            this.panelWebsite.Controls.Add(this.iWebsite13);
            this.panelWebsite.Controls.Add(this.iWebsite15);
            this.panelWebsite.Controls.Add(this.iWebsite2);
            this.panelWebsite.Controls.Add(this.iWebsite11);
            this.panelWebsite.Controls.Add(this.iWebsite6);
            this.panelWebsite.Controls.Add(this.iWebsite7);
            this.panelWebsite.Controls.Add(this.iWebsite10);
            this.panelWebsite.Controls.Add(this.iWebsite3);
            this.panelWebsite.Controls.Add(this.iWebsite14);
            this.panelWebsite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWebsite.Location = new System.Drawing.Point(3, 3);
            this.panelWebsite.Name = "panelWebsite";
            this.panelWebsite.Size = new System.Drawing.Size(473, 309);
            this.panelWebsite.TabIndex = 17;
            // 
            // iWebsite16
            // 
            this.iWebsite16.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite16.Location = new System.Drawing.Point(362, 255);
            this.iWebsite16.Name = "iWebsite16";
            this.iWebsite16.Size = new System.Drawing.Size(105, 24);
            this.iWebsite16.TabIndex = 16;
            this.iWebsite16.TabStop = false;
            this.iWebsite16.Visible = false;
            this.iWebsite16.WebsiteName = "16";
            this.iWebsite16.WebsiteUrl = null;
            // 
            // iWebsite1
            // 
            this.iWebsite1.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite1.Location = new System.Drawing.Point(11, 43);
            this.iWebsite1.Name = "iWebsite1";
            this.iWebsite1.Size = new System.Drawing.Size(105, 24);
            this.iWebsite1.TabIndex = 1;
            this.iWebsite1.TabStop = false;
            this.iWebsite1.Visible = false;
            this.iWebsite1.WebsiteName = "1";
            this.iWebsite1.WebsiteUrl = null;
            // 
            // iWebsite12
            // 
            this.iWebsite12.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite12.Location = new System.Drawing.Point(362, 184);
            this.iWebsite12.Name = "iWebsite12";
            this.iWebsite12.Size = new System.Drawing.Size(105, 24);
            this.iWebsite12.TabIndex = 12;
            this.iWebsite12.TabStop = false;
            this.iWebsite12.Visible = false;
            this.iWebsite12.WebsiteName = "12";
            this.iWebsite12.WebsiteUrl = null;
            // 
            // iWebsite5
            // 
            this.iWebsite5.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite5.Location = new System.Drawing.Point(11, 114);
            this.iWebsite5.Name = "iWebsite5";
            this.iWebsite5.Size = new System.Drawing.Size(105, 24);
            this.iWebsite5.TabIndex = 5;
            this.iWebsite5.TabStop = false;
            this.iWebsite5.Visible = false;
            this.iWebsite5.WebsiteName = "5";
            this.iWebsite5.WebsiteUrl = null;
            // 
            // iWebsite8
            // 
            this.iWebsite8.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite8.Location = new System.Drawing.Point(362, 113);
            this.iWebsite8.Name = "iWebsite8";
            this.iWebsite8.Size = new System.Drawing.Size(105, 24);
            this.iWebsite8.TabIndex = 8;
            this.iWebsite8.TabStop = false;
            this.iWebsite8.Visible = false;
            this.iWebsite8.WebsiteName = "8";
            this.iWebsite8.WebsiteUrl = null;
            // 
            // iWebsite9
            // 
            this.iWebsite9.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite9.Location = new System.Drawing.Point(11, 185);
            this.iWebsite9.Name = "iWebsite9";
            this.iWebsite9.Size = new System.Drawing.Size(105, 24);
            this.iWebsite9.TabIndex = 9;
            this.iWebsite9.TabStop = false;
            this.iWebsite9.Visible = false;
            this.iWebsite9.WebsiteName = "9";
            this.iWebsite9.WebsiteUrl = null;
            // 
            // iWebsite4
            // 
            this.iWebsite4.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite4.Location = new System.Drawing.Point(362, 42);
            this.iWebsite4.Name = "iWebsite4";
            this.iWebsite4.Size = new System.Drawing.Size(105, 24);
            this.iWebsite4.TabIndex = 4;
            this.iWebsite4.TabStop = false;
            this.iWebsite4.Visible = false;
            this.iWebsite4.WebsiteName = "4";
            this.iWebsite4.WebsiteUrl = null;
            // 
            // iWebsite13
            // 
            this.iWebsite13.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite13.Location = new System.Drawing.Point(11, 256);
            this.iWebsite13.Name = "iWebsite13";
            this.iWebsite13.Size = new System.Drawing.Size(105, 24);
            this.iWebsite13.TabIndex = 13;
            this.iWebsite13.TabStop = false;
            this.iWebsite13.Visible = false;
            this.iWebsite13.WebsiteName = "13";
            this.iWebsite13.WebsiteUrl = null;
            // 
            // iWebsite15
            // 
            this.iWebsite15.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite15.Location = new System.Drawing.Point(245, 256);
            this.iWebsite15.Name = "iWebsite15";
            this.iWebsite15.Size = new System.Drawing.Size(105, 24);
            this.iWebsite15.TabIndex = 15;
            this.iWebsite15.TabStop = false;
            this.iWebsite15.Visible = false;
            this.iWebsite15.WebsiteName = "15";
            this.iWebsite15.WebsiteUrl = null;
            // 
            // iWebsite2
            // 
            this.iWebsite2.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite2.Location = new System.Drawing.Point(128, 43);
            this.iWebsite2.Name = "iWebsite2";
            this.iWebsite2.Size = new System.Drawing.Size(105, 24);
            this.iWebsite2.TabIndex = 2;
            this.iWebsite2.TabStop = false;
            this.iWebsite2.Visible = false;
            this.iWebsite2.WebsiteName = "2";
            this.iWebsite2.WebsiteUrl = null;
            // 
            // iWebsite11
            // 
            this.iWebsite11.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite11.Location = new System.Drawing.Point(245, 185);
            this.iWebsite11.Name = "iWebsite11";
            this.iWebsite11.Size = new System.Drawing.Size(105, 24);
            this.iWebsite11.TabIndex = 11;
            this.iWebsite11.TabStop = false;
            this.iWebsite11.Visible = false;
            this.iWebsite11.WebsiteName = "11";
            this.iWebsite11.WebsiteUrl = null;
            // 
            // iWebsite6
            // 
            this.iWebsite6.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite6.Location = new System.Drawing.Point(128, 114);
            this.iWebsite6.Name = "iWebsite6";
            this.iWebsite6.Size = new System.Drawing.Size(105, 24);
            this.iWebsite6.TabIndex = 6;
            this.iWebsite6.TabStop = false;
            this.iWebsite6.Visible = false;
            this.iWebsite6.WebsiteName = "6";
            this.iWebsite6.WebsiteUrl = null;
            // 
            // iWebsite7
            // 
            this.iWebsite7.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite7.Location = new System.Drawing.Point(245, 114);
            this.iWebsite7.Name = "iWebsite7";
            this.iWebsite7.Size = new System.Drawing.Size(105, 24);
            this.iWebsite7.TabIndex = 7;
            this.iWebsite7.TabStop = false;
            this.iWebsite7.Visible = false;
            this.iWebsite7.WebsiteName = "7";
            this.iWebsite7.WebsiteUrl = null;
            // 
            // iWebsite10
            // 
            this.iWebsite10.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite10.Location = new System.Drawing.Point(128, 185);
            this.iWebsite10.Name = "iWebsite10";
            this.iWebsite10.Size = new System.Drawing.Size(105, 24);
            this.iWebsite10.TabIndex = 10;
            this.iWebsite10.TabStop = false;
            this.iWebsite10.Visible = false;
            this.iWebsite10.WebsiteName = "10";
            this.iWebsite10.WebsiteUrl = null;
            // 
            // iWebsite3
            // 
            this.iWebsite3.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite3.Location = new System.Drawing.Point(245, 43);
            this.iWebsite3.Name = "iWebsite3";
            this.iWebsite3.Size = new System.Drawing.Size(105, 24);
            this.iWebsite3.TabIndex = 3;
            this.iWebsite3.TabStop = false;
            this.iWebsite3.Visible = false;
            this.iWebsite3.WebsiteName = "3";
            this.iWebsite3.WebsiteUrl = null;
            // 
            // iWebsite14
            // 
            this.iWebsite14.BackColor = System.Drawing.Color.DodgerBlue;
            this.iWebsite14.Location = new System.Drawing.Point(128, 256);
            this.iWebsite14.Name = "iWebsite14";
            this.iWebsite14.Size = new System.Drawing.Size(105, 24);
            this.iWebsite14.TabIndex = 14;
            this.iWebsite14.TabStop = false;
            this.iWebsite14.Visible = false;
            this.iWebsite14.WebsiteName = "14";
            this.iWebsite14.WebsiteUrl = null;
            // 
            // BtnApply
            // 
            this.BtnApply.Enabled = false;
            this.BtnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnApply.Location = new System.Drawing.Point(378, 362);
            this.BtnApply.Name = "BtnApply";
            this.BtnApply.Size = new System.Drawing.Size(75, 23);
            this.BtnApply.TabIndex = 1;
            this.BtnApply.Text = "应用";
            this.BtnApply.UseVisualStyleBackColor = true;
            this.BtnApply.Click += new System.EventHandler(this.BtnApply_Click);
            this.BtnApply.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.BtnApply.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // BtnCancel
            // 
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnCancel.Location = new System.Drawing.Point(294, 362);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            this.BtnCancel.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.BtnCancel.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // BtnOK
            // 
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnOK.Location = new System.Drawing.Point(210, 362);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 1;
            this.BtnOK.Text = "确定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.BtnOK.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.BtnOK.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 391);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnApply);
            this.Controls.Add(this.qqTabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安虎桌面助手设置";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.qqTabControl.ResumeLayout(false);
            this.tabCommon.ResumeLayout(false);
            this.tabCommon.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabWebsite.ResumeLayout(false);
            this.panelWebsite.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Andwho.Windows.Forms.QQTabControl qqTabControl;
        private System.Windows.Forms.TabPage tabCommon;
        private System.Windows.Forms.TabPage tabWebsite;
        private System.Windows.Forms.CheckBox AutoStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RbtnBaidu;
        private System.Windows.Forms.RadioButton Rbtn360;
        private System.Windows.Forms.RadioButton RbtnGoogle;
        private System.Windows.Forms.RadioButton RbtnBing;
        private System.Windows.Forms.RadioButton RbtnSoso;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private iWebsite iWebsite1;
        private iWebsite iWebsite2;
        private iWebsite iWebsite16;
        private iWebsite iWebsite12;
        private iWebsite iWebsite8;
        private iWebsite iWebsite4;
        private iWebsite iWebsite15;
        private iWebsite iWebsite11;
        private iWebsite iWebsite7;
        private iWebsite iWebsite3;
        private iWebsite iWebsite14;
        private iWebsite iWebsite10;
        private iWebsite iWebsite6;
        private iWebsite iWebsite13;
        private iWebsite iWebsite9;
        private iWebsite iWebsite5;
        private System.Windows.Forms.Panel panelWebsite;
        private System.Windows.Forms.RadioButton RbtnYahoo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxXian;
        private System.Windows.Forms.ComboBox comboBoxShi;
        private System.Windows.Forms.ComboBox comboBoxSheng;
    }
}