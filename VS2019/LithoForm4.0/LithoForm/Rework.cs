//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
//using System.IO;
using System.Data.SQLite;
//using System.Data.OleDb;
//using System.Reflection;
//using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System;

namespace LithoForm
{
    class Rework
    {
        public static void RawData(string connStr,string sDate, string eDate,ref DataTable dtShow)
        {
            
            string sql; DataTable dt = null;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT B.FlowType,A.* FROM (SELECT * FROM tbl_rework WHERE HISTDATE BETWEEN '" + sDate + "' AND '" + eDate + "') A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.EQPTYPE=B.TOOLTYPE";


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
        public static void SummaryData(string connStr,ref DataTable dt)
        {
          //  MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    结束日期");
            string sql; 

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                //工艺按FEOL/BEOL
                string sql1, sql2, sql12;

                sql1 = " SELECT B.FlowType,A.* FROM tbl_rework A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.EQPTYPE=B.TOOLTYPE";
                sql1 = " SELECT FLOWTYPE,'MM'||MM AS RIQI, EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM (" + sql1 + ") WHERE CODE not in ( 'Q2','Q1','Q3','Z7','TB') GROUP BY FLOWTYPE,MM,EQPTYPE UNION SELECT FLOWTYPE,'WW'||WW AS RIQI, EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM (" + sql1 + ") WHERE CODE not in ( 'Q2','Q1','Q3','Z7','TB') GROUP BY FLOWTYPE,WW,EQPTYPE  ";//两组Select不能分别加括号
                sql1 = "SELECT * FROM (" + sql1 + ") ORDER BY RIQI,FLOWTYPE,EQPTYPE";


                sql2 = " SELECT B.FlowType,A.* FROM tbl_move A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.TOOL=B.TOOLTYPE";
                sql2 = " SELECT FLOWTYPE,'MM'||MM AS RIQI,TOOL AS EQPTYPE,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM (" + sql2 + ") GROUP BY FLOWTYPE,MM,TOOL UNION  SELECT FLOWTYPE,'WW'||WW AS RIQI,TOOL AS EQPTYPE,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM (" + sql2 + ") GROUP BY FLOWTYPE,WW,TOOL";
                sql2 = "SELECT * FROM (" + sql2 + ") ORDER BY RIQI,FLOWTYPE,EQPTYPE";

                sql12 = "SELECT T1.*,T2.LOTQTY AS MOVELOTQTY,T2.WFRQTY AS MOVEWFRQTY,T1.LOTQTY/T2.LOTQTY*100 AS LOTRATIO,T1.WFRQTY/T2.WFRQTY*100 WFRRATIO FROM (" + sql1 + ") T1,(" + sql2 + ") T2 WHERE T1.RIQI=T2.RIQI AND T1.FLOWTYPE=T2.FLOWTYPE AND T1.EQPTYPE=T2.EQPTYPE ORDER BY RIQI,FLOWTYPE,EQPTYPE";
                sql12 = " SELECT FLOWTYPE,RIQI,EQPTYPE,CAST(LOTQTY AS double) LOTQTY,WFRQTY,MOVELOTQTY, MOVEWFRQTY,ROUND(LOTRATIO,2) LOTRATIO,ROUND(WFRRATIO,2) WFRRATIO FROM (" + sql12 + ")";


                //设备返工

                string sql3, sql4, sql34;

                sql3 = " SELECT (case code when 'Q2' then '_ScannerStepper' when 'Q1' then '_Track'  when 'TB' then '_Track' else '_Metrology' end ) FLOWTYPE," +
                    " 'MM'||MM AS RIQI," +
                    "(case code when 'Q1' then ''   else '' end ) EQPTYPE," +
                    "COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK" +
                    " WHERE CODE IN ('Q1','Q2','Q3','TB') " +
                    "GROUP BY MM," +
                    "(case code when 'Q2' then '_ScannerStepper' when 'Q1' then '_Track' when 'TB' then '_Track' else '_Metrology' end ),(case code when 'Q1' then ''   else '' end ) UNION " +
                    "SELECT (case code when 'Q2' then '_ScannerStepper' when 'Q1' then '_Track'  when 'TB' then '_Track' else '_Metrology' end ) FLOWTYPE, 'WW'||WW AS RIQI,(case code when 'Q1' then ''   else '' end ) EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK WHERE CODE IN ('Q1','Q2','Q3','TB') GROUP BY WW,(case code when 'Q2' then '_ScannerStepper' when 'Q1' then '_Track'  when 'TB' then '_Track' else '_Metrology' end ),(case code when 'Q1' then ''   else '' end )";


                sql4 = " SELECT 'MM'||MM AS RIQI,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM TBL_MOVE GROUP BY MM UNION  SELECT 'WW'||WW AS RIQI,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM TBL_MOVE GROUP BY WW";

                sql34 = "SELECT T11.*,T22.LOTQTY AS MOVELOTQTY,T22.WFRQTY AS MOVEWFRQTY,ROUND(T11.LOTQTY/T22.LOTQTY*100,2) AS LOTRATIO,ROUND(T11.WFRQTY/T22.WFRQTY*100,2) WFRRATIO FROM (" + sql3 + ") T11,(" + sql4 + ") T22 WHERE T11.RIQI=T22.RIQI";//  ORDER BY RIQI";

                sql = sql34;
                //部门返工
                string sql5, sql56;
                sql5 = " SELECT (case code when 'Q1' then '_ALL'  else '_ALL' end ) FLOWTYPE, 'MM'||MM AS RIQI,(case code when 'Q1' then ''   else '' end ) EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK WHERE CODE !='Z7' GROUP BY MM,(case code when 'Q1' then '_ALL'  else '_ALL' end ),(case code when 'Q1' then ''   else '' end ) UNION SELECT (case code when 'Q1' then '_ALL'  else '_ALL' end ) FLOWTYPE, 'WW'||WW AS RIQI,(case code when 'Q1' then ''   else '' end ) EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK WHERE CODE !='Z7' GROUP BY WW,(case code when 'Q1' then '_ALL'  else '_ALL' end ),(case code when 'Q1' then ''   else '' end )";

                sql56 = "SELECT T1.*,T2.LOTQTY AS MOVELOTQTY,T2.WFRQTY AS MOVEWFRQTY,ROUND(T1.LOTQTY/T2.LOTQTY*100,2) AS LOTRATIO,ROUND(T1.WFRQTY/T2.WFRQTY*100,2) WFRRATIO FROM (" + sql5 + ") T1,(" + sql4 + ") T2 WHERE T1.RIQI=T2.RIQI";//  ORDER BY RIQI";

                sql = sql56;
                //按工艺返工

                string sql78;

                sql78 = " SELECT (CASE FLOWTYPE||EQPTYPE WHEN 'FEOLASML' THEN '_PE1' WHEN 'BEOLNIKON' THEN '_PE1' ELSE '_PE2' END) FLOWTYPE,RIQI,(CASE EQPTYPE WHEN 'ASML' THEN '' ELSE '' END) EQPTYPE,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY,SUM(MOVELOTQTY) AS MOVELOTQTY,SUM(MOVEWFRQTY) AS MOVEWFRQTY,ROUND(SUM(LOTQTY)/SUM(MOVELOTQTY)*100,2) AS LOTRATIO,ROUND(SUM(WFRQTY)/SUM(MOVEWFRQTY)*100,2) AS WFRRATIO FROM (" + sql12 + ") GROUP BY (CASE FLOWTYPE||EQPTYPE WHEN 'FEOLASML' THEN '_PE1' WHEN 'BEOLNIKON' THEN '_PE1' ELSE '_PE2' END),RIQI";




                sql = sql12 + " union " + sql34 + " union " + sql56 + " union " + sql78;

                sql = " SELECT RIQI,FLOWTYPE CATEGORY,EQPTYPE,LOTQTY,WFRQTY,MOVELOTQTY,MOVEWFRQTY,LOTRATIO,WFRRATIO FROM (" + sql + ") ORDER BY FLOWTYPE DESC,RIQI ASC";


         
     


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

           
        }
        public static void UpdateDb(string connStr)
        {
            //MessageBox.Show("更新返工数据和Move数据");
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
            //string strDateNew= tmp.AddDays(-10).ToString().Substring(0,10).Replace("/","-");
            string strDateNew = tmp.AddDays(-15).ToString("yyyy-MM-dd");
            //MessageBox.Show(strDate+"," +strDateNew);
        



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

            dt = LithoForm.LibF.CsmcOracle(sql);
            string dbTblName = "tbl_rework";
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            //MessageBox.Show("Rework Update Done");
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
            strDate = strDate + " 07:30:00";
            sql = "SELECT to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'yyyy')||to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'MM') MM,to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'yyyy')||to_char(to_date(to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd'),'yyyy-mm-dd'),'iw') WW, to_char(TRACKOUTTIME-7.5/24,'yyyy-mm-dd') DAY,TRACKOUTQTY QTY,STAGE,EQPID,decode(SUBSTR(EQPID,2,2),'LD','ASML','LI','NIKON','LS','NIKON','PENDING') as TOOL FROM RPTPRD.MFG_VIEW_STEP_MOVE WHERE substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') AND SUBSTR(EQPID,2,2) IN ('LD','LI','LS') AND TRACKOUTTIME >to_date('" + strDate + "','yyyy-mm-dd hh24:mi;ss')";

            sql = " SELECT MM,WW,DAY,TOOL,STAGE,  CAST(COUNT(QTY) AS float) as LOTQTY,CAST(SUM(QTY) AS float) AS WFRQTY FROM (" + sql + ") GROUP BY MM,WW,DAY,EQPID,STAGE ORDER BY DAY,TOOL";

            //count等统计函数计算的int值，即使在数据库中设为double字段，导入datatable,仍是int值，继续改为double，但在datatable做统计时，仍报错？？？？，用cast函数强制转换为double，同时针对oracle和SQLite数据
            dt = LithoForm.LibF.CsmcOracle(sql);
            dbTblName = "tbl_move";
            myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
           // MessageBox.Show("Move Update Done");

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
            //MessageBox.Show("P0 Update Done");
            MessageBox.Show(" Update Done");

        }
        public static void ManualUpdateReworkDB()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
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

            dt = LibF.CsmcOracle(sql);
            string dbTblName = "tbl_rework";
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
           
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
            dt = LibF.CsmcOracle(sql);
            dbTblName = "tbl_move";
            myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
          

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
          
            try
            {
                File.Copy(@"P:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);
               
            }
            catch
            {
               MessageBox.Show("没有权限读写P:\\_SQLITE目录");
            }
            sw.Stop();
            MessageBox.Show("===Rework Update Done====\n\n"+Convert.ToInt32(sw.Elapsed.TotalSeconds).ToString()+" Seconds");

        }





    }
}
