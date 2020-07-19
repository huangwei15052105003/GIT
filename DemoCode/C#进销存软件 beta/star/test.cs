using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star
{
	public class test : star.Public.Single
	{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		public test()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(test));
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(208, 307);
			// 
			// panel2
			// 
			this.panel2.Name = "panel2";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label1);
			this.panel3.DockPadding.All = 5;
			this.panel3.Name = "panel3";
			// 
			// panel4
			// 
			this.panel4.DockPadding.All = 5;
			this.panel4.Name = "panel4";
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// test
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 357);
			this.Name = "test";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion
		protected override void ReFill()
		{
			System.Windows.Forms.Label lab=new Label();
			this.Text="test1";
			this.Dock=System.Windows.Forms.DockStyle.Top;
			this.panel3.Controls.Add(lab);
			//lab=null;
			//lab=new Label();
			//this.Text="test2";
			//this.Dock=System.Windows.Forms.DockStyle.Top;
			//this.panel3.Controls.Add(lab);

		}

	}
}

