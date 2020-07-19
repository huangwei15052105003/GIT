using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.SA
{
	public class SA040 : star.SA.SA020
	{
		//private System.ComponentModel.IContainer components = null;

		public SA040()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.repName=Application.StartupPath.ToString ()+"\\SA032.rpt";
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
			components = new System.ComponentModel.Container();
		}
		#endregion
		protected override void clickOk(object sender, EventArgs e)
		{
			String basSql ="select * from SA030";
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
				star.RP.SA040 r=new star.RP.SA040 ();
				//r.Load (this.repName);
				System.Data.OleDb.OleDbDataAdapter Da=new System.Data.OleDb.OleDbDataAdapter ();
				Da.SelectCommand =new System.Data.OleDb.OleDbCommand(basSql +" order by SA_DATE DESC",((wmMain)w.MdiParent).db);
				Da.Fill(w.DsMain,"SA030");
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

