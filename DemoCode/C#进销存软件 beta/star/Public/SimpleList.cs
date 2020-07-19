using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.Public
{
	public class SimpleList : star.Public.Simple
	{
		public System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components = null;

		public SimpleList()
		{
			// This call is required by the Windows Form Designer.
			this.btEdit.Visible=false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SimpleList));
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// imageList1
			// 
		
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 40);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(368, 232);
			this.dataGrid1.TabIndex = 1;
			// 
			// SimpleList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 277);
			this.Controls.Add(this.dataGrid1);
			this.Name = "SimpleList";

			this.Controls.SetChildIndex(this.dataGrid1, 0);
			this.Controls.SetChildIndex(this.toolBar1, 0);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.dataGrid1!=null)
			{
				this.dataGrid1.Height=this.Height-70;
				this.dataGrid1.Width =this.Width-10 ;
			}
		}





		
	}
}

