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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LogType = 1;
        }
        //登录状态
        /// <summary>
        /// 表示是否登录成功,1:成功，2：失败 
        /// </summary>
        public int LogType { get; set; }


        private void LoginForm_Load(object sender, EventArgs e)
        {
          
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            BLLUsers.UserManagerServiceClient client = WCFServiceBLL.GetUserService();

          
            Model.UserModel userModel=client.Login(textBoxUserName.Text,textBoxPassword.Text);
            if (userModel!=null)
            {
                AppParams.CurrentUser = userModel;
                this.LogType = 1;
                this.Hide();
            }
            else
            {
                MessageBox.Show("登录失败，确认帐号密码是否正确");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.LogType = 1;
            this.Hide();
        }
    }
}
