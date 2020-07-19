using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.SA
{
	public class SA020 : star.Public.BaseDialog
	{
	
		protected System.ComponentModel.IContainer components = null;
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.Panel panel4;
		protected System.Windows.Forms.ComboBox comboBox3;
		protected System.Windows.Forms.ComboBox comboBox2;
		protected System.Windows.Forms.ComboBox comboBox1;
		protected System.Windows.Forms.DateTimePicker dateTimePicker2;
		protected System.Windows.Forms.DateTimePicker dateTimePicker1;
		protected System.Windows.Forms.Panel panel3;
		protected System.Windows.Forms.Label label5;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.Label label3;
		protected System.Windows.Forms.Label label2;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.Panel panel2;
		protected System.Windows.Forms.CheckBox ckIt;
		protected System.Windows.Forms.CheckBox ckPr;
		protected System.Windows.Forms.CheckBox ckCus;
		protected System.Windows.Forms.CheckBox ckDa;
		protected String repName;
		protected Public.BasicRP  w;
		public SA020()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.repName=Application.StartupPath.ToString ()+"\\SA020.rpt";
			// TODO: Add any initialization after the InitializeComponent call
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ckIt = new System.Windows.Forms.CheckBox();
			this.ckPr = new System.Windows.Forms.CheckBox();
			this.ckCus = new System.Windows.Forms.CheckBox();
			this.ckDa = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.DockPadding.All = 5;
			this.panel1.Location = new System.Drawing.Point(8, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(256, 120);
			this.panel1.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.comboBox3);
			this.panel4.Controls.Add(this.comboBox2);
			this.panel4.Controls.Add(this.comboBox1);
			this.panel4.Controls.Add(this.dateTimePicker2);
			this.panel4.Controls.Add(this.dateTimePicker1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.DockPadding.All = 1;
			this.panel4.Location = new System.Drawing.Point(93, 5);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(156, 108);
			this.panel4.TabIndex = 2;
			// 
			// comboBox3
			// 
			this.comboBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox3.Location = new System.Drawing.Point(1, 83);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(154, 21);
			this.comboBox3.TabIndex = 4;
			this.comboBox3.Text = "comboBox3";
			// 
			// comboBox2
			// 
			this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox2.Location = new System.Drawing.Point(1, 62);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(154, 21);
			this.comboBox2.TabIndex = 3;
			this.comboBox2.Text = "comboBox2";
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.Location = new System.Drawing.Point(1, 41);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(154, 21);
			this.comboBox1.TabIndex = 2;
			this.comboBox1.Text = "comboBox1";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(1, 21);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(154, 20);
			this.dateTimePicker2.TabIndex = 1;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(1, 1);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(154, 20);
			this.dateTimePicker1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.DockPadding.All = 1;
			this.panel3.Location = new System.Drawing.Point(21, 5);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(72, 108);
			this.panel3.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(1, 81);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 31);
			this.label5.TabIndex = 4;
			this.label5.Text = "Product Category";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(1, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Product";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(1, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Customer";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(1, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "End";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(1, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Start";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ckIt);
			this.panel2.Controls.Add(this.ckPr);
			this.panel2.Controls.Add(this.ckCus);
			this.panel2.Controls.Add(this.ckDa);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.DockPadding.All = 1;
			this.panel2.Location = new System.Drawing.Point(5, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(16, 108);
			this.panel2.TabIndex = 0;
			// 
			// ckIt
			// 
			this.ckIt.Dock = System.Windows.Forms.DockStyle.Top;
			this.ckIt.Location = new System.Drawing.Point(1, 80);
			this.ckIt.Name = "ckIt";
			this.ckIt.Size = new System.Drawing.Size(14, 20);
			this.ckIt.TabIndex = 4;
			this.ckIt.Text = "checkBox5";
			// 
			// ckPr
			// 
			this.ckPr.Dock = System.Windows.Forms.DockStyle.Top;
			this.ckPr.Location = new System.Drawing.Point(1, 60);
			this.ckPr.Name = "ckPr";
			this.ckPr.Size = new System.Drawing.Size(14, 20);
			this.ckPr.TabIndex = 3;
			this.ckPr.Text = "checkBox4";
			// 
			// ckCus
			// 
			this.ckCus.Dock = System.Windows.Forms.DockStyle.Top;
			this.ckCus.Location = new System.Drawing.Point(1, 40);
			this.ckCus.Name = "ckCus";
			this.ckCus.Size = new System.Drawing.Size(14, 20);
			this.ckCus.TabIndex = 1;
			this.ckCus.Text = "checkBox2";
			// 
			// ckDa
			// 
			this.ckDa.Dock = System.Windows.Forms.DockStyle.Top;
			this.ckDa.Location = new System.Drawing.Point(1, 1);
			this.ckDa.Name = "ckDa";
			this.ckDa.Size = new System.Drawing.Size(14, 39);
			this.ckDa.TabIndex = 0;
			this.ckDa.Text = "checkBox1";
			// 
			// SA020
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 149);
			this.Controls.Add(this.panel1);
			this.Name = "SA020";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			
			w=new star.Public.BasicRP();
			w.MdiParent =this.Owner;
			w.DsMain =new System.Data.DataSet ();
			
			this.dateTimePicker1.Value=DateTime.Now;
			this.dateTimePicker2.Value=DateTime.Now;
			
			System.Data.OleDb.OleDbDataAdapter DaTm=new System.Data.OleDb.OleDbDataAdapter ();
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from Customer ",((wmMain)w.MdiParent).db);
			DaTm.Fill(w.DsMain,"Cus");
			this.comboBox1.DataSource=w.DsMain.Tables["Cus"].DefaultView ;
			w.DsMain.Tables["Cus"].DefaultView.Sort="Cus_Name";
			this.comboBox1.DisplayMember ="Cus_Name";
			this.comboBox1.ValueMember="Auto_No";

			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR011",((wmMain)w.MdiParent).db);
			DaTm.Fill(w.DsMain,"PR");
			this.comboBox2.DataSource=w.DsMain.Tables["PR"].DefaultView;
			w.DsMain.Tables["PR"].DefaultView.Sort="FNAME";
			this.comboBox2.DisplayMember ="FNAME";
			this.comboBox2.ValueMember="AUTO_NO";
			
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR_Item",((wmMain)w.MdiParent).db);
			DaTm.Fill(w.DsMain,"PR_Item");
			this.comboBox3.DataSource =w.DsMain.Tables["PR_Item"].DefaultView ;
			w.DsMain.Tables["PR_Item"].DefaultView.Sort ="IT_NAME";
			this.comboBox3.DisplayMember ="IT_NAME";
			this.comboBox3.ValueMember="Auto_No";
		}

		protected override void clickOk(object sender, EventArgs e)
		{
			String basSql ="select * from SA010";
			String s="";
			if(this.ckDa.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " SA_DATE>=#"+this.dateTimePicker1.Text+"# and SA_DATE<=#"+this.dateTimePicker2.Text+"#";
			}
			if(this.ckCus.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " CUS_ID="+Convert.ToInt32(this.comboBox1.SelectedValue);

			}
			if(this.ckPr.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_ID="+Convert.ToInt32(this.comboBox2.SelectedValue);

			}
			if(this.ckIt.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PR_IT_ID="+Convert.ToInt32(this.comboBox3.SelectedValue);

			}
			//MessageBox.Show (s);
			if(s.Trim().Length>0)basSql=basSql+" where "+s;
			try
			{
				//w.DsMain.Dispose ();
				//w.DsMain=null;
				//w.DsMain=new System.Data.DataSet ();
				w.Show ();
				//CrystalDecisions.CrystalReports.Engine.ReportDocument r=new CrystalDecisions.CrystalReports.Engine.ReportDocument ();
				//	r.Load ("C:\\Program Files\\Microsoft Visual Studio .NET 2003\\Crystal Reports\\Samples\\Reports\\General Business\\Income Statement.rpt");
				star.RP.SA020 r=new star.RP.SA020 ();
				//r.Load (this.repName);
				System.Data.OleDb.OleDbDataAdapter Da=new System.Data.OleDb.OleDbDataAdapter ();
				Da.SelectCommand =new System.Data.OleDb.OleDbCommand(basSql +" order by SA_DATE DESC",((wmMain)w.MdiParent).db);
				Da.Fill(w.DsMain,"SA010");
				//	this.dataGrid1.SetDataBinding(w.DsMain,"Mast");
				r.Database.Tables [0].SetDataSource (w.DsMain);
				w.crystalReportViewer1.ReportSource =r;
				this.Close ();
			}
			catch(System.Exception  ex)
			{	
				MessageBox.Show (ex.Message.ToString ());
			}
			
			//	
		}

	}
}

