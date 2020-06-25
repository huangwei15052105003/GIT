using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Windows.Forms;

namespace LithoForm
{
    class classMaintain
    {
        public static bool QueryFlow(ref DataTable dtShow)
        {
            dtShow = null;
            dtShow = new DataTable();

            string part = Interaction.InputBox("请输入产品名;", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) 
            { 
                MessageBox.Show("产品名输入不正确，退出");
                return false;
            }
            part = part.Trim();
            dtShow = LithoForm.Flow.FlowQuery(part);
            MessageBox.Show("Done\n\nLines:" + dtShow.Rows.Count.ToString()) ;
            return true;
        }
      public static void DeleteCdSemRecipe(ref DataTable dtShow)
        {
            MessageBox.Show("暂不运行\n\n--> 程序仅删除IDW，IDP，LinkRecipe，但设备数据库中数据无法删除，LinkRecipe需要手动删除");
            //return;
            #region CD SEM IP & INLINE WIP
            //使用“df -k”命令,以KB为单位显示磁盘使用量和占用率。 
            MessageBox.Show("运行前，请先确认已下载最新WIP数据");

            Dictionary<string, string> toolIp = new Dictionary<string, string>(){
                {"ALCD01","10.4.152.56" }, 
                {"ALCD02", "10.4.152.53" }, 
                {"ALCD03", "10.4.152.54" },
                {"ALCD04", "10.4.151.79"}, 
                {"ALCD05", "10.4.151.81"}, 
                {"ALCD06", "10.4.151.82"},
                {"ALCD07", "10.4.151.50"}, 
                {"ALCD08", "10.4.153.26"}, 
                {"ALCD09", "10.4.152.55"},
                {"ALCD10", "10.4.153.32"}, 
                {"BLCD11", "10.4.131.48"}, 
                {"BLCD12", "10.4.131.47"},
                {"SERVER", "10.4.72.240"}  };

            string connStr, sql;

            try
            {

                connStr = @"data source=P:\_SQLite\ReworkMove.DB";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = "SELECT distinct substr(part,1,6) part FROM TBL_REALWIP";
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
            }
            catch //10.4.2.112 无法调用sqlite库
            {
                MessageBox.Show("10.4.2.112无法访问SQLITE数据，请读取最新的在线WIP数据");
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"p:\temp\";
                openFileDialog1.Title = "选择源文件";
                openFileDialog1.Filter = "csv文件(*.csv)|*.csv";//
                string filename;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName.Replace("\\", "\\\\");
                }
                else
                { MessageBox.Show("未选择数据文件"); return; }
                dtShow = LithoForm.LibF.OpenCsvWithComma(filename);
                MessageBox.Show("在线共有" + dtShow.Rows.Count.ToString() + "新品，Part前六位统计");

               
            }
        
            #endregion



            string[] recipeList;
            string[] dataList;
            int count = 0;
            string[] arr1;//class folder
            string[] arr2; //idw folder
            string[] arr3;//idp folder
            string[] arr4;//image folder
            string name;

            #region define tool/class

            string tool = Interaction.InputBox("请输设备名，如ALCD02，BLCD11", "定义设备", "", -1, -1);


            string recipeClass = Interaction.InputBox("请输入两位数字的CLASS名字", "定义CLASS", "", -1, -1);

            tool = tool.Trim().ToUpper();
            recipeClass = recipeClass.Trim();

            string ipaddress;
            try
            {
                ipaddress = toolIp[tool];
            }
            catch
            {
                MessageBox.Show("设备名不对,退出"); return;
            }

            if (recipeClass.Length == 2 && Char.IsNumber(recipeClass, 0) && Char.IsNumber(recipeClass, 1))
            { }
            else
            {
                MessageBox.Show(" _Class_ 名不对,退出"); return;
            }
            #endregion

   



            HitachiFtpWeb hitachiFtpWeb = new HitachiFtpWeb(ipaddress, "/HITACHI/DEVICE/HD", "user02", "qw!1234");
            recipeList = hitachiFtpWeb.GetFilesDetailList(recipeClass + "/recipe/");
            dataList = hitachiFtpWeb.GetFilesDetailList(recipeClass + "/data/");


            MessageBox.Show("Now Click 确定 To Start Deleting........\n\n命令可查看删除进度：  ls -l 查看目录 | grep \" ^ -\" | wc -l");



