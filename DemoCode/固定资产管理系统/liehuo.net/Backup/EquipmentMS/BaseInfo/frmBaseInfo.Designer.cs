namespace EquipmentMS.BaseInfo
{
    partial class frmBaseInfo
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
            this.lbName = new System.Windows.Forms.ListBox();
            this.btnZCMC = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labTitle = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnQLFS = new System.Windows.Forms.Button();
            this.btnJLDW = new System.Windows.Forms.Button();
            this.btnBGRY = new System.Windows.Forms.Button();
            this.btnCFDD = new System.Windows.Forms.Button();
            this.btnSYQK = new System.Windows.Forms.Button();
            this.btnSYBM = new System.Windows.Forms.Button();
            this.btnZJFS = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.FormattingEnabled = true;
            this.lbName.ItemHeight = 12;
            this.lbName.Location = new System.Drawing.Point(97, 39);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(271, 172);
            this.lbName.TabIndex = 0;
            // 
            // btnZCMC
            // 
            this.btnZCMC.Location = new System.Drawing.Point(16, 15);
            this.btnZCMC.Name = "btnZCMC";
            this.btnZCMC.Size = new System.Drawing.Size(75, 23);
            this.btnZCMC.TabIndex = 1;
            this.btnZCMC.Text = "资产名称";
            this.btnZCMC.UseVisualStyleBackColor = true;
            this.btnZCMC.Click += new System.EventHandler(this.btnZCMC_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.labTitle);
            this.groupBox1.Controls.Add(this.lbName);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnQLFS);
            this.groupBox1.Controls.Add(this.btnJLDW);
            this.groupBox1.Controls.Add(this.btnBGRY);
            this.groupBox1.Controls.Add(this.btnCFDD);
            this.groupBox1.Controls.Add(this.btnSYQK);
            this.groupBox1.Controls.Add(this.btnSYBM);
            this.groupBox1.Controls.Add(this.btnZJFS);
            this.groupBox1.Controls.Add(this.btnZCMC);
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 223);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(268, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 3;
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Location = new System.Drawing.Point(97, 20);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(41, 12);
            this.labTitle.TabIndex = 2;
            this.labTitle.Text = "label1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(374, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加新项";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(374, 73);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(374, 44);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消添加";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(374, 188);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "关闭";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(374, 102);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "删除指定项";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnQLFS
            // 
            this.btnQLFS.Location = new System.Drawing.Point(16, 190);
            this.btnQLFS.Name = "btnQLFS";
            this.btnQLFS.Size = new System.Drawing.Size(75, 23);
            this.btnQLFS.TabIndex = 1;
            this.btnQLFS.Text = "清理方式";
            this.btnQLFS.UseVisualStyleBackColor = true;
            this.btnQLFS.Click += new System.EventHandler(this.btnQLFS_Click);
            // 
            // btnJLDW
            // 
            this.btnJLDW.Location = new System.Drawing.Point(16, 165);
            this.btnJLDW.Name = "btnJLDW";
            this.btnJLDW.Size = new System.Drawing.Size(75, 23);
            this.btnJLDW.TabIndex = 1;
            this.btnJLDW.Text = "计量单位";
            this.btnJLDW.UseVisualStyleBackColor = true;
            this.btnJLDW.Click += new System.EventHandler(this.btnJLDW_Click);
            // 
            // btnBGRY
            // 
            this.btnBGRY.Location = new System.Drawing.Point(16, 140);
            this.btnBGRY.Name = "btnBGRY";
            this.btnBGRY.Size = new System.Drawing.Size(75, 23);
            this.btnBGRY.TabIndex = 1;
            this.btnBGRY.Text = "保管人员";
            this.btnBGRY.UseVisualStyleBackColor = true;
            this.btnBGRY.Click += new System.EventHandler(this.btnBGRY_Click);
            // 
            // btnCFDD
            // 
            this.btnCFDD.Location = new System.Drawing.Point(16, 115);
            this.btnCFDD.Name = "btnCFDD";
            this.btnCFDD.Size = new System.Drawing.Size(75, 23);
            this.btnCFDD.TabIndex = 1;
            this.btnCFDD.Text = "存放地点";
            this.btnCFDD.UseVisualStyleBackColor = true;
            this.btnCFDD.Click += new System.EventHandler(this.btnCFDD_Click);
            // 
            // btnSYQK
            // 
            this.btnSYQK.Location = new System.Drawing.Point(16, 90);
            this.btnSYQK.Name = "btnSYQK";
            this.btnSYQK.Size = new System.Drawing.Size(75, 23);
            this.btnSYQK.TabIndex = 1;
            this.btnSYQK.Text = "使用情况";
            this.btnSYQK.UseVisualStyleBackColor = true;
            this.btnSYQK.Click += new System.EventHandler(this.btnSYQK_Click);
            // 
            // btnSYBM
            // 
            this.btnSYBM.Location = new System.Drawing.Point(16, 65);
            this.btnSYBM.Name = "btnSYBM";
            this.btnSYBM.Size = new System.Drawing.Size(75, 23);
            this.btnSYBM.TabIndex = 1;
            this.btnSYBM.Text = "使用部门";
            this.btnSYBM.UseVisualStyleBackColor = true;
            this.btnSYBM.Click += new System.EventHandler(this.btnSYBM_Click);
            // 
            // btnZJFS
            // 
            this.btnZJFS.Location = new System.Drawing.Point(16, 40);
            this.btnZJFS.Name = "btnZJFS";
            this.btnZJFS.Size = new System.Drawing.Size(75, 23);
            this.btnZJFS.TabIndex = 1;
            this.btnZJFS.Text = "增加方式";
            this.btnZJFS.UseVisualStyleBackColor = true;
            this.btnZJFS.Click += new System.EventHandler(this.btnZJFS_Click);
            // 
            // frmBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 233);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBaseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本资料管理";
            this.Load += new System.EventHandler(this.frmBaseInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbName;
        private System.Windows.Forms.Button btnZCMC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQLFS;
        private System.Windows.Forms.Button btnJLDW;
        private System.Windows.Forms.Button btnBGRY;
        private System.Windows.Forms.Button btnCFDD;
        private System.Windows.Forms.Button btnSYQK;
        private System.Windows.Forms.Button btnSYBM;
        private System.Windows.Forms.Button btnZJFS;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnCancel;

    }
}