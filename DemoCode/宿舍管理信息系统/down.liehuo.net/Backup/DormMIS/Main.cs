using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//Download by http://down.liehuo.net
namespace DormMIS
{
	/// <summary>
	/// Main 的摘要说明。
	/// </summary>
	public class Main : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		public System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		private System.Windows.Forms.StatusBarPanel statusBarPanel4;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public Main()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Main));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel4 = new System.Windows.Forms.StatusBarPanel();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem9,
																					  this.menuItem10,
																					  this.menuItem11,
																					  this.menuItem8});
			this.menuItem1.Text = "系统管理";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "添加用户";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.Text = "修改密码";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 2;
			this.menuItem11.Text = "重新登录";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 3;
			this.menuItem8.Text = "退出";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem12,
																					  this.menuItem13});
			this.menuItem2.Text = "宿舍基本信息";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 0;
			this.menuItem12.Text = "添加宿舍";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 1;
			this.menuItem13.Text = "查询宿舍";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem14,
																					  this.menuItem15});
			this.menuItem3.Text = "学生入住";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 0;
			this.menuItem14.Text = "学生入住";
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 1;
			this.menuItem15.Text = "学生查询";
			this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem16,
																					  this.menuItem17});
			this.menuItem4.Text = "卫生检查";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 0;
			this.menuItem16.Text = "添加检查";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 1;
			this.menuItem17.Text = "查询检查";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem18,
																					  this.menuItem19});
			this.menuItem5.Text = "水电收费";
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 0;
			this.menuItem18.Text = "添加";
			this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 1;
			this.menuItem19.Text = "查询";
			this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem20,
																					  this.menuItem21});
			this.menuItem6.Text = "房屋报修";
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 0;
			this.menuItem20.Text = "添加";
			this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
			// 
			// menuItem21
			// 
			this.menuItem21.Index = 1;
			this.menuItem21.Text = "查询";
			this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem22,
																					  this.menuItem23});
			this.menuItem7.Text = "外来人员登记";
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 0;
			this.menuItem22.Text = "添加";
			this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 1;
			this.menuItem23.Text = "查询";
			this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 371);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2,
																						  this.statusBarPanel3,
																						  this.statusBarPanel4});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(584, 22);
			this.statusBar1.TabIndex = 1;
			this.statusBar1.Text = "statusBar1";
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.Width = 60;
			// 
			// statusBarPanel3
			// 
			this.statusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanel3.Width = 200;
			// 
			// statusBarPanel4
			// 
			this.statusBarPanel4.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarPanel4.Width = 200;
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1,
																						this.toolBarButton2,
																						this.toolBarButton3,
																						this.toolBarButton4,
																						this.toolBarButton5,
																						this.toolBarButton6});
			this.toolBar1.ButtonSize = new System.Drawing.Size(60, 22);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(584, 28);
			this.toolBar1.TabIndex = 2;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.Text = "宿舍";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex = 1;
			this.toolBarButton2.Text = "入住";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.ImageIndex = 2;
			this.toolBarButton3.Text = "卫生";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.ImageIndex = 3;
			this.toolBarButton4.Text = "水电";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.ImageIndex = 4;
			this.toolBarButton5.Text = "报修";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.ImageIndex = 5;
			this.toolBarButton6.Text = "登记";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Main
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(584, 393);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "主界面";
			this.Load += new System.EventHandler(this.Main_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel4)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		AddUser addUser;
		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			addUser = new AddUser();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addUser.MdiParent = this;
			addUser.WindowState = FormWindowState.Maximized;
			addUser.Show();
		}

		ModifyCode modifyCode;
		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			modifyCode = new ModifyCode();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			modifyCode.MdiParent = this;
			modifyCode.Tag = this.statusBarPanel2.Text.Trim();
			modifyCode.WindowState = FormWindowState.Maximized;
			modifyCode.Show();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			((System.Windows.Forms.Form)this.Tag).Visible=true;
			this.Close();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		AddDorm addDorm;
		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			addDorm = new AddDorm();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addDorm.MdiParent = this;
			addDorm.WindowState = FormWindowState.Maximized;
			addDorm.Show();
		}

		Dorm dorm;
		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			dorm = new Dorm();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			dorm.MdiParent = this;
			dorm.WindowState = FormWindowState.Maximized;
			dorm.Show();
		}

		AddStudent addStudent;
		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			addStudent = new AddStudent();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addStudent.MdiParent = this;
			addStudent.WindowState = FormWindowState.Maximized;
			addStudent.Show();
		}

		Student student;
		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			student = new Student();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			student.MdiParent = this;
			student.WindowState = FormWindowState.Maximized;
			student.Show();
		}

		AddCheck addCheck;
		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			addCheck = new AddCheck();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addCheck.MdiParent = this;
			addCheck.WindowState = FormWindowState.Maximized;
			addCheck.Show();
		}

		Check check;
		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			check = new Check();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			check.MdiParent = this;
			check.WindowState = FormWindowState.Maximized;
			check.Show();
		}

		AddCharge addCharge;
		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			addCharge = new AddCharge();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addCharge.MdiParent = this;
			addCharge.WindowState = FormWindowState.Maximized;
			addCharge.Show();
		}

		Charge charge;
		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			charge = new Charge();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			charge.MdiParent = this;
			charge.WindowState = FormWindowState.Maximized;
			charge.Show();
		}

		AddRepair addRepair;
		private void menuItem20_Click(object sender, System.EventArgs e)
		{
			addRepair = new AddRepair();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addRepair.MdiParent = this;
			addRepair.WindowState = FormWindowState.Maximized;
			addRepair.Show();
		}

		Repair repair;
		private void menuItem21_Click(object sender, System.EventArgs e)
		{
			repair = new Repair();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			repair.MdiParent = this;
			repair.WindowState = FormWindowState.Maximized;
			repair.Show();
		}

		AddRegis addRegis;
		private void menuItem22_Click(object sender, System.EventArgs e)
		{
			addRegis = new AddRegis();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			addRegis.MdiParent = this;
			addRegis.WindowState = FormWindowState.Maximized;
			addRegis.Show();
		}

		Register register;
		private void menuItem23_Click(object sender, System.EventArgs e)
		{
			register = new Register();
			for(int x=0;x<this.MdiChildren.Length;x++)
			{
				Form tempChild = (Form)this.MdiChildren[x];
				tempChild.Close();
			}
			register.MdiParent = this;
			register.WindowState = FormWindowState.Maximized;
			register.Show();
		}

		private void Main_Load(object sender, System.EventArgs e)
		{
			this.statusBarPanel1.Text="当前登录用户";
			this.statusBarPanel3.Text=DateTime.Now.ToString();
			this.statusBarPanel4.Text="宿舍管理信息系统";
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(toolBar1.Buttons.IndexOf(e.Button))
			{
				case 0:
					Form addDorm = new AddDorm();
					for(int x=0;x<this.MdiChildren.Length;x++)
					{
						Form tempChild = (Form)this.MdiChildren[x];
						tempChild.Close();
					}
					addDorm.MdiParent = this;
					addDorm.WindowState = FormWindowState.Maximized;
					addDorm.Show();
					break;
				case 1:
					Form addStudent = new AddStudent();
					for(int x=0;x<this.MdiChildren.Length;x++)
					{
						Form tempChild = (Form)this.MdiChildren[x];
						tempChild.Close();
					}
					addStudent.MdiParent = this;
					addStudent.WindowState = FormWindowState.Maximized;
					addStudent.Show();
					break;
				case 2:
					Form addCheck = new AddCheck();
					for(int x=0;x<this.MdiChildren.Length;x++)
					{
						Form tempChild = (Form)this.MdiChildren[x];
						tempChild.Close();
					}
					addCheck.MdiParent = this;
					addCheck.WindowState = FormWindowState.Maximized;
					addCheck.Show();
					break;
				case 3:
					Form addCharge = new AddCharge();
					for(int x=0;x<this.MdiChildren.Length;x++)
					{
						Form tempChild = (Form)this.MdiChildren[x];
						tempChild.Close();
					}
					addCharge.MdiParent = this;
					addCharge.WindowState = FormWindowState.Maximized;
					addCharge.Show();
					break;
				case 4:
					Form addRepair = new AddRepair();
					for(int x=0;x<MdiChildren.Length;x++)
					{
						Form tempChild = (Form)MdiChildren[x];
						tempChild.Close();
					}
					addRepair.MdiParent = this;
					addRepair.WindowState = FormWindowState.Maximized;
					addRepair.Show();
					break;
				case 5:
					Form addRegis = new AddRegis();
					for(int x=0;x<MdiChildren.Length;x++)
					{
						Form tempChild = (Form)MdiChildren[x];
						tempChild.Close();
					}
					addRegis.MdiParent = this;
					addRegis.WindowState = FormWindowState.Maximized;
					addRegis.Show();
					break;
			}
		}
	}
}
