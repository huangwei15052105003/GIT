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
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Data.OleDb;

/// <summary>
/// https://bbs.csdn.net/topics/380053920  datagridview  Ctr+C 乱码
/// </summary>

namespace LithoForm
{


    public partial class Form1 : Form
    {
        // display form to process datagridview1

        #region public fields
        public delegate void Form2Delegate(string[] passData);
        public event Form2Delegate Form2Trigger;

        public delegate void Form3Delegate(DataTable dt);
        public event Form3Delegate Form3Trigger;
        public List<string> list = new List<string>();
        public string connStr;


        DataTable dtShow; //通用DataGridView DataSourece
        DataTable dt1; //filter
        DataTable dt2; //R2R jobin

        public List<DataTable> listDatatable = new List<DataTable>(); //矢量图，WQ，MCC等
        public Dictionary<string, DataTable[]> myDic;
        string sql;

        static float zoom = 1f; //矢量放大
        static float zoom1 = 10f;//wafer quality 画圆
        #endregion

        #region  //clear memory
        ///https://www.cnblogs.com/mq0036/p/3707257.html
        ///Clear Memory
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public void ClearMemory()
        {
            GC.Collect();
            GC.SuppressFinalize(this);


            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion


        public Form1()
        {
            InitializeComponent();
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.Value = DateTime.Now.AddDays(1);
            this.tabControl1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create c:/temp directory, otherwise GDI+ error will be triggered
            if (!Directory.Exists("C:\\TEMP"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("C:\\TEMP");
                directoryInfo.Create();
            }
            chart1.Visible = false;

            panel6.Visible = false;
            LithoForm.LibF.DosCommand(@"net use P:\_SQLite p:");


        }

        #region HELP
        private void toolStripMenuItem2_Click(object sender, EventArgs e) //主要变更说明
        {
            dtShow = null; dtShow = classHelp.changeDescription();
            dataGridView1.DataSource = dtShow;
            ShowGridview();

        }

        private void 数据保存位置说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = classHelp.savePathDescription();
            dataGridView1.DataSource = dtShow;
            ShowGridview();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e) //手动复制单个文件
        {
            if (classHelp.CopyToLocalDisk())
            { MessageBox.Show(" File Is Copied"); }
            else { MessageBox.Show("File Is Not Copied"); }
        }
        private void 自动复制数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classHelp.AutoCopyToLocalDisk())
            {
                MessageBox.Show("All Files Are Copied");
            }
            else
                MessageBox.Show("Files Are Not All Copied");
        }
        private void 获取表名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            dtShow = classHelp.GetTableFieldName();
            dataGridView1.DataSource = dtShow;
        }
        private void 选择目录备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            classHelp.BackupFolder();
        }
        private void 运行待完成ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            classHelp.Backup_10_4_72_150();
        }
        private void 复制LithoFormExposureRecipeFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            classHelp.DuplicateExeFiles();
        }
        private void 复制ReworkMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmAweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"p:\_SQLite\AsmlAwe.DB", @"D:\TEMP\DB\AsmlAwe.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmlBatchReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\AsmlBatchreport.DB", @"D:\TEMP\DB\AsmlBatchreport.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmlJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\AsmlJobinStation.DB", @"D:\TEMP\DB\AsmlJobinStation.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制cDRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制FlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\Flow.DB", @"D:\TEMP\DB\Flow.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制NikonEgaParaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制NikonJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\NikonJobinStation.DB", @"D:\TEMP\DB\NikonJobinStation.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制R2RCDOVLCDCONFIGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\R2R.DB", @"D:\TEMP\DB\R2R.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制ChartRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\ChartRawData.db", @"D:\TEMP\DB\ChartRawData.DB", true);

            MessageBox.Show("复制完成");
        }
        #endregion

        #region Maintain
        private void 流程查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (classMaintain.QueryFlow(ref dtShow))
            { if (dtShow.Rows.Count > 0) { dataGridView1.DataSource = dtShow; } }
        }
        private void 删除CDSEMRECIPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            classMaintain.DeleteCdSemRecipe(ref dtShow);
        }
        private void opasSqlWipJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            classMaintain.MakeSqlForQueryJobinStationByWip(ref dtShow);
        }
        private void opasSqlCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            classMaintain.MakeSqlForQueryCD(ref dtShow);
        }
        private void nikon对位方式更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            #region list LSA/FIA
            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                sql = " Select * from (SELECT DISTINCT PPID, EGA  FROM tbl_index WHERE EGA = 'FIA' union SELECT DISTINCT PPID, EGA  FROM tbl_index WHERE EGA = 'LSA') a ";
                sql += " where a.ppid not in ( Select ppid from (Select count(ppid) as count, ppid from(SELECT DISTINCT PPID, EGA  FROM tbl_index) group by ppid) where count>1 and ppid is not null)";


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


            #endregion


            string dbName = string.Empty;
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; dbName = @"D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; dbName = @"P:\_SQLite\ChartRawData.DB"; }
            //删除数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM tbl_ega";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, "tbl_ega");
            myTabInfo.ImportToSqliteBatch(dtShow, dbName);
            MessageBox.Show("Nikon Align Method Done");

            dataGridView1.DataSource = dtShow;

        }

        private void seqLogFileNameToolStripMenuItem_Click(object sender, EventArgs e) //文件名重命名
        {
            string path = @"\\10.4.50.16\photo$\ppcs\seqlog\";
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04", "ALII05", "ALII06", "ALII07",
           "ALII08", "ALII09", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14",
           "ALII15", "ALII16", "ALII17", "ALII18", "BLII20", "BLII21", "BLII22","BLII23"};
            string folder;
            Regex regex = new Regex(@"^_\d{4}-\d{2}-\d{2}_\d{6}");
            Computer MyComputer = new Computer();//VB.NET重命名文件

            foreach (string tool in tools)
            {
                folder = path + tool + "\\";
                List<string> list = new List<string>();
                string[] files = Directory.GetFiles(folder);

                string str1, str2;
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Name.Length != 36) //需改用正则匹配
                    {
                        str1 = ""; str2 = "";
                        string[] lines = File.ReadAllLines(fileInfo.FullName);
                        for (int i = 0; i < lines.Length; ++i)
                        {
                            string tmp = "_" + lines[i].Trim().Replace(" ", "_").Replace(":", "");
                            // MessageBox.Show(tmp);
                            if (regex.IsMatch(tmp))
                            { str1 = tmp.Substring(0, 18); break; }
                            if (tmp.StartsWith("_Logging_start_at_"))
                            { str1 = tmp.Substring(17, 18); break; }

                        }
                        for (int i = lines.Length - 1; i > 0; --i)
                        {
                            string tmp = "_" + lines[i].Trim().Replace(" ", "_").Replace(":", "");
                            //MessageBox.Show(tmp);
                            if (regex.IsMatch(tmp))
                            { str2 = tmp.Substring(1, 17); break; }

                            if (tmp.StartsWith("_Logging_end_at_"))
                            { str2 = tmp.Substring(16, 17); break; }
                        }
                        if (str1 != "" && str2 != "")
                        {
                            string newName = str1 + "#" + str2;
                            try
                            { MyComputer.FileSystem.RenameFile(fileInfo.FullName, newName); }
                            catch
                            { File.Copy(fileInfo.FullName, fileInfo.DirectoryName + "\\" + newName, true); }
                        }
                        else
                        {
                            string newName = "_0" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                            MyComputer.FileSystem.RenameFile(fileInfo.FullName, newName);
                        }
                    }




                }













            }
        }
        private void reworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rework.ManualUpdateReworkDB();
        }
        private void chartingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            string folder;
            string file = string.Empty;
            DataTable dt;
            DataTable dt1;
            List<string> fileList = new List<string>();
            string connStr = "data source=D:\\TEMP\\DB\\ChartRawData.db";
            string dbName = "D:\\TEMP\\DB\\ChartRawData.db";
            string sql, latestDate;
            DataRow[] drs;
            DataRow newRow;

            //list RAR file
            folder = "P:\\_chartingrawdata";
            fileList = TaskExe.Share.ExportFileList(folder, fileList);
            fileList.Sort();
            foreach (string item in fileList)
            {
                if (item.Contains("ChartData.RAR"))
                {
                    file = item;
                }
                //unrar file to c:/temp
                folder = "P:\\_SQLite\\EXE\\haozip\\haozipc e -y " + file + " -oc:\\temp\\";
                TaskExe.Share.DosCommand(folder);
                #region //cd
                //read CD data
                dt = TaskExe.Share.ReadExcel("C:\\temp\\ChartData.xls", "CD$");
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
                    //MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出"); 
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
                      TaskExe.DataTableToSqlite myTabInfo = new TaskExe.DataTableToSqlite(dt1, "tbl_cd");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                   MessageBox.Show("CD Update Done");

                    dt = null; dt1 = null;
                }
                else
                {MessageBox.Show("无CD新数据,未更新"); }

                #endregion

                #region //asml ovl
                //read  data
                dt = TaskExe.Share.ReadExcel("C:\\temp\\ChartData.xls", "OL_ASML$");
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
                   MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出");
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
                    TaskExe.DataTableToSqlite myTabInfo = new TaskExe.DataTableToSqlite(dt1, "tbl_ovl");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                   MessageBox.Show("ASML OVL Update Done");

                    dt = null; dt1 = null;
                }
                else
                {
                   MessageBox.Show("无ASML OVL新数据,未更新");
                }

                #endregion

                #region //Nikon ovl
                //read  data
                dt = TaskExe.Share.ReadExcel("C:\\temp\\ChartData.xls", "OL_NIKON$");
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




                if (string.Compare(dt.Rows[1]["key"].ToString(), latestDate) == 1)
                {
                   MessageBox.Show("新数据和数据库数据无日期交叠，无法更新数据，退出");
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
                    TaskExe.DataTableToSqlite myTabInfo = new TaskExe.DataTableToSqlite(dt1, "tbl_ovl");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                    //MessageBox.Show("NIKON OVL Update Done");

                    dt = null; dt1 = null;

                    //更新P盘数据

                }
                else
                {
                    // MessageBox.Show("无NIKON OVL新数据,未更新");
                }

                #endregion
            }
            File.Copy("D:\\TEMP\\DB\\ChartRawData.db", "P:\\_SQLite\\ChartRawData.db",true);
           MessageBox.Show("===DONE====\n\n"+Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString()+ " Seconds");
        }
        private void trackRecipeEsfRewokMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            string connStr = @"data source=P:\_SQLite\ReworkMove.DB";

           MessageBox.Show("Update ESF , Track Recipe, InlineWip");
            string sql, sql1, sql2, sql3, sql4; DataTable dt; TaskExe.DataTableToSqlite myTabInfo; string dbTblName;
            #region
            //===ESF
            sql = " select PARTTITLE TECH, PART, STAGE, RECIPE, PPID, EQPID, FLAG Yes1No0, TYPE 反向1正向0, ACTIVEFLAG, FAILREASON, EXPIRETIME, CREATEUSER, USERDEPT, CREATETIME, REQUESTUSER  from rptprd.processconstraint A where ACTIVEFLAG='Y' and (substr(eqpid,1,4) in('ALCT', 'BLCT', 'ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI', 'ALTI', 'BLTI', 'ALTD', 'BLTD','ALSD','BLSD','ALCD','BLCD','ALOL','BLOL') ) order by eqpid,parttitle,part,stage";
            dt = TaskExe.Share.CsmcOracle(sql);
            //清空数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE FROM tbl_esf";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            dbTblName = "tbl_esf";
            myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
           MessageBox.Show("ESF UPDATE DONE");

            //ESF for CHECk
            sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE,FAILREASON from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";
            sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE,FAILREASON FROM (" + sql + ")";
            dt = TaskExe.Share.CsmcOracle(sql);
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE FROM tbl_esf1";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            dbTblName = "tbl_esf1";
            myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
           MessageBox.Show("ESF UPDATE DONE (For Check)");
            #endregion

            #region //trackrecipe
            //Nikon offline 流程
            sql1 = "select RECPID,DECODE(EQPTYPE,'LCT','NIKON') AS TOOLTYPE,    STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,PPID  from  RPTPRD.MFG_VIEW_FLOW ";
            sql1 += " where eqptype='LCT' and substr(partid,1,1) not in ('8','2','1') ";// order by part";


            sql2 = "select STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,substr(ppid,instr(ppid,'.')+1,2) LAYER,Mask  from  RPTPRD.MFG_VIEW_FLOW WHERE EQPTYPE='LSI'";
            sql3 = "select a.tooltype,a.stage,a.part,a.ppid trackrecipe,b.layer,b.mask from (" + sql1 + ") a,(" + sql2 + ") b where a.part=b.part and a.stage=b.stage";


            //inline flow
            sql4 = "select DECODE(EQPTYPE,'LDI','ASML','LII','NIKON') AS TOOLTYPE,    STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,substr(PPID,1,instr(ppid,';')-1) TrackRecipe,substr(ppid,instr(ppid,'.')+1,2) LAYER,mask from  RPTPRD.MFG_VIEW_FLOW ";
            sql4 += " where eqptype in ('LDI','LII') and substr(partid,1,1) not in ('8','2','1')";// order by part";





            sql = "Select distinct * from ( " + sql4 + " union (" + sql3 + ") )   ORDER BY TOOLTYPE,PART ";
            sql = "select tooltype,stage,part,trackrecipe,layer,mask,substr(trackrecipe,1,3) shorttrackrecipe from (" + sql + ") where substr(trackrecipe,1,2) not in ('A0','S0','T1') order by part,layer";



            connStr = "data source=P:\\_SQLite\\ReworkMove.db";
            dbTblName = "tbl_flowtrack";

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            //get data from oracle
            dt = TaskExe.Share.CsmcOracle(sql);
            //save to db
            myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");
           MessageBox.Show("PART & TRACK RECIPE  UPDATE DONE");
            #endregion

            #region //inline wip


            sql = " Select distinct partname part from rptprd.sdb_view_info_wip";
            sql += " where substr(lottype,1,1) in ('M','N','P','E')";
            sql += "  and STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')";
            sql += " and location not like '%bank'";
            sql += " and substr(partname,1,1) not in ('1','2','8')";

            sql = " select DISTINCT PARTID part from rptprd.actl@mfgprd   WHERE comclass = 'WIP'";

            //wip
            dbTblName = "tbl_realwip";
            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            dt = TaskExe.Share.CsmcOracle(sql);
            //save to db
            myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");

           MessageBox.Show("INLINE WIP UPDATE DONE");
            #endregion

            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");



        }
        private void techCodeReworkMoveToolStripMenuItem_Click(object sender, EventArgs e)

        {
            Stopwatch sw = new Stopwatch(); sw.Start();
            string connStr, sql;
            DataTable dtShow;

            #region refresh tbl_tech
            connStr = "data source=P:\\_SQLite\\ReworkMove.db";
            string dbTblName = "tbl_tech";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            sql = "select productcode2 part,processcode tech,processtype,processgroup,dienum,chipsizex,chipsizey,partavailible,last_update_time from  rptprd.part_checklist";
            dtShow = TaskExe.Share.CsmcOracle(sql);
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, "P:\\_SQLite\\ReworkMove.db");
            dtShow = null;
            #endregion

            #region get latest techcode that is not null and overwrite it 
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT PART as PART,TECH as TECH FROM " + dbTblName + " WHERE TECH IS NOT NULL";
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
            dtShow.Columns.Add("SOURCE");
            for (int i = 0; i < dtShow.Rows.Count; i++) { dtShow.Rows[i]["SOURCE"] = "Oracle"; }
            //===================
            dbTblName = "tbl_techFull";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName + " " +
                    "WHERE PART IN ( SELECT DISTINCT PART  FROM tbl_tech WHERE TECH IS NOT NULL)";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, "P:\\_SQLite\\ReworkMove.db");
            dtShow = null;
            #endregion

            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");

        }
        private void partVsMaskReworkMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch(); sw.Start();

            string sql;
            string connStr;
            DataTable dt1, dtShow, dt2;
            sql = " select distinct substr(partid,1,length(partid)-3) PART ,substr(mask,1,4) MASK from RPTPRD.MFG_VIEW_FLOW where mask is not null and regexp_like(substr(mask,1,4),'^[0-9]+$') order by mask";
            dt1 = TaskExe.Share.CsmcOracle(sql);

            // MessageBox.Show(dt1.Rows.Count.ToString());

            connStr = @"data source=P:\_SQLite\ReworkMove.db";

            sql = "select distinct substr(mask_mpw,1,4) MASK,maskshop VENDOR  from tbl_mask where length(maskshop)>1";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt2 = ds.Tables[0];
                        }
                    }
                }
            }
            //MessageBox.Show(dt2.Rows.Count.ToString());

            var query =
          from rHead in dt1.AsEnumerable()
          join rTail in dt2.AsEnumerable()

         on new { a = rHead.Field<string>("MASK") } equals new { a = rTail.Field<string>("MASK") }

          select rHead.ItemArray.Concat(rTail.ItemArray.Skip(1));

            dtShow = dt1.Clone();
            dtShow.Columns.Add("VENDOR");

            foreach (var obj in query)
            {
                DataRow dr = dtShow.NewRow();
                dr.ItemArray = obj.ToArray();
                dtShow.Rows.Add(dr);
            }

            dt1 = null; dt2 = null;


            //删除旧数据
            string dbTblName = "tbl_partmask";
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
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\ReworkMove.DB");

            //更新 ChartRawData.DB
            connStr = @"data source=D:\TEMP\DB\ChartRawData.db";


            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);

            myTabInfo.ImportToSqliteBatch(dtShow, @"D:\TEMP\DB\ChartRawData.db");
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private static void UpdateWithCsvFile(string connStr, string filePath, string dbTblName, string notes, bool timeCheckFlag = false)
        {
            DataTable dt;
            string sql;
            string dbPath = connStr.Split(new char[] { '=' })[1];
            DateTime tmpDate = DateTime.Now;

            if (timeCheckFlag == true)
            {
                //列出最新日期，
                string strDate;
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    sql = "SELECT max(DCOLL_TIME) FROM " + dbTblName;//first record select English from graduate_phrase limit 0,1
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
                DataTable dtTmp = TaskExe.Share.OpenCsvWithComma(filePath);
                if (string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate) == 1)
                {MessageBox.Show("新数据和数据库数据无日期交叠，请选择更早的日期下载数据:" + dbTblName); return; }
                //filter data
                DataRow[] drs = dtTmp.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
                dt = dtTmp.Clone();//Clone 复制结构，Copy 复制结构和数据
                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        dt.ImportRow(drs[i]);
                    }
                }
                else
                {
                   MessageBox.Show("无新数据，请从OPAS重新下载数据");
                }

            }

            else
            {
                dt = TaskExe.Share.OpenCsvWithComma(filePath);
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

            }

            //导入最新数据
            TaskExe.DataTableToSqlite myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, dbPath);
           MessageBox.Show(notes);

        }
        private void maskInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            UpdateWithCsvFile(@"data source=P:\_SQLite\ReworkMove.DB", "P:\\_SQLite\\OPAS\\MaskInfo.csv", "tbl_mask", "ReworkMove.DB Mask Information Update Done", false);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void pSQLiteOPASr2rCdConfigcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\r2rCdConfig.csv", "tbl_config", "R2R.DB CD CONFIG UPDATE DONE", false);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void pSQLiteOPASr2rCdConfigcsvToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            UpdateWithCsvFile("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\OPAS\\r2rCdConfig.csv", "tbl_cdconfig", "ReworkMove.DB CD CONFIG UPDATE DONE", false);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void pSQLiteOPASCDcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\CD.csv", "tbl_cd", "R2R.DB CD UPDATE DONE", true);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void pSQLiteOPASOVLcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\OVL.csv", "tbl_ovl", "R2R.DB OVL UPDATE DONE", true);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private static void DeleteUpdateDB(string connStr, string dbPath, string dbTblName, string sql, string notes)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            DataTable dtShow;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }


            dtShow = TaskExe.Share.CsmcOracle(sql);
            TaskExe.DataTableToSqlite myTabInfo = new TaskExe.DataTableToSqlite(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, dbPath);

           MessageBox.Show(notes);
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void mfgOracleFlowRelatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            //part tech stage
            DeleteUpdateDB("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\ReworkMove.db", "xls_partTechStage",
                 "select  TECHNOLOGY,substr(PARTID,1,length(PARTID)-3) as PARTID,STAGE,EQPTYPE      from RPTPRD.MFG_VIEW_FLOW  Where( ( EQPTYPE like '%'||'LDI'||'%' or  EQPTYPE like '%'||'LII'||'%' or EQPTYPE like '%'||'LSI'||'%' ) and STAGE like '%'||'PH'||'%' )", "Part Stage Update Done");

            //ovl ppid
            DeleteUpdateDB("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\ReworkMove.db", "xls_ovlPpid",
                "select  substr(PARTID,1,length(PARTID)-3) as PARTID, STAGE,PPID from  RPTPRD.MFG_VIEW_FLOW Where ( EQPTYPE like '%'||'LOL'||'%' and STAGE like '%'||'PH'||'%' )", "OVL PPID UPDATE DONE");

            //cd ppid
            DeleteUpdateDB("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\ReworkMove.db", "xls_cdPpid",
                "select  substr(PARTID,1,length(PARTID)-3) as PARTID, STAGE,PPID from  RPTPRD.MFG_VIEW_FLOW Where ( EQPTYPE like '%'||'LCD'||'%' and STAGE like '%'||'PH'||'%' )", "CD PPID UPDATE DONE");

            // nikon Part ,part in ppid 
            DeleteUpdateDB("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\ReworkMove.db", "xls_nikonPart",
             "select distinct * from (select  substr(PARTID,1,length(partid)-3) PART ,  substr(PPID,1,instr(ppid,'.')-1) CallPart from  RPTPRD.MFG_VIEW_FLOW Where ( EQPTYPE like '%'||'LSI'||'%'))", "NIKON PART PPID DONE");
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        public static void EsfToolAvailableFromLithoWip()
        {
            DataTable dt1, dt2, dt3;
            try
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                string connStr;

                string sql;
                DataRow[] drs;
                DataTable asmlWip, nikonWip;//WIP
                DataTable asmlPart, asmlTech, nikonPart, nikonTech;//ESF
                string[] asmlTools = new string[] { "ALDI02", "ALDI03", "ALDI05", "ALDI06", "ALDI07", "ALDI09", "ALDI10", "ALDI11", "ALDI12", "BLDI08", "BLDI13" };
                string[] nikonTools = new string[] { "ALII01", "ALII02", "ALII03", "ALII04", "ALII05", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14", "ALII15", "ALII16", "ALII17", "ALII18", "ALSIB6", "ALSIB7", "ALSIB8", "ALSIB9", "BLSIBK", "BLSIBL", "BLSIE1", "BLSIE2" };
                bool techFlag = false;
                string partStr;
                #region 抽取数据准备
                ///extract data
                ///sql = "  SELECT a.TECHNOLOGY, a.PARTID, a.LOTID, a.STATUS,a.EQPID,a.PPID, a.STAGE,a.EQPTYPE,a.LOTTYPE,a.PRIORITY,b.MFG_PRIORITY ,a.QTY,a.LASTDAY_TR,a.TR,a.RTIME, a.HTIME, a.QTIME, a.HOLDCOMMENT ";
                /// sql += " FROM RPTPRD.sdb_view_info_wip a, rptprd.mfg_tbl_internal_priority b ";
                /// sql += " WHERE  a.lotid=b.lotid(+) and (a.LOTTYPE Not In ('CM','MW','ZZ')) AND (a.STATUS Not In ('SCHED','COMPLT','FINISH','LHLD','TRAN'))   AND (a.LOTID Not Like 'F%') and (a.stage like '%-PH' or a.holdcomment like '%PHPE%') ORDER BY a.EQPTYPE,a.STAGE";
                /// 以上是MFGSQL,来自MFG EXCEL的WIP，简化如下
                /// ==========
                ///在线WIP， 字段  PART,RECIPE,STAGE,EQPTYPE,PPID,WFRQTY,LOTQTY
                sql = " SELECT  SUBSTR(PARTID,1,LENGTH(PARTID)-3) PART,SUBSTR(RECIPE,1,5) RECIPE,  STAGE,EQPTYPE,PPID,SUM( QTY)   WFRQTY,COUNT(QTY) LOTQTY FROM RPTPRD.sdb_view_info_wip WHERE   (LOTTYPE Not In ('CM','MW','ZZ')) AND (STATUS  In ('WAIT','HELD')) and STAGE like '%-PH' and EQPTYPE in ('LDI','LII','LSI') GROUP BY SUBSTR(PARTID,1,LENGTH(PARTID)-3),SUBSTR(RECIPE,1,5),RECIPE,  STAGE,EQPTYPE,PPID ORDER BY EQPTYPE,STAGE";

                dt1 = TaskExe.Share.CsmcOracle(sql);

                ///=============
                ///查询Constrain，并将其转化为正则表达式
                ///字段 TECH，PART，RECIPE，STAGE，EQPID，FLAG，TYPE，EQPTYPE （EQPID第三位）
                sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

                sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE FROM (" + sql + ")";

                dt2 = TaskExe.Share.CsmcOracle(sql);

                ///==========
                ///查询工艺代码，需要完善，部分产品缺工艺代码
                ///需要在ReworkMove.DB 建一个完整的表
                connStr = "data source=" + "P:\\_SQLite\\ReworkMove.db";
                sql = "SELECT distinct PART, TECH  FROM TBL_TECHFULL WHERE TECH IS NOT NULL";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds); dt3 = ds.Tables[0];
                            }
                        }
                    }
                }

                ///dt1:WIP   dt2:ESF, dt3:TECH
                ///为WIP并入TECH
                var query =
                       from t1 in dt1.AsEnumerable()
                       from t2 in dt3.AsEnumerable()
                       where t1.Field<string>("PART") == t2.Field<string>("PART")
                       select new
                       {
                           TECH = t2.Field<string>("TECH"),
                           PART = t1.Field<string>("PART"),
                           RECIPE = t1.Field<string>("RECIPE"),
                           STAGE = t1.Field<string>("STAGE"),
                           EQPTYPE = t1.Field<string>("EQPTYPE"),
                           PPID = t1.Field<string>("PPID"),
                           WFRQTY = t1.Field<decimal>("WFRQTY"),
                           LOTQTY = t1.Field<decimal>("LOTQTY")
                       };


                dt1 = TaskExe.Share.LINQToDataTable(query);
                ///分出NikonWIP，AsmlWIP
                drs = dt1.Select("EQPTYPE = 'LDI' ");
                asmlWip = dt1.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlWip.ImportRow(drs[i]);

                }

                drs = dt1.Select("EQPTYPE <> 'LDI' ");
                nikonWip = dt1.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonWip.ImportRow(drs[i]);

                }


                ///ESF 分类
                drs = dt2.Select("TYPE = '0' and EQPTYPE = 'D' ");

                asmlPart = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlPart.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '1' and EQPTYPE = 'D' ");

                asmlTech = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlTech.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '0' and EQPTYPE <> 'D' ");
                nikonPart = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonPart.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '1' and EQPTYPE <> 'D' ");
                nikonTech = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonTech.ImportRow(drs[i]);

                }

                dt1 = null; dt2 = null; dt3 = null;


                //MessageBox.Show(asmlWip.Rows.Count.ToString());
                //MessageBox.Show(nikonWip.Rows.Count.ToString());
                //MessageBox.Show(asmlPart.Rows.Count.ToString());
                //MessageBox.Show(asmlTech.Rows.Count.ToString());
                //MessageBox.Show(nikonPart.Rows.Count.ToString());
                //MessageBox.Show(nikonTech.Rows.Count.ToString());
                #endregion

                #region 比较开始
                dt1 = new DataTable();
                dt1.Columns.Add("TECH", Type.GetType("System.String"));
                dt1.Columns.Add("PART", Type.GetType("System.String"));
                dt1.Columns.Add("RECIPE", Type.GetType("System.String"));
                dt1.Columns.Add("STAGE", Type.GetType("System.String"));
                dt1.Columns.Add("EQPTYPE", Type.GetType("System.String"));
                dt1.Columns.Add("WFRQTY", Type.GetType("System.Double"));
                dt1.Columns.Add("LOTQTY", Type.GetType("System.Double"));
                dt1.Columns.Add("TOOL", Type.GetType("System.String"));
                #endregion


                #region Asml开始比较

                foreach (string tool in asmlTools)
                {
                    DataTable tblLoop;
                    tblLoop = null;
                    tblLoop = asmlTech.Clone();

                    //列出单个设备反向
                    drs = asmlTech.Select("EQPID = '" + tool + "'");


                    for (int i = 0; i < drs.Length; i++)
                    { tblLoop.ImportRow(drs[i]); }

                    foreach (DataRow x in asmlWip.Rows)
                    {
                        if (x["TECH"].ToString().Length > 1)
                        {



                            techFlag = false;
                            //反向判断，techFlag=true,无法作业
                            #region
                            foreach (DataRow y in tblLoop.Rows)
                            {
                                //  if (tool == "ALDI03" && x["RECIPE"].ToString() == "LDIGT"  && x["TECH"].ToString() == "C1813D5S0014" && x["PART"].ToString() == "WQ4061CJ-L"   && x["STAGE"].ToString() == "PO1-PH" && y["TECH"].ToString() == "^[A-Z0-9]{1}1\\S*$" && y["PART"].ToString() == "^$"  && y["STAGE"].ToString() == "^PO1-PH$")
                                //   {
                                if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                {
                                    // MessageBox.Show(x["TECH"].ToString());
                                    // MessageBox.Show(y["TECH"].ToString());
                                    // MessageBox.Show(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());



                                    if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                    {
                                        if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                        {
                                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                            {
                                                //到此一步，全部为True，有限制不能作业
                                                techFlag = true;

                                            }
                                        }
                                    }

                                }
                                //  }

                            }
                            #endregion
                            //正向
                            #region
                            if (techFlag == true)
                            {
                                //有反向限制不能作业
                            }
                            else
                            {
                                //判断是否有正向限制，针对所有设备，1）0台设备，可作业 2）多台设备，判断是否包含本设备
                                partStr = "";
                                foreach (DataRow y in asmlPart.Rows)
                                {
                                    if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                    {
                                        if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                        {
                                            if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                            {
                                                if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                                {
                                                    //到此一步，全部为True，有限制不能作业
                                                    partStr += y["EQPID"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                //以上正向判断结束；parStr长度为0，可以作业；不为0，包含设备名，才可作业
                                if (partStr.Length == 0 || partStr.Contains(tool))
                                {
                                    //可作业
                                    DataRow newRow = dt1.NewRow();
                                    newRow["TECH"] = x["TECH"].ToString();
                                    newRow["PART"] = x["PART"].ToString();
                                    newRow["RECIPE"] = x["RECIPE"].ToString();
                                    newRow["STAGE"] = x["STAGE"].ToString();
                                    newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                    newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                    newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                    newRow["TOOL"] = tool;
                                    dt1.Rows.Add(newRow);

                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //短流程无工艺代码，不判断
                        }

                    }

                }

                #endregion


                #region Nikon 开始比较
                foreach (string tool in nikonTools)
                {



                    DataTable tblLoop;
                    tblLoop = null;
                    tblLoop = nikonTech.Clone();

                    //列出单个设备反向
                    drs = nikonTech.Select("EQPID = '" + tool + "'");


                    for (int i = 0; i < drs.Length; i++)
                    { tblLoop.ImportRow(drs[i]); }

                    foreach (DataRow x in nikonWip.Rows)
                    {
                        if (x["TECH"].ToString().Length > 1)
                        {




                            //   if (tool == "ALDI03" && x["RECIPE"].ToString() == "LDIGT"
                            //      && x["TECH"].ToString()=="C1813D5S0014"
                            //    && x["PART"].ToString()=="WQ4061CJ-L"
                            //   && x["STAGE"].ToString()=="PO1-PH"
                            //  && y["TECH"].ToString()=="^[A-Z0-9]{1}1\\S*$"
                            //  && y["PART"].ToString()=="^$"
                            // && y["STAGE"].ToString()=="^PO1-PH$")
                            //   {
                            //    MessageBox.Show(x["TECH"].ToString() + "," + x["PART"].ToString() + "," + x["STAGE"].ToString() + "," + x["RECIPE"].ToString());


                            // MessageBox.Show(Regex.IsMatch("H5012","^[A-Z0-9]{1}1\\S*$").ToString());




                            techFlag = false;
                            //反向判断，techFlag=true,无法作业
                            #region
                            foreach (DataRow y in tblLoop.Rows)
                            {
                                if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                {

                                    if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                    {
                                        if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                        {
                                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                            {
                                                //到此一步，全部为True，有限制不能作业
                                                techFlag = true;

                                            }
                                        }
                                    }
                                }
                                //  MessageBox.Show(y["TECH"].ToString() + "," + y["PART"].ToString() + "," + y["STAGE"].ToString() + "," + y["RECIPE"].ToString());
                                //   MessageBox.Show(y["TECH"].ToString() + ",   " + y["PART"].ToString());
                                //  MessageBox.Show(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());
                                //MessageBox.Show(Regex.IsMatch(x["PART"].ToString(), y["TECH"].ToString()).ToString());
                                //MessageBox.Show(Regex.IsMatch(x["RECIPE"].ToString(), y["TECH"].ToString()).ToString());
                                // MessageBox.Show(Regex.IsMatch(x["STAGE"].ToString(), y["TECH"].ToString()).ToString());
                                //    MessageBox.Show("SDFSDFSDF     " + techFlag.ToString());
                                //  }




                            }



                            #endregion
                            //正向
                            #region
                            if (techFlag == true)
                            {
                                //有反向限制不能作业
                            }
                            else
                            {
                                //判断是否有正向限制，针对所有设备，1）0台设备，可作业 2）多台设备，判断是否包含本设备
                                partStr = "";
                                foreach (DataRow y in nikonPart.Rows)
                                {
                                    if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                    {
                                        if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                        {
                                            if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                            {
                                                if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                                {
                                                    //到此一步，全部为True，有限制不能作业
                                                    partStr += y["EQPID"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                //以上正向判断结束；parStr长度为0，可以作业；不为0，包含设备名，才可作业
                                if (partStr.Length == 0 || partStr.Contains(tool))
                                {
                                    //可作业
                                    DataRow newRow = dt1.NewRow();
                                    newRow["TECH"] = x["TECH"].ToString();
                                    newRow["PART"] = x["PART"].ToString();
                                    newRow["RECIPE"] = x["RECIPE"].ToString();
                                    newRow["STAGE"] = x["STAGE"].ToString();
                                    newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                    newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                    newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                    newRow["TOOL"] = tool;
                                    dt1.Rows.Add(newRow);

                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //短流程无工艺代码，不判断
                        }
                    }








                }

                #endregion


                dt1.Columns.Add("RefreshDate");
                foreach (DataRow row in dt1.Rows)
                {
                    row["RefreshDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }



                ///保存到数据库
                connStr = "data source=P:\\_SQLite\\ReworkMove.db";
                string dbTblName = "tbl_esfToolAvailable";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string tmpSql = "DELETE  FROM " + dbTblName;
                    using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }

                DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, dbTblName);
                myTabInfo.ImportToSqliteBatch(dt1, "P:\\_SQLite\\ReworkMove.db");



                stopwatch.Stop();

                // MessageBox.Show("用时" + stopwatch.ElapsedMilliseconds.ToString() + " msec\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n另短流程（-RD）无法从OPAS R2R导出完整工艺代码，不判断");
               MessageBox.Show("工艺代码来自：\n\n" +
                    "   MFG Oracle数据库\n" +
                    "   LiYan/RenY编程数据\n" +
                    "   因各种原因，部分产品可能缺工艺代码，无法判断\n\n" +
                    "当前WIP来自MFG Oracle DB，实时性待确认\n\n" +
                    "表格最后一栏系数据刷新时间");

            }
            catch
            {
               MessageBox.Show("更新失败；请确认是否可以访问MFG DB");


            }

        }
        public static void EsfToolAvailableFromFabWip()
        {
            DataTable dt1, dt2, dt3;
            try
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                string connStr;

                string sql;
                DataRow[] drs;
                DataTable asmlWip, nikonWip;//WIP
                DataTable asmlPart, asmlTech, nikonPart, nikonTech;//ESF
                string[] asmlTools = new string[] { "ALDI02", "ALDI03", "ALDI05", "ALDI06", "ALDI07", "ALDI09", "ALDI10", "ALDI11", "ALDI12", "BLDI08", "BLDI13" };
                string[] nikonTools = new string[] { "ALII01", "ALII02", "ALII03", "ALII04", "ALII05", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14", "ALII15", "ALII16", "ALII17", "ALII18", "ALSIB6", "ALSIB7", "ALSIB8", "ALSIB9", "BLSIBK", "BLSIBL", "BLSIE1", "BLSIE2" };
                bool techFlag = false;
                string partStr;
                #region 抽取数据准备
                ///extract data
                ///Fab WIP
                sql = " Select distinct partname part from rptprd.sdb_view_info_wip";
                sql += " where substr(lottype,1,1) in ('M','N','P','E')";
                sql += "  and STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')";
                sql += " and location not like '%bank'";
                sql += " and substr(partname,1,1) not in ('1','2','8')";
                sql = " SELECT  SUBSTR(PARTID,1,LENGTH(PARTID)-3) PART,SUBSTR(RECPID,1,LENGTH(RECPID)-3) RECIPE,STAGE,SUBSTR(RECPID,1,3) EQPTYPE from RPTPRD.MFG_VIEW_FLOW WHERE SUBSTR(RECPID,1,3) IN ('LDI','LII') AND SUBSTR(PARTID,1,LENGTH(PARTID)-3) IN (" + sql + ")";

                dt1 = TaskExe.Share.CsmcOracle(sql);

                ///=============
                ///查询Constrain，并将其转化为正则表达式
                ///字段 TECH，PART，RECIPE，STAGE，EQPID，FLAG，TYPE，EQPTYPE （EQPID第三位）
                sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

                sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE FROM (" + sql + ")";

                dt2 = TaskExe.Share.CsmcOracle(sql);

                ///==========
                ///查询工艺代码，需要完善，部分产品缺工艺代码
                ///需要在ReworkMove.DB 建一个完整的表
                connStr = "data source=" + "P:\\_SQLite\\ReworkMove.db";
                sql = "SELECT distinct PART, TECH  FROM TBL_TECHFULL WHERE TECH IS NOT NULL";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds); dt3 = ds.Tables[0];
                            }
                        }
                    }
                }

                ///dt1:WIP   dt2:ESF, dt3:TECH
                ///为WIP并入TECH
                var query =
                       from t1 in dt1.AsEnumerable()
                       from t2 in dt3.AsEnumerable()
                       where t1.Field<string>("PART") == t2.Field<string>("PART")
                       select new
                       {
                           TECH = t2.Field<string>("TECH"),
                           PART = t1.Field<string>("PART"),
                           RECIPE = t1.Field<string>("RECIPE"),
                           STAGE = t1.Field<string>("STAGE"),
                           EQPTYPE = t1.Field<string>("EQPTYPE"),

                       };


                dt1 = TaskExe.Share.LINQToDataTable(query);


                ///分出NikonWIP，AsmlWIP
                drs = dt1.Select("EQPTYPE = 'LDI' ");
                asmlWip = dt1.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlWip.ImportRow(drs[i]);

                }

                drs = dt1.Select("EQPTYPE <> 'LDI' ");
                nikonWip = dt1.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonWip.ImportRow(drs[i]);

                }


                ///ESF 分类
                drs = dt2.Select("TYPE = '0' and EQPTYPE = 'D' ");

                asmlPart = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlPart.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '1' and EQPTYPE = 'D' ");

                asmlTech = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    asmlTech.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '0' and EQPTYPE <> 'D' ");
                nikonPart = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonPart.ImportRow(drs[i]);

                }

                drs = dt2.Select("TYPE = '1' and EQPTYPE <> 'D' ");
                nikonTech = dt2.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    nikonTech.ImportRow(drs[i]);

                }

                dt1 = null; dt2 = null; dt3 = null;


                //MessageBox.Show(asmlWip.Rows.Count.ToString());
                //MessageBox.Show(nikonWip.Rows.Count.ToString());
                //MessageBox.Show(asmlPart.Rows.Count.ToString());
                //MessageBox.Show(asmlTech.Rows.Count.ToString());
                //MessageBox.Show(nikonPart.Rows.Count.ToString());
                //MessageBox.Show(nikonTech.Rows.Count.ToString());
                #endregion

                #region 比较开始
                dt1 = new DataTable();
                dt1.Columns.Add("TECH", Type.GetType("System.String"));
                dt1.Columns.Add("PART", Type.GetType("System.String"));
                dt1.Columns.Add("RECIPE", Type.GetType("System.String"));
                dt1.Columns.Add("STAGE", Type.GetType("System.String"));
                dt1.Columns.Add("EQPTYPE", Type.GetType("System.String"));
                //  dt1.Columns.Add("WFRQTY", Type.GetType("System.Double"));
                // dt1.Columns.Add("LOTQTY", Type.GetType("System.Double"));
                dt1.Columns.Add("TOOL", Type.GetType("System.String"));
                #endregion


                #region Asml开始比较

                foreach (string tool in asmlTools)
                {
                    DataTable tblLoop;
                    tblLoop = null;
                    tblLoop = asmlTech.Clone();

                    //列出单个设备反向
                    drs = asmlTech.Select("EQPID = '" + tool + "'");


                    for (int i = 0; i < drs.Length; i++)
                    { tblLoop.ImportRow(drs[i]); }

                    foreach (DataRow x in asmlWip.Rows)
                    {
                        if (x["TECH"].ToString().Length > 1)
                        {



                            techFlag = false;
                            //反向判断，techFlag=true,无法作业
                            #region
                            foreach (DataRow y in tblLoop.Rows)
                            {
                                //  if (tool == "ALDI03" && x["RECIPE"].ToString() == "LDIGT"  && x["TECH"].ToString() == "C1813D5S0014" && x["PART"].ToString() == "WQ4061CJ-L"   && x["STAGE"].ToString() == "PO1-PH" && y["TECH"].ToString() == "^[A-Z0-9]{1}1\\S*$" && y["PART"].ToString() == "^$"  && y["STAGE"].ToString() == "^PO1-PH$")
                                //   {
                                if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                {
                                    // MessageBox.Show(x["TECH"].ToString());
                                    // MessageBox.Show(y["TECH"].ToString());
                                    // MessageBox.Show(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());



                                    if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                    {
                                        if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                        {
                                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                            {
                                                //到此一步，全部为True，有限制不能作业
                                                techFlag = true;

                                            }
                                        }
                                    }

                                }
                                //  }

                            }
                            #endregion
                            //正向
                            #region
                            if (techFlag == true)
                            {
                                //有反向限制不能作业
                            }
                            else
                            {
                                //判断是否有正向限制，针对所有设备，1）0台设备，可作业 2）多台设备，判断是否包含本设备
                                partStr = "";
                                foreach (DataRow y in asmlPart.Rows)
                                {
                                    if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                    {
                                        if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                        {
                                            if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                            {
                                                if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                                {
                                                    //到此一步，全部为True，有限制不能作业
                                                    partStr += y["EQPID"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                //以上正向判断结束；parStr长度为0，可以作业；不为0，包含设备名，才可作业
                                if (partStr.Length == 0 || partStr.Contains(tool))
                                {
                                    //可作业
                                    DataRow newRow = dt1.NewRow();
                                    newRow["TECH"] = x["TECH"].ToString();
                                    newRow["PART"] = x["PART"].ToString();
                                    newRow["RECIPE"] = x["RECIPE"].ToString();
                                    newRow["STAGE"] = x["STAGE"].ToString();
                                    newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                    //newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                    //newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                    newRow["TOOL"] = tool;
                                    dt1.Rows.Add(newRow);

                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //短流程无工艺代码，不判断
                        }

                    }

                }

                #endregion


                #region Nikon 开始比较
                foreach (string tool in nikonTools)
                {



                    DataTable tblLoop;
                    tblLoop = null;
                    tblLoop = nikonTech.Clone();

                    //列出单个设备反向
                    drs = nikonTech.Select("EQPID = '" + tool + "'");


                    for (int i = 0; i < drs.Length; i++)
                    { tblLoop.ImportRow(drs[i]); }

                    foreach (DataRow x in nikonWip.Rows)
                    {
                        if (x["TECH"].ToString().Length > 1)
                        {




                            //   if (tool == "ALDI03" && x["RECIPE"].ToString() == "LDIGT"
                            //      && x["TECH"].ToString()=="C1813D5S0014"
                            //    && x["PART"].ToString()=="WQ4061CJ-L"
                            //   && x["STAGE"].ToString()=="PO1-PH"
                            //  && y["TECH"].ToString()=="^[A-Z0-9]{1}1\\S*$"
                            //  && y["PART"].ToString()=="^$"
                            // && y["STAGE"].ToString()=="^PO1-PH$")
                            //   {
                            //    MessageBox.Show(x["TECH"].ToString() + "," + x["PART"].ToString() + "," + x["STAGE"].ToString() + "," + x["RECIPE"].ToString());


                            // MessageBox.Show(Regex.IsMatch("H5012","^[A-Z0-9]{1}1\\S*$").ToString());




                            techFlag = false;
                            //反向判断，techFlag=true,无法作业
                            #region
                            foreach (DataRow y in tblLoop.Rows)
                            {
                                if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                {

                                    if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                    {
                                        if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                        {
                                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                            {
                                                //到此一步，全部为True，有限制不能作业
                                                techFlag = true;

                                            }
                                        }
                                    }
                                }
                                //  MessageBox.Show(y["TECH"].ToString() + "," + y["PART"].ToString() + "," + y["STAGE"].ToString() + "," + y["RECIPE"].ToString());
                                //   MessageBox.Show(y["TECH"].ToString() + ",   " + y["PART"].ToString());
                                //  MessageBox.Show(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());
                                //MessageBox.Show(Regex.IsMatch(x["PART"].ToString(), y["TECH"].ToString()).ToString());
                                //MessageBox.Show(Regex.IsMatch(x["RECIPE"].ToString(), y["TECH"].ToString()).ToString());
                                // MessageBox.Show(Regex.IsMatch(x["STAGE"].ToString(), y["TECH"].ToString()).ToString());
                                //    MessageBox.Show("SDFSDFSDF     " + techFlag.ToString());
                                //  }




                            }



                            #endregion
                            //正向
                            #region
                            if (techFlag == true)
                            {
                                //有反向限制不能作业
                            }
                            else
                            {
                                //判断是否有正向限制，针对所有设备，1）0台设备，可作业 2）多台设备，判断是否包含本设备
                                partStr = "";
                                foreach (DataRow y in nikonPart.Rows)
                                {
                                    if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                                    {
                                        if (y["PART"].ToString() == "^$" || Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                        {
                                            if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                            {
                                                if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                                                {
                                                    //到此一步，全部为True，有限制不能作业
                                                    partStr += y["EQPID"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                //以上正向判断结束；parStr长度为0，可以作业；不为0，包含设备名，才可作业
                                if (partStr.Length == 0 || partStr.Contains(tool))
                                {
                                    //可作业
                                    DataRow newRow = dt1.NewRow();
                                    newRow["TECH"] = x["TECH"].ToString();
                                    newRow["PART"] = x["PART"].ToString();
                                    newRow["RECIPE"] = x["RECIPE"].ToString();
                                    newRow["STAGE"] = x["STAGE"].ToString();
                                    newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                    //newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                    //newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                    newRow["TOOL"] = tool;
                                    dt1.Rows.Add(newRow);

                                }
                            }
                            #endregion
                        }
                        else
                        {
                            //短流程无工艺代码，不判断
                        }
                    }








                }

                #endregion


                dt1.Columns.Add("RefreshDate");
                foreach (DataRow row in dt1.Rows)
                {
                    row["RefreshDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }



                ///保存到数据库
                connStr = "data source=P:\\_SQLite\\ReworkMove.db";
                string dbTblName = "tbl_esfToolAvailableFabWip";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string tmpSql = "DELETE  FROM " + dbTblName;
                    using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }

                DataTableToSQLte myTabInfo = new DataTableToSQLte(dt1, dbTblName);
                myTabInfo.ImportToSqliteBatch(dt1, "P:\\_SQLite\\ReworkMove.db");



                stopwatch.Stop();

                // MessageBox.Show("用时" + stopwatch.ElapsedMilliseconds.ToString() + " msec\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n另短流程（-RD）无法从OPAS R2R导出完整工艺代码，不判断");
               MessageBox.Show("工艺代码来自：\n\n" +
                       "   MFG Oracle数据库\n" +
                       "   LiYan/RenY编程数据\n" +
                       "   因各种原因，部分产品可能缺工艺代码，无法判断\n\n" +
                       "当前WIP来自MFG Oracle DB，实时性待确认\n\n" +
                       "表格最后一栏系数据刷新时间\n\n\n" +
                        stopwatch.ElapsedMilliseconds.ToString());

            }
            catch
            {
               MessageBox.Show("更新失败；请确认是否可以访问MFG DB");


            }

        }
        private void mfgOracleEsfToolAvailableReworkMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            EsfToolAvailableFromLithoWip();
            EsfToolAvailableFromFabWip();
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private void deleteChartingSourceExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            string folder;
            string file = string.Empty;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            List<string> fileList = new List<string>();
            string connStr = "data source=D:\\TEMP\\DB\\ChartRawData.db";

            string sql, latestDate, excelDate;



            //list RAR file
            folder = "P:\\_chartingrawdata";
            fileList = TaskExe.Share.ExportFileList(folder, fileList);
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
                TaskExe.Share.DosCommand(folder);

                //read CD data
                dt = TaskExe.Share.ReadExcel("C:\\temp\\ChartData.xls", "CD$");

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
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private static void UpdateDbWithPython(string exeFile, string exeParameter)
        {            
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = exeFile;
            exep.StartInfo.Arguments = exeParameter;
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();

        }
        private void callPythonUpdateAweNikonEgaVeryLongTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            UpdateDbWithPython("c:\\anaconda3\\python.exe", "P:\\_SQLite\\VsPythonAuto.py");
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        public static void AsmlAweDbDelete(int days = 30, string file1 = "P:\\_SQLite\\AsmlAwe.db") //删除n天前数据
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();


            string connStr1 = "data source=" + file1;

            string sql;
            string[] tools = { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD08", "BLSD7D" };
            string[] tbls = { "measured", "para", "WqMccDelta" };
            string riqi = DateTime.Now.AddDays(-days).ToString("yyyyMMdd");
            string tblName;

            SQLiteConnection conn = new SQLiteConnection(connStr1);
            conn.Open();

            foreach (string tool in tools)
            {
                foreach (string tbl in tbls)
                {
                    tblName = "tbl_" + tool + "_" + tbl;

                    //delete rows
                    sql = "DELETE  FROM " + tblName;
                    sql += " WHERE id in ( SELECT id FROM tbl_index WHERE date<'" + riqi + "')";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                   MessageBox.Show(tblName);
                }
            }

            //delete reference coordinate
            tblName = "tbl_reference";
            sql = "DELETE  FROM " + tblName;
            sql += " WHERE id in ( SELECT id FROM tbl_index WHERE date<'" + riqi + "')";
            using (SQLiteCommand com = new SQLiteCommand(sql, conn))
            {
                com.ExecuteNonQuery();
            }
           MessageBox.Show(tblName);
            //delete index
            tblName = "tbl_index";
            sql = "DELETE  FROM " + tblName;
            sql += " WHERE  date<'" + riqi + "'";
            using (SQLiteCommand com = new SQLiteCommand(sql, conn))
            {
                com.ExecuteNonQuery();
            }
           MessageBox.Show(tblName);





            conn.Close();
            stopwatch.Stop();
            Int64 tmp = stopwatch.ElapsedMilliseconds / 1000;
           MessageBox.Show(tmp.ToString());
        }
        private void houseKeepingAsmlAweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            AsmlAweDbDelete(30, "P:\\_SQLite\\AsmlAwe.db");
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");

        }
        public static void CreateFullAsmlAweDb(string file1 = "P:\\_SQLite\\AsmlAwe.db", string file2 = "P:\\_SQLite\\FullAsmlAwe.db")
        {
            string connStr1 = "data source=" + file1;
            string connStr2 = "data source=" + file2;
            string sql = string.Empty;
            Int64 maxId;
            DataTable dt;
            DataTableToSQLte myTabInfo;

            string[] tools = { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD08", "BLSD7D" };
            string[] tbls = { "measured", "para", "WqMccDelta" };
            string[] tblPublic = { "tbl_reference", "tbl_index" };
            string tblName;
            SQLiteConnection conn1 = new SQLiteConnection(connStr1);
            conn1.Open();
            SQLiteConnection conn2 = new SQLiteConnection(connStr2);
            conn2.Open();

            //for all tools
            foreach (string tool in tools)
            {
                foreach (string tbl in tbls)
                {
                    tblName = "tbl_" + tool + "_" + tbl;
                    //get latest id from large DB
                    sql = "SELECT MAX(id) FROM " + tblName;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn2))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            maxId = Convert.ToInt64(rdr[0]);
                           MessageBox.Show(tool + ", " + maxId.ToString());
                        }
                    }

                    //get latest data from small DB
                    sql = "SELECT * FROM " + tblName + " WHERE ID>" + maxId;

                    using (SQLiteCommand command = new SQLiteCommand(sql, conn1))
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
                    //update large DB;
                    myTabInfo = new DataTableToSQLte(dt, tblName);
                    myTabInfo.ImportToSqliteBatch(dt, file2);
                    dt = null;

                }
            }

            //for others
            foreach (string tbl in tblPublic)
            {
                tblName = tbl;
                //get latest id from large DB
                sql = "SELECT MAX(id) FROM " + tblName;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn2))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        maxId = Convert.ToInt64(rdr[0]);
                       MessageBox.Show(tbl + ", " + maxId.ToString());
                    }
                }

                //get latest data from small DB
                sql = "SELECT * FROM " + tblName + " WHERE ID>" + maxId;

                using (SQLiteCommand command = new SQLiteCommand(sql, conn1))
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
                //update large DB;
                myTabInfo = new DataTableToSQLte(dt, tblName);
                myTabInfo.ImportToSqliteBatch(dt, file2);
                dt = null;

            }

            conn1.Close();
            conn2.Close();
        }
        private void fullAsmlAweLongTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            CreateFullAsmlAweDb("P:\\_SQLite\\AsmlAwe.db", "P:\\_SQLite\\FullAsmlAwe.db");
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        private static void UpdateFullJobinStation(string connStr, string filePath, string dbTblName, string notes)
        {
            DateTime tmpDate = DateTime.Now;
            string dbPath = connStr.Split(new char[] { '=' })[1];
            string sql;
            DataTable dtShow;

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

            //update by every 50000 rows
            bool isFirst = true;
            string strLine;
            int n = 0;
            int rowCount = 0;
            string newLine = string.Empty;
            dtShow = new DataTable();
            DataTable dtTmp = new DataTable();
            int stepCount = 0;
            TaskExe.DataTableToSqlite myTabInfo;

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
                        myTabInfo = new TaskExe.DataTableToSqlite(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, dbPath);
                        stepCount = 0;
                        dtShow.Rows.Clear();
                        dtShow = null;
                        dtShow = dtTmp.Clone();

                    }
                }
            }
            myTabInfo = new TaskExe.DataTableToSqlite(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, dbPath);
           MessageBox.Show((DateTime.Now - tmpDate).ToString());
           MessageBox.Show(notes + "\n\n导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();
        }
        private void jobinStationLongTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();sw.Start();
            if (TaskExe.Share.FileDateCompare("P:\\_SQLite\\OPAS\\NikonJobinStation.csv", "P:\\_SQLite\\NikonJobinStation.db"))
            {
                UpdateFullJobinStation("data source=P:\\_SQLite\\NikonJobinStation.db", "P:\\_SQLite\\OPAS\\NikonJobinStation.csv", "tbl_nikonjobinstation", "Full Nikon Jobin Station Update Done");
            }
            else
            {
                MessageBox.Show("CSV Is Not The Latest One\n\n\nDB Is Not Updated");
            }

            if (TaskExe.Share.FileDateCompare("P:\\_SQLite\\OPAS\\AsmlJobinStation.csv", "P:\\_SQLite\\AsmlJobinStation.db"))
            {
                UpdateFullJobinStation("data source=P:\\_SQLite\\AsmlJobinStation.db", "P:\\_SQLite\\OPAS\\AsmlJobinStation.csv", "tbl_asmljobinstation", "Full Nikon Jobin Station Update Done");
            }
            else
            {
                MessageBox.Show("CSV Is Not The Latest One\n\n\nDB Is Not Updated");
            }
            MessageBox.Show("===DONE====\n\n" + Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString() + " Seconds");
        }
        #endregion

        #region view

        private void 查看ESF每日修改记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string filename;
            openFileDialog1.InitialDirectory = @"p:\_DailyCheck\ESF\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "csv文件(result*.csv)|result*.csv";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
            dtShow = LithoForm.LibF.OpenCsvNew(filename);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;


        }
        private void 刷新显示数据需访问MFGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            dtShow = LithoForm.Esf.EsfToolAvailableFromLithoWip();
            dataGridView1.DataSource = dtShow;
        }
        private void 显示所有数据ToolStripMenuItem_Click(object sender, EventArgs e)  //ESF可作业设备
        {
            dtShow = null;
            bool choice = radioButton2.Checked;
            dtShow = LithoForm.Esf.ListAllToolAvailableFromLithoWip(choice);
            dataGridView1.DataSource = dtShow;
        }
        private void 显示各设备WIPToolStripMenuItem_Click(object sender, EventArgs e)//photo wip -->tool wip
        {
            dtShow = null;
            bool choice = radioButton2.Checked;
            dtShow = LithoForm.Esf.ListAllToolWipFromLithoWip(choice);
            dataGridView1.DataSource = dtShow;
        }
        private void 显示各设备WIPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dtShow = null;
            bool choice = radioButton2.Checked;
            dtShow = LithoForm.Esf.ListAllToolWipFromLithoWipAll(choice);
            dataGridView1.DataSource = dtShow;
        }

        private void 显示可作业设备PhotoWipByPartStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            bool choice = radioButton2.Checked;
            dtShow = LithoForm.Esf.ListToolNameQtyByPartStage(choice, "photoWip");
            dataGridView1.DataSource = dtShow;
        }
        private void 按FABWIP更新数据源需访问MFGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            dtShow = LithoForm.Esf.EsfToolAvailableFromFabWip();
            dataGridView1.DataSource = dtShow;
        }

        private void 显示可作业设备FabWipByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            bool choice = radioButton2.Checked;
            dtShow = LithoForm.Esf.ListToolNameQtyByPartStage(choice, "fabWip");
            dataGridView1.DataSource = dtShow;
        }
        private void 查看CD检查结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = @"P:\_DailyCheck\HITACHI\CHECK\dailycheck.csv";
            dtShow = null;
            dtShow = LithoForm.LibF.OpenCsvNew(filename);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看所有IDP数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            connStr = @"data source=P:\_SQLite\cdRecipe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select * from tbl_idp";
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
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看所有AMP数据DBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            MessageBox.Show("Part/Layer名字在最后几列");
            connStr = @"data source=P:\_SQLite\cdRecipe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select * from tbl_amp";
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
            this.tabControl1.SelectedIndex = 0;
        }
        private void 运行脚本查看MetalTopDown图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            if (MessageBox.Show("运行此命令\r\n\r\n\r\n\r\n" +
                "1)删除C:\\temp目录，再重建；\r\n\r\n" +
                "2)命令调用python.exe,请确认本机已安装；\r\n\r\n" +
                "3)后续运行窗口会提示选择python.exe;\r\n\r\n" +

                "继续请选择-->是（Y）；\r\n\r\n" +
                "退出请选择-->否（N）；", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string filename;
                openFileDialog1.InitialDirectory = "c:\\anaconda3\\";
                openFileDialog1.Title = "选择源文件";
                openFileDialog1.Filter = "文件(python.exe)|python.exe";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName.Replace("\\", "\\\\");
                }
                else
                { MessageBox.Show("未选择数据文件"); return; }



                System.Diagnostics.Process exep = new System.Diagnostics.Process();
                exep.StartInfo.FileName = filename;
                exep.StartInfo.Arguments = "P:\\_SQLite\\VsPython.py";
                exep.StartInfo.CreateNoWindow = false;
                exep.StartInfo.UseShellExecute = true;
                exep.Start();
                exep.WaitForExit();
            }
            else
            {
                MessageBox.Show("EXIT");
            }
        }
        private void 确认NikonSeqLog是否正常下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04" ,"ALII05","ALII06","ALII07","ALII08","ALII09",
                               "ALII10","ALII11", "ALII12", "ALII13" ,"ALII14","ALII15","ALII16","ALII17","ALII18",
                                "BLII20","BLII21","BLII22","BLII23"};
            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("LatestSeqLog");
            dtShow.Columns.Add("SeqFlag");

            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string str1 = string.Empty;
                string str2 = string.Empty;
                string path = @"p:\SEQLOG\" + tool;
                DirectoryInfo root = new DirectoryInfo(path);
                FileInfo[] files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    try
                    { if (str1.Substring(0, 6) == "_2020-" && String.Compare(str1, str2) == 1) { str2 = str1; } }
                    catch { }

                }
                DataRow dr = dtShow.NewRow();
                dr["ToolId"] = tool;
                dr["LatestSeqLog"] = str2;
                //string riqi = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                if (String.Compare(str2.Substring(1, 10), DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")) == 1)
                {
                    dr["SeqFlag"] = "";
                }
                else
                {
                    dr["SeqFlag"] = "False";
                }

                dtShow.Rows.Add(dr);




            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;


        }

        private void 确认NikonSeqLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04" ,"ALII05","ALII06","ALII07","ALII08","ALII09",
                               "ALII10","ALII11", "ALII12", "ALII13" ,"ALII14","ALII15","ALII16","ALII17","ALII18",
                                "BLII20","BLII21","BLII22","BLII23"};
            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("LatestSeqLog");
            dtShow.Columns.Add("SeqFlag");
            dtShow.Columns.Add("LatestEgaLog");
            dtShow.Columns.Add("EgaFlag");
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string str1 = string.Empty;
                string str2 = string.Empty;
                string path = @"p:\SEQLOG\" + tool;
                DirectoryInfo root = new DirectoryInfo(path);
                FileInfo[] files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    try { if (str1.Substring(0, 6) == "_2020-" && String.Compare(str1, str2) == 1) { str2 = str1; } } catch { }

                }
                DataRow dr = dtShow.NewRow();
                dr["ToolId"] = tool;
                dr["LatestSeqLog"] = str2;
                //string riqi = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                if (String.Compare(str2.Substring(1, 10), DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")) == 1)
                {
                    dr["SeqFlag"] = "";
                }
                else
                {
                    dr["SeqFlag"] = "False";
                }

                dtShow.Rows.Add(dr);



                tool = "A" + tool.Substring(1, 5);
                str1 = string.Empty;
                str2 = string.Empty;
                path = @"P:\EgaLog\" + tool;
                root = new DirectoryInfo(path);
                files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }



                }

                dtShow.Rows[i]["LatestEgaLog"] = str2;
                if (str2.Substring(0, 7) == "ega_202" && String.Compare(str2.Substring(4, 9), DateTime.Now.AddDays(-2).ToString("yyyyMMdd")) == 1)
                {
                    dtShow.Rows[i]["EgaFlag"] = "";
                }
                else
                {
                    dtShow.Rows[i]["EgaFlag"] = "False";
                }


            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;


        }
        private void 确认Asml下载是否正常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string[] tools = { "7D", "08", "82", "83", "85", "86", "87", "89", "8A", "8B", "8C" };
            string line;
            int n = 0;

            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("OldErrLogStartDate");
            dtShow.Columns.Add("ErrLogStartDate");
            dtShow.Columns.Add("AweDate");
            dtShow.Columns.Add("BatchReportDate");
            dtShow.Columns.Add("DateNow");


            for (int i = 0; i < tools.Length; i++)
            {

                string tool = tools[i];
                DataRow newRow = dtShow.NewRow();
                newRow["ToolID"] = tool;
                dtShow.Rows.Add(newRow);
                string root = @"p:\_AsmlDownload\";
                string path = root + @"\AsmlErrlog\" + tool + @"\ER_Event_log";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            try
                            {
                                if (line.Substring(2, 1) == "/" && line.Substring(5, 3) == "/20")
                                {
                                    dtShow.Rows[i]["ErrLogStartDate"] = line.Substring(0, 20);
                                    break;
                                }
                            }
                            catch
                            {
                                //line字符串，substring命令出错
                            }
                        }
                    }
                }
                else
                { }

            }
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string root = @"P:\_AsmlDownload\";
                string path = root + @"\AsmlErrlog\" + tool + @"\ER_Event_log.old";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 10) //字符串长度为0，非空，substring函数报错
                            {
                                if (line.Substring(2, 1) == "/" && line.Substring(5, 3) == "/20")
                                {
                                    dtShow.Rows[i]["OldErrLogStartDate"] = line.Substring(0, 20);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                { }






            }

            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string path = @"P:\_AsmlDownload\AWE\" + tool;
                string str1 = string.Empty;
                string str2 = string.Empty;
                DirectoryInfo dirinfo = new DirectoryInfo(path);
                FileInfo[] files = dirinfo.GetFiles();
                foreach (var file in files)
                {
                    str1 = file.Name; str1 = str1.Substring(str1.Length - 19, 12);
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }
                }
                dtShow.Rows[i]["AweDate"] = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + " " + str2.Substring(8, 2) + ":" + str2.Substring(10, 2);

            }
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string path = @"P:\_AsmlDownload\BatchReport\" + tool;
                string str1 = string.Empty;
                string str2 = string.Empty;
                DirectoryInfo dirinfo = new DirectoryInfo(path);
                FileInfo[] files = dirinfo.GetFiles();
                foreach (var file in files)
                {
                    str1 = file.Name; str1 = str1.Substring(str1.Length - 19, 12);
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }
                }
                try
                {
                    dtShow.Rows[i]["BatchReportDate"] = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + " " + str2.Substring(8, 2) + ":" + str2.Substring(10, 2);

                }
                catch
                { }
                dtShow.Rows[i]["DateNow"] = DateTime.Now.ToString();
            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看AsmlErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string filename;
            openFileDialog1.InitialDirectory = @"P:\_AsmlDownload\AsmlErrlog\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "所有文件(*.*)|*.*";//
            dtShow = new DataTable();
            dtShow.Columns.Add("No"); dtShow.Columns.Add("Content");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
                if (filename.Contains("ER_event_log"))
                {

                    int n = 0;
                    string line;
                    using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Trim().Length > 0)
                            {
                                DataRow row = dtShow.NewRow();
                                row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                            }
                        }
                    }

                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP");
                    directoryInfo.Create();
                    string newFile = DateTime.Now.ToString("yyyyMMddhhmmss");

                    newFile = @"D:\temp\" + newFile;
                    LithoForm.GzipFile.gunZipFile(filename, newFile);
                    int n = 0;
                    string line;
                    using (StreamReader sr = new StreamReader(newFile, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Trim().Length > 0)
                            {
                                DataRow row = dtShow.NewRow();
                                row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                            }
                        }
                    }
                    File.Delete(newFile);

                }
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }

        }
        private void 查看已下载NikonSeqLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            string filename;
            openFileDialog1.InitialDirectory = @"P:\SEQLOG";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "所有文件(*.*)|*.*";//
            dtShow = new DataTable();
            dtShow.Columns.Add("No"); dtShow.Columns.Add("Content");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");

                int n = 0;
                string line;
                using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Trim().Length > 0)
                        {
                            DataRow row = dtShow.NewRow();
                            row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                        }
                    }
                }

                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
        }
        private void 查看已下载NikonSeqLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dtShow = null;
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "P:\\_SQLite\\Shell\\exe\\NIKON LOG分析.exe";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();
        }
        private void 查看工艺代码层次TrackRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;


            if (MessageBox.Show("按2位层次名，工艺代码前三位或工艺代码第二位，TrackRecipe查询所有流程调用的Track Recipe\r\n\r\n\r\r\n" +
                "工艺层次和工艺代码是精确匹配；TrackRecipe是模糊匹配\r\n\r\n\r\r\n" +

              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及设备"); return; }


            //pick layer
            string layer = Interaction.InputBox("请输入层次名；\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim().ToUpper();
            //track
            string track = Interaction.InputBox("请输入Track Recipe；\r\n\r\n\r\n若查询所有Track Recipe，输入%", "定义Track Recipe", "", -1, -1);

            if (track.Trim().Length == 0) { MessageBox.Show("工艺代码输入不正确，退出"); return; }
            track = track.Trim().ToUpper();

            //tech
            string tech = Interaction.InputBox("请输入工艺代码前三位或第二位；\r\n\r\n\r\n若查询所有工艺，输入%", "定义工艺", "", -1, -1);

            if (tech.Trim().Length == 0 || !(tech.Trim().Length == 1 || tech.Trim().Length == 3))
            {
                MessageBox.Show("工艺代码输入不正确;\r\n\r\n\r\n" +
                  "只能输入 1）工艺代码第二位;\r\n\r\n\r\n" +
                  "         2）工艺代码前三位;\r\n\r\n\r\n" +
                  "         3）通配符 '%';\r\n\r\n\r\n" +
                  "退出"); return;
            }
            tech = tech.Trim().ToUpper();



            string sql1;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            sql1 = " Select * from tbl_flowtrack where ";

            if (layer.Length == 1)
            { sql1 += " layer like '%'"; }
            else
            { sql1 += " layer='" + layer + "' "; }

            if (track == "%")
            { }
            else
            { sql1 += " and trackrecipe like '%" + track + "%' "; }


            string sql2;

            sql2 = "Select part,tech,processtype,processgroup from tbl_tech ";
            if (tech == "%")
            { }
            else if (tech.Length == 1)
            {
                sql2 += " Where substr(tech,2,1)='" + tech + "'";
            }
            else if (tech.Length == 3)
            {
                sql2 += " Where substr(tech,1,3)='" + tech + "'";
            }





            sql = "select b.tech TECH,a.part,a.layer,a.trackrecipe,a.mask,a.tooltype, substr(a.part,3,4) MaskQueue from (" + sql1 + ") a, (" + sql2 + ") b where   a.part=b.part order by tech, substr(a.part,3,4) desc";


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
                            dtShow = ds.Tables[0];
                        }
                    }
                }
            }











            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("Done");
        }

        private void 刷新曝光程序日期ToolStripMenuItem_Click(object sender, EventArgs e) //asml程序检查
        {
            #region initialize
            MessageBox.Show("因IT限制：\n\n    服务器程序日期在OA端电脑刷新；\n\n    光刻机程序日期在10.4.2.112刷新；");
            string[] allTools;
            string[] dirs;
            AsmlFtpWeb asmlFtpWeb;
            string[] files;
            string riqi1 = DateTime.Now.ToString("yyyy-MM-d") + " 24:59"; //部分日期没有年份
            string part = string.Empty;
            string riqi = string.Empty;
            List<string> list = new List<string>();
            string password;
            string tblName;
            DataRow[] drs;
            string lastYear = DateTime.Now.AddYears(-1).ToString("yyyy");
            connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=P:\\_SQLite\\AsmlRecipeCheck.mdb";


            //read password
            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
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
            string ip = LibF.GetIpAddress();
            if (ip == "10.4.2.112")
            {
                allTools = new string[] { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD7D", "BLSD08" };
                dirs = new string[] { "/user_data/jobs/PROD" };
                password = list[1];
            }
            else
            {
                allTools = new string[] { "SERVER" };
                dirs = new string[] { "/user_data/jobs/PROD850", "/user_data/jobs/PROD700" };
                password = list[0];
            }
            list = null;

            #endregion

            foreach (string tool in allTools)
            {
                foreach (string dir in dirs)
                {
                    //定义表名
                    if (dir.Contains("850"))
                    { tblName = "SVR850"; }
                    else if (dir.Contains("700"))
                    { tblName = "SVR700"; }
                    else
                    { tblName = tool; }
                    //readdata from access

                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        sql = "SELECT * FROM " + tblName;
                        conn.Open();
                        using (OleDbCommand command = new OleDbCommand(sql, conn))
                        {
                            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    da.Fill(ds);
                                    dtShow = ds.Tables[0];
                                    dataGridView1.DataSource = dtShow;
                                }
                            }
                        }
                    }


                    asmlFtpWeb = new AsmlFtpWeb(ipUser[tool][1], "/usr/asm/data." + ipUser[tool][0].Substring(4, 4) + dir, ipUser[tool][0], password);



                    files = asmlFtpWeb.GetFilesDetailList("");
                    dt1 = new DataTable(); dt1 = dtShow.Clone();

                    OleDbConnection connInsert = new OleDbConnection(connStr);
                    connInsert.Open();
                    OleDbCommand commandInsert;

                    foreach (string file in files)
                    {
                        if (file.Substring(0, 1) == "-" && file.Length > 54)
                        {
                            riqi = file.Substring(41, 12).Trim();
                            if (riqi.Contains(":"))
                            {
                                riqi = DateTime.Now.Year.ToString() + " " + riqi;//riqi分两种：带年份，无时间；有时间
                            }
                            riqi = DateTime.Parse(riqi).ToString("yyyy-MM-dd HH:mm");
                            if (string.Compare(riqi, riqi1) > 0)
                            {
                                riqi = lastYear + riqi.Substring(4, riqi.Length - 4);
                            }
                            part = file.Substring(54, file.Length - 54).Trim();


                            DataRow newRow = dt1.NewRow();
                            newRow[0] = part;
                            newRow[1] = riqi;
                            dt1.Rows.Add(newRow);


                            drs = dtShow.Select("Part='" + part + "' AND Riqi='" + riqi + "'");
                            if (drs.Length == 0)
                            {
                                sql = "INSERT INTO " + tblName + " (Part,Riqi)  VALUES ('" + part + "','" + riqi + "')";
                                commandInsert = new OleDbCommand(sql, connInsert);
                                commandInsert.ExecuteNonQuery();
                            }
                        }
                    }
                    commandInsert = null; connInsert.Close(); connInsert = null;
                }
            }
            MessageBox.Show("Refreshing Recipe Date Done");
        }

        private void 查询曝光程序更改ToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void toolStripMenuItem4_Click(object sender, EventArgs e) // 查看所有工艺限制
        {
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            dtShow = LithoForm.Flow.EsfRawData(connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)//查看所有涂胶程序
        {
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            dtShow = LithoForm.Flow.TrackRecipe(connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show(dataGridView1.Rows.Count.ToString());

        }
        private void 查看产品工艺代码DieQtyChipSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入Part名查询\r\n\r\n\r\n输入空格或%查询所有", "", "", -1, -1);
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.db"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.db"; }


            sql = "select * from tbl_tech where part like '%" + str + "%'";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
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


            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未查询到数据"); }
        }
        private void 查看光刻版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入光刻版名前四位\r\n\r\n\r\n输入%查询所有", "", "", -1, -1);

            if (str.Trim().Length == 0) { MessageBox.Show("No Input,Exit"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.db"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.db"; }


            sql = "select * from tbl_mask where Mask_MPW like '" + str + "%'";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
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
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未查询到数据"); }
        }
        private void 查看当前WIP仅Part名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;

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
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0; ShowGridview();
          
        }
        #endregion

        #region rework
        private void 查询原始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }

            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            LithoForm.Rework.RawData(connStr, sDate, eDate, ref dtShow);
            dataGridView1.DataSource = dtShow;
            ShowGridview();

        }
        private void 查询统计数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            LithoForm.Rework.SummaryData(connStr, ref dtShow);
            dataGridView1.DataSource = dtShow;
            ShowGridview();
        }
        private void 作图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null; dtShow = new DataTable();
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            LithoForm.Rework.SummaryData(connStr, ref dtShow);




            listView1.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;//display full row
            listView1.MultiSelect = false;
            listView1.View = View.Details;
            listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
            listView1.Columns.Add("No", 20, HorizontalAlignment.Left);
            listView1.Columns.Add("Section", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Catetory", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("F", 20, HorizontalAlignment.Left);

            int i = 1;
            ListViewItem li;
            foreach (string item in new string[] { "光刻部", "光刻设备刻", "涂胶显影设备课", "量测设备课", "光刻工艺课", "涂胶显影工艺课", "ASML FEOL", "ASML BEOL", "NIKON FEOL", "NIKON BEOL" })
            {
                li = new ListViewItem(i.ToString());
                li.SubItems.Add(item); li.SubItems.Add("ByLot"); li.SubItems.Add("F"); listView1.Items.Add(li); i += 1;
                li = new ListViewItem(i.ToString());
                li.SubItems.Add(item); li.SubItems.Add("ByWafer"); li.SubItems.Add("F"); listView1.Items.Add(li); i += 1;

            }

        }
        #endregion

        #region Batch_Report
        private void batchReportQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = radioButton2.Checked;
            dtShow = null; dtShow = new DataTable();
            classBatchReport.QueryIlluminationByProduct(flag, ref dtShow);
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                ShowGridview();
                MessageBox.Show("DONE");
            }
            else
            {
                MessageBox.Show("No Data Available");
            }
        }
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            bool flag = radioButton2.Checked;
            dtShow = null; dtShow = new DataTable();
            classBatchReport.QueryIlluminationByTechRecipe(flag, ref dtShow);
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                ShowGridview();
                MessageBox.Show("DONE");
            }
            else
            {
                MessageBox.Show("No Data Available");
            }
        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            bool flag = radioButton2.Checked;
            dtShow = null; dtShow = new DataTable();
            classBatchReport.QueryFocus(flag, ref dtShow);
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                ShowGridview();
                MessageBox.Show("DONE");
            }
            else
            {
                MessageBox.Show("No Data Available");
            }
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            #region list parameters
            string[] tmp;
            tmp = LithoForm.Asml.ListBatchReportParameter();
            listView1.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;//display full row
            listView1.MultiSelect = true;
            listView1.View = View.Details;
            listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

            listView1.Columns.Add("ParameterName", 200, HorizontalAlignment.Left);

            foreach (string item in tmp)
            {
                ListViewItem li = new ListViewItem(item);//此处括号内的变量是第一个字段参数
                listView1.Items.Add(li);
            }
            #endregion
        }

        private void 自定义查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            #region query
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
     "结束日期\r\n\r\n    统计参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择统计参数\r\n\r\n" +
     "    选择 是（Y） 继续\r\n\r\n" +
     "    选择 否（N） 退出\r\n\r\n\r\n" +
     "注：查询前先选择日期，并运行 ‘列出BatchReport参数’命令选择参数", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }


            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;
            string para = string.Empty;

            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }


            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                { para += item.SubItems[0].Text + ","; }
                para = para.Substring(0, para.Length - 1);
            }
            else
            {
                MessageBox.Show("未选择参数，退出");
                return;
            }






            dtShow = LithoForm.Asml.QueryPara(connStr, sDate, eDate, para, part, layer);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            #endregion

        }
        #endregion

        #region ASML AWE
        private void aWELOGToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查看对位标记类型和对位程序\r\n\r\n\r\n\r\n查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出\r\n\r\n\r\n" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }

            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", ""));
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", ""));
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            string connStr1;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; connStr1 = @"data source=P:\_SQLite\ReworkMove.DB"; }



            dtShow = LithoForm.Asml.AweIndexQuery(sDate, eDate, connStr, part, layer, connStr1);
            dataGridView1.DataSource = dtShow;
            MessageBox.Show("数据：" + dtShow.Rows.Count.ToString() + "条");
            this.tabControl1.SelectedIndex = 0;
        }

        private void 统计WqMccDeltaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查询不区分日期，速度极慢，尽量按完整产品名查询\r\n\r\n是（Y），继续\r\n\r\n否（N），退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { return; }

            string tech = string.Empty;
            string part = string.Empty;
            string layer = string.Empty;



            if (MessageBox.Show("此命令是按工艺，产品，层次统计对位相关的WQ，MCC，DELTA SHIT数据\r\n\r\n后续按提示输入，若查询所有，不要输入内容\r\n\r\n注意：查询数据需提前复制到D:\\TEMP\\DB目录\r\n\r\n是（Y），继续\r\n\r\n否（N），退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { return; }
            tech = Interaction.InputBox("请输入工艺代码\r\n\r\n若按产品查询，直接回车，不用输入内容", "", "", -1, -1);
            part = Interaction.InputBox("请输入产品名\r\n\r\n若已输入工艺代码，直接回车，不用输入内容", "", "", -1, -1);
            layer = Interaction.InputBox("请输入层次名\r\n\r\n必须且只能输入两位层次代码", "", "", -1, -1);
            tech = tech.Trim().ToUpper(); part = part.Trim().ToUpper(); layer = layer.Trim().ToUpper();
            if (layer.Length != 2)
            { MessageBox.Show("Layer必须为两位，退出，请确认"); return; }
            if (tech.Length == 0 && part.Length == 0)
            { MessageBox.Show("Part和Tech不能同时为空，退出，请确认"); return; }

            dtShow = LithoForm.Asml.SummaryWqMccDelta(tech, part, layer);
            dataGridView1.DataSource = dtShow;
            MessageBox.Show("数据 " + dtShow.Rows.Count.ToString() + " 行");
        }

        private void 步骤一索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("列出索引项" +
           "    选择 是（Y） 按PPID/日期查询\r\n\r\n" +
           "    选择 否（N） 按LOTID查询\r\n\r\n\r\n" +
           "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { AweListByPpidRiqi(); }
            else
            { AweListByLotId(); }
        }

        private void AweListByPpidRiqi()
        {
            if (MessageBox.Show("查看对位标记类型和对位程序\r\n\r\n\r\n\r\n查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
           "结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适\r\n\r\n" +
           "    选择 是（Y） 继续\r\n\r\n" +
           "    选择 否（N） 退出\r\n\r\n\r\n" +
           "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }


            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", ""));
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", ""));
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }



            dtShow = LithoForm.Asml.VectorIndex1(sDate, eDate, connStr, part, layer);
            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Part", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LotID", 85, HorizontalAlignment.Left);
                listView1.Columns.Add("Date", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 50, HorizontalAlignment.Left);
                for (int i = 0; i < dtShow.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());
                    li.SubItems.Add(dtShow.Rows[i][5].ToString());
                    li.SubItems.Add(dtShow.Rows[i][4].ToString());
                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }




        }
        private void AweListByLotId()
        {


            string lot = Interaction.InputBox("该查询要求输入LotID名，通配符查询;\r\n\r\n\r\n现在请输入LotID;", "定义LotID", "", -1, -1);

            if (lot.Trim().Length == 0) { MessageBox.Show("LotID输入不正确，退出"); return; }
            lot = lot.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }



            dtShow = LithoForm.Asml.VectorIndex2(connStr, lot);
            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Part", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LotID", 85, HorizontalAlignment.Left);
                listView1.Columns.Add("Date", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 50, HorizontalAlignment.Left);
                for (int i = 0; i < dtShow.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());
                    li.SubItems.Add(dtShow.Rows[i][5].ToString());
                    li.SubItems.Add(dtShow.Rows[i][4].ToString());
                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }

        }
        private void 步骤二双击列表项作图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsmlVectorPlot();
        }

        public void AsmlVectorPlot()
        {


            long str = 0; string str1 = string.Empty;

            if (listView1.SelectedItems.Count == 0)
            { MessageBox.Show("请选择Lot显示，退出"); return; }


            else //multi禁用，==1
            {
                str = Convert.ToInt64(listView1.SelectedItems[0].SubItems[5].Text);
                str1 = listView1.SelectedItems[0].SubItems[0].Text;
            }

            listDatatable = LithoForm.Asml.VectorData(connStr, str1, str);

            dataGridView1.DataSource = listDatatable[2];



            //抽出wferid至列表框中，供后续选中作图；https://blog.csdn.net/qq_28249373/article/details/74905884
            wfrIdBox.Items.Clear();
            int count = 0;
            List<string> list = new List<string>();
            for (int k = 0; k < dataGridView1.Rows.Count - 1; k++) //datagridview1 有一空行，循环减1--》旧程序复制过来的，直接用dtShow即可
            { list.Add(dataGridView1.Rows[k].Cells[1].Value.ToString()); }
            HashSet<string> hs = new HashSet<string>(list);
            list = new List<string>(hs);
            foreach (var x in list)
            {
                this.wfrIdBox.Items.Add(x);
                wfrIdBox.SetItemChecked(count, true);
                count += 1;
            }



            List<string> typeList = new List<string>();
            for (int i = 0; i < vectorTypeList.Items.Count; i++)
            {

                typeList.Add(vectorTypeList.GetItemText(vectorTypeList.Items[i].ToString()));

            }
            List<string> wqmccList = new List<string>();
            wqmccList.Add("X_RedWQ5");

            LithoForm.Vector.PlotAsmlVectorNew(list, typeList, wqmccList, listDatatable, zoom, zoom1);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;


        }

        private void 重新ASML作图_Click(object sender, EventArgs e)
        {
            {
                List<string> wfrList = new List<string>();
                for (int i = 0; i < wfrIdBox.Items.Count; i++)
                {
                    if (wfrIdBox.GetItemChecked(i))
                    {
                        //wfrList.Add(checkedListBox1.GetItemText(wfrIdBox.Items[i].ToString()));
                        wfrList.Add(wfrIdBox.GetItemText(wfrIdBox.Items[i].ToString()));
                    }
                }

                List<string> typeList = new List<string>();
                for (int i = 0; i < vectorTypeList.Items.Count; i++)
                {
                    if (vectorTypeList.GetItemChecked(i))
                    {
                        typeList.Add(vectorTypeList.GetItemText(vectorTypeList.Items[i].ToString()));
                    }
                }

                List<string> wqmccList = new List<string>();

                for (int i = 0; i < wqMccBox.Items.Count; i++)
                {
                    if (wqMccBox.GetItemChecked(i))
                    {
                        wqmccList.Add(wqMccBox.GetItemText(wqMccBox.Items[i].ToString()));
                    }
                }


                //   LithoForm.Vector.PlotAsmlVector()





                LithoForm.Vector.PlotAsmlVectorNew(wfrList, typeList, wqmccList, listDatatable, zoom, zoom1);
                pictureBox1.Image = null;
                try
                {
                    //若直接引用，同名文件修改后无法正常显示；
                    FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show("文件显示失败");
                }
                this.tabControl1.SelectedIndex = 1;


            }
        }

        private void 步骤一读入数据并拟合5阶信号ToolStripMenuItem_Click(object sender, EventArgs e)
        {        
        string filename;
            openFileDialog1.InitialDirectory = @"C:\temp\";
            openFileDialog1.Title = "选择AWE文件";
            openFileDialog1.Filter = "awe文件(*.awe)|*.awe";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择AWE文件"); return; }

            Stopwatch sw = new Stopwatch();
            sw.Start();
           dt1= LithoForm.Vector.ReadAweFile(filename);
            sw.Stop();
            MessageBox.Show("Reading AWE & 5th Order Signal Linear Regression\n\nDONE !\n\n\nTotal Time: " + sw.ElapsedMilliseconds + " msec");

           


        }
        private void 步骤二WaferQualitySummaryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {        
        dtShow = LithoForm.Vector.SumWqMccDelta(ref dt1);
            dataGridView1.DataSource = dtShow;
        }

        private void 步骤二25thOrderResidualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = LithoForm.Vector.SumResidual(ref dt1);
            dataGridView1.DataSource = dt1;
        }
        #endregion

        #region NIKON


        private void 查看预对位ToolStripMenuItem_Click(object sender, EventArgs e)

        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate, eDate;
            sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.PreAlignment(sDate, eDate, connStr);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 查看对位方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }

            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }



            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.AlignMethod(sDate, eDate, connStr, ppid);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 查看对位数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入层次名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }

            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.AlignData(sDate, eDate, connStr, ppid);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 步骤1列出索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);
            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }

            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.VectorIndex(sDate, eDate, connStr, ppid);

            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Ppid", 130, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 100, HorizontalAlignment.Left);


                listView1.Columns.Add("Datetime", 100, HorizontalAlignment.Left);
                for (int i = 0; i < dtShow.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());

                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }




        }


        private void NikonVectorPlot()
        {

            long str = 0; string str1 = string.Empty;

            if (listView1.SelectedItems.Count == 0)
            { MessageBox.Show("请选择Lot显示，退出"); return; }


            else //multi禁用，==1
            {
                str = Convert.ToInt64(listView1.SelectedItems[0].SubItems[2].Text);
                str1 = listView1.SelectedItems[0].SubItems[0].Text;
            }

            dtShow = LithoForm.Nikon.VectorData(connStr, str1, str);
            dataGridView1.DataSource = dtShow;

            //抽出wferid至列表框中，供后续选中作图；https://blog.csdn.net/qq_28249373/article/details/74905884
            wfrIdBox.Items.Clear();
            int count = 0;
            List<string> list = new List<string>();
            for (int k = 0; k < dataGridView1.Rows.Count - 1; k++) //datagridview1 有一空行，循环减1--》旧程序复制过来的，直接用dtShow即可
            { list.Add(dataGridView1.Rows[k].Cells[1].Value.ToString()); }
            HashSet<string> hs = new HashSet<string>(list);
            list = new List<string>(hs);
            foreach (var x in list)
            {
                this.wfrIdBox.Items.Add(x);
                wfrIdBox.SetItemChecked(count, true);
                count += 1;
            }


            LithoForm.Vector.PlotNikonVector(list, dtShow, 1f);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;





        }
        private void 重新作图_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < wfrIdBox.Items.Count; i++)
            {
                if (wfrIdBox.GetItemChecked(i))
                {
                    // list.Add(checkedListBox1.GetItemText(wfrIdBox.Items[i].ToString()));
                    list.Add(wfrIdBox.GetItemText(wfrIdBox.Items[i].ToString()));
                }
            }

            LithoForm.Vector.PlotNikonVector(list, dtShow, zoom);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;


        }


        #endregion

        #region ChartRawData.DB

        private void chartRawDataDB查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CD/OVL数据来自IT邮件，每日更新");
        }
        private void 查询CDToolStripMenuItem3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
              "结束日期\r\n\r\n    设备名\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备\r\n\r\n" +
              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及设备"); return; }



            //pick tool
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }
            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.cdQueryExcel(tools, part, layer, date1, date2, connStr);

            if (dtShow.Rows.Count == 0)
            { MessageBox.Show("No Data,Exit"); return; }

            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            int rowsCount = dtShow.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "PartID", "Layer", "ProcEqID" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "PartID, Layer, ProcEqID";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("PartID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("A", 20, HorizontalAlignment.Left);

                if (dt1.Columns.Count > 2)
                {
                    for (int i = 3; i < dt1.Columns.Count; i++)

                    { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }














        }
        private void 查询OVLToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
           "结束日期\r\n\r\n    设备名\r\n\r\n    OVL参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备，OVL参数\r\n\r\n" +
           "    选择 是（Y） 继续\r\n\r\n" +
           "    选择 否（N） 退出\r\n\r\n\r\n" +
           "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期，设备，OVL参数"); return; }

            //pick tools
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            //pick parameter
            string para = "";
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    if (para == "")
                    { para = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    else
                    { para = para + ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                }
            }




            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawDate.DB"; }
            dtShow = LithoForm.Charting.ovlQueryExcel(tools, para, part, layer, date1, date2, connStr);
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;
            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据");

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "PartID", "Layer", "ProcEqID" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "PartID, Layer, ProcEqID";
            dt1 = dv.ToTable();
            dv = null;




            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("PartID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("B", 20, HorizontalAlignment.Left);
                //增加flag
                //    if (dt1.Columns.Count > 2)
                //    {
                //        for (int i = 3; i < dt1.Columns.Count; i++)
                //
                //        { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                //    }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }

        }
        private void 查询CDToolStripMenuItem1_Click(object sender, EventArgs e)
        {




            if (MessageBox.Show("查询筛选条件：\r\n\r\n" +
                "    起始日期\r\n\r\n" +
                "    结束日期\r\n\r\n\r\n\r" +
              "请确认日期是否合适r\n\r\n" +
              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，重新选择日期"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.qcQueryExcelForCd(date1, date2, connStr);

            if (dtShow.Rows.Count == 0)
            { MessageBox.Show("No Data,Exit"); return; }

            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;



            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "ProcEqID", "LayerID", "PartID" });


            DataView dv = dt1.DefaultView;
            dv.Sort = " LayerID,ProcEqID,PartID ";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("LayerID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("PartID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("C", 20, HorizontalAlignment.Left);

                if (dt1.Columns.Count > 2)
                {
                    for (int i = 3; i < dt1.Columns.Count; i++)

                    { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }




        }
        private void 查询OVLToolStripMenuItem1_Click(object sender, EventArgs e)
        {




            if (MessageBox.Show("查询筛选条件：\r\n\r\n" +
             "    起始日期\r\n\r\n" +
             "    结束日期\r\n\r\n\r\n\r" +
           "请确认日期是否合适r\n\r\n" +
           "    选择 是（Y） 继续\r\n\r\n" +
           "    选择 否（N） 退出\r\n\r\n" +
           "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，重新选择日期"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.qcQueryExcelForOverlay(date1, date2, connStr);
            dataGridView1.DataSource = dtShow;

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "ProcEqID", "LayerID", "PartID" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "ProcEqID, LayerID,PartID ";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("LayerID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("PartID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("D", 20, HorizontalAlignment.Left);

                if (dt1.Columns.Count > 2)
                {
                    for (int i = 3; i < dt1.Columns.Count; i++)

                    { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }




        }
        private void 查询ASMLToolStripMenuItem_Click(object sender, EventArgs e)
        {




            if (MessageBox.Show("查询筛选条件：\r\n\r\n" +
            "    起始日期\r\n\r\n" +
            "    结束日期\r\n\r\n\r\n\r" +
          "请确认日期是否合适r\n\r\n" +
          "    选择 是（Y） 继续\r\n\r\n" +
          "    选择 否（N） 退出\r\n\r\n" +
          "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，重新选择日期"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.asmlProductQueryExcelForOverlay(date1, date2, connStr);
            //dataGridView1.DataSource = dtShow;

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "ProcEqID", "Layer" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "ProcEqID, Layer ";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("E", 20, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 80, HorizontalAlignment.Left);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());
                    li.SubItems.Add("");
                    li.SubItems.Add(dt1.Rows[i][1].ToString().Substring(0, 2));
                    listView1.Items.Add(li);

                }

                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }






        }
        private void 查询NikonFIA产品趋势ToolStripMenuItem_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("查询筛选条件：\r\n\r\n" +
          "    起始日期\r\n\r\n" +
          "    结束日期\r\n\r\n\r\n\r" +
        "请确认日期是否合适r\n\r\n" +
        "    选择 是（Y） 继续\r\n\r\n" +
        "    选择 否（N） 退出\r\n\r\n" +
        "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，重新选择日期"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.nikonFiaLsaProductQueryExcelForOverlay(date1, date2, connStr, "FIA");
            dataGridView1.DataSource = dtShow;

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "ProcEqID", "Layer" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "ProcEqID, Layer ";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("E", 20, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 80, HorizontalAlignment.Left);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());
                    li.SubItems.Add("");
                    li.SubItems.Add(dt1.Rows[i][1].ToString().Substring(0, 2));
                    listView1.Items.Add(li);

                }

                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }



        }
        private void 查询NikonLSAToolStripMenuItem_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("查询筛选条件：\r\n\r\n" +
          "    起始日期\r\n\r\n" +
          "    结束日期\r\n\r\n\r\n\r" +
        "请确认日期是否合适r\n\r\n" +
        "    选择 是（Y） 继续\r\n\r\n" +
        "    选择 否（N） 退出\r\n\r\n" +
        "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，重新选择日期"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ChartRawData.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ChartRawData.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.Charting.nikonFiaLsaProductQueryExcelForOverlay(date1, date2, connStr, "LSA");
            dataGridView1.DataSource = dtShow;

            dt1 = dtShow.DefaultView.ToTable(true, new string[] { "ProcEqID", "Layer" });


            DataView dv = dt1.DefaultView;
            dv.Sort = "ProcEqID, Layer ";
            dt1 = dv.ToTable();
            dv = null;

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("ProcEqID", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("E", 20, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 80, HorizontalAlignment.Left);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());
                    li.SubItems.Add("");
                    li.SubItems.Add(dt1.Rows[i][1].ToString().Substring(0, 2));
                    listView1.Items.Add(li);

                }

                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }


        }

        #endregion

        #region R2R.DB
        private void r2RDB查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("数据源非实时生成，需在OPAS下载数据生成");
        }
        private void 查询CDToolStripMenuItem_Click_1(object sender, EventArgs e)
        {



            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
              "结束日期\r\n\r\n    设备名\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备\r\n\r\n" +
              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及设备"); return; }



            //pick tool
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }
            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\R2R.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\R2R.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.R2R.cdQuery(tools, part, layer, date1, date2, connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            int rowsCount = dtShow.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");
        }
        private void 查询OVLToolStripMenuItem2_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    设备名\r\n\r\n    OVL参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备，OVL参数\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出\r\n\r\n\r\n" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期，设备，OVL参数"); return; }

            //pick tools
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            //pick parameter
            string para = "";
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    if (para == "")
                    { para = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    else
                    { para = para + ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                }
            }




            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\R2R.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\R2R.DB"; }
            dtShow = LithoForm.R2R.ovlQuery(tools, para, part, layer, date1, date2, connStr);
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;
            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据");

        }
        private void 查询CD按工艺ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    设备名\r\n\r\n    工艺名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期，设备选择是否合适\r\n\r\n\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期，设备"); return; }

            string bDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;

            string sql2 = string.Empty; //-->tbl_config, cd target
            //pick tool

            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }

            bool sourceFlag = radioButton2.Checked;


            dtShow = LithoForm.R2R.QueryCdByTech(tools, bDate, eDate, sourceFlag);


            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;
        }
        #endregion

        #region JobinStation
        private void jobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("数据需要从OPAS下载，生成数据库，非实时");

            bool sourceFlag = radioButton2.Checked;
            dtShow = LithoForm.R2R.QueryJobinStation(sourceFlag);
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;

        }
        #endregion

        #region Extra
        private void 查询在线WIPJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sourceFlag = radioButton2.Checked;
            dtShow = LithoForm.R2R.NikonAsmlQueryJobinStation(sourceFlag);
            dataGridView1.DataSource = dtShow;
            dataGridView2.DataSource = dtShow;

        }

        private void 查询JobinStation套刻补值是否异常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tool = Interaction.InputBox("按设备类型查询当前WIP是否有异常补值\r\n\r\n\r\n" +
               "查询前请先设定如下参数规范：\r\n" +
               "    FlagTran：  Grid Translation\r\n" +
               "    FlagEXP：   Grid Expansion\r\n" +
               "    FlagOrtRot：Grid Ort & Rot\r\n" +
               "    FlagShot:   Shot Mag/Rot/ASMag/AsRot\r\n\r\n\r\n" +
               "输入 N 查询Nikon设备\r\n\r\n" +
               "输入 A 查询ASML设备\r\n\r\n" +
               "输入其它字符，退出（重新设定规范）" +
               "", "定义设备类型", "", -1, -1);
            tool = tool.Trim().ToUpper();
            if (tool == "N" || tool == "A") { }
            else { MessageBox.Show("Input Is Not 'A' OR 'N', EXIT"); return; }
            string s1, s2, s3, s4;
            s1 = textBox2.Text;  //tran
            s2 = textBox3.Text;  //exp
            s3 = textBox4.Text;  //rot,ort
            s4 = textBox5.Text;  //shot

            bool sourceFlag = radioButton2.Checked;
            dtShow = LithoForm.R2R.QueryAbnormalJobinstation(sourceFlag, tool, s1, s2, s3, s4);
            dataGridView1.DataSource = dtShow;
        }

        private void 步骤一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.调用 MAINTAIN 主菜单下的 OpasSql:Wip_JobinStation 子菜单\r\n\n\r\n" +
                "2.复制生成的SQL，登录10.4.3.130下载CSV格式数据");
        }

        private void 步骤二SQLITEDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sourceFlag = radioButton2.Checked;
            LithoForm.R2R.CreateLocalDb(sourceFlag);

        }

        private void 步骤三按设备前后段查询CDOVLJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region
            dtShow = null; dt1 = null; dt2 = null; ClearMemory();
            bool sourceFlag = radioButton2.Checked;

            string choice = Interaction.InputBox("数据查询按Nikon、ASML前后段区分\r\n\r\n\r\n" +
                "请按提示输入选项\r\n\r\n\r\n" +
                "   ASML FEOL， 请输入 AF\r\n\r\n\r\n" +
                "   ASML BEOL， 请输入 AB\r\n\r\n\r\n" +
                "   NIKON FEOL，请输入 NF\r\n\r\n\r\n" +
                "   NIKON BEOL，请输入 NB\r\n\r\n\r\n", "定义DUV/Iline 前后段", "", -1, -1);
            choice = choice.Trim().ToUpper();
            if (choice == "AF" || choice == "AB" || choice == "NB" || choice == "NF")
            { }
            else
            { MessageBox.Show("输入不正确，退出"); return; }

            //定义其它筛选参数
            bool optionCd = radioCd.Checked;
            string paraOvl = string.Empty;
            if (radioOvl.Checked)
            {
                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    if (checkedListBox3.GetItemChecked(i))
                    {
                        if (paraOvl == "")
                        { paraOvl = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                        else
                        { paraOvl += ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    }
                }
            }

            string[] cdFlag = new string[] { checkBoxFlagCD.Checked.ToString(), textBox1.Text.Trim() };
            string[] tranFlag = new string[] { checkBoxFlagTran.Checked.ToString(), textBox2.Text.Trim() };
            string[] expFlag = new string[] { checkBoxFlagExp.Checked.ToString(), textBox3.Text.Trim() };
            string[] ortRotFlag = new string[] { checkBoxFlagOrtRot.Checked.ToString(), textBox4.Text.Trim() };
            string[] shotFlag = new string[] { checkBoxFlagShot.Checked.ToString(), textBox5.Text.Trim() };




            DataTable[] dtTables;
            dtTables = LithoForm.R2R.ReadFeolBeolData(choice, sourceFlag, cdFlag, tranFlag, expFlag, ortRotFlag, shotFlag);

            // MessageBox.Show(dtTables.Length.ToString());

            dtShow = dtTables[0];
            dt2 = dtTables[1];
            dt1 = dtTables[2];
            #endregion

            //筛选项加入listview

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("PART", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("LAYER", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LOTID", 80, HorizontalAlignment.Left);

                if (dt1.Columns.Count > 2)
                {
                    for (int i = 3; i < dt1.Columns.Count; i++)

                    { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null; ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }


        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)  //全新新品筛选
        {
            int no;
            try
            {
                no = Convert.ToInt32(Interaction.InputBox("在完成步骤三的前提下，筛选出最新的新品供查询;\r\n\r\n\r\n" +
                   "现在请输入新品数量", "定义新品数量", "", -1, -1));
            }
            catch
            {
                MessageBox.Show("Input Is Not Correct,Exit"); return;
            }
            connStr = @"data source=P:\_SQLite\Flow.DB";

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
                            dt1 = ds.Tables[0];
                            if (dt1.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件"); return;

                            }

                        }
                    }
                }
            }

            string str = string.Empty;

            for (int i = dt1.Rows.Count - 1; i > dt1.Rows.Count - no - 1; i--)
            {
                if (i > -1)
                { str += dt1.Rows[i][0].ToString() + ","; }
            }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            foreach (ListViewItem item in listView1.Items)
            {

                if (!(str.Contains(item.SubItems[0].Text)))
                { listView1.Items.Remove(item); }
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e) //衍生新品
        {
            int no;
            try
            {
                no = Convert.ToInt32(Interaction.InputBox("在完成步骤三的前提下，筛选出最新的衍生新品供查询;\r\n\r\n\r\n" +
                   "现在请输入新品数量", "定义新品数量", "", -1, -1));
            }
            catch
            {
                MessageBox.Show("Input Is Not Correct,Exit"); return;
            }
            connStr = @"data source=P:\_SQLite\Flow.DB";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "select distinct part from tbl_biastable where part not in (SELECT DISTINCT PART FROM TBL_CHECK2 where part is not null ) and part is not null"; //where part is not null,否则not in 查询数据为空

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];
                            if (dt1.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件"); return;

                            }

                        }
                    }
                }
            }


            string str = string.Empty;

            for (int i = dt1.Rows.Count - 1; i > dt1.Rows.Count - no - 1; i--)
            {
                if (i > -1)
                { str += dt1.Rows[i][0].ToString() + ","; }
            }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            foreach (ListViewItem item in listView1.Items)
            {

                if (!(str.Contains(item.SubItems[0].Text)))
                { listView1.Items.Remove(item); }
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

        }
        private void DailyR2rReview()
        {
            ClearMemory();
            DataRow[] drs; DataTable dt11 = dtShow.Clone(); DataTable dt22 = dt2.Clone();
            //定义参数
            string items = string.Empty;
            if (radioCd.Checked)
            {
                items = " ITEM='DOSE' ";
            }
            else
            {
                if (checkedListBox3.GetItemChecked(0))
                {
                    items = " ITEM<>'DOSE' ";
                }
                else
                {

                    for (int i = 1; i < checkedListBox3.Items.Count; i++)
                    {

                        if (checkedListBox3.GetItemChecked(i))
                        {
                            if (items == "")
                            { items += " ITEM = '" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                            else
                            { items += " OR ITEM = '" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                        }
                    }


                }
                if (items.Length == 0)
                { items = " ITEM<>'DOSE' "; } //一个不选，默认全选
                else
                { items = " ( " + items + " ) "; }





            }


            // MessageBox.Show(items);
            if (radioBy2.Checked)  //by part layer
            {
                drs = dtShow.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and " + items);


            }
            else //by lot
            {
                drs = dtShow.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and LOTID = '" + listView1.SelectedItems[0].SubItems[2].Text + "' and " + items);
            }
            // MessageBox.Show("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and " + items);


            foreach (var row in drs)
            { dt11.ImportRow(row); }
            dt11.DefaultView.Sort = "DCOLL_TIME ASC";

            dataGridView1.DataSource = dt11;

            dt22 = dt2.Clone();
            drs = dt2.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'");
            foreach (var row in drs)
            { dt22.ImportRow(row); }
            dataGridView2.DataSource = dt22;


        }
        private void 步骤四可选仅列出CDOVL异常LOTToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (listView1.Items[0].SubItems.Count == 3)
            { MessageBox.Show("No CD/OVL Flag, Exit"); return; }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            bool f;
            foreach (ListViewItem item in listView1.Items)
            {
                f = false;
                for (int i = 3; i < item.SubItems.Count; i++)
                {
                    if (item.SubItems[i].Text == "True")
                    {
                        f = true;
                        break;
                    }
                }
                if (f == false) { listView1.Items.Remove(item); }
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。  
        }
        private void 步骤五可选JobInStation导出为Nikon格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dt1 = LithoForm.LibF.GetDgvToTable(dataGridView2);

            DataTable dt = new DataTable();
            string[] col = new string[] {"Tech",
"PartID","Layer","AlignLayer","FeedbackOvl","ReservedOvl","ReservedPartOvl","ExpiredHoursOvl","FeedbackCd","ReservedCd","ReservedPartCd","R_Value","E.L_Value","TypeCD","ExpiredHoursCd","Pre_EqId",              "EqId","CD_Target","Dose_Value","Dose_Up","Dose_Low","Focus_Value","Focus_Up","Focus_Low","TrX_Value","TrX_Up","TrX_Low","TrY_Value","TrY_Up","TrY_Low","ExpX_Value","ExpX_Up","ExpX_Low","ExpY_Value","ExpY_Up","ExpY_Low","Ort_Value","Ort_Up","Ort_Low","Rot_Value","Rot_Up","Rot_Low","SMag_Value","SMag_Up","SMag_Low","SRot_Value","SRot_Up","SRot_Low","Lock/Unlock","Fix","Constrain","Pre_Layer_Name"};

            foreach (string x in col)
            { dt.Columns.Add(x); }

            for (int count = 0; count < dataGridView2.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                dr["PartID"] = Convert.ToString(dataGridView2.Rows[count].Cells[0].Value);
                dr["Layer"] = Convert.ToString(dataGridView2.Rows[count].Cells[1].Value);
                dr["EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[2].Value);
                dr["Dose_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[3].Value);
                dr["Focus_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[4].Value);
                dr["TrX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[5].Value);
                dr["TrY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[6].Value);
                dr["ExpX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[7].Value);
                dr["ExpY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[8].Value);
                dr["Ort_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[9].Value);
                dr["Rot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[10].Value);
                dr["SMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[11].Value);
                dr["SRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[12].Value);
                dr["CD_Target"] = Convert.ToString(dataGridView2.Rows[count].Cells[15].Value);
                dr["Pre_EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[16].Value);
                dr["Pre_Layer_Name"] = Convert.ToString(dataGridView2.Rows[count].Cells[17].Value);
                dr["FeedbackCd"] = Convert.ToString(dataGridView2.Rows[count].Cells[18].Value);
                dr["FeedbackOvl"] = Convert.ToString(dataGridView2.Rows[count].Cells[19].Value);
                dr["Constrain"] = Convert.ToString(dataGridView2.Rows[count].Cells[20].Value);
                dr["E.L_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[21].Value);
                dr["TypeCD"] = Convert.ToString(dataGridView2.Rows[count].Cells[22].Value);
                dr["Fix"] = Convert.ToString(dataGridView2.Rows[count].Cells[23].Value);
                dr["Lock/Unlock"] = Convert.ToString(dataGridView2.Rows[count].Cells[24].Value);


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            LithoForm.LibF.dataTableToCsv(dt, "d:\\temp\\import.csv");
            MessageBox.Show("Save As D:\\TEMP\\IMPORT.CSV");

        }
        private void 步骤六可选JobInStation导出为Asml格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //dt1 = LithoForm.LibF.GetDgvToTable(dataGridView2);

            DataTable dt = new DataTable();
            string[] col = new string[] {"Tech",
"PartID","Layer","AlignLayer","FeedbackOvl","ReservedOvl","ReservedPartOvl","ExpiredHoursOvl","FeedbackCd","ReservedCd","ReservedPartCd","R_Value","E.L_Value","TypeCD","ExpiredHoursCd","Pre_EqId",              "EqId","CD_Target","Dose_Value","Dose_Up","Dose_Low","Focus_Value","Focus_Up","Focus_Low","TrX_Value","TrX_Up","TrX_Low","TrY_Value","TrY_Up","TrY_Low","ExpX_Value","ExpX_Up","ExpX_Low","ExpY_Value","ExpY_Up","ExpY_Low","Ort_Value","Ort_Up","Ort_Low","Rot_Value","Rot_Up","Rot_Low","SMag_Value","SMag_Up","SMag_Low","SRot_Value","SRot_Up","SRot_Low","ASMag_Value","ASMag_Up","ASMag_Low","ASRot_Value","ASRot_Up","ASRot_Low","Lock/Unlock","Fix","Constrain","Pre_Layer_Name"};

            foreach (string x in col)
            { dt.Columns.Add(x); }

            for (int count = 0; count < dataGridView2.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                dr["PartID"] = Convert.ToString(dataGridView2.Rows[count].Cells[0].Value);
                dr["Layer"] = Convert.ToString(dataGridView2.Rows[count].Cells[1].Value);
                dr["EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[2].Value);
                dr["Dose_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[3].Value);
                dr["Focus_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[4].Value);
                dr["TrX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[5].Value);
                dr["TrY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[6].Value);
                dr["ExpX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[7].Value);
                dr["ExpY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[8].Value);
                dr["Ort_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[9].Value);
                dr["Rot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[10].Value);
                dr["SMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[11].Value);
                dr["SRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[12].Value);
                dr["ASMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[13].Value);
                dr["ASRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[14].Value);


                dr["CD_Target"] = Convert.ToString(dataGridView2.Rows[count].Cells[15].Value);
                dr["Pre_EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[16].Value);
                dr["Pre_Layer_Name"] = Convert.ToString(dataGridView2.Rows[count].Cells[17].Value);
                dr["FeedbackCd"] = Convert.ToString(dataGridView2.Rows[count].Cells[18].Value);
                dr["FeedbackOvl"] = Convert.ToString(dataGridView2.Rows[count].Cells[19].Value);
                dr["Constrain"] = Convert.ToString(dataGridView2.Rows[count].Cells[20].Value);
                dr["E.L_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[21].Value);
                dr["TypeCD"] = Convert.ToString(dataGridView2.Rows[count].Cells[22].Value);
                dr["Fix"] = Convert.ToString(dataGridView2.Rows[count].Cells[23].Value);
                dr["Lock/Unlock"] = Convert.ToString(dataGridView2.Rows[count].Cells[24].Value);


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            LithoForm.LibF.dataTableToCsv(dt, "d:\\temp\\import.csv");
            MessageBox.Show("Save As D:\\TEMP\\IMPORT.CSV");
        }
        private void 步骤七可选列出TestWaferPerLotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("LOCK_FIXED='Y'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }

            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }
        private void 步骤八可选列出限制作业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("CONSTRAIN='Y'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }

            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }
        private void 步骤九可选列出反馈失败ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("LOCK_LOT like  '%反馈%'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }

            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }





        #endregion

        #region  vector


        private void 选择全部圆片_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < wfrIdBox.Items.Count; i++) { wfrIdBox.SetItemChecked(i, true); }
        }
        private void 取消选择_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < wfrIdBox.Items.Count; i++) { wfrIdBox.SetItemChecked(i, false); }
        }

        private void 释放内存重置_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
            //https://blog.csdn.net/qq_39216397/article/details/80139030
        }
        private void 释放_Click(object sender, EventArgs e)
        {
            ClearMemory();
            /*
            string  sqlx, sqly, sqlzx, sqlzy, sqlz;
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }
            connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sqlx = " SELECT * FROM TBL_ALSD82_WQMCCDELTA WHERE XY='X' AND ID=477831  ";
                sqly = " SELECT * FROM TBL_ALSD82_WQMCCDELTA WHERE XY='Y' AND ID=477831  ";
                sqlz = " SELECT a.id,a.WaferNr,a.MarkNrX,a.MarkNrY,a.type,b.X as X0,b.Y as Y0,a.X as X,a.Y as Y FROM " +
                    " TBL_ALSD82_MEASURED a,TBL_REFERENCE b WHERE " +
                    " a.MarkNrX=b.MarkNrX AND  a.ID = b.ID AND  a.ID=477831";



                sqlzx = " SELECT z1.id,z1.WaferNr,z1.X0,z1.Y0, " +
                       " x1.RedMCC1 RedMCC1X, x1.RedWQ1 RedWQ1X " +
                    " FROM (" + sqlz + ") z1,(" + sqlx + ") x1 WHERE " +
                    " z1.MarkNrX=x1.MarkNr and z1.WaferNr=x1.WaferNr  and z1.TYPE='R5_Measured'";

                sqlzy = " SELECT z2.id,z2.WaferNr,z2.X0,z2.Y0, " +
                       " y1.RedMCC1 RedMCC1Y, y1.RedWQ1 RedWQ1Y " +
                    " FROM (" + sqlz + ") z2,(" + sqly + ") y1 WHERE " +
                    "  z2.MarkNrY=y1.MarkNr and z2.WaferNr=y1.WaferNr and z2.TYPE='R5_Measured'";

                sql = sqlz;









                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];
                            //tblSecond = ds.Tables[1];

                        }
                    }
                }

            }

            dataGridView1.DataSource = dtShow;
            */
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            zoom = ScrollBar1.Value;
            zoom = (float)(zoom / 10);
        }

        private void ScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            zoom1 = ScrollBar2.Value;
            zoom1 = (float)(zoom1);
        }



        #endregion

        #region table
        private void 表格一筛选排序统计_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 x = dtShow.Rows.Count;
            }
            catch
            {
                MessageBox.Show("表格未绑定数据，退出"); return;
            }

            StringBuilder passData0 = new StringBuilder();
            StringBuilder passData1 = new StringBuilder();
            if (dtShow != null && dtShow.Columns.Count > 0)
            {
                for (int i = 0; i < dtShow.Columns.Count; i++)
                {

                    passData0.Append((dtShow.Columns[i].ColumnName.ToString() + ","));
                    passData1.Append((dtShow.Columns[i].DataType.Name.ToString() + ","));

                }

            }
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.Show();
            form2.ChildEvent1 += StatData;//统计
            form2.ChildEvent2 += SortData;  //排序
            form2.ChildEvent3 += FilterData;//筛选
            if (Form2Trigger == null)
            { MessageBox.Show("NULL"); }
            else
            {

                Form2Trigger(new string[] { passData0.ToString(), passData1.ToString() });
            }
        }
        private void StatData(string sumKey, string groupKey)
        {
            DataTable dtGroup = new DataTable();
            DataTable dtResult = new DataTable();

            string[] str1 = groupKey.Split(new char[] { ',' });
            string[] str2 = sumKey.Split(new char[] { ',' });
            string selectKey;
            string tmp;

            //dtResult
            foreach (var str in str1)
            { dtResult.Columns.Add(str); }
            foreach (var str in str2)
            {
                tmp = dtShow.Columns[str].DataType.Name.ToString().ToUpper();
                if (tmp.Contains("DOUBLE") || tmp.Contains("INT") || tmp.Contains("DECIMAL"))
                {
                    dtResult.Columns.Add((str + "_COUNT"),typeof(int));
                    dtResult.Columns.Add((str + "_SUM"), typeof(double));
                    dtResult.Columns.Add((str + "_MAX"), typeof(double));
                    dtResult.Columns.Add((str + "_MIN"), typeof(double));
                    dtResult.Columns.Add((str + "_AVG"), typeof(double));


                }
                else
                {
                    dtResult.Columns.Add((str + "_COUNT"), typeof(int));
                }
            }

            //  for (int i=0;i<dtResult.Columns.Count;i++)
            //   { MessageBox.Show(dtResult.Columns[i].ColumnName.ToString()); }






            if (str1.Length == 1)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0]); }
            else if (str1.Length == 2)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1]); }
            else if (str1.Length == 3)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2]); }
            else if (str1.Length == 4)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3]); }
            else if (str1.Length == 5)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4]); }
            else if (str1.Length == 6)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5]); }
            else if (str1.Length == 7)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6]); }
            else if (str1.Length == 8)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7]); }
            else if (str1.Length == 9)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7], str1[8]); }
            else if (str1.Length == 10)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7], str1[8], str1[9]); }

            else
            { MessageBox.Show("最多选择10个分类项目，现已超过10个，请重新选择"); return; }

            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                selectKey = string.Empty;
                for (int j = 0; j < str1.Length; j++)
                {
                    selectKey += (str1[j] + "='" + dtGroup.Rows[i][j] + "' and ");
                }
                selectKey = selectKey.Substring(0, selectKey.Length - 4);
                // MessageBox.Show(selectKey);
                DataRow[] drs = dtShow.Select(selectKey);
                //MessageBox.Show(drs.Length.ToString());

                DataTable dtTmp = dtShow.Clone();
                foreach (DataRow row in drs)
                { dtTmp.ImportRow(row); }



                DataRow dr = dtResult.NewRow();
                for (int j = 0; j < str1.Length; j++)
                {
                    dr[j] = dtGroup.Rows[i][j].ToString();
                }
                foreach (var str in str2)
                {
                    tmp = dtShow.Columns[str].DataType.Name.ToString().ToUpper();
                    if (tmp.Contains("DOUBLE") || tmp.Contains("INT") || tmp.Contains("DECIMAL"))
                    {
                        dr[str + "_COUNT"] = dtTmp.Compute("count(" + str + ")", "");
                        dr[str + "_SUM"] = dtTmp.Compute("sum(" + str + ")", "");
                        dr[str + "_MAX"] = dtTmp.Compute("max(" + str + ")", "");
                        dr[str + "_MIN"] = dtTmp.Compute("min(" + str + ")", "");
                        dr[str + "_AVG"] = dtTmp.Compute("avg(" + str + ")", "");
                    }
                    else
                    {
                        dr[str + "_COUNT"] = dtTmp.Compute("count(" + str + ")", "");
                    }
                }
                dtResult.Rows.Add(dr);






                // break;
            }
            dtShow = dtResult.Copy(); dtResult = null;
            dataGridView1.DataSource = dtShow;





            /*
            var query = from t in dtShow.AsEnumerable()
                        group t by new
                        {
                            t1 = t.Field<string>("FlowType"),
                            t2 = t.Field<string>("MM")
                        }
                        into m
                        select new
                        {
                            FlowType = m.Key.t1,
                            MM = m.Key.t2,
                            QTY = m.Sum(n => n.Field<double>("QTY"))
                        };
            MessageBox.Show(query.ToList().Count.ToString());
            */
        }

        private void SortData(string sortKey) //排序
        {
            DataView dv = dtShow.DefaultView;
            dv.Sort = sortKey;
            dataGridView1.DataSource = dv.ToTable();
        }
        private void FilterData(string filterKey) //筛选
        {
            DataRow[] drs = dtShow.Select(filterKey);
            DataTable dt = dtShow.Clone();
            foreach (var row in drs)
            { dt.ImportRow(row); }
            dtShow = dt.Copy();
            dt = null;
            dataGridView1.DataSource = dtShow;


        }

        #endregion

        #region 杂项
        private void ShowGridview()
        {
            chart1.Visible = false; chart1.SendToBack();
            panel6.Visible = false; panel6.SendToBack();




        }


        private void DisplayHideChart_Click(object sender, EventArgs e)  //hide chart

        {
            if (chart1.Visible == true)
            {
                chart1.Visible = false; chart1.SendToBack();
                panel6.Visible = false; panel6.SendToBack();
            }
            else
            {
                chart1.Visible = true; chart1.BringToFront();
                panel6.Visible = true; panel6.BringToFront();

            }
        }


        private void 清除设备选项_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++) { checkedListBox2.SetItemChecked(i, false); }
        }
        private void 清除参数选项_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox3.Items.Count; i++) { checkedListBox3.SetItemChecked(i, false); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LibF.dataGridViewToCsv(ref dataGridView1);
        }

        #endregion

        #region ListView Double Click


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {




            if (listView1.Columns.Count > 1 && listView1.Columns[0].Text == "PART" && listView1.Columns[1].Text == "LAYER")
            {
                DailyR2rReview();
            }
            else if (listView1.Columns.Count > 1 && listView1.Columns[1].Text == "Ppid" && listView1.Columns[0].Text == "Tool")
            {
                NikonVectorPlot();
            }
            else if (listView1.Columns.Count > 4 && listView1.Columns[4].Text == "Date")
            {
                AsmlVectorPlot();
            }
            else if (listView1.Columns.Count > 3 && listView1.Columns[3].Text == "A")
            {
                DrawChartCdA("A");
            }
            else if (listView1.Columns.Count > 3 && listView1.Columns[3].Text == "B")
            {
                DrawChartOvlB("B");
            }
            else if (listView1.Columns.Count > 3 && listView1.Columns[3].Text == "C")
            {
                DrawChartCdA("C");
            }
            else if (listView1.Columns.Count > 3 && listView1.Columns[3].Text == "D")
            {
                DrawChartOvlB("D");
            }
            else if (listView1.Columns.Count > 2 && listView1.Columns[1].Text == "E")
            {
                DrawChartOvlB("E");
            }
            else if (listView1.Columns.Count > 3 && listView1.Columns[3].Text == "F")
            {
                DrawChartRwkF("F");
            }
            else
            { MessageBox.Show("==当前不适用鼠标双击命令=="); }


        }


        private void DrawChartCdA(string choice)
        {
            #region initialize plot
            List<string> l = new List<string>();
            foreach (var series in chart1.Series)
            { l.Add(series.Name); }
            foreach (string x in l)
            { chart1.Series.Remove(chart1.Series[x]); }  //直接删除有问题？
            chart1.ChartAreas[0].Name = "C1";

            //否则设置坐标轴规范时出问题
            chart1.ChartAreas["C1"].AxisY.Maximum = 9999999.0;
            chart1.ChartAreas["C1"].AxisY.Minimum = 0.0;
            chart1.ChartAreas["C1"].AxisY2.Maximum = 9999999.0;
            chart1.ChartAreas["C1"].AxisY2.Minimum = 0.0;

            //显示坐标副轴，因在返工图中隐藏了
            chart1.ChartAreas["C1"].AxisY2.MajorGrid.Enabled = true;
            chart1.ChartAreas["C1"].AxisY2.LabelStyle.Enabled = true;
            chart1.ChartAreas["C1"].AxisY2.MajorTickMark.Enabled = true;

            #endregion
            #region select data

            foreach (var dataSeries in chart1.Series) { dataSeries.Points.Clear(); }

            ClearMemory();
            DataRow[] drs; DataTable dt = dtShow.Clone();


            if (choice == "A")  //普通CD
            {
                if (radioBy3.Checked)  //by part layer tool
                {
                    drs = dtShow.Select("PartID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and Layer='" + listView1.SelectedItems[0].SubItems[1].Text + "' and  ProcEqID='" + listView1.SelectedItems[0].SubItems[2].Text + "'");


                }
                else //by part layer
                {
                    drs = dtShow.Select("PartID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and Layer='" + listView1.SelectedItems[0].SubItems[1].Text + "'");
                }
            }
            else // CDU 不需要层次名
            {

                if (radioBy3.Checked)  //by part layer tool
                {
                    drs = dtShow.Select("ProcEqId='" + listView1.SelectedItems[0].SubItems[0].Text + "'  and  PartID='" + listView1.SelectedItems[0].SubItems[2].Text + "'");


                }
                else //by part layer
                {
                    drs = dtShow.Select("ProcEqID='" + listView1.SelectedItems[0].SubItems[0].Text + "' ");
                }
            }

            foreach (var row in drs)
            { dt.ImportRow(row); }
            dt.DefaultView.Sort = "DcollTime ASC";

            dataGridView1.DataSource = dt;
            drs = null; ClearMemory();
            #endregion

            #region 图表样式
            chart1.BackColor = System.Drawing.Color.LightCyan;//设置图表的背景颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightGreen;//设置背景的辅助颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightGreen;//设置背景的辅助颜色
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.BorderlineColor = System.Drawing.Color.LightGreen;//设置图像边框的颜色           
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 1;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;//设置图像的边框外观样式
            #endregion

            #region add two columns for cd spec plot
            dt.Columns.Add("cdU", Type.GetType("System.Double"));
            dt.Columns.Add("cdL", Type.GetType("System.Double"));
            if (choice == "C") //CDU QC 规范放5%
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["cdU"] = Convert.ToDouble(dt.Rows[i]["CdTarget"]) * 1.05;
                    dt.Rows[i]["cdL"] = Convert.ToDouble(dt.Rows[i]["CdTarget"]) * 0.95;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["cdU"] = Convert.ToDouble(dt.Rows[i]["CdTarget"]) * 1.1;
                    dt.Rows[i]["cdL"] = Convert.ToDouble(dt.Rows[i]["CdTarget"]) * 0.9;
                }
            }
            #endregion

            #region 图表标题
            chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "CD PLOT",
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);
            #endregion





            #region 数据绑定
            this.chart1.DataSource = dt;

            foreach (string x in new string[] { "JobIn", "Feedback", "Optimum" })
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series(x));
                chart1.Series[x].ChartArea = "C1";
                chart1.Series[x].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                chart1.Series[x].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                this.chart1.Series[x].YValueMembers = x;//设置X轴的数据源
                this.chart1.Series[x].XValueMember = "LotID";//设置Y轴的数据源
                chart1.Series[x].Name = x;//设置数据名称
            }
            foreach (string x in new string[] { "MET_AVG", "CdTarget", "cdU", "cdL", "S1", "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9" })
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series(x));
                chart1.Series[x].ChartArea = "C1";
                chart1.Series[x].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                chart1.Series[x].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                this.chart1.Series[x].YValueMembers = x;//设置X轴的数据源
                this.chart1.Series[x].XValueMember = "DcollTime";//设置Y轴的数据源
                chart1.Series[x].Name = x;//设置数据名称
            }
            this.chart1.DataBind();
            #endregion

            #region 数据样式
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            chart1.Series[0].MarkerSize = 8;
            chart1.Series[0].MarkerColor = System.Drawing.Color.DarkGreen;
            chart1.Series[0].BorderWidth = 1;
            chart1.Series[0].Color = Color.DarkGreen;
            chart1.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            chart1.Series[1].MarkerSize = 8;
            chart1.Series[1].MarkerColor = System.Drawing.Color.Purple;
            chart1.Series[1].BorderWidth = 1;
            chart1.Series[1].Color = Color.Purple;
            chart1.Series[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[2].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart1.Series[2].MarkerSize = 8;
            chart1.Series[2].MarkerColor = System.Drawing.Color.Blue;
            chart1.Series[2].BorderWidth = 1;
            chart1.Series[2].Color = Color.Blue;
            chart1.Series[2].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            //平均值
            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等
                                                                                                              //chart1.Series[3].IsValueShownAsLabel = false;//设置是否在Chart中显示坐标点值
                                                                                                              //chart1.Series[3].BorderColor = System.Drawing.Color.Red;//设置数据边框的颜色
                                                                                                              //chart1.Series[3].Color = System.Drawing.Color.Red;//设置数据的颜色
            chart1.Series[3].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
            chart1.Series[3].MarkerSize = 12;
            chart1.Series[3].MarkerColor = System.Drawing.Color.Red;
            chart1.Series[3].BorderWidth = 2;
            chart1.Series[3].Color = Color.Red;
            chart1.Series[3].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;

            //单点值
            for (int i = 7; i < 16; i++)
            {
                chart1.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
                chart1.Series[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
                chart1.Series[i].MarkerSize = 5;
                //chart1.Series[i].MarkerColor = System.Drawing.Color.LightBlue;
                chart1.Series[i].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            }



            //规范参考线
            chart1.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[4].BorderWidth = 1;
            chart1.Series[4].Color = Color.HotPink;
            chart1.Series[4].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.Series[5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[5].BorderWidth = 1;
            chart1.Series[5].Color = Color.HotPink;
            chart1.Series[5].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.Series[6].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[6].BorderWidth = 1;
            chart1.Series[6].Color = Color.HotPink;
            chart1.Series[6].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;




            #endregion

            #region 图例样式
            chart1.ChartAreas[0].Name = "C1";
            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].BackColor = System.Drawing.Color.LightBlue;//设置图例的背景颜色
                                                                         // chart1.Legends[0].DockedToChartArea = "C1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)
            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;//设置显示图例项方式(多列一行、一列多行、多列多行)
            #endregion

            #region 图表区域样式

            chart1.ChartAreas[0].Name = "C1";

            //chart1.ChartAreas[0].AxisX.LineColor = Color.Red;//X轴颜色   
            //chart1.ChartAreas[0].AxisY.LineColor = Color.Green;//Y轴颜色   
            //chart1.ChartAreas[0].AxisX.LineWidth = 2;         //X轴宽度   
            //chart1.ChartAreas[0].AxisY.LineWidth = 2;          //Y轴宽度 






            chart1.ChartAreas["C1"].Position.Auto = true;//设置是否自动设置合适的图表元素

            //chart1.ChartAreas["C1"].Position.Width = 100;//绘图区域在控件中的宽度 100是百分比
            //chart1.ChartAreas["C1"].Position.Height = 80;
            //chart1.ChartAreas["C1"].Position.X = 1;//绘图区域在控件中的绝对位置 横坐标
            //chart1.ChartAreas["C1"].Position.Y = 8; //绘图区域在控件中的绝对位置纵坐标



            //chart1.ChartAreas["C1"].ShadowColor = System.Drawing.Color.YellowGreen;//设置图表的阴影颜色
            // chart1.ChartAreas["C1"].Position.X = 2F;//设置图表元素左上角对应的X坐标
            //chart1.ChartAreas["C1"].Position.Y = 2F;//设置图表元素左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Position.Height = 86.76062F;//设置图表元素的高度
            //chart1.ChartAreas["C1"].Position.Width = 88F;//设置图表元素的宽度



            chart1.ChartAreas["C1"].InnerPlotPosition.Auto = true;//设置是否在内部绘图区域中自动设置合适的图表元素
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Height = 85F;//设置图表元素内部绘图区域的高度
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Width = 86F;//设置图表元素内部绘图区域的宽度
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.X = 8.3969F;//设置图表元素内部绘图区域左上角对应的X坐标
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Y = 3.63068F;//设置图表元素内部绘图区域左上角对应的Y坐标
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Inclination = 10;//设置三维图表的旋转角度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.IsClustered = true;//设置条形图或柱形图的的数据系列是否为簇状
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.IsRightAngleAxes = true;//设置图表区域是否使用等角投影显示
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.LightStyle =  System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;//设置图表的照明类型(色调随旋转角度改变而改变，不应用照明，色调不改变)
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Perspective = 50;//设置三维图区的透视百分比
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Rotation = 60;//设置三维图表区域绕垂直轴旋转的角度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.WallWidth = 0;//设置三维图区中显示的墙的宽度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Enable3D = true;//设置是否显示3D效果
                                                                  //chart1.ChartAreas["C1"].BackColor = System.Drawing.Color.Green;//设置图表区域的背景颜色
                                                                  //chart1.ChartAreas["C1"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.ChartAreas["C1"].BackSecondaryColor = System.Drawing.Color.White;//设置图表区域的辅助颜色
            chart1.ChartAreas["C1"].BorderColor = System.Drawing.Color.White;//设置图表区域边框颜色
            chart1.ChartAreas["C1"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)



            chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 2;//设置主网格线与次要网格线的间隔
            chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 2;//设置刻度线的间隔


            chart1.ChartAreas["C1"].AxisX2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
                                                                                                                                        //chart1.ChartAreas["C1"].AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
                                                                                                                                        // chart1.ChartAreas["C1"].AxisX2.LabelStyle.Format = "MM-dd";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX2.IsLabelAutoFit = true;//设置是否自动调整轴标签
                                                                 //chart1.ChartAreas["C1"].AxisX2.MajorGrid.Interval = 2;//设置主网格线与次要网格线的间隔
                                                                 // chart1.ChartAreas["C1"].AxisX2.MajorTickMark.Interval = 2;//设置刻度线的间隔








            //chart1.ChartAreas["C1"].AxisX.LabelStyle.Interval = 10D;//设置标签间隔的大小            
            //chart1.ChartAreas["C1"].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置间隔大小的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置主网格线与次网格线的间隔的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置刻度线的间隔的度量单位

            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisY2.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY2.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性




            //chart1.ChartAreas["C1"].AxisY.LineColor = System.Drawing.Color.DarkBlue;//设置轴的线条颜色
            //chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;//设置网格线颜色
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Snow;//设置网格线的颜色
            //chart1.ChartAreas["C1"].AxisX.LineColor = System.Drawing.Color.White;//设置X轴的线条颜色
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，
            chart1.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  

            #endregion

            #region spec calculation

            double axisYmax = 2;
            if (axisYmax < (double)dt.Compute("Max(JobIn)", "true") && (double)dt.Compute("Max(JobIn)", "true") > 8)
            { axisYmax = (double)dt.Compute("Max(JobIn)", "true"); }
            if (axisYmax < (double)dt.Compute("Max(Feedback)", "true") && (double)dt.Compute("Max(Feedback)", "true") > 8)
            { axisYmax = (double)dt.Compute("Max(Feedback)", "true"); }
            if (axisYmax < (double)dt.Compute("Max(Optimum)", "true") && (double)dt.Compute("Max(Optimum)", "true") > 8)
            { axisYmax = (double)dt.Compute("Max(Optimum)", "true"); }
            double axisYmin = 888.1;
            if (axisYmin > (double)dt.Compute("Min(JobIn)", "true") && (double)dt.Compute("Min(JobIn)", "true") > 8)
            { axisYmin = (double)dt.Compute("Min(JobIn)", "true"); }
            if (axisYmin > (double)dt.Compute("Min(Feedback)", "true") && (double)dt.Compute("Min(Feedback)", "true") > 8)
            { axisYmin = (double)dt.Compute("Min(Feedback)", "true"); }
            if (axisYmin > (double)dt.Compute("Min(Optimum)", "true") && (double)dt.Compute("Min(Optimum)", "true") > 8)
            { axisYmin = (double)dt.Compute("Min(Optimum)", "true"); }






            chart1.ChartAreas["C1"].AxisY.Maximum = Math.Round(axisYmax * 1.01, 1);
            chart1.ChartAreas["C1"].AxisY.Minimum = Math.Round(axisYmin * 0.98, 1);



            double axisY2max = (double)dt.Compute("Max(cdU)", "true");
            double axisY2min = (double)dt.Compute("Min(cdL)", "true");


            chart1.ChartAreas["C1"].AxisY2.Maximum = Math.Round(axisY2max, 2) + 0.005;
            chart1.ChartAreas["C1"].AxisY2.Minimum = Math.Round(axisY2min, 2) - 0.005;


            if (chart1.Visible == false)
            {
                chart1.Visible = true;
                chart1.BringToFront();
                panel6.Visible = true;
                panel6.BringToFront();

            }

            #endregion

        }
        private void DrawChartOvlB(string choice)
        {
            #region getdata

            ClearMemory();
            DataRow[] drs; dt1 = dtShow.Clone();

            if (choice == "B")
            {
                if (radioBy3.Checked)  //by part layer tool
                {
                    drs = dtShow.Select("PartID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and Layer='" + listView1.SelectedItems[0].SubItems[1].Text + "' and  ProcEqID='" + listView1.SelectedItems[0].SubItems[2].Text + "'");

                }
                else //by part layer
                {
                    drs = dtShow.Select("PartID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and Layer='" + listView1.SelectedItems[0].SubItems[1].Text + "'");
                }
            }
            else if (choice == "D")
            {
                drs = dtShow.Select("ProcEqID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LayerID like '%" + listView1.SelectedItems[0].SubItems[1].Text + "%'");
            }
            else if (choice == "E")
            {
                if (radioBy3.Checked)  //by part layer tool
                {
                    drs = dtShow.Select("ProcEqID='" + listView1.SelectedItems[0].SubItems[0].Text + "' and Layer like '%" + listView1.SelectedItems[0].SubItems[2].Text + "%'");

                }
                else //by part layer
                {
                    drs = dtShow.Select("ProcEqID='" + listView1.SelectedItems[0].SubItems[0].Text + "'");
                }

            }
            else
            {
                drs = null;
            }
            foreach (var row in drs)
            { dt1.ImportRow(row); }
            dt1.DefaultView.Sort = "DcollTime ASC";
            dataGridView1.DataSource = dt1;
            drs = null; ClearMemory();
            #endregion

            SingleParameterOvlPlot("tran-x");


        }
        private void DrawChartRwkF(string choice)
        {
            #region initialize plot
            List<string> l = new List<string>();
            foreach (var series in chart1.Series)
            { l.Add(series.Name); }
            foreach (string x in l)
            { chart1.Series.Remove(chart1.Series[x]); }  //直接删除有问题？
            chart1.ChartAreas[0].Name = "C1";
            chart1.ChartAreas["C1"].AxisY.Maximum = 9999999.0;
            chart1.ChartAreas["C1"].AxisY.Minimum = 0;
            chart1.ChartAreas["C1"].AxisY2.Maximum = 9999999.0;
            chart1.ChartAreas["C1"].AxisY2.Minimum = 0;
            #endregion

            #region getdata

            ClearMemory();
            DataRow[] drs; dt1 = dtShow.Clone();
            string category = listView1.SelectedItems[0].SubItems[1].Text;
            string eqptype = string.Empty;
            double goalByLot = 0, goalByWfr = 0;
            double axisYmax = 0;

            if (category == "光刻部") { category = "_ALL"; goalByLot = 3.2; goalByWfr = 1.6; }
            else if (category == "量测设备课") { category = "_Metrology"; goalByLot = 0; goalByWfr = 0; }
            else if (category == "光刻工艺课") { category = "_PE1"; goalByLot = 0; goalByWfr = 0; }
            else if (category == "涂胶显影工艺课") { category = "_PE2"; goalByLot = 0; goalByWfr = 0; }
            else if (category == "光刻设备刻") { category = "_ScannerStepper"; goalByLot = 0.66; goalByWfr = 0.2; }
            else if (category == "涂胶显影设备课") { category = "_Track"; goalByLot = 0.59; goalByWfr = 0.2; }
            else if (category == "ASML FEOL") { category = "FEOL"; eqptype = "ASML"; goalByLot = 2.3; goalByWfr = 1.2; }
            else if (category == "ASML BEOL") { category = "BEOL"; eqptype = "ASML"; goalByLot = 2.3; goalByWfr = 1.2; }
            else if (category == "NIKON FEOL") { category = "FEOL"; eqptype = "NIKON"; goalByLot = 1.8; goalByWfr = 1.1; }
            else { category = "BEOL"; eqptype = "NIKON"; goalByLot = 1.8; goalByWfr = 1.1; }


            string y = DateTime.Now.Year.ToString();

            drs = dtShow.Select("CATEGORY='" + category + "' and EQPTYPE='" + eqptype + "' and (RIQI like 'MM" + y + "%' or RIQI like 'WW" + y + "%')");
            foreach (var row in drs) { dt1.ImportRow(row); }
            dataGridView1.DataSource = dt1;
            drs = null; ClearMemory();

            string[] mon = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun","Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            for (int i = 1; i < 12; i++)
            {
                DataRow newRow = dt1.NewRow();
                newRow[0] = mon[i - 1];
                string riqi = "MM" + y;
                if (i < 10) { riqi += "0" + i.ToString(); }
                else { riqi += i.ToString(); }
                drs = dt1.Select("RIQI='" + riqi + "'");
                if (drs.Length > 0)
                {
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    { newRow[j] = drs[0][j].ToString(); }
                    dt1.Rows.Remove(drs[0]);
                }
                else
                {
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    { newRow[j] = "0"; }

                }

                dt1.Rows.Add(newRow);
            }

            foreach (DataRow x in dt1.Rows)
            {
                if (x[0].ToString().Contains("WW" + y))
                {
                    x[0] = "W" + x[0].ToString().Substring(6, 2);
                }
            }
            dataGridView1.DataSource = dt1;
            #endregion











            #region 图表样式
            chart1.BackColor = System.Drawing.Color.LightCyan;//设置图表的背景颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightYellow;//设置背景的辅助颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightYellow;//设置背景的辅助颜色
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
                                                                                                               //chart1.BorderlineColor = System.Drawing.Color.Yellow;//设置图像边框的颜色           
                                                                                                               // chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 0;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;//设置图像的边框外观样式
            #endregion


            #region 图表标题
            chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                 listView1.SelectedItems[0].SubItems[1].Text + "返工率统计:" + listView1.SelectedItems[0].SubItems[2].Text,
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);
            chart1.ChartAreas[0].AxisY.Title = "";
            chart1.ChartAreas[0].AxisY2.Title = "";

            #endregion


            #region 数据绑定
            List<string> x1 = new List<string>();
            List<string> x2 = new List<string>();
            List<double> y1 = new List<double>();
            List<double> y2 = new List<double>();
            List<double> goal = new List<double>();











            drs = dt1.Select("RIQI like 'W%'");


            if (listView1.SelectedItems[0].SubItems[2].Text == "ByLot")
            {
                foreach (DataRow row in drs)
                {
                    x1.Add(row["RIQI"].ToString());
                    y1.Add(Convert.ToDouble(row["LOTRATIO"].ToString()));
                    if (goalByLot > 0.1) { goal.Add(goalByLot); } else { goal.Add(0); }
                }

                drs = dt1.Select("RIQI not like 'W%'");
                foreach (DataRow row in drs)
                {
                    x2.Add(row["RIQI"].ToString());
                    y2.Add(Convert.ToDouble(row["LOTRATIO"].ToString()));
                }
            }
            else
            {
                foreach (DataRow row in drs)
                {
                    x1.Add(row["RIQI"].ToString());
                    y1.Add(Convert.ToDouble(row["WFRRATIO"].ToString()));
                    if (goalByWfr > 0.1) { goal.Add(goalByWfr); } else { goal.Add(0); }
                }

                drs = dt1.Select("RIQI not like 'W%'");
                foreach (DataRow row in drs)
                {
                    x2.Add(row["RIQI"].ToString());
                    y2.Add(Convert.ToDouble(row["WFRRATIO"].ToString()));
                }
            }

            #region spec calculation

            if (listView1.SelectedItems[0].SubItems[2].Text == "ByLot")
            { axisYmax = goalByLot; }
            else
            { axisYmax = goalByWfr; }



            foreach (double x in y1)
            { if (axisYmax < x) { axisYmax = x; } }
            foreach (double x in y2)
            { if (axisYmax < x) { axisYmax = x; } }

            axisYmax = Math.Round(axisYmax, 1);

            if (listView1.SelectedItems[0].SubItems[2].Text == "ByLot")
            {
                chart1.ChartAreas["C1"].AxisY.Maximum = axisYmax + 0.1;
                chart1.ChartAreas["C1"].AxisY2.Maximum = axisYmax + 0.1;
            }
            else
            {
                chart1.ChartAreas["C1"].AxisY.Maximum = axisYmax + 0.1;
                chart1.ChartAreas["C1"].AxisY2.Maximum = axisYmax + 0.1;
            }
            #endregion



            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Monthly Rework Ratio"));
            chart1.Series["Monthly Rework Ratio"].ChartArea = "C1";
            chart1.Series["Monthly Rework Ratio"].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            chart1.Series["Monthly Rework Ratio"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            chart1.Series["Monthly Rework Ratio"].Points.DataBindXY(x2, y2);

            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Weekly Rework Ratio"));
            chart1.Series["Weekly Rework Ratio"].ChartArea = "C1"; ;
            chart1.Series["Weekly Rework Ratio"].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            chart1.Series["Weekly Rework Ratio"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            chart1.Series["Weekly Rework Ratio"].Points.DataBindXY(x1, y1);


            if (goalByLot > 0.1)
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Goal"));
                chart1.Series["Goal"].ChartArea = "C1";
                chart1.Series["Goal"].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                chart1.Series["Goal"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                chart1.Series["Goal"].Points.DataBindXY(x1, goal);
            }




            #endregion

            #region 数据样式
            chart1.Series["Weekly Rework Ratio"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series["Weekly Rework Ratio"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
            chart1.Series["Weekly Rework Ratio"].MarkerSize = 12;
            chart1.Series["Weekly Rework Ratio"].MarkerColor = System.Drawing.Color.Red;
            chart1.Series["Weekly Rework Ratio"].BorderWidth = 2;
            chart1.Series["Weekly Rework Ratio"].Color = Color.Red;
            chart1.Series["Weekly Rework Ratio"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chart1.Series["Weekly Rework Ratio"].Label = "#VAL";
            chart1.Series["Weekly Rework Ratio"].LabelBorderColor = Color.Red;
            chart1.Series["Weekly Rework Ratio"].LabelForeColor = Color.Red;


            if (goalByLot > 0.1)
            {
                chart1.Series["Goal"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
                chart1.Series["Goal"].MarkerSize = 0;
                chart1.Series["Goal"].BorderWidth = 2;
                chart1.Series["Goal"].Color = Color.DarkGreen;
                chart1.Series["Goal"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            }




            chart1.Series["Monthly Rework Ratio"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;//设置图表的类型(饼状、线状等等)
            chart1.Series["Monthly Rework Ratio"].BorderWidth = 1;
            chart1.Series["Monthly Rework Ratio"].Color = System.Drawing.Color.FromArgb(70, Color.Blue);
            chart1.Series["Monthly Rework Ratio"]["DrawingStyle"] = "Emboss"; //设置柱状平面形状
            chart1.Series["Monthly Rework Ratio"]["PointWidth"] = "0.5"; //设置柱状大小
            chart1.Series["Monthly Rework Ratio"].Label = "#VAL";
            chart1.Series["Monthly Rework Ratio"].LabelBorderColor = Color.Blue;
            chart1.Series["Monthly Rework Ratio"].LabelForeColor = Color.Blue;
            chart1.Series["Monthly Rework Ratio"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            chart1.Series["Monthly Rework Ratio"].MarkerSize = 5;
            chart1.Series["Monthly Rework Ratio"].MarkerColor = System.Drawing.Color.Blue;

            // chart1.Series[4].ToolTip = "#VAL"; //鼠标移动到对应点显示数值

            #endregion

            #region 图例样式
            chart1.ChartAreas[0].Name = "C1";
            chart1.Legends[0].Enabled = false;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Near;
            chart1.Legends[0].BackColor = System.Drawing.Color.LightYellow;//设置图例的背景颜色
                                                                           // chart1.Legends[0].DockedToChartArea = "C1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"

            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)

            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;//设置显示图例项方式(多列一行、一列多行、多列多行)
            #endregion

            #region 图表区域样式

            chart1.ChartAreas[0].Name = "C1";
            chart1.ChartAreas["C1"].Position.Auto = true;//设置是否自动设置合适的图表元素
            chart1.ChartAreas["C1"].InnerPlotPosition.Auto = true;//设置是否在内部绘图区域中自动设置合适的图表元素

            chart1.ChartAreas["C1"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;



            chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX.IsLabelAutoFit = true;//设置是否自动调整轴标签



            chart1.ChartAreas["C1"].AxisX2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的
            chart1.ChartAreas["C1"].AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX2.IsLabelAutoFit = true;//设置是否自动调整轴标签

            //chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = Color.Transparent;
            //chart1.ChartAreas["C1"].AxisX2.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas["C1"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["C1"].AxisX2.MajorGrid.Enabled = false;






            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisY2.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY2.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性


            chart1.ChartAreas["C1"].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas["C1"].AxisY2.MajorGrid.Enabled = false;
            chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = Color.FromArgb(64, Color.Black);//数据区域，纵向的线条颜色  
            chart1.ChartAreas["C1"].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas["C1"].AxisY.MinorGrid.LineColor = Color.FromArgb(30, Color.Black);//数据区域，纵向的线条颜色  
            chart1.ChartAreas["C1"].AxisY2.LabelStyle.Enabled = false;
            chart1.ChartAreas["C1"].AxisY2.MajorTickMark.Enabled = false;
            //
            // 
            #endregion



            chart1.Visible = true;
            chart1.BringToFront();
            panel6.Visible = true;
            panel6.BringToFront();





            //  SingleParameterOvlPlot("tran-x");

        }

        private void TranX_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("tran-x");
        }
        private void TranY_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("tran-y");
        }
        private void ExpX_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("exp-x");
        }

        private void ExpY_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("exp-y");
        }

        private void Ort_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("non-ort");
        }

        private void W_ROT_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("w-rot");
        }

        private void Mag_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("mag");
        }

        private void Rot_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("rot");
        }

        private void AMag_Click(object sender, EventArgs e)
        {

            SingleParameterOvlPlot("asym-mag");


        }

        private void ARot_Click(object sender, EventArgs e)
        {
            SingleParameterOvlPlot("asym-rot");
        }






        private void SingleParameterOvlPlot(string item)
        {



            #region initialize plot
            List<string> l = new List<string>();
            foreach (var series in chart1.Series)
            { l.Add(series.Name); }
            foreach (string x in l)
            { chart1.Series.Remove(chart1.Series[x]); }  //直接删除有问题？
            chart1.ChartAreas[0].Name = "C1";

            //显示坐标副轴，因在返工图中隐藏了
            chart1.ChartAreas["C1"].AxisY2.MajorGrid.Enabled = true;
            chart1.ChartAreas["C1"].AxisY2.LabelStyle.Enabled = true;
            chart1.ChartAreas["C1"].AxisY2.MajorTickMark.Enabled = true;

            #endregion

            dt1 = (dataGridView1.DataSource as DataTable);

            #region define Ovl Parameter

            string[] para = new string[] { "TrxJobin", "TrxFb", "TrxOpt", "TrxValue", "TrxMin", "TrxMax", "TryMin", "TryMax" };
            if (item == "tran-x")
            { }
            else if (item == "tran-y")
            {
                para[0] = "TryJobin";
                para[1] = "TryFb";
                para[2] = "TryOpt";
                para[3] = "TryValue";
            }
            else if (item == "exp-x")
            {
                para[0] = "ExpxJobin";
                para[1] = "ExpxFb";
                para[2] = "ExpxOpt";
                para[3] = "ExpxValue";

            }
            else if (item == "exp-y")
            {
                para[0] = "ExpyJobin";
                para[1] = "ExpyFb";
                para[2] = "ExpyOpt";
                para[3] = "ExpyValue";

            }
            else if (item == "non-ort")
            {
                para[0] = "OrtJobin";
                para[1] = "OrtFb";
                para[2] = "OrtOpt";
                para[3] = "OrtValue";
            }
            else if (item == "w-rot")
            {
                para[0] = "RotJobin";
                para[1] = "RotFb";
                para[2] = "RotOpt";
                para[3] = "RotValue";

            }
            else if (item == "mag")
            {
                para[0] = "SMagJobin";
                para[1] = "SMagFb";
                para[2] = "SMagOpt";
                para[3] = "SMagValue";
            }
            else if (item == "rot")
            {
                para[0] = "SRotJobin";
                para[1] = "SRotFb";
                para[2] = "SRotOpt";
                para[3] = "SRotValue";

            }
            else if (item == "asym-mag")
            {
                para[0] = "ASMagJobin";
                para[1] = "ASMagFb";
                para[2] = "ASMagOpt";
                para[3] = "ASMagValue";
            }
            else //asym-rot
            {
                para[0] = "ASRotJobin";
                para[1] = "ASRotFb";
                para[2] = "ASRotOpt";
                para[3] = "ASRotValue";
            }
            #endregion

            #region 图表样式
            chart1.BackColor = System.Drawing.Color.LightCyan;//设置图表的背景颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightBlue;//设置背景的辅助颜色
            chart1.BackSecondaryColor = System.Drawing.Color.LightBlue;//设置背景的辅助颜色
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.BorderlineColor = System.Drawing.Color.LightBlue;//设置图像边框的颜色           
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 0;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;//设置图像的边框外观样式
            #endregion

            #region 图表标题
            chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "OVL PLOT: " + item.ToUpper(),
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue); ;
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);


            chart1.ChartAreas[0].AxisY.Title = item + "_Jobin_FB_Opt_Measured";
            chart1.ChartAreas[0].AxisY2.Title = "Max_X/Y or Min_X/Y";
            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Trebuchet MS", 5F, System.Drawing.FontStyle.Bold);
            chart1.ChartAreas[0].AxisY2.TitleFont = new System.Drawing.Font("Trebuchet MS", 5F, System.Drawing.FontStyle.Bold);

            #endregion

            #region 数据绑定


            if (listView1.Columns[1].Text == "E")
            {


                // 0 参照线

                for (int i = 0; i < dt1.Rows.Count; i++)
                { dt1.Rows[i][para[0]] = 0; }

            }


            this.chart1.DataSource = dt1;

            if (dt1.Rows[0][para[0]].ToString() == "")
            {
                MessageBox.Show("Asy_Mag & Asym_Rot Is Not Available For Nikon,Exit"); return;
            }





            for (int i = 0; i < 4; i++)
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series(para[i]));
                chart1.Series[para[i]].ChartArea = "C1";
                chart1.Series[para[i]].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                chart1.Series[para[i]].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;

                this.chart1.Series[para[i]].YValueMembers = para[i];
                if (listView1.Columns[1].Text == "E")
                {
                    this.chart1.Series[para[i]].XValueMember = "DcollTime";
                }
                else
                {
                    this.chart1.Series[para[i]].XValueMember = "LotID";
                }
                this.chart1.Series[i].Name = para[i];
                if (i == 2) { this.chart1.Series[para[2]].XValueMember = "DcollTime"; }

            }
















            if (checkBoxMaxMin.Checked)
            {
                for (int i = 4; i < 6; i++)
                {
                    chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series(para[i]));
                    chart1.Series[para[i]].ChartArea = "C1";
                    chart1.Series[para[i]].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    chart1.Series[para[i]].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;

                    this.chart1.Series[para[i]].YValueMembers = para[i];
                    this.chart1.Series[para[i]].XValueMember = "DcollTime";
                    this.chart1.Series[para[i]].Name = para[i];
                }
            }


            if (checkBoxMaxMin_Y.Checked)
            {
                for (int i = 6; i < 8; i++)
                {
                    chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series(para[i]));
                    chart1.Series[para[i]].ChartArea = "C1";
                    chart1.Series[para[i]].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    chart1.Series[para[i]].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
                    this.chart1.Series[para[i]].YValueMembers = para[i];
                    this.chart1.Series[para[i]].XValueMember = "DcollTime";
                    this.chart1.Series[para[i]].Name = para[i];
                }
            }

            this.chart1.DataBind();

            #endregion





            #region 数据样式
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            chart1.Series[0].MarkerSize = 8;
            chart1.Series[0].MarkerColor = System.Drawing.Color.Blue;
            chart1.Series[0].BorderWidth = 1;
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            chart1.Series[1].MarkerSize = 8;
            chart1.Series[1].MarkerColor = System.Drawing.Color.Purple;
            chart1.Series[1].BorderWidth = 1;
            chart1.Series[1].Color = Color.Purple;
            chart1.Series[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;

            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[2].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart1.Series[2].MarkerSize = 8;
            chart1.Series[2].MarkerColor = System.Drawing.Color.Red;
            chart1.Series[2].BorderWidth = 1;
            chart1.Series[2].Color = Color.Red;
            chart1.Series[2].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//设置图表的类型(饼状、线状等等)
            chart1.Series[3].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            chart1.Series[3].MarkerSize = 8;
            chart1.Series[3].MarkerColor = System.Drawing.Color.Black;
            chart1.Series[3].BorderWidth = 1;
            chart1.Series[3].Color = Color.Black;
            chart1.Series[3].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;


            //Max/Min

            if (checkBoxMaxMin.Checked)
            {
                for (int i = 4; i < 6; i++)
                {
                    chart1.Series[para[i]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chart1.Series[para[i]].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
                    chart1.Series[para[i]].MarkerSize = 14;
                    chart1.Series[para[i]].MarkerColor = System.Drawing.Color.DeepPink;
                    chart1.Series[para[i]].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
                }
            }

            if (checkBoxMaxMin_Y.Checked)
            {

                for (int i = 6; i < 8; i++)
                {
                    chart1.Series[para[i]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chart1.Series[para[i]].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
                    chart1.Series[para[i]].MarkerSize = 14;
                    chart1.Series[para[i]].MarkerColor = System.Drawing.Color.Green;
                    chart1.Series[para[i]].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
                }
            }



            #endregion

            #region 图例样式

            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].BackColor = System.Drawing.Color.LightBlue;//设置图例的背景颜色
                                                                         // chart1.Legends[0].DockedToChartArea = "C1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)
            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;//设置显示图例项方式(多列一行、一列多行、多列多行)
            #endregion

            #region 图表区域样式

            chart1.ChartAreas[0].Name = "C1";

            //chart1.ChartAreas[0].AxisX.LineColor = Color.Red;//X轴颜色   
            //chart1.ChartAreas[0].AxisY.LineColor = Color.Green;//Y轴颜色   
            //chart1.ChartAreas[0].AxisX.LineWidth = 2;         //X轴宽度   
            //chart1.ChartAreas[0].AxisY.LineWidth = 2;          //Y轴宽度 

            chart1.ChartAreas["C1"].Position.Auto = true;//设置是否自动设置合适的图表元素

            //chart1.ChartAreas["C1"].Position.Width = 100;//绘图区域在控件中的宽度 100是百分比
            //chart1.ChartAreas["C1"].Position.Height = 80;
            //chart1.ChartAreas["C1"].Position.X = 1;//绘图区域在控件中的绝对位置 横坐标
            //chart1.ChartAreas["C1"].Position.Y = 8; //绘图区域在控件中的绝对位置纵坐标



            //chart1.ChartAreas["C1"].ShadowColor = System.Drawing.Color.YellowGreen;//设置图表的阴影颜色
            // chart1.ChartAreas["C1"].Position.X = 2F;//设置图表元素左上角对应的X坐标
            //chart1.ChartAreas["C1"].Position.Y = 2F;//设置图表元素左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Position.Height = 86.76062F;//设置图表元素的高度
            //chart1.ChartAreas["C1"].Position.Width = 88F;//设置图表元素的宽度



            chart1.ChartAreas["C1"].InnerPlotPosition.Auto = true;//设置是否在内部绘图区域中自动设置合适的图表元素
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Height = 85F;//设置图表元素内部绘图区域的高度
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Width = 86F;//设置图表元素内部绘图区域的宽度
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.X = 8.3969F;//设置图表元素内部绘图区域左上角对应的X坐标
                                                                  //chart1.ChartAreas["C1"].InnerPlotPosition.Y = 3.63068F;//设置图表元素内部绘图区域左上角对应的Y坐标
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Inclination = 10;//设置三维图表的旋转角度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.IsClustered = true;//设置条形图或柱形图的的数据系列是否为簇状
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.IsRightAngleAxes = true;//设置图表区域是否使用等角投影显示
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.LightStyle =  System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;//设置图表的照明类型(色调随旋转角度改变而改变，不应用照明，色调不改变)
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Perspective = 50;//设置三维图区的透视百分比
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Rotation = 60;//设置三维图表区域绕垂直轴旋转的角度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.WallWidth = 0;//设置三维图区中显示的墙的宽度
                                                                  //chart1.ChartAreas["C1"].Area3DStyle.Enable3D = true;//设置是否显示3D效果
                                                                  //chart1.ChartAreas["C1"].BackColor = System.Drawing.Color.Green;//设置图表区域的背景颜色
                                                                  //chart1.ChartAreas["C1"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.ChartAreas["C1"].BackSecondaryColor = System.Drawing.Color.White;//设置图表区域的辅助颜色
            chart1.ChartAreas["C1"].BorderColor = System.Drawing.Color.White;//设置图表区域边框颜色
            chart1.ChartAreas["C1"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)



            chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 2;//设置主网格线与次要网格线的间隔
            chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 2;//设置刻度线的间隔



            chart1.ChartAreas["C1"].AxisX2.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 6F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
                                                                                                                                        //chart1.ChartAreas["C1"].AxisX2.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisX2.IsLabelAutoFit = true;//设置是否自动调整轴标签
                                                                 //chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 2;//设置主网格线与次要网格线的间隔
                                                                 //chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 2;//设置刻度线的间隔




            //chart1.ChartAreas["C1"].AxisX.LabelStyle.Interval = 10D;//设置标签间隔的大小            
            //chart1.ChartAreas["C1"].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置间隔大小的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置主网格线与次网格线的间隔的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置刻度线的间隔的度量单位

            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性




            //chart1.ChartAreas["C1"].AxisY.LineColor = System.Drawing.Color.DarkBlue;//设置轴的线条颜色
            //chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;//设置网格线颜色
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Snow;//设置网格线的颜色
            //chart1.ChartAreas["C1"].AxisX.LineColor = System.Drawing.Color.White;//设置X轴的线条颜色
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，
            chart1.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  

            #endregion







            if (chart1.Visible == false)
            {
                chart1.Visible = true;
                chart1.BringToFront();
                panel6.Visible = true;
                panel6.BringToFront();

            }










            //设备趋势图，只显示优化值
            if (listView1.Columns[1].Text == "E")
            {


                //零参照线
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[0].MarkerSize = 0;
                chart1.Series[0].MarkerColor = System.Drawing.Color.Purple;
                chart1.Series[0].BorderWidth = 1;
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

                this.chart1.Series[1].Points.Clear();
                chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                this.chart1.Series[3].Points.Clear();

                try
                {
                    for (int k = 4; k < 8; k++)
                    { this.chart1.Series[k].Points.Clear(); }
                }
                catch
                { }

            }

            double axisYmax = -99999.0;
            double axisYmin = 99999.0;

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (axisYmax < (double)dt1.Compute("Max(" + para[i] + ")", "true")) { axisYmax = (double)dt1.Compute("Max(" + para[i] + ")", "true"); }
                    if (axisYmin > (double)dt1.Compute("Min(" + para[i] + ")", "true")) { axisYmin = (double)dt1.Compute("Min(" + para[i] + ")", "true"); }
                }
            }
            catch
            {
                MessageBox.Show("ASYM_ROT/ASYM_MAG NOT AVAILABLE FOR NIKON");
            }


            chart1.ChartAreas["C1"].AxisY.Maximum = 99999;
            chart1.ChartAreas["C1"].AxisY.Minimum = -9999; //否则设置规范时会报错

            //趋势图限定规范

            if (listView1.Columns[1].Text == "E")
            {
                if (item == "tran-x" || item == "tran-y")
                {
                    if (axisYmax > 0.07) { axisYmax = 0.07; }
                    if (axisYmin < -0.07) { axisYmin = -0.07; }
                }
                else if (item == "exp-x" || item == "exp-y" || item == "non-ort" || item == "w-rot")
                {
                    if (axisYmax > 0.7) { axisYmax = 0.7; }
                    if (axisYmin < -0.7) { axisYmin = -0.7; }
                }
                else
                {
                    if (axisYmax > 5.9) { axisYmax = 5.9; }
                    if (axisYmin < -5.9) { axisYmin = -5.9; }
                }
            }



            if (item.Substring(0, 2) == "tr")
            {
                chart1.ChartAreas["C1"].AxisY.Maximum = Math.Round(axisYmax + 0.01, 2);
                chart1.ChartAreas["C1"].AxisY.Minimum = Math.Round(axisYmin - 0.01, 2);
            }
            else
            {
                chart1.ChartAreas["C1"].AxisY.Maximum = Math.Round(axisYmax + 0.1, 2);
                chart1.ChartAreas["C1"].AxisY.Minimum = Math.Round(axisYmin - 0.1, 2);
            }







            double axisY2max = -99999.0;
            double axisY2min = 99999.0;

            if (checkBoxMaxMin.Checked)
            {
                for (int i = 4; i < 6; i++)
                {
                    if (axisY2max < (double)dt1.Compute("Max(" + para[i] + ")", "true")) { axisY2max = (double)dt1.Compute("Max(" + para[i] + ")", "true"); }
                    if (axisY2min > (double)dt1.Compute("Min(" + para[i] + ")", "true")) { axisY2min = (double)dt1.Compute("Min(" + para[i] + ")", "true"); }
                }
            }
            if (checkBoxMaxMin_Y.Checked)
            {
                for (int i = 6; i < 8; i++)
                {
                    if (axisY2max < (double)dt1.Compute("Max(" + para[i] + ")", "true")) { axisY2max = (double)dt1.Compute("Max(" + para[i] + ")", "true"); }
                    if (axisY2min > (double)dt1.Compute("Min(" + para[i] + ")", "true")) { axisY2min = (double)dt1.Compute("Min(" + para[i] + ")", "true"); }
                }
            }
            else
            {
                axisY2max = 0.1; axisY2min = -0.1;
            }

            chart1.ChartAreas["C1"].AxisY2.Maximum = 99999;
            chart1.ChartAreas["C1"].AxisY2.Minimum = -9999; //否则设置规范时会报错



            chart1.ChartAreas["C1"].AxisY2.Maximum = Math.Round(axisY2max + 0.01, 2);
            chart1.ChartAreas["C1"].AxisY2.Minimum = Math.Round(axisY2min - 0.01, 2);




            if (chart1.Visible == false)
            {
                chart1.Visible = true;
                chart1.BringToFront();
                panel6.Visible = true;
                panel6.BringToFront();


            }




        }



        private void chart1_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {

            if (e.HitTestResult.ChartElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                //  System.Windows.Forms.DataVisualization.Charting.DataPoint dp = e.HitTestResult.Series.Points[i];
                // e.Text = string.Format("次数:{0};数值:{1:F3} ", dp.XValue, dp.YValues[0]);            
                DataTable dt1 = (dataGridView1.DataSource as DataTable);

                try
                {
                    e.Text = string.Format("" +
                        "Part:      " + dt1.Rows[i][1].ToString() + "\r\n" +
                        "Layer:     " + dt1.Rows[i][2].ToString() + "\r\n" +
                        "LotID:     " + dt1.Rows[i][3].ToString() + "\r\n" +
                        "Tool:      " + dt1.Rows[i][4].ToString() + "\r\n" +
                        "Metro:     " + dt1.Rows[i][6].ToString() + "\r\n" +
                        "DcollTime: " + dt1.Rows[i][7].ToString() + "\r\n" +
                        "Jobin:     " + dt1.Rows[i][8].ToString() + "\r\n" +
                        "Feedback:  " + dt1.Rows[i][9].ToString() + "\r\n" +
                        "Optimum:   " + dt1.Rows[i][10].ToString() + "\r\n" +
                        "Measured:        " + Math.Round(Convert.ToDouble(dt1.Rows[i][23]), 4).ToString());
                }
                catch
                { }

            }
        }


        #endregion

        #region AsmlErrorLog
        private void button2_Click(object sender, EventArgs e)
        {


            dtShow = null; dtShow = new DataTable();
            string riqi = DateTime.Now.AddDays(-8).ToString("yyMMdd");
            Asml.readLog(riqi, ref dtShow);
            dataGridView1.DataSource = dtShow;
            MessageBox.Show("Extraction Done\n\n\nRecords Quantities:" + dtShow.Rows.Count.ToString());
            this.tabControl1.SelectedIndex = 0; ShowGridview();
        }
        #endregion
        
        #region Nikon Sequence Log
        //read and extrct
        private void button3_Click(object sender, EventArgs e)
        {
            int days;
            try
            { days = int.Parse(textBox6.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: \n\n" + ex.Message + "\n\n抽取天数请输入数字");
                return;
            }


            string tool = string.Empty;
            myDic = new Dictionary<string, DataTable[]>();
            foreach (RadioButton x in panel7.Controls)
            { if (x.Checked) { tool = x.Name; break; } }

            try
            {
                bool boolSaveRaw = !(radioButton4.Checked);
                bool[] boolArr = new bool[6] { false, false, false, false, false,false };
                if (checkBoxLot.Checked) { boolArr[0] = true; } 
                if (checkBoxAssist.Checked) { boolArr[1] = true; }
                if (checkBoxOFretry.Checked) { boolArr[2] = true; }
                if (checkBoxErr.Checked) { boolArr[3] = true; }
                if (checkBoxWfr.Checked) { boolArr[4] = true; }
                if (checkBoxBaseline.Checked) { boolArr[5] = true; }
                classSeqLog.readAllLog(days, tool, ref myDic,boolSaveRaw,boolArr); 
            }
           catch (Exception ex)
            {
               MessageBox.Show("Error Code: \n\n" + ex.Message + "\n\n" + tool + ": Log读取错误，请确认");
            }
        }
        //view raw data
        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                if (myDic[tool][0].Rows.Count > 0)
                { dataGridView1.DataSource = myDic[tool][0]; }
                else
                { MessageBox.Show("原始数据选择了不保存，无数据显示"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }


        }
        //view lot data
        private void button7_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][1].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }

        }
        //view assist
        private void button8_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][2].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }

        }
        //view of retry
        private void button9_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][3].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }
        }

        //view error
        private void button10_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][4].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }
        }

        //view wfr
        private void button11_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][5].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }
        }
        //view summary
        private void button6_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            {
                dtShow = null; dtShow = new DataTable();
                dtShow.Columns.Add("a1_Tool", typeof(string));
                dtShow.Columns.Add("a2_Description", typeof(string));
                dtShow.Columns.Add("a3_Date", typeof(string));
                dtShow.Columns.Add("a4_Count", typeof(double));
                dtShow.Columns.Add("a5_TotalSeconds", typeof(double));
                dtShow.Columns.Add("a6_TotalMinuts", typeof(double));
                foreach (KeyValuePair<string, DataTable[]> kvp in myDic)
                {
                    for (int i = 0; i < kvp.Value[6].Rows.Count; ++i)
                    {
                        DataRow newRow = dtShow.NewRow();
                        newRow[0] = kvp.Key;
                        newRow[1] = kvp.Value[6].Rows[i][0].ToString();
                        newRow[2] = kvp.Value[6].Rows[i][1].ToString();
                        newRow[3] = kvp.Value[6].Rows[i][2].ToString();

                        if (kvp.Value[6].Rows[i][3].ToString() == "")
                        {
                            newRow[4] = 0;
                            newRow[5] = 0;
                        }
                        else
                        {
                            newRow[4] = kvp.Value[6].Rows[i][3];

                            newRow[5] = Math.Round(double.Parse(kvp.Value[6].Rows[i][3].ToString()) / 60.0, 0);
                        }
                        dtShow.Rows.Add(newRow);
                    }
                }
                dtShow.DefaultView.Sort = "a2_Description ASC,a1_Tool ASC,a3_Date ASC";
                dataGridView1.DataSource = dtShow;
            }

            else
            {
                try
                {
                    dtShow = myDic[tool][6].Copy();
                    dataGridView1.DataSource = dtShow;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
                }
            }
        }
        //query by lot

        private void button4_Click_1(object sender, EventArgs e)
        {
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }

            this.tabControl1.SelectedIndex = 0; ShowGridview();
            classSeqLog.queryByLotID(ref dtShow, ref myDic[tool][0]);
            dataGridView1.DataSource = dtShow;

        }
        private void button5_Click(object sender, EventArgs e)
        {
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }

            this.tabControl1.SelectedIndex = 0; ShowGridview();
            classSeqLog.queryByKey(ref dtShow, ref myDic[tool][0]);
            dataGridView1.DataSource = dtShow;
        }

        //view baseline
        private void button12_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][7].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }

            //dtShow.Columns.Add("FIA_X_Move", typeof(double));
            //dtShow.Columns.Add("FIA_Y_Move", typeof(double));
            // dtShow.Columns.Add("LSA_X_Move", typeof(double));
            // dtShow.Columns.Add("LSA_Y_Move", typeof(double));


            double tmpDouble;
            
            foreach( char x in textBoxBaseline.Text.Trim())
            {
                if(char.IsDigit(x))
                { }
                else
                { MessageBox.Show("Baseline Check规范非数字字符，无法标注是否超规范");return; }
            }
            int specBaseline = int.Parse(textBoxBaseline.Text.Trim());

            for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
            {
               if( Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[10].Value)) > 150)
                { dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.FromName("Yellow"); }
                for (int j = 2; j < 6; ++j)
                {
                    if (Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[j].Value)) > specBaseline ||
                        (Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value)) > specBaseline
                        &&
                      Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value)- Convert.ToDouble(dataGridView1.Rows[i-1].Cells[j].Value))> specBaseline)
                        ) //not correct，keep the first line only
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromName("Yellow");
                    }



                }

            //    for (int k = 10; k < 14; ++k)
            //    {

            //        if (i > 0 && Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[k].Value)) > 50)
            //        {
            //            dataGridView1.Rows[i].Cells[k].Style.BackColor = Color.FromName("Yellow");
             //       }
           //     }

                /*
                if (i > 0)
                {
                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["FIA_X"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["FIA_X"].Value);
                   
                    dataGridView1.Rows[i].Cells["FIA_X_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["FIA_X_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["FIA_Y"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["FIA_Y"].Value);
                    dataGridView1.Rows[i].Cells["FIA_Y_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["FIA_Y_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["LSA_X"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["LSA_X"].Value);
                    dataGridView1.Rows[i].Cells["LSA_X_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["LSA_X_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["LSA_Y"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["LSA_Y"].Value);
                    dataGridView1.Rows[i].Cells["LSA_Y_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["LSA_Y_Move"].Style.BackColor = Color.FromName("Yellow");


                    }






                }
                */


            }


        }

























        #endregion

        private void 测试用_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0; ShowGridview();
            string tool = string.Empty;
            foreach (RadioButton x in panel7.Controls)
            {
                if (x.Checked) { tool = x.Name; break; }
            }
            if (tool == "All")
            { MessageBox.Show("显示Log原始数据，不能选择所有设备，请选择单台设备，退出"); return; }
            try
            {
                dtShow = myDic[tool][7].Copy();
                dataGridView1.DataSource = dtShow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n请确认前置步骤已读取了 _" + tool + "_ 的Sequence Log");
            }

            //dtShow.Columns.Add("FIA_X_Move", typeof(double));
            //dtShow.Columns.Add("FIA_Y_Move", typeof(double));
           // dtShow.Columns.Add("LSA_X_Move", typeof(double));
           // dtShow.Columns.Add("LSA_Y_Move", typeof(double));


            double tmpDouble;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                for (int j = 2; j < 6; ++j)
                {
                    if (Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value)) > 50)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromName("Yellow");
                    }
                }

                for (int k = 10; k < 14; ++k)
                {

                    if (i>0 && Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells[k].Value)) > 50)
                    {
                        dataGridView1.Rows[i].Cells[k].Style.BackColor = Color.FromName("Yellow");
                    }
                }

                /*
                if (i > 0)
                {
                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["FIA_X"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["FIA_X"].Value);
                   
                    dataGridView1.Rows[i].Cells["FIA_X_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["FIA_X_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["FIA_Y"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["FIA_Y"].Value);
                    dataGridView1.Rows[i].Cells["FIA_Y_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["FIA_Y_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["LSA_X"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["LSA_X"].Value);
                    dataGridView1.Rows[i].Cells["LSA_X_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["LSA_X_Move"].Style.BackColor = Color.FromName("Yellow");
                    }

                    tmpDouble = Convert.ToDouble(dataGridView1.Rows[i].Cells["LSA_Y"].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells["LSA_Y"].Value);
                    dataGridView1.Rows[i].Cells["LSA_Y_Move"].Value = tmpDouble;
                    if (Math.Abs(tmpDouble) > 50)
                    {
                        dataGridView1.Rows[i].Cells["LSA_Y_Move"].Style.BackColor = Color.FromName("Yellow");


                    }






                }
                */


            }


        }

      
    }



  

       
}