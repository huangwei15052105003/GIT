using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.BA
{
	public class BA030 : star.Public.SimpleList
	{
		private System.ComponentModel.IContainer components = null;

		public BA030()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BA030));
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			// 
			// imageList1
			// 
			//this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// BA030
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 277);
			this.Name = "BA030";
			this.Text = "Product Items";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion
		protected override void ReFill()
		{
			this.DaMast.SelectCommand=new System.Data.OleDb.OleDbCommand("select * from PR_Item",((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder OleDbCommandBuilder1=new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			this.dataGrid1.TableStyles.Clear();
			this.DaMast.Fill(this.DsMast,"PR_Item");
			this.DsMast.Tables["PR_Item"].Columns["IT_NAME"].DefaultValue ="";
			this.dataGrid1.SetDataBinding (DsMast,"PR_Item");
			this.BindMast=(CurrencyManager)this.BindingContext[DsMast, "PR_Item"];
			
			this.dataGrid1.TableStyles.Add(new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="PR_Item";
			this.dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt(0);

		}
		protected override void Save()
		{
			this.BindMast.EndCurrentEdit ();
			try
			{
				this.DaMast.Update(DsMast,"PR_Item");
			}
			catch(Exception e)
			{
				MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ());
				this.DsMast.RejectChanges();
			}
			
		}
	}
}

