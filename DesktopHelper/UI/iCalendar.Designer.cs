namespace DesktopHelper.UI
{
    partial class iCalendar
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
            this.labelWeek = new System.Windows.Forms.Label();
            this.labelJieQi = new System.Windows.Forms.Label();
            this.labelDayNong = new System.Windows.Forms.Label();
            this.labelDayYang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelWeek
            // 
            this.labelWeek.AutoSize = true;
            this.labelWeek.Location = new System.Drawing.Point(54, 30);
            this.labelWeek.Name = "labelWeek";
            this.labelWeek.Size = new System.Drawing.Size(41, 12);
            this.labelWeek.TabIndex = 3;
            this.labelWeek.Text = "星期日";
            // 
            // labelJieQi
            // 
            this.labelJieQi.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelJieQi.Location = new System.Drawing.Point(9, 76);
            this.labelJieQi.Name = "labelJieQi";
            this.labelJieQi.Size = new System.Drawing.Size(131, 66);
            this.labelJieQi.TabIndex = 4;
            this.labelJieQi.Text = "雨水 邓小平逝世纪念日";
            // 
            // labelDayNong
            // 
            this.labelDayNong.AutoSize = true;
            this.labelDayNong.Location = new System.Drawing.Point(21, 53);
            this.labelDayNong.Name = "labelDayNong";
            this.labelDayNong.Size = new System.Drawing.Size(107, 12);
            this.labelDayNong.TabIndex = 1;
            this.labelDayNong.Text = "农历马年 正月十七";
            // 
            // labelDayYang
            // 
            this.labelDayYang.AutoSize = true;
            this.labelDayYang.Location = new System.Drawing.Point(33, 7);
            this.labelDayYang.Name = "labelDayYang";
            this.labelDayYang.Size = new System.Drawing.Size(83, 12);
            this.labelDayYang.TabIndex = 2;
            this.labelDayYang.Text = "2014年2月16日";
            // 
            // iCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 148);
            this.Controls.Add(this.labelWeek);
            this.Controls.Add(this.labelJieQi);
            this.Controls.Add(this.labelDayNong);
            this.Controls.Add(this.labelDayYang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "iCalendar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "iCalendarForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWeek;
        private System.Windows.Forms.Label labelJieQi;
        private System.Windows.Forms.Label labelDayNong;
        private System.Windows.Forms.Label labelDayYang;
    }
}