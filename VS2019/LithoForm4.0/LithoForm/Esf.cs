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

namespace LithoForm
{
    class Esf
    {
        public static DataTable EsfToolAvailableFromLithoWip()
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

                dt1 = LithoForm.LibF.CsmcOracle(sql);

                ///=============
                ///查询Constrain，并将其转化为正则表达式
                ///字段 TECH，PART，RECIPE，STAGE，EQPID，FLAG，TYPE，EQPTYPE （EQPID第三位）
                sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

                sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE FROM (" + sql + ")";

                dt2 = LithoForm.LibF.CsmcOracle(sql);

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


                dt1 = LithoForm.LibF.LINQToDataTable(query);
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
                return dt1;
            }
            catch
            {
                MessageBox.Show("更新失败；请确认是否可以访问MFG DB");
                dt1 = new DataTable();
                return dt1;
            }

        }
        public static DataTable ListAllToolAvailableFromLithoWip(bool choice)
        {
            string connStr, sql;
            DataTable dtShow;
            if (choice)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = " SELECT * FROM  tbl_esfToolAvailable";
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
            catch
            {
                dtShow = new DataTable();

                MessageBox.Show("Tbl_esfToolAvailable does not exist in local DB.\n\nPlease download ReworkMove.DB from drive P.");

            }
            return dtShow;
        }
        public static DataTable ListAllToolWipFromLithoWip(bool choice)
        {
            string connStr, sql;
            DataTable dtShow;
            if (choice)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = "SELECT TOOL,STAGE,SUM(WFRQTY) AS WFRQTY,COUNT(WFRQTY) AS LOTQTY,REFRESHDATE FROM tbl_esfToolAvailable GROUP BY  TOOL,STAGE,REFRESHDATE ORDER BY EQPTYPE,TOOL";
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
            catch
            {
                dtShow = new DataTable();

                MessageBox.Show("Tbl_esfToolAvailable does not exist in local DB.\n\nPlease download ReworkMove.DB from drive P.");

            }
            return dtShow;
        }
        public static DataTable ListAllToolWipFromLithoWipAll(bool choice)
        {
            string connStr, sql;
            DataTable dtShow;
            string tblName= "tbl_esfToolAvailable";



            if (choice)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = "SELECT TOOL,SUM(WFRQTY) AS WFRQTY,COUNT(WFRQTY) AS LOTQTY,REFRESHDATE FROM " + tblName + "  GROUP BY  TOOL,REFRESHDATE ORDER BY EQPTYPE,TOOL";
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
            catch
            {
                dtShow = new DataTable();

                MessageBox.Show("Tbl_esfToolAvailable does not exist in local DB.\n\nPlease download ReworkMove.DB from drive P.");

            }
            return dtShow;
        }
        public static DataTable ListToolNameQtyByPartStage(bool choice,string wip)
        {
            string connStr, sql;
            DataTable dt1, dt2;
            DataRow[] drs;
            string filterKey;
            string tools;
            string tblName;
            if (choice)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }
            
            if (wip == "photoWip")
            { tblName = "tbl_esfToolAvailable"; }
            else
            { tblName = "tbl_esfToolAvailableFabWip"; }


            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    if (wip == "photoWip")
                    {
                        sql = " SELECT DISTINCT TECH,PART,SUBSTR(RECIPE,1,5) RECIPE,SUBSTR(RECIPE,1,3) TOOLTYPE,STAGE,TOOL FROM  tbl_esfToolAvailable order by tool;" +
                            "SELECT  TECH,PART,SUBSTR(RECIPE,1,5) RECIPE,SUBSTR(RECIPE,1,3) TOOLTYPE,STAGE,COUNT(TOOL) ToolQty FROM  " + tblName + " GROUP BY TECH,PART,SUBSTR(RECIPE,1,5),SUBSTR(RECIPE,1,3) ,STAGE ORDER BY PART,SUBSTR(RECIPE,1,5)";
                    }
                    else
                    {
                        sql = " SELECT DISTINCT TECH,PART,SUBSTR(RECIPE,1,5) RECIPE,EQPTYPE TOOLTYPE,STAGE,TOOL FROM   " + tblName + " order by tool;" +
                           "SELECT  TECH,PART,SUBSTR(RECIPE,1,5) RECIPE,EQPTYPE TOOLTYPE,STAGE,COUNT(TOOL) ToolQty FROM  " + tblName + " GROUP BY TECH,PART,SUBSTR(RECIPE,1,5),EQPTYPE ,STAGE ORDER BY PART,SUBSTR(RECIPE,1,5)";
                    }
                    conn.Open();

                   

                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                dt1 = ds.Tables[0];
                                dt2 = ds.Tables[1];
                            }
                        }
                    }
                }

                
           


                dt2.Columns.Add("ToolName");

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    filterKey = "TECH='" + dt2.Rows[i]["TECH"] + "' and " +
                                    "PART='" + dt2.Rows[i]["PART"] + "' and " +
                                    "RECIPE='" + dt2.Rows[i]["RECIPE"] + "' and " +
                                    "STAGE='" + dt2.Rows[i]["STAGE"] + "'";

                    //  MessageBox.Show(filterKey);
                    drs = dt1.Select(filterKey);
                    // MessageBox.Show(drs.Length.ToString());
                    tools = "";
                    foreach (var row in drs)
                    {
                        tools += row["TOOL"] + ",";
                    }
                    dt2.Rows[i]["ToolName"] = tools.Substring(0, tools.Length - 1);
                }

            }
            catch
            {

                dt2 = new DataTable();

                MessageBox.Show("Tbl_esfToolAvailable does not exist in local DB.\n\nPlease download ReworkMove.DB from drive P.");

            }
            return dt2;

































        }
        public static DataTable EsfToolAvailableFromFabWip()
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
                sql = " SELECT  SUBSTR(PARTID,1,LENGTH(PARTID)-3) PART,SUBSTR(RECPID,1,LENGTH(RECPID)-3) RECIPE,STAGE,SUBSTR(RECPID,1,3) EQPTYPE from RPTPRD.MFG_VIEW_FLOW WHERE SUBSTR(RECPID,1,3) IN ('LDI','LII') AND SUBSTR(PARTID,1,LENGTH(PARTID)-3) IN (" + sql+")";
              
                dt1 = LithoForm.LibF.CsmcOracle(sql);
                
                ///=============
                ///查询Constrain，并将其转化为正则表达式
                ///字段 TECH，PART，RECIPE，STAGE，EQPID，FLAG，TYPE，EQPTYPE （EQPID第三位）
                sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

                sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE FROM (" + sql + ")";

                dt2 = LithoForm.LibF.CsmcOracle(sql);

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


                dt1 = LithoForm.LibF.LINQToDataTable(query);

               
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
                return dt1;
            }
            catch
            {
                MessageBox.Show("更新失败；请确认是否可以访问MFG DB");
                dt1 = new DataTable();
                return dt1;
            }

        }
    }
}
