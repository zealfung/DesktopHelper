namespace DesktopHelper.UI
{
    partial class iTimer
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
            this.BtnClose = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelEvent = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelFilepath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnClose.Location = new System.Drawing.Point(219, 5);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(19, 19);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "×";
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // labelTime
            // 
            this.labelTime.AutoEllipsis = true;
            this.labelTime.Location = new System.Drawing.Point(13, 37);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(220, 12);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "执行时间：12：12：12";
            this.labelTime.MouseEnter += new System.EventHandler(this.iTimer_MouseEnter);
            // 
            // labelEvent
            // 
            this.labelEvent.AutoEllipsis = true;
            this.labelEvent.BackColor = System.Drawing.Color.Transparent;
            this.labelEvent.Location = new System.Drawing.Point(13, 62);
            this.labelEvent.Name = "labelEvent";
            this.labelEvent.Size = new System.Drawing.Size(220, 12);
            this.labelEvent.TabIndex = 0;
            this.labelEvent.Text = "执行事件：打开软件";
            this.labelEvent.MouseEnter += new System.EventHandler(this.iTimer_MouseEnter);
            // 
            // labelFrequency
            // 
            this.labelFrequency.AutoEllipsis = true;
            this.labelFrequency.Location = new System.Drawing.Point(13, 12);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(180, 12);
            this.labelFrequency.TabIndex = 0;
            this.labelFrequency.Text = "执行频率：每天";
            this.labelFrequency.MouseEnter += new System.EventHandler(this.iTimer_MouseEnter);
            // 
            // labelFilepath
            // 
            this.labelFilepath.AutoEllipsis = true;
            this.labelFilepath.Location = new System.Drawing.Point(13, 87);
            this.labelFilepath.Name = "labelFilepath";
            this.labelFilepath.Size = new System.Drawing.Size(220, 12);
            this.labelFilepath.TabIndex = 0;
            this.labelFilepath.Visible = false;
            this.labelFilepath.MouseEnter += new System.EventHandler(this.iTimer_MouseEnter);
            // 
            // iTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelFilepath);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelEvent);
            this.Controls.Add(this.labelFrequency);
            this.Name = "iTimer";
            this.Size = new System.Drawing.Size(243, 108);
            this.MouseEnter += new System.EventHandler(this.iTimer_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.iTimer_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label BtnClose;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelEvent;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelFilepath;
    }
}
