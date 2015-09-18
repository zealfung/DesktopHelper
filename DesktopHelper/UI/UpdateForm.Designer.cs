namespace DesktopHelper.UI
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.BtnClose = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.labelTip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(238, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(20, 20);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "×";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "安虎桌面助手更新";
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnOK.ForeColor = System.Drawing.Color.White;
            this.BtnOK.Location = new System.Drawing.Point(90, 98);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(90, 39);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "立即升级";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.BtnOK.MouseEnter += new System.EventHandler(this.BtnOK_MouseEnter);
            this.BtnOK.MouseLeave += new System.EventHandler(this.BtnOK_MouseLeave);
            // 
            // labelTip
            // 
            this.labelTip.BackColor = System.Drawing.Color.Transparent;
            this.labelTip.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTip.Location = new System.Drawing.Point(36, 68);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(198, 15);
            this.labelTip.TabIndex = 3;
            this.labelTip.Text = "发现新版本";
            this.labelTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(271, 205);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateForm";
            this.ShowInTaskbar = false;
            this.Text = "UpdateForm";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BtnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label labelTip;
    }
}