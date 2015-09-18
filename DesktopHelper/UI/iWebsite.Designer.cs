namespace DesktopHelper.UI
{
    partial class iWebsite
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
            this.Website = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Website
            // 
            this.Website.AutoEllipsis = true;
            this.Website.BackColor = System.Drawing.Color.DodgerBlue;
            this.Website.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Website.ForeColor = System.Drawing.Color.White;
            this.Website.Location = new System.Drawing.Point(3, 2);
            this.Website.Name = "Website";
            this.Website.Size = new System.Drawing.Size(77, 19);
            this.Website.TabIndex = 0;
            this.Website.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Website.MouseEnter += new System.EventHandler(this.iWebsite_MouseEnter);
            this.Website.MouseLeave += new System.EventHandler(this.iWebsite_MouseLeave);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnDelete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnDelete.ForeColor = System.Drawing.Color.White;
            this.BtnDelete.Location = new System.Drawing.Point(82, 2);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(19, 19);
            this.BtnDelete.TabIndex = 1;
            this.BtnDelete.Text = "×";
            this.BtnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.BtnDelete.MouseEnter += new System.EventHandler(this.BtnDelete_MouseEnter);
            this.BtnDelete.MouseLeave += new System.EventHandler(this.BtnDelete_MouseLeave);
            // 
            // iWebsite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.Website);
            this.Name = "iWebsite";
            this.Size = new System.Drawing.Size(105, 24);
            this.MouseEnter += new System.EventHandler(this.iWebsite_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.iWebsite_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Website;
        private System.Windows.Forms.Label BtnDelete;
    }
}
