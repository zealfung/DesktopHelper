namespace DesktopHelper.UI
{
    partial class iDay
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
            this.iNew = new System.Windows.Forms.Label();
            this.iOld = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iNew
            // 
            this.iNew.AutoEllipsis = true;
            this.iNew.BackColor = System.Drawing.Color.Transparent;
            this.iNew.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iNew.Location = new System.Drawing.Point(1, 0);
            this.iNew.Name = "iNew";
            this.iNew.Size = new System.Drawing.Size(40, 20);
            this.iNew.TabIndex = 0;
            this.iNew.Text = "31";
            this.iNew.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.iNew.Click += new System.EventHandler(this.Label_Click);
            // 
            // iOld
            // 
            this.iOld.BackColor = System.Drawing.Color.Transparent;
            this.iOld.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iOld.Location = new System.Drawing.Point(1, 20);
            this.iOld.Name = "iOld";
            this.iOld.Size = new System.Drawing.Size(40, 15);
            this.iOld.TabIndex = 0;
            this.iOld.Text = "初一";
            this.iOld.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iOld.Click += new System.EventHandler(this.Label_Click);
            // 
            // iDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.iOld);
            this.Controls.Add(this.iNew);
            this.Name = "iDay";
            this.Size = new System.Drawing.Size(40, 35);
            this.Click += new System.EventHandler(this.iDay_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label iNew;
        private System.Windows.Forms.Label iOld;
    }
}
