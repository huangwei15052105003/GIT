namespace EquipmentMS.Equipment
{
    partial class frmEquimentSum
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
            this.dtpZjrq = new System.Windows.Forms.DateTimePicker();
            this.btnSum = new System.Windows.Forms.Button();
            this.dgvEquiment = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquiment)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpZjrq
            // 
            this.dtpZjrq.Location = new System.Drawing.Point(112, 12);
            this.dtpZjrq.Name = "dtpZjrq";
            this.dtpZjrq.Size = new System.Drawing.Size(111, 21);
            this.dtpZjrq.TabIndex = 1;
            // 
            // btnSum
            // 
            this.btnSum.Location = new System.Drawing.Point(243, 12);
            this.btnSum.Name = "btnSum";
            this.btnSum.Size = new System.Drawing.Size(75, 23);
            this.btnSum.TabIndex = 2;
            this.btnSum.Text = "折旧核算";
            this.btnSum.UseVisualStyleBackColor = true;
            this.btnSum.Click += new System.EventHandler(this.btnSum_Click);
            // 
            // dgvEquiment
            // 
            this.dgvEquiment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquiment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvEquiment.Location = new System.Drawing.Point(0, 41);
            this.dgvEquiment.Name = "dgvEquiment";
            this.dgvEquiment.RowTemplate.Height = 23;
            this.dgvEquiment.Size = new System.Drawing.Size(768, 344);
            this.dgvEquiment.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "请选择折旧日期：";
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(324, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(113, 23);
            this.btnExcel.TabIndex = 2;
            this.btnExcel.Text = "导出(电子表格）";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(443, 12);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 2;
            this.btnCalc.Text = "计算器";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(524, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "关　闭";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmEquimentSum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 385);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEquiment);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnSum);
            this.Controls.Add(this.dtpZjrq);
            this.Name = "frmEquimentSum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "固定资产折旧核算";
            this.Resize += new System.EventHandler(this.frmEquimentSum_Resize);
            this.Load += new System.EventHandler(this.frmEquimentSum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquiment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpZjrq;
        private System.Windows.Forms.Button btnSum;
        private System.Windows.Forms.DataGridView dgvEquiment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnExit;

    }
}