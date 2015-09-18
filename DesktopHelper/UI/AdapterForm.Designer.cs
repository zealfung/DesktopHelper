namespace DesktopHelper.UI
{
    partial class AdapterForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdapterForm));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearGarbageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AndwhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelPhysicalMemory = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem,
            this.ClearGarbageToolStripMenuItem,
            this.ClearMemoryToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.UpdateToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 136);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ConfigToolStripMenuItem.Text = "设置";
            this.ConfigToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // ClearGarbageToolStripMenuItem
            // 
            this.ClearGarbageToolStripMenuItem.Name = "ClearGarbageToolStripMenuItem";
            this.ClearGarbageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ClearGarbageToolStripMenuItem.Text = "清理系统垃圾";
            this.ClearGarbageToolStripMenuItem.Click += new System.EventHandler(this.ClearGarbageToolStripMenuItem_Click);
            // 
            // ClearMemoryToolStripMenuItem
            // 
            this.ClearMemoryToolStripMenuItem.Name = "ClearMemoryToolStripMenuItem";
            this.ClearMemoryToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ClearMemoryToolStripMenuItem.Text = "清理内存";
            this.ClearMemoryToolStripMenuItem.Click += new System.EventHandler(this.ClearMemoryToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AndwhoToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // AndwhoToolStripMenuItem
            // 
            this.AndwhoToolStripMenuItem.Name = "AndwhoToolStripMenuItem";
            this.AndwhoToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.AndwhoToolStripMenuItem.Text = "官方网站";
            this.AndwhoToolStripMenuItem.Click += new System.EventHandler(this.AndwhoToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // UpdateToolStripMenuItem
            // 
            this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
            this.UpdateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.UpdateToolStripMenuItem.Text = "检查更新";
            this.UpdateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // labelPhysicalMemory
            // 
            this.labelPhysicalMemory.BackColor = System.Drawing.Color.Transparent;
            this.labelPhysicalMemory.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPhysicalMemory.ForeColor = System.Drawing.Color.White;
            this.labelPhysicalMemory.Location = new System.Drawing.Point(6, 6);
            this.labelPhysicalMemory.Name = "labelPhysicalMemory";
            this.labelPhysicalMemory.Size = new System.Drawing.Size(45, 45);
            this.labelPhysicalMemory.TabIndex = 0;
            this.labelPhysicalMemory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPhysicalMemory.DoubleClick += new System.EventHandler(this.AdapterForm_DoubleClick);
            this.labelPhysicalMemory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseClick);
            this.labelPhysicalMemory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseDown);
            this.labelPhysicalMemory.MouseLeave += new System.EventHandler(this.AdapterForm_MouseLeave);
            this.labelPhysicalMemory.MouseHover += new System.EventHandler(this.AdapterForm_MouseHover);
            this.labelPhysicalMemory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseMove);
            this.labelPhysicalMemory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseUp);
            // 
            // AdapterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(56, 56);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.labelPhysicalMemory);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "AdapterForm";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.AdapterForm_Load);
            this.LocationChanged += new System.EventHandler(this.AdapterForm_LocationChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AdapterForm_Paint);
            this.DoubleClick += new System.EventHandler(this.AdapterForm_DoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseDown);
            this.MouseLeave += new System.EventHandler(this.AdapterForm_MouseLeave);
            this.MouseHover += new System.EventHandler(this.AdapterForm_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AdapterForm_MouseUp);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AndwhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateToolStripMenuItem;
        private System.Windows.Forms.Label labelPhysicalMemory;
        private System.Windows.Forms.ToolStripMenuItem ClearMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearGarbageToolStripMenuItem;
    }
}