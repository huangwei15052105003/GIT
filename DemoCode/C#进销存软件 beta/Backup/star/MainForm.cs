using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace star
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class wmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public  System.Data.OleDb.OleDbConnection db ;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem3;
		private star.BA.BA010  WmPre1;
		private star.BA.BA020  BA0201;
		private star.BA.BA030  BA0301;
		private star.BA.BA040  BA0401;
		private star.BA.BA050  BA0501;
		private star.PR.PR010  PR0101;
		private star.SA.SA010  SA0101;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private star.BA.BA060  BA0601;
		private System.Windows.Forms.MenuItem menuItem23;
		private star.IN.IN010  IN0101;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem30;
		public wmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			db=new System.Data.OleDb.OleDbConnection();


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem27 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem29 = new System.Windows.Forms.MenuItem();
			this.menuItem28 = new System.Windows.Forms.MenuItem();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			this.menuItem30 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem11,
																					  this.menuItem10,
																					  this.menuItem12,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem4,
																					  this.menuItem6,
																					  this.menuItem5,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem23});
			this.menuItem1.Text = "Basic Info";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "BA010 Customers";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "BA020 Suppliers";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "BA030 Product Items";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "BA040 Products";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "BA050 Reasons of Store Changing";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "BA060 Expense Types";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 6;
			this.menuItem23.Text = "TEST";
			this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem9,
																					   this.menuItem14,
																					   this.menuItem15,
																					   this.menuItem25,
																					   this.menuItem27});
			this.menuItem11.Text = "Purchase";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "PR010 Purchase Orders";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click_1);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 1;
			this.menuItem14.Text = "-";
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 2;
			this.menuItem15.Text = "PR020 Purchase Detail Report";
			this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 3;
			this.menuItem25.Text = "PR030 Group Payment by Purchase";
			this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
			// 
			// menuItem27
			// 
			this.menuItem27.Index = 4;
			this.menuItem27.Text = "PR040 Group Payments by Supplier";
			this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem16,
																					   this.menuItem29,
																					   this.menuItem28,
																					   this.menuItem26,
																					   this.menuItem30});
			this.menuItem10.Text = "Sales";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 0;
			this.menuItem16.Text = "SA010 Sale Orders";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem29
			// 
			this.menuItem29.Index = 1;
			this.menuItem29.Text = "-";
			// 
			// menuItem28
			// 
			this.menuItem28.Index = 2;
			this.menuItem28.Text = "SA020 Detail Report";
			this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
			// 
			// menuItem26
			// 
			this.menuItem26.Index = 3;
			this.menuItem26.Text = "SA030 Group Collection by Sale";
			this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click_1);
			// 
			// menuItem30
			// 
			this.menuItem30.Index = 4;
			this.menuItem30.Text = "SA040 Gronewup Collection by Customer";
			this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 3;
			this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem19,
																					   this.menuItem20,
																					   this.menuItem13,
																					   this.menuItem18});
			this.menuItem12.Text = "Inventory";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 0;
			this.menuItem19.Text = "IN010 Inventory Changes";
			this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 1;
			this.menuItem20.Text = "-";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 2;
			this.menuItem13.Text = "IN020 Inventory Change List";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 3;
			this.menuItem18.Text = "IN030 Check Inventory";
			this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.MdiList = true;
			this.menuItem3.Text = "Window";
			// 
			// wmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 186);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "wmMain";
			this.Text = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.wmMain_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new wmMain());
		}

		private void wmMain_Load(object sender, System.EventArgs e)
		{
			this.Text="Main Window";
			db.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=" + Application.StartupPath + @"\star.mdb;Mode=Share Deny None;Extended Properties='';Jet OLEDB:System database='';Jet OLEDB:Registry Path='';Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";
			try{
				 db.Open();
			}catch( System.Exception ex) {
				MessageBox.Show("Failed to connect to data source, details info:"+ex.Message );
			}finally{
				db.Close();
			}
        }
