using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.HS
{
	public class HS010 : star.Public.Single
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.ComponentModel.IContainer components = null;

		public HS010()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HS010));
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
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
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.dateTimePicker1);
			this.panel2.Name = "panel2";
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			// 
			// imageList1
			// 
			//this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Checked = false;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(96, 32);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(128, 20);
			this.dateTimePicker1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Barcode:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(96, 80);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(128, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Date:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "Status:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(96, 120);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(128, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// HS010
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 357);
			this.Name = "HS010";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion

		#region my Override functions
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);
			this.Text="Input Transaction";
		}
		protected override void ReFill()
		{
			int p=new int ();
			if(BindMast!=null&&BindMast.Position>0)p=BindMast.Position;
			this.DaMast.SelectCommand =new System.Data.SqlClient.SqlCommand("select * from history",((wmMain)this.MdiParent).db);
			System.Data.SqlClient.SqlCommandBuilder SqlCommandBuilder1 =new System.Data.SqlClient.SqlCommandBuilder (this.DaMast);
			
			this.DsMast.Clear();
			this.DaMast.Fill(this.DsMast,"Mast");
			this.dataGrid1.TableStyles.Clear();
			this.dataGrid1.SetDataBinding (this.DsMast,"Mast");
			this.BindMast =(CurrencyManager)this.BindingContext[DsMast,"Mast"];
			this.BindMast.Position =p;
			this.dataGrid1.TableStyles.Add (new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Mast";
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);			//if(textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.Bar_Code");
			//
			//Initial the Manufacture ComboBox
			//
			//System.Data.SqlClient.SqlDataAdapter DaTm=new System.Data.SqlClient.SqlDataAdapter ();
			//DaTm.SelectCommand =new System.Data.SqlClient.SqlCommand("select * from manus",((wmMain)this.MdiParent).db);
			//DaTm.Fill(this.DsMast,"manus");
			//this.comboBox2.DataSource=this.DsMast.Tables["manus"];
			//this.comboBox2.DisplayMember ="manufacturer";
			//this.comboBox2.ValueMember="Auto_No";
			//if(comboBox2.DataBindings.Count<=0) this.comboBox2.DataBindings.Add("SelectedValue",DsMast,"Mast.Ma_Id");
			//DaTm.Dispose();
			//DaTm=null;
			//
			//Initial the Hardware Type ComboBox
			//
			//DaTm=new System.Data.SqlClient.SqlDataAdapter ();
			//DaTm.SelectCommand =new System.Data.SqlClient.SqlCommand("select * from HW_Type",((wmMain)this.MdiParent).db);
			//DaTm.Fill(this.DsMast,"HW_Type");
			//this.DsMast.Tables["HW_Type"].DefaultView.Sort="Type_Name";
			//this.comboBox1.DataSource=this.DsMast.Tables["HW_Type"];
			//this.comboBox1.DisplayMember ="Type_Name";
			//this.comboBox1.ValueMember="Auto_No";
			//if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.Type_Id");
			//DaTm.Dispose ();
			//DaTm=null;
			//
			//Initial the TextBox.
			//
            if(textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.barcode");
		    if (dateTimePicker1.DataBindings.Count<=0)this.dateTimePicker1.DataBindings.Add("Text",this.DsMast,"Mast.Da");
			//Initial the Status ComboBox
			//
			System.Data.SqlClient.SqlDataAdapter DaTm=new System.Data.SqlClient.SqlDataAdapter ();
			DaTm.SelectCommand =new System.Data.SqlClient.SqlCommand("select * from hardwarestatus ",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"hardwarestatus");
			this.comboBox1.DataSource=this.DsMast.Tables["hardwarestatus"];
			this.comboBox1.DisplayMember ="status";
			this.comboBox1.ValueMember="Auto_No";

			if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.Status");
			DaTm.Dispose();
			DaTm=null;
		
		
		
		
		
		}
		protected override void Save()
		{
			//this.textBox1.
			//	this.DsMast.AcceptChanges ();
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
		#endregion

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}

