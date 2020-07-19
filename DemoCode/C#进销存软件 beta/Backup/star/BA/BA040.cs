using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.BA
{
	public class BA040 : star.Public.Single
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.ComponentModel.IContainer components = null;

		public BA040()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BA040));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(566, 309);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(184, 309);
			// 
			// panel2
			// 
			this.panel2.Location = new System.Drawing.Point(184, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(382, 309);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Name = "panel3";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.textBox4);
			this.panel4.Controls.Add(this.comboBox1);
			this.panel4.Controls.Add(this.textBox2);
			this.panel4.Controls.Add(this.textBox1);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(238, 309);
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new System.Drawing.Size(568, 40);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Product Code:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Product Name:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(0, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Product Item:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(0, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Memo: ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(238, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "textBox1";
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(0, 20);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(238, 20);
			this.textBox2.TabIndex = 5;
			this.textBox2.Text = "textBox2";
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox4.Location = new System.Drawing.Point(0, 61);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox4.Size = new System.Drawing.Size(238, 72);
			this.textBox4.TabIndex = 7;
			this.textBox4.Text = "textBox4";
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.Location = new System.Drawing.Point(0, 40);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(238, 21);
			this.comboBox1.TabIndex = 8;
			this.comboBox1.Text = "comboBox1";
			this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
			// 
			// BA040
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 357);
			this.Name = "BA040";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.Text="Products Information";
		}
		protected override void ReFill()
		{
			
			int p=-1;
			if(BindMast!=null&&BindMast.Position>=0)p=BindMast.Position;
			this.DaMast.SelectCommand =new System.Data.OleDb.OleDbCommand("select PR.AUTO_NO,PR_ITEM,PR_CODE,PR_NAME,IT_NAME,PR_MEMO from PR left outer join PR_Item on PR.PR_ITEM=PR_Item.Auto_No",((wmMain)this.MdiParent).db);
			//System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			this.DaMast.Fill(this.DsMast,"Mast");
			this.DsMast.Tables["Mast"].Columns["PR_CODE"].DefaultValue ="";
			this.DsMast.Tables["Mast"].Columns["PR_NAME"].DefaultValue ="";
			this.dataGrid1.TableStyles.Clear();
			System.Data.DataView dv=this.DsMast.Tables["Mast"].DefaultView;
			this.dataGrid1.DataSource=dv;
			this.BindMast =(CurrencyManager)this.dataGrid1.BindingContext[dv];
			if (p>=0)this.BindMast.Position =p;else this.BindMast.Position=0;
			this.dataGrid1.TableStyles.Add (new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Mast";
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			//
			//Initial TextBox
			//
			//textBox1.DataBindings.Clear();this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.PR_CODE");
			if(this.textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",dv,"PR_CODE");
			if(this.textBox2.DataBindings.Count <=0)this.textBox2.DataBindings.Add("Text",dv,"PR_Name");
			if(this.textBox4.DataBindings.Count <=0)this.textBox4.DataBindings.Add("Text",dv,"PR_Memo");
			//
			//Initial the Status ComboBox
			//
			
			System.Data.OleDb.OleDbDataAdapter DaTm=new System.Data.OleDb.OleDbDataAdapter ();
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR_Item ",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"PR_Item");
			this.comboBox1.DataSource=this.DsMast.Tables["PR_Item"];
			this.comboBox1.DisplayMember ="IT_NAME";
			this.comboBox1.ValueMember="Auto_No";
		//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",dv,"PR_ITEM");


			
			DaTm.Dispose();
			DaTm=null;
			


		}
		protected override void Save()
		{
			//this.textBox1.
			//	this.DsMast.AcceptChanges ();
			this.BindMast.EndCurrentEdit ();
			System.Data.OleDb.OleDbDataAdapter DaSave=new System.Data.OleDb.OleDbDataAdapter ();
			DaSave.SelectCommand =new System.Data.OleDb.OleDbCommand("select * from PR" ,((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (DaSave);
			try
			{
				DaSave.Update (DsMast,"Mast");
			}
			catch(Exception e)
			{
				MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ());
				this.DsMast.RejectChanges();
			}
			DaSave.Dispose ();
			DaSave=null;

		}

		

		
	

		private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (this.BindMast.Position >=0 && this.comboBox1.SelectedValue!=null)
			{
				try
				{
					System.Data.DataRowView drv=(System.Data.DataRowView)BindMast.Current ;
					((System.Data.DataRowView) this.BindMast.Current)["PR_ITEM"]=comboBox1.SelectedValue ;
					((System.Data.DataRowView) this.BindMast.Current)["IT_NAME"]=comboBox1.Text;

				}
				catch (Exception ex)
				{
					MessageBox.Show("Error:"+ex.Message.ToString ());
				}
			}

		}
	}
}

