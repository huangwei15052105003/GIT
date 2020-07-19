using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.BA
{
	public class BA010 : star.Public.SimpleList
	{
		
		private System.ComponentModel.IContainer components = null;

		public BA010()
		{
			// This call is required by the Windows Form Designer.
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BA010));
		
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
			this.dataGrid1.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dataGrid1_Navigate);
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			
	
			// 
			// BA010
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 277);
			this.Name = "BA010";

			this.Controls.SetChildIndex(this.dataGrid1, 0);
			this.Controls.SetChildIndex(this.toolBar1, 0);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		protected override void ReFill()
		{
			int p=new int();
			if(BindMast!=null&&BindMast.Position>0) p=BindMast.Position; 
			this.DaMast.SelectCommand=new System.Data.OleDb.OleDbCommand("select * from Customer",((wmMain)this.MdiParent).db);
 		    System.Data.OleDb.OleDbCommandBuilder OleDbCommandBuilder1=new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			this.dataGrid1.TableStyles.Clear();
			this.DaMast.Fill(this.DsMast,"Customer");
			this.dataGrid1.SetDataBinding (DsMast,"Customer");
			this.BindMast=(CurrencyManager)this.BindingContext[DsMast, "Customer"];
			this.BindMast.Position=p;
			this.dataGrid1.TableStyles.Add(new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Customer";
			this.dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt(0);
			

		}
		protected override void Save()
		{
		//	this.DsMast.AcceptChanges ();
			this.BindMast.EndCurrentEdit();
			//((System.Data.DataRowView)this.BindMast.Current).EndEdit ();  
			try
			{
				DaMast.Update (DsMast,"Customer");
			}
			catch(Exception e)
			{
				MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ());
				this.DsMast.RejectChanges();
			}
		}

		private void dataGrid1_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{		
		}


		
			
		


	}
}

