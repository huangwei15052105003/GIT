using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;
using System.IO;
//using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.Net; //get computer ip
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;

namespace TaskExe
{
    class Program
    {
        static void Main(string[] args)
        {
            

            if (1 == 1)
            {
                string ipAddress = TaskExe.Share.GetIpAddress();
                //run 10.4.72.120
                if (ipAddress == "fe80::a1c2:bc20:4b76:7c51%11")
                {
                    #region Long Time
                    if (string.Compare(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Substring(11, 2), "12") > 0)
                    {
                        Console.WriteLine("Long Time.......");
                        UpdateDbWithPython("c:\\anaconda3\\python.exe", "P:\\_SQLite\\VsPythonAuto.py");
                        AsmlAweDbDelete(30, "P:\\_SQLite\\AsmlAwe.db");
                        CreateFullAsmlAweDb("P:\\_SQLite\\AsmlAwe.db", "P:\\_SQLite\\FullAsmlAwe.db");
                                               
                        if (TaskExe.Share.FileDateCompare("P:\\_SQLite\\OPAS\\NikonJobinStation.csv", "P:\\_SQLite\\NikonJobinStation.db"))
                        {
                            UpdateFullJobinStation("data source=P:\\_SQLite\\NikonJobinStation.db", "P:\\_SQLite\\OPAS\\NikonJobinStation.csv", "tbl_nikonjobinstation", "Full Nikon Jobin Station Update Done");
                        }

                        if (TaskExe.Share.FileDateCompare("P:\\_SQLite\\OPAS\\AsmlJobinStation.csv", "P:\\_SQLite\\AsmlJobinStation.db"))
                        {
                            UpdateFullJobinStation("data source=P:\\_SQLite\\AsmlJobinStation.db", "P:\\_SQLite\\OPAS\\AsmlJobinStation.csv", "tbl_asmljobinstation", "Full Nikon Jobin Station Update Done");
                        }
                    }
                    else
                    {
                        Console.WriteLine("PYTHON UPDATE & JobinStation Update SKIPPED");
                    }
                    #endregion

                    #region critical
                    UpdateReworkDb();

                    CreateDbFromChartingDataOneByOne();

                    UpdateTrackRecipeEsfConstraintInReworkMoveDb();

                    UpdateTech();

                    UpdatePartVsMask();
                    #endregion

                    #region Source：CSV
                    UpdateWithCsvFile(@"data source=P:\_SQLite\ReworkMove.DB", "P:\\_SQLite\\OPAS\\MaskInfo.csv", "tbl_mask", "ReworkMove.DB Mask Information Update Done", false);


                    UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\r2rCdConfig.csv", "tbl_config", "R2R.DB CD CONFIG UPDATE DONE", false);

                    UpdateWithCsvFile("data source=P:\\_SQLite\\ReworkMove.db", "P:\\_SQLite\\OPAS\\r2rCdConfig.csv", "tbl_cdconfig", "ReworkMove.DB CD CONFIG UPDATE DONE", false);

                    UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\CD.csv", "tbl_cd", "R2R.DB CD UPDATE DONE", true);

                    UpdateWithCsvFile("data source=P:\\_SQLite\\R2R.db", "P:\\_SQLite\\OPAS\\OVL.csv", "tbl_ovl", "R2R.DB OVL UPDATE DONE", true);
                    #endregion

                    #region MFG ORACLE                                          
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

                    //ESF TOOL AVAILABLE 
                    EsfToolAvailableFromLithoWip();
                    EsfToolAvailableFromFabWip();

                    ///delete obselte excel file for r2r charting
                    DeleteObseleteExcelChart();

                    #endregion

                    Server10_4_2_112.RefreshRecipeLatestDate();
                    BackupDB();
                    
                }
                // run 10.4.72.74
                else if (ipAddress == "fe80::297a:edd3:4d41:a3aa%12")
                {
                    Console.WriteLine("This Is 10.4.72.74");
                    MoveAsmlDownload();
                    MoveNikonDownload();

                    if (string.Compare(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Substring(11, 2), "12") > 0)
                    {
                        if (DateTime.Now.ToString("-dd") == "-28")
                        {
                            try
                            { DeleteExcleSheet("P:\\_flow"); }
                            catch
                            { }
                        }
                        if (DateTime.Now.ToString("dd") == "10" || DateTime.Now.ToString("dd") == "20" || DateTime.Now.ToString("dd") == "30")
                        {
                            string srcPath = "P:\\_DailyCheck\\HITACHI\\";
                            string destPath = "D:\\Litho\\_DailyCheck\\HITACHI\\";
                            DirectoryInfo srcPathInformation = new DirectoryInfo(srcPath);
                            DirectoryInfo destPathInormation = new DirectoryInfo(destPath);
                            Share.CopyDirectory(srcPathInformation, destPathInormation, true);
                            Console.WriteLine("===HITACHI DONE===");


                            srcPath = "P:\\_SQLite\\";
                            destPath = "D:\\Litho\\Backup\\_SQLite";
                            srcPathInformation = new DirectoryInfo(srcPath);
                            destPathInormation = new DirectoryInfo(destPath);
                            Share.CopyDirectory(srcPathInformation, destPathInormation, true);
                            Console.WriteLine("===SQLite DONE===");

                            //TaskExe.Share.DosCommand("pause");
                        }
                    }
                }
                else if (ipAddress == "fe80::a488:7890:c268:8f28%12")
                {
                    Console.WriteLine("THIS IS RUN AT 10.4.2.112:9542");
                    Server10_4_2_112.RefreshRecipeLatestDate();
                
                }
                else
                {
                    Console.WriteLine("COMPUTER IP NOT CORRECT");
                }

                Console.WriteLine(ipAddress + ":  Command Finished");

                //  TaskExe.Share.DosCommand("pause");
            }
        }
                                   

