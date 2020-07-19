using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.PR
{
	public class PR010SE : star.Public.BaseDialog
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox CkDa;
		private System.Windows.Forms.CheckBox CkSup;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.CheckBox ckPR;
		private System.Windows.Forms.CheckBox ckPR_Item;
		private System.ComponentModel.IContainer components = null;

		public PR010SE()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		public PR010SE(star.Public.Simple si)
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
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.CkDa = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.CkSup = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ckPR = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.ckPR_Item = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(96, 24);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(112, 20);
			this.dateTimePicker1.TabIndex = 2;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(96, 56);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(112, 20);
			this.dateTimePicker2.TabIndex = 3;
			// 
			// CkDa
			// 
			this.CkDa.Location = new System.Drawing.Point(8, 40);
			this.CkDa.Name = "CkDa";
			this.CkDa.Size = new System.Drawing.Size(16, 16);
			this.CkDa.TabIndex = 4;
			this.CkDa.Text = "CkDa";
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(96, 96);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(160, 21);
			this.comboBox1.TabIndex = 5;
			this.comboBox1.Text = "comboBox1";
			// 
			// CkSup
			// 
			this.CkSup.Location = new System.Drawing.Point(8, 96);
			this.CkSup.Name = "CkSup";
			this.CkSup.Size = new System.Drawing.Size(16, 16);
			this.CkSup.TabIndex = 6;
			this.CkSup.Text = "CkSup";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Start:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "To:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Supplier";
			// 
			// ckPR
			// 
			this.ckPR.Location = new System.Drawing.Point(8, 152);
			this.ckPR.Name = "ckPR";
			this.ckPR.Size = new System.Drawing.Size(16, 16);
			this.ckPR.TabIndex = 10;
			this.ckPR.Text = "checkBox1";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Product";
			// 
			// comboBox2
			// 
			this.comboBox2.Location = new System.Drawing.Point(96, 144);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(160, 21);
			this.comboBox2.TabIndex = 12;
			this.comboBox2.Text = "comboBox2";
			// 
			// ckPR_Item
			// 
			this.ckPR_Item.Location = new System.Drawing.Point(8, 192);
			this.ckPR_Item.Name = "ckPR_Item";
			this.ckPR_Item.Size = new System.Drawing.Size(16, 16);
			this.ckPR_Item.TabIndex = 13;
			this.ckPR_Item.Text = "checkBox2";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(32, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 24);
			this.label5.TabIndex = 14;
			this.label5.Text = "Product Category ";
			// 
			// comboBox3
			// 
			this.comboBox3.Location = new System.Drawing.Point(96, 192);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(160, 21);
			this.comboBox3.TabIndex = 15;
			this.comboBox3.Text = "comboBox3";
			// 
			// PR010SE
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 245);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ckPR_Item);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ckPR);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CkSup);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.CkDa);
			this.Controls.Add(this.dateTimePicker2);
			this.Controls.Add(this.dateTimePicker1);
			this.Name = "PR010SE";
			this.Controls.SetChildIndex(this.dateTimePicker1, 0);
			this.Controls.SetChildIndex(this.dateTimePicker2, 0);
			this.Controls.SetChildIndex(this.CkDa, 0);
			this.Controls.SetChildIndex(this.comboBox1, 0);
			this.Controls.SetChildIndex(this.CkSup, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.ckPR, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.comboBox2, 0);
			this.Controls.SetChildIndex(this.ckPR_Item, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.comboBox3, 0);
			this.ResumeLayout(false);

		}
		#endregion
		protected override void clickOk(object sender, EventArgs e)
		{
			String s="";
			if(this.CkDa.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PUR_DATE>=#"+this.dateTimePicker1.Text+"# and PUR_DATE<=#"+this.dateTimePicker2.Text+"#";
			}
			if(this.CkSup.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " SUP_ID="+Convert.ToInt32(this.comboBox1.SelectedValue);

			}
			if(this.ckPR.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_ID="+Convert.ToInt32(this.comboBox2.SelectedValue);

			}
			if(this.ckPR_Item.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_IT_ID="+Convert.ToInt32(this.comboBox3.SelectedValue);

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
			this.comboBox1.DataSource=fa.DsMast.Tables["Sup"];
			this.comboBox1.DisplayMember ="Sup_Name";
			this.comboBox1.ValueMember="Auto_No";
			this.comboBox2.DataSource =fa.DsMast.Tables["PR"];
			this.comboBox2.DisplayMember ="FNAME";
			this.comboBox2.ValueMember="AUTO_NO";
			this.comboBox3.DataSource =fa.DsMast.Tables["PR_Item"];
			this.comboBox3.DisplayMember ="IT_NAME";
			this.comboBox3.ValueMember="Auto_No";

		}
		


	}
}

