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

using System.Drawing.Printing;

using System.Collections;

using System.Diagnostics;
using Microsoft.VisualBasic;







namespace WindowsFormsExposureRecipe
{
    public partial class MainForm : Form
    {
        System.Data.DataTable tblTmp, tblFlow, tblBt;
        string tech, layer, track, sql, connStr;
        string dbPath = @"p:\_SQLite\Flow.DB";
        public double aperture, outer, inner;
        string parttype;
        bool fullCover = false;//全覆盖，包括WEE
        string part;
        string savePath;
        string fullTech;
        string printPicPath;
        bool narrowScribelaneFlag;//判读对位方法；和读取1% Nikon对位方法



        public MainForm()
        {
            InitializeComponent();
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;//单击单元格编辑
            connStr = "data source=" + dbPath;

            savePath = @"C:\temp\";
            printPicPath = string.Empty;
            try
            {
              //  File.Copy(@"\\10.4.72.74\asml\_SQLite\Flow.DB", @"\\10.4.50.16\PHOTO$\PPCS\RECIPE\Flow.DB", true);
              //  MessageBox.Show("数据已备份到\\\\10.4.50.16\\PHOTO$\\PPCS\\RECIPE\\Flow.DB");
            }
            catch
            {
              //  MessageBox.Show("数据备份失败，请确认\\\\10.4.50.16\\PHOTO$是否可连接;\r\n\r\n或是否有权限读写 \\RECIPE 目录;");

            }
        }

        #region //主界面
        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)  //选择流程和bias table的默认路劲，列出sheet名
        {
            part = textBox1.Text.Trim().ToUpper();
            if (part.Length < 8)
            {
                MessageBox.Show("产品名少于8位，不符合正常产品名命名规则；\n\n" +
                    "其它目的，请输入至少8位； \n\n" +
                    "退出!!");
                return;
            }
            savePath += part;
            string flowPath = "P:\\_flow\\" + textBox1.Text.Trim() + ".xls";
            string btPath = "P:\\Recipe\\Biastable\\" + textBox1.Text.Trim() + ".xls";
            if (System.IO.File.Exists(flowPath) == false || System.IO.File.Exists(btPath) == false)
            { MessageBox.Show("Bias Table或流程文件不存在；\r\n\r\n请确认产品名或文件已保存；"); return; }
            ListExcelSheetName(flowPath, listBox1);
            ListExcelSheetName(btPath, listBox2);

        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e) // 选择sheet名，读取流程
        {

            string filepath = "P:\\_flow\\" + textBox1.Text.Trim() + ".xls";
            string strTmp = filepath.Trim().Substring(filepath.Length - 3, 3);
            string excelStr = string.Empty; //此判断无用，以上以指定xls后缀，后续弹出框选文件时有用
            if (strTmp == "xls")
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意

            }

            else if (strTmp == "lsx")
            {

                excelStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filepath + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';";

            }


            tblTmp = null;
            //https://www.cnblogs.com/ammy714926/p/4905026.html
            string sheetName;


