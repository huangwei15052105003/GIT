namespace Insigma.Eyes.PSS.WinUI.Controls
{
    partial class Purchase
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
// Downloads By http://www.veryhuo.com
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Purchase));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridViewPurchaseList = new System.Windows.Forms.DataGridView();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommodityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommodityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommodityType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommodityManufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTotalMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelOrderDate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelSupplierName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelTel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelOrderNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listViewOrders = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxdateEnd = new System.Windows.Forms.TextBox();
            this.textBoxdateStart = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchaseList)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(899, 546);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ImageIndex = 79;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(891, 519);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "采购管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(273, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(615, 513);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridViewPurchaseList);
            this.groupBox6.Controls.Add(this.toolStrip2);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 125);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(609, 385);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "订单产品列表";
            // 
            // dataGridViewPurchaseList
            // 
            this.dataGridViewPurchaseList.AllowUserToAddRows = false;
            this.dataGridViewPurchaseList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPurchaseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPurchaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPurchaseList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnOrderID,
            this.ColumnCommodityID,
            this.ColumnCommodityName,
            this.ColumnCommodityType,
            this.ColumnCommodityManufacturer,
            this.ColumnPrice,
            this.ColumnCount,
            this.ColumnTotalMoney});
            this.dataGridViewPurchaseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPurchaseList.Location = new System.Drawing.Point(3, 42);
            this.dataGridViewPurchaseList.MultiSelect = false;
            this.dataGridViewPurchaseList.Name = "dataGridViewPurchaseList";
            this.dataGridViewPurchaseList.ReadOnly = true;
            this.dataGridViewPurchaseList.RowHeadersVisible = false;
            this.dataGridViewPurchaseList.RowTemplate.Height = 23;
            this.dataGridViewPurchaseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPurchaseList.Size = new System.Drawing.Size(603, 340);
            this.dataGridViewPurchaseList.TabIndex = 3;
            // 
            // ColumnID
            // 
            this.ColumnID.DataPropertyName = "ID";
            this.ColumnID.HeaderText = "编号";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.ReadOnly = true;
            this.ColumnID.Visible = false;
            // 
            // ColumnOrderID
            // 
            this.ColumnOrderID.DataPropertyName = "PurchaseOrderID";
            this.ColumnOrderID.HeaderText = "订单ID";
            this.ColumnOrderID.Name = "ColumnOrderID";
            this.ColumnOrderID.ReadOnly = true;
            this.ColumnOrderID.Visible = false;
            // 
            // ColumnCommodityID
            // 
            this.ColumnCommodityID.DataPropertyName = "CommodityID";
            this.ColumnCommodityID.HeaderText = "产品ID";
            this.ColumnCommodityID.Name = "ColumnCommodityID";
            this.ColumnCommodityID.ReadOnly = true;
            this.ColumnCommodityID.Visible = false;
            // 
            // ColumnCommodityName
            // 
            this.ColumnCommodityName.DataPropertyName = "CommodityName";
            this.ColumnCommodityName.HeaderText = "产品名称";
            this.ColumnCommodityName.Name = "ColumnCommodityName";
            this.ColumnCommodityName.ReadOnly = true;
            // 
            // ColumnCommodityType
            // 
            this.ColumnCommodityType.DataPropertyName = "CommodityType";
            this.ColumnCommodityType.HeaderText = "产品型号";
            this.ColumnCommodityType.Name = "ColumnCommodityType";
            this.ColumnCommodityType.ReadOnly = true;
            // 
            // ColumnCommodityManufacturer
            // 
            this.ColumnCommodityManufacturer.DataPropertyName = "CommodityManufacturer";
            this.ColumnCommodityManufacturer.HeaderText = "生产厂家";
            this.ColumnCommodityManufacturer.Name = "ColumnCommodityManufacturer";
            this.ColumnCommodityManufacturer.ReadOnly = true;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.DataPropertyName = "PurchasePrice";
            this.ColumnPrice.HeaderText = "单价";
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.ReadOnly = true;
            // 
            // ColumnCount
            // 
            this.ColumnCount.DataPropertyName = "Count";
            this.ColumnCount.HeaderText = "数量";
            this.ColumnCount.Name = "ColumnCount";
            this.ColumnCount.ReadOnly = true;
            // 
            // ColumnTotalMoney
            // 
            this.ColumnTotalMoney.DataPropertyName = "TotalPrice";
            this.ColumnTotalMoney.HeaderText = "总金额";
            this.ColumnTotalMoney.Name = "ColumnTotalMoney";
            this.ColumnTotalMoney.ReadOnly = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip2.Location = new System.Drawing.Point(3, 17);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(603, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton4.Text = "新增产品";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton5.Text = "修改产品";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton6.Text = "删除产品";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelAddress);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.labelStatus);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.labelOrderDate);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.labelSupplierName);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.labelTel);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.labelOrderNumber);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(609, 108);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "订单信息";
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(393, 83);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(17, 12);
            this.labelAddress.TabIndex = 11;
            this.labelAddress.Text = "[]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(321, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "厂家地址：";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.labelStatus.Location = new System.Drawing.Point(393, 56);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(17, 12);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "[]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(321, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "订单状态：";
            // 
            // labelOrderDate
            // 
            this.labelOrderDate.AutoSize = true;
            this.labelOrderDate.Location = new System.Drawing.Point(106, 83);
            this.labelOrderDate.Name = "labelOrderDate";
            this.labelOrderDate.Size = new System.Drawing.Size(17, 12);
            this.labelOrderDate.TabIndex = 7;
            this.labelOrderDate.Text = "[]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "订单时间：";
            // 
            // labelSupplierName
            // 
            this.labelSupplierName.AutoSize = true;
            this.labelSupplierName.Location = new System.Drawing.Point(393, 29);
            this.labelSupplierName.Name = "labelSupplierName";
            this.labelSupplierName.Size = new System.Drawing.Size(17, 12);
            this.labelSupplierName.TabIndex = 5;
            this.labelSupplierName.Text = "[]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(321, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "供应商名称：";
            // 
            // labelTel
            // 
            this.labelTel.AutoSize = true;
            this.labelTel.Location = new System.Drawing.Point(106, 54);
            this.labelTel.Name = "labelTel";
            this.labelTel.Size = new System.Drawing.Size(17, 12);
            this.labelTel.TabIndex = 3;
            this.labelTel.Text = "[]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "联系电话：";
            // 
            // labelOrderNumber
            // 
            this.labelOrderNumber.AutoSize = true;
            this.labelOrderNumber.Location = new System.Drawing.Point(106, 28);
            this.labelOrderNumber.Name = "labelOrderNumber";
            this.labelOrderNumber.Size = new System.Drawing.Size(17, 12);
            this.labelOrderNumber.TabIndex = 1;
            this.labelOrderNumber.Text = "[]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "订单编号：";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(270, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 513);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 513);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listViewOrders);
            this.groupBox4.Controls.Add(this.toolStrip1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(261, 354);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "订单列表";
            // 
            // listViewOrders
            // 
            this.listViewOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOrders.HideSelection = false;
            this.listViewOrders.Location = new System.Drawing.Point(3, 42);
            this.listViewOrders.MultiSelect = false;
            this.listViewOrders.Name = "listViewOrders";
            this.listViewOrders.Size = new System.Drawing.Size(255, 309);
            this.listViewOrders.TabIndex = 2;
            this.listViewOrders.UseCompatibleStateImageBehavior = false;
            this.listViewOrders.View = System.Windows.Forms.View.List;
            this.listViewOrders.SelectedIndexChanged += new System.EventHandler(this.listViewOrders_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton7});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(255, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "新增";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "修改";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton3.Text = "删除";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton7.Text = "提交";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxdateEnd);
            this.groupBox3.Controls.Add(this.textBoxdateStart);
            this.groupBox3.Controls.Add(this.comboBoxStatus);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxNumber);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 139);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询订单";
            // 
            // textBoxdateEnd
            // 
            this.textBoxdateEnd.Location = new System.Drawing.Point(101, 79);
            this.textBoxdateEnd.Name = "textBoxdateEnd";
            this.textBoxdateEnd.Size = new System.Drawing.Size(138, 21);
            this.textBoxdateEnd.TabIndex = 13;
            this.textBoxdateEnd.Text = "2013-12-12";
            this.textBoxdateEnd.TextChanged += new System.EventHandler(this.TextChange);
            // 
            // textBoxdateStart
            // 
            this.textBoxdateStart.Location = new System.Drawing.Point(101, 51);
            this.textBoxdateStart.Name = "textBoxdateStart";
            this.textBoxdateStart.Size = new System.Drawing.Size(138, 21);
            this.textBoxdateStart.TabIndex = 14;
            this.textBoxdateStart.Text = "2011-05-06";
            this.textBoxdateStart.TextChanged += new System.EventHandler(this.TextChange);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "已入库",
            "未入库"});
            this.comboBoxStatus.Location = new System.Drawing.Point(102, 108);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(56, 20);
            this.comboBoxStatus.TabIndex = 11;
            this.comboBoxStatus.TextChanged += new System.EventHandler(this.TextChange);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "订单状态：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "至";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "订单时间：从";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(101, 20);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(138, 21);
            this.textBoxNumber.TabIndex = 3;
            this.textBoxNumber.TextChanged += new System.EventHandler(this.TextChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "订单编号：";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "321.ico");
            this.imageList1.Images.SetKeyName(1, "322.ico");
            this.imageList1.Images.SetKeyName(2, "323.ico");
            this.imageList1.Images.SetKeyName(3, "324.ico");
            this.imageList1.Images.SetKeyName(4, "325.ico");
            this.imageList1.Images.SetKeyName(5, "326.ico");
            this.imageList1.Images.SetKeyName(6, "327.ico");
            this.imageList1.Images.SetKeyName(7, "328.ico");
            this.imageList1.Images.SetKeyName(8, "329.ico");
            this.imageList1.Images.SetKeyName(9, "3210.ico");
            this.imageList1.Images.SetKeyName(10, "3211.ico");
            this.imageList1.Images.SetKeyName(11, "3212.ico");
            this.imageList1.Images.SetKeyName(12, "3213.ico");
            this.imageList1.Images.SetKeyName(13, "3214.ico");
            this.imageList1.Images.SetKeyName(14, "3215.ico");
            this.imageList1.Images.SetKeyName(15, "3216.ico");
            this.imageList1.Images.SetKeyName(16, "3217.ico");
            this.imageList1.Images.SetKeyName(17, "3218.ico");
            this.imageList1.Images.SetKeyName(18, "3219.ico");
            this.imageList1.Images.SetKeyName(19, "3220.ico");
            this.imageList1.Images.SetKeyName(20, "3221.ico");
            this.imageList1.Images.SetKeyName(21, "3222.ico");
            this.imageList1.Images.SetKeyName(22, "3223.ico");
            this.imageList1.Images.SetKeyName(23, "3224.ico");
            this.imageList1.Images.SetKeyName(24, "3225.ico");
            this.imageList1.Images.SetKeyName(25, "3226.ico");
            this.imageList1.Images.SetKeyName(26, "3227.ico");
            this.imageList1.Images.SetKeyName(27, "3228.ico");
            this.imageList1.Images.SetKeyName(28, "3229.ico");
            this.imageList1.Images.SetKeyName(29, "3230.ico");
            this.imageList1.Images.SetKeyName(30, "3231.ico");
            this.imageList1.Images.SetKeyName(31, "3232.ico");
            this.imageList1.Images.SetKeyName(32, "3233.ico");
            this.imageList1.Images.SetKeyName(33, "3234.ico");
            this.imageList1.Images.SetKeyName(34, "3235.ico");
            this.imageList1.Images.SetKeyName(35, "3236.ico");
            this.imageList1.Images.SetKeyName(36, "3237.ico");
            this.imageList1.Images.SetKeyName(37, "3238.ico");
            this.imageList1.Images.SetKeyName(38, "3239.ico");
            this.imageList1.Images.SetKeyName(39, "3240.ico");
            this.imageList1.Images.SetKeyName(40, "3241.ico");
            this.imageList1.Images.SetKeyName(41, "3242.ico");
            this.imageList1.Images.SetKeyName(42, "3243.ico");
            this.imageList1.Images.SetKeyName(43, "3244.ico");
            this.imageList1.Images.SetKeyName(44, "3245.ico");
            this.imageList1.Images.SetKeyName(45, "3246.ico");
            this.imageList1.Images.SetKeyName(46, "3247.ico");
            this.imageList1.Images.SetKeyName(47, "3248.ico");
            this.imageList1.Images.SetKeyName(48, "3249.ico");
            this.imageList1.Images.SetKeyName(49, "3250.ico");
            this.imageList1.Images.SetKeyName(50, "3251.ico");
            this.imageList1.Images.SetKeyName(51, "3252.ico");
            this.imageList1.Images.SetKeyName(52, "3253.ico");
            this.imageList1.Images.SetKeyName(53, "3254.ico");
            this.imageList1.Images.SetKeyName(54, "3255.ico");
            this.imageList1.Images.SetKeyName(55, "3256.ico");
            this.imageList1.Images.SetKeyName(56, "3257.ico");
            this.imageList1.Images.SetKeyName(57, "3258.ico");
            this.imageList1.Images.SetKeyName(58, "3259.ico");
            this.imageList1.Images.SetKeyName(59, "3260.ico");
            this.imageList1.Images.SetKeyName(60, "3261.ico");
            this.imageList1.Images.SetKeyName(61, "3262.ico");
            this.imageList1.Images.SetKeyName(62, "3263.ico");
            this.imageList1.Images.SetKeyName(63, "3264.ico");
            this.imageList1.Images.SetKeyName(64, "3265.ico");
            this.imageList1.Images.SetKeyName(65, "3266.ico");
            this.imageList1.Images.SetKeyName(66, "3267.ico");
            this.imageList1.Images.SetKeyName(67, "3268.ico");
            this.imageList1.Images.SetKeyName(68, "3269.ico");
            this.imageList1.Images.SetKeyName(69, "3270.ico");
            this.imageList1.Images.SetKeyName(70, "3271.ico");
            this.imageList1.Images.SetKeyName(71, "3272.ico");
            this.imageList1.Images.SetKeyName(72, "3273.ico");
            this.imageList1.Images.SetKeyName(73, "3274.ico");
            this.imageList1.Images.SetKeyName(74, "3275.ico");
            this.imageList1.Images.SetKeyName(75, "3276.ico");
            this.imageList1.Images.SetKeyName(76, "3277.ico");
            this.imageList1.Images.SetKeyName(77, "3278.ico");
            this.imageList1.Images.SetKeyName(78, "3279.ico");
            this.imageList1.Images.SetKeyName(79, "3280.ico");
            this.imageList1.Images.SetKeyName(80, "3281.ico");
            this.imageList1.Images.SetKeyName(81, "3282.ico");
            this.imageList1.Images.SetKeyName(82, "3283.ico");
            this.imageList1.Images.SetKeyName(83, "3284.ico");
            this.imageList1.Images.SetKeyName(84, "3285.ico");
            this.imageList1.Images.SetKeyName(85, "3286.ico");
            this.imageList1.Images.SetKeyName(86, "3287.ico");
            this.imageList1.Images.SetKeyName(87, "3288.ico");
            this.imageList1.Images.SetKeyName(88, "3289.ico");
            this.imageList1.Images.SetKeyName(89, "3290.ico");
            this.imageList1.Images.SetKeyName(90, "3291.ico");
            // 
            // Purchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "Purchase";
            this.Size = new System.Drawing.Size(899, 546);
            this.Load += new System.EventHandler(this.Purchase_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPurchaseList)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
       // private DevExpress.XtraEditors.DateEdit dateEditOrderDateEnd;
        //private DevExpress.XtraEditors.DateEdit dateEditOrderDateStart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelOrderDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelSupplierName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelTel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelOrderNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.DataGridView dataGridViewPurchaseList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommodityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommodityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommodityType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommodityManufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTotalMoney;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox textBoxdateEnd;
        private System.Windows.Forms.TextBox textBoxdateStart;
        private System.Windows.Forms.ListView listViewOrders;
    }
}