            //delete recipe


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"P:\temp\DeletedLinkRecipe"+ DateTime.Now.ToString("yyyyMMdd_HHmmss")+".txt", true))
            {
                file.WriteLine("    "); file.WriteLine("    ");
                file.WriteLine("###############################");
                file.WriteLine(DateTime.Now.ToString());
                file.WriteLine("###############################");
                 file.WriteLine("    ");
                foreach (string recipe in recipeList)
                {

                    // count += 1; if (count > 10) { break; }
                    arr1 = recipe.Split(new char[] { ' ' });
                    if (arr1[0].StartsWith("-") && arr1[arr1.Length - 1].EndsWith(".txt"))
                    {
                        name = arr1[arr1.Length - 1];
                        //   MessageBox.Show(name + ",  "+ name.Substring(0, 6)+",      " + (dtShow.Select("part='" + name.Substring(0, 6) + "'")).Length.ToString());
                        if (name.Length < 6 || (dtShow.Select("part='" + name.Substring(0, 6) + "'")).Length == 0)
                        {
                            name = recipeClass + "/recipe/" + name;

                            //        MessageBox.Show( name+",   ToDelete");
                            hitachiFtpWeb.Delete(name);
                            file.WriteLine(name);
                        }
                    }
                }

            }

            //delete idw/idp
            count = 0;
            foreach (string data in dataList)  //idw file and idw folder
            {
                arr1 = data.Split(new char[] { ' ' });
                name = arr1[arr1.Length - 1];

                if (name.Length < 6 || (dtShow.Select("part='" + name.Substring(0, 6) + "'")).Length == 0) //IDW 名字少于6位的，或
                {
                    //  count += 1; if (count > 2) { break; }
                    name = recipeClass + "/data/" + name;
                    //    MessageBox.Show("idw folder:  "+name);

                    if (arr1[0].StartsWith("-"))// && arr1[arr1.Length - 1].EndsWith(".idw"))  // delete IDW file
                    {
                        hitachiFtpWeb.Delete(name);
                    }
                    else if (arr1[0].StartsWith("d"))   //delete IDW folder, empty folder first
                    {
                        string[] idwList = hitachiFtpWeb.GetFilesDetailList(name);
                        foreach (string idp in idwList)
                        {

                            arr2 = idp.Split(new char[] { ' ' });
                            string name1 = name + "/" + arr2[arr2.Length - 1];
                            if (idp.Substring(0, 1) == "-")
                            {
                                hitachiFtpWeb.Delete(name1);
                            }
                            else if (idp.Substring(0, 1) == "d")
                            {
                                string[] idpList = hitachiFtpWeb.GetFilesDetailList(name1);
                                foreach (string str1 in idpList)
                                {
                                    arr3 = str1.Split(new char[] { ' ' });
                                    string name2 = name1 + "/" + arr3[arr3.Length - 1];
                                    if (str1.Substring(0, 1) == "-")
                                    {
                                        hitachiFtpWeb.Delete(name2);
                                    }
                                    else if (str1.Substring(0, 1) == "d")
                                    {

                                        string[] imageList = hitachiFtpWeb.GetFilesDetailList(name2);
                                        foreach (string str2 in imageList)
                                        {
                                            arr4 = str2.Split(new char[] { ' ' });
                                            string name3 = name2 + "/" + arr4[arr4.Length - 1];
                                            hitachiFtpWeb.Delete(name3);
                                        }
                                        hitachiFtpWeb.RemoveDirectory(name2);
                                    }
                                }
                            }
                            hitachiFtpWeb.RemoveDirectory(name1);
                        }
                        hitachiFtpWeb.RemoveDirectory(name);
                    }
                }

            }



            MessageBox.Show("========DONE============");
        }

        public static void MakeSqlForQueryJobinStationByWip(ref DataTable dtShow)
        {
            MessageBox.Show("生成 OPAS Oracle SQL\r\n\r\n\r\n\r\n查询Inline WIP Job-In-Station的值\r\n\r\n\r\n\r\nsql在10.4.3.130服务器使用\r\n\r\n\r\n\r\n数据格式符合R2R Import命令格式，按需修改数据后直接导入" +
                "\r\n\r\n\r\n\r\n另在线WIP需要访问MFG_DB提前刷新；未及时更新数据可能有误");
                              
            string connStr = @"data source=P:\_SQLite\ReworkMove.DB";
            string sql;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT * FROM TBL_REALWIP";
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
            LithoForm.R2R.GenerateJobinStationSql(dtShow);
            MessageBox.Show("文件 C:\\temp\\sql.txt 已保存\r\n\r\n\r\n使用sql在服务器10.4.3.130导出数据约需4分钟\r\n\r\n\r\n文件约50M");
        }
        public static void MakeSqlForQueryCD(ref DataTable dtShow)
        {

            MessageBox.Show("最近n天内R2R CD数据\r\n\r\n\r\n\r\n" +
             "" +
             "");
            long days;
            try
            {
                days = Convert.ToInt32(Interaction.InputBox("请输入倒数？天天数;", "定义天数", "", -1, -1).Trim());
            }
            catch
            {
                MessageBox.Show("请确认输入字符是否正确，退出"); return;
            }
            string riqi = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
            MessageBox.Show("开始的统计日期是：" + riqi);

            string sql = " SELECT " +
                  " to_char(DCOLL_TIME, 'yyyy-MM-dd HH24:mi:ss') DCOLL_TIME,to_char(JI_TIME, 'yyyy-MM-dd HH24:mi:ss') JI_TIME," +
                  " LOT_ID LotID, PART_NAME PART, TECH_NAME TECH, LAYER_NAME LAYER,	PROC_EQ_ID TOOL, MET_EQ_ID,	GROUP_TYPE TYPE," +
                  " EQP_TYPE,MET_AVG AVG,PARAM_VALUE JOBIN, OPT_VALUE OPT,	RESULT_VALUE FB,MET_PARAM_NAME ITEM " +
                  " FROM  r2rph.proc_dcoll_fb_view WHERE  RESULT_VALUE IS NOT NULL   AND " +
                  " DCOLL_TIME > SYSDATE - " + days + " ORDER BY  EQP_TYPE, DCOLL_TIME ASC ";

            System.IO.File.WriteAllText(@"C:\temp\sql.txt", sql);
            MessageBox.Show("文件 C:\\temp\\sql.txt 已保存");
        }
    }
}



