namespace WindowsFormsLitho
{
    partial class ChartForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mENU1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.作图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.作图ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x轴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选取字段ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获得最大最小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选取字段ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.获得最大最小值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.标题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取图表标题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取X轴标题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取YToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取YToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.其它设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.示例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.筛选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.三合一ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除空值行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空值替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 522);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage1.Size = new System.Drawing.Size(1152, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1152, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "表";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(685, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 44);
            this.panel1.TabIndex = 18;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(174, 13);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "折线图";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(93, 13);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "散点图";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 13);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "柱状图";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Navy;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(165, 13);
            this.textBox1.MaximumSize = new System.Drawing.Size(500, 4);
            this.textBox1.MinimumSize = new System.Drawing.Size(500, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(500, 50);
            this.textBox1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "数字变量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "字符变量";
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(84, 21);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(75, 40);
            this.listBox2.TabIndex = 14;
           // this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(5, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(75, 40);
            this.listBox1.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1150, 401);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.mENU1ToolStripMenuItem1,
            this.作图ToolStripMenuItem,
            this.统计ToolStripMenuItem,
            this.作图ToolStripMenuItem1,
            this.示例ToolStripMenuItem,
            this.空值ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 21);
            this.toolStripMenuItem1.Text = "主窗体";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // mENU1ToolStripMenuItem1
            // 
            this.mENU1ToolStripMenuItem1.Name = "mENU1ToolStripMenuItem1";
            this.mENU1ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.mENU1ToolStripMenuItem1.Text = "排序";
            this.mENU1ToolStripMenuItem1.Click += new System.EventHandler(this.mENU1ToolStripMenuItem1_Click);
            // 
            // 作图ToolStripMenuItem
            // 
            this.作图ToolStripMenuItem.Name = "作图ToolStripMenuItem";
            this.作图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.作图ToolStripMenuItem.Text = "筛选";
            this.作图ToolStripMenuItem.Click += new System.EventHandler(this.作图ToolStripMenuItem_Click);
            // 
            // 统计ToolStripMenuItem
            // 
            this.统计ToolStripMenuItem.Name = "统计ToolStripMenuItem";
            this.统计ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.统计ToolStripMenuItem.Text = "统计";
            this.统计ToolStripMenuItem.Click += new System.EventHandler(this.统计ToolStripMenuItem_Click);
            // 
            // 作图ToolStripMenuItem1
            // 
            this.作图ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x轴ToolStripMenuItem,
            this.yToolStripMenuItem,
            this.toolStripMenuItem2,
            this.标题ToolStripMenuItem,
            this.其它设置ToolStripMenuItem,
            this.画图ToolStripMenuItem});
            this.作图ToolStripMenuItem1.Name = "作图ToolStripMenuItem1";
            this.作图ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.作图ToolStripMenuItem1.Text = "作图";
            // 
            // x轴ToolStripMenuItem
            // 
            this.x轴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选取字段ToolStripMenuItem,
            this.获得最大最小ToolStripMenuItem});
            this.x轴ToolStripMenuItem.Name = "x轴ToolStripMenuItem";
            this.x轴ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.x轴ToolStripMenuItem.Text = "X轴定义";
            // 
            // 选取字段ToolStripMenuItem
            // 
            this.选取字段ToolStripMenuItem.Name = "选取字段ToolStripMenuItem";
            this.选取字段ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.选取字段ToolStripMenuItem.Text = "选取字段";
            this.选取字段ToolStripMenuItem.Click += new System.EventHandler(this.选取字段ToolStripMenuItem_Click);
            // 
            // 获得最大最小ToolStripMenuItem
            // 
            this.获得最大最小ToolStripMenuItem.Name = "获得最大最小ToolStripMenuItem";
            this.获得最大最小ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.获得最大最小ToolStripMenuItem.Text = "选取最大/最小值";
            // 
            // yToolStripMenuItem
            // 
            this.yToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选取字段ToolStripMenuItem1,
            this.获得最大最小值ToolStripMenuItem});
            this.yToolStripMenuItem.Name = "yToolStripMenuItem";
            this.yToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.yToolStripMenuItem.Text = "Y主轴定义";
            // 
            // 选取字段ToolStripMenuItem1
            // 
            this.选取字段ToolStripMenuItem1.Name = "选取字段ToolStripMenuItem1";
            this.选取字段ToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.选取字段ToolStripMenuItem1.Text = "选取字段";
            this.选取字段ToolStripMenuItem1.Click += new System.EventHandler(this.选取字段ToolStripMenuItem1_Click);
            // 
            // 获得最大最小值ToolStripMenuItem
            // 
            this.获得最大最小值ToolStripMenuItem.Name = "获得最大最小值ToolStripMenuItem";
            this.获得最大最小值ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.获得最大最小值ToolStripMenuItem.Text = "选取最小/最大值";
            this.获得最大最小值ToolStripMenuItem.Click += new System.EventHandler(this.获得最大最小值ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem2.Text = "Y副轴定义（无效）";
            // 
            // 标题ToolStripMenuItem
            // 
            this.标题ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取图表标题ToolStripMenuItem,
            this.获取X轴标题ToolStripMenuItem,
            this.获取YToolStripMenuItem,
            this.获取YToolStripMenuItem1});
            this.标题ToolStripMenuItem.Name = "标题ToolStripMenuItem";
            this.标题ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.标题ToolStripMenuItem.Text = "标题";
            // 
            // 获取图表标题ToolStripMenuItem
            // 
            this.获取图表标题ToolStripMenuItem.Name = "获取图表标题ToolStripMenuItem";
            this.获取图表标题ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.获取图表标题ToolStripMenuItem.Text = "获取图表标题";
            this.获取图表标题ToolStripMenuItem.Click += new System.EventHandler(this.获取图表标题ToolStripMenuItem_Click);
            // 
            // 获取X轴标题ToolStripMenuItem
            // 
            this.获取X轴标题ToolStripMenuItem.Name = "获取X轴标题ToolStripMenuItem";
            this.获取X轴标题ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.获取X轴标题ToolStripMenuItem.Text = "获取X轴标题";
            this.获取X轴标题ToolStripMenuItem.Click += new System.EventHandler(this.获取X轴标题ToolStripMenuItem_Click);
            // 
            // 获取YToolStripMenuItem
            // 
            this.获取YToolStripMenuItem.Name = "获取YToolStripMenuItem";
            this.获取YToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.获取YToolStripMenuItem.Text = "获取Y主轴标题";
            this.获取YToolStripMenuItem.Click += new System.EventHandler(this.获取YToolStripMenuItem_Click);
            // 
            // 获取YToolStripMenuItem1
            // 
            this.获取YToolStripMenuItem1.Name = "获取YToolStripMenuItem1";
            this.获取YToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.获取YToolStripMenuItem1.Text = "获取Y副轴标题(无效）";
            this.获取YToolStripMenuItem1.Click += new System.EventHandler(this.获取YToolStripMenuItem1_Click);
            // 
            // 其它设置ToolStripMenuItem
            // 
            this.其它设置ToolStripMenuItem.Name = "其它设置ToolStripMenuItem";
            this.其它设置ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.其它设置ToolStripMenuItem.Text = "其它设置（无效）";
            // 
            // 画图ToolStripMenuItem
            // 
            this.画图ToolStripMenuItem.Name = "画图ToolStripMenuItem";
            this.画图ToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.画图ToolStripMenuItem.Text = "画图";
           // this.画图ToolStripMenuItem.Click += new System.EventHandler(this.画图ToolStripMenuItem_Click_1);
            // 
            // 示例ToolStripMenuItem
            // 
            this.示例ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.排序ToolStripMenuItem,
            this.筛选ToolStripMenuItem,
            this.统计ToolStripMenuItem1,
            this.三合一ToolStripMenuItem});
            this.示例ToolStripMenuItem.Name = "示例ToolStripMenuItem";
            this.示例ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.示例ToolStripMenuItem.Text = "示例";
            // 
            // 排序ToolStripMenuItem
            // 
            this.排序ToolStripMenuItem.Name = "排序ToolStripMenuItem";
            this.排序ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.排序ToolStripMenuItem.Text = "排序";
            this.排序ToolStripMenuItem.Click += new System.EventHandler(this.排序ToolStripMenuItem_Click);
            // 
            // 筛选ToolStripMenuItem
            // 
            this.筛选ToolStripMenuItem.Name = "筛选ToolStripMenuItem";
            this.筛选ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.筛选ToolStripMenuItem.Text = "筛选";
            this.筛选ToolStripMenuItem.Click += new System.EventHandler(this.筛选ToolStripMenuItem_Click);
            // 
            // 统计ToolStripMenuItem1
            // 
            this.统计ToolStripMenuItem1.Name = "统计ToolStripMenuItem1";
            this.统计ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.统计ToolStripMenuItem1.Text = "统计";
            this.统计ToolStripMenuItem1.Click += new System.EventHandler(this.统计ToolStripMenuItem1_Click);
            // 
            // 三合一ToolStripMenuItem
            // 
            this.三合一ToolStripMenuItem.Name = "三合一ToolStripMenuItem";
            this.三合一ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.三合一ToolStripMenuItem.Text = "三合一";
            this.三合一ToolStripMenuItem.Click += new System.EventHandler(this.三合一ToolStripMenuItem_Click);
            // 
            // 空值ToolStripMenuItem
            // 
            this.空值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除空值行ToolStripMenuItem,
            this.空值替换ToolStripMenuItem});
            this.空值ToolStripMenuItem.Name = "空值ToolStripMenuItem";
            this.空值ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.空值ToolStripMenuItem.Text = "空值";
            // 
            // 删除空值行ToolStripMenuItem
            // 
            this.删除空值行ToolStripMenuItem.Name = "删除空值行ToolStripMenuItem";
            this.删除空值行ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除空值行ToolStripMenuItem.Text = "删除空值行";
            this.删除空值行ToolStripMenuItem.Click += new System.EventHandler(this.删除空值行ToolStripMenuItem_Click);
            // 
            // 空值替换ToolStripMenuItem
            // 
            this.空值替换ToolStripMenuItem.Name = "空值替换ToolStripMenuItem";
            this.空值替换ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.空值替换ToolStripMenuItem.Text = "空值替换";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            this.Load += new System.EventHandler(this.ChartForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem mENU1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 作图ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 作图ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 示例ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 排序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 筛选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 三合一ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除空值行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空值替换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x轴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选取字段ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获得最大最小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选取字段ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 获得最大最小值ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ToolStripMenuItem 标题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取图表标题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取X轴标题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取YToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取YToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 其它设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}