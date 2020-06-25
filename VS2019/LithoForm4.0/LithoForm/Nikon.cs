using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.IO;
using System.Data.SQLite;
//using System.Data.OleDb;
//using System.Reflection;
//using System.Text.RegularExpressions;

namespace LithoForm
{
    class Nikon
    {
        
        public static DataTable PreAlignment(long sDate, long eDate, string connStr)
        {
          
            string sql; DataTable dt = null;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string sql1, sql2, sql3;
                sql1 = "(select No, tool,Date,ppid,Search,EGA from tbl_index where Date between '" + sDate + "' and '" + eDate + "')";
                sql2 = "(select No,max(rot) as maxrot ,min(rot) as minrot  ,(max(rot)-min(rot)) as rotrange,count(rot) as qty  from tbl_parameter group by No)";
                //两句SQL和在一起，用；分割，可以生成2个datatable，依次类推，生成n个


                sql = " select a.*,b.maxrot,minrot,rotrange,qty from " + sql1 + " a ," + sql2 + " b where a.No=b.No";
                sql1 = "select No, tool,Date,ppid,Search,EGA from tbl_index where Date between " + sDate + " and " + eDate + " ORDER BY NO";
                sql2 = "SELECT NO,ROT FROM TBL_PARAMETER WHERE NO IN (select No from (" + sql1 + "))";
                sql3 = " SELECT NO,COUNT(NO) QTY,(MAX(ROT)-MIN(ROT)) RANGE FROM (" + sql2 + ")  GROUP BY NO";

                sql = "SELECT T2.TOOL,T2.PPID, T1.QTY,T1.RANGE,T2.DATE,T2.SEARCH,T2.EGA FROM (" + sql3 + ") T1,(" + sql1 + ") T2 WHERE T1.NO=T2.NO ORDER BY T2.TOOL,T2.DATE";
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

        public static DataTable AlignMethod(long sDate,long eDate,string connStr, string ppid)
        {
           
            string sql; DataTable dt = null;
           
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT DISTINCT TOOL,PPID,CAST(LSAREQSHOT AS DOUBLE) LSAREQSHOT,CAST(FIAREQSHOT AS DOUBLE) FIAREQSHOT,CAST(ALIGNNO as DOUBLE) ALIGNNO,SEARCH,EGA,DATE" +
                    " FROM TBL_INDEX  WHERE (DATE BETWEEN " + sDate + " AND "+ eDate + ") AND PPID LIKE '" + ppid + "'  ORDER BY PPID,TOOL,DATE";
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
        public static DataTable AlignData(long sDate, long eDate, string connStr, string ppid)
        {
            
            string sql; DataTable dt = null;
           
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  TOOL,PPID,LSAREQSHOT,FIAREQSHOT,ALIGNNO,SEARCH,EGA,TRANX,TRANY,SCALX,SCALY,ORT,ROT,MAGX,MAGY,SROT,WFRNO,DATE FROM TBL_INDEX WHERE " +
                    " PPID LIKE '" + ppid + "' AND DATE BETWEEN " + sDate + " AND " + eDate;
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
        public static DataTable VectorIndex(long sDate, long eDate, string connStr, string ppid)
        {
            
            string sql; DataTable dt = null;
           
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  TOOL,PPID,NO,DATE FROM TBL_INDEX WHERE " +
                    " PPID LIKE '" + ppid + "' AND DATE BETWEEN " + sDate + " AND " + eDate +" ORDER BY PPID,TOOL,DATE";

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
        public static DataTable VectorData(string connStr, string str1, long str)
        {
            // https://blog.csdn.net/hanyingzhong/article/details/48653177
            //  所以建议一般情况下不要在字符串列建立索引，如果非要使用字符串索引，可以采用以下两种方法：
            //https://blog.csdn.net/hanyingzhong/article/details/48653177

            string sql; DataTable dt;
            Dictionary<string, string> toolName = new Dictionary<string, string>();
            toolName.Add("ALSIB1", "ALII01"); toolName.Add("ALSIB2", "ALII02"); toolName.Add("ALSIB3", "ALII03");
            toolName.Add("ALSIB4", "ALII04"); toolName.Add("ALSIB5", "ALII05"); toolName.Add("ALSIB6", "ALII06");
            toolName.Add("ALSIB7", "ALII07"); toolName.Add("ALSIB8", "ALII08"); toolName.Add("ALSIB9", "ALII09");
            toolName.Add("ALSIBA", "ALII10"); toolName.Add("ALSIBB", "ALII11"); toolName.Add("ALSIBC", "ALII12");
            toolName.Add("ALSIBD", "ALII13"); toolName.Add("ALSIBE", "ALII14"); toolName.Add("ALSIBF", "ALII15");
            toolName.Add("ALSIBG", "ALII16"); toolName.Add("ALSIBH", "ALII17"); toolName.Add("ALSIBI", "ALII18");
            toolName.Add("BLSIBK", "ALII20"); toolName.Add("BLSIBL", "ALII21");
            toolName.Add("BLSIE1", "ALII22"); toolName.Add("BLSIE2", "ALII23");

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  a.No,a.wfr,b.x as x0,b.y as y0,a.x as x, a.y as y  FROM TBL_" + toolName[str1] + " a,TBL_COORDINATE b " +
                    "WHERE a.shot=b.shot AND a.no=b.no AND a.No=" + str;
                //   sql = "SELECT NO,WFR,X,Y,SHOT FROM TBL_"+toolName[str1]+"  WHERE NO=" + str;
                //   sql += ";SELECT NO,X,Y,SHOT FROM TBL_COORDINATE WHERE NO=" + str;


                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                            //tblSecond = ds.Tables[1];

                        }
                    }
                }

            }

            return dt;
        }