// veryhuo,com ×î»ðÈí¼þÕ¾
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if(WmPre1==null)
				WmPre1=new BA.BA010();
			else if(WmPre1.MdiParent==null)
				WmPre1=new BA.BA010();
			WmPre1.MdiParent=this;
			WmPre1.Show ();

		}

		private void oleDbConnection1_InfoMessage(object sender, System.Data.OleDb.OleDbInfoMessageEventArgs e)
		{
		
		}

		private void dbCon_InfoMessage(object sender, System.Data.OleDb.OleDbInfoMessageEventArgs e)
		{
		
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			if(BA0201==null)
				BA0201=new BA.BA020();
			else if(BA0201.MdiParent==null)
				BA0201=new BA.BA020();
			BA0201.MdiParent=this;
			BA0201.Show ();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			if(BA0601==null)
				BA0601=new BA.BA060();
			else if(BA0601.MdiParent==null)
				BA0601=new BA.BA060();
			BA0601.MdiParent=this;
			BA0601.Show ();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			if(BA0301==null)
				BA0301=new BA.BA030();
			else if(BA0301.MdiParent==null)
				BA0301=new BA.BA030();
			BA0301.MdiParent=this;
			BA0301.Show ();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			if(BA0401==null)
				BA0401=new BA.BA040();
			else if(BA0401.MdiParent==null)
				BA0401=new BA.BA040();
			BA0401.MdiParent=this;
			BA0401.Show ();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			(new Public.SimpleList()).Show();

		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			if(BA0501==null)
				BA0501=new BA.BA050();
			else if(BA0501.MdiParent==null)
				BA0501=new BA.BA050();
			BA0501.MdiParent=this;
			BA0501.Show ();
		
		}

		private void menuItem9_Click_1(object sender, System.EventArgs e)
		{
			if(PR0101==null)
				PR0101=new PR.PR010();
			else if(PR0101.MdiParent==null)
				PR0101=new PR.PR010();
			PR0101.MdiParent=this;
			PR0101.Show ();
		
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			new star.PR.PRO20 ().ShowDialog (this);
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			if(SA0101==null)
				SA0101=new SA.SA010();
			else if(SA0101.MdiParent==null)
				SA0101=new SA.SA010();
			SA0101.MdiParent=this;
			SA0101.Show ();
			
			
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			if(IN0101==null)
				IN0101=new IN.IN010();
			else if(IN0101.MdiParent==null)
				IN0101=new IN.IN010();
			IN0101.MdiParent=this;
			IN0101.Show ();
			
		}

		private void menuItem23_Click(object sender, System.EventArgs e)
		{
			new star.test().ShowDialog (this);
		}

		private void menuItem24_Click(object sender, System.EventArgs e)
		{
			/*if(PR0311==null)
				PR0311=new PR.PR031();
			else if(PR0311.MdiParent==null)
				PR0311=new PR.PR031();
			PR0311.MdiParent=this;
			PR0311.Show ();
			*/
			new PR.PR030 ().ShowDialog(this);
		}

		private void menuItem26_Click(object sender, System.EventArgs e)
		{
			new PR.PR040().ShowDialog(this);
		}

		private void menuItem28_Click(object sender, System.EventArgs e)
		{
			new SA.SA020().ShowDialog(this);
		}

		private void menuItem31_Click(object sender, System.EventArgs e)
		{
			//new SA.SA031().ShowDialog(this);
		}

		private void menuItem32_Click(object sender, System.EventArgs e)
		{
			//new SA.SA032().ShowDialog(this);
		}

		private void menuItem21_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			new star.IN.IN020().ShowDialog(this);
		}

		private void menuItem25_Click(object sender, System.EventArgs e)
		{
			new PR.PR030 ().ShowDialog(this);
		}

		private void menuItem27_Click(object sender, System.EventArgs e)
		{
			new PR.PR040 ().ShowDialog(this);
		}

		private void menuItem26_Click_1(object sender, System.EventArgs e)
		{
			new SA.SA030().ShowDialog(this);
		}

		private void menuItem30_Click(object sender, System.EventArgs e)
		{
			new SA.SA040().ShowDialog(this);
		}

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			new IN.IN030().ShowDialog(this);
		}

		

	}
}