/*
    #region //R2R
        private void 导入R2RCD数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string strDate;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr[0].ToString();
                    }
                }
            }
            //读取导入最新数据
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            DataTable dtTmp = LithoForm.LibF.OpenCsvNew(filePath);
            //判断导入文件中的日期是否有小于 数据库最新日期的，以保证数据连续性

            MessageBox.Show(dtTmp.Rows[0]["DCOLL_TIME"].ToString() + "," + strDate + "," + string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate).ToString());

            if (string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate) == 1)

            { MessageBox.Show("新数据和数据库数据无日期交叠，请选择更早的日期下载OPAS CD数据"); return; }



            DataRow[] drs = dtTmp.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            dtShow = dtTmp.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    dtShow.ImportRow(drs[i]);
                }

                string dbTblName = "tbl_cd";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
                MessageBox.Show("CD Update Done");
                dataGridView1.DataSource = dtShow;
                dtShow = null;
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }

            MessageBox.Show((DateTime.Now - tmpDate).ToString());

        }

        private void 导入R2ROVL数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strDate;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_ovl";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr[0].ToString();
                    }
                }
            }
            //读取导入最新数据
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            DataTable dtTmp = LithoForm.LibF.OpenCsvNew(filePath);
            //判断导入文件中的日期是否有小于 数据库最新日期的，以保证数据连续性

            MessageBox.Show(dtTmp.Rows[0]["DCOLL_TIME"].ToString() + "," + strDate + "," + string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate).ToString());

            if (string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate) == 1)

            { MessageBox.Show("新数据和数据库数据无日期交叠，请选择更早的日期下载OPAS CD数据"); return; }



            DataRow[] drs = dtTmp.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            dtShow = dtTmp.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    dtShow.ImportRow(drs[i]);
                }



                string dbTblName = "tbl_ovl";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
                MessageBox.Show("OVL Update Done");
                dataGridView1.DataSource = dtShow;
                dtShow = null;
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }



        }
        private void 导入CDCONFIG数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //读取
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            dtShow = LithoForm.LibF.OpenCsvNew(filePath);

            //删除旧数据
            string dbTblName = "tbl_config";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            //导入最新数据
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
            MessageBox.Show("CD Config Update Done");
            dataGridView1.DataSource = dtShow;
            dtShow = null;

        }
        private void 导入短流程工艺代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TO BE COMPLETED");
        }
        private void 导入NikonJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string filePath;
            connStr = "data source=P:\\_SQLite\\NikonJobinStation.db";
            string dbTblName = "tbl_nikonjobinstation";
            //读取
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(NikonJobinStation.csv)|NikonJobinStation.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }






            bool isFirst = true;
            string strLine;
            int n = 0;
            int rowCount = 0;
            string newLine = string.Empty;
            dtShow = new DataTable();
            DataTable dtTmp = new DataTable();
            int stepCount = 0;
            DataTableToSQLte myTabInfo;

            StreamReader sr = new StreamReader(filePath);
            while ((strLine = sr.ReadLine()) != null)
            {
                if (strLine.Contains("\""))   //将逗号改为分号，
                {
                    n = 0; newLine = "";
                    foreach (char x in strLine)
                    {
                        if (x.ToString() != "\"" && n % 2 == 0)
                        {
                            newLine += x.ToString();
                        }
                        else if (x.ToString() == "\"" && n % 2 == 0)
                        {
                            n += 1;
                        }
                        else if (x.ToString() != "\"" && n % 2 == 1)
                        {
                            if (x.ToString() != ",")
                            {
                                newLine += x.ToString();
                            }
                            else
                            {
                                newLine += ";";
                            }

                        }

                    }
                    strLine = newLine;
                }

                if (isFirst == true)
                {
                    isFirst = false;
                    // MessageBox.Show(strLine);
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {

                        dtShow.Columns.Add(colAry[i]);
                    }
                    dtTmp = dtShow.Clone();
                }
                else
                {
                    rowCount += 1;
                    stepCount += 1;
                    DataRow newRow = dtShow.NewRow();
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {
                        newRow[i] = colAry[i];
                    }
                    dtShow.Rows.Add(newRow);

                    if (stepCount > 50000)
                    {

                        myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\NikonJobinStation.DB");
                        stepCount = 0;
                        dtShow.Rows.Clear();
                        dtShow = null;
                        ClearMemory();
                        dtShow = dtTmp.Clone();

                    }



                }
            }
            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\NikonJobinStation.DB");
            MessageBox.Show((DateTime.Now - tmpDate).ToString());
            MessageBox.Show("导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();
        }
        private void 导入AsmlJobinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string filePath;
            connStr = "data source=P:\\_SQLite\\AsmlJobinStation.db";
            string dbTblName = "tbl_asmljobinstation";
            //读取
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(AsmlJobinStation.csv)|AsmlJobinStation.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }






            bool isFirst = true;
            string strLine;
            int n = 0;
            int rowCount = 0;
            string newLine = string.Empty;
            dtShow = new DataTable();
            DataTable dtTmp = new DataTable();
            int stepCount = 0;
            DataTableToSQLte myTabInfo;

            StreamReader sr = new StreamReader(filePath);
            while ((strLine = sr.ReadLine()) != null)
            {
                if (strLine.Contains("\""))   //将逗号改为分号，
                {
                    n = 0; newLine = "";
                    foreach (char x in strLine)
                    {
                        if (x.ToString() != "\"" && n % 2 == 0)
                        {
                            newLine += x.ToString();
                        }
                        else if (x.ToString() == "\"" && n % 2 == 0)
                        {
                            n += 1;
                        }
                        else if (x.ToString() != "\"" && n % 2 == 1)
                        {
                            if (x.ToString() != ",")
                            {
                                newLine += x.ToString();
                            }
                            else
                            {
                                newLine += ";";
                            }

                        }

                    }
                    strLine = newLine;
                }

                if (isFirst == true)
                {
                    isFirst = false;
                    MessageBox.Show(strLine);
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {

                        dtShow.Columns.Add(colAry[i]);
                    }
                    dtTmp = dtShow.Clone();
                }
                else
                {
                    rowCount += 1;
                    stepCount += 1;
                    DataRow newRow = dtShow.NewRow();
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {
                        newRow[i] = colAry[i];
                    }
                    dtShow.Rows.Add(newRow);

                    if (stepCount > 10000)
                    {

                        myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\AsmlJobinStation.DB");
                        stepCount = 0;
                        dtShow.Rows.Clear();

                        dtShow = null;
                        ClearMemory();
                        dtShow = dtTmp.Clone();

                    }



                }
            }
            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\AsmlJobinStation.DB");
            MessageBox.Show((DateTime.Now - tmpDate).ToString());
            MessageBox.Show("导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();

        }
          private void 导入MaskInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connStr = @"data source=P:\_SQLite\ReworkMove.DB";

            LithoForm.Flow.UpdateMask(connStr);
        }
                private void 调用Python更细AWEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "c:\\anaconda3\\python.exe";
            exep.StartInfo.Arguments = "P:\\_SQLite\\VsPython.py";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();


        }
        private void 运行曝光程序维护脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "P:\\_SQLite\\EXE\\exposurerecipe\\WindowsFormsExposureRecipe.exe";

            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();
        }
        #endregion
 */
