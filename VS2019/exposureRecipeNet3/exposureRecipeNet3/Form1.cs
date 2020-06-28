using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Printing;

using System.Collections;

using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Reflection;

namespace exposureRecipeNet3
{

    public partial class Form1 : Form
    {
        public xlsData data;
        public MapPlot wfr;
        public check myCheck;
        static string connStr = "data source=" + @"p:\_SQLite\Flow.DB";
        public string printPicPath;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.SendToBack();
            pictureBox1.Visible = false;
            this.WindowState = FormWindowState.Maximized;
        }

        #region biastable & coordinate
        private void r_ready_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 8)
            {
                MessageBox.Show("产品名长度小于8位，请重新设置");
                return;
            }

            string file;
            data = new xlsData(textBox1.Text.Trim().ToUpper());

            file = xlsData.flowPath + data.part + ".xls";
            if (System.IO.File.Exists(file))
            {
                fFlow.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fFlow.BackColor = System.Drawing.Color.Red;
            }

            file = xlsData.btPath + data.part + ".xls";
            if (System.IO.File.Exists(file))
            {
                fBT.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fBT.BackColor = System.Drawing.Color.Red;
            }

            file = xlsData.zbPath + data.part + ".txt";
            if (System.IO.File.Exists(file))
            {
                fCoordinate.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fCoordinate.BackColor = System.Drawing.Color.Red;
            }

            file = xlsData.recipePath + data.part;
            if (System.IO.File.Exists(file))
            {
                fRecipe.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fRecipe.BackColor = System.Drawing.Color.Red;
            }
            MessageBox.Show("请查看标记颜色，确认相关文件是否存在");
        }

        private void r_read_Click(object sender, EventArgs e)
        {




            data = new xlsData(textBox1.Text.Trim().ToUpper(), "DUMMY");
            //  if (data.dtFlow.Rows.Count > 10)
            if (!(data.dtFlow is null))
            {
                fFlow1.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fFlow1.BackColor = System.Drawing.Color.Red;
            }

            if (!(data.dtBt is null))
            {
                fBT1.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fBT1.BackColor = System.Drawing.Color.Red;
            }

            if (!(data.dtZb is null))
            {

                fCoordinate1.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                fCoordinate1.BackColor = System.Drawing.Color.Red;
            }
            MessageBox.Show("请查看标记颜色，确认相关流程，BiasTable中的工作表是否正确读取");
        }

        private void r_table_Click(object sender, EventArgs e)
        {
            if (data is null)
            {
                MessageBox.Show("未读取流程和BiasTable等相关数据，退出"); return;
            }

            //dtFlow 原流程；data.dt 精简后流程

            if (data.dtFlow is null)
            { MessageBox.Show("流程未正确读取,退出"); return; }
            else
            {
                if (data.FlowToDataTable())
                { }
                else
                {
                    MessageBox.Show("流程数据格式不对，退出");
                    return;
                }
            }



            //dtBT 原bias Table，dt1，处理后    
            if (data.dtBt is null)
            { MessageBox.Show("BiasTable未正确读取,退出"); return; }
            else
            {
                if (data.BiastableToDataTable())
                {
                }
                else
                { MessageBox.Show("BiasTable格式不对，退出"); return; }
            }




            //判读 dt1中的工艺代码是否唯一
            if (data.dt1.DefaultView.ToTable(true, "code").Rows.Count == 1)
            { }
            else
            { MessageBox.Show("注意，Bias Table各层次标注的工艺代码不唯一；\r\n\r\n后续以第一层的为准"); }
            //合并
            data.MergeFlowBiasTable();
            //定义对位方法；
            data.AlignMethod();
            data.AlignTo();
            dataGridView1.DataSource = data.dt;
            MessageBox.Show("===DONE===");
            //确认是否需要保存
            if (MessageBox.Show("是否需要保存合并的流程，BiasTable？", "确认产品名正确！！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                pubfunction.saveMergeTable(data);
            }


        }
        private void r_coordinate_Click(object sender, EventArgs e)
        {
            if (data is null || data.dtZb is null || data.dt is null)
            { MessageBox.Show("请先完成BiasTable，流程等读取转换。。。。"); return; }
            else
            {
                if (data.MatchCoordinate())
                {
                    //大视场坐标转换
                    if (radioButtonLarge.Checked)
                    {
                        //为大视场坐标转换
                        double sx;
                        try
                        { sx = Convert.ToDouble(textBoxSx.Text); sx = sx / 2; }
                        catch
                        { MessageBox.Show("大视场产品请输入正确的Step X,退出"); return; }

                        pubfunction.ConverLargeFieldCoordinate(ref sx, ref data);
                    }


                    dataGridView1.DataSource = data.dt2;
                    MessageBox.Show("===DONE===");
                    if (MessageBox.Show("是否需要保存坐标数据？", "确认产品名正确！！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        pubfunction.saveZb(data);
                    }
                }
                else
                {
                    MessageBox.Show("BiasTable中定义的被对位层次在坐标文件中不存在；\n\n坐标读取失败；");
                }
            }
        }


        private void saveBt_Click(object sender, EventArgs e)
        {
            pubfunction.saveMergeTable(data);
        }
        private void saveZb_Click(object sender, EventArgs e)
        {
            pubfunction.saveZb(data);
        }


        private void button1_Click(object sender, EventArgs e) //display asml map
        {
            try
            {
                pictureBox1.Image = null;
                FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Asml.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
                pictureBox1.BringToFront(); pictureBox1.Visible = true;
                dataGridView1.SendToBack(); dataGridView1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n尚未生成图像文件，退出");
            }
        }
        private void button2_Click(object sender, EventArgs e) //display largefield nikon map
        {
            try
            {
                pictureBox1.Image = null;
                FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Nikon.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
                pictureBox1.BringToFront(); pictureBox1.Visible = true;
                dataGridView1.SendToBack(); dataGridView1.Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Code: " + ex.Message + "\n\n\n\n尚未生成图像文件;\n\n或非大视场产品，无Nikon Map\n\n\n\n退出");
            }
        }
        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible == false)
            {
                dataGridView1.BringToFront(); dataGridView1.Visible = true;
                pictureBox1.SendToBack(); pictureBox1.Visible = false;
            }
            else
            {
                pictureBox1.BringToFront(); pictureBox1.Visible = true;
                dataGridView1.SendToBack(); dataGridView1.Visible = false;
            }
        }
        #endregion

        #region data
        private void 显示原始流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dtFlow;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }

        private void 显示原始BiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dtBt;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }
        private void 显示原始坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dtZb;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }
        private void 显示转换后流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dt0;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }

        private void 显示转换后BiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dt1;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }
        private void 显示读入的坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data.dtZb;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;
        }













        #endregion


        private void r_map1_Click(object sender, EventArgs e)
        {
            data = null;
            string parttype;
            if (radioButtonNormal.Checked)
            { parttype = "normalField"; }
            else if (radioButtonLarge.Checked)
            { parttype = "largeField"; }
            else if (radioButtonMpw.Checked)
            { parttype = "mpw"; }
            else
            { parttype = "spliteGate"; }


            try
            {
                float ox = (float)Convert.ToDouble(textBoxOx.Text) / 1000;
                float oy = (float)Convert.ToDouble(textBoxOy.Text) / 1000;
                wfr = new MapPlot(Convert.ToDouble(textBoxR.Text),
                              Convert.ToDouble(textBoxO.Text),
                              Convert.ToDouble(textBoxSx.Text) / 1000,
                              Convert.ToDouble(textBoxSy.Text) / 1000,
                              Convert.ToDouble(textBoxDx.Text) / 1000,
                              Convert.ToDouble(textBoxDy.Text) / 1000,
                              Convert.ToDouble(textBoxP.Text),
                              parttype, textBox1.Text.ToString().Trim().ToUpper(),
                              Convert.ToDouble(textBoxArea.Text), radioButtonAuto.Checked,
                              ox, oy);

            }
            catch (Exception ex)
            { MessageBox.Show("ERROR CODE:\n\n" + ex.Message + "\n\n\n\n请先输入Step Size，Die Size等相关数据\n\n\n\n退出!"); return; }

            wfr.CalculatOffset();
            dataGridView1.DataSource = wfr.dt;

        }
        private void r_map2_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = null;
            if (wfr is null)
            { MessageBox.Show("请先输入数据，计算Map Offset等参数"); return; }

            wfr.plotAsmlMap();
            FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Asml.emf", FileMode.Open, FileAccess.Read);
            pictureBox1.Image = Image.FromStream(fs);
            fs.Close();
            pictureBox1.BringToFront(); pictureBox1.Visible = true;
            dataGridView1.SendToBack(); dataGridView1.Visible = false;


        }






        #region pub
        private void textBoxSy_TextChanged(object sender, EventArgs e)
        {

            if (radioButtonMpw.Checked)
            {
                textBoxDx.Text = textBoxSx.Text;
                textBoxDy.Text = textBoxSy.Text;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (Label x in new Label[] { fFlow, fFlow1, fBT, fBT1, fCoordinate, fCoordinate1, fRecipe, fRecipe1, fSize, fPreAlign, fBlue, fCHK1, fCHK2, fREMA, fLocation, fLocation1 })
            { x.BackColor = System.Drawing.Color.Red; }


        }

        #endregion


        #region illumination

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e) //查询
        {

            string tech = Interaction.InputBox("请输入工艺代码（通配符查询）", "定义工艺", "", -1, -1);
            tech = tech.Trim().ToUpper();

            string layer = Interaction.InputBox("请输入层次（通配符查询）", "定义层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            string track = Interaction.InputBox("请输入TrackRecipe（通配符查询）", "定义TrackRecipe", "", -1, -1);
            track = track.Trim().ToUpper();

            MessageBox.Show("查询关键字\r\n\r\n" +
                "    工艺：       " + tech + "\r\n\r\n" +
                "    层次：       " + layer + "\r\n\r\n" +
                "    TrackRecipe：" + track);
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_ILLUMINATION WHERE TECH LIKE '%" + tech + "%' AND LAYER LIKE '%" + layer + "%' AND TRACK LIKE '%" + track + "%'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            DataTable tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件");

                            }

                            dataGridView1.DataSource = tblTmp;
                            tech = layer = track = "";

                        }
                    }
                }
            }



        }

        private void 插入或更新ToolStripMenuItem_Click(object sender, EventArgs e)//插入
        {
            DataTable dt = new DataTable();
            foreach (string item in new string[] { "Tech", "Layer", "Track", "Key", "Type", "NA", "Outer", "Inner", "Comment", "Key1" })
            {
                dt.Columns.Add(item);
            }
            string tech = Interaction.InputBox("请输入12位工艺代码", "", "", -1, -1);
            tech = tech.Trim().ToUpper();
            if (tech.Length != 12)
            { MessageBox.Show("工艺代码长度必须为12为，退出，请确认"); return; }

            string layer = Interaction.InputBox("请输入层次名", "", "", -1, -1);
            layer = layer.Trim().ToUpper();

            string track = Interaction.InputBox("请输入TrackRecipe", "", "", -1, -1);
            track = track.Trim().ToUpper();

            string type = Interaction.InputBox("请输入照明类型，只能输入 ANN 或 CON", "", "", -1, -1);
            type = type.Trim().ToUpper();

            if (type.Length == 3 && (type == "ANN" || type == "CON"))
            { }
            else
            { MessageBox.Show("Illumination Type只能输入\"CON\"或\"ANN\",退出，请重新输入"); return; }

            double aperture = 0, outer = 0, inner = 0;


            try
            {

                aperture = Convert.ToDouble(Interaction.InputBox("请输入照明NA", "", "", -1, -1));
                outer = Convert.ToDouble(Interaction.InputBox("请输入Conventional Sigma:\n或Annular Outer Sigma ", "", "", -1, -1));
                if (type == "ANN")
                {
                    inner = Convert.ToDouble(Interaction.InputBox("请输入Annular Inner Sigma ", "", "", -1, -1));
                }

            }
            catch
            {
                MessageBox.Show("请输入数字"); return;
            }

            string comment = Interaction.InputBox("请输入备注 ", "", "", -1, -1);
            comment = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "_" + comment;


            DataRow newRow = dt.NewRow();
            newRow["Tech"] = tech;
            newRow["Layer"] = layer;
            newRow["Track"] = track;
            newRow["Key"] = "";
            newRow["Type"] = type;
            newRow["NA"] = aperture;
            newRow["Outer"] = outer;
            newRow["Inner"] = inner;
            newRow["Comment"] = comment;
            dt.Rows.Add(newRow);
            dataGridView1.DataSource = dt;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;



            #region //判断是否已有数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_ILLUMINATION WHERE SUBSTR(TECH,1,3)='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                if (MessageBox.Show("需要更新的数据是：\r\n\r\nTECH: " + tech + "\r\nLAYER: " + layer + "\r\nTrackRecipe: " + track + "\r\nIllumination: " + type + "\r\nNA: " + aperture.ToString() + "\r\nOuterSigma: " + outer.ToString() + "\r\nInnerSigma: " + inner.ToString() + "\r\nComment: " + comment + "\r\n\r\n确认更改选\"是(Y)\"", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                DataTable tmpDt = ds.Tables[0];
                                if (tmpDt.Rows.Count > 0)
                                {
                                    dataGridView1.DataSource = tmpDt;
                                    if (MessageBox.Show("数据库中已有其它设置，请确认是否需要删除并插入新设置", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        //有数据，待删除更新
                                    }
                                    else
                                    {
                                        MessageBox.Show("已选则不更新，退出"); return;
                                    }


                                }



                            }
                        }
                    }
                }
            }
            #endregion
            #region //删除数据

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            #endregion
            #region //插入数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " INSERT  INTO TBL_ILLUMINATION (TECH,LAYER,TRACK,KEY,TYPE,NA,OUTER,INNER,COMMENT,KEY1) VALUES ('" + tech + "','" + layer + "','" + track + "','" + tech + "_" + layer + "_" + track + "','" + type.Substring(0, 3).ToUpper() + "'," + aperture.ToString() + "," + outer.ToString() + "," + inner.ToString() + ",'" + comment + "','" + tech.Substring(1, 2) + "')";

                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " INSERT  INTO TBL_ILLUMINATION (TECH,LAYER,TRACK,KEY,TYPE,NA,OUTER,INNER,COMMENT,KEY1) VALUES ('" + tech.Substring(0, 3) + "','" + layer + "','" + track + "','" + tech.Substring(0, 3) + "_" + layer + "_" + track + "','" + type.Substring(0, 3).ToUpper() + "'," + aperture.ToString() + "," + outer.ToString() + "," + inner.ToString() + ",'" + comment + "','" + tech.Substring(1, 2) + "')";

                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            MessageBox.Show("数据（删除）插入结束");

            #endregion

        }



        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (string item in new string[] { "Tech", "Layer", "Track", "Key", "Type", "NA", "Outer", "Inner", "Comment", "Key1" })
            {
                dt.Columns.Add(item);
            }
            string tech = Interaction.InputBox("请输入12位工艺代码", "", "", -1, -1);
            tech = tech.Trim().ToUpper();
            if (tech.Length != 12)
            { MessageBox.Show("工艺代码长度必须为12为，退出，请确认"); return; }

            string layer = Interaction.InputBox("请输入层次名", "", "", -1, -1);
            layer = layer.Trim().ToUpper();

            string track = Interaction.InputBox("请输入TrackRecipe", "", "", -1, -1);
            track = track.Trim().ToUpper();

            string type = Interaction.InputBox("请输入照明类型，只能输入 ANN 或 CON", "", "", -1, -1);
            type = type.Trim().ToUpper();

            if (type.Length == 3 && (type == "ANN" || type == "CON"))
            { }
            else
            { MessageBox.Show("Illumination Type只能输入\"CON\"或\"ANN\",退出，请重新输入"); return; }

            double aperture = 0, outer = 0, inner = 0;


            try
            {

                aperture = Convert.ToDouble(Interaction.InputBox("请输入照明NA", "", "", -1, -1));
                outer = Convert.ToDouble(Interaction.InputBox("请输入Conventional Sigma:\n或Annular Outer Sigma ", "", "", -1, -1));
                if (type == "ANN")
                {
                    inner = Convert.ToDouble(Interaction.InputBox("请输入Annular Inner Sigma ", "", "", -1, -1));
                }

            }
            catch
            {
                MessageBox.Show("请输入数字"); return;
            }

            string comment = Interaction.InputBox("请输入备注 ", "", "", -1, -1);
            comment = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "_" + comment;


            DataRow newRow = dt.NewRow();
            newRow["Tech"] = tech;
            newRow["Layer"] = layer;
            newRow["Track"] = track;
            newRow["Key"] = "";
            newRow["Type"] = type;
            newRow["NA"] = aperture;
            newRow["Outer"] = outer;
            newRow["Inner"] = inner;
            newRow["Comment"] = comment;
            dt.Rows.Add(newRow);
            dataGridView1.DataSource = dt;
            dataGridView1.BringToFront(); dataGridView1.Visible = true;
            pictureBox1.SendToBack(); pictureBox1.Visible = false;



            #region //判断是否已有数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_ILLUMINATION WHERE SUBSTR(TECH,1,3)='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                if (MessageBox.Show("需要删除的数据是：\r\n\r\nTECH: " + tech + "\r\nLAYER: " + layer + "\r\nTrackRecipe: " + track + "\r\nIllumination: " + type + "\r\nNA: " + aperture.ToString() + "\r\nOuterSigma: " + outer.ToString() + "\r\nInnerSigma: " + inner.ToString() + "\r\nComment: " + comment + "\r\n\r\n确认更改选\"是(Y)\"", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                DataTable tmpDt = ds.Tables[0];
                                if (tmpDt.Rows.Count > 0)
                                {
                                    dataGridView1.DataSource = tmpDt;
                                    if (MessageBox.Show("数据库中已有其它设置，请确认是否需要删除", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        //有数据，待删除
                                    }
                                    else
                                    {
                                        MessageBox.Show("已选则不删除，退出"); return;
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("无数据，不用删除，退出"); return;
                                }



                            }
                        }
                    }
                }
            }
            #endregion
            #region //删除数据

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            MessageBox.Show("删除完成");
            #endregion

        }


        #endregion

        #region check
        private void r_check_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length < 8)
            {
                MessageBox.Show("产品名长度小于8位，请重新设置"); return;
            }
            foreach (Label x in new Label[] { fSize, fPreAlign, fBlue, fCHK1, fCHK2, fREMA, fLocation, fLocation1 })
            { x.BackColor = System.Drawing.Color.Red; }


            string part = textBox1.Text.Trim().ToUpper();
            bool flag = radioButtonLarge.Checked;
            bool flagRevisedFlow = false;//改版流程

            if (checkBox2.Checked)
            {
                flagRevisedFlow = true;
            }



            if (!(checkBox1.Checked))
            {
                myCheck = new check(part, flag, flagRevisedFlow);//第三参数无用，
            }
            else
            {
                if (!(textBoxMLM.Text.Trim() == "2" || textBoxMLM.Text.Trim() == "3" | textBoxMLM.Text.Trim() == "4"))
                { MessageBox.Show("请正确输入复合版相关信息"); return; }
                int mlm; double x1, x2, y1, y2;
                try
                {
                    mlm = Convert.ToInt16(textBoxMLM.Text.Trim());
                    x1 = Convert.ToDouble(textBoxX1.Text.Trim());
                    y1 = Convert.ToDouble(textBoxY1.Text.Trim());
                    x2 = Convert.ToDouble(textBoxX2.Text.Trim());
                    y2 = Convert.ToDouble(textBoxY2.Text.Trim());
                    myCheck = new check(part, flag, mlm, x1, y1, x2, y2, flagRevisedFlow);
                }
                catch { MessageBox.Show("请正确输入复合版相关信息"); return; }
            }


            bool f = true;
            //NA Sigma
            if (myCheck.readFlag == false) { return; } else { fRecipe1.BackColor = System.Drawing.Color.Green; }
            if (myCheck.compareNaSigma())
            {
                foreach (DataRow row in myCheck.dt11.Rows)
                {

                    f = f && (row["illflag"].ToString() == "True") && (row["maskflag"].ToString() == "True");
                    if (f == false)
                    {
                        break;
                    }
                }
                if (f) { fCHK1.BackColor = System.Drawing.Color.Green; }
            }
            else
            {
                MessageBox.Show("未查询到基准BiasTable数据，退出"); return;
            }
            //坐标
            if (flagRevisedFlow == false)
            {
                try
                {
                    if (myCheck.compareCoordinates())
                    {
                        f = true;
                        foreach (DataRow row in myCheck.dt22.Rows)
                        {


                            f = f && (row["flag"].ToString() == "True");
                            if (f == false)
                            {
                                break;
                            }
                        }
                        if (f) { fCHK2.BackColor = System.Drawing.Color.Green; }
                    }
                    else
                    {
                        MessageBox.Show("请确认基准坐标文件是否存在");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ErrorCode: " + ex.Message + "\n\n基准坐标文件是否生成？？\n\n是否是改版产品？？");
                }

            }
            //blue Alignment
            if (myCheck.blueAlign) { fBlue.BackColor = System.Drawing.Color.Green; }
            //预对位
            if (flag)
            {
                if (myCheck.preAlign == "METHOD_1") { fPreAlign.BackColor = System.Drawing.Color.Green; }
            }
            else
            {
                if (myCheck.preAlign == "STANDARD") { fPreAlign.BackColor = System.Drawing.Color.Green; }
            }

            //mask size,laser REMA
            if (flagRevisedFlow == false)
            {
                myCheck.compareSize();
                if (myCheck.flagSize) { fSize.BackColor = System.Drawing.Color.Green; }
                if (myCheck.flagRema) { fREMA.BackColor = System.Drawing.Color.Green; }
            }
            //复合版
            myCheck.compareLocation();
            if (myCheck.flagLocation) { fLocation.BackColor = System.Drawing.Color.Green; }

            myCheck.compareMLM();
            if (myCheck.flagLocation1) { fLocation1.BackColor = System.Drawing.Color.Green; }



            //最终结果是否准确
            f = true;
            f = f && (fCHK1.BackColor == System.Drawing.Color.Green);
            if (flagRevisedFlow == false)
            {
                f = f && (fCHK2.BackColor == System.Drawing.Color.Green);
                f = f && (fSize.BackColor == System.Drawing.Color.Green);
                f = f && (fREMA.BackColor == System.Drawing.Color.Green);

            }

            f = f && (fBlue.BackColor == System.Drawing.Color.Green);
            f = f && (fPreAlign.BackColor == System.Drawing.Color.Green);
            f = f && (fLocation.BackColor == System.Drawing.Color.Green);
            f = f && (fLocation1.BackColor == System.Drawing.Color.Green);

            if (f)
            {
                myCheck.saveDt11(); myCheck.saveDt22();
                MessageBox.Show("程序检查已完成，未发现错误\n\n数据已保存");
            }
            else
            { MessageBox.Show("程序检查已完成，发现错误\n\n数据未保存"); }





        }




        private void saveCheck_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = myCheck.dt11;
                MessageBox.Show("请确认表格数据是否正确？");
                string pin = Interaction.InputBox("请输入 123 确认保存", "定义工艺", "", -1, -1);
                if (pin == "123")
                {
                    myCheck.saveDt11(); MessageBox.Show("Saving File......");
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ErrorCode: " + ex.Message + "\n\n先运行检查程序；");
            }
        }

        private void saveCheck2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = myCheck.dt22;
                MessageBox.Show("请确认表格数据是否正确？");
                string pin = Interaction.InputBox("请输入 123 确认保存", "定义工艺", "", -1, -1);
                if (pin == "123")
                {
                    myCheck.saveDt22(); MessageBox.Show("Saving File......");
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ErrorCode: " + ex.Message + "\n\n先运行检查程序；");
            }
        }
        #endregion


        #region print



        /*
        private void 显示BiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            DataTable tblTmp;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_BIASTABLE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据,请确认产品名");
                                return;
                            }
                            dataGridView1.DataSource = tblTmp;
                        }
                    }
                }
            }
        
    }

        private void 显示坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
      
            DataTable tblTmp;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_COORDINATE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请确认产品名");
                                return;

                            }

                            dataGridView1.DataSource = tblTmp;


                        }
                    }
                }
            }

        
    }
        */

        private void button6_Click(object sender, EventArgs e)

        {
            testPrint.PrintDGV.Print_DataGridView(dataGridView1, true);
        }

        /*
        private void 显示WaferMapToolStripMenuItem_Click(object sender, EventArgs e)
        {  

            string part = textBox1.Text.Trim().ToUpper();
            DataTable tblTmp;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_MAP WHERE PART LIKE '" + part + "%'";
                    // and NikonFlag is false";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件");
                                return;

                            }
                            dataGridView1.DataSource = tblTmp;
                        }
                    }
                }
            }

            for (int i = 0; i < tblTmp.Rows.Count; ++i)
            {
                wfr = new MapPlot();
                wfr.part = part;
                wfr.k = (float)Convert.ToDouble(tblTmp.Rows[i][0]);
                wfr.sx = (float)Convert.ToDouble(tblTmp.Rows[i][1]);
                wfr.sy = (float)Convert.ToDouble(tblTmp.Rows[i][2]);
                wfr.dx = (float)Convert.ToDouble(tblTmp.Rows[i][3]);
                wfr.dy = (float)Convert.ToDouble(tblTmp.Rows[i][4]);
                wfr.ox = (float)Convert.ToDouble(tblTmp.Rows[i][5]);
                wfr.oy = (float)Convert.ToDouble(tblTmp.Rows[i][6]);
                wfr.areaRatio = (double)tblTmp.Rows[i][7];
                wfr.fullCover = (bool)tblTmp.Rows[i][8];
                wfr.parttype = (string)tblTmp.Rows[i][9];
                bool nikonFlag = (bool)tblTmp.Rows[i][10];
                if (nikonFlag)
                {
                    wfr.plotAsmlMap("Print_Nikon");
                   
                    pictureBox1.Image = null;
                    FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Nikon.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                    pictureBox1.BringToFront(); pictureBox1.Visible = true;
                    dataGridView1.SendToBack(); dataGridView1.Visible = false;
                   
                }
                else
                {
                    wfr.plotAsmlMap("Print_Asml");
                    
                    pictureBox1.Image = null;
                    FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Asml.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                    pictureBox1.BringToFront(); pictureBox1.Visible = true;
                    dataGridView1.SendToBack(); dataGridView1.Visible = false;
                    
                }
            }








            

            }
        */
        private void PrintPicture(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {




            //图片抗锯齿            
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Stream fs = new FileStream(savePath + ".emf", FileMode.Open, FileAccess.Read);
            //System.Drawing.Image image = System.Drawing.Image.FromStream(fs);

            Bitmap bmp = new Bitmap(new System.Drawing.Imaging.Metafile(printPicPath));//打印emf矢量图，似乎负坐标有问题，转BMP再打印

            System.Drawing.Image image = bmp;

            int x = e.MarginBounds.X;//100
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;



            if ((width * 1.0 / e.MarginBounds.Width) > (height * 1.0 / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = Convert.ToInt32(image.Height * e.MarginBounds.Width * 1.0 / image.Width);
            }
            else
            {
                height = e.MarginBounds.Height;
                width = Convert.ToInt32(image.Width * e.MarginBounds.Height * 1.0 / image.Height);
            }




            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);


            e.Graphics.DrawImage(image, destRect, (e.PageBounds.Width - width) / 2, (e.PageBounds.Height - height) / 2, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel); //其中e.PageBounds属性表示页面全部区域的矩形区域
                                                                                                                                                                                       //e.Graphics.DrawImage(image, destRect, 50, 50, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);



        }
        private void PirntScreenPicture()
        {
            PrintDocument emfPicture = new PrintDocument();

            emfPicture.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPicture);
            emfPicture.DefaultPageSettings.Landscape = true;

            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowSomePages = true;
            printDialog.ShowHelp = true;
            printDialog.Document = emfPicture;


            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                emfPicture.Print();
            }
        }


        private void button8_Click(object sender, EventArgs e)



        {

            try
            {
                printPicPath = "C:\\temp\\" + wfr.part + "_Asml.emf";
                if (File.Exists(printPicPath))
                {
                    PirntScreenPicture();
                }
                else
                {
                    MessageBox.Show("请确认图片文件 " + printPicPath + " 是否存在？");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("ErrorCode:  " + exception.Message + "\n\n请先读取图片");
            }

        }



        private void button9_Click(object sender, EventArgs e)



        {
            try
            {
                if (wfr.parttype == "largeField")
                {
                    printPicPath = "C:\\temp\\" + wfr.part + "_Nikon.emf";
                    if (File.Exists(printPicPath))
                    {
                        PirntScreenPicture();
                    }
                    else
                    {
                        MessageBox.Show("请确认图片文件 " + printPicPath + " 是否存在？");
                    }
                }
                else
                { MessageBox.Show("非大视场产品，无 Nikon Map，Exit"); return; }
            }
            catch (Exception exception)
            {
                MessageBox.Show("ErrorCode:  " + exception.Message + "\n\n请先读取图片");
            }
        }
        #endregion
        #region help
        private void 统一日期格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Reserved For Date Format Unification\r\n\r\nChange Code To Run\r\n\r\nNow,Exit");
            DataTable dtShow = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string sql = " SELECT distinct  riqi FROM TBL_biastable";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }
            dataGridView1.DataSource = dtShow;


            foreach (DataRow row in dtShow.Rows)
            {

                string str1 = row["riqi"].ToString().Trim();
                string key = row["riqi"].ToString();
                string[] str2 = str1.Split(new char[] { ' ' });

                if (str2[0].Contains("/"))
                {
                    string[] str3 = str2[0].Split(new char[] { '/' });
                    str1 = str3[0] + "-";
                    if (str3[1].Length == 1)
                    { str1 += "0" + str3[1] + "-"; }
                    else
                    { str1 += str3[1] + "-"; }
                    if (str3[2].Length == 1)
                    { str1 += "0" + str3[2]; }
                    else
                    { str1 += str3[2]; }

                }
                else if (str2[0].Contains("\\"))
                {
                    string[] str3 = str2[0].Split(new char[] { '\\' });
                    str1 = str3[0] + "-";
                    if (str3[1].Length == 1)
                    { str1 += "0" + str3[1] + "-"; }
                    else
                    { str1 += str3[1] + "-"; }
                    if (str3[2].Length == 1)
                    { str1 += "0" + str3[2]; }
                    else
                    { str1 += str3[2]; }
                }
                else if (str2[0].Contains("-"))
                {
                    string[] str3 = str2[0].Split(new char[] { '-' });
                    str1 = str3[0] + "-";
                    if (str3[1].Length == 1)
                    { str1 += "0" + str3[1] + "-"; }
                    else
                    { str1 += str3[1] + "-"; }
                    if (str3[2].Length == 1)
                    { str1 += "0" + str3[2]; }
                    else
                    { str1 += str3[2]; }
                }
                else
                { }

                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string sql = "UPDATE TBL_biastable set riqi='" + str1 + "' WHERE riqi='" + key + "'";
                    // sql = "DELETE FROM tbl_move";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }


            }

        }
        #endregion

        #region query
        private void buttonTbl1_Click(object sender, EventArgs e)
        {

            DataTable tblTmp;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_BIASTABLE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据,请确认产品名");
                                return;

                            }

                            dataGridView1.DataSource = tblTmp;


                        }
                    }
                }
            }

        }
        private void buttonTbl2_Click(object sender, EventArgs e)
        {

            DataTable tblTmp;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_COORDINATE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请确认产品名");
                                return;

                            }

                            dataGridView1.DataSource = tblTmp;


                        }
                    }
                }
            }

        }

        private void buttonChk1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_CHECK1 WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            DataTable tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请确认产品名");
                                return;

                            }

                            dataGridView1.DataSource = tblTmp;


                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!(row.Cells["illflag"].Value is null))
                {
                    try
                    {
                        if (!((bool)row.Cells["illflag"].Value && (bool)row.Cells["maskflag"].Value))
                        {
                            row.DefaultCellStyle.BackColor = Color.DeepPink;
                        }
                    }
                    catch
                    {
                        //DataTable 保存时无值，从数据库读出时为""
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void buttonChk2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_CHECK2 WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            DataTable tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请确认产品名");
                                return;

                            }

                            dataGridView1.DataSource = tblTmp;


                        }
                    }
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!(row.Cells["flag"].Value is null))
                {
                    try
                    {
                        if (!(bool)row.Cells["flag"].Value)
                        {
                            row.DefaultCellStyle.BackColor = Color.DeepPink;
                        }
                    }
                    catch
                    {
                        //DataTable 保存时无值，从数据库读出时为""
                        row.DefaultCellStyle.BackColor = Color.Yellow;

                    }
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)

        {
            try
            {
                if (myCheck.dt1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = myCheck.dt11;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(row.Cells["illflag"].Value is null))
                        {
                            try
                            {
                                if (row.Cells["illflag"].Value.ToString() == "" || row.Cells["maskflag"].Value.ToString() == "")
                                {
                                    row.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                                else if (!(row.Cells["illflag"].Value.ToString() == "True" && row.Cells["maskflag"].Value.ToString() == "True"))
                                {
                                    row.DefaultCellStyle.BackColor = Color.DeepPink;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error Code: " + ex.Message + "\n\n请通知调试程序");

                            }
                        }
                    }
                }
                else
                { MessageBox.Show("检查失败，请确认曝光文本文件存在；"); }
            }
            catch
            {
                MessageBox.Show("先运行检查程序；");
            }
        }

        private void button5_Click(object sender, EventArgs e)

        {
            try
            {
                if (myCheck.dt1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = myCheck.dt22;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(row.Cells["flag"].Value is null))
                        {
                            try
                            {
                                if (row.Cells["flag"].Value.ToString() == "")
                                {
                                    row.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                                else if (row.Cells["flag"].Value.ToString() != "True")
                                {
                                    row.DefaultCellStyle.BackColor = Color.DeepPink;
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("Error Code: " + ex.Message + "\n\n请通知调试程序");

                            }
                        }
                    }
                }
                else
                { MessageBox.Show("检查失败，请确认曝光文本文件存在；"); }
            }
            catch
            {
                MessageBox.Show("先运行检查程序；");
            }
        }

        private void button_Click(object sender, EventArgs e)

        {

            if (data is null || data.dt is null)
            { MessageBox.Show("先运行程序，生成表格；"); }
            else
            { dataGridView1.DataSource = data.dt; }

        }
        private void button7_Click(object sender, EventArgs e)

        {
            if (data is null || data.dt2 is null)
            { MessageBox.Show("先运行程序，生成表格；"); }
            else
            { dataGridView1.DataSource = data.dt2; }


        }

        private void buttonMap_Click(object sender, EventArgs e)

        {

            string part = textBox1.Text.Trim().ToUpper();
            DataTable tblTmp;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_MAP WHERE PART LIKE '" + part + "%'";
                // and NikonFlag is false";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblTmp = ds.Tables[0];
                            if (tblTmp.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件");
                                return;

                            }
                            dataGridView1.DataSource = tblTmp;
                        }
                    }
                }
            }

            for (int i = 0; i < tblTmp.Rows.Count; ++i)
            {
                wfr = new MapPlot();
                wfr.part = part;
                wfr.k = (float)Convert.ToDouble(tblTmp.Rows[i][0]);
                wfr.sx = (float)Convert.ToDouble(tblTmp.Rows[i][1]);
                wfr.sy = (float)Convert.ToDouble(tblTmp.Rows[i][2]);
                wfr.dx = (float)Convert.ToDouble(tblTmp.Rows[i][3]);
                wfr.dy = (float)Convert.ToDouble(tblTmp.Rows[i][4]);
                wfr.ox = (float)Convert.ToDouble(tblTmp.Rows[i][5]);
                wfr.oy = (float)Convert.ToDouble(tblTmp.Rows[i][6]);
                wfr.areaRatio = (double)tblTmp.Rows[i][7];
                wfr.fullCover = (bool)tblTmp.Rows[i][8];
                wfr.parttype = (string)tblTmp.Rows[i][9];
                bool nikonFlag = (bool)tblTmp.Rows[i][10];
                if (nikonFlag)
                {
                    wfr.plotAsmlMap("Print_Nikon");

                    pictureBox1.Image = null;
                    FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Nikon.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                    pictureBox1.BringToFront(); pictureBox1.Visible = true;
                    dataGridView1.SendToBack(); dataGridView1.Visible = false;

                }
                else
                {
                    wfr.plotAsmlMap("Print_Asml");

                    pictureBox1.Image = null;
                    FileStream fs = new FileStream("C:\\temp\\" + wfr.part + "_Asml.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                    pictureBox1.BringToFront(); pictureBox1.Visible = true;
                    dataGridView1.SendToBack(); dataGridView1.Visible = false;

                }
            }










        }







        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            int cNo = dataGridView1.CurrentCell.ColumnIndex;
            int rNo = dataGridView1.CurrentRow.Index;

            if (data.dt.Columns[13].ColumnName == "illumination")
            {
                MessageBox.Show(cNo.ToString() + "," + rNo.ToString());
            }



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int cNo = dataGridView1.CurrentCell.ColumnIndex;
            int rNo = dataGridView1.CurrentRow.Index;

            if (data.dt.Columns[11].ColumnName == "illumination")
            {


                MessageBox.Show(dataGridView1.Rows[rNo].Cells[cNo].Value.ToString());
            }

        }
        #region ping/viwe date/distribution etc
        private void button3_Click_1(object sender, EventArgs e)

        {

            string filepath = "";

            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
            file.InitialDirectory = @"P:\_DailyCheck\NikonRecipe\";


            file.Filter = "CSV文档(*.csv)|*.csv";//|文档(*.xls)|**.xls";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                DataTable tblTmp = Share.OpenCsvWithComma(filepath);
                dataGridView1.DataSource = tblTmp;
            }


            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

     
        private void button3_Click_2(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Tool"); dt.Columns.Add("Status"); dt.Columns.Add("Content");
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            Dictionary<string, string> tool = new Dictionary<string, string>();
            tool.Add("ALSIB1", "10.4.152.230");
            tool.Add("ALSIB2", "10.4.152.231");
            tool.Add("ALSIB3", "10.4.152.229");
            tool.Add("ALSIB4", "10.4.152.228");
            tool.Add("ALSIB5", "10.4.152.227");
            tool.Add("ALSIB6", "10.4.152.226");
            tool.Add("ALSIB7", "10.4.152.225");
            tool.Add("ALSIB8", "10.4.152.224");
            tool.Add("ALSIB9", "10.4.152.223");
            tool.Add("ALSIBA", "10.4.152.222");
            tool.Add("ALSIBB", "10.4.152.221");
            tool.Add("ALSIBC", "10.4.152.220");
            tool.Add("ALSIBD", "10.4.152.219");
            tool.Add("ALSIBE", "10.4.152.218");
            tool.Add("ALSIBF", "10.4.152.217");
            tool.Add("ALSIBG", "10.4.152.216");
            tool.Add("ALSIBH", "10.4.152.210");
            tool.Add("ALSIBI", "10.4.152.211");

            tool.Add("BLSIBK", "10.4.131.30");
            tool.Add("BLSIBL", "10.4.131.31");
            tool.Add("BLSIE1", "10.4.131.75");
            tool.Add("BLSIE2", "10.4.131.78");
            tool.Add("SerNik", "10.4.72.145");
            tool.Add("ALSD82", "10.4.152.29");
            tool.Add("ALSD83", "10.4.151.64");
            tool.Add("ALSD85", "10.4.152.37");
            tool.Add("ALSD86", "10.4.152.31");
            tool.Add("ALSD87", "10.4.152.46");
            tool.Add("ALSD89", "10.4.152.39");
            tool.Add("ALSD8A", "10.4.152.42");
            tool.Add("ALSD8B", "10.4.152.47");
            tool.Add("ALSD8C", "10.4.152.48");
            tool.Add("BLSD7D", "10.4.131.32");
            tool.Add("BLSD08", "10.4.131.63");
            tool.Add("SerAsm", "10.4.72.253");
            tool.Add("ALSIBJ", "10.4.152.212");
            pingTool1(ref tool, ref dt);


        }

        private void pingTool(ref Dictionary<string, string> tool, ref DataTable dt)
        
        {
                  

            string rValue;
            int n = 0;

            foreach (KeyValuePair<string, string> kvp in tool)
            {
                rValue = PingService.Test(kvp.Value);
                DataRow row = dt.NewRow();
                row[0] = kvp.Key;
                row[2] = rValue.Split(new string[] { "数据包:" }, StringSplitOptions.RemoveEmptyEntries)[1];
                if (rValue.Contains("数据包: 已发送 = 3，已接收 = 3，丢失 = 0 (0% 丢失)"))
                {
                    row[1] = "True";
                    row[2] = rValue.Split(new string[] { "数据包:" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }

                else
                {
                    row[1] = "False";
                }
                dt.Rows.Add(row);
                if (dataGridView1.Rows[n].Cells[1].Value.ToString() == "False" && dataGridView1.Rows[n].Cells[1].Value.ToString() != "ALSIBJ")
                { dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Yellow; }
                dataGridView1.Refresh();


                n++;

            }
      
        }




        private void pingTool1(ref Dictionary<string, string> tool, ref DataTable dt)

        {


    
            int n = 0;

            foreach (KeyValuePair<string, string> kvp in tool)
            {

                string[] result = getResponse(kvp);

                DataRow row = dt.NewRow();
                row[0] = result[0];
                row[1] = result[1];
                row[2] = result[2];
               
                dt.Rows.Add(row);
                if (dataGridView1.Rows[n].Cells[1].Value.ToString() == "False" && dataGridView1.Rows[n].Cells[1].Value.ToString() != "ALSIBJ")
                { dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Yellow; }
                dataGridView1.Refresh();


                n++;

            }

        }

        private string[] getResponse(KeyValuePair<string,string> kvp)
        {
            string[] result = new string[3];
            string rValue = string.Empty;
            string status = string.Empty;
            result[0] = kvp.Key;
            rValue = PingService.Test(kvp.Value);
           
            if (rValue.Contains("数据包: 已发送 = 3，已接收 = 3，丢失 = 0 (0% 丢失)"))
            {
                result[1] = "True";
                result[2] = rValue.Split(new string[] { "数据包:" }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            else
            {
                result[1] = "False";
                result[2] = rValue;

            }

            return result;
        }


 
     






        private void button10_Click(object sender, EventArgs e)

        {
            DataTable dtShow = new DataTable();
            string password = string.Empty;
            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };
            AsmlFtpWeb asmlFtpWeb;
            string path850 = @"P:\Transfer\850";
            string path700 = @"P:\Transfer\700";
            string path850bak = @"P:\Transfer\bak\850";
            string path700bak = @"P:\Transfer\bak\700";
            string path850_local = @"D:\shareD\Recipe\PROD850";
            string path700_local = @"D:\shareD\Recipe\PROD700";

            //get password

            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    password = sr.ReadLine();
                    break;
                }
            }
            try
            {
                //delete all files
                LibF.DosCommand("del /F /Q " + path850 + "\\*");
                LibF.DosCommand("del /F /Q " + path700 + "\\*");
                //输入需要下载的文件
                string str = Interaction.InputBox("请输入需要下载的程序，用空格分割", "定义产品", "", -1, -1);
                str = str.Trim().ToUpper();
                string[] strArr = str.Split(new char[] { ' ' });
                List<string> downList = new List<string>();
                foreach (string part in strArr)
                {
                    if (part.Length > 0) { downList.Add(part); }
                }
                //下载
                asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/usr/asm/data.1725/user_data/jobs/PROD850", "sys.1725", password);

                foreach (string x in downList)
                {
                    asmlFtpWeb.Download(path850, x);
                }

                asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/usr/asm/data.1725/user_data/jobs/PROD700", "sys.1725", password);

                foreach (string x in downList)
                {
                    asmlFtpWeb.Download(path700, x);
                }
                asmlFtpWeb = null;

                //列出文件大小，判断文件是否下载成功
                dtShow = new DataTable();
                dtShow.Columns.Add("PartName"); dtShow.Columns.Add("PROD850"); dtShow.Columns.Add("PROD700");

                foreach (string part in downList)
                {
                    DataRow newRow = dtShow.NewRow();
                    newRow[0] = part;
                    FileInfo fileInfo = new FileInfo(path850 + "\\" + part);
                    if (fileInfo.Length == 0)
                    {
                        newRow[1] = "下载失败！";
                        MessageBox.Show("PROD850/" + part + "：下载失败！！");
                        fileInfo.Delete();
                    }
                    else
                    {
                        newRow[1] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                        fileInfo.CopyTo(path850_local + "\\" + fileInfo.Name, true);
                        fileInfo.CopyTo(path850bak + "\\" + fileInfo.Name, true);

                    }


                    fileInfo = new FileInfo(path700 + "\\" + part);
                    if (fileInfo.Length == 0)
                    {
                        newRow[2] = "下载失败！";
                        MessageBox.Show("PROD700/" + part + "：下载失败！！");

                        fileInfo.Delete();
                    }
                    else
                    {
                        newRow[2] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                        fileInfo.CopyTo(path700_local + "\\" + fileInfo.Name, true);
                        fileInfo.CopyTo(path700bak + "\\" + fileInfo.Name, true);
                    }
                    dtShow.Rows.Add(newRow);

                }
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.DataSource = dtShow;
            }
            catch
            {
                MessageBox.Show("请确认是否有 \"\\\\10.4.50.16\\photo$\\ppcs\\Transfer\"目录的读写权限");
            }

            //将文件备份到 /scratch 目录


            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD850", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[1].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path850 + "\\" + row[0].ToString());

                }
            }
            asmlFtpWeb = null;

            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD700", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[2].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path700 + "\\" + row[0].ToString());
                }
            }
            asmlFtpWeb = null;




        }
        private void button14_Click(object sender, EventArgs e) //download 700
        
        {
            DataTable dtShow = new DataTable();
            string password = string.Empty;
            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };
            AsmlFtpWeb asmlFtpWeb;
            string path850 = @"P:\Transfer\850";
            string path700 = @"P:\Transfer\700";
            string path850bak = @"P:\Transfer\bak\850";
            string path700bak = @"P:\Transfer\bak\700";
            string path850_local = @"D:\shareD\Recipe\PROD850";
            string path700_local = @"D:\shareD\Recipe\PROD700";

            //get password

            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    password = sr.ReadLine();
                    break;
                }
            }
            try
            {
                //delete all files
                LibF.DosCommand("del /F /Q " + path850 + "\\*");
                LibF.DosCommand("del /F /Q " + path700 + "\\*");
                //输入需要下载的文件
                string str = Interaction.InputBox("请输入需要下载的程序，用空格分割", "定义产品", "", -1, -1);
                str = str.Trim().ToUpper();
                string[] strArr = str.Split(new char[] { ' ' });
                List<string> downList = new List<string>();
                foreach (string part in strArr)
                {
                    if (part.Length > 0) { downList.Add(part); }
                }
                //下载
          

                asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/usr/asm/data.1725/user_data/jobs/PROD700", "sys.1725", password);

                foreach (string x in downList)
                {
                    asmlFtpWeb.Download(path700, x);
                }
                asmlFtpWeb = null;

                //列出文件大小，判断文件是否下载成功
                dtShow = new DataTable();
                dtShow.Columns.Add("PartName"); dtShow.Columns.Add("PROD850"); dtShow.Columns.Add("PROD700");

                foreach (string part in downList)
                {
                    DataRow newRow = dtShow.NewRow();
                    newRow[0] = part;
                    try
                    {

                        FileInfo fileInfo = new FileInfo(path850 + "\\" + part);
                        if (fileInfo.Length == 0)
                        {
                            newRow[1] = "下载失败！";
                            MessageBox.Show("PROD850/" + part + "：下载失败！！");
                            fileInfo.Delete();
                        }
                        else
                        {
                            newRow[1] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                            fileInfo.CopyTo(path850_local + "\\" + fileInfo.Name, true);
                            fileInfo.CopyTo(path850bak + "\\" + fileInfo.Name, true);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    try
                    {

                        FileInfo fileInfo = new FileInfo(path700 + "\\" + part);
                        if (fileInfo.Length == 0)
                        {
                            newRow[2] = "下载失败！";
                            MessageBox.Show("PROD700/" + part + "：下载失败！！");

                            fileInfo.Delete();
                        }
                        else
                        {
                            newRow[2] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                            fileInfo.CopyTo(path700_local + "\\" + fileInfo.Name, true);
                            fileInfo.CopyTo(path700bak + "\\" + fileInfo.Name, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dtShow.Rows.Add(newRow);

                }
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               // MessageBox.Show("请确认是否有 \"\\\\10.4.50.16\\photo$\\ppcs\\Transfer\"目录的读写权限");
            }

            //将文件备份到 /scratch 目录


            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD850", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[1].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path850 + "\\" + row[0].ToString());

                }
            }
            asmlFtpWeb = null;

            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD700", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[2].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path700 + "\\" + row[0].ToString());
                }
            }
            asmlFtpWeb = null;




        }

        private void button15_Click(object sender, EventArgs e)
  

        {
            DataTable dtShow = new DataTable();
            string password = string.Empty;
            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };
            AsmlFtpWeb asmlFtpWeb;
            string path850 = @"P:\Transfer\850";
            string path700 = @"P:\Transfer\700";
            string path850bak = @"P:\Transfer\bak\850";
            string path700bak = @"P:\Transfer\bak\700";
            string path850_local = @"D:\shareD\Recipe\PROD850";
            string path700_local = @"D:\shareD\Recipe\PROD700";

            //get password

            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    password = sr.ReadLine();
                    break;
                }
            }
            try
            {
                //delete all files
                LibF.DosCommand("del /F /Q " + path850 + "\\*");
                LibF.DosCommand("del /F /Q " + path700 + "\\*");
                //输入需要下载的文件
                string str = Interaction.InputBox("请输入需要下载的程序，用空格分割", "定义产品", "", -1, -1);
                str = str.Trim().ToUpper();
                string[] strArr = str.Split(new char[] { ' ' });
                List<string> downList = new List<string>();
                foreach (string part in strArr)
                {
                    if (part.Length > 0) { downList.Add(part); }
                }
                //下载
                asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/usr/asm/data.1725/user_data/jobs/PROD850", "sys.1725", password);

                foreach (string x in downList)
                {
                    asmlFtpWeb.Download(path850, x);
                }

         
                asmlFtpWeb = null;

                //列出文件大小，判断文件是否下载成功
                dtShow = new DataTable();
                dtShow.Columns.Add("PartName"); dtShow.Columns.Add("PROD850"); dtShow.Columns.Add("PROD700");

                foreach (string part in downList)
                {
                    DataRow newRow = dtShow.NewRow();
                    newRow[0] = part;
                    try
                    {

                        FileInfo fileInfo = new FileInfo(path850 + "\\" + part);
                        if (fileInfo.Length == 0)
                        {
                            newRow[1] = "下载失败！";
                            MessageBox.Show("PROD850/" + part + "：下载失败！！");
                            fileInfo.Delete();
                        }
                        else
                        {
                            newRow[1] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                            fileInfo.CopyTo(path850_local + "\\" + fileInfo.Name, true);
                            fileInfo.CopyTo(path850bak + "\\" + fileInfo.Name, true);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    try
                    {

                        FileInfo fileInfo = new FileInfo(path700 + "\\" + part);
                        if (fileInfo.Length == 0)
                        {
                            newRow[2] = "下载失败！";
                            MessageBox.Show("PROD700/" + part + "：下载失败！！");

                            fileInfo.Delete();
                        }
                        else
                        {
                            newRow[2] = "下载成功！ 文件大小：" + fileInfo.Length.ToString();
                            fileInfo.CopyTo(path700_local + "\\" + fileInfo.Name, true);
                            fileInfo.CopyTo(path700bak + "\\" + fileInfo.Name, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dtShow.Rows.Add(newRow);

                }
                this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.DataSource = dtShow;
            }
            catch
            {
                MessageBox.Show("请确认是否有 \"\\\\10.4.50.16\\photo$\\ppcs\\Transfer\"目录的读写权限");
            }

            //将文件备份到 /scratch 目录


            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD850", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[1].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path850 + "\\" + row[0].ToString());

                }
            }
            asmlFtpWeb = null;

            asmlFtpWeb = new AsmlFtpWeb("10.4.72.253", "/scratch/PROD700", "sys.1725", password);
            foreach (DataRow row in dtShow.Rows)
            {
                if (row[2].ToString().Contains("成功"))
                {
                    asmlFtpWeb.Upload(path700 + "\\" + row[0].ToString());
                }
            }
            asmlFtpWeb = null;




        }
        private void button11_Click(object sender, EventArgs e)

        {
            DataTable dtShow = new DataTable();
            string pin = Interaction.InputBox("请输入Pin Number", "", "", -1, -1);
            pin = pin.Trim();
            if (pin != "123456")
            { MessageBox.Show("Pin Number不对，退出"); return; }

            string password = string.Empty;
            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };
            AsmlFtpWeb asmlFtpWeb;
            bool flag;
            string path850 = @"P:\Transfer\850";
            string path700 = @"P:\Transfer\700";
            string[] tool850 = { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C" };
            string[] tool700 = { "BLSD7D", "BLSD08" };
            List<string> list700 = new List<string>();
            List<string> list850 = new List<string>();
            dtShow = new DataTable();
            dtShow.Columns.Add("产品");
            dtShow.Columns.Add("设备");
            dtShow.Columns.Add("状态");
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DataSource = dtShow;


            //get password

            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                { password = sr.ReadLine(); }
            }

            //get list

            list850 = LibF.ExportFileList(path850, list850);
            list700 = LibF.ExportFileList(path700, list700);

            //upload




            if (list850.ToArray().Length > 0)
            {

                try
                {

                    foreach (string tool in tool850)
                    {
                        asmlFtpWeb = new AsmlFtpWeb(ipUser[tool][1], "/usr/asm/data." + ipUser[tool][0].Substring(4, 4) + "/user_data/jobs/PROD", ipUser[tool][0], password);



                        foreach (string file in list850)
                        {
                            flag = asmlFtpWeb.Upload(file);
                            DataRow newRow = dtShow.NewRow();
                            newRow[0] = file;
                            newRow[1] = tool;
                            newRow[2] = flag.ToString();
                            dtShow.Rows.Add(newRow);

                        }
                        asmlFtpWeb = null;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (list700.ToArray().Length > 0)
            {
                try
                {
                    foreach (string tool in tool700)
                    {
                        asmlFtpWeb = new AsmlFtpWeb(ipUser[tool][1], "/usr/asm/" + ipUser[tool][0] + "/user_data/jobs/PROD/", ipUser[tool][0], "litho");
                        foreach (string file in list700)
                        {
                            flag = asmlFtpWeb.Upload(file);
                            DataRow newRow = dtShow.NewRow();
                            newRow[0] = file;
                            newRow[1] = tool;
                            newRow[2] = flag.ToString();
                            dtShow.Rows.Add(newRow);
                        }
                        asmlFtpWeb = null;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }






        }

        private void button12_Click(object sender, EventArgs e)

        {
            #region initialize
            string days = Interaction.InputBox("查询前请先刷新数据\n\n" +
                "请输入整数天数\n\n" +
                "    0 -->当天更改的程序清单\n\n" +
                "    1 -->昨天天更改的程序清单\n\n" +
                "    n -->其余依次类推", "定义天数", "", -1, -1);
            days = days.Trim();
            foreach (char x in days)
            {
                if (!char.IsDigit(x))
                { MessageBox.Show("未输入整数,退出!"); return; }
            }

            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };

            string[] allTools = new string[] { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD7D", "BLSD08", "SERVER" };
            string[] dirs;
            string tblName;
            string riqi = DateTime.Now.AddDays(-Convert.ToInt32(days)).ToString("yyyy-MM-dd");
            connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=P:\\_SQLite\\AsmlRecipeCheck.mdb";

            DataTable dt1, dtShow, dt2;
            string sql;

            dt1 = new DataTable();
            dt1.Columns.Add("PART");
            for (int i = 0; i < 11; i++)
            { dt1.Columns.Add(allTools[i]); }
            dt1.Columns.Add("SVR700");
            dt1.Columns.Add("SVR850");
            #endregion

            foreach (string tool in allTools)
            {
                if (tool == "SERVER")
                { dirs = new string[] { "/user_data/jobs/PROD850", "/user_data/jobs/PROD700" }; }
                else
                { dirs = new string[] { "/user_data/jobs/PROD" }; }

                foreach (string dir in dirs)
                {
                    if (dir.Contains("850"))
                    { tblName = "SVR850"; }
                    else if (dir.Contains("700"))
                    { tblName = "SVR700"; }
                    else
                    { tblName = tool; }

                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        sql = "SELECT DISTINCT PART FROM " + tblName + " WHERE Riqi>'" + riqi + "'";
                        sql = " SELECT PART,RIQI FROM " + tblName + " WHERE PART IN (" + sql + ")";
                        sql = " SELECT PART,COUNT(PART) AS QTY FROM (" + sql + ") GROUP BY PART";
                        sql = " SELECT DISTINCT PART FROM (" + sql + ") a WHERE a.QTY>1";

                        //列出变更的Part
                        conn.Open();
                        using (OleDbCommand command = new OleDbCommand(sql, conn))
                        {
                            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    da.Fill(ds);
                                    dtShow = ds.Tables[0];

                                }
                            }
                        }
                        //汇总数据，列出最新日期
                        foreach (DataRow row in dtShow.Rows)
                        {
                            sql = "SELECT PART,RIQI FROM " + tblName + " WHERE PART='" + row[0] + "' ORDER BY RIQI DESC";
                            using (OleDbCommand command = new OleDbCommand(sql, conn))
                            {
                                using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                                {
                                    using (DataSet ds = new DataSet())
                                    {
                                        da.Fill(ds);
                                        dt2 = ds.Tables[0];
                                    }
                                }
                            }
                            bool flag = false;
                            //存在part
                            foreach (DataRow item in dt1.Rows)
                            {

                                if (item[0].ToString() == dt2.Rows[0][0].ToString())
                                {
                                    if (tool == "SERVER")
                                    {
                                        item[tblName] = dt2.Rows[0][1];
                                    }
                                    else
                                    {
                                        item[tool] = dt2.Rows[0][1];
                                    }
                                    flag = true;
                                    break;
                                }
                            }
                            //不存在Part
                            if (flag == false)
                            {
                                DataRow newRow = dt1.NewRow();
                                newRow[0] = dt2.Rows[0][0];
                                if (tool == "SERVER")
                                {
                                    newRow[tblName] = dt2.Rows[0][1];
                                }
                                else
                                {
                                    newRow[tool] = dt2.Rows[0][1];
                                }
                                dt1.Rows.Add(newRow);


                            }
                        }

                    }







                }

            }


            dt1.DefaultView.Sort = "PART ASC";
            dataGridView1.DataSource = dt1;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            DataTable tmpDt;
            string str = "\\\\10.4.50.16\\photo$\\ppcs\\recipe\\曝光菜单备份表1.xls";

            File.Copy(str, "C:\\temp\\123.xls", true);
            string excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + "c:\\temp\\123.xls" + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    string sql = string.Format("SELECT * FROM  [{0}$]", "曝光菜单备份表");

                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        tmpDt = ds.Tables[0];
                    }

                }
                dataGridView1.DataSource = tmpDt;
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }




























                /*

            dataGridView1.DataSource = dt;
            foreach (KeyValuePair<string, string> kvp in myDic)
            {
                DataRow row = dt.NewRow();
                row[0] = kvp.Key;
                if (kvp.Value.Contains("(0% 丢失)"))
                {
                    row[1] = "True";
                }
                else
                {
                    row[1] = "False";
                }
                row[2] = kvp.Value;
                dt.Rows.Add(row);
                dataGridView1.DataSource = dt;

                */
            }
            
        
    

    



