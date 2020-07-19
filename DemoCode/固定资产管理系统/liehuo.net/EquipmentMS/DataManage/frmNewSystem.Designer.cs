namespace EquipmentMS.DataManage
{
    partial class frmNewSystem
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkBase = new System.Windows.Forms.CheckBox();
            this.chkOperation = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "                                                              注意：系统数据初始化，根据用户选择进行" +
                "系统全部清理或清理业务数据，在系统数据初始化前，请事先作好备份工作，否则造成大量数据丢失带来不必要的损失。";
            // 
            // chkBase
            // 
            this.chkBase.AutoSize = true;
            this.chkBase.ForeColor = System.Drawing.Color.Blue;
            this.chkBase.Location = new System.Drawing.Point(154, 77);
            this.chkBase.Name = "chkBase";
            this.chkBase.Size = new System.Drawing.Size(72, 16);
            this.chkBase.TabIndex = 1;
            this.chkBase.Text = "全部清理";
            this.chkBase.UseVisualStyleBackColor = true;
            // 
            // chkOperation
            // 
            this.chkOperation.AutoSize = true;
            this.chkOperation.ForeColor = System.Drawing.Color.Blue;
            this.chkOperation.Location = new System.Drawing.Point(39, 77);
            this.chkOperation.Name = "chkOperation";
            this.chkOperation.Size = new System.Drawing.Size(96, 16);
            this.chkOperation.TabIndex = 2;
            this.chkOperation.Text = "清理业务数据";
            this.chkOperation.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(154, 118);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(77, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "初始化...";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(246, 118);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "关　闭";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmNewSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 153);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkOperation);
            this.Controls.Add(this.chkBase);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmNewSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统数据初始化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBase;
        private System.Windows.Forms.CheckBox chkOperation;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
    }
}