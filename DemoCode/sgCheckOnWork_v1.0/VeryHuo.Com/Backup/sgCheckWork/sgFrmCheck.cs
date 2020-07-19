using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;
using DevComponents.DotNetBar;
using sgCheckWork;
using System.Threading;

namespace sgFrmCheckWork
{
    public partial class sgFrmCheck : Office2007Form
    {
        SoundPlayer sp;
        public sgFrmCheck()
        {
            InitializeComponent();
            this.AcceptButton = this.btn_Check;
        }

        private void sgFrmCheck_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lbl_DateTime.Text = System.DateTime.Now.ToString();
            lbl_DateWeek.Text = sgClass.Common.getWeekDay();

            lbl_Info.Text = "当前考勤设置  上班时间:" + CheckSet.Default.Check_Work + "点,下班时间:" + CheckSet.Default.Check_UnWork+"点";
            txt_NumOrName.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_DateTime.Text = System.DateTime.Now.ToString();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            Regex Digital = new Regex("^[0-9]{1,}$"); //正则表达式 表示数字的范围 ^符号是开始，$是关闭

            if (string.IsNullOrEmpty(txt_NumOrName.Text) || !sgClass.DbHelperSQL.Exists("select * from [user] where ID='" + txt_NumOrName.Text + "' or Name='" + txt_NumOrName.Text + "'"))
            {
                try
                {
                    SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"\sound\NumOrName_Null.wav");
                    sndPlayer.Play();
                    MessageBox.Show("工号或姓名不存在!");
                    txt_NumOrName.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                string ID = null;
                string Sound = null;
                //string btnTime = null;

                //DateTime dt1, dt2;
                //TimeSpan ts;

                if (Digital.IsMatch(txt_NumOrName.Text.Trim()))
                {
                    ID = txt_NumOrName.Text.Trim();
                }
                else
                {
                    ID = sgClass.DbHelperSQL.Query("select ID from [user] where Name='" + txt_NumOrName.Text.Trim() + "'").Tables[0].Rows[0][0].ToString();
                }


                try
                {
                    Sound = sgClass.DbHelperSQL.Query("select Sound from [user] where ID=" + ID).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                string sqlIn = "insert into [check](checkDate,ID,checkContent) values(@checkDate,@ID,@checkContent)";
                string sqlUp = "update [check] set checkContent=checkContent+';'+@checkContent where checkDate=@checkDate and ID=@ID";

                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@checkDate",System.DateTime.Now.ToShortDateString()),
                    new SqlParameter("@ID",ID),
                    new SqlParameter("@checkContent",System.DateTime.Now.ToString())
                };

                if (sgClass.DbHelperSQL.Exists("select * from [check] where checkDate='" + System.DateTime.Now.ToShortDateString() + "' and ID='" + ID + "'"))
                {
                    string CheckContent = sgClass.DbHelperSQL.Query("select checkContent from [check] where checkDate='" + System.DateTime.Now.ToShortDateString() + "' and ID='" + ID + "'").Tables[0].Rows[0][0].ToString();

                    int CheckCount = CheckContent.Split(new string[] { ";" }, StringSplitOptions.None).Length+1;

                    string sMes = sgClass.DbHelperSQL.ExecuteSql(sqlUp, sp);
                    if (sMes == "1")
                    {
                        labNote.Text = "工号" + ID + "当日第" + CheckCount + "次考勤,考勤时间:" + System.DateTime.Now.ToString();
                        string[] wav = new string[] { Sound, "WorkNum.wav", "Num" + ID + ".wav", "CheckSusscess.wav" };
                        for (int i = 0; i < wav.Length; i++)
                        {
                            try
                            {
                                SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"\sound\" + wav[i] + "");
                                sndPlayer.PlaySync();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(sMes);
                    }
                }
                else
                {
                    string sMes = sgClass.DbHelperSQL.ExecuteSql(sqlIn, sp);
                    if (sMes == "1")
                    {
                        labNote.Text = "工号" + ID + "当日第1次考勤,考勤时间:" + System.DateTime.Now.ToString();
                        string[] wav = new string[] { Sound, "WorkNum.wav", "Num" + ID + ".wav", "CheckSusscess.wav" };
                        for (int i = 0; i < wav.Length; i++)
                        {
                            try
                            {
                                SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"\sound\" + wav[i] + "");
                                sndPlayer.PlaySync();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(sMes);
                    }
                }

            }
            txt_NumOrName.Text = string.Empty;

            }
        private void AudioPlay()
        {
            this.sp = new System.Media.SoundPlayer(Application.StartupPath + @"\LiChengWei.wav");
            this.sp.PlaySync();

            this.sp = new System.Media.SoundPlayer(Application.StartupPath + @"\WorkNum.wav");
            this.sp.PlaySync();

            this.sp = new System.Media.SoundPlayer(Application.StartupPath + @"\Num01.wav");
            this.sp.PlaySync();
        }

    }
}
