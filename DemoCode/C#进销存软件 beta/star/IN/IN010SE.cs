using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.IN
{
	public class IN010SE : star.Public.BaseDialog
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.ComponentModel.IContainer components = null;

		public IN010SE()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		public IN010SE(star.Public.Simple si)
		{
			this.fa=si;	
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(16, 24);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "checkBox1";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(8, 96);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(16, 24);
			this.checkBox2.TabIndex = 3;
			this.checkBox2.Text = "checkBox2";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(8, 136);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(16, 24);
			this.checkBox3.TabIndex = 4;
			this.checkBox3.Text = "checkBox3";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 5;
			this.label1.Text = "FROM";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 6;
			this.label2.Text = "TO";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(96, 24);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(136, 20);
			this.dateTimePicker1.TabIndex = 7;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(96, 56);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(136, 20);
			this.dateTimePicker2.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 24);
			this.label3.TabIndex = 9;
			this.label3.Text = "Product";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(96, 96);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(176, 21);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.Text = "comboBox1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 40);
			this.label4.TabIndex = 11;
			this.label4.Text = "Product Category";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox2
			// 
			this.comboBox2.Location = new System.Drawing.Point(96, 144);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(176, 21);
			this.comboBox2.TabIndex = 12;
			this.comboBox2.Text = "comboBox2";
			// 
			// IN010SE
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 225);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dateTimePicker2);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Name = "IN010SE";
			this.Controls.SetChildIndex(this.checkBox1, 0);
			this.Controls.SetChildIndex(this.checkBox2, 0);
			this.Controls.SetChildIndex(this.checkBox3, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.dateTimePicker1, 0);
			this.Controls.SetChildIndex(this.dateTimePicker2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.comboBox1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.comboBox2, 0);
			this.ResumeLayout(false);

		}
		#endregion
		protected override void clickOk(object sender, EventArgs e)
		{
			String s="";
			if(this.checkBox1.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " IN_DATE>=#"+this.dateTimePicker1.Text+"# and IN_DATE<=#"+this.dateTimePicker2.Text+"#";
			}
			/*if(this.checkBox1.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " CUS_ID="+Convert.ToInt32(this.comboBox1.SelectedValue);

			}*/
			if(this.checkBox2.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_ID="+Convert.ToInt32(this.comboBox1.SelectedValue);

			}
			if(this.checkBox3.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_IT_ID="+Convert.ToInt32(this.comboBox2.SelectedValue);

			}
			//MessageBox.Show (s);
			if(s.Trim().Length>0)this.fa.Filter (" where "+s);
			else this.fa.Filter("");
			this.Close();

		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.dateTimePicker1.Value=DateTime.Now;
			this.dateTimePicker2.Value=DateTime.Now;
			//this.comboBox1.DataSource=fa.DsMast.Tables["Cus"];
			//this.comboBox1.DisplayMember ="Cus_Name";
			//this.comboBox1.ValueMember="Auto_No";
			this.comboBox1.DataSource =fa.DsMast.Tables["PR"];
			this.comboBox1.DisplayMember ="FNAME";
			this.comboBox1.ValueMember="AUTO_NO";
			this.comboBox2.DataSource =fa.DsMast.Tables["PR_Item"];
			this.comboBox2.DisplayMember ="IT_NAME";
			this.comboBox2.ValueMember="Auto_No";

		}
	}
}

