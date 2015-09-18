namespace DesktopHelper.UI
{
    partial class AddWebsiteForm
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
            this.BtnClose = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnClose.Location = new System.Drawing.Point(260, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(19, 19);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "×";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(4, 32);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 12);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "名称：";
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Location = new System.Drawing.Point(4, 70);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(41, 12);
            this.labelURL.TabIndex = 0;
            this.labelURL.Text = "网址：";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(40, 66);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(239, 21);
            this.txtURL.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(40, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(239, 21);
            this.txtName.TabIndex = 0;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.Transparent;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAdd.Location = new System.Drawing.Point(113, 101);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(58, 19);
            this.BtnAdd.TabIndex = 2;
            this.BtnAdd.Text = "添加";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.BtnAdd.MouseEnter += new System.EventHandler(this.BtnAdd_MouseEnter);
            this.BtnAdd.MouseLeave += new System.EventHandler(this.BtnAdd_MouseLeave);
            // 
            // AddWebsiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 127);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.labelURL);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.BtnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddWebsiteForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddWebsiteForm";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddWebsiteForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button BtnAdd;
    }
}