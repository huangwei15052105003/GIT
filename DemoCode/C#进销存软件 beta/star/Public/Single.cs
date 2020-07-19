using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.Public
{
	public class Single : star.Public.Simple
	{
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Splitter splitter1;
		protected System.Windows.Forms.Panel panel2;
		protected System.Windows.Forms.Panel panel3;
		protected System.Windows.Forms.Panel panel4;
		private System.ComponentModel.IContainer components = null;

		public Single()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Single));
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new System.Drawing.Size(504, 40);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.dataGrid1);
			this.panel1.Location = new System.Drawing.Point(0, 45);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(496, 307);
			this.panel1.TabIndex = 1;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(208, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 305);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(208, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(286, 305);
			this.panel2.TabIndex = 2;
			// 
			// panel4
			// 
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.DockPadding.All = 5;
			this.panel4.Location = new System.Drawing.Point(144, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(142, 305);
			this.panel4.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.DockPadding.All = 5;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(144, 305);
			this.panel3.TabIndex = 0;
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Left;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(208, 305);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
			// 
			// Single
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 357);
			this.Controls.Add(this.panel1);
			this.Name = "Single";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolBar1, 0);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		protected override void OnResize(EventArgs e)
		{
			if (this.panel1!=null)
			{
				this.panel1.Height =this.Height-75;
				this.panel1.Width =this.Width -10;
			}
		}
		protected virtual void Calc()
		{
			
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad( e);
			this.Text="single";
		}

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{
			this.Save();
		}


	}
}