        public static void UpdateEgaLogDb()
        {
            string[] tools = { "ALII01", "ALII02","ALII03","ALII04","ALII05","ALII06","ALII07","ALII08", "ALII09", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14", "ALII15", "ALII16", "ALII17", "ALII18", "ALII20", "ALII21", "ALII22", "ALII23" };
            // string[] tools = { "BLSD08" };
            string root = @"\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\NikonEga\";
            string connStr = @"data source=P:\_SQLite\NikonEgaPara.DB";
            List<string> list;
            List<string> fileList;
            string csvPath = string.Empty;
            string sql;
            DataTable dt;
            StringBuilder item;
            string tblName;
            bool flag;
            long rowqty;
            DataRow[] drs;
            DataRow newRow;
            DataTable newDt;
            DataTableToSQLte myTabInfo;

            if (1==1)
            {
                #region //create table

                // CreateDbTable
                //tbl_index
                tblName = "tbl_index";
                flag = LithoForm.LibF.CheckTableExist(connStr, tblName);
                item = new StringBuilder();
                item.Append("(");
                item.Append("MEAS_SENS TEXT," +
                    "Tool TEXT," +
                    "Ppid TEXT," +
                    "StepX REAL," +
                    "StepY REAL, " +
                    "OffsetX REAL, " +
                    "OffsetY REAL," +
                    "LsaReqShot INTEGER," +
                    "FiaReqShot INTEGER," +
                    "FM_ROT REAL," +
                    "TranX REAL," +
                    "TranY REAL," +
                    "ScalX REAL," +
                    "ScalY REAL," +
                    "Ort REAL," +
                    "Rot REAL," +
                    "MagX REAL," +
                    "MagY REAL," +
                    "SRot REAL," +
                    "WfrNo INTEGER," +
                    "AlignNo INTEGER," +
                    "Search TEXT," +
                    "EGA TEXT," +
                    "ULimit REAL," +
                    "Llimit REAL," +
                    "No INTEGER," +
                    "Date INTEGER");
                item.Append(")");
                if (flag == false)
                {
                    LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
                }

                //tbl_parameter
                tblName = "tbl_parameter";
                flag = LithoForm.LibF.CheckTableExist(connStr, tblName);
                item = new StringBuilder();
                item.Append("(");
                item.Append("wfrNo INTEGER," +
                    "No INTEGER," +
                    "scalingx REAL," +
                    "scalingy REAL," +
                    "rot REAL, " +
                    "ort REAL");
                item.Append(")");
                if (flag == false)
                {
                    LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
                }
                //tbl_coordinate
                tblName = "tbl_coordinate";
                flag = LithoForm.LibF.CheckTableExist(connStr, tblName);
                item = new StringBuilder();
                item.Append("(");
                item.Append("No INTEGER," +
                    "x REAL," +
                    "y REAL," +
                    "shot TEXT");
                item.Append(")");
                if (flag == false)
                {
                    LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
                }

                //vector
                foreach (var tool in tools)
                {
                    tblName = "tbl_" + tool;
                    flag = LithoForm.LibF.CheckTableExist(connStr, tblName);
                    item = new StringBuilder();
                    item.Append("(");
                    item.Append("No INTEGER," +
                        "wfr INTEGER" +
                        "shot TEXT" +
                        "x REAL," +
                        "y REAL");

                    item.Append(")");
                    if (flag == false)
                    {
                        LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
                    }
                }
                #endregion

                #region //update index
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = "select No from tbl_index where rowid = (select max(rowid) from tbl_index)";
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

                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
                { rowqty = Convert.ToInt64(dt.Rows[0][0].ToString()); }
                else
                { rowqty = 0; }
                dt = LithoForm.LibF.OpenCsv(root + "index.csv");

                dt.Columns.Add("id", typeof(System.Int64));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["no"].ToString().Replace("No", ""));
                }
                dt.Columns.Remove("No");
                dt.Columns.Add("No", typeof(System.Int64));
                dt.Columns.Add("Date", typeof(System.Int64));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = Convert.ToInt64(dt.Rows[i]["id"].ToString());
                    sql = dt.Rows[i]["File"].ToString();
                    sql = sql.Split(new char[] { '_' })[1];
                    sql = sql.Split(new char[] { '.' })[0];
                    dt.Rows[i]["Date"] = Convert.ToInt64(sql);
                }
                dt.Columns.Remove("id");
                dt.Columns.Remove("File");

