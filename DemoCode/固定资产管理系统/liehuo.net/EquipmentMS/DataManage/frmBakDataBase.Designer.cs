namespace EquipmentMS.DataManage
{
    partial class frmBakDataBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBakDataBase));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnBak = new System.Windows.Forms.Button();
            this.btnRes = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkBak = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(551, 204);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bakup.ico");
            // 
            // btnBak
            // 
            this.btnBak.Location = new System.Drawing.Point(302, 209);
            this.btnBak.Name = "btnBak";
            this.btnBak.Size = new System.Drawing.Size(75, 23);
            this.btnBak.TabIndex = 1;
            this.btnBak.Text = "数据备份";
            this.btnBak.UseVisualStyleBackColor = true;
            this.btnBak.Click += new System.EventHandler(this.btnBak_Click);
            // 
            // btnRes
            // 
            this.btnRes.Location = new System.Drawing.Point(383, 209);
            this.btnRes.Name = "btnRes";
            this.btnRes.Size = new System.Drawing.Size(75, 23);
            this.btnRes.TabIndex = 1;
            this.btnRes.Text = "数据恢复";
            this.btnRes.UseVisualStyleBackColor = true;
            this.btnRes.Click += new System.EventHandler(this.btnRes_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(464, 209);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "关　闭";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkBak
            // 
            this.chkBak.AutoSize = true;
            this.chkBak.ForeColor = System.Drawing.Color.Blue;
            this.chkBak.Location = new System.Drawing.Point(12, 213);
            this.chkBak.Name = "chkBak";
            this.chkBak.Size = new System.Drawing.Size(240, 16);
            this.chkBak.TabIndex = 2;
            this.chkBak.Text = "数据备份过程中，清理以前所有的备份。";
            this.chkBak.UseVisualStyleBackColor = true;
            // 
            // frmBakDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 238);
            this.Controls.Add(this.chkBak);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRes);
            this.Controls.Add(this.btnBak);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Name = "frmBakDataBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统数据备份与恢复";
            this.Load += new System.EventHandler(this.frmBakDataBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnBak;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkBak;
    }
}