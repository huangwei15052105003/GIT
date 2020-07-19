using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.IN
{
	public class IN010 : star.Public.Single
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.ComponentModel.IContainer components = null;

		public IN010()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.btSearch.Visible =true;
			this.Text="Inventory Changing Records";
			this.BasSql ="select * from IN010";
			this.SelSql ="";
			

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IN010));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(598, 309);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(208, 307);
			// 
			// panel2
			// 
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(388, 307);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label2);
			this.panel3.DockPadding.All = 5;
			this.panel3.Name = "panel3";
			this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.comboBox2);
			this.panel4.Controls.Add(this.textBox4);
			this.panel4.Controls.Add(this.textBox3);
			this.panel4.Controls.Add(this.textBox2);
			this.panel4.Controls.Add(this.textBox1);
			this.panel4.Controls.Add(this.dateTimePicker1);
			this.panel4.Controls.Add(this.comboBox1);
			this.panel4.DockPadding.All = 5;
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(244, 307);
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new System.Drawing.Size(600, 40);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(5, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Product";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(5, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "IN_DATE";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(5, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "IN_CODE";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Top;
			this.label8.Location = new System.Drawing.Point(5, 65);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 20);
			this.label8.TabIndex = 7;
			this.label8.Text = "PR_PRICE";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Location = new System.Drawing.Point(5, 85);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(134, 20);
			this.label9.TabIndex = 8;
			this.label9.Text = "QTY";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Dock = System.Windows.Forms.DockStyle.Top;
			this.label10.Location = new System.Drawing.Point(5, 105);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(134, 20);
			this.label10.TabIndex = 9;
			this.label10.Text = "Total Amount";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.Location = new System.Drawing.Point(5, 5);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(234, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.Text = "comboBox1";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(5, 26);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(234, 20);
			this.dateTimePicker1.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(5, 46);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(234, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "textBox1";
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(5, 66);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(234, 20);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox3.Location = new System.Drawing.Point(5, 86);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(234, 20);
			this.textBox3.TabIndex = 4;
			this.textBox3.Text = "textBox3";
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox4.Location = new System.Drawing.Point(5, 106);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(234, 20);
			this.textBox4.TabIndex = 5;
			this.textBox4.Text = "textBox4";
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(5, 125);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "RE_NAME";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBox2
			// 
			this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox2.Location = new System.Drawing.Point(5, 126);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(234, 21);
			this.comboBox2.TabIndex = 6;
			this.comboBox2.Text = "comboBox2";
			// 
			// IN010
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 357);
			this.Name = "IN010";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion
		protected override void ReFill()
		{
			int p=0;
			
			if(BindMast!=null&&BindMast.Position>0)p=BindMast.Position;
			this.DaMast.SelectCommand =new System.Data.OleDb.OleDbCommand(BasSql+SelSql,((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			//this.DsMast.Dispose();
			//this.DsMast =new System.Data.DataSet ();
			this.DaMast.Fill(this.DsMast,"Mast");
			DsMast.Tables ["Mast"].Columns["PR_PRICE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["QTY"].DefaultValue=0;
			
			
			//
			//Initial CurrencyManager;
			//
			
			this.BindMast =(CurrencyManager)this.BindingContext[DsMast,"Mast"];
			this.BindMast.Position =p;
			//this.BindMast.PositionChanged-=new EventHandler(BindMast_PositionChanged);
			//this.BindMast.PositionChanged+=new EventHandler(BindMast_PositionChanged);
			
			//
			//Initial DataGrid;
			//
			this.dataGrid1.TableStyles.Clear();
			this.dataGrid1.SetDataBinding (this.DsMast,"Mast");
			this.dataGrid1.TableStyles.Add (new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Mast";
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			//SwiColSty(dataGrid1.TableStyles[0].GridColumnStyles,6,14);
			//dataGrid1.TableStyles.
			//
			//Initial TextBox;
			//
			
			if(this.textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.IN_CODE");
			if(this.textBox2.DataBindings.Count<=0)this.textBox2.DataBindings.Add("Text",this.DsMast,"Mast.PR_PRICE");
			if(this.textBox3.DataBindings.Count<=0)this.textBox3.DataBindings.Add("Text",this.DsMast,"Mast.QTY");
			if(this.textBox4.DataBindings.Count<=0)this.textBox4.DataBindings.Add("Text",this.DsMast,"Mast.TOTAL_AMOUNT");
			
			
			this.dateTimePicker1.Value=System.DateTime.Now;
			if(this.dateTimePicker1.DataBindings.Count<=0)this.dateTimePicker1.DataBindings.Add("Text",this.DsMast,"Mast.IN_DATE");
			
			//	if(this.checkBox1.DataBindings.Count<=0)this.checkBox1.DataBindings.Add("Checked", this.DsMast, "Mast.CR");
			
			//
			//Initial the ComboBoxs
			//
			
			System.Data.OleDb.OleDbDataAdapter DaTm=new System.Data.OleDb.OleDbDataAdapter ();
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from RE ",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"RE");
			this.comboBox2.DataSource=this.DsMast.Tables["RE"];
			this.comboBox2.DisplayMember ="RE_NAME";
			this.comboBox2.ValueMember="AUTO_NO";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox2.DataBindings.Count<=0) this.comboBox2.DataBindings.Add("SelectedValue",DsMast,"Mast.RE_ID");
			
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR011",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"PR");
			this.comboBox1.DataSource=this.DsMast.Tables["PR"].DefaultView;
			this.DsMast.Tables["PR"].DefaultView.Sort="FNAME";
			this.comboBox1.DisplayMember ="FNAME";
			this.comboBox1.ValueMember="AUTO_NO";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ID");
			
			//
			// Initial other tables in memory
			//
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR_Item",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"PR_Item");

			DaTm.Dispose();
			DaTm=null;
			


		}

		protected override void Save()
		{
			//this.textBox1.
			//	this.DsMast.AcceptChanges ();
			this.BindMast.EndCurrentEdit ();
			System.Data.OleDb.OleDbDataAdapter DaSave=new System.Data.OleDb.OleDbDataAdapter ();
			DaSave.SelectCommand =new System.Data.OleDb.OleDbCommand("select * from [INV]" ,((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (DaSave);
			//MessageBox.Show (OleDbCommandBuilder1.GetUpdateCommand ().CommandText.ToString ());
			DaSave.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(DaSave_RowUpdated);
				//MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ()+OleDbCommandBuilder1.GetInsertCommand().CommandText);
			try
			{
				
				DaSave.Update (DsMast,"Mast");
				
			}
			catch(Exception e)
			{
				MessageBox.Show("Current records have error. The transaction will be ignore! Error:"+e.Message.ToString ());//+OleDbCommandBuilder1.GetInsertCommand().CommandText);
				this.DsMast.RejectChanges();
			}
			DaSave.Dispose ();
			DaSave=null;
		}

		private void DaSave_RowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs e)
		{
			int newID = 0;
			System.Data.OleDb.OleDbCommand idCMD = new System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY",((wmMain)this.MdiParent).db);
			
			if (e.StatementType == System.Data.StatementType.Insert)
			{
				// Retrieve the identity value and store it in the CategoryID column.
				newID = (int)idCMD.ExecuteScalar();
				e.Row["AUTO_NO"] = newID;
				//	MessageBox.Show (newID.ToString ());
			}
			if(e.StatementType!= System.Data.StatementType.Delete)//e.StatementType == System.Data.StatementType.Insert ||e.StatementType == System.Data.StatementType.Update )
			{
				
				idCMD.CommandText="select TOTAL_AMOUNT,PR_ITEM,RE_NAME,PR_CODE,PR_NAME,IT_NAME from IN010 where AUTO_NO="+e.Row["AUTO_NO"].ToString ();
				System.Data.OleDb.OleDbDataReader dr=idCMD.ExecuteReader();
				if(dr.Read ())
				{
					e.Row["TOTAL_AMOUNT"]=dr.GetValue(0);
					e.Row["PR_ITEM"]=dr.GetValue(1);
					
					e.Row["RE_NAME"]=dr.GetValue (2);
					e.Row["PR_CODE"]=dr.GetValue (3);
					e.Row["PR_NAME"]=dr.GetValue (4);
					e.Row["IT_NAME"]=dr.GetValue (5);
					//e.Row["PR_CODE"]=dr.GetValue (6);
					
				}
				dr.Close ();
				dr=null;
				
				
				
			}
			idCMD=null;
		}
	

		private void panel3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}
		protected override void Search()
		{
			new star.IN.IN010SE(this).ShowDialog();
		}


		
	}
}

