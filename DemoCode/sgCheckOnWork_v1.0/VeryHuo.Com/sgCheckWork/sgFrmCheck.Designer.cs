namespace sgFrmCheckWork
{
    partial class sgFrmCheck
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
            this.lbl_DateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_DateWeek = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Info = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_NumOrName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btn_Check = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labNote = new DevComponents.DotNetBar.LabelX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_DateTime
            // 
            this.lbl_DateTime.AutoSize = true;
            this.lbl_DateTime.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbl_DateTime.ForeColor = System.Drawing.Color.Red;
            this.lbl_DateTime.Location = new System.Drawing.Point(30, 30);
            this.lbl_DateTime.Name = "lbl_DateTime";
            this.lbl_DateTime.Size = new System.Drawing.Size(0, 17);
            this.lbl_DateTime.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_DateWeek
            // 
            this.lbl_DateWeek.AutoSize = true;
            this.lbl_DateWeek.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbl_DateWeek.ForeColor = System.Drawing.Color.Red;
            this.lbl_DateWeek.Location = new System.Drawing.Point(193, 30);
            this.lbl_DateWeek.Name = "lbl_DateWeek";
            this.lbl_DateWeek.Size = new System.Drawing.Size(0, 17);
            this.lbl_DateWeek.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Info);
            this.groupBox1.Controls.Add(this.lbl_DateWeek);
            this.groupBox1.Controls.Add(this.lbl_DateTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 104);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前时间";
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbl_Info.Location = new System.Drawing.Point(32, 67);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(50, 17);
            this.lbl_Info.TabIndex = 3;
            this.lbl_Info.Text = "lbl_Info";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_NumOrName);
            this.groupBox2.Controls.Add(this.btn_Check);
            this.groupBox2.Location = new System.Drawing.Point(12, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "请在下面的输入框中输入您的工号或姓名,之后按回车即可考勤！";
            // 
            // txt_NumOrName
            // 
            // 
            // 
            // 
            this.txt_NumOrName.Border.Class = "TextBoxBorder";
            this.txt_NumOrName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_NumOrName.Location = new System.Drawing.Point(33, 44);
            this.txt_NumOrName.Name = "txt_NumOrName";
            this.txt_NumOrName.Size = new System.Drawing.Size(359, 21);
            this.txt_NumOrName.TabIndex = 2;
            // 
            // btn_Check
            // 
            this.btn_Check.Location = new System.Drawing.Point(328, 44);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Size = new System.Drawing.Size(0, 0);
            this.btn_Check.TabIndex = 1;
            this.btn_Check.UseVisualStyleBackColor = true;
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labNote);
            this.groupBox3.Location = new System.Drawing.Point(12, 257);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 75);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "考勤相关";
            // 
            // labNote
            // 
            // 
            // 
            // 
            this.labNote.BackgroundStyle.Class = "";
            this.labNote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labNote.Location = new System.Drawing.Point(33, 33);
            this.labNote.Name = "labNote";
            this.labNote.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labNote.Size = new System.Drawing.Size(359, 23);
            this.labNote.TabIndex = 0;
            // 
            // sgFrmCheck
            // 
            this.AcceptButton = this.btn_Check;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 350);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "sgFrmCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "世广集团考勤系统";
            this.Load += new System.EventHandler(this.sgFrmCheck_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_DateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_DateWeek;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Check;
        private System.Windows.Forms.Label lbl_Info;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_NumOrName;
        private DevComponents.DotNetBar.LabelX labNote;
    }
}

