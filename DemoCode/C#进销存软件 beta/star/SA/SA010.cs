using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.SA
{
	public class SA010 : star.Public.Single
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.ComponentModel.IContainer components = null;

		public SA010()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.btSearch.Visible =true;
			this.Text="Sale Records";
			this.BasSql ="select * from SA010";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SA010));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(614, 405);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(184, 403);
			// 
			// panel2
			// 
			this.panel2.Location = new System.Drawing.Point(184, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(428, 403);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.label18);
			this.panel3.Controls.Add(this.label17);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Controls.Add(this.label16);
			this.panel3.Controls.Add(this.label15);
			this.panel3.Controls.Add(this.label11);
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
			this.panel3.Size = new System.Drawing.Size(144, 403);
			// 
			// panel4
			// 
			this.panel4.AutoScroll = true;
			this.panel4.Controls.Add(this.dateTimePicker2);
			this.panel4.Controls.Add(this.textBox12);
			this.panel4.Controls.Add(this.textBox11);
			this.panel4.Controls.Add(this.checkBox1);
			this.panel4.Controls.Add(this.button1);
			this.panel4.Controls.Add(this.textBox10);
			this.panel4.Controls.Add(this.textBox9);
			this.panel4.Controls.Add(this.textBox8);
			this.panel4.Controls.Add(this.textBox7);
			this.panel4.Controls.Add(this.textBox6);
			this.panel4.Controls.Add(this.textBox5);
			this.panel4.Controls.Add(this.textBox4);
			this.panel4.Controls.Add(this.textBox3);
			this.panel4.Controls.Add(this.textBox2);
			this.panel4.Controls.Add(this.comboBox2);
			this.panel4.Controls.Add(this.comboBox1);
			this.panel4.Controls.Add(this.dateTimePicker1);
			this.panel4.Controls.Add(this.textBox1);
			this.panel4.DockPadding.All = 5;
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(284, 403);
			// 
			// toolBar1
			// 
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new System.Drawing.Size(616, 40);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(5, 5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(274, 20);
			this.textBox1.TabIndex = 32;
			this.textBox1.Text = "textBox1";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(5, 25);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(274, 20);
			this.dateTimePicker1.TabIndex = 35;
			// 
			// comboBox2
			// 
			this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox2.Location = new System.Drawing.Point(5, 66);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(274, 21);
			this.comboBox2.TabIndex = 34;
			this.comboBox2.Text = "comboBox2";
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.Location = new System.Drawing.Point(5, 45);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(274, 21);
			this.comboBox1.TabIndex = 33;
			this.comboBox1.Text = "comboBox1";
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(5, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 20);
			this.label4.TabIndex = 31;
			this.label4.Text = "Product:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(5, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 20);
			this.label3.TabIndex = 30;
			this.label3.Text = "Customer:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(5, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Date:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 20);
			this.label1.TabIndex = 37;
			this.label1.Text = "Code:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox6
			// 
			this.textBox6.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox6.Location = new System.Drawing.Point(5, 167);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(274, 20);
			this.textBox6.TabIndex = 43;
			this.textBox6.Text = "textBox6";
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox3.Location = new System.Drawing.Point(5, 107);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(274, 20);
			this.textBox3.TabIndex = 42;
			this.textBox3.Text = "textBox3";
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(5, 87);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(274, 20);
			this.textBox2.TabIndex = 41;
			this.textBox2.Text = "textBox2";
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Top;
			this.label7.Location = new System.Drawing.Point(5, 125);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134, 20);
			this.label7.TabIndex = 40;
			this.label7.Text = "Sold Price:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Top;
			this.label6.Location = new System.Drawing.Point(5, 105);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(134, 20);
			this.label6.TabIndex = 39;
			this.label6.Text = "Total Cost:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(5, 85);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 20);
			this.label5.TabIndex = 38;
			this.label5.Text = "Purchase Price:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox4.Location = new System.Drawing.Point(5, 127);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(274, 20);
			this.textBox4.TabIndex = 45;
			this.textBox4.Text = "textBox4";
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Top;
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label8.Location = new System.Drawing.Point(5, 145);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 20);
			this.label8.TabIndex = 44;
			this.label8.Text = "QTY";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox5
			// 
			this.textBox5.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox5.Location = new System.Drawing.Point(5, 147);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(274, 20);
			this.textBox5.TabIndex = 46;
			this.textBox5.Text = "textBox5";
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Location = new System.Drawing.Point(5, 165);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(134, 20);
			this.label9.TabIndex = 47;
			this.label9.Text = "Total Amount:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label10
			// 
			this.label10.Dock = System.Windows.Forms.DockStyle.Top;
			this.label10.Location = new System.Drawing.Point(5, 185);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(134, 20);
			this.label10.TabIndex = 48;
			this.label10.Text = "A.O.E:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox7
			// 
			this.textBox7.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox7.Location = new System.Drawing.Point(5, 187);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(274, 20);
			this.textBox7.TabIndex = 49;
			this.textBox7.Text = "textBox7";
			// 
			// label11
			// 
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Location = new System.Drawing.Point(5, 205);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(134, 20);
			this.label11.TabIndex = 50;
			this.label11.Text = "Profit:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox8
			// 
			this.textBox8.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox8.Location = new System.Drawing.Point(5, 207);
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new System.Drawing.Size(274, 20);
			this.textBox8.TabIndex = 51;
			this.textBox8.Text = "textBox8";
			// 
			// textBox12
			// 
			this.textBox12.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox12.Location = new System.Drawing.Point(5, 303);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new System.Drawing.Size(274, 20);
			this.textBox12.TabIndex = 61;
			this.textBox12.Text = "textBox12";
			// 
			// textBox11
			// 
			this.textBox11.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox11.Location = new System.Drawing.Point(5, 283);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new System.Drawing.Size(274, 20);
			this.textBox11.TabIndex = 52;
			this.textBox11.Text = "textBox11";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(5, 323);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(274, 20);
			this.dateTimePicker2.TabIndex = 60;
			// 
			// label19
			// 
			this.label19.Dock = System.Windows.Forms.DockStyle.Top;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label19.Location = new System.Drawing.Point(5, 325);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(134, 20);
			this.label19.TabIndex = 59;
			this.label19.Text = "Due Date";
			this.label19.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label18
			// 
			this.label18.Dock = System.Windows.Forms.DockStyle.Top;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label18.Location = new System.Drawing.Point(5, 305);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(134, 20);
			this.label18.TabIndex = 58;
			this.label18.Text = "Other Payable";
			this.label18.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label17
			// 
			this.label17.Dock = System.Windows.Forms.DockStyle.Top;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label17.Location = new System.Drawing.Point(5, 285);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(134, 20);
			this.label17.TabIndex = 57;
			this.label17.Text = "Receivable:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox10
			// 
			this.textBox10.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox10.Location = new System.Drawing.Point(5, 247);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(274, 20);
			this.textBox10.TabIndex = 56;
			this.textBox10.Text = "textBox10";
			// 
			// textBox9
			// 
			this.textBox9.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox9.Location = new System.Drawing.Point(5, 227);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(274, 20);
			this.textBox9.TabIndex = 54;
			this.textBox9.Text = "textBox9";
			// 
			// label16
			// 
			this.label16.Dock = System.Windows.Forms.DockStyle.Top;
			this.label16.Location = new System.Drawing.Point(5, 245);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(134, 20);
			this.label16.TabIndex = 55;
			this.label16.Text = "Certified:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label15
			// 
			this.label15.Dock = System.Windows.Forms.DockStyle.Top;
			this.label15.Location = new System.Drawing.Point(5, 225);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(134, 20);
			this.label15.TabIndex = 53;
			this.label15.Text = "Remarks:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(5, 374);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(274, 24);
			this.button1.TabIndex = 62;
			this.button1.Text = "Payment";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label12
			// 
			this.label12.Dock = System.Windows.Forms.DockStyle.Top;
			this.label12.Location = new System.Drawing.Point(5, 265);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(134, 20);
			this.label12.TabIndex = 60;
			this.label12.Text = "Credit Sales?";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// checkBox1
			// 
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkBox1.Location = new System.Drawing.Point(5, 267);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(274, 16);
			this.checkBox1.TabIndex = 63;
			// 
			// SA010
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 453);
			this.Name = "SA010";
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
			
			this.DaMast.Fill(this.DsMast,"Mast");
			
			DsMast.Tables ["Mast"].Columns["PUR_PRICE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["QTY"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["TOTALC"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["AOE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["SA_PRICE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["RF_CUS"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["PT_OT"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["CR"].DefaultValue=false;
			
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
			if(this.textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.SA_CODE");
			if(this.textBox2.DataBindings.Count<=0)this.textBox2.DataBindings.Add("Text",this.DsMast,"Mast.PUR_PRICE");
			if(this.textBox3.DataBindings.Count<=0)this.textBox3.DataBindings.Add("Text",this.DsMast,"Mast.TOTALC");
			if(this.textBox4.DataBindings.Count<=0)this.textBox4.DataBindings.Add("Text",this.DsMast,"Mast.SA_PRICE");
			if(this.textBox5.DataBindings.Count<=0)this.textBox5.DataBindings.Add("Text",this.DsMast,"Mast.QTY");
			if(this.textBox6.DataBindings.Count<=0)this.textBox6.DataBindings.Add("Text",this.DsMast,"Mast.TOTAL_AMOUNT");
			if(this.textBox7.DataBindings.Count<=0)this.textBox7.DataBindings.Add("Text",this.DsMast,"Mast.AOE");
			if(this.textBox8.DataBindings.Count<=0)this.textBox8.DataBindings.Add("Text",this.DsMast,"Mast.PROFIT");
			if(this.textBox9.DataBindings.Count<=0)this.textBox9.DataBindings.Add("Text",this.DsMast,"Mast.REMARKS");
			if(this.textBox10.DataBindings.Count<=0)this.textBox10.DataBindings.Add("Text",this.DsMast,"Mast.CERTIFIED");
			if(this.textBox11.DataBindings.Count<=0)this.textBox11.DataBindings.Add("Text",this.DsMast,"Mast.RF_CUS");
			if(this.textBox12.DataBindings.Count<=0)this.textBox12.DataBindings.Add("Text",this.DsMast,"Mast.PT_OT");
			if(this.checkBox1.DataBindings.Count <=0)this.checkBox1.DataBindings.Add ("Checked",this.DsMast,"Mast.CR"); 

			
			this.dateTimePicker1.Value=System.DateTime.Now;
			if(this.dateTimePicker1.DataBindings.Count<=0)this.dateTimePicker1.DataBindings.Add("Text",this.DsMast,"Mast.SA_DATE");
			this.dateTimePicker2.Value=System.DateTime.Now;
			if(this.dateTimePicker2.DataBindings.Count<=0)this.dateTimePicker2.DataBindings.Add("Text",this.DsMast,"Mast.DUE_DATE");
			
			//if(this.checkBox1.DataBindings.Count<=0)this.checkBox1.DataBindings.Add("Checked", this.DsMast, "Mast.CR");
			
			//
			//Initial the ComboBoxs
			//
			
			System.Data.OleDb.OleDbDataAdapter DaTm=new System.Data.OleDb.OleDbDataAdapter ();
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from Customer ",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"Cus");
			this.comboBox1.DataSource=this.DsMast.Tables["Cus"];
			this.comboBox1.DisplayMember ="Cus_Name";
			this.comboBox1.ValueMember="Auto_No";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.CUS_ID");
			
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR011",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"PR");
			this.comboBox2.DataSource=this.DsMast.Tables["PR"].DefaultView;
			this.DsMast.Tables["PR"].DefaultView.Sort="FNAME";
			this.comboBox2.DisplayMember ="FNAME";
			this.comboBox2.ValueMember="AUTO_NO";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox2.DataBindings.Count<=0) this.comboBox2.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ID");
			
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
			DaSave.SelectCommand =new System.Data.OleDb.OleDbCommand("select * from SA" ,((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (DaSave);
			//MessageBox.Show (OleDbCommandBuilder1.GetUpdateCommand ().CommandText.ToString ());
			DaSave.RowUpdated += new System.Data.OleDb.OleDbRowUpdatedEventHandler(DaSave_RowUpdated);
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
				
				idCMD.CommandText="select TOTAL_AMOUNT,PROFIT,Cus_Name,PR_NAME,PR_IT_ID,IT_NAME,PR_CODE from SA010 where AUTO_NO="+e.Row["AUTO_NO"].ToString ();
				System.Data.OleDb.OleDbDataReader dr=idCMD.ExecuteReader();
				if(dr.Read ())
				{
					e.Row["TOTAL_AMOUNT"]=dr.GetValue(0);
					e.Row["PROFIT"]=dr.GetValue(1);
					
					e.Row["Cus_Name"]=dr.GetValue (2);
					e.Row["PR_NAME"]=dr.GetValue (3);
					e.Row["PR_IT_ID"]=dr.GetValue (4);
					e.Row["IT_NAME"]=dr.GetValue (5);
					e.Row["PR_CODE"]=dr.GetValue (6);
					
				}
				dr.Close ();
				dr=null;
				
				
				
			}
			idCMD=null;/**/
		}
		protected override void Search()
		{
			new star.SA.SA010SE(this).ShowDialog();
		}

		private void label2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Save();
			if(this.BindMast.Count>0 && this.BindMast.Position>=0)
			{
				System.Data.DataRowView drv=(System.Data.DataRowView)this.BindMast.Current ;
				new SA.SA011 (((wmMain)this.MdiParent).db,Convert.ToInt32 (drv[0])).ShowDialog (this);
			}
			else
			{
				MessageBox.Show ("Please choose right purchase orders in left data list.");
			}
			
		//	new PR.PR011 (((wmMain)this.MdiParent).db);
		}

	
	}
}

