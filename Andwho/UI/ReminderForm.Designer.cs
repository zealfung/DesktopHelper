namespace Andwho.UI
{
    partial class ReminderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReminderForm));
            this.BtnClose = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.BtnGotit = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnClose.Location = new System.Drawing.Point(241, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(19, 19);
            this.BtnClose.TabIndex = 0;
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
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "提醒事件";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoEllipsis = true;
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInfo.Location = new System.Drawing.Point(12, 42);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(247, 123);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "message";
            // 
            // BtnGotit
            // 
            this.BtnGotit.BackColor = System.Drawing.Color.Transparent;
            this.BtnGotit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnGotit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnGotit.Location = new System.Drawing.Point(106, 174);
            this.BtnGotit.Name = "BtnGotit";
            this.BtnGotit.Size = new System.Drawing.Size(58, 25);
            this.BtnGotit.TabIndex = 2;
            this.BtnGotit.Text = "知道了";
            this.BtnGotit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnGotit.Click += new System.EventHandler(this.BtnGotit_Click);
            this.BtnGotit.MouseEnter += new System.EventHandler(this.BtnGotit_MouseEnter);
            this.BtnGotit.MouseLeave += new System.EventHandler(this.BtnGotit_MouseLeave);
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(22, 174);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(75, 23);
            this.axWindowsMediaPlayer.TabIndex = 3;
            this.axWindowsMediaPlayer.Visible = false;
            // 
            // ReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(271, 205);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Controls.Add(this.BtnGotit);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReminderForm";
            this.ShowInTaskbar = false;
            this.Text = "ReminderForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ReminderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BtnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label BtnGotit;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
    }
}