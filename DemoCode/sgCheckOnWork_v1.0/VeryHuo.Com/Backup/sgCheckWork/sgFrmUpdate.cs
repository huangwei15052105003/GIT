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
using System.IO;

namespace sgFrmCheckWork
{
    public delegate void upManData();
    public partial class sgFrmUpdate : Office2007Form
    {
        public string strID = null;
        public string strName = null;
        public string strPinYin = null;
        public string strSex = null;
        public string strInDate = null;
        public string strOutDate = null;
        public string strSound = null;

        public sgFrmUpdate()
        {
            InitializeComponent();
        }

        public sgFrmUpdate(string ID,string Name,string PinYin,string Sex,string InDate,string OutDate,string Sound)
        {
            InitializeComponent();
            strID = ID;
            strName = Name;
            strPinYin = PinYin;
            strSex = Sex;
            strInDate = InDate;
            strOutDate = OutDate;
            strSound = Sound;
        }

        public event upManData uMD;

        private void sgFrmUpdate_Load(object sender, EventArgs e)
        {
            txtID.Text = strID;
            txtName.Text = strName;
            txtPinYin.Text = strPinYin;
            comboBoxEx1.Text = strSex;
            dtiInDate.Text = strInDate;
            dtiOutDate.Text = strOutDate;
            txtSound.Text = strSound;
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
                        string sql = "update [user] set Name=@Name,PinYin=@PinYin,Sex=@Sex,InDate=@InDate,OutDate=@OutDate,Sound=@Sound where ID=@ID";
                        SqlParameter[] sp = new SqlParameter[]
                        {
                            new SqlParameter("@Name",txtName.Text),
                            new SqlParameter("@PinYin",txtPinYin.Text),
                            new SqlParameter("@Sex",Convert.ToString(comboBoxEx1.SelectedItem)),
                            new SqlParameter("@InDate",dtiInDate.Text),
                            new SqlParameter("@OutDate",dtiOutDate.Text),
                            new SqlParameter("@Sound",strSound),
                            new SqlParameter("@ID",txtID.Text)
                        };
                        string sMes = sgClass.DbHelperSQL.ExecuteSql(sql, sp);
                        if (sMes == "1")
                        {
                            DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                            b.Style = eBallonStyle.Balloon;
                            b.CaptionText = "OK";
                            b.Text = "用户信息修改成功!";
                            b.AlertAnimation = eAlertAnimation.TopToBottom;
                            b.AutoResize();
                            b.AutoClose = true;
                            b.AutoCloseTimeOut = 2;
                            b.Owner = this;
                            b.Show(labMessage, false);

                            uMD();
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
            //txtID.Text = string.Empty;
            //txtName.Text = string.Empty;
            //txtPinYin.Text = string.Empty;
            //comboBoxEx1.SelectedItem = null;
            //dtiInDate.Text = string.Empty;
            //dtiOutDate.Text = string.Empty;
            //txtSound.Text = string.Empty;
            this.Close();
        }

        private void bSound_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strSound))
            {
                DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
                b.Style = eBallonStyle.Balloon;
                b.CaptionText = "注意事项";
                b.Text = "暂无声音文件!";
                b.AlertAnimation = eAlertAnimation.TopToBottom;
                b.AutoResize();
                b.AutoClose = true;
                b.AutoCloseTimeOut = 2;
                b.Owner = this;
                b.Show(labMessage, false);
            }
            else
            {
                SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"\sound\" + strSound + "");
                sndPlayer.Play();
            }
        }

        private void bUSound_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtSound.Text = strSound = ofd.FileName.Split(new string[] { "\\" }, StringSplitOptions.None)[ofd.FileName.Split(new string[] { "\\" }, StringSplitOptions.None).Length-1].ToString();
                
                SoundPlayer sndPlayer = new System.Media.SoundPlayer(ofd.FileName);
                sndPlayer.Play();
            }

        }
    }
}
