namespace DesktopHelper.UI
{
    partial class ClearSystemGarbageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClearSystemGarbageForm));
            this.BtnStartClear = new System.Windows.Forms.Label();
            this.labelDetail = new System.Windows.Forms.Label();
            this.labelTip = new System.Windows.Forms.Label();
            this.labelTotalSize = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnStartClear
            // 
            this.BtnStartClear.Image = ((System.Drawing.Image)(resources.GetObject("BtnStartClear.Image")));
            this.BtnStartClear.Location = new System.Drawing.Point(26, 53);
            this.BtnStartClear.Name = "BtnStartClear";
            this.BtnStartClear.Size = new System.Drawing.Size(129, 44);
            this.BtnStartClear.TabIndex = 1;
            this.BtnStartClear.Click += new System.EventHandler(this.BtnStartClear_Click);
            // 
            // labelDetail
            // 
            this.labelDetail.AutoEllipsis = true;
            this.labelDetail.BackColor = System.Drawing.Color.Transparent;
            this.labelDetail.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDetail.ForeColor = System.Drawing.Color.Green;
            this.labelDetail.Location = new System.Drawing.Point(25, 177);
            this.labelDetail.Name = "labelDetail";
            this.labelDetail.Size = new System.Drawing.Size(353, 66);
            this.labelDetail.TabIndex = 2;
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.BackColor = System.Drawing.Color.Transparent;
            this.labelTip.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTip.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelTip.Location = new System.Drawing.Point(23, 124);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(285, 22);
            this.labelTip.TabIndex = 3;
            this.labelTip.Text = "刚刚清理的系统垃圾大小为:";
            this.labelTip.Visible = false;
            // 
            // labelTotalSize
            // 
            this.labelTotalSize.AutoSize = true;
            this.labelTotalSize.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalSize.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotalSize.ForeColor = System.Drawing.Color.Green;
            this.labelTotalSize.Location = new System.Drawing.Point(303, 126);
            this.labelTotalSize.Name = "labelTotalSize";
            this.labelTotalSize.Size = new System.Drawing.Size(0, 22);
            this.labelTotalSize.TabIndex = 4;
            // 
            // ClearSystemGarbageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.labelTotalSize);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.labelDetail);
            this.Controls.Add(this.BtnStartClear);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClearSystemGarbageForm";
            this.Padding = new System.Windows.Forms.Padding(3, 26, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安虎桌面助手系统垃圾清理器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClearSystemGarbageForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BtnStartClear;
        private System.Windows.Forms.Label labelDetail;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.Label labelTotalSize;
    }
}