using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Insigma.Eyes.PSS.WinUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 库存管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (AppParams.CurrentUser.Role==0||AppParams.CurrentUser.Role==3)
            {
                LoadControls.LoadInventory(panelContainer);
            }
            else
            {
                MessageBox.Show("你没有权限");
            }
        }
        /// <summary>
        /// 采购管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (AppParams.CurrentUser.Role==0||AppParams.CurrentUser.Role==1)
            {
                LoadControls.LoadPurchase(panelContainer);
            }
            else
            {
                MessageBox.Show("你没有权限");
            }
        }
        /// <summary>
        /// 销售管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (AppParams.CurrentUser.Role==0||AppParams.CurrentUser.Role==2)
            {
                LoadControls.LoadSales(panelContainer);
            }
            else
            {
                MessageBox.Show("你没有权限");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
            //登录这儿如果写DialogResult=DialogResult.OK 会有问题，若点击叉，则程序无法释放
            if (login.LogType == 1)
            {
                login.Close();//登录成功
            }
            if (login.LogType == 2)//登录失败
            {
                this.Close();
            }
        }
    }
}