        ///=======================================================================================================

        #region functions called
        ///Update DB FROM ALL EXCEL FILES
        private static void CreateDbFromChartingDataOneByOne()
        {
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
                    // Console.WriteLine("新数据和数据库数据无日期交叠，无法更新数据，退出"); 
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
                    DataTableToSqlite myTabInfo = new DataTableToSqlite(dt1, "tbl_cd");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                    Console.WriteLine("CD Update Done");

                    dt = null; dt1 = null;
                }
                else
                { Console.WriteLine("无CD新数据,未更新"); }

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
                    Console.WriteLine("新数据和数据库数据无日期交叠，无法更新数据，退出");
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
                    DataTableToSqlite myTabInfo = new DataTableToSqlite(dt1, "tbl_ovl");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                    Console.WriteLine("ASML OVL Update Done");

                    dt = null; dt1 = null;
                }
                else
                {
                    Console.WriteLine("无ASML OVL新数据,未更新");
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
                    Console.WriteLine("新数据和数据库数据无日期交叠，无法更新数据，退出");
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
                    DataTableToSqlite myTabInfo = new DataTableToSqlite(dt1, "tbl_ovl");
                    myTabInfo.ImportToSqliteBatch(dt1, dbName);
                    // Console.WriteLine("NIKON OVL Update Done");

                    dt = null; dt1 = null;

                    //更新P盘数据

                }
                else
                {
                    //  Console.WriteLine("无NIKON OVL新数据,未更新");
                }

