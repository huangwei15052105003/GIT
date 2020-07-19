using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace star.Public
{
	/// <summary>
	/// Summary description for simple.
	/// </summary>
	public class Simple : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ToolBar toolBar1;
		public System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.ToolBarButton btExit;
		public System.Windows.Forms.ToolBarButton btNew;
		public System.Windows.Forms.ToolBarButton btEdit;
		public System.Windows.Forms.ToolBarButton btSave;
		public System.Windows.Forms.ToolBarButton btDel;
		public System.Windows.Forms.ToolBarButton btRefresh;
		public System.Windows.Forms.ToolBarButton btFirst;
		public System.Windows.Forms.ToolBarButton btPre;
		public System.Windows.Forms.ToolBarButton btNext;
		public System.Windows.Forms.ToolBarButton btLast;
		protected System.Windows.Forms.ToolBarButton btSearch;
		protected String BasSql;
		protected String SelSql;
		#region Database Component
		public CurrencyManager BindMast;
		public System.Data.OleDb.OleDbDataAdapter DaMast;
		public System.Data.DataSet DsMast;
		#endregion

		public Simple()
		{
			//
			// Required for Windows Form Designer support
			//
			this.DaMast=new System.Data.OleDb.OleDbDataAdapter ();
			DsMast=new System.Data.DataSet ();
			InitializeComponent();

			

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
				if(components != null)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Simple));
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.btExit = new System.Windows.Forms.ToolBarButton();
			this.btNew = new System.Windows.Forms.ToolBarButton();
			this.btEdit = new System.Windows.Forms.ToolBarButton();
			this.btSave = new System.Windows.Forms.ToolBarButton();
			this.btDel = new System.Windows.Forms.ToolBarButton();
			this.btRefresh = new System.Windows.Forms.ToolBarButton();
			this.btFirst = new System.Windows.Forms.ToolBarButton();
			this.btPre = new System.Windows.Forms.ToolBarButton();
			this.btNext = new System.Windows.Forms.ToolBarButton();
			this.btLast = new System.Windows.Forms.ToolBarButton();
			this.btSearch = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.btExit,
																						this.btNew,
																						this.btEdit,
																						this.btSave,
																						this.btDel,
																						this.btRefresh,
																						this.btFirst,
																						this.btPre,
																						this.btNext,
																						this.btLast,
																						this.btSearch});
			this.toolBar1.Divider = false;
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(368, 40);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.Wrappable = false;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// btExit
			// 
			this.btExit.ImageIndex = 0;
			this.btExit.Text = "Exit";
			// 
			// btNew
			// 
			this.btNew.ImageIndex = 2;
			this.btNew.Text = "New";
			// 
			// btEdit
			// 
			this.btEdit.ImageIndex = 3;
			this.btEdit.Text = "Edit";
			// 
			// btSave
			// 
			this.btSave.ImageIndex = 4;
			this.btSave.Text = "Save";
			// 
			// btDel
			// 
			this.btDel.ImageIndex = 5;
			this.btDel.Text = "Delete";
			// 
			// btRefresh
			// 
			this.btRefresh.ImageIndex = 6;
			this.btRefresh.Text = "Refresh";
			// 
			// btFirst
			// 
			this.btFirst.ImageIndex = 7;
			this.btFirst.Text = "First";
			// 
			// btPre
			// 
			this.btPre.ImageIndex = 8;
			this.btPre.Text = "Pre";
			// 
			// btNext
			// 
			this.btNext.ImageIndex = 9;
			this.btNext.Text = "Next";
			// 
			// btLast
			// 
			this.btLast.ImageIndex = 10;
			this.btLast.Text = "Last";
			// 
			// btSearch
			// 
			this.btSearch.ImageIndex = 12;
			this.btSearch.Text = "Search";
			this.btSearch.Visible = false;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Simple
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 273);
			this.Controls.Add(this.toolBar1);
			this.Name = "Simple";
			this.Text = "simple";
			this.ResumeLayout(false);

		}
		#endregion

		protected virtual void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(BindMast!=null&& BindMast.Count!=0)
			{
				if(e.Button.Equals(btFirst)) First();
				if(e.Button.Equals(btPre)) Pre();
				if(e.Button.Equals(btNext)) Next();
				if(e.Button.Equals(btLast)) Last();	
				if(e.Button.Equals(this.btDel)) Remove();
				if(e.Button.Equals(this.btEdit)) Edit();
				
			}
			if(e.Button.Equals(btExit)) this.Close();
			if(e.Button.Equals (this.btRefresh)) ReFill();
			if(e.Button.Equals (this.btSave)) Save();
			if(e.Button.Equals(btNew)) AddNew();
			if(e.Button.Equals(this.btSearch)) Search();
		}
		protected virtual void Edit()
		{

		}
		protected virtual void Save()
		{
			MessageBox.Show(this,"Save changing data into database");
		}
		protected virtual void ReFill()
		{
			MessageBox.Show(this,"Read the data from Database");
		}
		protected virtual void AddNew()
		{
			Save();
			BindMast.AddNew ();
		}
		protected virtual void Remove()
		{
			Save();
			BindMast.RemoveAt(BindMast.Position);
		}
		protected virtual void First()
		{
			Save();
			BindMast.Position=0;
		}
		protected virtual void Pre()
		{
			Save();
			BindMast.Position-=1;
		}
		protected virtual void Next()
		{
			Save();
			BindMast.Position+=1;
		}
		protected virtual void Last()
		{
			Save();
			BindMast.Position=BindMast.Count -1;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ReFill();

		}
		public virtual void Filter(String s)
		{
			this.SelSql=s;
			//MessageBox.Show ( this.BasSql +this.SelSql) ;
			this.ReFill ();
			
		}
		protected virtual void Search()
		{
		}

	}
}
