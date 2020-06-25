using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Data.OleDb;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WindowsFormsLitho
{
    class Nikon
    {

        public static DataTable PreAlignment(long sDate, long eDate, string connStr)
        {

            string sql;
            DataTable dt;
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

                sql = "SELECT T2.TOOL,T2.PPID, T1.QTY,T1.RANGE,T2.DATE,T2.SEARCH,T2.EGA FROM (" + sql3 + ") T1,(" + sql1 + ") T2 WHERE T1.NO=T2.NO ORDER BY T2.DATE,T2.TOOL";
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

        public static DataTable AlignMethod( string connStr,string ppid)
        {
            string sql;DataTable dt = null; 
            if (!ppid.Contains("."))
            {
                MessageBox.Show("文本框中PPID格式不对，无’.'区分,请重新输入"); 
                return dt;
            }
            else
            {
                string[] arr = ppid.Split(new char[] { '.' });
                ppid = "%" + arr[0].Trim() + "%.%" + arr[1].Trim() + "%";
            }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT DISTINCT TOOL,PPID,CAST(LSAREQSHOT AS DOUBLE) LSAREQSHOT,CAST(FIAREQSHOT AS DOUBLE) FIAREQSHOT,CAST(ALIGNNO as DOUBLE) ALIGNNO,SEARCH,EGA,DATE FROM TBL_INDEX  WHERE PPID LIKE '" + ppid + "'  ORDER BY PPID,TOOL,DATE";
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
        public static DataTable AlignData(long sDate, long eDate, string connStr,string ppid)
        {
            string sql; DataTable dt=null;
            if (!ppid.Contains("."))
            { MessageBox.Show("文本框中PPID格式不对，无’.'区分,请重新输入"); return dt; }
            else
            {  string[] arr = ppid.Split(new char[] { '.' });   ppid = "%" + arr[0].Trim() + "%.%" + arr[1].Trim() + "%";  }
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
            if (!ppid.Contains("."))
            { MessageBox.Show("文本框中PPID格式不对，无’.'区分,请重新输入"); return dt; }
            else
            { string[] arr = ppid.Split(new char[] { '.' }); ppid = "%" + arr[0].Trim() + "%.%" + arr[1].Trim() + "%"; }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  TOOL,PPID,NO,DATE FROM TBL_INDEX WHERE " +
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
        public static DataTable VectorData( string connStr, string str1,long str)
        {
            // https://blog.csdn.net/hanyingzhong/article/details/48653177
            //  所以建议一般情况下不要在字符串列建立索引，如果非要使用字符串索引，可以采用以下两种方法：
            //https://blog.csdn.net/hanyingzhong/article/details/48653177
            
            string sql;DataTable dt;
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
    }

    
}
