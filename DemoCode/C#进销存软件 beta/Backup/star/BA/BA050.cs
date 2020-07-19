using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.BA
{
	public class BA050 : star.Public.SimpleList
	{
		private System.ComponentModel.IContainer components = null;

		public BA050()
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
			components = new System.ComponentModel.Container();
		}
		#endregion
		protected override void ReFill()
		{
			int p=new int();
			if(BindMast!=null&&BindMast.Position>0) p=BindMast.Position; 
			this.DaMast.SelectCommand=new System.Data.OleDb.OleDbCommand("select * from RE",((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder OleDbCommandBuilder1=new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			this.dataGrid1.TableStyles.Clear();
			this.DaMast.Fill(this.DsMast,"Mast");
			this.dataGrid1.SetDataBinding (DsMast,"Mast");
			this.BindMast=(CurrencyManager)this.BindingContext[DsMast, "Mast"];
			this.BindMast.Position=p;
			this.dataGrid1.TableStyles.Add(new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Mast";
			this.dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt(0);
			

		}
		protected override void Save()
		{
		//	this.DsMast.AcceptChanges ();
			this.BindMast.EndCurrentEdit();
			this.DaMast.Update(DsMast,"Mast");
		}
	}
}