            if (listBox1.SelectedItems.Count > 0)
            {
                sheetName = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("请选择查阅的Excel表"); return;
            }


            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    sql = string.Format("SELECT * FROM  [{0}]", sheetName);
                 

                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        tblTmp = ds.Tables[0];
                    }

                }

                dataGridView1.DataSource = tblTmp;
               
                MessageBox.Show("数据绑定Excel成功；\r\n\r\n开始抽取数据");
                FlowToDataTable();

            }
            catch (Exception)
            {
                MessageBox.Show("数据绑定Excel失败;\r\n\r\n数据抽取失败"); return;
            }


        }
        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e) //选择sheet名，读取biastable
        {

            string filepath = "P:\\Recipe\\Biastable\\" + textBox1.Text.Trim() + ".xls";
            string strTmp = filepath.Trim().Substring(filepath.Length - 3, 3);
            string excelStr = string.Empty;
            if (strTmp == "xls")
            { excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; } // Office 07及以上版本 不能出现多余的空格 而且分号注意

            else if (strTmp == "lsx")
            { excelStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filepath + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';"; }

            tblTmp = null;
            //https://www.cnblogs.com/ammy714926/p/4905026.html
            string sheetName;


            if (listBox2.SelectedItems.Count > 0) {sheetName = listBox2.SelectedItem.ToString();   }
            else { MessageBox.Show("请选择查阅的Excel表"); return; }


            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    sql = string.Format("SELECT * FROM  [{0}]", sheetName);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        tblTmp = ds.Tables[0];
                    }

                }

                dataGridView1.DataSource = tblTmp;
                MessageBox.Show("数据绑定Excel成功；\r\n\r\n开始抽取BiasTable数据");
                BaistableToDataTable();
            }
            catch (Exception)
            {
                MessageBox.Show("数据绑定Excel失败;\r\n\r\n数据抽取失败"); return;
            }

        }
        private void flowBiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            MergeFlowBiasTable();
        }
        private void alignToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlignTo();
        }
        private void coordinateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadCoordinateFile();//读入所有坐标
         
            MatchCoordinate();//各层读入坐标
      

            #region //大视场坐标转换
            part = textBox1.Text.Trim().ToUpper();

            if (part.Substring(part.Length - 2, 2) == "-L")
            {



                decimal sx, x1, x2, y1, y2;
                try
                { sx = Convert.ToDecimal(textBox2.Text); sx = sx / 2; }
                catch
                { MessageBox.Show("大视场产品请输入正确的Step X"); return; }
                for (int i = 0; i < tblBt.Rows.Count; i++)
                {




                    if (tblBt.Rows[i]["eqptype"].ToString() == "LDI")
                    {
                        x1 = Convert.ToDecimal(tblBt.Rows[i]["x1"]);
                        y1 = Convert.ToDecimal(tblBt.Rows[i]["y1"]);
                        x2 = Convert.ToDecimal(tblBt.Rows[i]["x2"]);
                        y2 = Convert.ToDecimal(tblBt.Rows[i]["y2"]);
                        tblBt.Rows[i]["x1"] = y2;
                        tblBt.Rows[i]["y1"] = -(x2 + sx);
                        tblBt.Rows[i]["x2"] = y1;
                        tblBt.Rows[i]["y2"] = -(x1 + sx);

                    }

                }


            }

            #endregion

            tblBt.DefaultView.Sort = "eqptype ASC";

            dataGridView1.DataSource = tblBt;

        }
        private void 三合一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            MergeFlowBiasTable();
            AlignTo();
            ReadCoordinateFile();//读入所有坐标
            MatchCoordinate();//各层读入坐标
            #region //大视场坐标转换
            //string part = textBox1.Text.Trim().ToUpper();

            //if (part.Substring(part.Length - 2, 2) == "-L")
            if (radioButton2.Checked)
            {



                decimal sx, x1, x2, y1, y2;
                try
                { sx = Convert.ToDecimal(textBox2.Text); sx = sx / 2; }
                catch
                { MessageBox.Show("大视场产品请输入正确的Step X"); return; }
                for (int i = 0; i < tblBt.Rows.Count; i++)
                {




                    if (tblBt.Rows[i]["eqptype"].ToString() == "LDI")
                    {
                        x1 = Convert.ToDecimal(tblBt.Rows[i]["x1"]);
                        y1 = Convert.ToDecimal(tblBt.Rows[i]["y1"]);
                        x2 = Convert.ToDecimal(tblBt.Rows[i]["x2"]);
                        y2 = Convert.ToDecimal(tblBt.Rows[i]["y2"]);
                        tblBt.Rows[i]["x1"] = y2;
                        tblBt.Rows[i]["y1"] = -(x2 + sx);
                        tblBt.Rows[i]["x2"] = y1;
                        tblBt.Rows[i]["y2"] = -(x1 + sx);

                    }

                }


            }

            #endregion

            tblBt.DefaultView.Sort = "eqptype ASC";

            dataGridView1.DataSource = tblBt;
        }
        private void 打印BiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tblFlow;
            testPrint.PrintDGV.Print_DataGridView(dataGridView1, true);

            // 保存到数据库
          
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_BIASTABLE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            tblFlow.Columns.Add("riqi");
            tblFlow.Columns.Add("part");
            for (int i = 0; i < tblFlow.Rows.Count; i++)
            { tblFlow.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); tblFlow.Rows[i]["part"] = textBox1.Text.Trim().ToUpper(); }
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFlow, "tbl_biastable");
            myTabInfo.ImportToSqliteBatch(tblFlow, dbPath);








        }
        private void 打印坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tblBt;
            testPrint.PrintDGV.Print_DataGridView(dataGridView1, true);


            // 保存到数据库
         
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_COORDINATE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            tblBt.Columns.Add("riqi");
            tblBt.Columns.Add("part");
            for (int i = 0; i < tblBt.Rows.Count; i++)
            { tblBt.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); tblBt.Rows[i]["part"] = textBox1.Text.Trim().ToUpper(); }
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblBt, "tbl_coordinate");
            myTabInfo.ImportToSqliteBatch(tblBt, dbPath);


        }
        private void button2_Click(object sender, EventArgs e)
        {
         
            MergeFlowBiasTable();
            AlignTo();


            

            if (MessageBox.Show("请确认是否需要读入坐标\r\n\r\n新品请选Yes，读入坐标\r\n\r\n改版请选No，不读取坐标", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ReadCoordinateFile();//读入所有坐标
                MatchCoordinate();//各层读入坐标
                #region //大视场坐标转换
                string part = textBox1.Text.Trim().ToUpper();

                // if (part.Substring(part.Length - 2, 2) == "-L")
                if (radioButton2.Checked)
                {



                    decimal sx, x1, x2, y1, y2;
                    try
                    { sx = Convert.ToDecimal(textBox2.Text); sx = sx / 2; }
                    catch
                    { MessageBox.Show("大视场产品请输入正确的Step X"); return; }
                    for (int i = 1; i < tblBt.Rows.Count; i++) //i=0是第一层，无坐标转换
                    {




                        if (tblBt.Rows[i]["eqptype"].ToString() == "LDI")
                        {
                            x1 = Convert.ToDecimal(tblBt.Rows[i]["x1"]);
                            y1 = Convert.ToDecimal(tblBt.Rows[i]["y1"]);
                            x2 = Convert.ToDecimal(tblBt.Rows[i]["x2"]);
                            y2 = Convert.ToDecimal(tblBt.Rows[i]["y2"]);
                            tblBt.Rows[i]["x1"] = y2;
                            tblBt.Rows[i]["y1"] = -(x2 + sx);
                            tblBt.Rows[i]["x2"] = y1;
                            tblBt.Rows[i]["y2"] = -(x1 + sx);

                        }

                    }


                }

                #endregion

               // tblBt.DefaultView.Sort = "eqptype ASC"; //取消，保证顺序和biasTable一致

                dataGridView1.DataSource = tblBt;



                // 保存到数据库
              
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = " DELETE  FROM TBL_COORDINATE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }

                tblBt.Columns.Add("riqi");
                tblBt.Columns.Add("part");
                for (int i = 0; i < tblBt.Rows.Count; i++)
                { tblBt.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); tblBt.Rows[i]["part"] = textBox1.Text.Trim().ToUpper(); }
                DataTableToSQLte myTabInfo1 = new DataTableToSQLte(tblBt, "tbl_coordinate");
                myTabInfo1.ImportToSqliteBatch(tblBt, dbPath);


            }






            // 保存到数据库
         
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_BIASTABLE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            tblFlow.Columns.Add("riqi");
            tblFlow.Columns.Add("part");
            for (int i = 0; i < tblFlow.Rows.Count; i++)
            { tblFlow.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); tblFlow.Rows[i]["part"] = textBox1.Text.Trim().ToUpper(); }
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFlow, "tbl_biastable");
            myTabInfo.ImportToSqliteBatch(tblFlow, dbPath);





          







        }
        private void 计算MapOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

            part = textBox1.Text.Trim().ToUpper();
            if (part.Length < 8)
            {
                MessageBox.Show("产品名少于8位，不符合正常产品名命名规则；\n\n" +
                    "其它目的，请输入至少8位； \n\n" +
                    "退出!!");
                return;
            }
            savePath += part;
            pictureBox1.Image = null;
            CallCalculateOffset();
            this.tabControl1.SelectedTab = this.tabPage1;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            part = textBox1.Text.Trim().ToUpper();
            if (part.Length < 8)
            {
                MessageBox.Show("产品名少于8位，不符合正常产品名命名规则；\n\n" +
                    "其它目的，请输入至少8位； \n\n" +
                    "退出!!");
                return;
            }
            bool nikonFlag = false;//用于大视场NikonFlag作图
            pictureBox1.Image = null;


            PlotAsml(nikonFlag);
            
        }

        private void 二合一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool nikonFlag = false; //用于大视场NikonFlag作图
            pictureBox1.Image = null;
            //  CallCalculateOffset(); 
            // PlotAsml(nikonFlag);
            if (radioButton2.Checked)
            {
                nikonFlag = true;
                PlotAsml(nikonFlag);
                this.tabControl1.SelectedTab = this.tabPage2;
            }
            //大视场Nikon作图 offset
            //NikonY= AsmlX
            //tmp= (AsmlY+AsmlStepY/4)
            // if   tmp>-AsmlStepY/4 and tmp<AsmlStepY/4-->NikonX=-tmp
            // else
            //      if tmp<0
            //             NiknonX=AsmlStepY/2+tmp
            //       else
            //             NikonX=AsmlStepY/2-tmp


        }

        private void 打印图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //https://www.cnblogs.com/EasyInvoice/p/8333923.html
            //https://www.cnblogs.com/EasyInvoice/p/8333923.html
            //https://www.debugease.com/csharp/1785804.html

            // PrintDirectClass p =new PrintDirectClass();
            //  p.imageFile =savePath+".emf";

            //   p.PrintPreview();


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
        private void aSML程序比对ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            //tblFlow 坐标
            //tblTmp illumination
            #region //get recipe data
            tblTmp = tblFlow = tblBt = null;
            part = textBox1.Text.Trim().ToUpper();
            string filePath = filePath = @"P:\RECIPE\RECIPE\" + part;
            string allStr = File.ReadAllText(filePath);

            StringBuilder lineTxt = new StringBuilder();
            string str;
            bool flagZb = false;
            bool flagType = false;
            bool flagUsage = false;
            bool flagLayer = false;
            bool flagMask = false;
            bool flagIll1 = false;
            bool flagIll2 = false;

            string[] strArr;



            foreach (char x in allStr)
            {
                if (x.ToString() != "\n") 
                {
                    lineTxt.Append(x.ToString());
                }
                else
                {
                    str = lineTxt.ToString().Trim();//一行读取结束
                    //预对位
                    if (str.Contains("Prealignment Method"))
                    {
                        if (radioButton2.Checked)
                        {
                            if (str.Contains("METHOD_1"))
                            {
                               
                            }
                            else
                            {
                                MessageBox.Show("大视场预对位方式不对:\r\n " + str);
                            }
                        }
                        else
                        {
                            if (str.Contains("STANDARD"))
                            { }
                            else
                            {
                                MessageBox.Show("普通视场预对位方式不对:\r\n " + str);
                            }
                        }
                    }


                    #region //坐标
                    if (str == "+=================+=====+=====+==========+==========+============+============+")
                    {
                        flagZb = true;
                        tblTmp = null; tblTmp = new DataTable();
                        tblTmp.Columns.Add("markid", Type.GetType("System.String"));
                        tblTmp.Columns.Add("layer", Type.GetType("System.String"));
                        tblTmp.Columns.Add("x", Type.GetType("System.Double"));
                        tblTmp.Columns.Add("y", Type.GetType("System.Double"));
                        tblTmp.Columns.Add("type", Type.GetType("System.String"));
                        tblTmp.Columns.Add("strategy", Type.GetType("System.String"));
                        tblTmp.Columns.Add("ppid", Type.GetType("System.String"));

                    }
                    if (str != "+=================+=====+=====+==========+==========+============+============+"
                        && str != "+-----------------+-----+-----+----------+----------+------------+------------+"
                        && flagZb == true)
                    {
                        strArr = str.Split(new char[] { '|' });
                        DataRow newRow = tblTmp.NewRow();
                        newRow["markid"] = strArr[1].Trim();
                        newRow["layer"] = strArr[1].Split(new char[] { '_' })[0].Substring(strArr[1].Split(new char[] { '_' })[0].Length - 2, 2).Trim();
                        ///stack mark 有问题；待解决
                        ///读标记时，应该列出实际读入的层次名，主要是针对WT/TT/CP及stack mark
                        newRow["x"] = strArr[4].Trim();
                        newRow["y"] = strArr[5].Trim();
                        tblTmp.Rows.Add(newRow);
                    }
                    if (str == "+-----------------+-----+-----+----------+----------+------------+------------+")
                    {
                        flagZb = false;
                        //dataGridView1.DataSource = tblTmp;
                    }
                    #endregion

                    #region //mark type
                    if (str == "+=================+===========+=================+=====+=======+======+======+")
                    {
                        flagType = true;
                    }
                    if (str != "+=================+===========+=================+=====+=======+======+======+"
                        && str != "+-----------------+-----------+-----------------+-----+-------+------+------+"
                        && flagType == true)
                    {
                        strArr = str.Split(new char[] { '|' });
                        // MessageBox.Show(str);


                        for (int i = 0; i < tblTmp.Rows.Count; i++)
                        {
                            if (tblTmp.Rows[i]["markid"].ToString().Trim() == strArr[1].Trim())
                            {
                                tblTmp.Rows[i]["type"] = strArr[3].Trim();
                            }
                        }

                    }
                    if (str == "+-----------------+-----------+-----------------+-----+-------+------+------+")
                    {
                        flagType = false;
                        //  dataGridView1.DataSource = tblTmp;
                    }
                    #endregion                 

                    #region //mark usage
                    if (str == "+==================+==================+==============+===============+")
                    {
                        flagUsage = true;
                    }
                    if (str != "+==================+==================+==============+===============+"
                        && str != "+------------------+------------------+--------------+---------------+"
                        && flagUsage == true)
                    {
                        strArr = str.Split(new char[] { '|' });

                        for (int i = 0; i < tblTmp.Rows.Count; i++)
                        {
                            if (tblTmp.Rows[i]["markid"].ToString().Trim() == strArr[2].Trim())
                            {
                                tblTmp.Rows[i]["strategy"] = strArr[1].Trim();
                            }
                        }

                    }
                    if (str == "+------------------+------------------+--------------+---------------+")
                    {
                        flagUsage = false;
                        //去除重复标记
                        //  for(int i=0;i<tblTmp.Rows.Count;i++)
                        //  { tblTmp.Rows[i]["markid"]=(tblTmp.Rows[i]["markid"].ToString()).Split(new char[] { '_' })[0];}
                        //   DataView dv = new DataView(tblTmp);
                        //   tblBt = dv.ToTable("Dist", true, "markid", "layer", "x", "y", "type", "strategy", "ppid");
                        //    dataGridView1.DataSource = tblTmp;

                    }
                    #endregion

                    #region // layer strategy
                    if (str == "+=================+==================+=============+")
                    {
                        flagLayer = true;
                        tblBt = tblTmp.Clone();

                    }
                    if (str != "+=================+==================+=============+"
                        && str != "+-----------------+------------------+-------------+"
                        && flagLayer == true)
                    {
                        strArr = str.Split(new char[] { '|' });

                        for (int i = 0; i < tblTmp.Rows.Count; i++)
                        {
                            if (tblTmp.Rows[i]["strategy"].ToString().Trim() == strArr[2].Trim())
                            {
                                DataRow newRow = tblBt.NewRow();
                                newRow["ppid"] = strArr[1].Trim();
                                newRow["markid"] = tblTmp.Rows[i]["markid"].ToString().Trim().Split(new char[] { '_' })[0];
                                newRow["layer"] = tblTmp.Rows[i]["layer"].ToString().Trim();
                                newRow["x"] = tblTmp.Rows[i]["x"].ToString().Trim();
                                newRow["y"] = tblTmp.Rows[i]["y"].ToString().Trim();
                                newRow["type"] = tblTmp.Rows[i]["type"].ToString().Trim();
                                newRow["strategy"] = tblTmp.Rows[i]["strategy"].ToString().Trim();
                                tblBt.Rows.Add(newRow);


                            }
                        }

                    }
                    if (str == "+-----------------+------------------+-------------+")
                    {

                        flagLayer = false;


                    }
                    #endregion

                    #region //mask
                    if (str == "+=================+=================+==========================+==============+")
                    {
                        flagMask = true; tblTmp = new DataTable();
                        tblTmp.Columns.Add("layer");
                        tblTmp.Columns.Add("image");
                        tblTmp.Columns.Add("mask");
                    }

                    if (str != "+=================+=================+==========================+==============+"
                        && str != "+-----------------+-----------------+--------------------------+--------------+"
                        && !str.Contains("SPM")
                        && flagMask == true)
                    {
                        strArr = str.Split(new char[] { '|' });


                        DataRow newRow = tblTmp.NewRow();
                        newRow["layer"] = strArr[1].Trim();
                        newRow["image"] = strArr[2].Trim();
                        newRow["mask"] = strArr[3].Trim();

                        tblTmp.Rows.Add(newRow);




                    }
                    if (str == "+-----------------+-----------------+--------------------------+--------------+")
                    {

                        flagMask = false;
                        //dataGridView1.DataSource = tblTmp;

                    }




                    #endregion

                    #region // annualar conventional
                    if (str == "+=================+=================+==============+=================+")
                    {
                        flagIll1 = true;
                        tblTmp.Columns.Add("mode");


                    }

                    if (str != "+=================+=================+==============+=================+"
                        && str != "+-----------------+-----------------+--------------+-----------------+"
                        && !str.Contains("SPM")
                        && flagIll1 == true)
                    {
                        strArr = str.Split(new char[] { '|' });


                        for (int i = 0; i < tblTmp.Rows.Count; i++)
                        {
                            if (tblTmp.Rows[i]["layer"].ToString().Trim() == strArr[1].Trim())
                            {
                                tblTmp.Rows[i]["mode"] = strArr[3];

                            }
                        }




                    }
                    if (str == "+-----------------+-----------------+--------------+-----------------+")
                    {

                        flagIll1 = false;
                        // dataGridView1.DataSource = tblTmp;

                    }

                    #endregion


                    #region //ill setting


                    if (str == "+=================+=================+=========+========+========+")
                    {
                        flagIll2 = true;
                        tblTmp.Columns.Add("aperture");
                        tblTmp.Columns.Add("outer");
                        tblTmp.Columns.Add("inner");

                    }

                    if (str != "+=================+=================+=========+========+========+"
                        && str != "+-----------------+-----------------+---------+--------+--------+"
                        && !str.Contains("SPM")
                        && flagIll2 == true)
                    {
                        strArr = str.Split(new char[] { '|' });
                        for (int i = 0; i < tblTmp.Rows.Count; i++)
                        {


                            if (tblTmp.Rows[i]["layer"].ToString().Trim() == strArr[1].Trim() && tblTmp.Rows[i]["image"].ToString().Trim() == strArr[2].Trim())
                            {
                                tblTmp.Rows[i]["aperture"] = strArr[3].Trim();
                                tblTmp.Rows[i]["outer"] = strArr[4].Trim();
                                tblTmp.Rows[i]["inner"] = strArr[5].Trim();
                            }
                        }




                    }
                    if (str == "+-----------------+-----------------+---------+--------+--------+")
                    {

                        flagIll2 = false;
                       // dataGridView1.DataSource = tblTmp;

                    }




                    #endregion



                    lineTxt.Remove(0, lineTxt.Length);//重置，重新读取
                }


            }

            //大视场孔层次的Image Name必须为FULL-K，FULL-KB，如果不存在报错
            bool imageCount = false;

            if (radioButton2.Checked)
            {
                foreach (string hole in new string[] { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "W8", "WT" })
                {
                    DataRow[] drs = tblTmp.Select("layer='" + hole + "'");
                    if (drs.Length == 1)
                    {
                        MessageBox.Show("请确认大视场产品Wx是否同时定义了 FULL-K和FULL-KB Image");
                        imageCount = true;
                        break;
                    }
                    else if (drs.Length == 2)
                    {
                        if (
                      drs[0]["image"].ToString().ToUpper() == "FULL-K"
                             &&
                       drs[1]["image"].ToString().ToUpper() == "FULL-KB"
                            )
                        {

                        }
                        else
                        {
                            MessageBox.Show("==请确认大视场产品Wx是否同时定义了 FULL-K和FULL-KB Image==");
                            imageCount = true;
                            break;
                        }

                    }
                }




            }

           


            DataView dv = new DataView(tblBt);
            tblFlow = dv.ToTable("Dist", true, "markid", "layer", "x", "y", "type", "strategy", "ppid");

            #endregion

            //dataGridView1.DataSource = tblFlow;
            //MessageBox.Show("break");


            #region //check NA/Sigma
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT ppid,illumination,MASK FROM TBL_BIASTABLE WHERE EQPTYPE='LDI' AND PART='" + part + "'";
               
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblBt = ds.Tables[0];
                            if (tblBt.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到BIAS TABLE数据，退出");
                                return;

                            }
                            else
                            {
                                tblTmp.Columns.Add("mask1");
                                tblTmp.Columns.Add("aperture1");
                                tblTmp.Columns.Add("outer1");
                                tblTmp.Columns.Add("inner1");
                                tblTmp.Columns.Add("illflag");
                                tblTmp.Columns.Add("maskflag");
                                tblTmp.Columns.Add("part");
                                tblTmp.Columns.Add("riqi");
                                for (int i=0;i<tblTmp.Rows.Count;i++)
                                {
                                    for (int j=0;j<tblBt.Rows.Count;j++)
                                    {
                                        if (tblTmp.Rows[i]["layer"].ToString().Trim()==(tblBt.Rows[j]["ppid"]).ToString().Trim())
                                        {
                                            sql = tblBt.Rows[j]["illumination"].ToString();
                                            strArr = sql.Split(new char[] { ',' });
                                            tblTmp.Rows[i]["aperture1"] = strArr[1];
                                            tblTmp.Rows[i]["outer1"] = strArr[2];
                                            tblTmp.Rows[i]["inner1"] = strArr[3];
                                            tblTmp.Rows[i]["mask1"] = tblBt.Rows[j]["Mask"];

                                           tblTmp.Rows[i]["illflag"] = System.Math.Abs((Convert.ToDouble(tblTmp.Rows[i]["aperture1"].ToString()) - Convert.ToDouble(tblTmp.Rows[i]["aperture"].ToString()))) < 0.00001
                                                && System.Math.Abs((Convert.ToDouble(tblTmp.Rows[i]["outer1"].ToString()) - Convert.ToDouble(tblTmp.Rows[i]["outer"].ToString()))) < 0.00001
                                                && System.Math.Abs((Convert.ToDouble(tblTmp.Rows[i]["inner1"].ToString()) - Convert.ToDouble(tblTmp.Rows[i]["inner"].ToString()))) < 0.00001
                                                && tblTmp.Rows[i]["mode"].ToString().ToUpper().Substring(1, 3) == strArr[0].Trim().ToUpper();
                                            tblTmp.Rows[i]["maskFlag"] = (tblTmp.Rows[i]["mask"].ToString().Trim() == tblTmp.Rows[i]["mask1"].ToString().Trim());
                                            tblTmp.Rows[i]["part"] = part;
                                            tblTmp.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                        }
                                    }
                                }
                                
                            }
                            
                          

                        }
                    }
                }
            }
            #endregion


            if (imageCount)
            {
                DataRow newRow = tblTmp.NewRow();
                newRow["layer"] = "请确认大视场产品Wx是否同时定义了 FULL-K和FULL-K Image";
                newRow["image"] = "错误!!";
                newRow["mask"] = "错误!!";
                newRow[13] = tblTmp.Rows[1][13].ToString();
                tblTmp.Rows.Add(newRow);

            }



            dataGridView1.DataSource = tblTmp;


            MessageBox.Show("请确认Illumination设置检查结果并保存检查数据");







            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_CHECK1 WHERE PART='" + part + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

     



            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblTmp, "tbl_check1");
            myTabInfo.ImportToSqliteBatch(tblTmp, dbPath);



                                                                              



            #region // check coordinate
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT ppid,alignto layer,x1/1000.0 x,y1/1000.0 y,typex type  FROM TBL_COORDINATE WHERE  EQPTYPE='LDI' AND PART='" + part + "'";
                sql += "UNION SELECT ppid,alignto layer,x2/1000.0 x,y2/1000.0 y,typey type  FROM TBL_COORDINATE WHERE  EQPTYPE='LDI' AND PART='" + part + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblBt = ds.Tables[0];
                            if (tblBt.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询坐标数据,退出");
                                return;

                            }
                            else
                            {
                                dataGridView1.DataSource = tblFlow;

                            
                             //MessageBox.Show("READ JOBFILE DONE01");
                             // dataGridView1.DataSource = tblBt;
                             //MessageBox.Show("READ JOBFILE DONE02");


                                tblFlow.Columns.Add("x1");
                                tblFlow.Columns.Add("y1");
                                tblFlow.Columns.Add("type1");
                                tblFlow.Columns.Add("flag");
                                tblFlow.Columns.Add("part");
                                for (int i = 0; i < tblFlow.Rows.Count; i++)
                                {
                                    for (int j = 0; j < tblBt.Rows.Count; j++)
                                    {
                                        if (
                                            ///stack mark只取前缀前两位对比
                                            tblFlow.Rows[i]["layer"].ToString().Trim().Substring(0,2) == tblBt.Rows[j]["layer"].ToString().Trim().Substring(0, 2) &&
                                            tblFlow.Rows[i]["ppid"].ToString().Trim() == tblBt.Rows[j]["ppid"].ToString().Trim()
                                            &&(    
                                                   (tblFlow.Rows[i]["type"].ToString().Trim().Contains("X") && tblBt.Rows[j]["type"].ToString().Trim().Contains("X"))
                                                    || (tblFlow.Rows[i]["type"].ToString().Trim().Contains("Y") && tblBt.Rows[j]["type"].ToString().Trim().Contains("Y"))
                                               )
                                            && (
                                                      (tblFlow.Rows[i]["type"].ToString().Trim().Contains("AH53") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AH53"))
                                                   || (tblFlow.Rows[i]["type"].ToString().Trim().Contains("AH53") && tblBt.Rows[j]["type"].ToString().Trim().Contains("SPM53"))
                                                   || (tblFlow.Rows[i]["type"].ToString().Trim().Contains("AH325374") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AH325374"))
                                                   || (tblFlow.Rows[i]["type"].ToString().Trim().Contains("AH325374") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AA157") && tblFlow.Rows[i]["strategy"].ToString().Trim().Contains("AA157"))
                                                )
                                            )
                                        {

                                            tblFlow.Rows[i]["x1"] = tblBt.Rows[j]["x"].ToString().Trim();
                                            tblFlow.Rows[i]["y1"] = tblBt.Rows[j]["y"].ToString().Trim();
                                            tblFlow.Rows[i]["type1"] = tblBt.Rows[j]["type"].ToString().Trim();
                                            tblFlow.Rows[i]["part"] = part;
                                            try
                                            {
                                                tblFlow.Rows[i]["flag"] = System.Math.Abs((Convert.ToDouble(tblFlow.Rows[i]["x"].ToString()) - Convert.ToDouble(tblFlow.Rows[i]["x1"].ToString()))) < 0.00001
                                                    && System.Math.Abs((Convert.ToDouble(tblFlow.Rows[i]["y"].ToString()) - Convert.ToDouble(tblFlow.Rows[i]["y1"].ToString()))) < 0.000001;
                                            }
                                            catch
                                            {
                                                //无数据
                                            }

                                        }
                                    }
                                }
                           
                            }

                            dataGridView1.DataSource = tblFlow;

                        }
                    }
                }
            }


            #endregion
            dataGridView1.DataSource = tblFlow;
            MessageBox.Show("请确认坐标检查结果并保存检查数据");

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_CHECK2 WHERE PART='" + part + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }



            myTabInfo = new DataTableToSQLte(tblFlow, "tbl_check2");
            myTabInfo.ImportToSqliteBatch(tblFlow, dbPath);


           

        }
        private void biasTable相关查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_BIASTABLE WHERE PART='"+textBox1.Text.Trim().ToUpper()+"'";
      
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
        private void 坐标相关查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_COORDINATE WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

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

        private void biasTable比对结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_CHECK1 WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

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
        private void 坐标比对结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            dataGridView1.DataSource = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_CHECK2 WHERE PART='" + textBox1.Text.Trim().ToUpper() + "'";

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
        private void nikon程序变更检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            string filepath = "";

            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
            file.InitialDirectory = @"P:\_DailyCheck\NikonRecipe\";


            file.Filter = "CSV文档(*.csv)|*.csv";//|文档(*.xls)|**.xls";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                tblTmp = OpenCSV(filepath);
                dataGridView1.DataSource = tblTmp;
            }
           

            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void 打印屏幕表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testPrint.PrintDGV.Print_DataGridView(dataGridView1, true);
        }
        private void 打印注意事项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string part = textBox1.Text.Trim().ToUpper();
            string tech;
            int k = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT DISTINCT CODE FROM TBL_BIASTABLE WHERE PART='" + part+ "'";

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
                                MessageBox.Show("未查询到Bias Table工艺代码");
                                return;

                            }

                            tech = tblTmp.Rows[0][0].ToString();


                        }
                    }
                }
            }
                    
           tblTmp = null;tblTmp = new DataTable();
           tblTmp.Columns.Add("序号");tblTmp.Columns.Add("注意事项");
           for (int i=1;i<20;i++)
            {
                DataRow newRow = tblTmp.NewRow();
                newRow["序号"] = i.ToString();
                tblTmp.Rows.Add(newRow);
            }

            tblTmp.Rows[k][1] = "NIKON新品四合一的版在菜单编辑时把Metal层的focus disable range设置为6000"; k += 1;
            tblTmp.Rows[k][1] = "implant layer、Contact、Via-*及PAD和FU打标处不曝光（特殊产品除外）"; k += 1;
            tblTmp.Rows[k][1] = "NIKON上TO/GT/PC，SHIFT FOCUS改成YES"; k += 1;
            tblTmp.Rows[k][1] = "遇到新品有PI层次的菜单，请将focus offset 0.2 直接加进菜单，并通知朱龙飞设置R2R"; k += 1;
            tblTmp.Rows[k][1] = "ASML C18工艺SI，_1%工艺TO,所有GT/W1不需要更改如下项目"; k += 1;
            tblTmp.Rows[k][1] = "(续）Wafer alignment:Y / Reduce Reticle Alignments:Y / Maximum Drift:10 / Maximum Interval(wafers):25"; k += 1;
            tblTmp.Rows[k][1] = "(续）其他层都需要更改"; k += 1;
            tblTmp.Rows[k][1] = "W1所有工艺不用XPA mark；除C18产品外，对位参照OVL测量层次"; k += 1;
            tblTmp.Rows[k][1] = "新品W1之后NP/PP layer 按照对位GT LSA 算法;NIKON作业，TO之前的层次EGA对位方式都用LSA"; k += 1;
            tblTmp.Rows[k][1] = "PAD 菜单对位rule，EGA shot由4过4，改为6过4"; k += 1;
            tblTmp.Rows[k][1] = "新品 MAP边缘只要ASML服务器模拟出来的shot全部曝光(应该是有DN后的Pattern层次？？）"; k += 1;
            tblTmp.Rows[k][1] = "ZZ曝光菜单要求：使用后一层的mask，曝光菜单layer使用ZZ，采用extended方式，dose为-280，只选择1个edge最小shot曝光"; k += 1;
            tblTmp.Rows[k][1] = "（续）          R2R中同步维护dose 280(例：AD5441AB)"; k += 1;
            tblTmp.Rows[k][1] = "60um新品TO之后GT 之前的非关键层次， Search 优先FIA 专用mark，选取Dark FIA 42，"; k += 1;
            tblTmp.Rows[k][1] = "(续）若无选取普通Dark 标记FIA 42，若只有一组，算法改到FIA 42，EGA 选取 13根条Dark FIA double 42"; k += 1;






            if (tech.Substring(0, 1) == "T")
            { 
                tblTmp.Rows[k][1] = "DOMS的PAD层search 、EGA都用FIA,算法41";k += 1;
                tblTmp.Rows[k][1] = "DMOS新品菜单新建时，SN / SP层次默认EGA对位方式设置为FIA"; k += 1;
                tblTmp.Rows[k][1] = "所有DMOS产品新建，Nikon前段对位固定使用TR层次标记"; k += 1;
                tblTmp.Rows[k][1] = "DOSM产品除split gate的产品ASML上坐标用SPM53/ NAH53外其他用SPM53(60)那组坐标";k += 1;
            }
            if (tech.Substring(0,3)=="M52"||tech.Substring(1,1)=="1")
            { 
                tblTmp.Rows[k][1] = "M52 / .18以下工艺的非关键层(除RE层外）NIKON参数设置: EGA为FIA, Shot的数为8个，EGA Reject：Yes，EGA Limitation：0.65,";k += 1;
                tblTmp.Rows[k][1] = "(续）                                                EGA Requisite shot：6，Result Allowance：1.5um "; k += 1;
            }
            if (part.Substring(0,2)=="XU"|| part.Substring(0, 2) == "XV" || part.Substring(0, 2) == "UF")
            {
                tblTmp.Rows[k][1] = "XU/XV/UF开头的产品除了注入、孔层打标处不曝光以外其他边缘全部曝光"; k += 1;
               
            }
            if ( part.Substring(0, 2) == "UF")
            {
                tblTmp.Rows[k][1] =" UF开头的产品PAD层打标处曝光, PAD层所有shot必须都要曝光，无论里面有没有有效管芯。"; k += 1;

            }
            if (tech.Substring(0, 3) == "B52")
            {
                tblTmp.Rows[k][1] = "B52有PX layer的,需要将TO 对位改到对PX"; k += 1;

            }
            if (part.Substring(part.Length-1,1)=="M"|| part.Substring(part.Length - 1, 1) == "N")
            {
                tblTmp.Rows[k][1] = "M/N 结尾的split gate产品,定义一个Image全部shot曝光"; k += 1;
            }
            if (radioButton2.Checked)
            {
                tblTmp.Rows[k][1] = "新品大视场产品Prealignment  Method：METHOD_1"; k += 1;
                tblTmp.Rows[k][1] = "大视场打标处在孔层、PAD(CP、CF、PI、PN、CK、FU)层补加image，其它层次全曝"; k += 1;

            }
            if (part.Substring(0,2)=="D6")
            {
                tblTmp.Rows[k][1] = "D6* 产品所有层次都会曝光，保证边缘不完整shot都曝光上（打标处除外）"; k += 1;
            }



            dataGridView1.DataSource = tblTmp;

            testPrint.PrintDGV.Print_DataGridView(dataGridView1, true);



        }
        private void bugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tblTmp = null; tblTmp = new DataTable();
            tblTmp.Columns.Add("序号"); tblTmp.Columns.Add("问题描述");
            for (int i = 1; i < 30; i++)
            {
                DataRow newRow = tblTmp.NewRow();
                newRow["序号"] = i.ToString();
                tblTmp.Rows.Add(newRow);
            }

            int k = 0;
            tblTmp.Rows[k][1] = "大视场产品，ASML/NIKON map拟合的管芯数目不完全匹配；";k += 1;
            tblTmp.Rows[k][1] = "0,6点位置，shot顶点未落在圆内（wfr内），但单边与圆相割情况，未考虑，待增加判断；3，9点无此问题  "; k += 1;
            tblTmp.Rows[k][1] = "大视场Nikon TXT MAP有问题-->数组全部预置值可暂时解决；其它问题待发现"; k += 1;
            tblTmp.Rows[k][1] = "待补充。。。。。。。。。。。。。。。。。。。。。。。"; k += 1;
            tblTmp.Rows[k][1] = "O55734AJ_D,GT之后的Nikon层次对。。。。。。。。。。。。。。。。。。。。。。。"; k += 1;
            tblTmp.Rows[k][1] = "DMOS工艺代码，除T之外，其它？？"; k += 1;
           
          
            tblTmp.Rows[k][1] = "DMOS CP 加入 FU，FI，PN"; k += 1;
            tblTmp.Rows[k][1] = "ASML 坐标有效位数只有四位，SY5719AK"; k += 1;





            dataGridView1.DataSource = tblTmp;
        }
        private void 简单使用说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tblTmp = null; tblTmp = new DataTable();
            tblTmp.Columns.Add("序号"); tblTmp.Columns.Add("步骤说明");
            for (int i = 1; i < 200; i++)
            {
                DataRow newRow = tblTmp.NewRow();
                newRow["序号"] = i.ToString();
                tblTmp.Rows.Add(newRow);
            }

            int k = 0;
            tblTmp.Rows[k][1] = "下载保存坐标，流程，biastable三个文件"; k += 1;
            tblTmp.Rows[k][1] = "红色字体文本框处，输入完整Part名，不区分大小写；"; k += 1;
            tblTmp.Rows[k][1] = "红色背景Panel处选择产品类型；"; k += 1;
            tblTmp.Rows[k][1] = "输入Image Size和Shot Size，单位um"; k += 1;
            tblTmp.Rows[k][1] = "鼠标双击Part名文本框-->以下两个列表框会显示流程和BiasTable文件中的工作表名；"; k += 1;
            tblTmp.Rows[k][1] = "双击左侧列表框中的流程sheet名，读取流程相关内容；"; k += 1;
            tblTmp.Rows[k][1] = "双击左侧列表框中的Bias Table sheet名，读取BiasTable相关内容；；"; k += 1;
            tblTmp.Rows[k][1] = "点击 ‘运行并保存’ 命令按钮，生成编程相关表格，并保存；"; k += 1;
            tblTmp.Rows[k][1] = "‘TOOL’->'基准BiasTable查询' / '基准坐标相关查询',检查数据是否准确"; k += 1;
            tblTmp.Rows[k][1] = "若数据无误，‘TOOL’->'打印屏幕表格'，打印数据供编程用；打印坐标时，非相关列不要选，保证坐标显示完全；"; k += 1;
            tblTmp.Rows[k][1] = "===================================================================================================== "; k += 1;

            tblTmp.Rows[k][1] = "‘MAP’->'计算Map Offset'，优化map offset；"; k += 1;
            tblTmp.Rows[k][1] = "‘MAP’->'画图'，作图，并保存相关作图数据；选择 '打印图片',打印屏幕显示图片 "; k += 1;
            tblTmp.Rows[k][1] = "‘MAP’->'大视场Nikon'，大视场产品Nikon Map作图，并保存相关作图数据； "; k += 1;
            tblTmp.Rows[k][1] = "‘TOOL’->'调用数据作图'，显示/打印，文件保存在C:/temp目录，包括TXT文本文件； "; k += 1;
            tblTmp.Rows[k][1] = "‘TOOL’->'打印注意事项' "; k += 1;
            tblTmp.Rows[k][1] = "。。。。。。编程并下载ASML程序文本文件，保存。。。。。。 "; k += 1;
            tblTmp.Rows[k][1] = "‘TOOL’->'ASML程序比对'，检查光刻版名，NA/Sigma/坐标是否准确，并保存检查 结果； "; k += 1;
            tblTmp.Rows[k][1] = "选择‘TOOL’子菜单，确认检查结果 "; k += 1;
            tblTmp.Rows[k][1] = "===================================================================================================== "; k += 1;
            tblTmp.Rows[k][1] = "#######以下说明NA/SIGMA数据更新####### "; k += 1;
            tblTmp.Rows[k][1] = "'ILLUMINATION'->'生成表格',在Tech/Layer/Track/Type/NA/Outer/Inner等栏目输入相应内容操作 "; k += 1;
            tblTmp.Rows[k][1] = "单元格输入内容后，务必按回车键！！单元格输入内容后，务必按回车键！！单元格输入内容后，务必按回车键！！ "; k += 1;
            tblTmp.Rows[k][1] = "‘查询’Tech/Layer/Track 栏输入字符查询，Tech栏至少输入三位工艺代码，或输入%%%，查询所有工艺"; k += 1;
            tblTmp.Rows[k][1] = "‘插入或更新’除key/key1/Comment栏必须按要求输入内容"; k += 1;
            tblTmp.Rows[k][1] = "‘从DataTable导入’，先利用‘EXCEL'菜单下的'列出表格名'/'读取文件'命令，读入设置，然后导入；"; k += 1;







            dataGridView1.DataSource = tblTmp;
        }

        private void 更改历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {

                tblTmp = null; tblTmp = new DataTable();
                tblTmp.Columns.Add("日期"); tblTmp.Columns.Add("更改说明");
                DataRow newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-14";
                newRow["更改说明"] = "PlotAsml函数：NormalField增加FullCover判断-->XU/XV/UF/D6/O1开头的Part，及Part第7位为M/N（split gate），曝光Wafer Map是全覆盖";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-14";
                newRow["更改说明"] = "MatchCoordinate函数：CP/PI/PN/CF/FU层次查找坐标时，额外增加TT,AT,T1,T2,T3,A7,A6,A5,A4,A3,A2,A1层次";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-14";
                newRow["更改说明"] = "打标处是否曝光，增加如下判断，打标区域的+/-13，改为+/-9（打标实际X尺寸为15.9）-->shot部分落在Y>94.5,X(-9,9)区间才不曝光-->是否曝光，基本受最小面积决定，除非将默认值从0.2改至更小，该判断才有效";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "";
                newRow["更改说明"] = "判断条件：fullCover == false && lly >= 94.5 && ((llx < -9 && (llx + sx) > -9 && (llx + sx) < 9) ||((llx + sx) > 9 && llx > -9 && llx < 9) ||(llx > -13 && (llx + sx < 13))))";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-14";
                newRow["更改说明"] = "FlowToDataTabl函数：流程读取 1）判断一行内是否同时有\"步骤/stage/recpname/ppid\"关键字 2）若同时存在，读取Column No，抽取流程";
                tblTmp.Rows.Add(newRow);
                
                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-15";
                newRow["更改说明"] = "对位定义：含ZA工艺，TO（含）之前，ZA（不含）之后层次，对位ZA；不含ZA工艺的TO，对位到ovlto层次，若不测OVL，对位到第一层";
                tblTmp.Rows.Add(newRow);
                
                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-17";
                newRow["更改说明"] = "读取坐标：坐标前缀排序，X坐标在前，Y坐标后，正常情况datarows两行，X取第0行，Y取第1行；部分是同名多组，改为X第0行，Y第n/2行";
                tblTmp.Rows.Add(newRow);
               
                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-19";
                newRow["更改说明"] = "读取坐标,生成坐标文件时，align to不再从bias table直接复制，而是取坐标文件的实际层次（主要是针对stackmak），坐标比较也相应更改";
                tblTmp.Rows.Add(newRow);
                
                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-19";
                newRow["更改说明"] = "SPLIT GATE:ASML对位TR，优先读取TR60前缀标记，不再区分普通DMOS和SPLIT GATE";
                tblTmp.Rows.Add(newRow);
                
                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-21";
                newRow["更改说明"] = "脚本初始化时，就备份数据文件到\\\\10.4.50.16\\PHOTO$\\PPCS\\RECIPE";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-21";
                newRow["更改说明"] = "BiasTable不规范，部分MaskLabe栏填入的是版名-->读取 \"-\" 后的两位字符";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-22";
                newRow["更改说明"] = "NIKON对位方式用类似LSA/FIA表示，/前表示Search,/后表示EGA；TT/ T% DMOS PAD/1% NarrowMark非RE/CT层用FIA Search，其余全部是LSA Search";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-22";
                newRow["更改说明"] = "NarrorMark,_1% 非CP/PI/RE/FU/FN层次用DARK 标记，其它层次待加入-->(后续加入gtFlag，w1Flag解决，现有toFlag，zaFlag）";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-23";
                newRow["更改说明"] = "Nikon对位方式定义，CAF工艺归为_1%";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-01-23";
                newRow["更改说明"] = " MergeFlowBiasTable函数：合并Flow和BiasTable时，若流程中的PPID未在BiasTable中找到对应的PPID，提示报错，退出";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-02-01";
                newRow["更改说明"] = "大视场产品ASML Map Offset，绝对值大于1/2 step时的换算时，误用Nikon Step Size";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-02-17";
                newRow["更改说明"] = "Tool菜单增加命令，检查光刻机是否联机";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-02-24";
                newRow["更改说明"] = "CE坐标文件异常：数组[1],[2]定义为标记前缀，条数；非空的数值[max]为Y,然后是X";
                tblTmp.Rows.Add(newRow);

                newRow = tblTmp.NewRow();
                newRow["日期"] = "2020-04-03";
                newRow["更改说明"] = "CE坐标文件异常：X,Y坐标的坐标前缀一样，加入判断是否最后一位分别是X,Y";
                tblTmp.Rows.Add(newRow);




                dataGridView1.DataSource = tblTmp;
            }

        }
        private void 查询1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "123456")
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = " SELECT DISTINCT RIQI,PART FROM TBL_BIASTABLE";
                    MessageBox.Show(sql);
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

                                }

                                dataGridView1.DataSource = tblTmp;
                             

                            }
                        }
                    }
                }
            }
        }

        private void 查询2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "123456")
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = " SELECT DISTINCT PART FROM TBL_CHECK2 WHERE PART IS NOT NULL";

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

                                }

                                dataGridView1.DataSource = tblTmp;


                            }
                        }
                    }
                }
            }
        }

        private void 查询Nikon是否联机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //https://blog.csdn.net/qq_21747999/article/details/79151910

            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "z:\\_SQLite\\checkTool.bat";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();

        }
        private void 统一日期格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reserved For Date Format Unification\r\n\r\nChange Code To Run\r\n\r\nNow,Exit");
            DataTable dtShow = new DataTable();
          
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT distinct  riqi FROM TBL_biastable";
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
                    sql = "UPDATE TBL_biastable set riqi='" + str1 + "' WHERE riqi='" + key + "'";
                    // sql = "DELETE FROM tbl_move";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }


            }
        }
        #endregion

        #region//Na/Sigma 表格维护

        private void 生成表格ToolStripMenuItem_Click(object sender, EventArgs e) //生成空白查询表格
        {
            dataGridView1.DataSource = null;
            tblTmp = null;
            tblTmp = new System.Data.DataTable();
            tblTmp.Columns.Add("Tech", Type.GetType("System.String"));
            tblTmp.Columns.Add("Layer", Type.GetType("System.String"));
            tblTmp.Columns.Add("Track", Type.GetType("System.String"));
            tblTmp.Columns.Add("Key", Type.GetType("System.String"));
            tblTmp.Columns.Add("Type", Type.GetType("System.String"));
            tblTmp.Columns.Add("NA", Type.GetType("System.Double"));
            tblTmp.Columns.Add("Outer", Type.GetType("System.Double"));
            tblTmp.Columns.Add("Inner", Type.GetType("System.Double"));
            tblTmp.Columns.Add("Comment", Type.GetType("System.String"));
            tblTmp.Columns.Add("Key1", Type.GetType("System.String"));


            DataRow newRow = tblTmp.NewRow();
            newRow["Tech"] = "12位工艺代码";
            newRow["Layer"] = " ";
            newRow["Track"] = "6位涂胶程序";
            newRow["Type"] = "只能输入Con或Ann";
            newRow["Comment"] = "输入备注,另NA/Outer/Inner三列输入小数，最多精确到小数点后三位";
            tblTmp.Rows.Add(newRow);
            dataGridView1.DataSource = tblTmp;





        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e) //查询
        {

            tech =Interaction.InputBox("请输入工艺代码（通配符查询）", "定义工艺", "", -1, -1);
            tech = tech.Trim().ToUpper();

            layer = Interaction.InputBox("请输入层次（通配符查询）", "定义层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            track = Interaction.InputBox("请输入TrackRecipe（通配符查询）", "定义TrackRecipe", "", -1, -1);
            track = track.Trim().ToUpper();






            // dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //   if (dataGridView1.Rows.Count == 1)
            //  { MessageBox.Show("请在表格首行的TECH/LAYER/TRACK栏输入查询信息\r\n若查询所有，对应栏目输入%"); return; }
            //  else
            // {
            //   tech = dataGridView1[0, 0].Value.ToString().Trim().ToUpper();//列写在前面
            //  layer = dataGridView1[1, 0].Value.ToString().Trim().ToUpper();
            //  track = dataGridView1[2, 0].Value.ToString().Trim().ToUpper();

            //   MessageBox.Show(tech + "," + layer + "," + track);
            // }

            MessageBox.Show("查询关键字\r\n\r\n" +
                "    工艺：       "+ tech + "\r\n\r\n" +
                "    层次：       " + layer + "\r\n\r\n" +
                "    TrackRecipe：" + track);
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_ILLUMINATION WHERE TECH LIKE '%" + tech + "%' AND LAYER LIKE '%" + layer + "%' AND TRACK LIKE '%" + track + "%'";
                MessageBox.Show(sql);
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

                            }

                            dataGridView1.DataSource = tblTmp;
                            tech = layer = track = "";

                        }
                    }
                }
            }



        }

        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)//插入
        {
            string type, comment;
            tech = dataGridView1[0, 0].Value.ToString().Trim().ToUpper();//列写在前面
            if (tech.Length != 12)
            { MessageBox.Show("工艺代码长度必须为12为，退出，请确认"); return; }
            layer = dataGridView1[1, 0].Value.ToString().Trim().ToUpper();
            track = dataGridView1[2, 0].Value.ToString().Trim().ToUpper();
            type = dataGridView1[4, 0].Value.ToString().Trim().ToUpper();
            if (type.Substring(0, 3) == "ANN" || type.Substring(0, 3) == "CON")
            { }
            else
            { MessageBox.Show("Illumination Type只能输入\"CON\"或\"ANN\",退出，请重新输入"); return; }

            try
            {
                aperture = (double)dataGridView1[5, 0].Value;
                outer = (double)dataGridView1[6, 0].Value;
                inner = (double)dataGridView1[7, 0].Value;
            }
            catch
            {
                MessageBox.Show("退出；\r\n\r\nNA/Outer/Inner栏请输入数字；\r\n\r\nConventional设置，Inner输入0"); return;
            }


            comment = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "," + dataGridView1[8, 0].Value.ToString().Trim();

            #region //判断是否已有数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_ILLUMINATION WHERE SUBSTR(TECH,1,3)='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                if (MessageBox.Show("需要更新的数据是：\r\n\r\nTECH: " + tech + "\r\nLAYER: " + layer + "\r\nTrackRecipe: " + track + "\r\nIllumination: " + type + "\r\nNA: " + aperture.ToString() + "\r\nOuterSigma: " + outer.ToString() + "\r\nInnerSigma: " + inner.ToString() + "\r\nComment: " + comment + "\r\n\r\n确认更改选\"是(Y)\"", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                tblTmp = ds.Tables[0];
                                if (tblTmp.Rows.Count > 0)
                                {
                                    if (MessageBox.Show("数据库中已有其它设置，请确认是否需要删除并插入新设置", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        //有数据，删除更新 
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
            if (tblTmp.Rows.Count > 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }
            }
            #endregion
            #region //插入数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " INSERT  INTO TBL_ILLUMINATION (TECH,LAYER,TRACK,KEY,TYPE,NA,OUTER,INNER,COMMENT,KEY1) VALUES ('" + tech + "','" + layer + "','" + track + "','" + tech + "_" + layer + "_" + track + "','" + type.Substring(0, 3).ToUpper() + "'," + aperture.ToString() + "," + outer.ToString() + "," + inner.ToString() + ",'" + comment + "','" + tech.Substring(1, 2) + "')";

                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " INSERT  INTO TBL_ILLUMINATION (TECH,LAYER,TRACK,KEY,TYPE,NA,OUTER,INNER,COMMENT,KEY1) VALUES ('" + tech.Substring(0, 3) + "','" + layer + "','" + track + "','" + tech.Substring(0, 3) + "_" + layer + "_" + track + "','" + type.Substring(0, 3).ToUpper() + "'," + aperture.ToString() + "," + outer.ToString() + "," + inner.ToString() + ",'" + comment + "','" + tech.Substring(1, 2) + "')";

                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            MessageBox.Show("数据（删除）插入结束");

            #endregion
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)//删除
        {
            string type;
            tech = dataGridView1[0, 0].Value.ToString().Trim().ToUpper();//列写在前面
            if (tech.Length != 12)
            { MessageBox.Show("工艺代码长度必须为12为，退出，请确认"); return; }
            layer = dataGridView1[1, 0].Value.ToString().Trim().ToUpper();
            track = dataGridView1[2, 0].Value.ToString().Trim().ToUpper();
            type = dataGridView1[4, 0].Value.ToString().Trim().ToUpper();
            if (type.Substring(0, 3) == "ANN" || type.Substring(0, 3) == "CON")
            { }
            else
            { MessageBox.Show("Illumination Type只能输入\"CON\"或\"ANN\",退出，请重新输入"); return; }

            try
            {
                aperture = (double)dataGridView1[5, 0].Value;
                outer = (double)dataGridView1[6, 0].Value;
                inner = (double)dataGridView1[7, 0].Value;
            }
            catch
            {
                MessageBox.Show("退出；\r\n\r\nNA/Outer/Inner栏请输入数字；\r\n\r\nConventional设置，Inner输入0"); return;
            }

            MessageBox.Show("需要删除的数据是：\r\n\r\nTECH: " + tech + "\r\nLAYER: " + layer + "\r\nTrackRecipe: " + track + "\r\nIllumination: " + type + "\r\nNA: " + aperture.ToString() + "\r\nOuterSigma: " + outer.ToString() + "\r\nInnerSigma: " + inner.ToString());

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech.Substring(0, 3) + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " DELETE  FROM TBL_ILLUMINATION WHERE TECH='" + tech + "' AND LAYER= '" + layer + "' AND TRACK = '" + track + "' AND UPPER(SUBSTR(TYPE,1,3))='" + type.Substring(0, 3) + "' AND ABS(na-" + aperture.ToString() + ")<0.0001 AND ABS(OUTER-" + outer.ToString() + ")<0.0001 AND ABS(INNER-" + inner.ToString() + ")<0.0001";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }



        }
        #endregion

        #region//Excel File
        private void sHEET名ToolStripMenuItem_Click(object sender, EventArgs e) //获得excel表名
        {




            string filepath = "";

            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  

            if (MessageBox.Show("Bias Table-->否(N)\r\n\r\nFlow -->是(Y)\"", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                file.InitialDirectory = @"P:\_flow\";
            }
            else
            {
                file.InitialDirectory = @"P:\Recipe\Biastable\";
            }
            file.Filter = "文档(*" + part + "*.xls)|*" + part + "*.xls|文档(*" + part + "*.xlsx)|*" + part + "*.xlsx";
            if (file.ShowDialog() == DialogResult.OK) filepath = file.FileName;
            //   string fileNameWithoutExtension = System.IO.Path.GetDirectoryName(filepath);// 没有扩展名的文件名 “Default”

            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            ListExcelSheetName(filepath, listBox1);

            sql = filepath;


        }
        private void 读取文件ToolStripMenuItem_Click(object sender, EventArgs e) //read excel
        {
            tblTmp = null;
            //https://www.cnblogs.com/ammy714926/p/4905026.html
            string sheetName;


            if (listBox1.SelectedItems.Count > 0)
            {
                sheetName = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("请选择查阅的Excel表"); return;
            }

            string filepath = sql;
            sql = string.Empty;
            string strTmp = filepath.Trim().Substring(filepath.Length - 3, 3);
            string excelStr = string.Empty; //此判断无用，以上以指定xls后缀，后续弹出框选文件时有用
            if (strTmp == "xls")
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意

            }

            else if (strTmp == "lsx")
            {

                excelStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filepath + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';";

            }
















            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    sql = string.Format("SELECT * FROM  [{0}]", sheetName);

                  
                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                    {

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        tblTmp = ds.Tables[0];
                    }

                }
                // MessageBox.Show("数据绑定Excel成功");
                dataGridView1.DataSource = tblTmp;
            }
            catch (Exception)
            {
                MessageBox.Show("数据绑定Excel失败");
            }



        }
        private void fLOWToolStripMenuItem_Click(object sender, EventArgs e)//获得流程相关信息
        {
            FlowToDataTable();
            /*
         string eqptype, oldPpid, newPpid, ppid, stage;
         tblFlow = new System.Data.DataTable();
         tblFlow.Columns.Add("stage", Type.GetType("System.String"));
         tblFlow.Columns.Add("eqptype", Type.GetType("System.String"));
         tblFlow.Columns.Add("ppid", Type.GetType("System.String"));
         tblFlow.Columns.Add("track", Type.GetType("System.String"));
         
         foreach (DataRow item in tblTmp.Rows)
         {
             try
             {
                 eqptype = (item[6].ToString()).Substring(0, 3);

                 if (eqptype == "LDI" || eqptype == "LII" )
                 {
                     stage = item[5].ToString().ToUpper().Trim();
                     oldPpid = item[8].ToString().ToUpper().Trim();
                     newPpid = item[8].ToString().ToUpper().Trim();
                     if (newPpid == "")
                     { ppid = oldPpid; }
                     else
                     { ppid = newPpid; }

                     DataRow newRow = tblFlow.NewRow();
                     newRow["stage"] = stage;
                     newRow["eqptype"] = eqptype;

                     newRow["ppid"] = ppid.Split(new char[] { '.' })[1];
                     if (eqptype == "LDI")
                     { newRow["track"] = ppid.Split(new char[] { ';' })[0]; }
                     else
                     { newRow["track"] = ""; }

                     tblFlow.Rows.Add(newRow);
                     dataGridView1.DataSource = tblFlow;



                 }
             }
             catch //eqptye length=0
             {
             }
                
         }
         //https://www.cnblogs.com/sunxi/p/4767577.html
         DataView dv = tblFlow.DefaultView;
         tblTmp= dv.ToTable("Dist", true, "stage","eqptype","ppid","track");
         tblFlow = tblTmp.Copy();

         dataGridView1.DataSource = tblFlow;
          **/
        }
        private void biasTableToolStripMenuItem_Click(object sender, EventArgs e) // read bias table
        {
            BaistableToDataTable();
            /*
            //读取biastable
            tblBt = new System.Data.DataTable();
            tblBt.Columns.Add("code", Type.GetType("System.String"));
            tblBt.Columns.Add("ppid", Type.GetType("System.String"));
            tblBt.Columns.Add("mask", Type.GetType("System.String"));
            tblBt.Columns.Add("mlm", Type.GetType("System.String"));
            tblBt.Columns.Add("maskLabel", Type.GetType("System.String"));
            tblBt.Columns.Add("eqptype", Type.GetType("System.String"));
            tblBt.Columns.Add("ovlto", Type.GetType("System.String"));

            string mask, code, ppid, mlm, maskLabel, eqptype, ovlto;
            foreach (DataRow item in tblTmp.Rows)
            {
                try
                {
                    mask = item[4].ToString().Trim().ToUpper();
                     if (mask.Length==8 && mask.Substring(4,1)=="-")
                     {
                         code = item[0].ToString().Trim().ToUpper();
                         ppid = item[3].ToString().Trim().ToUpper();
                         mlm = item[5].ToString().Trim().ToUpper();
                         maskLabel = item[6].ToString().Trim().ToUpper();
                         eqptype = item[17].ToString().Trim().ToUpper();
                         ovlto = item[19].ToString().Trim().ToUpper();

                         DataRow newRow = tblBt.NewRow();
                         newRow["code"] = code;
                         newRow["ppid"] = ppid;
                         newRow["mask"] = mask;
                         newRow["mlm"] = mlm;
                         newRow["maskLabel"] = maskLabel;
                         newRow["eqptype"] = eqptype;
                         newRow["ovlto"] = ovlto;
                         tblBt.Rows.Add(newRow);
                     }
                }
                catch { }
         
            }
            dataGridView1.DataSource = tblBt;

            */

        }
        private void 合并FlowBiasTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeFlowBiasTable();
            /*
            if (tblBt.Rows.Count != tblFlow.Rows.Count)
            { MessageBox.Show("BiasTable中的工艺层次数量和Flow中的工艺层次数量不匹配；\r\n\r\n退出\r\n\r\n请重新确认"); return; }

            tblFlow.Columns.Add("code");
            tblFlow.Columns.Add("mask");
            tblFlow.Columns.Add("mlm");
            tblFlow.Columns.Add("maskLabel");
            tblFlow.Columns.Add("ovlto");
            tblFlow.Columns.Add("toFlag");
            tblFlow.Columns.Add("illumination");
            string toFlag = "N";

            for (int i = 0; i < tblFlow.Rows.Count; i++)
            {
                
                foreach (DataRow item in tblBt.Rows)
                {
                   
                    if (tblFlow.Rows[i]["ppid"].ToString() == item["ppid"].ToString())
                    {
                        tblFlow.Rows[i]["code"] = item["code"].ToString();
                        tblFlow.Rows[i]["mask"] = item["mask"].ToString(); 
                        tblFlow.Rows[i]["mlm"] = item["mlm"].ToString(); 
                        tblFlow.Rows[i]["maskLabel"] = item["maskLabel"].ToString();
                        tblFlow.Rows[i]["ovlto"] = item["ovlto"].ToString();
                        tblFlow.Rows[i]["toFlag"]=toFlag;
                        if (tblFlow.Rows[i]["eqptype"].ToString() == "LDI")
                        {
                            tblFlow.Rows[i]["illumination"] = GetNaSigma(item["code"].ToString(), tblFlow.Rows[i]["ppid"].ToString(), tblFlow.Rows[i]["track"].ToString());
                        }
                        else
                        {
                            tblFlow.Rows[i]["illumination"] = "";
                        }
                        if (tblFlow.Rows[i]["ppid"].ToString() == "TO") { toFlag = "Y"; }
                        break;
                    }
                }
            }
            dataGridView1.DataSource = tblFlow;
            */


        }
        private void 读入显示坐标文件ToolStripMenuItem_Click(object sender, EventArgs e)
        { ReadCoordinateFile(); }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // k,StepX,StepY,DieX,DieY,OffsetX,OffsetY,AreaRatio,FullCover,PartType,NikonFlag,Part,Riqi
            // k , sx , sy , dx  dy    ox     oy +     areaRatio fullCover  parttype  nikonFlag  savePart  DateTime.Now.ToString() 
            //public static void Plot(Graphics wfrmap, float k, float sx, float sy, float dx, float dy, float ox, float oy, double areaRatio, bool fullCover, string parttype, bool nikonFlag, string savePath)
            float k, sx, sy, dx, dy, ox, oy;
            double areaRatio;
            bool fullCover;
            string parttype, savePart;
            bool nikonFlag;

            part = textBox1.Text.Trim().ToUpper();


            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_MAP WHERE PART LIKE '" + part + "%'";
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

            string filePath;
            Bitmap bmp;
            for ( int i=0;i<tblTmp.Rows.Count;i++)
            {

                
                k =(float)  Convert.ToDouble(  tblTmp.Rows[i][0]);
              
                sx = (float)Convert.ToDouble(tblTmp.Rows[i][1]); sy = (float)Convert.ToDouble(tblTmp.Rows[i][2]);
                dx = (float)Convert.ToDouble(tblTmp.Rows[i][3]); dy = (float)Convert.ToDouble(tblTmp.Rows[i][4]);
                ox = (float)Convert.ToDouble(tblTmp.Rows[i][5]); oy = (float)Convert.ToDouble(tblTmp.Rows[i][6]);
                areaRatio = (double)tblTmp.Rows[i][7];
                fullCover = (bool)tblTmp.Rows[i][8];
                parttype=(string)tblTmp.Rows[i][9];
                nikonFlag=(bool)tblTmp.Rows[i][10];
                savePart=(string)tblTmp.Rows[i][11];

                filePath = "C:\\temp\\"+savePart+".emf";




                bmp = new Bitmap(1169, 827); //实际三楼打印机的设置
                Graphics gs = Graphics.FromImage(bmp);
                System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                Graphics wfrmap = Graphics.FromImage(mf);

                Map.DieQty.Plot(wfrmap, k, sx, sy, dx, dy, ox, oy, areaRatio, fullCover, parttype, nikonFlag, savePath,part);
                // public static void Plot(Graphics wfrmap, float k, float sx, float sy, float dx, float dy, float ox, float oy,double areaRatio)
                wfrmap.Save();
                wfrmap.Dispose();
                mf.Dispose();

                try
                {
                    //若直接引用，同名文件修改后无法正常显示；
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                    printPicPath = filePath;
                    this.tabControl1.SelectedTab = this.tabPage2;
                    if (MessageBox.Show("是否需要打印图片", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        PirntScreenPicture();
                    }

                    else
                    { }

                       
                }
                catch
                {
                    MessageBox.Show("文件显示失败");
                }

            }

        }


        #endregion

        #region public function
        private void ListExcelSheetName(string filepath, ListBox listBox1)
        {
            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"
            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值
            // 3、判断是xls还是xlsx
            // string[] sArray = filepath.Split('.');//有问题,路径名带"."

            string strTmp = filepath.Trim().Substring(filepath.Length - 3, 3);

            string excelStr = string.Empty;
            if (strTmp == "xls")
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意

            }

            else if (strTmp == "lsx")
            {

                excelStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filepath + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';";

            }


            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    System.Data.DataTable sheetsName = OleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    listBox1.Items.Clear();
                    for (int i = 0; i < sheetsName.Rows.Count; i++)
                    {
                        strTmp = sheetsName.Rows[i][2].ToString();
                        // if (strTmp.Substring(strTmp.Length - 1, 1) == "$"||strTmp.Substring(strTmp.Length - 2, 1) == "$");
                        //{
                        listBox1.Items.Add(sheetsName.Rows[i][2].ToString());
                        //}

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("READING EXCEL ERROR");
            }





            /*
               





                
              
 




            /// 
             2 /// 写入Excel文档
             3 /// 
             4 public bool SaveFP2toExcel(string filePathath)
             5 {
             6     try
             7     {
             8         string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ filePathath +";Extended Properties=Excel 8.0;";
             9         OleDbConnection conn = new OleDbConnection(strConn);
            10         conn.Open();  
            11         System.Data.OleDb.OleDbCommand cmd=new OleDbCommand ();
            12         cmd.Connection =conn;
            13 
            14         for(int i=0;i0].RowCount -1;i++)
            15         {
            16             if(fp2.Sheets [0].Cells[i,0].Text!="")
            17             {
            18                 cmd.CommandText ="INSERT INTO [sheet1$] (工号,姓名,部门,职务,日期,时间) VALUES('"+fp2.Sheets [0].Cells[i,0].Text+ "','"+
            19                 fp2.Sheets [0].Cells[i,1].Text+"','"+fp2.Sheets [0].Cells[i,2].Text+"','"+fp2.Sheets [0].Cells[i,3].Text+
            20                 "','"+fp2.Sheets [0].Cells[i,4].Text+"','"+fp2.Sheets [0].Cells[i,5].Text+"')";
            21                 cmd.ExecuteNonQuery ();
            22             }
            23         }
            24         
            25         conn.Close ();
            26         return true;
            27     }
            28     catch(System.Data.OleDb.OleDbException ex)
            29     {
            30         Console.WriteLine ("写入Excel发生错误："+ex.Message );
            31         return false;
            32     }
            33 }
            ————————————————
            版权声明：本文为CSDN博主「彭世瑜」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
            原文链接：https://blog.csdn.net/mouday/article/details/81049212


            */



        }
        private string GetNaSigma(string code, string layer, string track)
        {


            // code = "C18";
            //layer = "SN";
            //track = "Y2NND2";
            string setting = "";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_ILLUMINATION WHERE TECH ='" + code.Substring(0, 3) + "' AND LAYER = '" + layer + "' AND TRACK = '" + track + "'";

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
                                //MessageBox.Show("未查询到数据，请重新输入查询条件");
                                setting = "未查到设置";
                                return setting;


                            }

                            //dataGridView1.DataSource = tblTmp;
                            //tech = layer = track = "";

                        }
                    }
                }
            }

            if (tblTmp.Rows.Count == 1)
            {
                setting = tblTmp.Rows[0]["Type"].ToString().Substring(0, 3) + ", " + tblTmp.Rows[0]["NA"].ToString() + ",  " + tblTmp.Rows[0]["Outer"].ToString() + ",  " + tblTmp.Rows[0]["Inner"].ToString();

                return setting;
            }
            else
            {
                setting = "多重设置"; return setting;
            }
        }
        private void FlowToDataTable()
        {

        
            string eqptype, oldPpid, newPpid, ppid, stage;
            tblFlow = new System.Data.DataTable();
            tblFlow.Columns.Add("stage", Type.GetType("System.String"));
            tblFlow.Columns.Add("eqptype", Type.GetType("System.String"));
            tblFlow.Columns.Add("ppid", Type.GetType("System.String"));
            tblFlow.Columns.Add("track", Type.GetType("System.String"));

            bool stepFlag , stageFlag, recpFlag, ppidFlag;
            stepFlag = stageFlag = recpFlag = ppidFlag = false;
            int no1 = 0;int no2 = 0; int no3 = 0;int no4 = 0;

           
            foreach (DataRow row in tblTmp.Rows)
            {
                
                for (int i=0;i<tblTmp.Columns.Count;i++)
                { 
                    if (row[i].ToString().ToUpper().Trim().Contains ("步骤")){  stepFlag = true; no1 = i;}
                    if (row[i].ToString().ToUpper().Trim().Contains( "STAGE")) { stageFlag = true; no2 = i; }
                    if (row[i].ToString().ToUpper().Trim().Contains( "RECPNAME")) { recpFlag = true; no3 = i; }
                    if (row[i].ToString().ToUpper().Trim(). Contains( "PPID")) { ppidFlag = true; no4 = i;  }
                    if (stepFlag && stageFlag && recpFlag && ppidFlag & no1 > 0) { break; }
                }
               
            }
            if (stepFlag && stageFlag && recpFlag && ppidFlag & no1 > 0)
            { }
            else
            {
                MessageBox.Show("未找到 \"步骤\" \"STAGE\" \"RECPNAME\" \"PPID\"等关键字，退出，请确认流程是否正确");
                return;

            }

           // MessageBox.Show(no1.ToString() + "," + no2.ToString() + "," + no3.ToString() + "," + no4.ToString());



            foreach (DataRow item in tblTmp.Rows)
            {
          
                try
                {
                    eqptype = (item[no3].ToString()).Substring(0, 3);

                    if ((eqptype == "LDI" || eqptype == "LII") && (item[no1 - 1].ToString().Length==0 || item[no1-1].ToString().Substring(0, 1) != "删"))
                    {
                        stage = item[no2].ToString().ToUpper().Trim();
                        oldPpid = item[no4].ToString().ToUpper().Trim();
                    
                        newPpid = item[no4+1].ToString().ToUpper().Trim();
                  
                        if (newPpid == "")
                        { ppid = oldPpid; }
                        else
                        { ppid = newPpid; }

                        DataRow newRow = tblFlow.NewRow();
                        newRow["stage"] = stage;
                        newRow["eqptype"] = eqptype;

                        newRow["ppid"] = ppid.Split(new char[] { '.' })[1];
                        if (eqptype == "LDI")
                        { newRow["track"] = ppid.Split(new char[] { ';' })[0]; }
                        else
                        { newRow["track"] = ""; }

                        tblFlow.Rows.Add(newRow);
                        dataGridView1.DataSource = tblFlow;



                    }
                }
                catch //eqptye length=0
                {
                }
            }
            DataView dv = tblFlow.DefaultView;
            tblTmp = dv.ToTable("Dist", true, "stage", "eqptype", "ppid", "track");
            tblFlow = tblTmp.Copy();

            dataGridView1.DataSource = tblFlow;
           
        }
        private void BaistableToDataTable() //从excle读取biastable后，选取有用数据
        {
            //读取biastable
            tblBt = new System.Data.DataTable();
            tblBt.Columns.Add("code", Type.GetType("System.String"));
            tblBt.Columns.Add("ppid", Type.GetType("System.String"));
            tblBt.Columns.Add("mask", Type.GetType("System.String"));
            tblBt.Columns.Add("mlm", Type.GetType("System.String"));
            tblBt.Columns.Add("maskLabel", Type.GetType("System.String"));
            tblBt.Columns.Add("eqptype", Type.GetType("System.String"));
            tblBt.Columns.Add("ovlto", Type.GetType("System.String"));

            string mask, code, ppid, mlm, maskLabel, eqptype, ovlto;
            foreach (DataRow item in tblTmp.Rows)
            {
                try
                {

                    mask = item[4].ToString().Trim().ToUpper();


                    if ((mask.Length == 8 && mask.Substring(4, 1) == "-") || mask.Contains("ZERO"))
                    {

                        // MessageBox.Show(mask+","+ item[0].ToString().Trim().ToUpper()+","+ item[3].ToString().Trim().ToUpper()+","+ item[5].ToString().Trim().ToUpper()
                        //     +","+ item[6].ToString().Trim().ToUpper()+"," + item[17].ToString().Trim().ToUpper()+","+item[19].ToString().Trim().ToUpper());

                        code = item[0].ToString().Trim().ToUpper();
                        ppid = item[3].ToString().Trim().ToUpper();
                        mlm = item[5].ToString().Trim().ToUpper();
                        maskLabel = item[6].ToString().Trim().ToUpper();

                        if (maskLabel.Contains("-"))
                        { maskLabel = maskLabel.Split(new char[] { '-' })[1].Trim().Substring(0, 2); }
                        if (maskLabel.Length > 3 && mask != "FAB2-ZERO1")
                        { maskLabel = maskLabel.Substring(0, 2); }
                       


                        eqptype = item[17].ToString().Trim().ToUpper();
                        ovlto = item[19].ToString().Trim().ToUpper();



                        DataRow newRow = tblBt.NewRow();
                        newRow["code"] = code;
                        newRow["ppid"] = ppid;
                        newRow["mask"] = mask;
                        newRow["mlm"] = mlm;
                        newRow["maskLabel"] = maskLabel;
                        newRow["eqptype"] = eqptype;
                        newRow["ovlto"] = ovlto;
                        tblBt.Rows.Add(newRow);


                    }
                }
                catch { }

            }
            dataGridView1.DataSource = tblBt;
         

            //判读 tblBt中的工艺代码是否唯一
            if (tblBt.DefaultView.ToTable(true, "code").Rows.Count == 1)
            {
                //工艺代码唯一

            }
            else
            {
                MessageBox.Show("注意，Bias Table各层次标注的工艺代码不唯一；\r\n\r\n后续以第一层的为准");
            }

            fullTech = tblBt.Rows[0]["code"].ToString();




        }
        private void MergeFlowBiasTable()
        {
            if (tblBt.Rows.Count != tblFlow.Rows.Count)
            { MessageBox.Show("流程:      "+ tblFlow.Rows.Count.ToString()+"层\r\n\r\n"+
                              "BiasTable: "+ tblBt.Rows.Count.ToString()+"层\r\n\r\n"+
                              "工艺层次数量不匹配,退出,请重新确认"); return; }

            tblFlow.Columns.Add("code");
            tblFlow.Columns.Add("mask");
            tblFlow.Columns.Add("mlm");
            tblFlow.Columns.Add("maskLabel");
            tblFlow.Columns.Add("ovlto");
            tblFlow.Columns.Add("toFlag");
            tblFlow.Columns.Add("zaFlag");
            tblFlow.Columns.Add("illumination");
            string toFlag = "N";
            string zaFlag = "N";
            bool flag;//判断flow中的PPID是否和Bias table中的PPID匹配
  


            for (int i = 0; i < tblFlow.Rows.Count; i++)
            {
                flag = false;

                foreach (DataRow item in tblBt.Rows)
                {

                    if (tblFlow.Rows[i]["ppid"].ToString() == item["ppid"].ToString())
                    {
                        tblFlow.Rows[i]["code"] = item["code"].ToString();
                        tblFlow.Rows[i]["mask"] = item["mask"].ToString();
                        tblFlow.Rows[i]["mlm"] = item["mlm"].ToString();
                        tblFlow.Rows[i]["maskLabel"] = item["maskLabel"].ToString();
                        tblFlow.Rows[i]["ovlto"] = item["ovlto"].ToString();
                        tblFlow.Rows[i]["toFlag"] = toFlag;
                        tblFlow.Rows[i]["zaFlag"] = zaFlag;
                        if (tblFlow.Rows[i]["eqptype"].ToString() == "LDI")
                        {
                            tblFlow.Rows[i]["illumination"] = GetNaSigma(item["code"].ToString(), tblFlow.Rows[i]["ppid"].ToString(), tblFlow.Rows[i]["track"].ToString());
                        }
                        else
                        {
                            tblFlow.Rows[i]["illumination"] = "";
                        }
                        if (tblFlow.Rows[i]["ppid"].ToString() == "TO") { toFlag = "Y"; }
                        if (tblFlow.Rows[i]["ppid"].ToString() == "ZA") { zaFlag = "Y"; }
                        flag = true;
                        break;
                    }
                }
                if (flag == false) { MessageBox.Show("Bias Table中的PPID和流程中的PPID不匹配，脚本无法正常运行,退出");return; }
            }
      
            AlignMethod();
            dataGridView1.DataSource = tblFlow;

            // MessageBox.Show("Flow和BiasTable数据合并结束,继续运行AlignTo命令，定义对位层次");
            //  ReadCoordinateFile();
            //   MessageBox.Show("坐标文件读取完毕");
            //  MatchCoordinate();


        }
        private void AlignTo() //定义alignment tree
        {
            tblFlow.Columns.Add("alignto");
            //      T_Nikon= {'RN':'TO','SN':'TR','TT':'W1','CP':'A1'}


            // H_Asml = {"W2":"W1",'W3':'W2','W4':'W3','W5':'W4','W6':'W5','W7':'W6',
            //          "A1":"W1",'A2':'W2','A3':'W3','A4':'W4','A5':'W5','A6':'W6'}
            Dictionary<string, string> myDictN = new Dictionary<string, string>();
            Dictionary<string, string> myDictA = new Dictionary<string, string>();

            myDictN.Add("W2", "A1"); myDictN.Add("W3", "A2"); myDictN.Add("A1", "W1"); myDictN.Add("A2", "W2");
            myDictN.Add("NP", "GT"); myDictN.Add("PP", "GT"); myDictN.Add("W4", "A3"); myDictN.Add("A3", "W3");
            myDictN.Add("A4", "W4"); myDictN.Add("A5", "W5");

            myDictA.Add("W2", "W1"); myDictA.Add("W3", "W2"); myDictA.Add("W4", "W3"); myDictA.Add("W5", "W4");
            myDictA.Add("W6", "W5"); myDictA.Add("W7", "W6"); myDictA.Add("A1", "W1"); myDictA.Add("A2", "W2");
            myDictA.Add("A3", "W3"); myDictA.Add("A4", "W4"); myDictA.Add("A5", "W5"); myDictA.Add("A6", "W6");
            myDictA.Add("A7", "W7");
            //https://www.cnblogs.com/ChenMM/p/9479987.html
            string[] layersN = { "W2", "W3", "A1", "A2", "A3","A4","A5", "W4", "NP", "PP" };
            string[] layersA = { "W2", "W3", "W4", "W5", "W6", "W7", "A1", "A2", "A3", "A4", "A5", "A6", "A7" };


            for (int i = 1; i < tblFlow.Rows.Count; i++)
            {

                if (tblFlow.Rows[i]["code"].ToString().Substring(0, 1) == "T") //DMOS
                {
                    #region //DMOS
                  //  if (tblFlow.Rows[i]["ppid"].ToString() == "RN")
                  //  { tblFlow.Rows[i]["alignto"] = "TO"; }
                     if (tblFlow.Rows[i]["ppid"].ToString() == "SN" || tblFlow.Rows[i]["ppid"].ToString() == "W1")
                    { tblFlow.Rows[i]["alignto"] = "TR"; }
                    else if (tblFlow.Rows[i]["ppid"].ToString() == "TT")
                    { tblFlow.Rows[i]["alignto"] = "W1"; }
                    else if (tblFlow.Rows[i]["ppid"].ToString() == "CP")
                    { tblFlow.Rows[i]["alignto"] = "A1"; }
                    else
                    {
                        if (tblFlow.Rows[i]["ovlto"].ToString().Trim().Length>1)
                        { 
                            tblFlow.Rows[i]["alignto"]=tblFlow.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2);
                        }
                        else
                        {
                
                        MessageBox.Show("DMOS产品Align Tree的层次不完善，请通知更改");
                        }
                    }
                    #endregion
                }

                else //NON-DMOS
                {
                    string str1 = tblFlow.Rows[i]["ppid"].ToString();

                    //MessageBox.Show(str1);
                  
                    if (tblFlow.Rows[i]["eqptype"].ToString() == "LII") //Nikon
                    {
                        #region //NIkon
                        if (layersN.Contains(str1)) //metal,hole,NP,PP
                        { tblFlow.Rows[i]["alignto"] = myDictN[str1]; }
                        else if (str1 == "CT")
                        {
                            if (i >= 1)
                            {
                                if (tblFlow.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2);  }
                            }
                            else if (i >= 2)
                            {
                                if (tblFlow.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (tblFlow.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "WT")
                        {
                            if (i >= 1)
                            {
                                if (tblFlow.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 2)
                            {
                                if (tblFlow.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (tblFlow.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "AT" || str1 == "TT")
                        {
                            if (i >= 1)
                            {
                                if (tblFlow.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 2)
                            {
                                if (tblFlow.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (tblFlow.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "CP" || str1 == "CF" || str1 == "PI" || str1 == "P2" || str1 == "PN")
                        {

                            if (tblFlow.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "A" || tblFlow.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "T")
                            { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            else
                            {
                                if (i >= 2)
                                {
                                    if (tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "A" || tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "T")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                            }
                        }
                        //目前没有规定 SN OVL 量到 GT，必须对GT
                        //else if (tblFlow.Rows[i]["ovlto"].ToString().Length > 1)
                        //{ tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString(); }
                        else if (tblFlow.Rows[i]["toFlag"].ToString() == "N" && tblFlow.Rows[i]["zaFlag"].ToString() == "Y")
                        { tblFlow.Rows[i]["alignto"] = "ZA"; }
                        else if (str1 == "TO" && tblFlow.Rows[i]["zaFlag"].ToString() == "N" && tblFlow.Rows[i]["ovlto"].ToString().Length > 0)
                        { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }

                        else if (tblFlow.Rows[i]["toFlag"].ToString() == "Y")
                        { tblFlow.Rows[i]["alignto"] = "TO"; }
                        else
                        {
                            tblFlow.Rows[i]["alignto"] = tblFlow.Rows[0]["maskLabel"].ToString().Trim().Substring(0, 2);
                            MessageBox.Show("无ZA层，TO及之前的层次，统一对位到第一层\r\n\r\nNikon Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                        }
                        #endregion

                    }

                    else //ASML
                    {
                        #region //ADVANCED ASML
                        if (tblFlow.Rows[i]["code"].ToString().Substring(1, 1) == "1")
                        {
                            if (layersA.Contains(str1)) //Asml Metal,Hole       
                            { tblFlow.Rows[i]["alignto"] = myDictA[str1]; }
                            else if (str1 == "AT" || str1 == "TT" || str1 == "WT" || str1 == "CT" || str1 == "OE" || str1 == "OV") //-->OE，OV可能变化
                            {

                                try
                                {
                                    if (tblFlow.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                    else if (tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                    else if (tblFlow.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                { 
                                    
                                    tblFlow.Rows[i]["alignto"] = "请修改脚本";

                                }

                            }
                            else if (str1 == "W1")
                            {
                                if (tblFlow.Rows[i]["code"].ToString().Substring(0, 3) == "C18")
                                { tblFlow.Rows[i]["alignto"] = "TO"; }
                                else if (tblFlow.Rows[i]["ovlto"].ToString().Length > 1)
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                                else
                                { tblFlow.Rows[i]["alignto"] = "TO"; }
                            }
                            //目前没有规定 SN OVL 量到 GT，必须对GT
                            // else if (tblFlow.Rows[i]["ovlto"].ToString().Length > 1)
                            // { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString(); }
                            else if (tblFlow.Rows[i]["toFlag"].ToString() == "N" && tblFlow.Rows[i]["zaFlag"].ToString() == "Y")
                            { tblFlow.Rows[i]["alignto"] = "ZA"; }
                            else if (str1 == "TO" && tblFlow.Rows[i]["zaFlag"].ToString() == "N" && tblFlow.Rows[i]["ovlto"].ToString().Length > 0)
                            { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                            else if (tblFlow.Rows[i]["toFlag"].ToString() == "Y")
                            { tblFlow.Rows[i]["alignto"] = "TO"; }
                            else
                            {
                                tblFlow.Rows[i]["alignto"] = tblFlow.Rows[0]["maskLabel"].ToString();
                                MessageBox.Show("TO及之前的层次，统一对位到第一层\r\n\r\nAsml Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                            }

                        }
                        #endregion
                        #region //lowend ASML,almost copy from Nikon Setting (HOLE align to Metal)
                        else
                        {
                          

                            if (layersN.Contains(str1)) //metal,hole,NP,PP
                            { tblFlow.Rows[i]["alignto"] = myDictN[str1]; }
                            else if (str1 == "W1")
                            {
                                if (tblFlow.Rows[i]["code"].ToString().Substring(0, 3) == "C18") //copy from advanced process，to be delete
                                { tblFlow.Rows[i]["alignto"] = "TO"; }
                                else if (tblFlow.Rows[i]["ovlto"].ToString().Length > 1)
                                { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                                else
                                { tblFlow.Rows[i]["alignto"] = "TO"; }
                            }
                            else if (str1 == "CT")
                            {
                                try
                                {

                                    if (tblFlow.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (tblFlow.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                {
                                    tblFlow.Rows[i]["alignto"] = "请修改脚本";
                                }
                              
                            }
                            else if (str1 == "WT")
                            {
                                try
                                {

                                    if (tblFlow.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "A")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "A")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (tblFlow.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "A")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                { tblFlow.Rows[i]["alignto"] = "请修改脚本"; }
                               
                            }
                            else if (str1 == "AT" || str1 == "TT")
                            {
                                try
                                {

                                    if (tblFlow.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }


                                    if (tblFlow.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }


                                    if (tblFlow.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                {
                                    tblFlow.Rows[i]["alignto"] = "请修改脚本";
                                }

                            }
                            //目前没有规定 SN OVL 量到 GT，必须对GT
                            //else if (tblFlow.Rows[i]["ovlto"].ToString().Length > 1)
                            //{ tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString(); }
                            else if (tblFlow.Rows[i]["toFlag"].ToString() == "N" && tblFlow.Rows[i]["zaFlag"].ToString() == "Y")
                            { tblFlow.Rows[i]["alignto"] = "ZA"; }
                            else if (str1 == "TO" && tblFlow.Rows[i]["zaFlag"].ToString() == "N" && tblFlow.Rows[i]["ovlto"].ToString().Length > 0)
                            { tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString(); }
                            else if (tblFlow.Rows[i]["toFlag"].ToString() == "Y")
                            { tblFlow.Rows[i]["alignto"] = "TO"; }
                            else if (tblFlow.Rows[i]["toFlag"].ToString() == "N" && tblFlow.Rows[i]["ovlto"].ToString().Length > 0)
                            {
                               
                                tblFlow.Rows[i]["alignto"] = tblFlow.Rows[i]["ovlto"].ToString();
                            }

                            else
                            {
                                tblFlow.Rows[i]["alignto"] = tblFlow.Rows[0]["maskLabel"].ToString();
                                MessageBox.Show("TO及之前的层次，统一对位到第一层\r\n\r\nAsml Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                            }
                        }
                        #endregion
                    }
                }
            }

            MessageBox.Show("除第一层外，AlignTo列必须有被对位层次；\r\n\r\n否则读取坐标时会报错；\r\n\r\n请确认本步骤正确，再读取坐标");

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 从ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
         //   for (int i = 0; i < tblTmp.Rows.Count; i++)
        //    {
              
              
        //        if (tblTmp.Rows[i]["Type"].ToString().Trim().Substring(0,3).ToUpper()=="CON")
        //        {
        //            tblTmp.Rows[i]["Inner"] = 0;
        //        }
        //    }
         //   dataGridView1.DataSource = tblTmp;
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblTmp, "tbl_illumination");
            myTabInfo.ImportToSqliteBatch(tblTmp, dbPath);
        }

       
        private void AlignMethod()
        {
            //check whether it is narrow mark
            //bool narrowScribelaneFlag;
            string part = textBox1.Text.Trim().ToUpper();
            string filePath = "P:\\Recipe\\Coordinate\\" +part + ".txt";
            if (System.IO.File.Exists(filePath) == false)
            {
                MessageBox.Show("坐标文件不存在,手动选择");
                OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
                file.InitialDirectory = "P:\\Recipe\\Coordinate\\";


                file.Filter = "文档(" + part.Substring(0, 6) + "*.txt)|*.txt";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    filePath = file.FileName;

                }

                if (file.FileName.Length == 0)//判断有没有文件导入  
                {
                    

                    if (MessageBox.Show("未选择坐标文件,请手动设定产品是否为60um划片槽\r\n\r\nYes(是）-->是60um划片槽\r\n\r\nNo (否）-->非60um划片槽", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        narrowScribelaneFlag = true;
                    }
                    else
                    { narrowScribelaneFlag = false; }
                }
                else
                { narrowScribelaneFlag = CheckNarrowMarkFromCoordinateFile(filePath); }

            }

            else
            {
                narrowScribelaneFlag = CheckNarrowMarkFromCoordinateFile(filePath);
            }





            tblFlow.Columns.Add("method");

            string tech, layer, tool;
            for (int i = 0; i < tblFlow.Rows.Count; i++)
            {
              
                tech = tblFlow.Rows[i]["code"].ToString().Substring(0, 3).Trim().ToUpper();
                layer = tblFlow.Rows[i]["ppid"].ToString().Trim().ToUpper();
                tool = tblFlow.Rows[i]["eqptype"].ToString().Trim().ToUpper();
                if (tool == "LII")
                {
                    #region
                    if (tech.Substring(0, 1) == "T") //DMOS
                    {
                        
                        if (layer=="CP"||layer=="TT") { tblFlow.Rows[i]["method"] = "FIA/FIA"; }
                        else if (layer=="SN"||layer=="SP") { tblFlow.Rows[i]["method"] = "LSA/FIA"; }
                        else { tblFlow.Rows[i]["method"] = "LSA/LSA"; }

                    }
                    else //NonDMOS
                    {
                        if (layer.Substring(0, 1) == "W" || layer.Substring(0, 1) == "A"  || layer == "CT")
                        { tblFlow.Rows[i]["method"] = "LSA/FIA"; }
                        else if (layer == "TT") 
                        {  tblFlow.Rows[i]["method"] = "FIA/FIA";  }
                        else
                        {
                            string[] mylayers = { "CP", "PN", "CF", "PI", "P2" };
                            if (mylayers.Contains(layer))
                            { tblFlow.Rows[i]["method"] = "LSA/LSA"; }
                            else
                            {
                                if (tech == "M52" || tech.Substring(1, 1) == "1"||tech=="CAF")
                                {
                                    if (layer == "RE" || tblFlow.Rows[i]["toFlag"].ToString() == "N" || layer == "TO")
                                    { tblFlow.Rows[i]["method"] = "LSA/LSA"; }
                                    else
                                    {
                                        if (narrowScribelaneFlag)   //narrow mark 用FIA
                                        {
                                            tblFlow.Rows[i]["method"] = "FIA/FIA";
                                        }
                                        else
                                        {
                                            tblFlow.Rows[i]["method"] = "LSA/FIA";
                                        }
                                    }
                                }
                                else
                                { tblFlow.Rows[i]["method"] = "LSA/LSA"; }
                            }

                        }
                    }
                    #endregion
                }
                else //LDI
                {

                    string[] myHoleLayers = { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "WT" };
                    string[] myMetalLayers = { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "AT", "CT", "OE", "OV" };

                    //  if (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") //narrow mark
                    if (narrowScribelaneFlag) //从坐标文件，判断narrow mark
                    {
                        if (tech.Substring(1, 1) == "1" && myHoleLayers.Contains(layer))
                        { tblFlow.Rows[i]["method"] = "AA157"; }
                        else if (tech.Substring(1, 1) == "1" && myMetalLayers.Contains(layer))
                        { tblFlow.Rows[i]["method"] = "AH325374"; }
                        else
                        { tblFlow.Rows[i]["method"] = "AH53"; }
                    }



                    else
                    { tblFlow.Rows[i]["method"] = "AH53"; }
                }

            }


        }

    

        private void ReadCoordinateFile()
        {
            string filePath = "P:\\Recipe\\Coordinate\\" + textBox1.Text.Trim().ToUpper() + ".txt";
            string line;
            string str1;
            if (System.IO.File.Exists(filePath) == false)
            {
                MessageBox.Show("坐标文件不存在,手动选择");
                OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
                file.InitialDirectory = "P:\\Recipe\\Coordinate\\";


                file.Filter = "文档(*.txt)|*.txt";
                if (file.ShowDialog() == DialogResult.OK) filePath = file.FileName;
                //   string fileNameWithoutExtension = System.IO.Path.GetDirectoryName(filepath);// 没有扩展名的文件名 “Default”

                if (file.FileName.Length == 0)//判断有没有文件导入  
                {
                    MessageBox.Show("未选择坐标文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }




            tblTmp = null; tblTmp = new DataTable();
            tblTmp.Columns.Add("mark");
            tblTmp.Columns.Add("layer");
            tblTmp.Columns.Add("lines");
            tblTmp.Columns.Add("X");
            tblTmp.Columns.Add("Y");


            StreamReader sr = new StreamReader(filePath);

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();

                if ((line.Contains("WY") || line.Contains("WX") || line.Contains("SPM") || line.Contains("NAA157") || line.Contains("NAH") || line.Contains("FIA") || line.Contains("LSA")) && (!line.Contains("DRC")))
                {
                    var x = line.Split(new char[] { '\t' });

               

                    DataRow newRow = tblTmp.NewRow();
                    str1 = x[0];
                    if (str1.Contains("X"))
                    {
                        str1 = str1.Replace("X", "");
                        str1 = str1 + "X";
                    }
                    else if (str1.Contains("Y"))
                    {
                        str1 = str1.Replace("Y", "");
                        str1 = str1 + "Y";
                    }

                    try
                    {
                        if (x.Length == 7)
                        {
                            newRow["mark"] = str1;
                            newRow["layer"] = x[1].Trim();
                            newRow["lines"] = x[2].Trim();
                            newRow["X"] = x[4].Trim();
                            newRow["Y"] = x[6].Trim();
                        }
                        else //DMOS TR60坐标特殊
                        {
                            newRow["mark"] = str1;
                            newRow["layer"] = x[1].Trim();
                            newRow["lines"] = x[2].Trim();
                            newRow["X"] = x[5].Trim();
                            newRow["Y"] = x[7].Trim();

                        }
                    }
                    catch
                    {
                        MessageBox.Show("请确认坐标数据格式是否正确");
                        //MessageBox.Show(line);    
                        //MessageBox.Show(strAry.Length.ToString());
                        newRow["mark"] = str1;
                        newRow["layer"] = x[1].Trim();
                        newRow["lines"] = x[2].Trim();

                        int markXY = 0;
                        for (int m=x.Length-1;m>0;m--)
                        {
                            if (x[m].Trim().Length>0)
                            {
                                if (markXY==0)
                                {
                                    newRow["Y"] = x[m].Trim();
                                    markXY += 1;
                                }
                                else if (markXY==1)
                                { 
                                    newRow["X"] = x[m].Trim();
                                    break;
                                }

                            }
                        }

                        





                    
                    }


                    tblTmp.Rows.Add(newRow);


                }
            }

            sr.Close(); sr = null;

            dataGridView1.DataSource = tblTmp;
        }



        private bool CheckNarrowMarkFromCoordinateFile(string filePath)
        {
           
            string line;
           // string str1;
           



             StreamReader sr = new StreamReader(filePath);

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine().Trim().ToUpper();

                if ((line.Contains("WY") || line.Contains("WX") || line.Contains("SPM") || line.Contains("NAA157") || line.Contains("NAH") || line.Contains("FIA") || line.Contains("LSA")) && (!line.Contains("DRC")))
                {
                     if (line.Substring(0,1)!="N")
                    {
                        return false;
                    }


                }
            }

            sr.Close(); sr = null;
            return true;

        }

        private void 复制EXE文件到ZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Copy(@"\\10.4.72.150\share\VC\VS2019\WindowsFormsExposureRecipe_NET3.5\WindowsFormsExposureRecipe\bin\Debug\System.Data.SQLite.dll", @"\\10.4.72.74\asml\_NET3.5\ExposureRecipe\System.Data.SQLite.dll", true);
            File.Copy(@"\\10.4.72.150\share\VC\VS2019\WindowsFormsExposureRecipe_NET3.5\WindowsFormsExposureRecipe\bin\Debug\WindowsFormsExposureRecipe.exe", @"\\10.4.72.74\asml\_NET3.5\ExposureRecipe\WindowsFormsExposureRecipe.exe", true);
            File.Copy(@"\\10.4.72.150\share\VC\VS2019\WindowsFormsExposureRecipe_NET3.5\WindowsFormsExposureRecipe\bin\Debug\WindowsFormsExposureRecipe.exe.config", @"\\10.4.72.74\asml\_NET3.5\ExposureRecipe\WindowsFormsExposureRecipe.exe.config", true);
            File.Copy(@"\\10.4.72.150\share\VC\VS2019\WindowsFormsExposureRecipe_NET3.5\WindowsFormsExposureRecipe\bin\Debug\WindowsFormsExposureRecipe.pdb", @"\\10.4.72.74\asml\_NET3.5\ExposureRecipe\WindowsFormsExposureRecipe.pdb", true);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ReadMark(string[] layers, string[] prefix,string[] markType)
        {
            string key;DataRow[] drs = null;
   
           
            int rowno = Convert.ToInt32(layers[0]);

     
                                                                    
            for (int i = 1; i < layers.Length; i++) //第一位是tblFlow的行数
            {
                foreach (string item in prefix)
                {
                    foreach (string markLine in markType)
                    {                      
                        key = "layer='" + layers[i] + "' and mark like '" + item + "%' and lines='" + markLine + "' ";
                                     

                        

                        drs = tblTmp.Select(key, "lines asc,mark asc");

                     //  if (layers[i] == "GT") {
                    //        MessageBox.Show(key + "\n" + drs.Length.ToString()); 
                     //  }

                        if (drs.Length > 0)
                        {
                            //MessageBox.Show(rowno.ToString() + "," + drs.Length.ToString());
                            // MessageBox.Show(drs[0]["mark"].ToString()+","+drs[0]["X"].ToString());
                            //MessageBox.Show(drs[0]["mark"].ToString() + "," + drs[0]["Y"].ToString());
                            // MessageBox.Show(drs[1]["mark"].ToString() + "," + drs[1]["X"].ToString());
                            // MessageBox.Show(drs[1]["mark"].ToString() + "," + drs[1]["Y"].ToString());
                            DataRow newRow = tblBt.NewRow();
                            newRow["stage"] = tblFlow.Rows[rowno]["stage"].ToString();
                            newRow["Mask"] = tblFlow.Rows[rowno]["Mask"].ToString();
                            newRow["MLM"] = tblFlow.Rows[rowno]["MLM"].ToString();
                            newRow["ppid"] = tblFlow.Rows[rowno]["ppid"].ToString();
                            newRow["eqptype"] = tblFlow.Rows[rowno]["eqptype"].ToString();
                            if (tblFlow.Rows[rowno]["eqptype"].ToString() == "LDI")
                            {
                                newRow["illuminations"] = tblFlow.Rows[rowno]["illumination"].ToString();
                            }
                            else
                            {
                                if (item == "WD" || item == "WC" || item == "W" || item == "NWD" || item == "NWC" || item == "NW")
                                {
                                    newRow["illuminations"] = "LsaS坐标";
                                }
                                else if ((item.Length > 1 && item.Substring(0, 2) == "WF") || (item.Length > 2 && item.Substring(0, 3) == "NWF"))
                                {
                                    newRow["illuminations"] = "FiaS坐标";
                                }
                                else if ((item.Length > 2 && item.Substring(0, 3) == "LSA") || (item.Length > 3 && item.Substring(0, 4) == "NLSA"))
                                {
                                    newRow["illuminations"] = "LsaE坐标";
                                }
                                else if ((item.Length > 2 && item.Substring(0, 3) == "FIA") || (item.Length > 3 && item.Substring(0, 4) == "NFIA"))
                                {
                                    newRow["illuminations"] = "FiaE坐标";
                                }

                            }
                            newRow["method"] = tblFlow.Rows[rowno]["method"].ToString();
                            newRow["alignto"] = tblFlow.Rows[rowno]["alignto"].ToString();
                            ///针对stack mark此处应该改为坐标文件的实际标记前缀，
                            newRow["alignto"] = layers[i];
                          




                            if (item.Substring(0, 1) == "W" || item.Substring(0, 2) == "NW")//Search Y Mark在前
                            {
                                newRow["typex"] = drs[drs.Length / 2]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x1"] = drs[drs.Length / 2]["X"].ToString();
                                newRow["y1"] = drs[drs.Length / 2]["Y"].ToString();
                                newRow["typey"] = drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x2"] = drs[0]["X"].ToString();
                                newRow["y2"] = drs[0]["Y"].ToString();

                            }
                            else
                            {
                                newRow["typex"] = drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x1"] = drs[0]["X"].ToString();
                                newRow["y1"] = drs[0]["Y"].ToString();
                                newRow["typey"] = drs[drs.Length / 2]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x2"] = drs[drs.Length / 2]["X"].ToString();
                                newRow["y2"] = drs[drs.Length / 2]["Y"].ToString();
                                

                            }
                            tblBt.Rows.Add(newRow);

                            ///部分坐标文件的X,Y的前缀一样，有误，以下进行判断
                            ///
                          //  MessageBox.Show(newRow["typex"].ToString() + ",  " + newRow["typey"].ToString());
                            if ( newRow["typex"].ToString().Length>1 && newRow["typeY"].ToString().Length > 1)
                            {
                                if (
                                       (newRow["typeY"].ToString().Split(new char[] { '#' })[0].EndsWith("X") && newRow["typeX"].ToString().Split(new char[] { '#' })[0].EndsWith("Y"))
                                    || (newRow["typeY"].ToString().Split(new char[] { '#' })[0].EndsWith("Y") && newRow["typeX"].ToString().Split(new char[] { '#' })[0].EndsWith("X"))
                                    )
                                { }
                                else
                                {
                                    MessageBox.Show("坐标前缀异常" +
                                     "\n\n    " + newRow["ppid"]+","+ newRow["alignto"]+","+  newRow["typeX"].ToString()+
                                      "\n\n    " + newRow["ppid"]+","+ newRow["alignto"] + "," + newRow["typeY"].ToString() + 
                                       "\n\n\n\n不是分别以X/Y结尾，请确认");
                                }
                            }
                                 
           



                            return;


                        }
                        else
                        {
                            

                        }


                    }

                }
               // StringBuilder tmp = new StringBuilder();
               // tmp.Append(tblFlow.Rows[rowno]["ppid"].ToString());
               // tmp.Append(":所有标记组合\r\n\r\n");
               // foreach (var x in prefix) { tmp.Append(x); tmp.Append(","); }
              //  tmp.Append("\r\n\r\n未找到对应坐标");
              //  MessageBox.Show(tmp.ToString());


              




            }
            DataRow newRow1 = tblBt.NewRow();
            newRow1["stage"] = tblFlow.Rows[rowno]["stage"].ToString();
            newRow1["Mask"] = tblFlow.Rows[rowno]["Mask"].ToString();
            newRow1["MLM"] = tblFlow.Rows[rowno]["MLM"].ToString();
            newRow1["ppid"] = tblFlow.Rows[rowno]["ppid"].ToString();
            newRow1["eqptype"] = tblFlow.Rows[rowno]["eqptype"].ToString();
            newRow1["method"] = tblFlow.Rows[rowno]["method"].ToString();

            newRow1["typex"] = "#";//drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
            newRow1["x1"] = "#";//drs[0]["X"].ToString();
            newRow1["y1"] = "#";//drs[0]["Y"].ToString();
            newRow1["typey"] = "#";// drs[1]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
            newRow1["x2"] = "#";//drs[1]["X"].ToString();
            newRow1["y2"] = "#";//drs[1]["Y"].ToString();
            tblBt.Rows.Add(newRow1);


       



        }
        private void MatchCoordinate()
        {

            //2867
            string part = textBox1.Text.Trim().ToUpper();
            string tech, ppid, alignto, method, tool;

            tblBt = null; tblBt = new DataTable();
            tblBt.Columns.Add("stage");
            tblBt.Columns.Add("Mask");
            tblBt.Columns.Add("MLM");
            tblBt.Columns.Add("ppid");
            tblBt.Columns.Add("alignto");

            tblBt.Columns.Add("eqptype");
            tblBt.Columns.Add("illuminations");
            tblBt.Columns.Add("method");


            tblBt.Columns.Add("typex");
            tblBt.Columns.Add("x1");
            tblBt.Columns.Add("y1");
            tblBt.Columns.Add("typey");
            tblBt.Columns.Add("x2");
            tblBt.Columns.Add("y2");

           

            

            //将第一层数据，加入到坐标表格中
            DataRow newRow = tblBt.NewRow();
            newRow["stage"] = tblFlow.Rows[0]["stage"].ToString();
            newRow["Mask"] = tblFlow.Rows[0]["Mask"].ToString();
            newRow["MLM"] = tblFlow.Rows[0]["MLM"].ToString();
            newRow["ppid"] = tblFlow.Rows[0]["ppid"].ToString();
            newRow["eqptype"] = tblFlow.Rows[0]["eqptype"].ToString();
            if (tblFlow.Rows[0]["eqptype"].ToString() == "LDI") { newRow["illuminations"] = tblFlow.Rows[0]["illumination"].ToString(); }
            newRow["alignto"] = "NA"; newRow["typex"] = "NA"; newRow["method"] = "NA";
            newRow["x1"] = 88888;
            newRow["y1"] = 88888;
            newRow["typey"] = "NA";
            newRow["x2"] = 88888;
            newRow["y2"] = 88888;
            tblBt.Rows.Add(newRow);

            dataGridView1.DataSource = tblFlow;
      

            for (int i = 1; i < tblFlow.Rows.Count; i++)
            {
               
                tech = tblFlow.Rows[i]["code"].ToString();

                tool = tblFlow.Rows[i]["eqptype"].ToString();

                ppid = tblFlow.Rows[i]["ppid"].ToString().Substring(0, 2);

                alignto = tblFlow.Rows[i]["alignto"].ToString().Substring(0, 2);

                method = tblFlow.Rows[i]["method"].ToString();

                string[] markAll = { "SPM53", "AH325374", "AA157", "NAH53", "NAH325374", "NAA157" };
                string[] markAdvancedHole = { "AA157", "SPM53", "AH325374", "NAA157", "NAH53", "NAH325374" };
                string[] markAdvancedMetal = { "AH325374", "SPM53", "AA157", "NAH325374", "NAH53", "NAA157" };

                string[] markLsaSearch = { "WC", "W", "WD", "NWC", "NW", "NWD" };
                string[] markFiaSearch = { "WFC", "WF", "WFD", "NWFC", "NWF", "NWFD" };
                string[] markLsaEga = { "LSAC", "LSA", "LSAD", "NLSAC", "NLSA", "NLSAD" };
                string[] markFiaEga = { "FIAC", "FIA", "FIAD", "NFIAC", "NFIA", "NFIAD" };

                string[] markLsaSearchD = { "WD", "W", "WC", "NWD", "NW", "NWC" };
                string[] markFiaSearchD = { "WFD", "WF", "WFC", "NWFD", "NWF", "NWFC" };
                string[] markLsaEgaD = { "LSAD", "LSA", "LSAC", "NLSAD", "NLSA", "NLSAC" };
                string[] markFiaEgaD = { "FIAD", "FIA", "FIAC", "NFIAD", "NFIA", "NFIAC" };

                string[] markType = { "" };


     



                if (tool == "LDI") //asml
                {
                    #region
                    if (tech.Substring(0, 1) == "T") //DMOS
                    {
                        #region DMOS
                     //   if (part.Substring(7, 1) == "N" || part.Substring(7, 1) == "M") //split gate
                     //   {
                     //       string[] strQuery = { i.ToString(), alignto };
                      //      ReadMark(strQuery, markAll, markType);
                      //  }
                      //  else //非 split gate
                      //  {
                            if (alignto == "TR")
                            {
                                string[] strQuery = { i.ToString(), "TR60", "TR" };
                                ReadMark(strQuery, markAll, markType);
                            }
                            else
                            {
                                string[] strQuery = { i.ToString(), alignto };
                                ReadMark(strQuery, markAll, markType);
                            }


                        //}
                        #endregion
                    }
                    else
                    {
                        string[] holes = { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "WT" };
                        string[] metals = { "A1", "A2", "A3", "A4", "A5", "A6", "AT", "CT", "OE", "OV" };
                        string[] stack1 = { "TO", "W2", "W4", "W6" };
                        string[] stack2 = { "W1", "W3", "W5", "W7" };
                        if (tech.Substring(1, 1) != "1") //"非先进工艺"
                        {
                            #region lowend
                            // MessageBox.Show(tech + "," + tool + "," + ppid + "," + alignto + "," + method + "," + tech.Substring(1, 1));

                            if (stack1.Contains(alignto))
                            {
                                //alignto + TOW246
                                string[] strQuery = { i.ToString(), "TOW246", alignto };
                                //MessageBox.Show(tech + "," + tool + "," + ppid + "," + alignto + "," + method + "," + tech.Substring(1, 1));

                                ReadMark(strQuery, markAll, markType);

                            }
                            else if (stack2.Contains(alignto))
                            {
                                //alignto + W1357
                                string[] strQuery = { i.ToString(), "W1357", alignto };
                                ReadMark(strQuery, markAll, markType);
                            }
                            else
                            {
                                //alignto
                                string[] strQuery = { i.ToString(), alignto };
                                ReadMark(strQuery, markAll, markType);
                            }
                            #endregion
                        }
                        else //先进工艺
                        {
                            #region highend

                         

                           // if (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") //narrow mark
                            if (narrowScribelaneFlag)

                            {
                                #region narrow mark

                                if (holes.Contains(ppid)) // 孔用157
                                {
                                    #region hole
                                    if (stack1.Contains(alignto))
                                    {
                                        //alignto + TOW246
                                        string[] strQuery = { i.ToString(), "TOW246", alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);

                                    }
                                    else if (stack2.Contains(alignto))
                                    {
                                        //alignto + W1357
                                        string[] strQuery = { i.ToString(), "W1357", alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);
                                    }
                                    else
                                    {
                                        //W1-->GT
                                        string[] strQuery = { i.ToString(), alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);
                                    }
                                    #endregion
                                }
                                else if (metals.Contains(ppid)) //铝用325374
                                {
                                    #region metal
                                    if (stack1.Contains(alignto))
                                    {
                                        //alignto + TOW246
                                        string[] strQuery = { i.ToString(), "TOW246", alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);
                                    }
                                    else if (stack2.Contains(alignto))
                                    {
                                        //alignto + W1357
                                        string[] strQuery = { i.ToString(), "W1357", alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);
                                    }
                                    else
                                    {
                                        //AT-->WT
                                        string[] strQuery = { i.ToString(), alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);

                                    }
                                    #endregion
                                }
                                else  //非孔，铝用53
                                {
                                    if (alignto.Contains("TO"))
                                    { string[] strQuery = { i.ToString(), "TOW246", alignto }; ReadMark(strQuery, markAll, markType); }
                                    else
                                    { string[] strQuery = { i.ToString(), alignto }; ReadMark(strQuery, markAll, markType); };

                                }
                                #endregion
                            }
                            else // 非narrow mark
                            {
                                #region
                                if (stack1.Contains(alignto))
                                {
                                    //alignto + TOW246
                                    string[] strQuery = { i.ToString(), "TOW246", alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                else if (stack2.Contains(alignto))
                                {
                                    //alignto + W1357
                                    string[] strQuery = { i.ToString(), "W1357", alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                else
                                {
                                    //alignto
                                    string[] strQuery = { i.ToString(), alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                #endregion
                            }
                            #endregion
                        }

                    }
                    #endregion

                }
                else //Nikon
                {
                    #region
                    //  if ((ppid == "CP" || ppid == "PN" || ppid == "PI" || ppid == "P2") && tech.Substring(0, 1) != "T") //DMOS PAD
                    //   if (  tech.Substring(1, 1) == "1" &&   (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") &&        (alignto=="TO")          )

                   
                    
                    if (narrowScribelaneFlag == true && fullTech.Substring(1,1)=="1")// || alignto == "TO")
                    {
                        if (ppid == "CP" || ppid == "PI" || ppid == "PN" || ppid == "CF" || ppid == "FU")
                        {
                            //   string[] strQuery = { i.ToString(), alignto, "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            //string[] strQuery = { i.ToString(), alignto };// "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            string[] strQuery = { i.ToString(), alignto, "T" + alignto.Substring(1, 1), "A" + alignto.Substring(1, 1) };


                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }

                        else if (alignto=="TO")
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearchD, markType);
                            ReadMark(strQuery, markFiaSearchD, markType);
                            ReadMark(strQuery, markLsaEgaD, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEgaD, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        /*
                        else if (ppid=="RE"||ppid=="CT"||ppid=="TT"|| ppid == "AT"||ppid=="EP"||ppid=="SN"||ppid=="SP"||ppid=="NP"||ppid=="PP")  //其它层次待加入
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });

                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearchD, markType);
                            ReadMark(strQuery, markFiaSearchD, markType);
                            ReadMark(strQuery, markLsaEgaD, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEgaD, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        */
                    }
                    else
                    {
                        if (ppid == "CP" || ppid == "PI" || ppid == "PN" || ppid == "CF" || ppid == "FU")
                        {
                            //   string[] strQuery = { i.ToString(), alignto, "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            //string[] strQuery = { i.ToString(), alignto };// "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            string[] strQuery = { i.ToString(), alignto, "T" + alignto.Substring(1, 1), "A" + alignto.Substring(1, 1) };

                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                    }
                    #endregion

                }

            }
        }
        //计算offset
        private void CallCalculateOffset()
        {
            double wee = Convert.ToDouble(textBox8.Text);
            double xImg = Convert.ToDouble(textBox9.Text);
            double stepX = Convert.ToDouble(textBox2.Text) / 1000;
            double stepY = Convert.ToDouble(textBox3.Text) / 1000;
            double dieX = Convert.ToDouble(textBox6.Text) / 1000;
            double dieY = Convert.ToDouble(textBox7.Text) / 1000;
            double pricision = 0.1;


            if (radioButton1.Checked)
            {
                //  MessageBox.Show("Normal");
                //    MessageBox.Show(wee.ToString() + "," + xImg.ToString() + "," + stepX.ToString() + "," + stepY.ToString() + "," + dieX.ToString() + "," + dieY.ToString() + "," + pricision.ToString());
                parttype = "normalField";
                tblTmp = Map.DieQty.CalculatOffset(wee, xImg, stepX, stepY, dieX, dieY, pricision, parttype);

            }
            else if (radioButton2.Checked)
            {
                // MessageBox.Show("Large");
                parttype = "largeField";
                tblTmp = Map.DieQty.CalculatOffset(wee, xImg, stepX, stepY, dieX, dieY, pricision, parttype);
            }
            else if (radioButton3.Checked)
            {
                //  MessageBox.Show("MPW");
                parttype = "mpw";
                tblTmp = Map.DieQty.CalculatOffset(wee, xImg, stepX, stepY, dieX, dieY, pricision, parttype);

            }
            else if (radioButton4.Checked)
            {
                //  MessageBox.Show("SPLIT GATE");
                parttype = "spliteGate";
                tblTmp = Map.DieQty.CalculatOffset(wee, xImg, stepX, stepY, dieX, dieY, pricision, parttype);
            }


            tblTmp.DefaultView.Sort = "totalShot asc,dieQty desc,delta ASC";
            //DataRow[] drs = dt.Select("shiftY <> 888 and leftFlag=true and rightFlag=true");

            dataGridView1.DataSource = tblTmp;
        }
        //画图
        private void PlotAsml(bool nikonFlag)  //预置参数，调用 DieQty类画图
        {

            part = textBox1.Text.Trim().ToUpper();
            string filePath;
            string savePart;
            if (nikonFlag == true)
            {
                filePath = savePath + "_Nikon";
                savePart = part + "_Nikon";
            }
            else
            {
                filePath = savePath;
                savePart = part;
            }

            filePath += ".emf";
            double areaRatio = Convert.ToDouble(textBox10.Text); //partial shot 小于此阈值不画
            //手动作图，定义parttype
            if (radioButton1.Checked)
            {
                parttype = "normalField";


                if (part.Substring(0, 2) == "XU" ||
                    part.Substring(0, 2) == "XV" ||
                    part.Substring(0, 2) == "UF" ||
                    part.Substring(0, 2) == "D6" ||
                    part.Substring(0, 2) == "O1" ||
                    part.Substring(0, 7) == "M" ||
                    part.Substring(0, 7) == "N")
                {
                    fullCover = true;
                }
                else
                {
                    fullCover = false;
                }

            }
            else if (radioButton2.Checked)
            {
                if (nikonFlag == false)
                { parttype = "largeField"; fullCover = true; }  //大视场，ASML MAP
                else
                { parttype = "spliteGate"; fullCover = true; }  //大视场,Nikon Map

            }
            else if (radioButton3.Checked)
            { parttype = "mpw"; fullCover = false; }
            else if (radioButton4.Checked)
            { parttype = "spliteGate"; fullCover = true; }
            pictureBox1.Image = null;

            float k, sx, sy, dx, dy, ox, oy;
            try
            {
                Bitmap bmp = new Bitmap(1169, 827); //实际三楼打印机的设置
                Graphics gs = Graphics.FromImage(bmp);
                System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                Graphics wfrmap = Graphics.FromImage(mf);


                k = 20f;//如果不放大，picturebox1图片失真；过大，windows画板无法看；实际图片较大，打印时进行缩放
                sx = ((float)System.Math.Round(Convert.ToDouble(textBox2.Text), 1)) / 1000;
                sy = ((float)System.Math.Round(Convert.ToDouble(textBox3.Text), 1)) / 1000;
                dx = ((float)System.Math.Round(Convert.ToDouble(textBox6.Text), 1)) / 1000;
                dy = ((float)System.Math.Round(Convert.ToDouble(textBox7.Text), 1)) / 1000;
             

                //以上方法，损失精度，微米为单位，以下小数点后仍旧保留两位，毫米为单位，保留5位 
                //以上实际可以将小数点保留两位，或直接取消 Round命令
                //使用float是因为作图参数不接受double，原因待澄清
                sx = ((float)System.Math.Round(Convert.ToDouble(textBox2.Text) / 1000, 5));
                sy = ((float)System.Math.Round(Convert.ToDouble(textBox3.Text) / 1000, 5));
                dx = ((float)System.Math.Round(Convert.ToDouble(textBox6.Text) / 1000, 5));
                dy = ((float)System.Math.Round(Convert.ToDouble(textBox7.Text) / 1000, 5));






                if (radioButton5.Checked) //自动作图
                {
                    if (nikonFlag == false) //非大视场Nikon的offset
                    {
                        ox = ((float)Convert.ToDouble(tblTmp.Rows[0]["shiftX"].ToString()));
                        oy = ((float)Convert.ToDouble(tblTmp.Rows[0]["shiftY"].ToString()));
                        if (radioButton2.Checked)

                        {
                            float x;
                            x = sx;
                            sx = sy;
                            sy = 2 * x;

                            if (ox > sx / 2)
                            { ox = sx - ox; }
                            else if (ox < -sx / 2)
                            { ox = sx + ox; }

                            if (oy > sy / 2)
                            { oy = sy - oy; }
                            else if (oy < -sy / 2)
                            { oy = sy + oy; }

                            x = sx;
                            sx = sy / 2;
                            sy = x;

                        }
                        else
                        {



                            if (ox > sx / 2)
                            { ox = sx - ox; }
                            else if (ox < -sx / 2)
                            { ox = sx + ox; }

                            if (oy > sy / 2)
                            { oy = sy - oy; }
                            else if (oy < -sy / 2)
                            { oy = sy + oy; }
                        }

                        // MessageBox.Show(ox.ToString() + "," + oy.ToString());
                        // MessageBox.Show(sx.ToString() + "," + sy.ToString());



                    }
                    else //大视场Nikon offset
                    {
                        //   MessageBox.Show(tblTmp.Rows[0]["shiftX"].ToString()+",sssss");
                        //    MessageBox.Show(tblTmp.Rows[0]["shiftY"].ToString());

                        //  tblTmp.Rows[0]["shiftX"] = -7.231;
                        //   tblTmp.Rows[0]["shiftY"] = 11.492;

                        //   MessageBox.Show(tblTmp.Rows[0]["shiftX"].ToString());
                        //   MessageBox.Show(tblTmp.Rows[0]["shiftY"].ToString());



                        oy = ((float)Convert.ToDouble(tblTmp.Rows[0]["shiftX"].ToString()));
                        float tmp = ((float)Convert.ToDouble(tblTmp.Rows[0]["shiftY"].ToString()));
                        tmp += sx / 2;
                        if (tmp >= -sx / 2 && tmp <= sx / 2)
                        { ox = -tmp; }
                        else
                        {
                            if (tmp < 0) { ox = sx + tmp; }
                            else { ox = sx - tmp; }
                        }



                    }
                }
                else
                {
                    ox = ((float)Convert.ToDouble(textBox4.Text)) / 1000;
                    oy = ((float)Convert.ToDouble(textBox5.Text)) / 1000;
                }
                //应该取消round 函数
                ox = (float)System.Math.Round(Convert.ToDouble(ox), 5);
                oy = (float)System.Math.Round(Convert.ToDouble(oy), 5);





               




                Map.DieQty.Plot(wfrmap, k, sx, sy, dx, dy, ox, oy, areaRatio, fullCover, parttype, nikonFlag, savePath, part);
                // public static void Plot(Graphics wfrmap, float k, float sx, float sy, float dx, float dy, float ox, float oy,double areaRatio)
                wfrmap.Save();
                wfrmap.Dispose();
                mf.Dispose();

                // return true;
            }
            catch
            {
               MessageBox.Show("作图失败，请检查输入参数是否正常；或程序是否异常");
              return;
            }
     


            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
                printPicPath = filePath;
                this.tabControl1.SelectedTab = this.tabPage2;

                //保存map参数

                if (MessageBox.Show("请确认是Part名等参数是否和Map数据匹配!!\n\n" +
                    "   同名Part旧数据会被覆盖!!\n\n" +
                    "   另手动作图数据不保存!!\n\n",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (Interaction.InputBox("请输入\"123\"以保存数据", "确认保存", "", -1, -1).Trim() == "123")
                    {
                        if (radioButton5.Checked)//自动作图数据保存
                        {
                            using (SQLiteConnection conn = new SQLiteConnection(connStr))
                            {
                                conn.Open();
                                sql = " DELETE  FROM TBL_MAP WHERE PART='" + savePart + "'";
                                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                                {
                                    com.ExecuteNonQuery();
                                }
                            }
                            //添加
                            using (SQLiteConnection conn = new SQLiteConnection(connStr))
                            {
                                conn.Open();
                                sql = " INSERT  INTO TBL_MAP (k,StepX,StepY,DieX,DieY,OffsetX,OffsetY,AreaRatio,FullCover,PartType,NikonFlag,Part,Riqi) " +
                                    "VALUES (" +
                                    k + "," + sx + "," + sy + "," + dx + "," + dy + "," + ox + "," + oy + "," + areaRatio + "," + fullCover + ",'" + parttype + "'," + nikonFlag + ",'" + savePart + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                   + ")";

                                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                                {
                                    com.ExecuteNonQuery();
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show(" 手动输入Map Offset作图，相关数据不保存 ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("未正确输入\"123\"，作图数据未保存");
                    }
                }
                else
                {
                    MessageBox.Show("已选择不保存数据");
                }

            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
          





        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //设置printDocument控件的PrintPage事件：
            //https://www.cnblogs.com/hfzsjz/archive/2010/08/26/1809241.html
            e.Graphics.DrawImage(pictureBox1.Image, 20, 20);
        }

        public static DataTable OpenCSV(string filePath)
        {
           // Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
          

            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
               // dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
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

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // testPrint.PrintDGV xxx = new testPrint.PrintDGV();
            //testPrint.PrintDGV.Print_DataGridView(dataGridView1,true);
            //string[] mylayers = { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "WT" };
            // string layer = "W1";
            // MessageBox.Show(mylayers.Contains(layer).ToString());

            // tblTmp = Map.DieQty.NormalField(97, 0, 25, 32, 0.5, 0.8, 0.1);
            //dataGridView1.DataSource = tblTmp;
            MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            MessageBox.Show(pictureBox1.Image.Width.ToString() + "," + pictureBox1.Image.Height.ToString());



        }





      











    }

    #region other class
    public class ToPrint
    {

        //以下用户可自定义
        //当前要打印文本的字体及字号
        private static Font TableFont = new Font("Verdana", 10, FontStyle.Regular);
        //表头字体
        private Font HeadFont = new Font("Verdana", 15, FontStyle.Bold);
        //表头文字
        private string HeadText = string.Empty;
        //表头高度
        private int HeadHeight = 20;
        //表的基本单位
        private int[] XUnit;
        private int YUnit = Convert.ToInt32(TableFont.Height * 1.5);
        //以下为模块内部使用
        private PrintDocument DataTablePrinter;
        private DataRow DataGridRow;
        private DataTable DataTablePrint;
        //当前要所要打印的记录行数,由计算得到
        private int PageRecordNumber;
        //正要打印的页号
        private int PrintingPageNumber = 1;
        //已经打印完的记录数
        private int PrintRecordComplete;
        private int PLeft;
        private int PTop;
        private int PRight;
        private int PBottom;
        private int PWidth;
        private int PHeigh;
        //当前画笔颜色
        private SolidBrush DrawBrush = new SolidBrush(Color.Black);
        //每页打印的记录条数
        private int PrintRecordNumber;
        //第一页打印的记录条数
        private int FirstPrintRecordNumber;
        //总共应该打印的页数
        private int TotalPage;
        //与列名无关的统计数据行的类目数（如，总计，小计......）
        public int TotalNum = 0;

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="Title">打印文件的标题</param>
        public void Print(DataTable dt, string Title)
        {
            try
            {
                CreatePrintDocument(dt, Title).Print();
            }
            catch (Exception)
            {
                MessageBox.Show("打印错误，请检查打印设置！");

            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="Title">打印文件的标题</param>
        public void PrintPriview(DataTable dt, string Title)
        {
            try
            {
                PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                PrintPriview.Document = CreatePrintDocument(dt, Title);
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("打印错误，请检查打印设置！");

            }
        }

        /// <summary>
        /// 创建打印文件
        /// </summary>
        private PrintDocument CreatePrintDocument(DataTable dt, string Title)
        {

            DataTablePrint = dt;
            HeadText = Title;
            DataTablePrinter = new PrintDocument();

            PageSetupDialog PageSetup = new PageSetupDialog();
            PageSetup.Document = DataTablePrinter;
            DataTablePrinter.DefaultPageSettings = PageSetup.PageSettings;
            DataTablePrinter.DefaultPageSettings.Landscape = true;//设置打印横向还是纵向
            PLeft = 30; //PLeft=DataTablePrinter.DefaultPageSettings.Margins.Left;
            PTop = 30;// PTop = DataTablePrinter.DefaultPageSettings.Margins.Top;
            PRight = 30;// 
            PRight = DataTablePrinter.DefaultPageSettings.Margins.Right;
            PBottom = 30;// PBottom = DataTablePrinter.DefaultPageSettings.Margins.Bottom;
            PWidth = DataTablePrinter.DefaultPageSettings.Bounds.Width;
            PHeigh = DataTablePrinter.DefaultPageSettings.Bounds.Height;
            XUnit = new int[DataTablePrint.Columns.Count];
            PrintRecordNumber = Convert.ToInt32((PHeigh - PTop - PBottom - YUnit) / YUnit);
            FirstPrintRecordNumber = Convert.ToInt32((PHeigh - PTop - PBottom - HeadHeight - YUnit) / YUnit);

            if (DataTablePrint.Rows.Count > PrintRecordNumber)
            {
                if ((DataTablePrint.Rows.Count - FirstPrintRecordNumber) % PrintRecordNumber == 0)
                {
                    TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 1;
                }
                else
                {
                    TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 2;
                }
            }
            else
            {
                TotalPage = 1;
            }

            DataTablePrinter.PrintPage += new PrintPageEventHandler(DataTablePrinter_PrintPage);
            DataTablePrinter.DocumentName = HeadText;

            return DataTablePrinter;
        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        private void DataTablePrinter_PrintPage(object sende, PrintPageEventArgs Ev)
        {


            int tableWith = 0;
            string ColumnText;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //打印表格线格式
            Pen Pen = new Pen(Brushes.Black, 1);

            #region 设置列宽

            foreach (DataRow dr in DataTablePrint.Rows)
            {
                for (int i = 0; i < DataTablePrint.Columns.Count; i++)
                {
                    int colwidth = Convert.ToInt32(Ev.Graphics.MeasureString(dr[i].ToString().Trim(), TableFont).Width);
                    if (colwidth > XUnit[i])
                    {
                        XUnit[i] = colwidth;
                    }
                }
            }

            if (PrintingPageNumber == 1)
            {
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();
                    int colwidth = Convert.ToInt32(Ev.Graphics.MeasureString(ColumnText, TableFont).Width);
                    if (colwidth > XUnit[Cols])
                    {
                        XUnit[Cols] = colwidth;
                    }
                }
            }
            for (int i = 0; i < XUnit.Length; i++)
            {
                tableWith += XUnit[i];
            }
            #endregion

            PLeft = (Ev.PageBounds.Width - tableWith) / 2;
            int x = PLeft;
            int y = PTop;
            int stringY = PTop + (YUnit - TableFont.Height) / 2;
            int rowOfTop = PTop;

            //第一页
            if (PrintingPageNumber == 1)
            {
                //打印表头
                Ev.Graphics.DrawString(HeadText, HeadFont, DrawBrush, new Point(Ev.PageBounds.Width / 2, PTop), sf);


                //设置为第一页时行数
                PageRecordNumber = FirstPrintRecordNumber;
                rowOfTop = y = PTop + HeadFont.Height + 10;
                stringY = PTop + HeadFont.Height + 10 + (YUnit - TableFont.Height) / 2;
            }
            else
            {
                //计算,余下的记录条数是否还可以在一页打印,不满一页时为假
                if (DataTablePrint.Rows.Count - PrintRecordComplete >= PrintRecordNumber)
                {
                    PageRecordNumber = PrintRecordNumber;
                }
                else
                {
                    PageRecordNumber = DataTablePrint.Rows.Count - PrintRecordComplete;
                }
            }

            #region 列名
            if (PrintingPageNumber == 1 || PageRecordNumber > TotalNum)//最后一页只打印统计行时不打印列名
            {
                //得到datatable的所有列名
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();

                    int colwidth = Convert.ToInt32(Ev.Graphics.MeasureString(ColumnText, TableFont).Width);
                    Ev.Graphics.DrawString(ColumnText, TableFont, DrawBrush, x, stringY);
                    x += XUnit[Cols];
                }
            }
            #endregion



            Ev.Graphics.DrawLine(Pen, PLeft, rowOfTop, x, rowOfTop);
            stringY += YUnit;
            y += YUnit;
            Ev.Graphics.DrawLine(Pen, PLeft, y, x, y);

            //当前页面已经打印的记录行数
            int PrintingLine = 0;
            while (PrintingLine < PageRecordNumber)
            {
                x = PLeft;
                //确定要当前要打印的记录的行号
                DataGridRow = DataTablePrint.Rows[PrintRecordComplete];
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    Ev.Graphics.DrawString(DataGridRow[Cols].ToString().Trim(), TableFont, DrawBrush, x, stringY);
                    x += XUnit[Cols];
                }
                stringY += YUnit;
                y += YUnit;
                Ev.Graphics.DrawLine(Pen, PLeft, y, x, y);

                PrintingLine += 1;
                PrintRecordComplete += 1;
                if (PrintRecordComplete >= DataTablePrint.Rows.Count)
                {
                    Ev.HasMorePages = false;
                    PrintRecordComplete = 0;
                }
            }

            Ev.Graphics.DrawLine(Pen, PLeft, rowOfTop, PLeft, y);
            x = PLeft;
            for (int Cols = 0; Cols < DataTablePrint.Columns.Count; Cols++)
            {
                x += XUnit[Cols];
                Ev.Graphics.DrawLine(Pen, x, rowOfTop, x, y);
            }



            PrintingPageNumber += 1;

            if (PrintingPageNumber > TotalPage)
            {
                Ev.HasMorePages = false;
                PrintingPageNumber = 1;
                PrintRecordComplete = 0;
            }
            else
            {
                Ev.HasMorePages = true;
            }


        }

    }

    public class DataTableToSQLte   //https://blog.csdn.net/qq_42678477/article/details/81660682
    {
        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private string insertHead;
        public string InsertHead
        {
            get { return insertHead; }
        }
        private string[] separators;
        public string[] Separators
        {
            get { return separators; }
            set { separators = value; }
        }
        private string insertCmdText;
        private int colCount;
        private string[] fields;

        public DataTableToSQLte(DataTable dt, string dbTblName)
        {
            List<string> myFields = new List<string>();
            List<string> mySeparators = new List<string>();
            List<string> valueVars = new List<string>();// insert command text            
            colCount = dt.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                string colName = dt.Columns[i].ColumnName;
                myFields.Add(colName);
                mySeparators.Add(GetSeperator(dt.Columns[i].DataType.ToString()));
                valueVars.Add("@" + colName);

            }
            // insertHead = string.Format("insert into {0} ({1})"                
            //    , dt.TableName                
            //   , string.Join(",", myFields.ToArray()));

            insertHead = string.Format("insert into {0} ({1})"
                , dbTblName
                , string.Join(",", myFields.ToArray()));

            separators = mySeparators.ToArray();
            insertCmdText = string.Format("{0} values ({1})", insertHead
                , string.Join(",", valueVars.ToArray()));

            fields = myFields.ToArray();
        }
        private string GetSeperator(string typeName)
        {
            string result = string.Empty;
            switch (typeName)
            {
                case "System.String":
                    result = "'";
                    break;

                default:
                    result = typeName;
                    break;
            }
            return result;
        }
        public string GenInsertSql(DataRow dr)
        {
            List<string> strs = new List<string>();
            for (int i = 0; i < colCount; i++)
            {
                if (DBNull.Value == dr[i])  //null or DBNull                    
                    strs.Add("null");
                else
                    strs.Add(string.Format("{0}{1}{0}", separators[i], dr[i].ToString()));
            }
            return string.Format("{0} values ({1})", insertHead, string.Join(",", strs.ToArray()));


        }
        public void ImportToSqliteBatch(DataTable dt, string dbFullName)
        {
            string strConn = string.Format("data source={0}", dbFullName);

            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                using (SQLiteCommand insertCmd = conn.CreateCommand())
                {
                    insertCmd.CommandText = insertCmdText;
                    conn.Open();
                    SQLiteTransaction tranction = conn.BeginTransaction();
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            object o = null;
                            string paraName = "@" + fields[i];
                            if (DBNull.Value != dr[fields[i]])
                                o = dr[fields[i]];
                            insertCmd.Parameters.AddWithValue(paraName, o);

                        }

                        insertCmd.ExecuteNonQuery();

                    }
                    tranction.Commit();
                }
            }
        }

    }

    //打印多页时 要根据剩余页数判断并设置 HasMorePages 是否继续执行 PrintPage

    //打印预览中点击打印按钮，实际上会再次执行 PrintPage 事件，所以再打印完文档后 应该对相关属性进行重置

    //if (PrintingPageNumber > TotalPage)
    // {
    //   Ev.HasMorePages = false;
    //   PrintingPageNumber = 1;
    //   PrintRecordComplete = 0;
    // }
    //else
    // {
    //   Ev.HasMorePages = true;
    // }

    /*

   参考代码如下（或者说是复制的原本 ^_^）

   版本一（VB）

   来自 ：http://bbs.csdn.net/topics/340132124 printdocument控件如何绘制表格？

   Imports System.Drawing
   Imports System.Drawing.Pen
   Imports System.Drawing.Font
   Imports System.Drawing.PointF
   Imports System.Drawing.Color
   Imports System.Drawing.Printing
   Imports System.Windows.Forms
   Imports System.Windows.Forms.DataGrid

   Public Class PrintDataTable
       '以下用户可自定义
       '当前要打印文本的字体及字号
       Private TableFont As New Font("宋体", 10)
       '表头字体
       Private HeadFont As New Font("宋体", 20, FontStyle.Bold)
       '副表头字体
       Private SubHeadFont As New Font("宋体", 10, FontStyle.Regular)
       '表头文字
       Private HeadText As String
       '副表头左文字
       Private SubHeadLeftText As String
       '副表头右文字
       Private SubHeadRightText As String
       '表头高度
       Private HeadHeight As Integer = 40
       '副表头高度
       Private SubHeadHeight As Integer = 20
       '表脚字体
       Private FootFont As New Font("宋体", 10, FontStyle.Regular)
       '副表脚字体
       Private SubFootFont As New Font("宋体", 10, FontStyle.Regular)
       '表脚文字
       Private FootText As String
       '副表脚左文字
       Private SubFootLeftText As String
       '副表脚右文字
       Private SubFootRightText As String
       '表脚高度
       Private FootHeight As Integer = 30
       '副表脚高度
       Private SubFootHeight As Integer = 20
       '表的基本单位
       Dim XUnit As Integer
       Dim YUnit As Integer = TableFont.Height * 2.5
       '以下为模块内部使用
       Private Ev As PrintPageEventArgs
       Private DataTablePrinter As PrintDocument
       Private DataGridColumn As DataColumn
       Private DataGridRow As DataRow
       Private DataTablePrint As DataTable
       '当前要打印的行
       Private Rows As Integer
       '当前DATAGRID共有多少列
       Private ColsCount As Integer
       '当前正要打印的行号
       Private PrintingLineNumber As Integer
       '当前要所要打印的记录行数,由计算得到
       Private PageRecordNumber As Integer
       '正要打印的页号
       Private PrintingPageNumber As Integer
       '共需要打印的页数
       Private PageNumber As Integer
       '当前还有多少页没有打印
       Private PrintRecordLeave As Integer
       '已经打印完的记录数
       Private PrintRecordComplete As Integer
       Private PLeft As Integer
       Private PTop As Integer
       Private PRight As Integer
       Private PBottom As Integer
       Private PWidth As Integer
       Private PHeigh As Integer
       '当前画笔颜色
       Private DrawBrush As New SolidBrush(System.Drawing.Color.Black)
       '每页打印的记录条数
       Private PrintRecordNumber As Integer
       '总共应该打印的页数
       Private TotalPage As Integer

       Sub New(ByVal TableSource As DataTable)
           DataTablePrint = New DataTable
           DataTablePrint = TableSource
           ColsCount = DataTablePrint.Columns.Count
       End Sub

       '用户自定义字体及字号
       Public WriteOnly Property SetTableFont() As System.Drawing.Font
           Set(ByVal Value As System.Drawing.Font)
               TableFont = Value
           End Set
       End Property

       Public WriteOnly Property SetHeadFont() As System.Drawing.Font
           Set(ByVal Value As System.Drawing.Font)
               HeadFont = Value
           End Set
       End Property

       Public WriteOnly Property SetSubHeadFont() As System.Drawing.Font
           Set(ByVal Value As System.Drawing.Font)
               SubHeadFont = Value
           End Set
       End Property

       Public WriteOnly Property SetFootFont() As System.Drawing.Font
           Set(ByVal Value As System.Drawing.Font)
               FootFont = Value
           End Set
       End Property

       Public WriteOnly Property SetSubFootFont() As System.Drawing.Font
           Set(ByVal Value As System.Drawing.Font)
               SubFootFont = Value
           End Set
       End Property

       Public WriteOnly Property SetHeadText() As String
           Set(ByVal Value As String)
               HeadText = Value
           End Set
       End Property

       Public WriteOnly Property SetSubHeadLeftText() As String
           Set(ByVal Value As String)
               SubHeadLeftText = Value
           End Set
       End Property

       Public WriteOnly Property SetSubHeadRightText() As String
           Set(ByVal Value As String)
               SubHeadRightText = Value
           End Set
       End Property

       Public WriteOnly Property SetFootText() As String
           Set(ByVal Value As String)
               FootText = Value
           End Set
       End Property

       Public WriteOnly Property SetSubFootLeftText() As String
           Set(ByVal Value As String)
               SubFootLeftText = Value
           End Set
       End Property

       Public WriteOnly Property SetSubFootRightText() As String
           Set(ByVal Value As String)
               SubFootRightText = Value
           End Set
       End Property

       Public WriteOnly Property SetHeadHeight() As Integer
           Set(ByVal Value As Integer)
               HeadHeight = Value
           End Set
       End Property

       Public WriteOnly Property SetSubHeadHeight() As Integer
           Set(ByVal Value As Integer)
               SubHeadHeight = Value
           End Set
       End Property

       Public WriteOnly Property SetFootHeight() As Integer
           Set(ByVal Value As Integer)
               FootHeight = Value
           End Set
       End Property

       Public WriteOnly Property SetSubFootHeight() As Integer
           Set(ByVal Value As Integer)
               SubFootHeight = Value
           End Set
       End Property

       Public WriteOnly Property SetCellHeight() As Integer
           Set(ByVal Value As Integer)
               YUnit = Value
           End Set
       End Property

       Public Sub Print()
           Try
               DataTablePrinter = New Printing.PrintDocument
               AddHandler DataTablePrinter.PrintPage, AddressOf DataTablePrinter_PrintPage
               Dim PageSetup As PageSetupDialog
               PageSetup = New PageSetupDialog
               PageSetup.Document = DataTablePrinter
               DataTablePrinter.DefaultPageSettings = PageSetup.PageSettings
               If PageSetup.ShowDialog() = DialogResult.Cancel Then
                   Exit Sub
               End If
               PLeft = DataTablePrinter.DefaultPageSettings.Margins.Left
               PTop = DataTablePrinter.DefaultPageSettings.Margins.Top
               PRight = DataTablePrinter.DefaultPageSettings.Margins.Right
               PBottom = DataTablePrinter.DefaultPageSettings.Margins.Bottom
               PWidth = DataTablePrinter.DefaultPageSettings.Bounds.Width
               PHeigh = DataTablePrinter.DefaultPageSettings.Bounds.Height
               '将当前页分成基本的单元
               XUnit = (PWidth - PLeft - PRight) / DataTablePrint.Columns.Count - 1
               PrintRecordNumber = (PHeigh - PTop - PBottom - HeadHeight - SubHeadHeight - FootHeight - SubFootHeight - YUnit) \ YUnit
               If DataTablePrint.Rows.Count > PrintRecordNumber Then
                   If DataTablePrint.Rows.Count Mod PrintRecordNumber = 0 Then
                       TotalPage = DataTablePrint.Rows.Count \ PrintRecordNumber
                   Else
                       TotalPage = DataTablePrint.Rows.Count \ PrintRecordNumber + 1
                   End If
               Else
                   TotalPage = 1
               End If
               DataTablePrinter.DocumentName = TotalPage.ToString
               Dim PrintPriview As PrintPreviewDialog
               PrintPriview = New PrintPreviewDialog
               PrintPriview.Document = DataTablePrinter
               PrintPriview.WindowState = FormWindowState.Maximized
               PrintPriview.ShowDialog()
           Catch ex As Exception
               MsgBox("打印错误，请检查打印设置！", 16, "错误")
           End Try
       End Sub



       Private Sub DataTablePrinter_PrintPage(ByVal sender As Object, ByVal Ev As System.Drawing.Printing.PrintPageEventArgs)
           '还有多少条记录没有打印
           PrintRecordLeave = DataTablePrint.Rows.Count - PrintRecordComplete
           If PrintRecordLeave > 0 Then
               If PrintRecordLeave Mod PrintRecordNumber = 0 Then
                   PageNumber = PrintRecordLeave \ PrintRecordNumber
               Else
                   PageNumber = PrintRecordLeave \ PrintRecordNumber + 1
               End If
           Else
               PageNumber = 0
           End If
           '正在打印的页数，因为每打印一个新页都要计算还有多少页没有打印所以以打印的页数初始为0
           PrintingPageNumber = 0
           '计算,余下的记录条数是否还可以在一页打印,不满一页时为假
           If DataTablePrint.Rows.Count - PrintingPageNumber * PrintRecordNumber >= PrintRecordNumber Then
               PageRecordNumber = PrintRecordNumber
           Else
               PageRecordNumber = (DataTablePrint.Rows.Count - PrintingPageNumber * PrintRecordNumber) Mod PrintRecordNumber
           End If
           Dim fmt As New StringFormat
           '上下对齐
           fmt.LineAlignment = StringAlignment.Center
           '自动换行
           fmt.FormatFlags = StringFormatFlags.LineLimit
           '打印区域
           Dim Rect As New Rectangle
           '打印表格线格式
           Dim Pen As New Pen(Brushes.Black, 1)
           While PrintingPageNumber <= PageNumber
               '表头中间对齐
               fmt.Alignment = StringAlignment.Center
               '表头和副表头宽度等于设置区域宽度
               Rect.Width = PWidth - PLeft - PRight
               Rect.Height = HeadHeight
               Rect.X = PLeft
               Rect.Y = PTop
               '打印表头
               Ev.Graphics.DrawString(HeadText, HeadFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               '副表头左对齐
               fmt.Alignment = StringAlignment.Near
               Rect.Width = (PWidth - PLeft - PRight) / 2 - 1
               Rect.Height = SubHeadHeight
               Rect.Y = PTop + HeadHeight
               '打印副表头左
               Ev.Graphics.DrawString(SubHeadLeftText, SubHeadFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               '右副表头文字从右往左排列
               fmt.FormatFlags = StringFormatFlags.DirectionRightToLeft
               '右副表头右对齐
               fmt.Alignment = StringAlignment.Near
               Rect.X = PLeft + (PWidth - PLeft - PRight) / 2
               '打印副表头右
               Ev.Graphics.DrawString(SubHeadRightText, SubHeadFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               fmt.Alignment = StringAlignment.Center
               Rect.X = PLeft
               Rect.Y = PTop + HeadHeight + SubHeadHeight + (PrintRecordNumber + 1) * (YUnit) + SubFootHeight
               Rect.Height = FootHeight
               Rect.Width = PWidth - PLeft - PRight
               '打印表脚
               Ev.Graphics.DrawString(FootText, FootFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               '副表左左对齐
               fmt.Alignment = StringAlignment.Far
               Rect.X = PLeft
               Rect.Y = PTop + HeadHeight + SubHeadHeight + (PrintRecordNumber + 1) * (YUnit)
               Rect.Height = SubFootHeight
               Rect.Width = (PWidth - PLeft - PRight) / 2 - 1
               '打印左表脚
               Ev.Graphics.DrawString(SubFootLeftText, SubFootFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               '副表头右对齐
               fmt.Alignment = StringAlignment.Near
               Rect.X = PLeft + (PWidth - PLeft - PRight) / 2
               If DataTablePrint.Rows.Count = 0 Then
                   SubFootRightText = "第" & TotalPage & "页，共" & TotalPage & "页"
               Else
                   SubFootRightText = "第" & TotalPage - PageNumber + 1 & "页，共" & TotalPage & "页"
               End If
               '打印右表脚
               Ev.Graphics.DrawString(SubFootRightText, SubFootFont, Brushes.Black, RectangleF.op_Implicit(Rect), fmt)
               '得到datatable的所有列名
               fmt.Alignment = StringAlignment.Center
               Dim ColumnText(DataTablePrint.Columns.Count) As String
               For Cols = 0 To DataTablePrint.Columns.Count - 1
                   '得到当前所有的列名
                   ColumnText(Cols) = DataTablePrint.Columns(Cols).ToString
                   Rect.X = PLeft + XUnit * Cols
                   Rect.Y = PTop + HeadHeight + SubHeadHeight
                   Rect.Width = XUnit
                   Rect.Height = YUnit
                   Ev.Graphics.DrawString(ColumnText(Cols), New Font(TableFont, FontStyle.Bold), DrawBrush, RectangleF.op_Implicit(Rect), fmt)
                   Ev.Graphics.DrawRectangle(Pen, Rect)
               Next
               '结束---------------------得到datatable的所有列名
               '当前页面已经打印的记录行数
               Dim PrintingLine As Integer = 0
               While PrintingLine < PageRecordNumber
                   '确定要当前要打印的记录的行号
                   DataGridRow = DataTablePrint.Rows(PrintRecordComplete)
                   For Cols = 0 To DataTablePrint.Columns.Count - 1
                       Rect.X = PLeft + XUnit * Cols
                       Rect.Y = PTop + HeadHeight + SubHeadHeight + (PrintingLine + 1) * (YUnit)
                       Rect.Width = XUnit
                       Rect.Height = YUnit
                       If DataGridRow(ColumnText(Cols)) Is System.DBNull.Value = False Then
                           Ev.Graphics.DrawString(DataGridRow(ColumnText(Cols)), TableFont, DrawBrush, RectangleF.op_Implicit(Rect), fmt)
                       End If
                       Ev.Graphics.DrawRectangle(Pen, Rect)
                   Next
                   PrintingLine += 1
                   PrintRecordComplete += 1
                   If PrintRecordComplete >= DataTablePrint.Rows.Count Then
                       Ev.HasMorePages = False
                       PrintRecordComplete = 0
                       Exit Sub
                   End If
               End While
               PrintingPageNumber += 1
               If PrintingPageNumber >= PageNumber Then
                   Ev.HasMorePages = False
               Else
                   Ev.HasMorePages = True
                   Exit While
               End If
           End While
       End Sub

       '附转换函数
       'listview转为Datatable函数
       Public Function ListviewToDatatable(ByVal Listview1 As ListView)
           Dim Table1 As New DataTable
           Dim i As Integer
           Dim datacol As DataColumn
           For i = 0 To Listview1.Columns.Count - 1
               datacol = New DataColumn
               datacol.DataType = Type.GetType("System.Object")
               datacol.ColumnName = Listview1.Columns(i).Text.Trim
               Table1.Columns.Add(datacol)
           Next i
           Dim j As Integer
           Dim Datarow1 As DataRow
           For j = 0 To Listview1.Items.Count - 1
               Datarow1 = Table1.NewRow
               For i = 0 To Listview1.Columns.Count - 1
                   Datarow1.Item(Listview1.Columns(i).Text.Trim) = Listview1.Items(j).SubItems(i).Text.ToString
               Next i
               Table1.Rows.Add(Datarow1)
           Next j
           Return Table1
       End Function

       '数据集转为Datatable函数
       Public Function DataBaseToDataTable(ByVal SqlDataAdapter1 As Data.SqlClient.SqlDataAdapter, ByVal TableName As String)
           Dim Table1 As New DataTable
           Dim DataSet1 As New DataSet
           DataSet1.Clear()
           SqlDataAdapter1.Fill(DataSet1, TableName)
           Table1 = DataSet1.Tables(TableName)
           Return Table1
       End Function
   End Class



   Public Class FormPrintDataTable

       Private Sub ButtonClickMe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClickMe.Click
           Dim SqlConn As New OleDb.OleDbConnection
           Dim SqlCmd As New OleDb.OleDbCommand
           Dim SqlAda As New OleDb.OleDbDataAdapter
           Dim Dt As New DataTable
           SqlConn.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=" & Application.StartupPath & "\data\data.mdb"
           SqlCmd.Connection = SqlConn
           SqlCmd.CommandText = "select * from [CARD]"
           SqlConn.Open()
           SqlAda.SelectCommand = SqlCmd
           SqlAda.Fill(Dt)
           SqlCmd.Cancel()
           SqlConn.Close()
           Dim PDt As New PrintDataTable(Dt)
           PDt.SetHeadText = "XXXX煤矿报表测试"
           PDt.SetFootText = "矿长签字：             总工签字：             值班人员签字："
           PDt.SetSubHeadLeftText = "数据时间：" & Now.Date
           PDt.SetSubHeadRightText = "打印时间：" & Now.Date
           PDt.Print()
       End Sub
   End Class

   版本二 C# 打印DataTable 来自 http://blog.csdn.net/lee576/article/details/3352335

   public class PrintFunction
       {
           public String printName = String.Empty;
           public Font prtTextFont = new Font("Verdana", 10);
           public Font prtTitleFont = new Font("宋体", 10);
           private String[] titles = new String[0]; 
           public String[] Titles
           {
               set
               {
                   titles = value as String[];
                   if (null == titles)
                   {
                       titles = new String[0];
                   }
               }
               get
               {
                   return titles;
               }
           }
           private Int32 left = 20;
           private Int32 top = 20;
           public Int32 Top
           {
               set
               {
                   top = value;                
               }
               get
               {
                   return top;
               }
           }
           public Int32 Left
           {
               set
               {
                   left = value;
               }
               get
               {
                   return left;
               }
           }
           private DataTable printedTable;
           private Int32 pheight;
           private Int32 pWidth;
           private Int32 pindex;
           private Int32 curdgi;
           private Int32[] width;
           private Int32 rowOfDownDistance = 3;
           private Int32 rowOfUpDistance = 2;
           public Boolean PrintDataTable(DataTable table)
           {
               PrintDocument prtDocument = new PrintDocument();
               try
               {
                   if (printName != String.Empty)
                   {
                       prtDocument.PrinterSettings.PrinterName = printName;
                   }
                   prtDocument.DefaultPageSettings.Landscape = true;
                   prtDocument.OriginAtMargins = false;
                   PrintDialog prtDialog = new PrintDialog();
                   prtDialog.AllowSomePages = true;

                   prtDialog.ShowHelp = false;
                   prtDialog.Document = prtDocument;

                   if (DialogResult.OK != prtDialog.ShowDialog())
                   {
                       return false;
                   }

                   printedTable = table;
                   pindex = 0;
                   curdgi = 0;
                   width = new Int32[printedTable.Columns.Count];
                   pheight = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Bottom;
                   pWidth = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Right;

                   prtDocument.PrintPage += new PrintPageEventHandler(Document_PrintPage);

                   prtDocument.Print();

               }
               catch( InvalidPrinterException ex )
               {
                   MessageBox.Show("没有安装打印机");
               }
               catch (Exception ex)
               {
                   MessageBox.Show("打印错误");
               }
               prtDocument.Dispose();
               return true;
           }

           Int32 startColumnControls = 0;
           Int32 endColumnControls = 0;
           private void Document_PrintPage(object sender, PrintPageEventArgs ev)
           {
               Int32 x = left;
               Int32 y = top;
               Int32 rowOfTop = top;
               Int32 i;
               Pen pen = new Pen(Brushes.Black, 1);
               if (0 == pindex)
               {
                   for (i = 0; i < titles.Length; i++)
                   {
                       ev.Graphics.DrawString(titles[i], prtTitleFont, Brushes.Black, left, y + rowOfUpDistance);
                       y += prtTextFont.Height + rowOfDownDistance;
                   }
                   rowOfTop = y;
                   foreach(DataRow dr in printedTable.Rows)
                   {
                       for (i = 0; i < printedTable.Columns.Count; i++)
                       {
                           Int32 colwidth = Convert.ToInt16(ev.Graphics.MeasureString(dr[i].ToString().Trim(), prtTextFont).Width);
                           if (colwidth>width[i])
                           {
                               width[i] = colwidth;
                           }
                       }
                   }
               }
               for (i = endColumnControls; i < printedTable.Columns.Count; i++)
               {
                   String headtext = printedTable.Columns[i].ColumnName.Trim();
                   if (pindex == 0)
                   {
                       int colwidth = Convert.ToInt16(ev.Graphics.MeasureString(headtext, prtTextFont).Width);
                       if (colwidth > width[i])
                       {
                           width[i] = colwidth;
                       }
                   }
                   if (x + width[i] > pheight)
                   {
                       break;
                   }
                   ev.Graphics.DrawString(headtext, prtTextFont, Brushes.Black, x, y + rowOfUpDistance);
                   x += width[i];
               }
               startColumnControls = endColumnControls;
               if (i < printedTable.Columns.Count)
               {
                   endColumnControls = i;
                   ev.HasMorePages = true;
               }
               else
               {   
                   endColumnControls = printedTable.Columns.Count;
               }
               ev.Graphics.DrawLine(pen, left, rowOfTop, x, rowOfTop);
               y += rowOfUpDistance + prtTextFont.Height + rowOfDownDistance;
               ev.Graphics.DrawLine(pen, left, y, x, y);
               for (i = curdgi; i < printedTable.Rows.Count; i++)
               {
                   x = left;
                   for (Int32 j = startColumnControls; j < endColumnControls; j++)
                   {
                       ev.Graphics.DrawString(printedTable.Rows[i][j].ToString().Trim(), prtTextFont, Brushes.Black, x, y + rowOfUpDistance);
                       x += width[j];
                   }
                   y += rowOfUpDistance + prtTextFont.Height + rowOfDownDistance;
                   ev.Graphics.DrawLine(pen, left, y, x, y);
                   if (y > pWidth - prtTextFont.Height - 30)
                   {
                       break;
                   }
               }
               ev.Graphics.DrawLine(pen, left, rowOfTop, left, y);
               x = left;
               for (Int32 k = startColumnControls; k < endColumnControls; k++)
               {
                   x += width[k];
                   ev.Graphics.DrawLine(pen, x, rowOfTop, x, y);
               }
               if (y > pWidth - prtTextFont.Height - 30)
               {
                   pindex++;
                   if (0 != startColumnControls)
                   {
                       curdgi = i + 1;
                   }
                   if (!ev.HasMorePages)
                   {
                       endColumnControls = 0;
                   }
                   ev.HasMorePages = true;
               }
           }
           public void PrintPreviewDataTable(DataTable prtTable)
           {
               PrintDocument prtDocument = new PrintDocument();
               try
               {
                   if (printName != String.Empty)
                   {
                       prtDocument.PrinterSettings.PrinterName = printName;

                   }
                   prtDocument.DefaultPageSettings.Landscape = true;
                   prtDocument.OriginAtMargins = false;
                   printedTable = prtTable;
                   pindex = 0;
                   curdgi = 0;
                   width = new int[printedTable.Columns.Count];
                   pheight = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Bottom;
                   pWidth = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Right;
                   prtDocument.PrintPage += new PrintPageEventHandler(Document_PrintPage);
                   PrintPreviewDialog prtPreviewDialog = new PrintPreviewDialog();
                   prtPreviewDialog.Document = prtDocument;
                   prtPreviewDialog.ShowIcon = false;
                   prtPreviewDialog.WindowState = FormWindowState.Maximized;
                   prtPreviewDialog.ShowDialog();

               }
               catch (InvalidPrinterException ex)
               {
                   MessageBox.Show("没有安装打印机");
               }
               catch (Exception ex)
               {
                   MessageBox.Show("打印预览失败");
               }

           }

  

       }

     */

    public class PrintDirectClass
    {
        private int printNum = 0;//多页打印
        public string imageFile = "";//单个图片文件
        private ArrayList fileList = new ArrayList();//多个图片文件

        public void PrintPreview()
        {
            PrintDocument docToPrint = new PrintDocument();
            docToPrint.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.docToPrint_BeginPrint);
            docToPrint.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.docToPrint_EndPrint);
            docToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.docToPrint_PrintPage);
            docToPrint.DefaultPageSettings.Landscape = true;

            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowSomePages = true;
            printDialog.ShowHelp = true;
            printDialog.Document = docToPrint;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }
        private void docToPrint_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (fileList.Count == 0)
                fileList.Add(imageFile);
        }
        private void docToPrint_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {




            //图片抗锯齿            
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Stream fs = new FileStream(fileList[printNum].ToString().Trim(), FileMode.Open, FileAccess.Read);
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            int x = e.MarginBounds.X;//100
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }
            //DrawImage参数根据打印机和图片大小自行调整     




            // width += 60; height += 60;


            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(20, 20, width, height);
            // System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, width, height); 

            if (image.Height < 310)
            {
                // e.Graphics.DrawImage(image, 0, 30, image.Width + 20, image.Height);
                //  System.Drawing.Rectangle destRect1 = new System.Drawing.Rectangle(0, 30, image.Width, image.Height);                
                // e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel); 
                //  e.Graphics.DrawImage(image, destRect, (e.PageBounds.Width - width) / 2, (e.PageBounds.Height - height) / 2, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel); //其中e.PageBounds属性表示页面全部区域的矩形区域
                e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel); //其中e.PageBounds属性表示页面全部区域的矩形区域
            }
            else
            {
                //e.Graphics.DrawImage(image, 0, 0, image.Width + 20, image.Height);
                //  System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);                
                //e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);  
                //  e.Graphics.DrawImage(image, destRect, (e.PageBounds.Width-width)/2, (e.PageBounds.Height-height)/2, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel); 
                e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
            }
            if (printNum < fileList.Count - 1)
            {
                printNum++;
                e.HasMorePages = true;//HasMorePages为true则再次运行PrintPage事件                
                return;
            }
            e.HasMorePages = false;
        }
    }

    #endregion




}



