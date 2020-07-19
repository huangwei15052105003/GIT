namespace Insigma.Eyes.PSS.WinUI.Controls
{
    partial class Inventory
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewCommodity = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manufacturerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPriceHigh = new System.Windows.Forms.TextBox();
            this.textBoxPriceLow = new System.Windows.Forms.TextBox();
            this.textBoxManufacturer = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommodity)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(712, 532);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ImageIndex = 7;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(704, 505);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "库存管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewCommodity);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(698, 399);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "库存信息";
            // 
            // dataGridViewCommodity
            // 
            this.dataGridViewCommodity.AllowUserToAddRows = false;
            this.dataGridViewCommodity.AllowUserToDeleteRows = false;
            this.dataGridViewCommodity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCommodity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCommodity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCommodity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.nameColumn,
            this.typeColumn,
            this.manufacturerColumn,
            this.inventoryColumn,
            this.unitPriceColumn,
            this.unitColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCommodity.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCommodity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCommodity.Location = new System.Drawing.Point(3, 42);
            this.dataGridViewCommodity.MultiSelect = false;
            this.dataGridViewCommodity.Name = "dataGridViewCommodity";
            this.dataGridViewCommodity.ReadOnly = true;
            this.dataGridViewCommodity.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewCommodity.RowHeadersVisible = false;
            this.dataGridViewCommodity.RowTemplate.Height = 23;
            this.dataGridViewCommodity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCommodity.Size = new System.Drawing.Size(692, 354);
            this.dataGridViewCommodity.TabIndex = 1;
            // 
            // idColumn
            // 
            this.idColumn.DataPropertyName = "ID";
            this.idColumn.HeaderText = "编号";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Visible = false;
            // 
            // nameColumn
            // 
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "  产品名称";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // typeColumn
            // 
            this.typeColumn.DataPropertyName = "Type";
            this.typeColumn.HeaderText = "  产品型号";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            // 
            // manufacturerColumn
            // 
            this.manufacturerColumn.DataPropertyName = "Manufacturer";
            this.manufacturerColumn.HeaderText = "  生产厂家";
            this.manufacturerColumn.Name = "manufacturerColumn";
            this.manufacturerColumn.ReadOnly = true;
            // 
            // inventoryColumn
            // 
            this.inventoryColumn.DataPropertyName = "Inventory";
            this.inventoryColumn.HeaderText = "  库存";
            this.inventoryColumn.Name = "inventoryColumn";
            this.inventoryColumn.ReadOnly = true;
            // 
            // unitPriceColumn
            // 
            this.unitPriceColumn.DataPropertyName = "UnitPrice";
            this.unitPriceColumn.HeaderText = "  参考单价";
            this.unitPriceColumn.Name = "unitPriceColumn";
            this.unitPriceColumn.ReadOnly = true;
            // 
            // unitColumn
            // 
            this.unitColumn.DataPropertyName = "Unit";
            this.unitColumn.HeaderText = "  单位";
            this.unitColumn.Name = "unitColumn";
            this.unitColumn.ReadOnly = true;
            this.unitColumn.Width = 140;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(692, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "新增产品";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton2.Text = "修改成品";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Controls.Add(this.buttonSearch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxPriceHigh);
            this.groupBox1.Controls.Add(this.textBoxPriceLow);
            this.groupBox1.Controls.Add(this.textBoxManufacturer);
            this.groupBox1.Controls.Add(this.textBoxType);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(698, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库存查询";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(569, 58);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "重置";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(569, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "查询";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "至";
            // 
            // textBoxPriceHigh
            // 
            this.textBoxPriceHigh.Location = new System.Drawing.Point(184, 60);
            this.textBoxPriceHigh.Name = "textBoxPriceHigh";
            this.textBoxPriceHigh.Size = new System.Drawing.Size(74, 21);
            this.textBoxPriceHigh.TabIndex = 2;
            // 
            // textBoxPriceLow
            // 
            this.textBoxPriceLow.Location = new System.Drawing.Point(84, 60);
            this.textBoxPriceLow.Name = "textBoxPriceLow";
            this.textBoxPriceLow.Size = new System.Drawing.Size(73, 21);
            this.textBoxPriceLow.TabIndex = 1;
            // 
            // textBoxManufacturer
            // 
            this.textBoxManufacturer.Location = new System.Drawing.Point(344, 60);
            this.textBoxManufacturer.Name = "textBoxManufacturer";
            this.textBoxManufacturer.Size = new System.Drawing.Size(174, 21);
            this.textBoxManufacturer.TabIndex = 1;
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(344, 24);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(174, 21);
            this.textBoxType.TabIndex = 1;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(84, 24);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(174, 21);
            this.textBoxName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "参考单价：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(282, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "生产厂家：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "产品型号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品名称：";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3237.ico");
            this.imageList1.Images.SetKeyName(1, "327.ico");
            this.imageList1.Images.SetKeyName(2, "328.ico");
            this.imageList1.Images.SetKeyName(3, "329.ico");
            this.imageList1.Images.SetKeyName(4, "3210.ico");
            this.imageList1.Images.SetKeyName(5, "3211.ico");
            this.imageList1.Images.SetKeyName(6, "3212.ico");
            this.imageList1.Images.SetKeyName(7, "3213.ico");
            this.imageList1.Images.SetKeyName(8, "3214.ico");
            this.imageList1.Images.SetKeyName(9, "3215.ico");
            this.imageList1.Images.SetKeyName(10, "3216.ico");
            this.imageList1.Images.SetKeyName(11, "3217.ico");
            this.imageList1.Images.SetKeyName(12, "3218.ico");
            this.imageList1.Images.SetKeyName(13, "3219.ico");
            this.imageList1.Images.SetKeyName(14, "3220.ico");
            this.imageList1.Images.SetKeyName(15, "3221.ico");
            this.imageList1.Images.SetKeyName(16, "3222.ico");
            this.imageList1.Images.SetKeyName(17, "3223.ico");
            this.imageList1.Images.SetKeyName(18, "3224.ico");
            this.imageList1.Images.SetKeyName(19, "3225.ico");
            this.imageList1.Images.SetKeyName(20, "3226.ico");
            this.imageList1.Images.SetKeyName(21, "3227.ico");
            this.imageList1.Images.SetKeyName(22, "3228.ico");
            this.imageList1.Images.SetKeyName(23, "3229.ico");
            this.imageList1.Images.SetKeyName(24, "3230.ico");
            this.imageList1.Images.SetKeyName(25, "3231.ico");
            this.imageList1.Images.SetKeyName(26, "3232.ico");
            this.imageList1.Images.SetKeyName(27, "3233.ico");
            this.imageList1.Images.SetKeyName(28, "3234.ico");
            this.imageList1.Images.SetKeyName(29, "3235.ico");
            this.imageList1.Images.SetKeyName(30, "3236.ico");
            this.imageList1.Images.SetKeyName(31, "3237.ico");
            this.imageList1.Images.SetKeyName(32, "3238.ico");
            this.imageList1.Images.SetKeyName(33, "3239.ico");
            this.imageList1.Images.SetKeyName(34, "3240.ico");
            this.imageList1.Images.SetKeyName(35, "3241.ico");
            this.imageList1.Images.SetKeyName(36, "3242.ico");
            this.imageList1.Images.SetKeyName(37, "3243.ico");
            this.imageList1.Images.SetKeyName(38, "3244.ico");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "321.ico");
            this.imageList2.Images.SetKeyName(1, "322.ico");
            this.imageList2.Images.SetKeyName(2, "323.ico");
            this.imageList2.Images.SetKeyName(3, "324.ico");
            this.imageList2.Images.SetKeyName(4, "325.ico");
            this.imageList2.Images.SetKeyName(5, "326.ico");
            this.imageList2.Images.SetKeyName(6, "327.ico");
            this.imageList2.Images.SetKeyName(7, "328.ico");
            this.imageList2.Images.SetKeyName(8, "329.ico");
            this.imageList2.Images.SetKeyName(9, "3210.ico");
            this.imageList2.Images.SetKeyName(10, "3211.ico");
            this.imageList2.Images.SetKeyName(11, "3212.ico");
            this.imageList2.Images.SetKeyName(12, "3213.ico");
            this.imageList2.Images.SetKeyName(13, "3214.ico");
            this.imageList2.Images.SetKeyName(14, "3215.ico");
            this.imageList2.Images.SetKeyName(15, "3216.ico");
            this.imageList2.Images.SetKeyName(16, "3217.ico");
            this.imageList2.Images.SetKeyName(17, "3218.ico");
            this.imageList2.Images.SetKeyName(18, "3219.ico");
            this.imageList2.Images.SetKeyName(19, "3220.ico");
            this.imageList2.Images.SetKeyName(20, "3221.ico");
            this.imageList2.Images.SetKeyName(21, "3222.ico");
            this.imageList2.Images.SetKeyName(22, "3223.ico");
            this.imageList2.Images.SetKeyName(23, "3224.ico");
            this.imageList2.Images.SetKeyName(24, "3225.ico");
            this.imageList2.Images.SetKeyName(25, "3226.ico");
            this.imageList2.Images.SetKeyName(26, "3227.ico");
            this.imageList2.Images.SetKeyName(27, "3228.ico");
            this.imageList2.Images.SetKeyName(28, "3229.ico");
            this.imageList2.Images.SetKeyName(29, "3230.ico");
            this.imageList2.Images.SetKeyName(30, "3231.ico");
            this.imageList2.Images.SetKeyName(31, "3232.ico");
            this.imageList2.Images.SetKeyName(32, "3233.ico");
            this.imageList2.Images.SetKeyName(33, "3234.ico");
            this.imageList2.Images.SetKeyName(34, "3235.ico");
            this.imageList2.Images.SetKeyName(35, "3236.ico");
            this.imageList2.Images.SetKeyName(36, "3237.ico");
            this.imageList2.Images.SetKeyName(37, "3238.ico");
            this.imageList2.Images.SetKeyName(38, "3239.ico");
            this.imageList2.Images.SetKeyName(39, "3240.ico");
            this.imageList2.Images.SetKeyName(40, "3241.ico");
            this.imageList2.Images.SetKeyName(41, "3242.ico");
            this.imageList2.Images.SetKeyName(42, "3243.ico");
            this.imageList2.Images.SetKeyName(43, "3244.ico");
            this.imageList2.Images.SetKeyName(44, "3245.ico");
            this.imageList2.Images.SetKeyName(45, "3246.ico");
            this.imageList2.Images.SetKeyName(46, "3247.ico");
            this.imageList2.Images.SetKeyName(47, "3248.ico");
            this.imageList2.Images.SetKeyName(48, "3249.ico");
            this.imageList2.Images.SetKeyName(49, "3250.ico");
            this.imageList2.Images.SetKeyName(50, "3251.ico");
            this.imageList2.Images.SetKeyName(51, "3252.ico");
            this.imageList2.Images.SetKeyName(52, "3253.ico");
            this.imageList2.Images.SetKeyName(53, "3254.ico");
            this.imageList2.Images.SetKeyName(54, "3255.ico");
            this.imageList2.Images.SetKeyName(55, "3256.ico");
            this.imageList2.Images.SetKeyName(56, "3257.ico");
            this.imageList2.Images.SetKeyName(57, "3258.ico");
            this.imageList2.Images.SetKeyName(58, "3259.ico");
            this.imageList2.Images.SetKeyName(59, "3260.ico");
            this.imageList2.Images.SetKeyName(60, "3261.ico");
            this.imageList2.Images.SetKeyName(61, "3262.ico");
            this.imageList2.Images.SetKeyName(62, "3263.ico");
            this.imageList2.Images.SetKeyName(63, "3264.ico");
            this.imageList2.Images.SetKeyName(64, "3265.ico");
            this.imageList2.Images.SetKeyName(65, "3266.ico");
            this.imageList2.Images.SetKeyName(66, "3267.ico");
            this.imageList2.Images.SetKeyName(67, "3268.ico");
            this.imageList2.Images.SetKeyName(68, "3269.ico");
            this.imageList2.Images.SetKeyName(69, "3270.ico");
            this.imageList2.Images.SetKeyName(70, "3271.ico");
            this.imageList2.Images.SetKeyName(71, "3272.ico");
            this.imageList2.Images.SetKeyName(72, "3273.ico");
            this.imageList2.Images.SetKeyName(73, "3274.ico");
            this.imageList2.Images.SetKeyName(74, "3275.ico");
            this.imageList2.Images.SetKeyName(75, "3276.ico");
            this.imageList2.Images.SetKeyName(76, "3277.ico");
            this.imageList2.Images.SetKeyName(77, "3278.ico");
            this.imageList2.Images.SetKeyName(78, "3279.ico");
            this.imageList2.Images.SetKeyName(79, "3280.ico");
            this.imageList2.Images.SetKeyName(80, "3281.ico");
            this.imageList2.Images.SetKeyName(81, "3282.ico");
            this.imageList2.Images.SetKeyName(82, "3283.ico");
            this.imageList2.Images.SetKeyName(83, "3284.ico");
            this.imageList2.Images.SetKeyName(84, "3285.ico");
            this.imageList2.Images.SetKeyName(85, "3286.ico");
            this.imageList2.Images.SetKeyName(86, "3287.ico");
            this.imageList2.Images.SetKeyName(87, "3288.ico");
            this.imageList2.Images.SetKeyName(88, "3289.ico");
            this.imageList2.Images.SetKeyName(89, "3290.ico");
            this.imageList2.Images.SetKeyName(90, "3291.ico");
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "Inventory";
            this.Size = new System.Drawing.Size(712, 532);
            this.Load += new System.EventHandler(this.Inventory_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommodity)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPriceHigh;
        private System.Windows.Forms.TextBox textBoxPriceLow;
        private System.Windows.Forms.TextBox textBoxManufacturer;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dataGridViewCommodity;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventoryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitColumn;
    }
}
