using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Data.OleDb;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LithoForm
{
    class Asml
    {


        #region //batch report
        public static string[] ListBatchReportParameter()
        {
            string[] paraArr ={"tool",




"jobModified",

"doseActual",
"doseJob",
"focusActual",
"focusJob",
"focusRxActual",
"focusRxJob",
"focusRyActual",
"focusRyJob",
"aperture",
"illuminationMode",
"sigmaOutActual",
"sigmaOutJob",
"sigmaInActual",
"sigmaInJob",
"alignStrategy",
"markRequired",
"spmMarkScan",
"MaxDynErrCount",
"MaxDoseErrCount",
"deltaRedX",
"deltaRedY",
"deltaGreenX",
"deltaGreenY",
"batchStart",
"batchFinish",
"wfrBatchOut",
"wfrAccept",
"wfrReject",
"throughputWfrFirst",
"throughputWfrMax",
"throughputWfrMin",
"alignRecipe",
"tranXave",
"tranYave",
"expXave",
"expYave",
"wfrRotAve",
"wfrOrtAve",
"tranXdev",
"tranYdev",
"expXdev",
"expYdev",
"wfrRotDev",
"wfrOrtDev",
"reticleMagAve",
"reticleRotAve",
"redXave",
"redYave",
"greenXave",
"greenYave",
"reticleMagDev",
"reticleRotDev",
"redXdev",
"redYdev",
"greenXdev",
"greenYdev",
"globalLevelDzAve",
"globalLevelPhixAve",
"globalLevelPhiyAve",
"blueFocusAve",
"blueRyAve",
"globalLevelDzDev",
"globalLevelPhixDev",
"globalLevelPhiyDev",
"blueFocusDev",
"blueRyDev",
"fieldLevelDzMinAve",
"fieldLevelPhixMinAve",
"fieldLevelPhiyMinAve",
"fieldLevelDzMaxAve",
"fieldLevelPhixMaxAve",
"fieldLevelPhiyMaxAve",
"fieldLevelDzMinDev",
"fieldLevelPhixMinDev",
"fieldLevelPhiyMinDev",
"fieldLevelDzMaxDev",
"fieldLevelPhixMaxDev",
"fieldLevelPhiyMaxDev",
"fieldLevelDzMeanAve",
"fieldLevelPhixMeanAve",
"fieldLevelPhiyMeanAve",
"fieldLevelDzSigmaAve",
"fieldLevelPhixSigmaAve",
"fieldLevelPhiySigmaAve",
"fieldLevelDzMeanDev",
"fieldLevelPhixMeanDev",
"fieldLevelPhiyMeanDev",
"fieldLevelDzSigmaDev",
"fieldLevelPhixSigmaDev",
"fieldLevelPhiySigmaDev",
"MaXmin",
"MaYmin",
"MaRzMin",
"MaZmin",
"MsdXmin",
"MsdYmin",
"MsdRzMin",
"MsdZmin",
"MaXmax",
"MaYmax",
"MaRzMax",
"MaZmax",
"MsdXmax",
"MsdYmax",
"MsdRzMax",
"MsdZmax",
"MaXave",
"MaYave",
"MaRzAve",
"MaZave",
"MsdXave",
"MsdYave",
"MsdRzAve",
"MsdZave",
"MaXdev",
"MaYdev",
"MaRzDev",
"MaZdev",
"MsdXdev",
"MsdYdev",
"MsdRzDev",
"MsdZdev",
"intraFieldRxMinAve",
"intraFieldRxMaxAve",
"intraFieldRxAveAve",
"intraFieldRxDevAve",
"intraFieldRxMinAve.1",
"intraFieldRyMaxAve",
"intraFieldRyAveAve",
"intraFieldRyDevAve",
"intraFieldRxMinDev",
"intraFieldRxMaxDev",
"intraFieldRxAveDev",
"intraFieldRxDevDev",
"intraFieldRxMinDev.1",
"intraFieldRyMaxDev",
"intraFieldRyAveDev",
"intraFieldRyDevDev",
"TiltZplaneDzAve",
"TiltZplanePhixAve",
"TiltZplanePhiyAve",
"TiltZplaneMccAve",
"TiltResidualRxAve",
"TiltResidualRyAve",
"TiltZplaneDzDev",
"TiltZplanePhixDev",
"TiltZplanePhiyDev",
"TiltZplaneMccDev",
"TiltResidualRxDev",
"TiltResidualRyDev",
"doseError",
"Reticle",
"Inside_S",
"Inside_M",
"Inside_L",
"Outside_S",
"Outside_M",
"Outside_L",
"Full_S",
"Full_M",
"Full_L",
"focusErr",
"dynErr",
"doseErr",
"bqErr",
"chuckErr",
"xColorRG",
"xColorR",
"xColorG",
"yColorRG",
"yColorR",
"yColorG",
"xpaLargestOrderRedMax",
"xpaLargestOrderRedMin",
"xpaLargestOrderGreenMax",
"xpaLargestOrderGreenMin",
"xpaWorstRedWqMax",
"xpaWorstRedWqMin",
"xpaWorstGreenWqMax",
"xpaWorstGreenWqMin",
"xLargestOrderRedMax",
"xLargestOrderRedMin",
"xLargestOrderGreenMax",
"xLargestOrderGreenMin",
"xWorstRedWqMax",
"xWorstRedWqMin",
"xWorstGreenWqMax",
"xWorstGreenWqMin",
"yLargestOrderRedMax",
"yLargestOrderRedMin",
"yLargestOrderGreenMax",
"yLargestOrderGreenMin",
"yWorstRedWqMax",
"yWorstRedWqMin",
"yWorstGreenWqMax",
"yWorstGreenWqMin",
"xResidualMax",
"xResidualMin",
"xResidualAve",
"xResidualDev",
"yResidualMax",
"yResidualMin",
"yResidualAve",
"yResidualDev",
"batchType",
"QabovePoffsetAve",
"QabovePoffsetDev",
"blueFocusMax",
"blueFocusMin",
"blueFocusRange",
"markType"};
            return paraArr;
        }
        public static DataTable QueryPara(string connStr, string sDate, string eDate, string para, string part, string layer)
        {
           
            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT DATE,TIME,LOTID,JOBNAME,LAYER," + para + " FROM TBL_ASMLBATCHREPORT" +
                    "  WHERE DATE(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)) BETWEEN DATE('" + sDate + "') AND DATE('" + eDate + "')" +
                    "  AND JOBNAME LIKE '%" + part + "%'" +
                    "  AND LAYER LIKE '%" + layer + "%' " +
                    "  ORDER BY JOBNAME,LAYER,TOOL";

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
        public static DataTable QueryIllumination(string connStr, string part, string layer)
        {

            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT tool,date,jobName part,layer,illuminationmode,aperture,sigmaoutjob,sigmainjob,dosejob,focusjob,alignstrategy," +
                    " substr(date,7,4)|| substr(date,1,2)|| substr(date,4,2) key from tbl_asmlbatchreport ";
                sql += " WHERE Layer='" + layer + "' and jobname='" + part + "'";
                string sql1;
                sql1 = "select tool,date,part,layer,illuminationmode,aperture,sigmaoutjob,sigmainjob,dosejob,focusjob,alignstrategy from (" + sql + ") order by key desc limit 0,10" ;
              

                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql1, conn))
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

      
        public static DataTable QueryIlluminationTechLayer(string connStr,string connStr1, string tech, string layer)
        {

            string sql; 
            //get part and trackrecipe
            DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr1))
            {
                sql = "SELECT DISTINCT  PART  FROM TBL_TECH WHERE SUBSTR(TECH,1,3)='" + tech + "'";
                sql = "SELECT PART,TRACKRECIPE FROM TBL_FLOWTRACK WHERE PART IN (SELECT DISTINCT  PART  FROM TBL_TECH WHERE SUBSTR(TECH,1,3)='" + tech + "') AND LAYER='"+layer+"' AND TOOLTYPE='ASML'";
            
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
            if (dt.Rows.Count == 0) { MessageBox.Show("当前工艺代码和层次设定：__" + tech + "__"+layer+"___不含任何产品\r\n\r\n\r\n退出，请确认");return dt; }
            //generate part list
            string key = string.Empty;
            foreach(DataRow row in dt.Rows)
            { key += "'PROD/" + row["part"].ToString() + "',"; }
            key = "(" + key + "' ')";
            //query na/sigma
            DataTable dt1;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT distinct substr(jobname,6,60) as part,illuminationmode||','||aperture||','||sigmaoutjob||','||sigmainjob as illumination from tbl_asmlbatchreport ";
                sql = " SELECT distinct substr(jobname,6,60) as part,illuminationmode,aperture,sigmaoutjob,sigmainjob from tbl_asmlbatchreport ";
                sql += " WHERE Layer='" + layer + "' and jobname in " + key;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];

                        }
                    }
                }
            }

            var query =
               from rHead in dt1.AsEnumerable()
               join rTail in dt.AsEnumerable()
             
              on new { a = rHead.Field<string>("part") } equals new { a = rTail.Field<string>("part") }



               select rHead.ItemArray.Concat(rTail.ItemArray.Skip(1));



            DataTable dt2 = dt1.Clone();
            dt2.Columns.Add("TrackRecipe");


            foreach (var obj in query)
            {
                DataRow dr = dt2.NewRow();
                dr.ItemArray = obj.ToArray();
                dt2.Rows.Add(dr);
            }

            DataView dv = dt2.DefaultView;
            dv.Sort = ("TrackRecipe");
           



            MessageBox.Show(dt2.Rows.Count.ToString());



            return dv.ToTable();
        }
        public static DataTable QueryFocusTechLayer(string connStr, string connStr1, string tech, string layer)
        {

            string sql;
            //get part and trackrecipe
            DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr1))
            {
                sql = "SELECT DISTINCT  PART  FROM TBL_TECH WHERE SUBSTR(TECH,1,3)='" + tech + "'";
                sql = "SELECT PART,TRACKRECIPE FROM TBL_FLOWTRACK WHERE PART IN (SELECT DISTINCT  PART  FROM TBL_TECH WHERE SUBSTR(TECH,1,3)='" + tech + "') AND LAYER='" + layer + "' AND TOOLTYPE='ASML'";

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
            if (dt.Rows.Count == 0) { MessageBox.Show("当前工艺代码和层次设定：__" + tech + "__" + layer + "___不含任何产品\r\n\r\n\r\n退出，请确认"); return dt; }
            //generate part list
            string key = string.Empty;
            foreach (DataRow row in dt.Rows)
            { key += "'PROD/" + row["part"].ToString() + "',"; }
            key = "(" + key + "' ')";
            //query focus
            DataTable dt1;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
               
                sql = " SELECT distinct tool,substr(jobname,6,60) as part,focusjob,focusactual from tbl_asmlbatchreport ";
                sql += " WHERE Layer='" + layer + "' and jobname in " + key;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];

                        }
                    }
                }
            }

            var query =
               from rHead in dt1.AsEnumerable()
               join rTail in dt.AsEnumerable()

              on new { a = rHead.Field<string>("part") } equals new { a = rTail.Field<string>("part") }



               select rHead.ItemArray.Concat(rTail.ItemArray.Skip(1));



            DataTable dt2 = dt1.Clone();
            dt2.Columns.Add("TrackRecipe");


            foreach (var obj in query)
            {
                DataRow dr = dt2.NewRow();
                dr.ItemArray = obj.ToArray();
                dt2.Rows.Add(dr);
            }

            DataView dv = dt2.DefaultView;
            dv.Sort = ("TrackRecipe");




            MessageBox.Show(dt2.Rows.Count.ToString());



            return dv.ToTable();
        }

        public static DataTable SummaryParaByName(string connStr, string sDate, string eDate, string para, string part, string layer)
        {
       
            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT TOOL,JOBNAME,LAYER,Count(" + para + ") AS " + para + "_qty FROM TBL_ASMLBATCHREPORT" +
                    "  WHERE DATE(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)) BETWEEN DATE('" + sDate + "') AND DATE('" + eDate + "')" +
                    "  AND JOBNAME LIKE '%" + part + "%'" +
                    "  AND LAYER LIKE '%" + layer + "%' " +
                    "  GROUP BY JOBNAME,LAYER,TOOL";

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
        public static DataTable SummaryParaByValue(string connStr, string sDate, string eDate, string para, string part, string layer)
        {
            
            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT TOOL,JOBNAME,LAYER,Count(" + para + ") AS " + para + "_qty " +
                    ",MAX(" + para + ") AS " + para + "_max " +
                    ",MIN(" + para + ") AS " + para + "_min " +
                     ",AVG(" + para + ") AS " + para + "_avg " +

                    "FROM TBL_ASMLBATCHREPORT" +
                    "  WHERE DATE(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)) BETWEEN DATE('" + sDate + "') AND DATE('" + eDate + "')" +
                    "  AND JOBNAME LIKE '%" + part + "%'" +
                    "  AND LAYER LIKE '%" + layer + "%' " +
                    "  GROUP BY JOBNAME,LAYER,TOOL";

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
        public static DataTable DistinctParaByValue(string connStr, string sDate, string eDate, string para, string part, string layer)
        {
            
            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT distinct TOOL,JOBNAME,LAYER," + para +

                    "  FROM TBL_ASMLBATCHREPORT" +
                    "  WHERE DATE(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)) BETWEEN DATE('" + sDate + "') AND DATE('" + eDate + "')" +
                    "  AND JOBNAME LIKE '%" + part + "%'" +
                    "  AND LAYER LIKE '%" + layer + "%' " +
                    "  ORDER BY JOBNAME,LAYER,TOOL";

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
        public static void UpdateBatchReport()
        {
            string connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB";
            connStr = @"data source=d:\temp\db\AsmlBatchreport.DB";
            string root = @"\\10.4.72.74\litho\ASML_BATCH_REPORT\";
            string[] tools = { "87", "89", "86", "85", "83", "82", "8C", "8B", "8A", "08", "7D" };
            string path = string.Empty;
            DataTable dt, dt1;
            string sql;
            string maxDate;
            DataRow[] drs;
            foreach (string tool in tools)
            {
                path = root + tool + "_batchreport.csv";

                if (tool == "08" || tool == "7D")
                {
                    sql = "SELECT max(DATETIME(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)||' '||TIME)) FROM TBL_ASMLBATCHREPORT WHERE TOOL='BLSD" + tool + "'";
                }
                else
                {
                    sql = "SELECT max(DATETIME(SUBSTR(date,7,4)|| '-'||SUBSTR(date,1,2)||'-'||SUBSTR(date,4,2)||' '||TIME)) FROM TBL_ASMLBATCHREPORT WHERE TOOL='ALSD" + tool + "'";
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
                maxDate = dt.Rows[0][0].ToString();

                dt = LithoForm.LibF.OpenCsvNew(path);
                dt.Columns.Add("maxDate");
                string str, str1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = dt.Rows[i]["date"].ToString();
                    str1 = dt.Rows[i]["time"].ToString();
                    if (str1.Length == 4) { str1 = "0" + str1; }
                    dt.Rows[i]["maxDate"] = str.Substring(6, 4) + "-" + str.Substring(0, 2) + "-" + str.Substring(3, 2) + " " + str1 + ":00";
                }
                drs = dt.Select("maxDate>'" + maxDate + "'");


                MessageBox.Show(maxDate + "," + dt.Rows.Count.ToString() + "," + drs.Length.ToString());



                return;



            }
        }
        #endregion

        #region //Awe1

        /*
        Dictionary<string, string> toolid = new Dictionary<string, string>() { { "4666", "ALSD82" },{ "4730", "ALSD83" },
                {"6450", "ALSD85" },{ "8144","ALSD86" },{ "4142", "ALSD87" },{"6158","ALSD89" },{"5688","ALSD8A"},{"4955","ALSD8B" },
                { "9726","ALSD8C" },{"8111","BLSD7D" },{ "3527","BLSD08"} };
        public static void ReadAweFile()
        {
           
            string[] lines = System.IO.File.ReadAllLines(@"C:\TEMP\AWFT_CLD206.5_0_W1_NAH325374_B5.awe");
            MessageBox.Show(lines.Length.ToString());
            StringBuilder sb = new StringBuilder();
            foreach (string x in lines)
            {
                sb.Append(x);
                sb.Append("\r\n");
                MessageBox.Show(x);

            }
          
          

        }
        */


        public static void UpdateAweIndex()
        {
            string csvPath = @"\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\index.csv";
            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            string sql;
            DataTable dt;


            //create table
            StringBuilder item;
            string tblName;
            bool flag;
            flag = LithoForm.LibF.CheckTableExist(connStr, "tbl_index");
            tblName = "tbl_index";
            item = new StringBuilder();
            item.Append("(");
            item.Append("date INT,");
            item.Append("time,part,layer,tool,lot,mark,recipe, id INT64");
            item.Append(")");
            if (flag == false)
            {
                LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
            }























            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT max(id)  FROM TBL_INDEX";
                sql = "select id from tbl_index where rowid = (select max(rowid) from tbl_index)";

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

            long rowqty;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
            {
                rowqty = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            { rowqty = 0; }


            MessageBox.Show("Max ID in DB is: " + rowqty.ToString());

            dt = LithoForm.LibF.OpenCsv(csvPath);

            MessageBox.Show("Max ID in Csv File is: " + dt.Rows[dt.Rows.Count - 1]["No"].ToString());

            dt.Columns.Add("id", typeof(System.Int64));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["no"].ToString().Replace("No", ""));

                dt.Rows[i]["date"] = dt.Rows[i]["date"].ToString().Replace("_", "");

            }
            dt.Columns.Remove("no");

            {
                DataRow[] drs = dt.Select("id>" + rowqty);
                //    MessageBox.Show("Rows To Be Inserted: " + drs.Length.ToString());
                DataTable newDt = dt.Clone();
                if (drs.Length > 0)
                {
                    foreach (var row in drs)
                    {
                        newDt.ImportRow(row);
                    }
                    DataTableToSQLte myTabInfo = new DataTableToSQLte(newDt, "tbl_index");
                    myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\AsmlAwe.DB");
                }


                MessageBox.Show("Completed Refreshing Asml Awe Index");
            }
        }

        public static DataTable AweIndexQuery(long sDate, long eDate, string connStr, string part, string layer, string connStr1)
        {
            string sql; DataTable dt = null; DataTable dt1 = null;
            StringBuilder sb = new StringBuilder();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT *  FROM tbl_index  WHERE (DATE BETWEEN " + sDate + " AND " + eDate + ") " +
                      " AND PART LIKE '" + part + "' " +
                      " AND LAYER LIKE '" + layer + "'  ORDER BY PART,LAYER,TOOL,DATE,RECIPE";
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

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("未在指定条件下筛选到数据，退出");
                return dt;
            }
            //列出Part唯一项
            dt1 = dt.DefaultView.ToTable(true, "part"); //https://blog.csdn.net/sinat_40692412/article/details/85781736
            //生成sql字段，查询工艺代码
            sb.Append("(");
            foreach (DataRow row in dt1.Rows)
            {
                sb.Append("'" + row["part"] + "',");
            }
            string str1 = sb.ToString().Substring(0, sb.ToString().Length - 1);
            str1 += ")";

            //查询工艺代码
            using (SQLiteConnection conn = new SQLiteConnection(connStr1))
            {
                sql = " SELECT DISTINCT PART,FULLTECH ,TECH FROM tbl_cdconfig WHERE TECH!=''AND PART IN " + str1;
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];

                        }
                    }
                }
            }

            

            //合并工艺代码
            dt.Columns.Add("fulltech");
            dt.Columns.Add("tech");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0 && dt.Rows[i]["part"] == dt.Rows[i - 1]["part"])
                {
                    dt.Rows[i]["tech"] = dt.Rows[i - 1]["tech"].ToString();
                    dt.Rows[i]["fulltech"] = dt.Rows[i - 1]["fulltech"].ToString();
                }
                else
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["part"].ToString() == dt1.Rows[j]["PART"].ToString())
                        {
                            dt.Rows[i]["tech"] = dt1.Rows[j]["TECH"].ToString();
                            dt.Rows[i]["fulltech"] = dt1.Rows[j]["FULLTECH"].ToString();
                            break;
                        }

                    }
                }
            }




            return dt;


        }
        public static void UpdateAweVector(string tool)
        {

            string root = @"\\10.4.3.130\ftpdata\LITHO\ExcelCsvFile\AsmlAwe\";

            List<string> list;
            List<string> fileList;

            CreateDbTable(tool);

            string dir;
            list = new List<string>();
            fileList = new List<string>();
            dir = root + @"\" + tool;
            list = LithoForm.LibF.ExportFileList(dir, fileList);
            foreach (string x in list)
            {

                if (x.Contains("2020_02") && x.Contains("parameter.csv"))
                {
                    //  MessageBox.Show(tool + "," + x + " is to be refreshed");
                    UpdateParameter(tool, x);

                }
                else if (x.Contains("2020_02") && x.Contains("_WQ_MCC_DELTA"))
                {
                    //   MessageBox.Show(tool + "," + x + " is to be refreshed");
                    UpdateWqMccDelta(tool, x);
                }
                 else if (x.Contains("2020_02") && x.Contains("vector"))
                //else if (x.Contains("2020_02") && x.Contains("activeVector"))

             
                {
                    //  MessageBox.Show(tool + "," + x + " is to be refreshed");
                    UpdateVectorNew(tool, x);


                    //return;
                }
            }



        }

        static void CreateDbTable(string tool)
        {
            StringBuilder item;
            string tblName;
            bool flag;
            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            //parameter
            flag = LithoForm.LibF.CheckTableExist(connStr, "tbl_" + tool + "_para");
            tblName = "tbl_" + tool + "_para";
            item = new StringBuilder();
            item.Append("(");
            item.Append("wfrid INT,");
            item.Append(" r5x_tr,  r5x_exp, r5x_rotort,  r5y_tr, r5y_rotort,  r5y_exp, g5x_tr,  g5x_exp, g5x_rotort , " +
                "g5y_tr, g5y_rotort,  g5y_exp, sm5x_tr ,sm5x_exp, sm5x_rotort ,sm5y_tr, sm5y_rotort, sm5y_exp, id INT64");
            item.Append(")");
            if (flag == false)
            {
                LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
            }

            //WQ,MCC,DELTA
            //字段名更换，将第一位数字，移到第二位
            tblName = "tbl_" + tool + "_WqMccDelta";
            flag = LithoForm.LibF.CheckTableExist(connStr, tblName);

            item = new StringBuilder();
            item.Append("(");
            item.Append("WaferNr INT,MarkNr,RedMCC1,RedWQ1,RedMCC5,RedWQ5,GreenMCC1,GreenWQ1,GreenMCC5,GreenWQ5,RedDelta,GreenDelta,XY,id INT64");
            item.Append(")");
            if (flag == false)
            {
                LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
            }



            ///Vector
            ///去掉了key字段
            tblName = "tbl_" + tool + "_measured";
            flag = LithoForm.LibF.CheckTableExist(connStr, tblName);

            item = new StringBuilder();
            item.Append("(");
            item.Append("WaferNr INT,MarkNrX,MarkNrY,X,Y,type,id INT64");
            item.Append(")");
            if (flag == false)
            {
                LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
            }

            tblName = "tbl_reference";
            flag = LithoForm.LibF.CheckTableExist(connStr, tblName);

            item = new StringBuilder();
            item.Append("(");
            item.Append("WaferNr INT,MarkNrX,X,Y,id INT64");
            item.Append(")");
            if (flag == false)
            {
                LithoForm.LibF.CreateSqliteTable(connStr, tblName, item.ToString());
            }


        }
        public static void UpdateParameter(string tool, string csvPath)
        {

            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            string sql;
            DataTable dt;
            string tblName = "tbl_" + tool + "_para";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                sql = "select id from " + tblName + "  where rowid = (select max(rowid) from " + tblName + " )";

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
            long rowqty;
            if (dt.Rows.Count > 0)
            {
                rowqty = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            { rowqty = 0; }



            // MessageBox.Show("Max ID in DB is: " + rowqty.ToString());

            dt = LithoForm.LibF.OpenCsv(csvPath);

            // MessageBox.Show("Max ID in Csv File is: " + dt.Rows[dt.Rows.Count - 1]["No"].ToString());

            dt.Columns.Add("id", typeof(System.Int64));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["no"].ToString().Replace("No", ""));

            }
            dt.Columns.Remove("no");

            {
                DataRow[] drs = dt.Select("id>" + rowqty);
                // MessageBox.Show("Rows To Be Inserted: " + drs.Length.ToString());
                DataTable newDt = dt.Clone();
                if (drs.Length > 0)
                {
                    foreach (var item in drs)
                    {
                        newDt.ImportRow(item);
                    }
                    DataTableToSQLte myTabInfo = new DataTableToSQLte(newDt, tblName);
                    myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\AsmlAwe.DB");
                    // MessageBox.Show("Completed Refreshing Asml Parameter" + "," + tool);
                }
                else
                {
                    // MessageBox.Show("No New Data of  Asml Parameter" + "," + tool);
                }



            }
        }

        public static void UpdateWqMccDelta(string tool, string csvPath)
        {
            // "WaferNr INT,MarkNr,RedMCC1,RedWQ1,RedMCC5,RedWQ5,GreenMCC1,GreenWQ1,GreenMCC5,GreenWQ5,RedDeltaX,GreenDeltaX,XY,id INT64"
            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            string sql;
            DataTable dt;
            string tblName = "tbl_" + tool + "_WqMccDelta";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                if (csvPath.Contains("X_WQ_MCC_DELTA"))
                {
                    sql = "select MAX(id) from " + tblName + "  where XY='X'";
                    // MessageBox.Show(sql);
                }
                else
                {
                    sql = "select MAX(id) from " + tblName + "  where XY='Y'";
                    // MessageBox.Show(sql);

                }


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

            long rowqty;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
            {
                rowqty = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            { rowqty = 0; }



            // MessageBox.Show("Max ID in DB is: " + rowqty.ToString());

            dt = LithoForm.LibF.OpenCsv(csvPath);

            // MessageBox.Show("Max ID in Csv File is: " + dt.Rows[dt.Rows.Count - 1]["No"].ToString());

            dt.Columns.Add("id", typeof(System.Int64));
            dt.Columns.Add("RedMCC1"); dt.Columns.Add("RedWQ1");
            dt.Columns.Add("RedMCC5"); dt.Columns.Add("RedWQ5");
            dt.Columns.Add("GreenMCC1"); dt.Columns.Add("GreenWQ1");
            dt.Columns.Add("GreenMCC5"); dt.Columns.Add("GreenWQ5");
            dt.Columns.Add("RedDelta");
            dt.Columns.Add("GreenDelta");
            dt.Columns.Add("XY");




            if (csvPath.Contains("X_WQ_MCC_DELTA"))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["no"].ToString().Replace("No", ""));
                    dt.Rows[i]["RedMCC1"] = dt.Rows[i]["1XRedMCC"];
                    dt.Rows[i]["RedMCC5"] = dt.Rows[i]["5XRedMCC"];
                    dt.Rows[i]["GreenMCC1"] = dt.Rows[i]["1XGreenMCC"];
                    dt.Rows[i]["GreenMCC5"] = dt.Rows[i]["5XGreenMCC"];
                    dt.Rows[i]["RedWQ1"] = dt.Rows[i]["1XRedWQ"];
                    dt.Rows[i]["RedWQ5"] = dt.Rows[i]["5XRedWQ"];
                    dt.Rows[i]["GreenWQ1"] = dt.Rows[i]["1XGreenWQ"];
                    dt.Rows[i]["GreenWQ5"] = dt.Rows[i]["5XGreenWQ"];
                    dt.Rows[i]["XY"] = "X";
                    dt.Rows[i]["RedDelta"] = dt.Rows[i]["XRedDelta"];
                    dt.Rows[i]["GreenDelta"] = dt.Rows[i]["XGreenDelta"];


                }
                dt.Columns.Remove("no");
                dt.Columns.Remove("1XRedMCC");
                dt.Columns.Remove("5XRedMCC");
                dt.Columns.Remove("1XGreenMCC");
                dt.Columns.Remove("5XGreenMCC");
                dt.Columns.Remove("1XRedWQ");
                dt.Columns.Remove("5XRedWQ");
                dt.Columns.Remove("1XGreenWQ");
                dt.Columns.Remove("5XGreenWQ");
                dt.Columns.Remove("XRedDelta");
                dt.Columns.Remove("XGreenDelta");


            }
            else
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["no"].ToString().Replace("No", ""));
                    dt.Rows[i]["RedMCC1"] = dt.Rows[i]["1YRedMCC"];
                    dt.Rows[i]["RedMCC5"] = dt.Rows[i]["5YRedMCC"];
                    dt.Rows[i]["GreenMCC1"] = dt.Rows[i]["1YGreenMCC"];
                    dt.Rows[i]["GreenMCC5"] = dt.Rows[i]["5YGreenMCC"];
                    dt.Rows[i]["RedWQ1"] = dt.Rows[i]["1YRedWQ"];
                    dt.Rows[i]["RedWQ5"] = dt.Rows[i]["5YRedWQ"];
                    dt.Rows[i]["GreenWQ1"] = dt.Rows[i]["1YGreenWQ"];
                    dt.Rows[i]["GreenWQ5"] = dt.Rows[i]["5YGreenWQ"];
                    dt.Rows[i]["XY"] = "Y";
                    dt.Rows[i]["RedDelta"] = dt.Rows[i]["YRedDelta"];
                    dt.Rows[i]["GreenDelta"] = dt.Rows[i]["YGreenDelta"];


                }
                dt.Columns.Remove("no");
                dt.Columns.Remove("1YRedMCC");
                dt.Columns.Remove("5YRedMCC");
                dt.Columns.Remove("1YGreenMCC");
                dt.Columns.Remove("5YGreenMCC");
                dt.Columns.Remove("1YRedWQ");
                dt.Columns.Remove("5YRedWQ");
                dt.Columns.Remove("1YGreenWQ");
                dt.Columns.Remove("5YGreenWQ");
                dt.Columns.Remove("YRedDelta");
                dt.Columns.Remove("YGreenDelta");


            }






            DataRow[] drs = dt.Select("id>" + rowqty);
            //  MessageBox.Show("Rows To Be Inserted: " + drs.Length.ToString());
            DataTable newDt = dt.Clone();
            if (drs.Length > 0)
            {
                foreach (var item in drs)
                {
                    newDt.ImportRow(item);
                }
                DataTableToSQLte myTabInfo = new DataTableToSQLte(newDt, tblName);
                myTabInfo.ImportToSqliteBatch(newDt, @"P:\_SQLite\AsmlAwe.DB");
                // MessageBox.Show("Completed Refreshing Asml WQ_MCC_DELTA" + "," + tool);
            }
            else
            {
                //MessageBox.Show("No New Data of  Asml WQ_MCC_DELTA" + "," + tool);
            }




        }

        public static void UpdateVector(string tool, string csvPath)
        {

            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            string sql;
            DataTable dt; DataRow[] drs;
            string tblName = "tbl_" + tool + "_measured";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                sql = "select id from " + tblName + "  where rowid = (select max(rowid) from " + tblName + " )";

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
            long rowqty;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
            {
                rowqty = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            { rowqty = 0; }

            // MessageBox.Show("Max ID in DB is: " + rowqty.ToString());

            dt = LithoForm.LibF.OpenCsvNew(csvPath);

            // MessageBox.Show("Max ID in CSV file is: " + dt.Rows[dt.Rows.Count-1]["No"].ToString());


            dt.Columns.Add("id", typeof(System.Int64));



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["id"] = Convert.ToInt64(dt.Rows[i]["No"].ToString().Replace("No", ""));

            }
            dt.Columns.Remove("no");
            dt.Columns.Remove("key");
            dt.Columns.Remove("key0");
            dt.Columns.Remove("key1");
            // dt.Columns.Remove("MarkNrY");

            ///列出未更新的




            drs = dt.Select("id>" + rowqty);
            //  MessageBox.Show("Rows To Be Inserted: " + drs.Length.ToString());
            DataTable newDt = dt.Clone();
            if (drs.Length > 0)
            {
                foreach (var item in drs)
                {
                    newDt.ImportRow(item);
                }

                dt = newDt.Copy();




                /// 按No索引，列出基准坐标，保存到reference table，其它测试结果，保存到measured table
                /// 单一lot（NO/ID）的数据结构是：类型按R5_Measured,G5_Measured,Sm Measured 排序
                /// 每一类型的前半部分是基准坐标，后半部分是测试坐标

                DataTable idSet = dt.DefaultView.ToTable(true, "id"); //https://blog.csdn.net/sinat_40692412/article/details/85781736
                DataTable reference = dt.Clone();
                DataTable measured = dt.Clone();





                foreach (DataRow id in idSet.Rows)
                {
                    ///基准坐标
                    drs = dt.Select("id=" + id["id"] + " and WaferNr='1' and type='R5_Measured'");
                    for (int i = 0; i < drs.Length / 2; i++)
                    {
                        reference.ImportRow(drs[i]);
                    }
                    ///测试，residual坐标
                    foreach (string typeItem in new string[] { "R5_Measured", "R5_Residual", "G5_Measured", "G5_Residual", "Sm_Measured", "Sm_Residual" })
                    {
                        drs = dt.Select("id=" + id["id"] + " and type='" + typeItem + "'");
                        for (int i = drs.Length / 2; i < drs.Length; i++)
                        {
                            measured.ImportRow(drs[i]);
                        }
                    }


                }


                //  MessageBox.Show(reference.Rows.Count.ToString());
                //  MessageBox.Show(measured.Rows.Count.ToString());

                reference.Columns.Remove("type");


                ///复制到数据库
                tblName = "tbl_" + tool + "_measured";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(measured, tblName);
                myTabInfo.ImportToSqliteBatch(measured, @"P:\_SQLite\AsmlAwe.DB");
                tblName = "tbl_reference";
                myTabInfo = new DataTableToSQLte(reference, tblName);
                myTabInfo.ImportToSqliteBatch(reference, @"P:\_SQLite\AsmlAwe.DB");

            }
            else
            {
                // MessageBox.Show("No New Data of  Asml Parameter" + "," + tool);
            }




        }
        public static void UpdateVectorNew(string tool, string csvPath)
        {

            string connStr = @"data source=P:\_SQLite\AsmlAwe.DB";
            string sql;
            DataTable dt; DataRow[] drs;
            DataTable newDt;
            DataTable reference;
            DataTable measured;
            DataTableToSQLte myTabInfo;
            string tblName = "tbl_" + tool + "_measured";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                sql = "select id from " + tblName + "  where rowid = (select max(rowid) from " + tblName + " )";

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
            long rowqty;
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
            {
                rowqty = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            { rowqty = 0; }



            dt = LithoForm.LibF.OpenCsvNew(csvPath);
            // dt.Columns.Remove("MarkNrY");
            dt.Columns.Remove("key");
            dt.Columns.Remove("key0");
            dt.Columns.Remove("key1");
            dt.Columns.Add("id", typeof(System.Int64));

            long id;

            foreach (DataRow item in dt.DefaultView.ToTable(true, "No").Rows)
            {
                id = Convert.ToInt64(item["No"].ToString().Replace("No", ""));






                if (id > rowqty)
                {

                    newDt = dt.Clone();
                    reference = newDt.Clone();
                    measured = newDt.Clone();
                    drs = dt.Select("No='" + item["No"].ToString() + "'");



                    foreach (var row in drs)
                    { newDt.ImportRow(row); }
                    for (int i = 0; i < newDt.Rows.Count; i++) { newDt.Rows[i]["id"] = id; }



                    ///基准坐标
                    drs = newDt.Select("  WaferNr='1' and type='R5_Measured'");
                    for (int k = 0; k < drs.Length / 2; k++)
                    {
                        reference.ImportRow(drs[k]);
                    }







                    ///测试，residual坐标
                    foreach (string typeItem in new string[] { "R5_Measured", "R5_Residual", "G5_Measured", "G5_Residual", "Sm_Measured", "Sm_Residual" })
                    {
                        drs = newDt.Select("type='" + typeItem + "'");
                        for (int k = drs.Length / 2; k < drs.Length; k++)
                        {
                            measured.ImportRow(drs[k]);
                        }
                    }




                    tblName = "tbl_" + tool + "_measured";
                    measured.Columns.Remove("No");
                    myTabInfo = new DataTableToSQLte(measured, tblName);
                    myTabInfo.ImportToSqliteBatch(measured, @"P:\_SQLite\AsmlAwe.DB");



                    tblName = "tbl_reference";
                    reference.Columns.Remove("type");
                    reference.Columns.Remove("No");
                    reference.Columns.Remove("MarkNrY");
                    myTabInfo = new DataTableToSQLte(reference, tblName);
                    myTabInfo.ImportToSqliteBatch(reference, @"P:\_SQLite\AsmlAwe.DB");


                }


            }

            dt = null; drs = null; newDt = null; reference = null; measured = null;
            GC.Collect();









            /*

                drs = dt.Select("id>" + rowqty);
            //  MessageBox.Show("Rows To Be Inserted: " + drs.Length.ToString());
            DataTable newDt = dt.Clone();
            if (drs.Length > 0)
            {
                foreach (var item in drs)
                {
                    newDt.ImportRow(item);
                }

                dt = newDt.Copy();


                /// 按No索引，列出基准坐标，保存到reference table，其它测试结果，保存到measured table
                /// 单一lot（NO/ID）的数据结构是：类型按R5_Measured,G5_Measured,Sm Measured 排序
                /// 每一类型的前半部分是基准坐标，后半部分是测试坐标

                DataTable idSet = dt.DefaultView.ToTable(true, "id"); //https://blog.csdn.net/sinat_40692412/article/details/85781736
                DataTable reference = dt.Clone();
                DataTable measured = dt.Clone();





                foreach (DataRow id in idSet.Rows)
                {
                    ///基准坐标
                    drs = dt.Select("id=" + id["id"] + " and WaferNr='1' and type='R5_Measured'");
                    for (int i = drs.Length / 2; i < drs.Length; i++)
                    {
                        reference.ImportRow(drs[i]);
                    }
                    ///测试，residual坐标
                    foreach (string typeItem in new string[] { "R5_Measured", "R5_Residual", "G5_Measured", "G5_Residual", "Sm_Measured", "Sm_Residual" })
                    {
                        drs = dt.Select("id=" + id["id"] + " and type='" + typeItem + "'");
                        for (int i = drs.Length / 2; i < drs.Length; i++)
                        {
                            measured.ImportRow(drs[i]);
                        }
                    }


                }


                //  MessageBox.Show(reference.Rows.Count.ToString());
                //  MessageBox.Show(measured.Rows.Count.ToString());

                reference.Columns.Remove("type");


                ///复制到数据库
                tblName = "tbl_" + tool + "_measured";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(measured, tblName);
                myTabInfo.ImportToSqliteBatch(measured, @"P:\_SQLite\AsmlAwe.DB");
                tblName = "tbl_reference";
                myTabInfo = new DataTableToSQLte(reference, tblName);
                myTabInfo.ImportToSqliteBatch(reference, @"P:\_SQLite\AsmlAwe.DB");

            }
            else
            {
                // MessageBox.Show("No New Data of  Asml Parameter" + "," + tool);
            }
            */



        }


        #endregion

        #region //Awe2
        public static DataTable VectorIndex1(long sDate, long eDate, string connStr, string part, string layer)
        {
            
            string sql; DataTable dt = null;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  TOOL,PART,LAYER,LOT,ID,DATE FROM TBL_INDEX WHERE " +
                    " PART LIKE '%" + part + "%'" +
                    " AND LAYER LIKE '%" + layer + "%' " +
                    " AND DATE BETWEEN " + sDate + " AND " + eDate + " ORDER BY PART,LAYER,LOT,TOOL,DATE";

                MessageBox.Show(sql);

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
        public static DataTable VectorIndex2(string connStr, string lot)
        {
           
            string sql; DataTable dt = null;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT  TOOL,PART,LAYER,LOT,ID,DATE FROM TBL_INDEX WHERE " +
                    " LOT  like  '%" + lot + "%'  ORDER BY PART,LAYER,LOT,TOOL,DATE";

                MessageBox.Show(sql);

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

        public static List<DataTable> VectorData(string connStr, string str1, long str)
        {
            // https://blog.csdn.net/hanyingzhong/article/details/48653177
            //  所以建议一般情况下不要在字符串列建立索引，如果非要使用字符串索引，可以采用以下两种方法：
            //https://blog.csdn.net/hanyingzhong/article/details/48653177

            string sql; DataTable dt, dt1, dt2;



            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string sqlx, sqly, sqlz, sqlzx, sqlzy;
                sqlx = " SELECT * FROM TBL_" + str1 + "_WQMCCDELTA WHERE XY='X' AND ID=" + str;
                sqly = " SELECT * FROM TBL_" + str1 + "_WQMCCDELTA WHERE XY='Y' AND ID=" + str;
                sqlz = " SELECT a.id,a.WaferNr,a.MarkNrX,a.MarkNrY,a.type,b.X as X0,b.Y as Y0,a.X as X,a.Y as Y FROM " +
                    " TBL_" + str1 + "_MEASURED a,TBL_REFERENCE b WHERE " +
                    " a.MarkNrX=b.MarkNrX AND  a.ID = b.ID AND  a.ID=" + str;



                sqlzx = " SELECT z1.id,z1.WaferNr,z1.X0,z1.Y0, " +
                       " x1.RedMCC1,x1.RedWQ1,x1.RedMCC5,x1.RedWQ5,x1.GreenMCC1,x1.GreenWQ1,x1.GreenMCC5,x1.GreenWQ5,x1.RedDelta,x1.GreenDelta " +
                    " FROM (" + sqlz + ") z1,(" + sqlx + ") x1 WHERE " +
                    " z1.MarkNrX=x1.MarkNr and z1.WaferNr=x1.WaferNr  and z1.TYPE='R5_Measured'";

                sqlzy = " SELECT z2.id,z2.WaferNr,z2.X0,z2.Y0, " +
                       " y1.RedMCC1,y1.RedWQ1,y1.RedMCC5,y1.RedWQ5,y1.GreenMCC1,y1.GreenWQ1,y1.GreenMCC5,y1.GreenWQ5,y1.RedDelta,y1.GreenDelta " +
                    " FROM (" + sqlz + ") z2,(" + sqly + ") y1 WHERE " +
                    "  z2.MarkNrY=y1.MarkNr and z2.WaferNr=y1.WaferNr and z2.TYPE='R5_Measured'";

                sql = sqlz + ";" + sqlzx + ";" + sqlzy;



                // sql = " SELECT a.id,a.WaferNr,a.type,b.X as x0,b.y as y0,a.X as x,a.Y as Y FROM TBL_" + str1 + "_MEASURED" + " a," +
                //     "TBL_REFERENCE b WHERE a.MarkNrX=b.MarkNrX AND a.ID=b.ID AND a.ID=" + str;

                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                            dt1 = ds.Tables[1];
                            dt2 = ds.Tables[2];
                            //tblSecond = ds.Tables[1];

                        }
                    }
                }

            }
            List<DataTable> list = new List<DataTable>();
            list.Add(dt); list.Add(dt1); list.Add(dt2);
            return list;
        }

        #endregion
        #region //WQ,MCC,DELTA
        public static DataTable SummaryWqMccDelta(string tech, string part, string layer)
        {
            string sql;
            DataTable dt = null;
            DataTable dt1 = null;
            DataTable output = null;
           
            string connStr, connStr1;
            string parts;
            StringBuilder sb = new StringBuilder();
            connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB";
            connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
            #region //Get Index No
            if (tech.Length == 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = " SELECT * FROM TBL_INDEX WHERE PART LIKE '%" + part + "%' AND LAYER='" + layer + "' ORDER BY TOOL";
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
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr1))
                {
                    sql = " SELECT DISTINCT PART,FULLTECH ,TECH FROM TBL_CDCONFIG WHERE FULLTECH LIKE '%" + tech + "%'";
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
                sb.Append("(");
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("'" + row["part"] + "',");
                }
                parts = sb.ToString().Substring(0, sb.ToString().Length - 1);
                parts += ")";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = " SELECT * FROM TBL_INDEX WHERE PART IN "+ parts+ " AND LAYER='" + layer + "' ORDER BY TOOL";
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
            #endregion
            #region //extract data
            string tool, no;
            for (int i=0;i<dt.Rows.Count;i++)
            {
                tool = dt.Rows[i]["tool"].ToString();
                no = dt.Rows[i]["id"].ToString();

                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    sql = " SELECT * FROM TBL_" + tool + "_WQMCCDELTA WHERE ID=" + no;
                   conn.Open();
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                dt1 = ds.Tables[0]; 
                            }
                        }
                    }

                }
                if (output == null) { output = dt1.Clone(); }
                foreach (DataRow row in dt1.Rows)
                { output.ImportRow(row); }
            }
            #endregion

            dt1 = JoinTbl.JoinTwoTable(output, dt, "id", "id");

            return dt1;
        }
        #endregion


        //============================================================================
        #region Error Log


        static readonly string[] tools = new string[] { "7D", "08", "8A", "8B", "8C", "82", "83", "85", "86", "87", "89" };
        static readonly string gzPath = "\\\\10.4.50.16\\photo$\\ppcs\\_AsmlDownload\\AsmlErrLog\\";



        public static void readLog(string riqi, ref DataTable dtShow)
        {
            List<string[]> maskDelay = new List<string[]>();
            string folder;
            string[] files;
            List<string> logfiles = new List<string>();
            foreach (string tool in tools)
            {
                logfiles.Clear();
                folder = gzPath + tool + "\\";
                files = Directory.GetFiles(folder);
                //get file name
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Name.Contains("gz") && int.Parse(fileInfo.Name.Substring(8, 6)) > int.Parse(riqi))
                    {
                        logfiles.Add(fileInfo.FullName);
                    }
                }
                //read file
                logfiles.Sort();
                if (logfiles.Count() > 0)
                {
                    singToolErrlog(riqi, tool, logfiles, maskDelay);
                }
            }

            // return datadable
             
            dtShow.Columns.Add("tool", typeof(string));
            dtShow.Columns.Add("reportGeneratedTime", typeof(string));
            dtShow.Columns.Add("startedWithNewReticle", typeof(string));
            dtShow.Columns.Add("timeSpanMinutes", typeof(double));
            foreach (string[] item in maskDelay)
            {
                DataRow newRow = dtShow.NewRow();
                newRow[0] = item[0];
                newRow[1] = item[1];
                newRow[2] = item[2];
                newRow[3] = item[3];
                dtShow.Rows.Add(newRow);
            }
            maskDelay = null;




        }
        static void singToolErrlog(string riqi, string tool, List<string> files, List<string[]> maskDelay)
        {
            //combin log
            string name = "C:\\temp\\asmlTempErrLog";
            List<string> err = new List<string>();
            foreach (string file in files)
            {
                LibF.ungzip(file, name, true);
                err.AddRange(File.ReadAllLines(name));
            }
            err.AddRange(File.ReadAllLines(gzPath + tool + "\\ER_event_log.old"));
            err.AddRange(File.ReadAllLines(gzPath + tool + "\\ER_event_log"));
            err.RemoveAll(j => j.Length == 0);
            //extract data
            riqi = DateTime.Now.ToString().Substring(0, 2) + riqi;
            int s = int.Parse(DateTime.ParseExact(riqi, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).AddDays(+1).ToString("yyyyMMdd"));
            //若昨天数据未下载
            //  int e = int.Parse(DateTime.Now.AddDays(-2).ToString("yyyyMMdd"));
            //若昨天数据已下载
            int e = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));


            //记录起始点
            int indexS = 0, indexE = 0;
            for (int i = 0; i < err.Count(); ++i)
            {

                if (err[i].Length > 10 &&
                    Regex.IsMatch(err[i].Replace("/", "").Substring(0, 8), @"^\d{8}$") &&
                    int.Parse(DateTime.ParseExact(err[i].Replace("/", "").Substring(0, 8)
                        , "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyyMMdd")) == s
                    )
                {
                    indexS = i; break;
                }
            }
            for (int i = err.Count() - 1; i > 0; --i)
            {
                if (err[i].Length > 10 &&
                    Regex.IsMatch(err[i].Replace("/", "").Substring(0, 8), @"^\d{8}$") &&
                   int.Parse(DateTime.ParseExact(err[i].Replace("/", "").Substring(0, 8)
                       , "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyyMMdd")) == e
                   )
                {
                    indexE = i; break;
                }
            }
            //读取
            string[] arr = new string[4];
            string str;
            try
            {
                for (int i = indexS; i < indexE + 1; ++i)//正常应该是indexE+1，但后续不带日期的，还有部分行，暂时+3
                {
                    str = err[i];
                    if (str.EndsWith("Report Generated"))
                    {
                        Array.Clear(arr, 0, 4);
                        arr[1] = err[i - 2].Substring(0, 20);
                        continue;
                    }
                    if (str.EndsWith("Started with new reticle"))
                    {
                        if (arr[1] != null && arr[1].Length > 0)
                        {
                            arr[2] = err[i - 2].Substring(0, 20);
                            arr[0] = tool;


                            arr[3] = DateTime.ParseExact(arr[2].Replace("/", "")
                            , "MMddyyyy  HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture).Subtract(DateTime.ParseExact(arr[1].Replace("/", "")
                           , "MMddyyyy  HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture)).TotalMinutes.ToString();



                            maskDelay.Add(new string[] { arr[0], arr[1], arr[2], arr[3] });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\n\n请确认昨天的Error Log已下载");
            }
            
        }

        #endregion
    }
}
