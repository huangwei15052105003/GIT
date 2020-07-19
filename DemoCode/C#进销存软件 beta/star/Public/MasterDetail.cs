using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.Public
{
	public class MasterDetail : star.Public.Single
	{
		protected System.Windows.Forms.DataGrid dataGrid2;
		protected System.Windows.Forms.Splitter splitter2;
		private System.ComponentModel.IContainer components = null;
		//private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button button1;
		public System.Data.OleDb.OleDbDataAdapter DaDetail;
	//	protected System.Windows.Forms.ToolBarButton btSearch;
		public MasterDetail()
		{
			// This call is required by the Windows Form Designer.
			DaDetail=new System.Data.OleDb.OleDbDataAdapter();
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MasterDetail));
			this.dataGrid2 = new System.Windows.Forms.DataGrid();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
			this.panel3.SuspendLayout();
			this.btSearch = new System.Windows.Forms.ToolBarButton();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(184, 309);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.splitter2);
			this.panel2.Controls.Add(this.dataGrid2);
			this.panel2.Name = "panel2";
			// 
			// toolBar1
			// 
			this.btSearch.ImageIndex = 11;
			this.btSearch.Text = "Search";
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.btSearch});
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new System.Drawing.Size(576, 40);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// dataGrid2
			// 
			this.dataGrid2.DataMember = "";
			this.dataGrid2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dataGrid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid2.Location = new System.Drawing.Point(0, 221);
			this.dataGrid2.Name = "dataGrid2";
			this.dataGrid2.ReadOnly = true;
			this.dataGrid2.Size = new System.Drawing.Size(374, 88);
			this.dataGrid2.TabIndex = 0;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter2.Location = new System.Drawing.Point(0, 213);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(374, 8);
			this.splitter2.TabIndex = 0;
			this.splitter2.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 189);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(374, 24);
			this.panel3.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Right;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.ImageIndex = 3;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(278, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "Edit List";
			this.button1.Click += new System.EventHandler(this.Sh_DeList);
			// 
			// MasterDetail
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 357);
			this.Name = "MasterDetail";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
			this.panel3.ResumeLayout(false);

		}
		#endregion
	
	
		protected virtual void ReFillMast()
		{
		}
		protected virtual void ReFillDetail()
		{
		}
		protected override void ReFill()
		{
			this.ReFillMast();
			if (BindMast!=null)
			{
			//	this.BindMast.ItemChanged+=new ItemChangedEventHandler(BindMast_ItemChanged);
				this.BindMast.PositionChanged -=new EventHandler(BindMast_PositionChanged);
				this.BindMast.PositionChanged +=new EventHandler(BindMast_PositionChanged);
			//	this.BindMast.CurrentChanged+=new EventHandler(BindMast_CurrentChanged);
			}
			if(BindMast!=null&&BindMast.Position>=0) 
			{
				this.ReFillDetail();
				this.Calc();
			}
		}
		protected override void Save()
		{
			
			this.BindMast.EndCurrentEdit ();
			try
			{
				this.DaMast.Update (DsMast,"Mast");
			}
			catch(Exception e)
			{
				MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ());
				this.DsMast.RejectChanges();
			}
		}
		

		protected virtual void BindMast_CurrentChanged(object sender, EventArgs e)
		{
			
		}

		protected virtual void BindMast_PositionChanged(object sender, EventArgs e)
		{
			this.ReFillDetail();
			this.Calc();
		//	MessageBox.Show(((CurrencyManager)sender).Position.ToString());

		}

		protected virtual void BindMast_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			//MessageBox.Show(((CurrencyManager)sender).Position.ToString());
		}

		protected virtual void Sh_DeList(object sender, System.EventArgs e)
		{
		
		}
		
		protected override void AddNew()
		{
			base.AddNew ();
			this.Calc ();
			this.DsMast.Tables["Detail"].Clear ();
		}
	

	
	}
}

