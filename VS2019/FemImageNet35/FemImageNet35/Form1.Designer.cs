namespace FemImageNet35
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelRow = new System.Windows.Forms.Label();
            this.labelCol = new System.Windows.Forms.Label();
            this.labelIdw = new System.Windows.Forms.Label();
            this.labelRecipe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRecipe = new System.Windows.Forms.TextBox();
            this.textBoxIdw = new System.Windows.Forms.TextBox();
            this.textBoxLotid = new System.Windows.Forms.TextBox();
            this.textBoxMpqty = new System.Windows.Forms.TextBox();
            this.textBoxCol = new System.Windows.Forms.TextBox();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonCD = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonImage = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(44, 21);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Read";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(748, 7);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(23, 12);
            this.labelRow.TabIndex = 1;
            this.labelRow.Text = "ROW";
            // 
            // labelCol
            // 
            this.labelCol.AutoSize = true;
            this.labelCol.Location = new System.Drawing.Point(663, 7);
            this.labelCol.Name = "labelCol";
            this.labelCol.Size = new System.Drawing.Size(23, 12);
            this.labelCol.TabIndex = 1;
            this.labelCol.Text = "COL";
            // 
            // labelIdw
            // 
            this.labelIdw.AutoSize = true;
            this.labelIdw.Location = new System.Drawing.Point(256, 7);
            this.labelIdw.Name = "labelIdw";
            this.labelIdw.Size = new System.Drawing.Size(23, 12);
            this.labelIdw.TabIndex = 1;
            this.labelIdw.Text = "IDW";
            // 
            // labelRecipe
            // 
            this.labelRecipe.AutoSize = true;
            this.labelRecipe.Location = new System.Drawing.Point(61, 7);
            this.labelRecipe.Name = "labelRecipe";
            this.labelRecipe.Size = new System.Drawing.Size(41, 12);
            this.labelRecipe.TabIndex = 1;
            this.labelRecipe.Text = "RECIPE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(560, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "MP_QTY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "LOT_ID";
            // 
            // textBoxRecipe
            // 
            this.textBoxRecipe.Location = new System.Drawing.Point(112, 3);
            this.textBoxRecipe.Name = "textBoxRecipe";
            this.textBoxRecipe.Size = new System.Drawing.Size(134, 21);
            this.textBoxRecipe.TabIndex = 2;
            // 
            // textBoxIdw
            // 
            this.textBoxIdw.Location = new System.Drawing.Point(289, 3);
            this.textBoxIdw.Name = "textBoxIdw";
            this.textBoxIdw.Size = new System.Drawing.Size(100, 21);
            this.textBoxIdw.TabIndex = 2;
            // 
            // textBoxLotid
            // 
            this.textBoxLotid.Location = new System.Drawing.Point(450, 3);
            this.textBoxLotid.Name = "textBoxLotid";
            this.textBoxLotid.Size = new System.Drawing.Size(100, 21);
            this.textBoxLotid.TabIndex = 2;
            // 
            // textBoxMpqty
            // 
            this.textBoxMpqty.Location = new System.Drawing.Point(611, 3);
            this.textBoxMpqty.Name = "textBoxMpqty";
            this.textBoxMpqty.Size = new System.Drawing.Size(42, 21);
            this.textBoxMpqty.TabIndex = 2;
            // 
            // textBoxCol
            // 
            this.textBoxCol.Location = new System.Drawing.Point(696, 3);
            this.textBoxCol.Name = "textBoxCol";
            this.textBoxCol.Size = new System.Drawing.Size(42, 21);
            this.textBoxCol.TabIndex = 2;
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(781, 3);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(42, 21);
            this.textBoxRow.TabIndex = 2;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 40);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(99, 68);
            this.checkedListBox1.TabIndex = 3;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedListBox1_ItemCheck);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(362, 296);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
            // 
            // buttonCD
            // 
            this.buttonCD.Location = new System.Drawing.Point(3, 114);
            this.buttonCD.Name = "buttonCD";
            this.buttonCD.Size = new System.Drawing.Size(44, 23);
            this.buttonCD.TabIndex = 5;
            this.buttonCD.Text = "CD";
            this.buttonCD.UseVisualStyleBackColor = true;
            this.buttonCD.Click += new System.EventHandler(this.ButtonDisplay_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(371, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 460);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // buttonImage
            // 
            this.buttonImage.Location = new System.Drawing.Point(58, 114);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(44, 23);
            this.buttonImage.TabIndex = 7;
            this.buttonImage.Text = "IMAGE";
            this.buttonImage.UseVisualStyleBackColor = true;
            this.buttonImage.Click += new System.EventHandler(this.ButtonImage_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(112, 40);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(101, 16);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "With CD Label";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(112, 62);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "W/O CD Label";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 494);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.buttonImage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonCD);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.textBoxCol);
            this.Controls.Add(this.textBoxMpqty);
            this.Controls.Add(this.textBoxLotid);
            this.Controls.Add(this.textBoxIdw);
            this.Controls.Add(this.textBoxRecipe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelRecipe);
            this.Controls.Add(this.labelIdw);
            this.Controls.Add(this.labelCol);
            this.Controls.Add(this.labelRow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpen);
            this.Name = "Form1";
            this.Text = "FEM CD & TOP-DOWN IMAGES";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.Label labelCol;
        private System.Windows.Forms.Label labelIdw;
        private System.Windows.Forms.Label labelRecipe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRecipe;
        private System.Windows.Forms.TextBox textBoxIdw;
        private System.Windows.Forms.TextBox textBoxLotid;
        private System.Windows.Forms.TextBox textBoxMpqty;
        private System.Windows.Forms.TextBox textBoxCol;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonCD;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonImage;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}

