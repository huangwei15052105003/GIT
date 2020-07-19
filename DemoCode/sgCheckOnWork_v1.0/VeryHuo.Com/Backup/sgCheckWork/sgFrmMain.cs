using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace sgFrmCheckWork
{
    public partial class sgFrmMain : Office2007Form
    {
        private static sgFrmConfig sgFrmConfig;
        private static sgFrmCheck sgFrmCheck;
        private static sgFrmManage sgFrmManage;
        private static sgFrmSeo sgFrmSeo;
        public sgFrmMain()
        {
            InitializeComponent();
        }

        private void sgFrmMain_Load(object sender, EventArgs e)
        {

        }

        private sgFrmConfig getFrmConfig()
        {
            if (sgFrmConfig == null || sgFrmConfig.IsDisposed)
            {
                sgFrmConfig = new sgFrmConfig();
                sgFrmConfig.MdiParent = this;
            }
            else
            {
                sgFrmConfig.Activate();
            }
            return sgFrmConfig;
        }

        private sgFrmSeo getFrmSeo()
        {
            if (sgFrmSeo == null || sgFrmSeo.IsDisposed)
            {
                sgFrmSeo = new sgFrmSeo();
                sgFrmSeo.MdiParent = this;
            }
            else
            {
                sgFrmSeo.Activate();
            }
            return sgFrmSeo;
        }

        private sgFrmCheck getCheck()
        {
            if (sgFrmCheck == null || sgFrmCheck.IsDisposed)
            {
                sgFrmCheck = new sgFrmCheck();
                sgFrmCheck.MdiParent = this;
            }
            else
            {
                sgFrmCheck.Activate();
            }
            return sgFrmCheck;
        }

        private sgFrmManage getManage()
        {
            if (sgFrmManage == null || sgFrmManage.IsDisposed)
            {
                sgFrmManage = new sgFrmManage();
                sgFrmManage.MdiParent = this;
            }
            else
            {
                sgFrmManage.Activate();
            }
            return sgFrmManage;
        }

        //private void bCheck_Click(object sender, EventArgs e)
        //{
        //    sgFrmCheck sgFrmCheck = getCheck();
        //    sgFrmCheck.Show();
        //}

        private void bConfig_Click(object sender, EventArgs e)
        {
            sgFrmConfig sgFrmConfig = getFrmConfig();
            sgFrmConfig.Show();
        }

        private void bManage_Click(object sender, EventArgs e)
        {
            sgFrmManage sgFrmManage = getManage();
            sgFrmManage.Show();
        }

        private void b_Check_Click(object sender, EventArgs e)
        {
            sgFrmCheck sgFrmCheck = getCheck();
            sgFrmCheck.Show();
        }

        private void b_Seo_Click(object sender, EventArgs e)
        {
            sgFrmSeo sgFrmSeo = getFrmSeo();
            sgFrmSeo.Show();
        }

        private void b_Manage_Click(object sender, EventArgs e)
        {
            sgFrmManage sgFrmManage = getManage();
            sgFrmManage.Show();
        }
    }
}
