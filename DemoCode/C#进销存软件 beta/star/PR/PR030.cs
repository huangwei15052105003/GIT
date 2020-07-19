using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.PR
{
	public class PR030 : star.PR.PRO20
	{
		//private System.ComponentModel.IContainer components = null;

		public PR030()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.repName=Application.StartupPath.ToString ()+"\\PR031.rpt";

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
			// 
			// PR031
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 213);
			this.Name = "PR031";

		}
		#endregion
		//
		//4. Search Records
		//

		protected override void clickOk(object sender, EventArgs e)
		{
			String basSql ="select * from PR030";
			String s="";
			if(this.ckDa.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " PUR_DATE>=#"+this.dateTimePicker1.Text+"# and PUR_DATE<=#"+this.dateTimePicker2.Text+"#";
			}
			if(this.ckSup.Checked)
			{
				s=s.Trim();
				if(s.Length>0) s=s+ " and ";
				s=s+ " SUP_ID="+Convert.ToInt32(this.comboBox1.SelectedValue);

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
			if(s.Trim().Length>0)basSql=basSql+" where "+s;
			try
			{
				w.Show ();
				//CrystalDecisions.CrystalReports.Engine.ReportDocument r=new CrystalDecisions.CrystalReports.Engine.ReportDocument ();
				//r.Load (this.repName);
				star.RP.PR030 r=new star.RP.PR030 ();
				System.Data.OleDb.OleDbDataAdapter Da=new System.Data.OleDb.OleDbDataAdapter ();
				Da.SelectCommand =new System.Data.OleDb.OleDbCommand(basSql +" order by PUR_DATE DESC",((wmMain)w.MdiParent).db);
				Da.Fill(w.DsMain,"PR030");
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

