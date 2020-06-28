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

namespace LithoForm
{
    class Charting
    {
        public static void CreateDbFromChartingData()
        {
            string folder;
            string file = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<string> fileList = new List<string>();
            string connStr = "data source=D:\\TEMP\\DB\\ChartRawData.db";
            string dbName = "D:\\TEMP\\DB\\ChartRawData.db";
            string sql, latestDate;
            DataRow[] drs;
            DataRow newRow;

            //
            if (MessageBox.Show("是否需要将 P:\\_SQLite\\ChartRawData.db\r\n\r\n" +
                "复制到本机 D:\\TEMP\\DB\\ChartRawData.DB\r\n\r\n" +
                "\r\n\r\n注意：复制后程序从Excel文件更新本机数据文件" +
                "\r\n\r\n       Excel文件保存在 P:\\_ChartingRawData", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                File.Copy(@"p:\_SQLite\ChartRawData.DB", @"D:\TEMP\DB\ChartRawData.DB", true);
            }
            else
            { }

          

            //list RAR file
            folder = "P:\\_chartingrawdata";
            fileList = LithoForm.LibF.ExportFileList(folder, fileList);
            

            foreach (string item in fileList)
            {
                //手动保存和自动保存时，后缀rar有大小写区分
                string tmp = item.ToUpper();                              
                if (tmp.Contains("CHARTDATA.RAR") && string.Compare(tmp, file) > 0) 
                {
                 
                    file = tmp; 
                } 
            }

            //unrar file to c:/temp
            folder = "P:\\_SQLite\\EXE\\haozip\\haozipc e -y " + file + " -oc:\\temp\\";
          

            LithoForm.LibF.DosCommand(folder);
            #region //cd
            //read CD data
            dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "CD$");

            //get latest CD date
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DcollTime) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        latestDate = rdr[0].ToString();
                    }
                }
            }
            //列出最新日期的数据


          //  MessageBox.Show(latestDate.ToString());



            dt.Columns.Add("key");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (string.Compare(dt.Rows[0]["key"].ToString(), latestDate) == 1)
            { MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出"); return; }

           // MessageBox.Show(dt.Rows[0]["key"].ToString());

            //导入CD数据

            drs = dt.Select("key>'" + latestDate + "'", "key asc");
            dt1 = new DataTable();
            foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "JobIn", "Feedback", "Optimum", "CdAvg", "CdTarget", "WaferID", "S1", "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "MET_AVG" })
            { dt1.Columns.Add(col); }

            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    newRow = dt1.NewRow();
                    for (int j = 0; j < 24; j++)
                    {
                        if (j == 7)
                        {
                            newRow[j] = drs[i]["key"].ToString();
                        }
                        else
                        {
                            newRow[j] = drs[i][j].ToString();
                        }
                    }
                    dt1.Rows.Add(newRow);

                }
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_cd");
                myTabInfo.ImportToSqliteBatch(dt1, dbName);
                MessageBox.Show("CD Update Done");

                dt = null; dt1 = null;
            }
            else
            { MessageBox.Show("无CD新数据,未更新"); }

            #endregion

            #region //asml ovl
            //read  data
            dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "OL_ASML$");
            //get latest  date
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DcollTime) FROM tbl_ovl where substr(ProcEqID,3,1)='D'";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        latestDate = rdr[0].ToString();
                    }
                }
            }
            //列出最新日期的数据
            dt.Columns.Add("key");
            dt.Rows[0]["key"] = "0";//第一行为空
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }




            if (string.Compare(dt.Rows[1]["key"].ToString(), latestDate) == 1)
            { MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出"); return; }



            drs = dt.Select("key>'" + latestDate + "'", "key asc");
            dt1 = new DataTable();
            foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "WaferID",
                "TrxJobin", "TrxFb", "TrxOpt", "TrxAvg", "TrxValue", "TrxMin", "TrxMax",
                "TryJobin", "TryFb", "TryOpt", "TryAvg", "TryValue", "TryMin", "TryMax",
                "ExpxJobin", "ExpxFb", "ExpxOpt", "ExpxAvg", "ExpxValue",
                "ExpyJobin", "ExpyFb", "ExpyOpt", "ExpyAvg", "ExpyValue",
                "OrtJobin", "OrtFb", "OrtOpt", "OrtAvg", "OrtValue",
                "RotJobin", "RotFb", "RotOpt", "RotAvg", "RotValue",
                "SMagJobin", "SMagFb", "SMagOpt", "SMagAvg", "SMagValue",
                "SRotJobin", "SRotFb", "SRotOpt", "SRotAvg", "SRotValue",
                "ASMagJobin", "ASMagFb", "ASMagOpt", "ASMagAvg", "ASMagValue",
                "ASRotJobin",  "ASRotFb", "ASRotOpt", "ASRotAvg", "ASRotValue"  })
            { dt1.Columns.Add(col); }


            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    newRow = dt1.NewRow();
                    for (int j = 0; j < 63; j++)
                    {
                        if (j == 7)
                        {
                            newRow[j] = drs[i]["key"].ToString();
                        }
                        else
                        {
                            newRow[j] = drs[i][j].ToString();
                        }
                    }
                    dt1.Rows.Add(newRow);

                }
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_ovl");
                myTabInfo.ImportToSqliteBatch(dt1, dbName);
                MessageBox.Show("ASML OVL Update Done");

                dt = null; dt1 = null;
            }
            else
            { MessageBox.Show("无ASML OVL新数据,未更新"); }

            #endregion

            #region //Nikon ovl
            //read  data
            dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "OL_NIKON$");
            //get latest  date
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DcollTime) FROM tbl_ovl where substr(ProcEqID,3,1)<>'D'";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        latestDate = rdr[0].ToString();
                    }
                }
            }
            //列出最新日期的数据
            dt.Columns.Add("key");
            dt.Rows[0]["key"] = "0";//第一行为空
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }

            //  MessageBox.Show(dt.Rows[1]["key"].ToString());
            //  MessageBox.Show(latestDate);
            //  MessageBox.Show(string.Compare(dt.Rows[1]["key"].ToString(), latestDate).ToString());
            //  return;


            if (string.Compare(dt.Rows[1]["key"].ToString(), latestDate) == 1)
            { MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出"); return; }



            drs = dt.Select("key>'" + latestDate + "'", "key asc");
            dt1 = new DataTable();
            foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "WaferID",
                "TrxJobin", "TrxFb", "TrxOpt", "TrxAvg", "TrxValue", "TrxMin", "TrxMax",
                "TryJobin", "TryFb", "TryOpt", "TryAvg", "TryValue", "TryMin", "TryMax",
                "ExpxJobin", "ExpxFb", "ExpxOpt", "ExpxAvg", "ExpxValue",
                "ExpyJobin", "ExpyFb", "ExpyOpt", "ExpyAvg", "ExpyValue",
                "OrtJobin", "OrtFb", "OrtOpt", "OrtAvg", "OrtValue",
                "RotJobin", "RotFb", "RotOpt", "RotAvg", "RotValue",
                "SMagJobin", "SMagFb", "SMagOpt", "SMagAvg", "SMagValue",
                "SRotJobin", "SRotFb", "SRotOpt", "SRotAvg", "SRotValue",
                "ASMagJobin", "ASMagFb", "ASMagOpt", "ASMagAvg", "ASMagValue",
                "ASRotJobin",  "ASRotFb", "ASRotOpt", "ASRotAvg", "ASRotValue"  })
            { dt1.Columns.Add(col); }


            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    newRow = dt1.NewRow();
                    for (int j = 0; j < 53; j++)
                    {
                        if (j == 7)
                        {
                            newRow[j] = drs[i]["key"].ToString();
                        }
                        else
                        {
                            newRow[j] = drs[i][j].ToString();
                        }
                    }
                    dt1.Rows.Add(newRow);

                }
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_ovl");
                myTabInfo.ImportToSqliteBatch(dt1, dbName);
                MessageBox.Show("NIKON OVL Update Done");

                dt = null; dt1 = null;

                //更新P盘数据
                try
                {
                    File.Copy(@"D:\TEMP\DB\ChartRawData.DB", @"p:\_SQLite\ChartRawData.DB", true);
                    MessageBox.Show("P:\\_SQLite\\ChartRawData.DB已被本机文件覆盖");
                }
                catch
                {
                    MessageBox.Show("没有权限读写P:\\_SQLITE目录");
                }
            }
            else
            { MessageBox.Show("无NIKON OVL新数据,未更新"); }

            #endregion

        }

        public static void CreateDbFromChartingDataOneByOne()
        {
            string folder;
            string file = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<string> fileList = new List<string>();
            string connStr = "data source=D:\\TEMP\\DB\\ChartRawData.db";
            string dbName = "D:\\TEMP\\DB\\ChartRawData.db";
            string sql, latestDate;
            DataRow[] drs;
            DataRow newRow;

            //
            //  string pin = Interaction.InputBox("INPUT PIN:", "PIN", "", -1, -1);
            //  if (pin.Trim() == "123") { }
            //  else { MessageBox.Show("PIN IS NOT CORRECT,EXIT"); return; }


            //list RAR file
            folder = "P:\\_chartingrawdata";
            fileList = LithoForm.LibF.ExportFileList(folder, fileList);
            fileList.Sort();
            foreach (string item in fileList)
            {

                if (item.Contains("ChartData.RAR"))
                {
                    file = item;
                    //   MessageBox.Show(file);
                }


                //unrar file to c:/temp
                folder = "P:\\_SQLite\\EXE\\haozip\\haozipc e -y " + file + " -oc:\\temp\\";
                LithoForm.LibF.DosCommand(folder);
                #region //cd
                //read CD data
                dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "CD$");

                //get latest CD date
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = "SELECT max(DcollTime) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            latestDate = rdr[0].ToString();
                        }
                    }
                }
                //列出最新日期的数据

                dt.Columns.Add("key");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (string.Compare(dt.Rows[0]["key"].ToString(), latestDate) == 1)
                {
                    // MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出"); 
                    break;
                }
                //导入CD数据

                drs = dt.Select("key>'" + latestDate + "'", "key asc");
                dt1 = new DataTable();
                foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "JobIn", "Feedback", "Optimum", "CdAvg", "CdTarget", "WaferID", "S1", "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "MET_AVG" })
                { dt1.Columns.Add(col); }

                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        newRow = dt1.NewRow();
                        for (int j = 0; j < 24; j++)
                        {
                            if (j == 7)
                            {
                                newRow[j] = drs[i]["key"].ToString();
                            }
                            else
                            {
                                newRow[j] = drs[i][j].ToString();
                            }
                        }
                        dt1.Rows.Add(newRow);

                    }
                    DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_cd");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                    // MessageBox.Show("CD Update Done");

                    dt = null; dt1 = null;
                }
                else
                { //MessageBox.Show("无CD新数据,未更新"); }

                    #endregion

                    #region //asml ovl
                    //read  data
                    dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "OL_ASML$");
                    //get latest  date
                    using (SQLiteConnection conn = new SQLiteConnection(connStr))
                    {
                        conn.Open();
                        sql = "SELECT max(DcollTime) FROM tbl_ovl where substr(ProcEqID,3,1)='D'";//first record select English from graduate_phrase limit 0,1
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                rdr.Read();
                                latestDate = rdr[0].ToString();
                            }
                        }
                    }
                    //列出最新日期的数据
                    dt.Columns.Add("key");
                    dt.Rows[0]["key"] = "0";//第一行为空
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }




                    if (string.Compare(dt.Rows[1]["key"].ToString(), latestDate) == 1)
                    {
                        //MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出");
                        break;
                    }



                    drs = dt.Select("key>'" + latestDate + "'", "key asc");
                    dt1 = new DataTable();
                    foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "WaferID",
                "TrxJobin", "TrxFb", "TrxOpt", "TrxAvg", "TrxValue", "TrxMin", "TrxMax",
                "TryJobin", "TryFb", "TryOpt", "TryAvg", "TryValue", "TryMin", "TryMax",
                "ExpxJobin", "ExpxFb", "ExpxOpt", "ExpxAvg", "ExpxValue",
                "ExpyJobin", "ExpyFb", "ExpyOpt", "ExpyAvg", "ExpyValue",
                "OrtJobin", "OrtFb", "OrtOpt", "OrtAvg", "OrtValue",
                "RotJobin", "RotFb", "RotOpt", "RotAvg", "RotValue",
                "SMagJobin", "SMagFb", "SMagOpt", "SMagAvg", "SMagValue",
                "SRotJobin", "SRotFb", "SRotOpt", "SRotAvg", "SRotValue",
                "ASMagJobin", "ASMagFb", "ASMagOpt", "ASMagAvg", "ASMagValue",
                "ASRotJobin",  "ASRotFb", "ASRotOpt", "ASRotAvg", "ASRotValue"  })
                    { dt1.Columns.Add(col); }


                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            newRow = dt1.NewRow();
                            for (int j = 0; j < 63; j++)
                            {
                                if (j == 7)
                                {
                                    newRow[j] = drs[i]["key"].ToString();
                                }
                                else
                                {
                                    newRow[j] = drs[i][j].ToString();
                                }
                            }
                            dt1.Rows.Add(newRow);

                        }
                        DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_ovl");
                        myTabInfo.ImportToSqliteBatch(dt1, dbName);
                        // MessageBox.Show("ASML OVL Update Done");

                        dt = null; dt1 = null;
                    }
                    else
                    {
                        //  MessageBox.Show("无ASML OVL新数据,未更新");
                    }

                    #endregion

                    #region //Nikon ovl
                    //read  data
                    dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "OL_NIKON$");
                    //get latest  date
                    using (SQLiteConnection conn = new SQLiteConnection(connStr))
                    {
                        conn.Open();
                        sql = "SELECT max(DcollTime) FROM tbl_ovl where substr(ProcEqID,3,1)<>'D'";//first record select English from graduate_phrase limit 0,1
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                rdr.Read();
                                latestDate = rdr[0].ToString();
                            }
                        }
                    }
                    //列出最新日期的数据
                    dt.Columns.Add("key");
                    dt.Rows[0]["key"] = "0";//第一行为空
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["key"] = Convert.ToDateTime(dt.Rows[i]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //  MessageBox.Show(dt.Rows[1]["key"].ToString());
                    //  MessageBox.Show(latestDate);
                    //  MessageBox.Show(string.Compare(dt.Rows[1]["key"].ToString(), latestDate).ToString());
                    //  return;


                    if (string.Compare(dt.Rows[1]["key"].ToString(), latestDate) == 1)
                    {
                        //  MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出");
                        break;
                    }



                    drs = dt.Select("key>'" + latestDate + "'", "key asc");
                    dt1 = new DataTable();
                    foreach (string col in new string[] { "Tech", "PartID", "Layer", "LotID", "ProcEqID", "JiTime", "MetEqID", "DcollTime", "WaferID",
                "TrxJobin", "TrxFb", "TrxOpt", "TrxAvg", "TrxValue", "TrxMin", "TrxMax",
                "TryJobin", "TryFb", "TryOpt", "TryAvg", "TryValue", "TryMin", "TryMax",
                "ExpxJobin", "ExpxFb", "ExpxOpt", "ExpxAvg", "ExpxValue",
                "ExpyJobin", "ExpyFb", "ExpyOpt", "ExpyAvg", "ExpyValue",
                "OrtJobin", "OrtFb", "OrtOpt", "OrtAvg", "OrtValue",
                "RotJobin", "RotFb", "RotOpt", "RotAvg", "RotValue",
                "SMagJobin", "SMagFb", "SMagOpt", "SMagAvg", "SMagValue",
                "SRotJobin", "SRotFb", "SRotOpt", "SRotAvg", "SRotValue",
                "ASMagJobin", "ASMagFb", "ASMagOpt", "ASMagAvg", "ASMagValue",
                "ASRotJobin",  "ASRotFb", "ASRotOpt", "ASRotAvg", "ASRotValue"  })
                    { dt1.Columns.Add(col); }


                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            newRow = dt1.NewRow();
                            for (int j = 0; j < 53; j++)
                            {
                                if (j == 7)
                                {
                                    newRow[j] = drs[i]["key"].ToString();
                                }
                                else
                                {
                                    newRow[j] = drs[i][j].ToString();
                                }
                            }
                            dt1.Rows.Add(newRow);

                        }
                        DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, "tbl_ovl");
                        myTabInfo.ImportToSqliteBatch(dt1, dbName);
                        // MessageBox.Show("NIKON OVL Update Done");

                        dt = null; dt1 = null;

                        //更新P盘数据
                        try
                        {
                            File.Copy(@"D:\TEMP\DB\ChartRawData.DB", @"p:\_SQLite\ChartRawData.DB", true);
                            MessageBox.Show("P:\\_SQLite\\ChartRawData.DB已被本机文件覆盖");
                        }
                        catch
                        {
                            MessageBox.Show("没有权限读写P:\\_SQLITE目录");
                        }
                    }
                    else
                    {
                        //  MessageBox.Show("无NIKON OVL新数据,未更新");
                    }

                    #endregion
                }
            }

            MessageBox.Show("DONE");
        }
        public static void DeleteObseleteExcelChart()
        {
            string folder;
            string file = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<string> fileList = new List<string>();
            string connStr = "data source=D:\\TEMP\\DB\\ChartRawData.db";

            string sql, latestDate, excelDate;


            //
            string pin = Interaction.InputBox("INPUT PIN:", "PIN", "", -1, -1);
            if (pin.Trim() == "11067305") { }
            else { MessageBox.Show("PIN IS NOT CORRECT,EXIT"); return; }
            //list RAR file
            folder = "P:\\_chartingrawdata";
            fileList = LithoForm.LibF.ExportFileList(folder, fileList);
            fileList.Sort();
            //get latest db date

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DcollTime) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        latestDate = rdr[0].ToString();
                    }
                }
            }


            foreach (string item in fileList)
            {
                if (item.Contains("ChartData.RAR"))
                {
                    file = item;
                    //MessageBox.Show(file);
                }

                //unrar file to c:/temp
                folder = "P:\\_SQLite\\EXE\\haozip\\haozipc e -y " + file + " -oc:\\temp\\";
                LithoForm.LibF.DosCommand(folder);

                //read CD data
                dt = LithoForm.LibF.ReadExcel("C:\\temp\\ChartData.xls", "CD$");

                excelDate = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["Dcoll Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                if (string.Compare(excelDate, latestDate) < 1)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    { }
                }



            }

        }

        public static DataTable cdQueryExcel(string tools, string part, string layer, string date1, string date2, string connStr)
        {
            string sql;
            DataTable dt = new DataTable();
            DataTable dt1;

            // if (tools.Length == 0)
            //  { MessageBox.Show("未选择任何设备，默认全选"); }


            //make sql
            //==tool
            sql = " SELECT * FROM tbl_cd ";
            if (tools.Length == 0 || tools.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql += " WHERE ProcEqID IN  (" + tools + ")"; }
            //==part
            if (part.Trim().Length == 0)
            { }
            else
            {
                string[] partArr = part.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in partArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " PARTID like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " or  PARTID like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "tbl_cd")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }
            //==layer
            if (layer.Trim().Length == 0)
            { }
            else
            {
                string[] layerArr = layer.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in layerArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " layer like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " OR layer like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "tbl_cd")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }
            //==date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "tbl_cd")
            {
                sql += " WHERE (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }
            else
            {
                sql = sql + " AND (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }


            sql = " SELECT A.*,B.MASK,B.VENDOR FROM (" + sql + ") A,(SELECT * FROM TBL_PARTMASK) B WHERE A.PARTID=B.PART";


            if (MessageBox.Show("查询条件是：\r\n\r\n" + sql + "\r\n\r\n\r\n\r\n选择   是(Y)  -->继续查询\r\n\r\n\r\n\r\n选择   否(N)-->  重新定义条件\r\n\r\n\r\n\r\n注意数据源的选择，尽量复制数据至本机查询", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // MessageBox.Show("YES");
            }
            else
            {
                MessageBox.Show("退出，重新定义条件"); return dt;
            }


            MessageBox.Show(sql);
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                            
                        }
                    }
                }
            }

            //合并mask shop
       




            return dt;




        }
        public static DataTable ovlQueryExcel(string tools, string para, string part, string layer, string date1, string date2, string connStr)
        {
            DataTable dt = new DataTable();
            string sql;
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }



            //make sql
            sql = " SELECT * FROM TBL_OVL ";
            //==tool
            if (tools.Length == 0 || tools.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql += " WHERE PROCEQPID IN  (" + tools + ")"; }
            //==parameter


            //==part
            if (part.Trim().Length == 0)
            { }
            else
            {
                string[] partArr = part.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in partArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " PARTID like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " or PARTID like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }
            //==layer
            if (layer.Trim().Length == 0)
            { }
            else
            {
                string[] layerArr = layer.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in layerArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " layer like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " or layer like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }
            //==date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
            {
                sql += " WHERE (DCOLLTIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }
            else
            {
                sql = sql + " AND (DCOLLTIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }

            sql += " ORDER BY DCOLLTIME";

            if (MessageBox.Show("查询条件是：\r\n\r\n" + sql + "\r\n\r\n\r\n\r\n选择   是(Y)  -->继续查询\r\n\r\n\r\n\r\n选择   否(N)-->  重新定义条件\r\n\r\n\r\n\r\n注意数据源的选择，尽量复制数据至本机查询", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // MessageBox.Show("YES");
            }
            else
            {
                MessageBox.Show("退出，重新定义条件"); return dt;
            }


            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            int rowsCount = dt.Rows.Count;


            return dt;
        }
        public static DataTable qcQueryExcelForCd(string date1, string date2, string connStr)
        {
            string sql;
            sql = "select substr(Layer,1,2) LayerID,*  from tbl_cd where ( partid like '2LXXXSCD%' and layer like 'A1%') or ((partid like '2LXXXUCD%' or partid like '2LXXXVCD%') and layer='GT') or partid='2LFOUSCD02' AND (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }


            return dt;

        }
        public static DataTable qcQueryExcelForOverlay(string date1, string date2, string connStr)
        {
            string sql;
            sql = "select substr(Layer,1,2) LayerID,*  from tbl_ovl where  " +
                "    partid like '2LXXXSPM%'  " +
                " or partid like '2LXXXFIA%' " +
                " or partid like '2LXXXLSA%' " +
                " AND (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }


            return dt;

        }
        public static DataTable asmlProductQueryExcelForOverlay(string date1,string date2,string connStr)
        {
          string sql;
        sql = "select *   from tbl_ovl where  " +
                "  substr(ProcEqId,3,1)='D'  " +
                "  " +
                " " +
                " AND (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }


            return dt;
        }
        public static DataTable nikonFiaLsaProductQueryExcelForOverlay(string date1, string date2, string connStr,string ega)
        {
            string sql;
            sql = "select PartID||'.'||Layer as ppid,*  from tbl_ovl where  " +
                    "  substr(ProcEqId,3,1)<>'D'  " +
                   "  and PartID||'.'||Layer in ( select ppid from tbl_ega where ega='"+ega +"' and ppid is not null) " +
                    " " +
                    " AND (DcollTime BETWEEN '" + date1 + "' and  '" + date2 + "')";

           // sql = "Select * from (" +sql+ ") a where a.ppid in (select ppid from tbl_ega where ega='FIA' and ppid is not null) ";

            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }


            return dt;
        }

        
    }
}
