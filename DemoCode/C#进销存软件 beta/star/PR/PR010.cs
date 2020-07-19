using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace star.PR
{
	public class PR010 : star.Public.Single
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
		private System.Windows.Forms.TextBox textBox11;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBox12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBox13;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.TextBox textBox14;
		private System.Windows.Forms.TextBox textBox15;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.ComponentModel.IContainer components = null;

		public PR010()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.btSearch.Visible =true;
			this.Text="Purchase Records";
			this.BasSql ="select * from PR010";
			this.SelSql ="";
			this.Text="Purchase Data";

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PR010));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label15 = new System.Windows.Forms.Label();
			this.textBox12 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBox13 = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.textBox14 = new System.Windows.Forms.TextBox();
			this.textBox15 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DsMast)).BeginInit();
			// 
			// panel1
			// 
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(614, 453);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(272, 451);
			// 
			// panel2
			// 
			this.panel2.Location = new System.Drawing.Point(272, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(340, 451);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.label18);
			this.panel3.Controls.Add(this.label17);
			this.panel3.Controls.Add(this.label20);
			this.panel3.Controls.Add(this.label16);
			this.panel3.Controls.Add(this.label15);
			this.panel3.Controls.Add(this.label14);
			this.panel3.Controls.Add(this.label13);
			this.panel3.Controls.Add(this.label12);
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
			this.panel3.Size = new System.Drawing.Size(144, 451);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.dateTimePicker2);
			this.panel4.Controls.Add(this.textBox15);
			this.panel4.Controls.Add(this.textBox14);
			this.panel4.Controls.Add(this.checkBox1);
			this.panel4.Controls.Add(this.button1);
			this.panel4.Controls.Add(this.textBox13);
			this.panel4.Controls.Add(this.textBox12);
			this.panel4.Controls.Add(this.textBox11);
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
			this.panel4.Size = new System.Drawing.Size(196, 451);
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
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Code:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(5, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Supplier:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label4
			// 
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(5, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Product:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(5, 85);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "Price:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Top;
			this.label6.Location = new System.Drawing.Point(5, 105);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(134, 20);
			this.label6.TabIndex = 5;
			this.label6.Text = "QTY:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Top;
			this.label7.Location = new System.Drawing.Point(5, 125);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134, 20);
			this.label7.TabIndex = 6;
			this.label7.Text = "Total Amount:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Top;
			this.label8.Location = new System.Drawing.Point(5, 145);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 20);
			this.label8.TabIndex = 7;
			this.label8.Text = "Freight:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Location = new System.Drawing.Point(5, 165);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(134, 20);
			this.label9.TabIndex = 8;
			this.label9.Text = "Comm:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label10
			// 
			this.label10.Dock = System.Windows.Forms.DockStyle.Top;
			this.label10.Location = new System.Drawing.Point(5, 185);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(134, 20);
			this.label10.TabIndex = 9;
			this.label10.Text = "A.O.E:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label11
			// 
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Location = new System.Drawing.Point(5, 205);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(134, 20);
			this.label11.TabIndex = 10;
			this.label11.Text = "Transportion";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label12
			// 
			this.label12.Dock = System.Windows.Forms.DockStyle.Top;
			this.label12.Location = new System.Drawing.Point(5, 225);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(134, 20);
			this.label12.TabIndex = 11;
			this.label12.Text = "Sub Total";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label13
			// 
			this.label13.Dock = System.Windows.Forms.DockStyle.Top;
			this.label13.Location = new System.Drawing.Point(5, 245);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(134, 20);
			this.label13.TabIndex = 12;
			this.label13.Text = "Tax:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label14
			// 
			this.label14.Dock = System.Windows.Forms.DockStyle.Top;
			this.label14.Location = new System.Drawing.Point(5, 265);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(134, 20);
			this.label14.TabIndex = 13;
			this.label14.Text = "Total:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Location = new System.Drawing.Point(5, 5);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(186, 20);
			this.textBox1.TabIndex = 14;
			this.textBox1.Text = "textBox1";
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(5, 87);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(186, 20);
			this.textBox2.TabIndex = 15;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox3.Location = new System.Drawing.Point(5, 107);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(186, 20);
			this.textBox3.TabIndex = 16;
			this.textBox3.Text = "textBox3";
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox4.Location = new System.Drawing.Point(5, 127);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(186, 20);
			this.textBox4.TabIndex = 17;
			this.textBox4.Text = "textBox4";
			// 
			// textBox5
			// 
			this.textBox5.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox5.Location = new System.Drawing.Point(5, 147);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(186, 20);
			this.textBox5.TabIndex = 18;
			this.textBox5.Text = "textBox5";
			// 
			// textBox6
			// 
			this.textBox6.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox6.Location = new System.Drawing.Point(5, 167);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(186, 20);
			this.textBox6.TabIndex = 19;
			this.textBox6.Text = "textBox6";
			// 
			// textBox7
			// 
			this.textBox7.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox7.Location = new System.Drawing.Point(5, 187);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(186, 20);
			this.textBox7.TabIndex = 20;
			this.textBox7.Text = "textBox7";
			// 
			// textBox8
			// 
			this.textBox8.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox8.Location = new System.Drawing.Point(5, 207);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(186, 20);
			this.textBox8.TabIndex = 21;
			this.textBox8.Text = "textBox8";
			// 
			// textBox9
			// 
			this.textBox9.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox9.Location = new System.Drawing.Point(5, 227);
			this.textBox9.Name = "textBox9";
			this.textBox9.ReadOnly = true;
			this.textBox9.Size = new System.Drawing.Size(186, 20);
			this.textBox9.TabIndex = 22;
			this.textBox9.Text = "textBox9";
			// 
			// textBox10
			// 
			this.textBox10.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox10.Location = new System.Drawing.Point(5, 247);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(186, 20);
			this.textBox10.TabIndex = 23;
			this.textBox10.Text = "textBox10";
			// 
			// textBox11
			// 
			this.textBox11.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox11.Location = new System.Drawing.Point(5, 267);
			this.textBox11.Name = "textBox11";
			this.textBox11.ReadOnly = true;
			this.textBox11.Size = new System.Drawing.Size(186, 20);
			this.textBox11.TabIndex = 24;
			this.textBox11.Text = "textBox11";
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox1.Location = new System.Drawing.Point(5, 45);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(186, 21);
			this.comboBox1.TabIndex = 25;
			this.comboBox1.Text = "comboBox1";
			// 
			// comboBox2
			// 
			this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBox2.Location = new System.Drawing.Point(5, 66);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(186, 21);
			this.comboBox2.TabIndex = 26;
			this.comboBox2.Text = "comboBox2";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(5, 25);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(186, 20);
			this.dateTimePicker1.TabIndex = 27;
			// 
			// label15
			// 
			this.label15.Dock = System.Windows.Forms.DockStyle.Top;
			this.label15.Location = new System.Drawing.Point(5, 285);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(134, 20);
			this.label15.TabIndex = 28;
			this.label15.Text = "Remarks:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox12
			// 
			this.textBox12.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox12.Location = new System.Drawing.Point(5, 287);
			this.textBox12.Name = "textBox12";
			this.textBox12.Size = new System.Drawing.Size(186, 20);
			this.textBox12.TabIndex = 29;
			this.textBox12.Text = "textBox12";
			// 
			// label16
			// 
			this.label16.Dock = System.Windows.Forms.DockStyle.Top;
			this.label16.Location = new System.Drawing.Point(5, 305);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(134, 20);
			this.label16.TabIndex = 30;
			this.label16.Text = "Certified:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textBox13
			// 
			this.textBox13.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox13.Location = new System.Drawing.Point(5, 307);
			this.textBox13.Name = "textBox13";
			this.textBox13.Size = new System.Drawing.Size(186, 20);
			this.textBox13.TabIndex = 31;
			this.textBox13.Text = "textBox13";
			// 
			// label17
			// 
			this.label17.Dock = System.Windows.Forms.DockStyle.Top;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label17.Location = new System.Drawing.Point(5, 345);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(134, 20);
			this.label17.TabIndex = 32;
			this.label17.Text = "Payable to Supplier:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label18
			// 
			this.label18.Dock = System.Windows.Forms.DockStyle.Top;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label18.Location = new System.Drawing.Point(5, 365);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(134, 20);
			this.label18.TabIndex = 33;
			this.label18.Text = "Other Payable:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label19
			// 
			this.label19.Dock = System.Windows.Forms.DockStyle.Top;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label19.Location = new System.Drawing.Point(5, 385);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(134, 20);
			this.label19.TabIndex = 34;
			this.label19.Text = "Due Date";
			this.label19.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Dock = System.Windows.Forms.DockStyle.Top;
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(5, 391);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(186, 20);
			this.dateTimePicker2.TabIndex = 35;
			// 
			// textBox14
			// 
			this.textBox14.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox14.Location = new System.Drawing.Point(5, 351);
			this.textBox14.Name = "textBox14";
			this.textBox14.Size = new System.Drawing.Size(186, 20);
			this.textBox14.TabIndex = 1;
			this.textBox14.Text = "textBox14";
			// 
			// textBox15
			// 
			this.textBox15.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox15.Location = new System.Drawing.Point(5, 371);
			this.textBox15.Name = "textBox15";
			this.textBox15.Size = new System.Drawing.Size(186, 20);
			this.textBox15.TabIndex = 36;
			this.textBox15.Text = "textBox15";
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.button1.Location = new System.Drawing.Point(5, 422);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(186, 24);
			this.button1.TabIndex = 39;
			this.button1.Text = "Payment";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label20
			// 
			this.label20.Dock = System.Windows.Forms.DockStyle.Top;
			this.label20.Location = new System.Drawing.Point(5, 325);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(134, 20);
			this.label20.TabIndex = 35;
			this.label20.Text = "Credit ?";
			this.label20.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// checkBox1
			// 
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkBox1.Location = new System.Drawing.Point(5, 327);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(186, 24);
			this.checkBox1.TabIndex = 40;
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PR010
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 501);
			this.Name = "PR010";
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
			DsMast.Tables ["Mast"].Columns["PRICE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["QTY"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["FREIGHT"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["COMM"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["AOE"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["TRAN"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["TAX"].DefaultValue=0;
			DsMast.Tables ["Mast"].Columns["PT_SUP"].DefaultValue=0;
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
			
			//
			//Initial TextBox;
			//
			if(this.textBox1.DataBindings.Count<=0)this.textBox1.DataBindings.Add("Text",this.DsMast,"Mast.PUR_CODE");
			if(this.textBox2.DataBindings.Count<=0)this.textBox2.DataBindings.Add("Text",this.DsMast,"Mast.PRICE");
			if(this.textBox3.DataBindings.Count<=0)this.textBox3.DataBindings.Add("Text",this.DsMast,"Mast.QTY");
			if(this.textBox4.DataBindings.Count<=0)this.textBox4.DataBindings.Add("Text",this.DsMast,"Mast.TOTAL_AMOUNT");
			if(this.textBox5.DataBindings.Count<=0)this.textBox5.DataBindings.Add("Text",this.DsMast,"Mast.FREIGHT");
			if(this.textBox6.DataBindings.Count<=0)this.textBox6.DataBindings.Add("Text",this.DsMast,"Mast.COMM");
			if(this.textBox7.DataBindings.Count<=0)this.textBox7.DataBindings.Add("Text",this.DsMast,"Mast.AOE");
			if(this.textBox8.DataBindings.Count<=0)this.textBox8.DataBindings.Add("Text",this.DsMast,"Mast.TRAN");
			if(this.textBox9.DataBindings.Count<=0)this.textBox9.DataBindings.Add("Text",this.DsMast,"Mast.GRASS_TOTAL");
			if(this.textBox10.DataBindings.Count<=0)this.textBox10.DataBindings.Add("Text",this.DsMast,"Mast.TAX");
			if(this.textBox11.DataBindings.Count<=0)this.textBox11.DataBindings.Add("Text",this.DsMast,"Mast.TOTAL");
			if(this.textBox12.DataBindings.Count<=0)this.textBox12.DataBindings.Add("Text",this.DsMast,"Mast.REMARKS");
			if(this.textBox13.DataBindings.Count<=0)this.textBox13.DataBindings.Add("Text",this.DsMast,"Mast.CERTIFIED");
			if(this.textBox14.DataBindings.Count<=0)this.textBox14.DataBindings.Add("Text",this.DsMast,"Mast.PT_SUP");
			if(this.textBox15.DataBindings.Count<=0)this.textBox15.DataBindings.Add("Text",this.DsMast,"Mast.PT_OT");
			if(this.checkBox1.DataBindings.Count <=0)this.checkBox1.DataBindings.Add ("Checked",this.DsMast,"Mast.CR"); 
			this.dateTimePicker1.Value=System.DateTime.Now;
			if(this.dateTimePicker1.DataBindings.Count<=0)this.dateTimePicker1.DataBindings.Add("Text",this.DsMast,"Mast.PUR_DATE");
			this.dateTimePicker2.Value=System.DateTime.Now;
			if(this.dateTimePicker2.DataBindings.Count<=0)this.dateTimePicker2.DataBindings.Add("Text",this.DsMast,"Mast.DUE_DATE");
			
		//	if(this.checkBox1.DataBindings.Count<=0)this.checkBox1.DataBindings.Add("Checked", this.DsMast, "Mast.CR");
			
			//
			//Initial the ComboBoxs
			//
			
			System.Data.OleDb.OleDbDataAdapter DaTm=new System.Data.OleDb.OleDbDataAdapter ();
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from Sup ",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"Sup");
			this.comboBox1.DataSource=this.DsMast.Tables["Sup"];
			this.comboBox1.DisplayMember ="Sup_Name";
			this.comboBox1.ValueMember="Auto_No";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox1.DataBindings.Count<=0) this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.SUP_ID");
			
			DaTm.SelectCommand =new System.Data.OleDb.OleDbCommand ("select * from PR011",((wmMain)this.MdiParent).db);
			DaTm.Fill(this.DsMast,"PR");
			this.comboBox2.DataSource=this.DsMast.Tables["PR"].DefaultView;
			this.DsMast.Tables["PR"].DefaultView.Sort="FNAME";
			this.comboBox2.DisplayMember ="FNAME";
			this.comboBox2.ValueMember="AUTO_NO";
			//	this.comboBox1.DataBindings.Clear(); this.comboBox1.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ITEM");
			if(comboBox2.DataBindings.Count<=0) this.comboBox2.DataBindings.Add("SelectedValue",DsMast,"Mast.PR_ID");
			
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
			DaSave.SelectCommand =new System.Data.OleDb.OleDbCommand("select * from PUR" ,((wmMain)this.MdiParent).db);
			System.Data.OleDb.OleDbCommandBuilder  OleDbCommandBuilder1 =new System.Data.OleDb.OleDbCommandBuilder (DaSave);
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
		//	this.Calc ();

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
				
				//newID = (int)idCMD.ExecuteScalar();
					//e.Row["AUTO_NO"] = newID;
				idCMD.CommandText="select TOTAL_AMOUNT,GRASS_TOTAL,TOTAL,Sup_Name,PR_NAME,PR_IT_ID,IT_NAME,PR_CODE from PR010 where AUTO_NO="+e.Row["AUTO_NO"];
				System.Data.OleDb.OleDbDataReader dr=idCMD.ExecuteReader();
				if(dr.Read ())
				{
					e.Row["TOTAL_AMOUNT"]=dr.GetValue(0);
					e.Row["GRASS_TOTAL"]=dr.GetValue(1);
					e.Row["TOTAL"]=dr.GetValue(2);
					e.Row["Sup_Name"]=dr.GetValue (3);
					e.Row["PR_NAME"]=dr.GetValue (4);
					e.Row["PR_IT_ID"]=dr.GetValue (5);
					e.Row["IT_NAME"]=dr.GetValue (6);
					e.Row["PR_CODE"]=dr.GetValue (7);
				}
				dr.Close ();
				dr=null;
			}
			idCMD=null;
		}
		protected override void Search()
		{
			new star.PR.PR010SE(this).ShowDialog();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			
			this.Save ();
			if(this.BindMast.Count>0 && this.BindMast.Position>=0)
			{
				System.Data.DataRowView drv=(System.Data.DataRowView)this.BindMast.Current ;
				new PR.PR011 (((wmMain)this.MdiParent).db,Convert.ToInt32 (drv[0])).ShowDialog (this);
			}
			else
			{
				MessageBox.Show ("Please choose right purchase orders in left data list.");
			}
		}
	}
}

