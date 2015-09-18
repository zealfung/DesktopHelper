namespace DesktopHelper.UI
{
    partial class iNotepad
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
            this.BtnDelete = new System.Windows.Forms.Label();
            this.BtnEdit = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnDelete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnDelete.Location = new System.Drawing.Point(218, 3);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(19, 19);
            this.BtnDelete.TabIndex = 5;
            this.BtnDelete.Text = "×";
            this.BtnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.BtnDelete.MouseEnter += new System.EventHandler(this.BtnDelete_MouseEnter);
            this.BtnDelete.MouseLeave += new System.EventHandler(this.BtnDelete_MouseLeave);
            // 
            // BtnEdit
            // 
            this.BtnEdit.BackColor = System.Drawing.Color.Transparent;
            this.BtnEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnEdit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnEdit.Location = new System.Drawing.Point(218, 29);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(19, 19);
            this.BtnEdit.TabIndex = 5;
            this.BtnEdit.Text = "三";
            this.BtnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            this.BtnEdit.MouseEnter += new System.EventHandler(this.BtnEdit_MouseEnter);
            this.BtnEdit.MouseLeave += new System.EventHandler(this.BtnEdit_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoEllipsis = true;
            this.labelTitle.Location = new System.Drawing.Point(4, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(208, 39);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "长标题测试";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.MouseEnter += new System.EventHandler(this.iNotepad_MouseEnter);
            // 
            // iNotepad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnDelete);
            this.Name = "iNotepad";
            this.Size = new System.Drawing.Size(243, 52);
            this.MouseEnter += new System.EventHandler(this.iNotepad_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.iNotepad_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label BtnDelete;
        private System.Windows.Forms.Label BtnEdit;
        private System.Windows.Forms.Label labelTitle;
    }
}
