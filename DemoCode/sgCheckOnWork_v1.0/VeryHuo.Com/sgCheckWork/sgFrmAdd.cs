using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Media;

namespace sgFrmCheckWork
{
    public delegate void aupManData();
    public partial class sgFrmAdd : Office2007Form
    {
        public sgFrmAdd()
        {
            InitializeComponent();
        }
        public event aupManData auMD;
        private void sgFrmAdd_Load(object sender, EventArgs e)
        {

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                b.Style = eBallonStyle.Balloon;
                b.CaptionText = "ERROR";
                b.Text = "工号不能为空!";
                b.AlertAnimation = eAlertAnimation.TopToBottom;
                b.AutoResize();
                b.AutoClose = true;
                b.AutoCloseTimeOut = 2;
                b.Owner = this;
                b.Show(txtID, false);
            }
            else
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                    b.Style = eBallonStyle.Balloon;
                    b.CaptionText = "ERROR";
                    b.Text = "姓名不能为空!";
                    b.AlertAnimation = eAlertAnimation.TopToBottom;
                    b.AutoResize();
                    b.AutoClose = true;
                    b.AutoCloseTimeOut = 2;
                    b.Owner = this;
                    b.Show(txtName, false);
                }
                else
                {
                    if (string.IsNullOrEmpty(Convert.ToString(comboBoxEx1.SelectedItem)))
                    {
                        DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                        b.Style = eBallonStyle.Balloon;
                        b.CaptionText = "ERROR";
                        b.Text = "请选择性别!";
                        b.AlertAnimation = eAlertAnimation.TopToBottom;
                        b.AutoResize();
                        b.AutoClose = true;
                        b.AutoCloseTimeOut = 2;
                        b.Owner = this;
                        b.Show(comboBoxEx1, false);
                    }
                    else
                    {
                        string sql = "insert into [user](ID,Name,PinYin,Sex,InDate,OutDate,Sound) values(@ID,@Name,@PinYin,@Sex,@InDate,@OutDate,@Sound)";
                        SqlParameter[] sp = new SqlParameter[]
                        {
                            new SqlParameter("@ID",txtID.Text),
                            new SqlParameter("@Name",txtName.Text),
                            new SqlParameter("@PinYin",txtPinYin.Text),
                            new SqlParameter("@Sex",Convert.ToString(comboBoxEx1.SelectedItem)),
                            new SqlParameter("@InDate",dtiInDate.Text),
                            new SqlParameter("@OutDate",dtiOutDate.Text),
                            new SqlParameter("@Sound",txtSound.Text),
                        };
                        string sMes = sgClass.DbHelperSQL.ExecuteSql(sql, sp);
                        if (sMes == "1")
                        {
                            DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                            b.Style = eBallonStyle.Balloon;
                            b.CaptionText = "OK";
                            b.Text = "用户添加成功!";
                            b.AlertAnimation = eAlertAnimation.TopToBottom;
                            b.AutoResize();
                            b.AutoClose = true;
                            b.AutoCloseTimeOut = 2;
                            b.Owner = this;
                            b.Show(bAdd, false);

                            auMD();
                        }
                        else
                        {
                            MessageBox.Show(sMes);
                        }
                    }
                }
            }
        }

        private void txtID_MouseClick(object sender, MouseEventArgs e)
        {
            DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
            b.Style = eBallonStyle.Balloon;
            b.CaptionText = "注意事项";
            b.Text = "请输入3位工号,例如001";
            b.AlertAnimation = eAlertAnimation.TopToBottom;
            b.AutoResize();
            b.AutoClose = true;
            b.AutoCloseTimeOut = 2;
            b.Owner = this;
            b.Show(txtID, false);
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPinYin.Text = string.Empty;
            comboBoxEx1.SelectedItem = null;
            dtiInDate.Text = string.Empty;
            dtiOutDate.Text = string.Empty;
            txtSound.Text = string.Empty;
        }

        private void bUSound_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtSound.Text = ofd.FileName.Split(new string[] { "\\" }, StringSplitOptions.None)[ofd.FileName.Split(new string[] { "\\" }, StringSplitOptions.None).Length - 1].ToString();

                SoundPlayer sndPlayer = new System.Media.SoundPlayer(ofd.FileName);
                sndPlayer.Play();
            }
        }

        private void bSound_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSound.Text))
            {
                DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                b.Style = eBallonStyle.Balloon;
                b.CaptionText = "注意事项";
                b.Text = "请先选取声音文件!";
                b.AlertAnimation = eAlertAnimation.TopToBottom;
                b.AutoResize();
                b.AutoClose = true;
                b.AutoCloseTimeOut = 2;
                b.Owner = this;
                b.Show(bSound, false);
            }
            else
            {
                SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"\sound\" + txtSound.Text + "");
                sndPlayer.Play();
            }
        }
    }
}
