using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.PR
{
	public class PR011 : star.Public.Single
	{
		private System.ComponentModel.IContainer components = null;
		
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		//
		//1. define purId
		//
		private int purId;
		private System.Data.OleDb.OleDbConnection dbCon;

		public PR011()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}
		//
		//2. construct the class;
		//
		public PR011(System.Data.OleDb.OleDbConnection dbCon,int purId)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.dbCon=dbCon;
			this.purId=purId;
			this.BasSql="select * from PUR_CF " ;
			this.DaMast.RowUpdated +=new System.Data.OleDb.OleDbRowUpdatedEventHandler(DaMast_RowUpdated);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PR011));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
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
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label6);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.label1);
			this.panel3.DockPadding.All = 5;
			this.panel3.Name = "panel3";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.textBox9);
			this.panel4.Controls.Add(this.textBox8);
			this.panel4.Controls.Add(this.textBox7);
			this.panel4.Controls.Add(this.textBox6);
			this.panel4.Controls.Add(this.textBox5);
			this.panel4.Controls.Add(this.textBox4);
			this.panel4.Controls.Add(this.textBox3);
			this.panel4.Controls.Add(this.textBox2);
			this.panel4.Controls.Add(this.dateTimePicker1);
			this.panel4.Controls.Add(this.textBox1);
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
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(5, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(5, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Pay to Supplier";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(5, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Pay to Others";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(5, 5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(138, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "textBox1";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(5, 25);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(138, 20);
			this.dateTimePicker1.TabIndex = 1;
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(5, 45);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(138, 20);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox3.Location = new System.Drawing.Point(5, 65);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(138, 20);
			this.textBox3.TabIndex = 3;
			this.textBox3.Text = "textBox3";
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(5, 282);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "Balance to Others";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label6.Location = new System.Drawing.Point(5, 262);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(134, 20);
			this.label6.TabIndex = 5;
			this.label6.Text = "Sum of Payment to OT:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label7.Location = new System.Drawing.Point(5, 242);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134, 20);
			this.label7.TabIndex = 6;
			this.label7.Text = "Payable to Others";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label8.Location = new System.Drawing.Point(5, 222);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 20);
			this.label8.TabIndex = 7;
			this.label8.Text = "Balance to Supplier";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label9.Location = new System.Drawing.Point(5, 202);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(134, 20);
			this.label9.TabIndex = 8;
			this.label9.Text = "Sum of Payment to Sup:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label10.Location = new System.Drawing.Point(5, 182);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(134, 20);
			this.label10.TabIndex = 9;
			this.label10.Text = "Payable to Supplier:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBox4.Location = new System.Drawing.Point(5, 282);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(138, 20);
			this.textBox4.TabIndex = 4;
			this.textBox4.Text = "textBox4";
			this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox5
			// 
			this.textBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox5.Location = new System.Drawing.Point(5, 262);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(138, 20);
			this.textBox5.TabIndex = 5;
			this.textBox5.Text = "textBox5";
			// 
			// textBox6
			// 
			this.textBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox6.Location = new System.Drawing.Point(5, 242);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(138, 20);
			this.textBox6.TabIndex = 6;
			this.textBox6.Text = "textBox6";
			// 
			// textBox7
			// 
			this.textBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBox7.Location = new System.Drawing.Point(5, 222);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(138, 20);
			this.textBox7.TabIndex = 7;
			this.textBox7.Text = "textBox7";
			this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox8
			// 
			this.textBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox8.Location = new System.Drawing.Point(5, 202);
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new System.Drawing.Size(138, 20);
			this.textBox8.TabIndex = 8;
			this.textBox8.Text = "textBox8";
			// 
			// textBox9
			// 
			this.textBox9.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox9.Location = new System.Drawing.Point(5, 182);
			this.textBox9.Name = "textBox9";
			this.textBox9.ReadOnly = true;
			this.textBox9.Size = new System.Drawing.Size(138, 20);
			this.textBox9.TabIndex = 9;
			this.textBox9.Text = "textBox9";
			// 
			// PR011
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 357);
			this.Name = "PR011";
			this.Text = "single";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).EndInit();

		}
		#endregion
		//
		//3. load database
		//
		protected override void ReFill()
		{
			//3.1 Mast
			int p=0;
			this.SelSql=" where PUR_ID="+this.purId;
			if(BindMast!=null&&BindMast.Position>0)p=BindMast.Position;
			this.DaMast.SelectCommand =new System.Data.OleDb.OleDbCommand(BasSql+SelSql,dbCon);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (this.DaMast);
			this.DsMast.Clear();
			//MessageBox.Show (this.BasSql +this.SelSql );
			this.DaMast.Fill(this.DsMast,"Mast");
			DsMast.Tables ["Mast"].Columns["PT_SUP"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["PT_OT"].DefaultValue=0;
			//DsMast.Tables ["Mast"].Columns.Add("TOT_SUP",System.Type.GetType("System.Decimal"),"Sum(PT_SUP)");
			//DsMast.Tables ["Mast"].Columns.Add("TOT_OT",System.Type.GetType("System.Decimal"),"Sum(PT_OT)");
			this.Ref_OV ();
			
			//
			//3.2 Initial CurrencyManager;
			//
			
			this.BindMast =(CurrencyManager)this.BindingContext[DsMast,"Mast"];
			this.BindMast.Position =p;
			//this.BindMast.PositionChanged-=new EventHandler(BindMast_PositionChanged);
			//this.BindMast.PositionChanged+=new EventHandler(BindMast_PositionChanged);
			
			//
			//3.3 Initial DataGrid;
			//
			this.dataGrid1.TableStyles.Clear();
			this.dataGrid1.SetDataBinding (this.DsMast,"Mast");
			this.dataGrid1.TableStyles.Add (new DataGridTableStyle());
			dataGrid1.TableStyles[0].MappingName="Mast";
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (0);
			dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (2);
			//dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (4);
			//dataGrid1.TableStyles[0].GridColumnStyles.RemoveAt (4);
			//
			//3.4 Initial Textbox
			//
			this.dateTimePicker1.Value=System.DateTime.Now;
			if(this.textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.CODE");
			if(this.textBox2.DataBindings.Count<=0)this.textBox2.DataBindings.Add("Text",this.DsMast,"Mast.PT_SUP");
			if(this.textBox3.DataBindings.Count<=0)this.textBox3.DataBindings.Add("Text",this.DsMast,"Mast.PT_OT");
			if(this.textBox4.DataBindings.Count<=0)this.textBox4.DataBindings.Add("Text",this.DsMast,"OV.BL_OT");
			if(this.textBox5.DataBindings.Count<=0)this.textBox5.DataBindings.Add("Text",this.DsMast,"OV.PT_OTED");
			if(this.textBox6.DataBindings.Count<=0)this.textBox6.DataBindings.Add("Text",this.DsMast,"OV.PT_OT");
			if(this.textBox7.DataBindings.Count<=0)this.textBox7.DataBindings.Add("Text",this.DsMast,"OV.BL_SUP");
			if(this.textBox8.DataBindings.Count<=0)this.textBox8.DataBindings.Add("Text",this.DsMast,"OV.PT_SUPED");
			if(this.textBox9.DataBindings.Count<=0)this.textBox9.DataBindings.Add("Text",this.DsMast,"OV.PT_SUP");
			if(this.dateTimePicker1.DataBindings.Count<=0)this.dateTimePicker1.DataBindings.Add("Text",this.DsMast,"Mast.C_DATE");
			

		}
		//
		//4. Save to Database
		//
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
		//
		// 5. this function includes the additional action when save the database.
		//

		private void DaMast_RowUpdated(object sender, System.Data.OleDb.OleDbRowUpdatedEventArgs e)
		{
			int newID = 0;
			System.Data.OleDb.OleDbCommand idCMD = new System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY",dbCon);
			
			if (e.StatementType == System.Data.StatementType.Insert)
			{
				// Retrieve the identity value and store it in the CategoryID column.
				newID = (int)idCMD.ExecuteScalar();
				e.Row["AUTO_NO"] = newID;
				//	MessageBox.Show (newID.ToString ());
			}
			this.Ref_OV ();
		}
		//
		// 6. Override the AddNew records
		//
		protected override void AddNew()
		{
			base.AddNew ();
			System.Data.DataRowView drv=(System.Data.DataRowView)this.BindMast.Current ;
			drv["PUR_ID"]=this.purId;
		}
		private void Ref_OV()
		{
			System.Data.OleDb.OleDbDataAdapter tmda=new System.Data.OleDb.OleDbDataAdapter ("select * from PUR_CF_OV where AUTO_NO="+this.purId,this.dbCon );
			if(this.DsMast.Tables.Contains ("OV"))this.DsMast.Tables["OV"].Clear ();
			tmda.Fill(this.DsMast,"OV");
			tmda.Dispose ();
			tmda=null;
		}
		
	}
}