                #endregion
            }
            Console.WriteLine("DONE");
        }

        //update rework database-->MFG ORACLE
        private static void UpdateReworkDb()
        {
            string connStr = @"data source=P:\_SQLite\ReworkMove.DB";
            string sql; DataTable dt; string strDate;
            //===rework
            //列出最新日期，然后删除，因其数据可能不完整
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(rowid),HISTDATE from tbl_rework";//first record select English from graduate_phrase where packid=1 and levelid=1 limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr["HISTDATE"].ToString();
                    }
                }
            }

            System.DateTime tmp = System.Convert.ToDateTime(strDate);

            string strDateNew = tmp.AddDays(-15).ToString("yyyy-MM-dd");
            Console.WriteLine(strDate + "," + strDateNew);

            //删除最新15天数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM tbl_rework where HISTDATE>='" + strDateNew + "'";
                //sql = "DELETE FROM tbl_move";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            sql = " select to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'MM') MM,  to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'iw') WW, HISTDATE, LOTID,PRIORITY Pri,PARTID,STAGE,REWORKQTY QTY,decode(eqptype,'LDI','ASML','NIKON') eqptype , eqpid, PPID,trackintime ,TIMEREV, REWORKCODE CODE, Description, EVVARIANT,lottype from RPTPRD.mfg_tbl_rework  where eqptype IS NOT NULL  AND substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') and HISTDATE >='" + strDateNew + "' order by TIMEREV";

            dt = TaskExe.Share.CsmcOracle(sql);
            string dbTblName = "tbl_rework";
            DataTableToSqlite myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            Console.WriteLine("Rework Update Done");
            strDate = null;
            //==move
            //列出最新日期，然后删除，因其数据可能不完整
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(rowid),day from tbl_move";//first record select English from graduate_phrase where packid=1 and levelid=1 limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr["DAY"].ToString();
                    }

                }
            }
            //删除最新一天数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM tbl_move where DAY='" + strDate + "'";
                // sql = "DELETE FROM tbl_move";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            strDate += " 07:30:00";
            sql = "SELECT to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'yyyy')||to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'MM') MM,to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'yyyy')||to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'iw') WW, to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd') DAY,TRACKOUTQTY QTY,STAGE,EQPID,decode(SUBSTR(EQPID,2,2),'LD','ASML','LI','NIKON','LS','NIKON','PENDING') as TOOL FROM RPTPRD.MFG_VIEW_STEP_MOVE WHERE substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') AND SUBSTR(EQPID,2,2) IN ('LD','LI','LS') AND TRACKOUTTIME >to_date('" + strDate + "','yyyy-mm-dd hh24:mi;ss')";

            sql = " SELECT MM,WW,DAY,TOOL,STAGE,  CAST(COUNT(QTY) AS float) as LOTQTY,CAST(SUM(QTY) AS float) AS WFRQTY FROM (" + sql + ") GROUP BY MM,WW,DAY,EQPID,STAGE ORDER BY DAY,TOOL";

            //count等统计函数计算的int值，即使在数据库中设为double字段，导入datatable,仍是int值，继续改为double，但在datatable做统计时，仍报错？？？？，用cast函数强制转换为double，同时针对oracle和SQLite数据
            dt = TaskExe.Share.CsmcOracle(sql);
            dbTblName = "tbl_move";
            myTabInfo = new TaskExe.DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            Console.WriteLine("Move Update Done");

            //更新P0 层次
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "UPDATE tbl_rework set EQPTYPE='ASML',EQPID='ALDI01' WHERE PPID='R0000A'";
                // sql = "DELETE FROM tbl_move";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            //Console.WriteLine("P0 Update Done");
            Console.WriteLine(" Update Done");
            try
            {
                File.Copy(@"P:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);
                Console.WriteLine("D:\\TEMP\\DB\\ReworkMove.DB已被本机文件覆盖");
            }
            catch
            {
                Console.WriteLine("没有权限读写P:\\_SQLITE目录");
            }

        }

        //Update TrackRecipe,ESF-->MFG ORACLE
        private static void UpdateTrackRecipeEsfConstraintInReworkMoveDb()
        {
            string connStr = @"data source=P:\_SQLite\ReworkMove.DB";

            Console.WriteLine("Update ESF , Track Recipe, InlineWip");
            string sql, sql1, sql2, sql3, sql4; DataTable dt; DataTableToSqlite myTabInfo; string dbTblName;
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
            myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            Console.WriteLine("ESF UPDATE DONE");

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
            myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            Console.WriteLine("ESF UPDATE DONE (For Check)");
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
            myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");
            Console.WriteLine("PART & TRACK RECIPE  UPDATE DONE");
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
            myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");

            Console.WriteLine("INLINE WIP UPDATE DONE");
            #endregion





        }

        //import full jobinstation
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
            DataTableToSqlite myTabInfo;

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
                    //  Console.WriteLine(strLine);
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
                        myTabInfo = new DataTableToSqlite(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, dbPath);
                        stepCount = 0;
                        dtShow.Rows.Clear();
                        dtShow = null;
                        dtShow = dtTmp.Clone();

                    }
                }
            }
            myTabInfo = new DataTableToSqlite(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, dbPath);
            Console.WriteLine((DateTime.Now - tmpDate).ToString());
            Console.WriteLine(notes + "\n\n导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();
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
                { Console.WriteLine("新数据和数据库数据无日期交叠，请选择更早的日期下载数据:" + dbTblName); return; }
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
                    Console.WriteLine("无新数据，请从OPAS重新下载数据");
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
            DataTableToSqlite myTabInfo = new DataTableToSqlite(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, dbPath);
            Console.WriteLine(notes);

        }

        //Ptyhon Update 
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

        private static void DeleteUpdateDB(string connStr, string dbPath, string dbTblName, string sql, string notes)
        {
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
            DataTableToSqlite myTabInfo = new DataTableToSqlite(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, dbPath);

            Console.WriteLine(notes);

        }


        #endregion

        #region for LithoForm
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


                // Console.WriteLine(asmlWip.Rows.Count.ToString());
                // Console.WriteLine(nikonWip.Rows.Count.ToString());
                // Console.WriteLine(asmlPart.Rows.Count.ToString());
                // Console.WriteLine(asmlTech.Rows.Count.ToString());
                // Console.WriteLine(nikonPart.Rows.Count.ToString());
                // Console.WriteLine(nikonTech.Rows.Count.ToString());
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
                                    //  Console.WriteLine(x["TECH"].ToString());
                                    //  Console.WriteLine(y["TECH"].ToString());
                                    //  Console.WriteLine(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());



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
                            //     Console.WriteLine(x["TECH"].ToString() + "," + x["PART"].ToString() + "," + x["STAGE"].ToString() + "," + x["RECIPE"].ToString());


                            //  Console.WriteLine(Regex.IsMatch("H5012","^[A-Z0-9]{1}1\\S*$").ToString());




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
                                //   Console.WriteLine(y["TECH"].ToString() + "," + y["PART"].ToString() + "," + y["STAGE"].ToString() + "," + y["RECIPE"].ToString());
                                //    Console.WriteLine(y["TECH"].ToString() + ",   " + y["PART"].ToString());
                                //   Console.WriteLine(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());
                                // Console.WriteLine(Regex.IsMatch(x["PART"].ToString(), y["TECH"].ToString()).ToString());
                                // Console.WriteLine(Regex.IsMatch(x["RECIPE"].ToString(), y["TECH"].ToString()).ToString());
                                //  Console.WriteLine(Regex.IsMatch(x["STAGE"].ToString(), y["TECH"].ToString()).ToString());
                                //     Console.WriteLine("SDFSDFSDF     " + techFlag.ToString());
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

                //  Console.WriteLine("用时" + stopwatch.ElapsedMilliseconds.ToString() + " msec\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n另短流程（-RD）无法从OPAS R2R导出完整工艺代码，不判断");
                Console.WriteLine("工艺代码来自：\n\n" +
                    "   MFG Oracle数据库\n" +
                    "   LiYan/RenY编程数据\n" +
                    "   因各种原因，部分产品可能缺工艺代码，无法判断\n\n" +
                    "当前WIP来自MFG Oracle DB，实时性待确认\n\n" +
                    "表格最后一栏系数据刷新时间");

            }
            catch
            {
                Console.WriteLine("更新失败；请确认是否可以访问MFG DB");


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


                // Console.WriteLine(asmlWip.Rows.Count.ToString());
                // Console.WriteLine(nikonWip.Rows.Count.ToString());
                // Console.WriteLine(asmlPart.Rows.Count.ToString());
                // Console.WriteLine(asmlTech.Rows.Count.ToString());
                // Console.WriteLine(nikonPart.Rows.Count.ToString());
                // Console.WriteLine(nikonTech.Rows.Count.ToString());
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
                                    //  Console.WriteLine(x["TECH"].ToString());
                                    //  Console.WriteLine(y["TECH"].ToString());
                                    //  Console.WriteLine(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());



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
                            //     Console.WriteLine(x["TECH"].ToString() + "," + x["PART"].ToString() + "," + x["STAGE"].ToString() + "," + x["RECIPE"].ToString());


                            //  Console.WriteLine(Regex.IsMatch("H5012","^[A-Z0-9]{1}1\\S*$").ToString());




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
                                //   Console.WriteLine(y["TECH"].ToString() + "," + y["PART"].ToString() + "," + y["STAGE"].ToString() + "," + y["RECIPE"].ToString());
                                //    Console.WriteLine(y["TECH"].ToString() + ",   " + y["PART"].ToString());
                                //   Console.WriteLine(Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()).ToString());
                                // Console.WriteLine(Regex.IsMatch(x["PART"].ToString(), y["TECH"].ToString()).ToString());
                                // Console.WriteLine(Regex.IsMatch(x["RECIPE"].ToString(), y["TECH"].ToString()).ToString());
                                //  Console.WriteLine(Regex.IsMatch(x["STAGE"].ToString(), y["TECH"].ToString()).ToString());
                                //     Console.WriteLine("SDFSDFSDF     " + techFlag.ToString());
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

                //  Console.WriteLine("用时" + stopwatch.ElapsedMilliseconds.ToString() + " msec\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n另短流程（-RD）无法从OPAS R2R导出完整工艺代码，不判断");
                Console.WriteLine("工艺代码来自：\n\n" +
                       "   MFG Oracle数据库\n" +
                       "   LiYan/RenY编程数据\n" +
                       "   因各种原因，部分产品可能缺工艺代码，无法判断\n\n" +
                       "当前WIP来自MFG Oracle DB，实时性待确认\n\n" +
                       "表格最后一栏系数据刷新时间\n\n\n" +
                        stopwatch.ElapsedMilliseconds.ToString());

            }
            catch
            {
                Console.WriteLine("更新失败；请确认是否可以访问MFG DB");


            }

        }
        //delete obselete excel file for r2r charting
        public static void DeleteObseleteExcelChart()
        {
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
                    // Console.WriteLine(file);
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

        }
        public static void CreateLocalDb()  //r2r data and jobin statin with OPAS sql
        {

            ;
            DataTable dt;
            string sql;

            File.Copy(@"P:\_SQLite\BlankJobinCdOvl.DB", @"P:\_SQLite\JobinCdOvl.DB", true);
            string dbName = @"P:\_SQLite\JobinCdOvl.DB";


            //r2r cd/ovl
            string src = "P:\\_SQLite\\OPAS\\inlineR2r.csv";
            dt = TaskExe.Share.OpenCsvWithComma(src);
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, "t1");
            myTabInfo.ImportToSqliteBatch(dt, dbName);
            Console.WriteLine("CD/OVL DB Done");
            dt = null;

            //r2r jobin station

            src = "P:\\_SQLite\\OPAS\\inlineJobin.csv";
            dt = TaskExe.Share.OpenCsvWithComma(src);
            myTabInfo = new DataTableToSQLte(dt, "t2");
            myTabInfo.ImportToSqliteBatch(dt, dbName);
            Console.WriteLine("Jobin DB Done");
            dt = null;

            string connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr1))
            {
                sql = " SELECT distinct layer,flowtype,tooltype from tbl_layer";
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
            myTabInfo = new DataTableToSQLte(dt, "t3");
            myTabInfo.ImportToSqliteBatch(dt, dbName);



        }
        public static void BackupDB() //only run at 10.4.72.120
        {
            //disk D --> disk p
            TaskExe.Share.DateTimeFileCopy(@"D:\TEMP\DB\ChartRawData.DB", @"p:\_SQLite\ChartRawData.DB");

            //disk P --> disk D
            if (string.Compare(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Substring(11, 2), "17") > 0)
            {
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\AsmlJobinStation.DB", @"D:\TEMP\DB\AsmlJobinStation.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\NikonJobinStation.DB", @"D:\TEMP\DB\NikonJobinStation.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\AsmlAwe.DB", @"D:\TEMP\DB\AsmlAwe.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\AsmlBatchreport.DB", @"D:\TEMP\DB\AsmlBatchreport.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\R2R.DB", @"D:\TEMP\DB\R2R.DB");


                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\JobinCdOvl.DB", @"D:\TEMP\DB\JobinCdOvl.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB");



            }
            else
            {
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\JobinCdOvl.DB", @"D:\TEMP\DB\JobinCdOvl.DB");
                TaskExe.Share.DateTimeFileCopy(@"p:\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB");

            }
        }


        public static void UpdateTech()
        {
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



        }
        public static void UpdatePartVsMask()
        {
            string sql;
            string connStr;
            DataTable dt1, dtShow, dt2;
            sql = " select distinct substr(partid,1,length(partid)-3) PART ,substr(mask,1,4) MASK from RPTPRD.MFG_VIEW_FLOW where mask is not null and regexp_like(substr(mask,1,4),'^[0-9]+$') order by mask";
            dt1 = TaskExe.Share.CsmcOracle(sql);

            //  Console.WriteLine(dt1.Rows.Count.ToString());

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
            // Console.WriteLine(dt2.Rows.Count.ToString());

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
            Console.WriteLine("PartMaskShop Update Done");
        }

        #endregion

        #region housekeeping P-->Z
        public static void MoveAsmlDownload()
        {
            string root1 = "P:\\_AsmlDownload\\";
            string root2 = "F:\\share\\_AsmlDownload\\";
            string[] tools = new string[] { "7D", "08", "8A", "8B", "8C", "82", "83", "85", "86", "87", "89" };
            string[] nikontools = new string[] {"ALII01","ALII02","ALII03","ALII04","ALII05","ALII06","ALII07","ALII08","ALII09",
                                                "ALII10","ALII11","ALII12","ALII13","ALII14","ALII15","ALII16","ALII17","ALII18",
                                                 "BLII20","BLII21","BLII22","BLII23"};
            List<string> filelist;
            string src, dst;
            FileInfo myfile;
            //awe
            foreach (string tool in tools)
            {
                src = root1 + "AWE\\" + tool + "\\";
                dst = root2 + "AWE\\" + tool + "\\";
                filelist = new List<string>();
                filelist = Share.ExportFileList(src, filelist);

                foreach (string x in filelist)
                {
                    myfile = new FileInfo(x);
                    if (myfile.Name.StartsWith("FINISHED"))
                    {
                        try
                        {
                            myfile.MoveTo(dst + myfile.Name);
                            Console.WriteLine(x);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Move Awe:" + "," + ex.Message);
                        }
                    }

                }

            }
            //batchreport
            foreach (string tool in tools)
            {
                src = root1 + "BatchReport\\" + tool + "\\";
                dst = root2 + "BatchReport\\" + tool + "\\";
                filelist = new List<string>();
                filelist = Share.ExportFileList(src, filelist);

                foreach (string x in filelist)
                {
                    myfile = new FileInfo(x);

                    if (myfile.Name.StartsWith("Finished_"))
                    {
                        try
                        {
                            myfile.MoveTo(dst + myfile.Name);
                            Console.WriteLine(x);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Move BatchReport:" + "," + ex.Message);
                        }
                    }

                }

            }
            //BatchAlignReport
            foreach (string tool in tools)
            {
                src = root1 + "BatchAlignReport\\" + tool + "\\";
                dst = root2 + "BatchAlignReport\\" + tool + "\\";
                filelist = new List<string>();
                filelist = Share.ExportFileList(src, filelist);

                foreach (string x in filelist)
                {
                    myfile = new FileInfo(x);
                    if (myfile.Name.EndsWith(".tar.gz"))
                    {
                        try
                        {
                            myfile.MoveTo(dst + myfile.Name);
                            Console.WriteLine(x);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Move BatchAlignReport:" + "," + ex.Message);
                        }
                    }

                }

            }



        }
        public static void MoveNikonDownload()
        {
            string root1 = "P:\\SEQLOG\\";
            string root2 = "F:\\share\\SEQLOG\\";
            string riqi = DateTime.Now.AddDays(-300).ToString("_yyyy-MM-dd_");

            string[] tools = new string[] {"ALII01","ALII02","ALII03","ALII04","ALII05","ALII06","ALII07","ALII08","ALII09",
                                           "ALII10","ALII11","ALII12","ALII13","ALII14","ALII15","ALII16","ALII17","ALII18",
                                           "BLII20","BLII21","BLII22","BLII23"};
            List<string> filelist;
            string src, dst;
            FileInfo myfile;
            //Nikon SequenceLog
            foreach (string tool in tools)
            {
                src = root1 + tool + "\\";
                dst = root2 + tool + "\\";
                filelist = new List<string>();
                filelist = Share.ExportFileList(src, filelist);

                foreach (string x in filelist)
                {
                    myfile = new FileInfo(x);
                    if (string.Compare(myfile.Name, riqi) < 0 && myfile.Name.StartsWith("_20"))
                    {
                        myfile.MoveTo(dst + myfile.Name);
                        Console.WriteLine(x);

                    }

                }

            }



        }

        #endregion

        #region flow
        public static void DeleteExcleSheet(string path)
        {
            try
            {
                //get excel filelist
                List<string> fileList = new List<string>();

                fileList = Share.ExportFileList(path, fileList);

                //准备，实例化........
                ExcelHelp excel = new ExcelHelp();
                foreach (string file in fileList)
                {
                    if (file.ToUpper().EndsWith(".XLS"))
                    {
                        try
                        {
                            //列出表名
                            List<string> sheetsName = excel.ListSheetName(file);
                            //删除符合条件的表
                            foreach (var st in sheetsName)
                            {
                                if (st.Contains("参考") || st.ToUpper().Contains("LIST") || st.ToUpper().Contains("CHECK") || st.ToUpper().Contains("SHEET"))
                                {
                                    excel.DeleteSheetByName(file, st);
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Failed");
                        }
                    }
                }
            }
            catch
            {

            }

            Console.WriteLine("DONE");


        }
        #endregion

        #region AsmlMaitain
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
                    Console.WriteLine(tblName);
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
            Console.WriteLine(tblName);
            //delete index
            tblName = "tbl_index";
            sql = "DELETE  FROM " + tblName;
            sql += " WHERE  date<'" + riqi + "'";
            using (SQLiteCommand com = new SQLiteCommand(sql, conn))
            {
                com.ExecuteNonQuery();
            }
            Console.WriteLine(tblName);





            conn.Close();
            stopwatch.Stop();
            Int64 tmp = stopwatch.ElapsedMilliseconds / 1000;
            Console.WriteLine(tmp.ToString());
        }
        public static void RestoreSqliteDbSpace(string dbPath = "P:\\_SQLite\\AsmlAwe.db")  //回收DB空间
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=" + dbPath))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                cmd.CommandText = "VACUUM";
                cmd.ExecuteNonQuery();
            }
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
                            Console.WriteLine(tool + ", " + maxId.ToString());
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
                        Console.WriteLine(tbl + ", " + maxId.ToString());
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
        #endregion
    }
}