                drs = dt.Select("No>" + rowqty);

                newDt = dt.Clone();
                if (drs.Length > 0)
                {
                    foreach (var row in drs)
                    {
                        newDt.ImportRow(row);
                    }
                    myTabInfo = new DataTableToSQLte(newDt, "tbl_index");
                    myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\NikonEgaPara.DB");
                }


                #endregion
            }
         

            #region //update vector/parameter
            foreach (string tool in tools)
            {
                csvPath = root + tool;
                list = new List<string>(); fileList = new List<string>();
                list = LithoForm.LibF.ExportFileList(csvPath, fileList);
                tblName = "tbl_" + tool;
                foreach (string file in list)
                {
                    if (file.Contains("2020") && file.Contains("vector.csv"))
                    {
                        //数据库最大索引
                        using (SQLiteConnection conn = new SQLiteConnection(connStr))
                        {
                            sql = "select No from "+ tblName +"  where rowid = (select max(rowid) from " + tblName + ")";
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
                        if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
                        { rowqty = Convert.ToInt64(dt.Rows[0][0].ToString()); }
                        else
                        { rowqty = 0; }
                         //读入目前Vector文件，更改No列数据--》直接更新python数据
                        dt = LithoForm.LibF.OpenCsv(file);
                        dt.Columns.Add("id", typeof(System.Int64));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["No"].ToString().Replace("No", ""));
                        }
                        dt.Columns.Remove("No");
                      //  dt.Columns.Add("No", typeof(System.Int64));
                      //  for (int i = 0; i < dt.Rows.Count; i++)
                      //  {
                      //      dt.Rows[i]["No"] = Convert.ToInt64(dt.Rows[i]["id"].ToString());
                           
                     //   }
                      //  dt.Columns.Remove("id");
                        //列出基准坐标
                        drs = dt.Select("type='coordinate' and wfr='1' and id>" + rowqty);
                        newDt = new DataTable();
                        newDt.Columns.Add("No", typeof(System.Int64));
                        newDt.Columns.Add("x");
                        newDt.Columns.Add("y");
                        newDt.Columns.Add("shot");
                        
                        if (drs.Length>0)
                        {
                            foreach (DataRow rowItem in drs)
                            {
                                newRow= newDt.NewRow();
                                newRow["No"]=rowItem["id"];
                                newRow["x"] = rowItem["x"];
                                newRow["y"] = rowItem["y"];
                                newRow["shot"] = rowItem["shot"];
                                newDt.Rows.Add(newRow);
                            }
                              myTabInfo = new DataTableToSQLte(newDt, "tbl_coordinate");
                               myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\NikonEgaPara.DB");
                        }


                       // MessageBox.Show(tool + "," + drs.Length.ToString() + "," + newDt.Rows.Count.ToString());
                        //更新测试坐标
                        drs = dt.Select("type='residual' and id>" + rowqty);
                        newDt = new DataTable();
                        newDt.Columns.Add("No", typeof(System.Int64));
                        newDt.Columns.Add("wfr");
                        newDt.Columns.Add("shot");
                        newDt.Columns.Add("x");
                        newDt.Columns.Add("y");
                        if (drs.Length > 0)
                        {
                            foreach (DataRow rowItem in drs)
                            {
                                newRow = newDt.NewRow();
                                newRow["No"] = rowItem["id"];
                                newRow["wfr"] = rowItem["wfr"];
                                newRow["x"] = rowItem["x"];
                                newRow["y"] = rowItem["y"];
                                newRow["shot"] = rowItem["shot"];
                                newDt.Rows.Add(newRow);
                            }
                               myTabInfo = new DataTableToSQLte(newDt, tblName);
                              myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\NikonEgaPara.DB");
                        }
                       // MessageBox.Show(tool + "," + drs.Length.ToString() + "," + newDt.Rows.Count.ToString() + "," + tblName);
                        //更新parameter
                        sql = file;
                        sql = sql.Replace("vector", "parameter");
                        dt = LithoForm.LibF.OpenCsv(sql);
                        dt.Columns.Add("id", typeof(System.Int64));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["No"].ToString().Replace("No", ""));
                        }
                        dt.Columns.Remove("No");
                        drs = dt.Select( "id>" + rowqty);
                        newDt = new DataTable();
                        newDt.Columns.Add("wfrNo");
                        newDt.Columns.Add("No", typeof(System.Int64));
                        newDt.Columns.Add("scalingx");
                        newDt.Columns.Add("scalingy");
                        newDt.Columns.Add("rot");
                        newDt.Columns.Add("ort");
                        if (drs.Length > 0)
                        {
                            foreach (DataRow rowItem in drs)
                            {
                                newRow = newDt.NewRow();
                                newRow["wfrNo"] = rowItem["wfrNo"];
                                newRow["No"] = rowItem["id"];
                                newRow["scalingx"] = rowItem["scalingx"];
                                newRow["scalingy"] = rowItem["scalingy"];
                                newRow["rot"] = rowItem["rot"];
                                newRow["ort"] = rowItem["ort"];
                                newDt.Rows.Add(newRow);
                            }
                            myTabInfo = new DataTableToSQLte(newDt, "tbl_parameter");
                            myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\NikonEgaPara.DB");
                        }


                       // MessageBox.Show(tool + "," + drs.Length.ToString() +","+ newDt.Rows.Count.ToString()+","+tblName) ;

                     
                  
                    }
                }
            }
           

            #endregion




       
        }
    }
}
