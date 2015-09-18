namespace DesktopHelper.UI
{
    partial class iReminder
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.labelBeginTime = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoEllipsis = true;
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Location = new System.Drawing.Point(3, 72);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(173, 28);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "1";
            this.labelInfo.MouseEnter += new System.EventHandler(this.iReminder_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(2, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "提醒信息：";
            this.label1.MouseEnter += new System.EventHandler(this.iReminder_MouseEnter);
            // 
            // label0
            // 
            this.label0.AutoEllipsis = true;
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(3, 9);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(65, 12);
            this.label0.TabIndex = 0;
            this.label0.Text = "开始时间：";
            this.label0.MouseEnter += new System.EventHandler(this.iReminder_MouseEnter);
            // 
            // labelBeginTime
            // 
            this.labelBeginTime.AutoEllipsis = true;
            this.labelBeginTime.AutoSize = true;
            this.labelBeginTime.Location = new System.Drawing.Point(3, 31);
            this.labelBeginTime.Name = "labelBeginTime";
            this.labelBeginTime.Size = new System.Drawing.Size(131, 12);
            this.labelBeginTime.TabIndex = 0;
            this.labelBeginTime.Text = "2013-12-19 12：12：12";
            this.labelBeginTime.MouseEnter += new System.EventHandler(this.iReminder_MouseEnter);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnClose.Location = new System.Drawing.Point(157, 6);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(19, 19);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.Text = "×";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // iReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.labelBeginTime);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label0);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "iReminder";
            this.Size = new System.Drawing.Size(193, 103);
            this.MouseEnter += new System.EventHandler(this.iReminder_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.iReminder_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label labelBeginTime;
        private System.Windows.Forms.Label BtnClose;
    }
}
