using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.IN
{
	public class IN020 : star.Public.BaseDialog
	{
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.Panel panel4;
		protected System.Windows.Forms.DateTimePicker dateTimePicker2;
		protected System.Windows.Forms.DateTimePicker dateTimePicker1;
		protected System.Windows.Forms.Panel panel3;
		protected System.Windows.Forms.Label label2;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.Panel panel2;
		protected System.Windows.Forms.CheckBox ckDa;
		private System.ComponentModel.IContainer components = null;
		//
		//1. Identify
		//
		protected String repName;
		protected Public.BasicRP  w;
		//
		//2.Initial
		//

		public IN020()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.repName=Application.StartupPath.ToString ()+"\\PR020.rpt";

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
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
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
			this.panel1.Location = new System.Drawing.Point(8, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(256, 64);
			this.panel1.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.dateTimePicker2);
			this.panel4.Controls.Add(this.dateTimePicker1);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.DockPadding.All = 1;
			this.panel4.Location = new System.Drawing.Point(93, 5);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(156, 52);
			this.panel4.TabIndex = 2;
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
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.DockPadding.All = 1;
			this.panel3.Location = new System.Drawing.Point(21, 5);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(72, 52);
			this.panel3.TabIndex = 1;
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
			this.panel2.Controls.Add(this.ckDa);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.DockPadding.All = 1;
			this.panel2.Location = new System.Drawing.Point(5, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(16, 52);
			this.panel2.TabIndex = 0;
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
			// IN020
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 101);
			this.Controls.Add(this.panel1);
			this.Name = "IN020";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		//
		//3.Load database
		//
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			
			w=new star.Public.BasicRP();
			w.MdiParent =this.Owner;
			w.DsMain =new System.Data.DataSet ();
			
			this.dateTimePicker1.Value=DateTime.Now;
			this.dateTimePicker2.Value=DateTime.Now;
			
		}
		//
		//4. Search Records
		//

		protected override void clickOk(object sender, EventArgs e)
		{
			String basSql ="select * from IN020";
			String s="";
			if(this.ckDa.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " C_DATE>=#"+this.dateTimePicker1.Text+"# and C_DATE<=#"+this.dateTimePicker2.Text+"#";
			}
			if(s.Trim().Length>0)basSql=basSql+" where "+s;
			try
			{
				w.Show ();
				//CrystalDecisions.CrystalReports.Engine.ReportDocument r=new CrystalDecisions.CrystalReports.Engine.ReportDocument ();
				//r.Load (this.repName);
				star.RP.IN020 r=new star.RP.IN020 ();
				System.Data.OleDb.OleDbDataAdapter Da=new System.Data.OleDb.OleDbDataAdapter ();
				Da.SelectCommand =new System.Data.OleDb.OleDbCommand(basSql,((wmMain)w.MdiParent).db);
				Da.Fill(w.DsMain,"IN020");
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

