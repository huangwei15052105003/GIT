namespace EmailSendAndReceive
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.邮件发送ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.邮件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.邮件发送ToolStripMenuItem,
            this.邮件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 邮件发送ToolStripMenuItem
            // 
            this.邮件发送ToolStripMenuItem.Name = "邮件发送ToolStripMenuItem";
            this.邮件发送ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.邮件发送ToolStripMenuItem.Text = "邮件发送";
            this.邮件发送ToolStripMenuItem.Click += new System.EventHandler(this.邮件发送ToolStripMenuItem_Click);
            // 
            // 邮件ToolStripMenuItem
            // 
            this.邮件ToolStripMenuItem.Name = "邮件ToolStripMenuItem";
            this.邮件ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.邮件ToolStripMenuItem.Text = "邮件接收";
            this.邮件ToolStripMenuItem.Click += new System.EventHandler(this.邮件接收ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(590, 388);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "邮件的发送与接收";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 邮件发送ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 邮件ToolStripMenuItem;

    }
}

