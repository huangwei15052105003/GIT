using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace exposureRecipeNet3
{
    public class pubfunction
    {
        static string connStr = "data source=" + @"p:\_SQLite\Flow.DB";
        public static string GetNaSigma(string code, string layer, string track)
        {
            string sql;
            string setting = "";
            DataTable tblTmp;
            

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_ILLUMINATION WHERE TECH ='" + code.Substring(0, 3) + "' AND LAYER = '" + layer.Substring(0, 2) + "' AND TRACK = '" + track + "'";

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
        public static void ConverLargeFieldCoordinate(ref double sx,ref xlsData data)
        {
            double x1, x2, y1, y2;
            for (int i = 1; i < data.dt2.Rows.Count; i++) //i=0是第一层，无坐标转换
            {
                if (data.dt2.Rows[i]["eqptype"].ToString() == "LDI")
                {
                    x1 = Convert.ToDouble(data.dt2.Rows[i]["x1"]);
                    y1 = Convert.ToDouble(data.dt2.Rows[i]["y1"]);
                    x2 = Convert.ToDouble(data.dt2.Rows[i]["x2"]);
                    y2 = Convert.ToDouble(data.dt2.Rows[i]["y2"]);
                    data.dt2.Rows[i]["x1"] = y2;
                    data.dt2.Rows[i]["y1"] = -(x2 + sx);
                    data.dt2.Rows[i]["x2"] = y1;
                    data.dt2.Rows[i]["y2"] = -(x1 + sx);

                }

            }
        }
        public static void saveMergeTable( xlsData data)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {

                    conn.Open();
                    string sql = " DELETE  FROM TBL_BIASTABLE WHERE PART='" + data.part + "'";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }


                try
                {
                    data.dt.Columns.Add("riqi");
                    data.dt.Columns.Add("part");
                    for (int i = 0; i < data.dt.Rows.Count; i++)
                    { data.dt.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); data.dt.Rows[i]["part"] = data.part; }
                }
                catch
                {
                    //如果生成表格时，同步保存，riqi/part字段已有，后续手动保存出错
                }


                DataTableToSQLite myTabInfo = new DataTableToSQLite(data.dt, "tbl_biastable");
                myTabInfo.ImportToSqliteBatch(data.dt, @"p:\_SQLite\Flow.DB");
                MessageBox.Show("Saving Bias Table Done");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Code: " + exception.Message + "\n\n先运行脚本生成数据");
            }


        }
        public static void saveZb( xlsData data)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string sql = " DELETE  FROM TBL_COORDINATE WHERE PART='" + data.part + "'";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }

                try
                {
                    data.dt2.Columns.Add("riqi");
                    data.dt2.Columns.Add("part");
                    for (int i = 0; i < data.dt2.Rows.Count; i++)
                    { data.dt2.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); data.dt2.Rows[i]["part"] = data.part; }
                }
                catch
                {
                    //如果生成表格时，同步保存，riqi/part字段已有，后续手动保存出错
                }
                DataTableToSQLite myTabInfo1 = new DataTableToSQLite(data.dt2, "tbl_coordinate");
                myTabInfo1.ImportToSqliteBatch(data.dt2, @"p:\_SQLite\Flow.DB");
                MessageBox.Show("Saving Coordinate Done");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Code: " + exception.Message + "\n\n先运行脚本生成数据");
            }
        }
    }
}
    
