using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.OleDb;
using System.IO;

using System.Drawing.Printing;

using System.Collections;

using System.Diagnostics;
using Microsoft.VisualBasic;

namespace exposureRecipeNet3
{
    public class check
    {
        static string connStr = "data source=" + @"p:\_SQLite\Flow.DB";
        public string part = string.Empty;
        public bool largeField;
        public Dictionary<int, string> myDic;
        public string preAlign;

        public List<string[]> imageSize = new List<string[]>();
        public bool blueAlign = true;



        public double stepX, stepY;
        public double shiftX, shiftY;
        public DataTable dt1 = new DataTable();//mask,NA,etc
        public DataTable dt11;
        public DataTable dt2 = new DataTable(); //coordinate
        public DataTable dt22;
        public bool flagSize = false;
        public bool flagRema = true;  //大视场打标处imageoffset，及正常imagesize
        public bool flagRevisedFlow = false;

        public bool readFlag = false;

        //MLM
        int mlm;double x1, x2, y1, y2;
        public bool flagLocation = true;//复合版象限offset
        public bool flagLocation1 = true;//复合版象限选择

        public check(string _part, bool _largeField,bool _flagRevisedFlow)
        {
            part = _part;
            largeField = _largeField;
            flagRevisedFlow = _flagRevisedFlow;
            if (readFile())
            {
                readFlag = true;
                readImage();
                readBlueAlign();
            }
            else
            {
                readFlag = false;
            }

        }
        public check(string _part, bool _largeField,int _mlm,double _x1,double _y1,double _x2,double _y2,bool _flagRevisedFlow)
        {
            part = _part;
            largeField = _largeField;
            mlm = _mlm;x1 = _x1;y1 = _y1; x2 = _x2;y2 = _y2;
            flagRevisedFlow = _flagRevisedFlow;
            if (readFile())
            {
                readFlag = true;
                readImage();
                readBlueAlign();
            }
            else
            {
                readFlag = false;
            }

        }




        private bool readFile()   //从旧文件转移过来，无变化
        {
            string filePath = @"P:\RECIPE\RECIPE\" + part;
            string[] allStr;
            try
            {
                myDic = new Dictionary<int, string>();
              
                allStr = File.ReadAllLines(filePath);
                int n = 1;

              foreach (var item in allStr)
                {
                    if (item.Trim().Length > 0) { myDic.Add(n, item); ++n; }



                }

                //   using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                //    {
                //       // 从文件读取并显示行，直到文件的末尾 
                //       string line;
                //       int n = 1;
                //       while ((line = sr.ReadLine()) != null)
                //       {
                //           if (line.Length > 0)
                //           {
                //               if (n > 970 && n<990) { MessageBox.Show(line); }
                //               myDic.Add(n, line);
                //                ++n;
                //           }

                //     }

                // }





            }
            catch (Exception ex)
            { 
                MessageBox.Show("Erro Code:\n\n"+ex.Message+"\n\n\n曝光文本文件读取错误，退出"); return false; 
            }
         

           // 
     


            string str;
            bool flagZb = false;
            bool flagType = false;
            bool flagUsage = false;
            bool flagLayer = false;
            bool flagMask = false;
            bool flagIll1 = false;
            bool flagIll2 = false;

            string[] strArr;

            foreach (KeyValuePair<int, string> kvp in myDic)
            {
                // MessageBox.Show( kvp.Key+","+ kvp.Value);
                str = kvp.Value;
                if (str.Contains("Prealignment Method"))
                {
                    preAlign = str.Split(':')[1].Trim();
                    // MessageBox.Show(preAlign);
                }
                if (kvp.Value.Contains("Cell Size [mm]"))
                {
                    stepX = Convert.ToDouble(str.Split(':')[1].Trim().Replace("Y", ""));
                    stepY = Convert.ToDouble(str.Split(':')[2].Trim());
                    // MessageBox.Show(stepX.ToString() + "," + stepY.ToString());
                }
                if (kvp.Value.Contains("Matrix Shift [mm]  X "))
                {
                    shiftX = Convert.ToDouble(str.Split(':')[1].Trim().Replace("Y", ""));
                    shiftY = Convert.ToDouble(str.Split(':')[2].Trim());
                    // MessageBox.Show(shiftX.ToString() + "," + shiftY.ToString());
                }

                //==============================================================================
                #region ===========================

                #region //坐标
                if (str == "+=================+=====+=====+==========+==========+============+============+")
                {
                    flagZb = true;
                    dt1 = null; dt1 = new DataTable();
                    dt1.Columns.Add("markid", Type.GetType("System.String"));
                    dt1.Columns.Add("layer", Type.GetType("System.String"));
                    dt1.Columns.Add("x", Type.GetType("System.Double"));
                    dt1.Columns.Add("y", Type.GetType("System.Double"));
                    dt1.Columns.Add("type", Type.GetType("System.String"));
                    dt1.Columns.Add("strategy", Type.GetType("System.String"));
                    dt1.Columns.Add("ppid", Type.GetType("System.String"));

                }
                if (str != "+=================+=====+=====+==========+==========+============+============+"
                    && str != "+-----------------+-----+-----+----------+----------+------------+------------+"
                    && flagZb == true)
                {
                    strArr = str.Split(new char[] { '|' });
                    DataRow newRow = dt1.NewRow();
                    newRow["markid"] = strArr[1].Trim();
                    newRow["layer"] = strArr[1].Split(new char[] { '_' })[0].Substring(strArr[1].Split(new char[] { '_' })[0].Length - 2, 2).Trim();
                    ///stack mark 有问题；待解决
                    ///读标记时，应该列出实际读入的层次名，主要是针对WT/TT/CP及stack mark
                    newRow["x"] = strArr[4].Trim();
                    newRow["y"] = strArr[5].Trim();
                    dt1.Rows.Add(newRow);
                }
                if (str == "+-----------------+-----+-----+----------+----------+------------+------------+")
                {
                    flagZb = false;

                }
                #endregion

                #region //mark type
                if (str == "+=================+===========+=================+=====+=======+======+======+")
                {
                    flagType = true;
                }
                if (str != "+=================+===========+=================+=====+=======+======+======+"
                    && str != "+-----------------+-----------+-----------------+-----+-------+------+------+"
                    && flagType == true)
                {
                    strArr = str.Split(new char[] { '|' });
                    // MessageBox.Show(str);


                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["markid"].ToString().Trim() == strArr[1].Trim())
                        {
                            dt1.Rows[i]["type"] = strArr[3].Trim();
                        }
                    }

                }
                if (str == "+-----------------+-----------+-----------------+-----+-------+------+------+")
                {
                    flagType = false;
                    //  dataGridView1.DataSource = dt1;
                }
                #endregion

                #region //mark usage
                if (str == "+==================+==================+==============+===============+")
                {
                    flagUsage = true;
                }
                if (str != "+==================+==================+==============+===============+"
                    && str != "+------------------+------------------+--------------+---------------+"
                    && flagUsage == true)
                {
                    strArr = str.Split(new char[] { '|' });

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["markid"].ToString().Trim() == strArr[2].Trim())
                        {
                            dt1.Rows[i]["strategy"] = strArr[1].Trim();
                        }
                    }

                }
                if (str == "+------------------+------------------+--------------+---------------+")
                {
                    flagUsage = false;
                    //去除重复标记
                    //  for(int i=0;i<dt1.Rows.Count;i++)
                    //  { dt1.Rows[i]["markid"]=(dt1.Rows[i]["markid"].ToString()).Split(new char[] { '_' })[0];}
                    //   DataView dv = new DataView(dt1);
                    //   dt2 = dv.ToTable("Dist", true, "markid", "layer", "x", "y", "type", "strategy", "ppid");
                    //    dataGridView1.DataSource = dt1;

                }
                #endregion

                #region // layer strategy
                if (str == "+=================+==================+=============+")
                {
                    flagLayer = true;
                    dt2 = dt1.Clone();

                }
                if (str != "+=================+==================+=============+"
                    && str != "+-----------------+------------------+-------------+"
                    && flagLayer == true)
                {
                    strArr = str.Split(new char[] { '|' });

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["strategy"].ToString().Trim() == strArr[2].Trim())
                        {
                            DataRow newRow = dt2.NewRow();
                            newRow["ppid"] = strArr[1].Trim();
                            newRow["markid"] = dt1.Rows[i]["markid"].ToString().Trim().Split(new char[] { '_' })[0];
                            newRow["layer"] = dt1.Rows[i]["layer"].ToString().Trim();
                            newRow["x"] = dt1.Rows[i]["x"].ToString().Trim();
                            newRow["y"] = dt1.Rows[i]["y"].ToString().Trim();
                            newRow["type"] = dt1.Rows[i]["type"].ToString().Trim();
                            newRow["strategy"] = dt1.Rows[i]["strategy"].ToString().Trim();
                            dt2.Rows.Add(newRow);


                        }
                    }

                }
                if (str == "+-----------------+------------------+-------------+")
                {

                    flagLayer = false;


                }
                #endregion

                #region //mask
                if (str == "+=================+=================+==========================+==============+")
                {
                    flagMask = true; dt1 = new DataTable();
                    dt1.Columns.Add("layer");
                    dt1.Columns.Add("image");
                    dt1.Columns.Add("mask");
                }

                if (str != "+=================+=================+==========================+==============+"
                    && str != "+-----------------+-----------------+--------------------------+--------------+"
                    && !str.Contains("SPM")
                    && flagMask == true)
                {
                    strArr = str.Split(new char[] { '|' });


                    DataRow newRow = dt1.NewRow();
                    newRow["layer"] = strArr[1].Trim();
                    newRow["image"] = strArr[2].Trim();
                    newRow["mask"] = strArr[3].Trim();

                    dt1.Rows.Add(newRow);




                }
                if (str == "+-----------------+-----------------+--------------------------+--------------+")
                {

                    flagMask = false;
                    //dataGridView1.DataSource = dt1;

                }




                #endregion

                #region // annualar conventional
                if (str == "+=================+=================+==============+=================+")
                {
                    flagIll1 = true;
                    dt1.Columns.Add("mode");


                }

                if (str != "+=================+=================+==============+=================+"
                    && str != "+-----------------+-----------------+--------------+-----------------+"
                    && !str.Contains("SPM")
                    && flagIll1 == true)
                {
                    strArr = str.Split(new char[] { '|' });


                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i]["layer"].ToString().Trim() == strArr[1].Trim())
                        {
                            dt1.Rows[i]["mode"] = strArr[3];

                        }
                    }




                }
                if (str == "+-----------------+-----------------+--------------+-----------------+")
                {

                    flagIll1 = false;
                    // dataGridView1.DataSource = dt1;

                }

                #endregion


                #region //ill setting


                if (str == "+=================+=================+=========+========+========+")
                {
                    flagIll2 = true;
                    dt1.Columns.Add("aperture");
                    dt1.Columns.Add("outer");
                    dt1.Columns.Add("inner");

                }

                if (str != "+=================+=================+=========+========+========+"
                    && str != "+-----------------+-----------------+---------+--------+--------+"
                    && !str.Contains("SPM")
                    && flagIll2 == true)
                {
                    strArr = str.Split(new char[] { '|' });
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {


                        if (dt1.Rows[i]["layer"].ToString().Trim() == strArr[1].Trim() && dt1.Rows[i]["image"].ToString().Trim() == strArr[2].Trim())
                        {
                            dt1.Rows[i]["aperture"] = strArr[3].Trim();
                            dt1.Rows[i]["outer"] = strArr[4].Trim();
                            dt1.Rows[i]["inner"] = strArr[5].Trim();
                        }
                    }




                }
                if (str == "+-----------------+-----------------+---------+--------+--------+")
                {

                    flagIll2 = false;
                    // dataGridView1.DataSource = dt1;

                }




                #endregion


                #endregion
                //=============================================================================================


            }


            return true;




        }
        private void readImage()
        {
            string str; string[] strArr; bool f = false, f1 = false;
            foreach (KeyValuePair<int, string> kvp in myDic)
            {
                str = kvp.Value; str = str.Trim();
                if (str.Contains("Reticle Image (4x Lens Reduction)"))
                { 
                    f1 = true;
                }
                if (str == "+=================+===========+===========+===========+===========+" && f1) 
                { 
                    f = true; 
                }

                if (f1 && f && str.Contains("|"))
                {
                    imageSize.Add(str.Split('|'));

                }
                if (str == "+-----------------+-----------+-----------+-----------+-----------+" && f1 && f) { return; }

            }
        }
        private void readBlueAlign()
        {
            string str; string[] strArr; bool f = false;
            foreach (KeyValuePair<int, string> kvp in myDic)
            {
                str = kvp.Value; str = str.Trim();
                if (str == "+=================+========================+") { f = true; }


                if (f && str.Contains("|") && str.Split('|')[2].Trim().Length > 2 && str.Split('|')[2].Trim() != "RBA")
                { blueAlign = false; return; }
                if (f && str == "+-----------------+------------------------+") { return; }

            }
        }

        public bool compareNaSigma()
        {
            #region //check NA/Sigma
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT ppid,illumination,MASK FROM TBL_BIASTABLE WHERE EQPTYPE='LDI' AND PART='" + part + "'";
                dt11 = dt1.Copy();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    string[] strArr;
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            DataTable tblBt = ds.Tables[0];
                            if (tblBt.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到BIAS TABLE数据，退出");
                                return false;

                            }
                            else
                            {
                                dt11.Columns.Add("mask1");
                                dt11.Columns.Add("aperture1");
                                dt11.Columns.Add("outer1");
                                dt11.Columns.Add("inner1");
                                dt11.Columns.Add("illflag");
                                dt11.Columns.Add("maskflag");
                                dt11.Columns.Add("part");
                                dt11.Columns.Add("riqi");
                                for (int i = 0; i < dt11.Rows.Count; i++)
                                {
                                    for (int j = 0; j < tblBt.Rows.Count; j++)
                                    {
                                        if (dt11.Rows[i]["layer"].ToString().Trim() == (tblBt.Rows[j]["ppid"]).ToString().Trim())
                                        {
                                            sql = tblBt.Rows[j]["illumination"].ToString();
                                            try
                                            {
                                                strArr = sql.Split(new char[] { ',' });
                                                if (strArr.Length > 3)
                                                {
                                                    dt11.Rows[i]["aperture1"] = strArr[1];
                                                    dt11.Rows[i]["outer1"] = strArr[2];
                                                    dt11.Rows[i]["inner1"] = strArr[3];



                                                    dt11.Rows[i]["illflag"] = System.Math.Abs((Convert.ToDouble(dt11.Rows[i]["aperture1"].ToString()) - Convert.ToDouble(dt11.Rows[i]["aperture"].ToString()))) < 0.00001
                                                         && System.Math.Abs((Convert.ToDouble(dt11.Rows[i]["outer1"].ToString()) - Convert.ToDouble(dt11.Rows[i]["outer"].ToString()))) < 0.00001
                                                         && System.Math.Abs((Convert.ToDouble(dt11.Rows[i]["inner1"].ToString()) - Convert.ToDouble(dt11.Rows[i]["inner"].ToString()))) < 0.00001
                                                         && dt11.Rows[i]["mode"].ToString().ToUpper().Substring(1, 3) == strArr[0].Trim().ToUpper();
                                                }
                                                else
                                                {
                                                    dt11.Rows[i]["illflag"] = false;
                                                    MessageBox.Show("请确认BiasTable中的Illumination数据是否正确");
                                                }
                                                    dt11.Rows[i]["mask1"] = tblBt.Rows[j]["Mask"];
                                                    dt11.Rows[i]["maskFlag"] = (dt11.Rows[i]["mask"].ToString().Trim() == dt11.Rows[i]["mask1"].ToString().Trim());
                                                dt11.Rows[i]["part"] = part;
                                                dt11.Rows[i]["riqi"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                            }
                                            catch(Exception ex)
                                            {
                                                MessageBox.Show("Error Code:" + ex.Message);
                                                MessageBox.Show("请通知修改程序");
                                           

                                            }
                                        }
                                    }
                                }

                            }



                        }
                    }
                }
            }
            return true;
            #endregion
        }
        public bool compareCoordinates()
        {
            #region // check coordinate
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT ppid,alignto layer,x1/1000.0 x,y1/1000.0 y,typex type  FROM TBL_COORDINATE WHERE  EQPTYPE='LDI' AND PART='" + part + "'";
                sql += "UNION SELECT ppid,alignto layer,x2/1000.0 x,y2/1000.0 y,typey type  FROM TBL_COORDINATE WHERE  EQPTYPE='LDI' AND PART='" + part + "'";
                sql = "SELECT * FROM (" + sql + ") a WHERE a.type <>'#' and a.type <>'NA'";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            DataTable tblBt = ds.Tables[0];
                            if (tblBt.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到基准坐标数据,退出");
                                return false;

                            }
                            else
                            {
                                dt22 = dt2.Copy();



                                dt22.Columns.Add("x1");
                                dt22.Columns.Add("y1");
                                dt22.Columns.Add("type1");
                                dt22.Columns.Add("flag");
                                dt22.Columns.Add("part");
                                for (int i = 0; i < dt22.Rows.Count; i++)
                                {
                                    for (int j = 0; j < tblBt.Rows.Count; j++)
                                    {
                                        if (
                                            ///stack mark只取前缀前两位对比
                                            dt22.Rows[i]["layer"].ToString().Trim().Substring(0, 2) == tblBt.Rows[j]["layer"].ToString().Trim().Substring(0, 2) &&
                                            dt22.Rows[i]["ppid"].ToString().Trim() == tblBt.Rows[j]["ppid"].ToString().Trim()
                                            && (
                                                   (dt22.Rows[i]["type"].ToString().Trim().Contains("X") && tblBt.Rows[j]["type"].ToString().Trim().Contains("X"))
                                                    || (dt22.Rows[i]["type"].ToString().Trim().Contains("Y") && tblBt.Rows[j]["type"].ToString().Trim().Contains("Y"))
                                               )
                                            && (
                                                      (dt22.Rows[i]["type"].ToString().Trim().Contains("AH53") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AH53"))
                                                   || (dt22.Rows[i]["type"].ToString().Trim().Contains("AH53") && tblBt.Rows[j]["type"].ToString().Trim().Contains("SPM53"))
                                                   || (dt22.Rows[i]["type"].ToString().Trim().Contains("AH325374") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AH325374"))
                                                   || (dt22.Rows[i]["type"].ToString().Trim().Contains("AH325374") && tblBt.Rows[j]["type"].ToString().Trim().Contains("AA157") && dt22.Rows[i]["strategy"].ToString().Trim().Contains("AA157"))
                                                )
                                            )
                                        {

                                            dt22.Rows[i]["x1"] = tblBt.Rows[j]["x"].ToString().Trim();
                                            dt22.Rows[i]["y1"] = tblBt.Rows[j]["y"].ToString().Trim();
                                            dt22.Rows[i]["type1"] = tblBt.Rows[j]["type"].ToString().Trim();
                                            dt22.Rows[i]["part"] = part;
                                            try
                                            {
                                                dt22.Rows[i]["flag"] = System.Math.Abs((Convert.ToDouble(dt22.Rows[i]["x"].ToString()) - Convert.ToDouble(dt22.Rows[i]["x1"].ToString()))) < 0.00001
                                                    && System.Math.Abs((Convert.ToDouble(dt22.Rows[i]["y"].ToString()) - Convert.ToDouble(dt22.Rows[i]["y1"].ToString()))) < 0.000001;
                                            }
                                            catch
                                            {
                                                //无数据
                                            }

                                        }
                                    }
                                }

                            }



                        }
                    }
                }
            }

            dt22 = dt22.DefaultView.ToTable(true, new string[] { "layer", "markid", "x", "y", "type", "strategy", "ppid", "x1", "y1", "type1", "flag" });
            return true;
            #endregion
        }

        public void saveDt11()
        {

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " DELETE  FROM TBL_CHECK1 WHERE PART='" + part + "'";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }


            DataTableToSQLite myTabInfo = new DataTableToSQLite(dt11, "tbl_check1");
            myTabInfo.ImportToSqliteBatch(dt11, @"p:\_SQLite\Flow.DB");

        }
        public void saveDt22()
        {

            if (flagRevisedFlow == false) //改版流程无dt22
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string sql = " DELETE  FROM TBL_CHECK2 WHERE PART='" + part + "'";
                    using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                    {
                        com.ExecuteNonQuery();
                    }
                }


                try
                {
                    dt22.Columns.Add("part");
                    foreach (DataRow row in dt22.Rows)
                    { row["part"] = part; }
                }
                catch
                { }


                DataTableToSQLite myTabInfo = new DataTableToSQLite(dt22, "tbl_check2");
                myTabInfo.ImportToSqliteBatch(dt22, @"p:\_SQLite\Flow.DB");
            }

        }
        public void compareSize()
        {
            DataTable tblTmp;
            double rx = 0, rx1 = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT * FROM TBL_MAP WHERE PART LIKE '" + part + "%' and NikonFlag is false";
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
                                MessageBox.Show("未查询到数据，请重新输入查询条件");
                                return;

                            }

                        }
                    }
                }
            }


            double sx = Convert.ToDouble(tblTmp.Rows[0][1].ToString());
            double sy = Convert.ToDouble(tblTmp.Rows[0][2].ToString());
            double ox = Convert.ToDouble(tblTmp.Rows[0][5].ToString());
            double oy = Convert.ToDouble(tblTmp.Rows[0][6].ToString());


            if (largeField)
            {
                bool f1 = Math.Abs(stepX - sy) < 0.0001;
                bool f2 = Math.Abs(stepY / 2 - sx) < 0.0001;

                bool f3 = Math.Abs(shiftX - ox) < 0.0001;
                bool f4 = Math.Abs(shiftY - oy) < 0.0001;
                flagSize = f1 && f2 && f3 && f4;


                rx = System.Math.Round((100 - 5.5 - sy / 2 - ox) % sy, 5);
                rx1 = System.Math.Round(((100 - 5.5 - sy / 2 - ox) % sy - sy) / 2, 5);







            }
            else
            {
                bool f1 = Math.Abs(stepX - sx) < 0.0001;
                bool f2 = Math.Abs(stepY - sy) < 0.0001;
                bool f3 = Math.Abs(shiftX - ox) < 0.0001;
                bool f4 = Math.Abs(shiftY - oy) < 0.0001;
                flagSize = f1 && f2 && f3 && f4;

            }


            //image for lasermark image
            //not available for MPW
            if (largeField)
            {
                foreach (string hole in new string[] { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "W8", "WT" })
                {
                    DataRow[] drs = dt11.Select("layer='" + hole + "'");


                    if (drs.Length == 1)
                    {
                        flagRema = false; return;
                    }
                    else if (drs.Length == 2)
                    {
                        if ( drs[0]["image"].ToString().ToUpper() == "FULL-K" &&  drs[1]["image"].ToString().ToUpper() == "FULL-KB"    )
                        {
                            flagRema = true;
                        }
                        else
                        {
                            flagRema = false; return;
                        }

                    }
                }
            }
            //image size
            foreach (string[] item in imageSize)
            {

                if (largeField && item[1].Trim().Contains("FULL"))
                {

                    if (item[1].Trim() != "FULL-KB")
                    {
                        bool f1 = Math.Abs((Convert.ToDouble(item[2]) - 4 * sy)) < 0.001;
                        bool f2 = Math.Abs((Convert.ToDouble(item[3]) - 4 * 2 * sx)) < 0.001;
                        bool f3 = Math.Abs(Convert.ToDouble(item[4])) < 0.001;
                        bool f4 = Math.Abs(Convert.ToDouble(item[5])) < 0.001;
                        flagRema = f1 && f2 && f3 && f4;
                        if (flagRema == false) { return; }
                    }
                    else
                    {
                        bool f1 = Math.Abs((Convert.ToDouble(item[2]) - 4 * rx)) < 0.001;
                        bool f2 = Math.Abs((Convert.ToDouble(item[3]) - 4 * 2 * sx)) < 0.001;
                        bool f3 = Math.Abs(Convert.ToDouble(item[4]) - 4 * rx1) < 0.001;
                        bool f4 = Math.Abs(Convert.ToDouble(item[5])) < 0.001;
                        flagRema = f1 && f2 && f3 && f4;
                        if (flagRema == false) { return; }
                    }

                }
                else if ((!largeField) && item[1].Trim().Contains("FULL"))
                {
                    bool f1 = Math.Abs((Convert.ToDouble(item[2]) - 4 * sx)) < 0.001;
                    bool f2 = Math.Abs((Convert.ToDouble(item[3]) - 4 * sy)) < 0.001;
                    flagRema = f1 && f2;
                    bool f3 = true, f4 = true;//复合版各象限单独计算
                    if (mlm==0)
                    {
                         f3 = Math.Abs(Convert.ToDouble(item[4])) < 0.001;
                        f4 = Math.Abs(Convert.ToDouble(item[5])) < 0.001;
                    }
                    flagRema = f1 && f2 && f3 && f4;
                        if (flagRema == false) { return; }

                }


            }



        }
        public void compareLocation() //确定象限offset是否正确
        {
            if (mlm != 0) {  }
            else { return; }
        
            double c1x = (x1 + x2) / 2/1000*4;
            double c1y = (y1 + y2) / 2/1000*4;
            double c2x, c2y, c3x, c3y, c4x, c4y;
            c2x = c2y = c3x = c3y = c4x = c4y = 0;
            if(mlm==2)
            {
                c2x = -c1x;c2y = -c1y;
            }
            else if (mlm==3)
            {
                c2x = 0;c2y = 0;c3x = -c1x;c3y = -c1y;
            }
            else
            {
                c2x = -c1x;c2y = c1y;
                c3x = -c1x;c3y = -c1y;
                c4x = c1x;c4y = -c1y;
            }
       
            foreach ( string[] item in imageSize)
            {
                string tmp = item[1].Trim();

                if(tmp.StartsWith("FULL-"))
               // if(tmp.Substring(0,5)=="FULL-")
                {
                    if (tmp.Substring(5,1)=="1")
                    {
                        bool f1 = Math.Abs(Convert.ToDouble(item[4]) - c1x) < 0.001;
                        bool f2 = Math.Abs(Convert.ToDouble(item[5]) - c1y) < 0.001;
                        if (f1 && f2 && flagLocation) { } else { flagLocation = false; return; }
                    }
                    else if (tmp.Substring(5, 1) == "2")
                    {
                        bool f1 = Math.Abs(Convert.ToDouble(item[4]) - c2x) < 0.001;
                        bool f2 = Math.Abs(Convert.ToDouble(item[5]) - c2y) < 0.001;
                        if (f1 && f2 && flagLocation) { } else { flagLocation = false; return; }
                    }
                    else if (tmp.Substring(5, 1) == "3")
                    {
                        bool f1 = Math.Abs(Convert.ToDouble(item[4]) - c3x) < 0.001;
                        bool f2 = Math.Abs(Convert.ToDouble(item[5]) - c3y) < 0.001;
                        if (f1 && f2&&flagLocation) { } else { flagLocation = false; return; }
                    }
                    else if(tmp.Substring(5, 1) == "4")
                    {
                        bool f1 = Math.Abs(Convert.ToDouble(item[4]) - c4x) < 0.001;
                        bool f2 = Math.Abs(Convert.ToDouble(item[5]) - c4y) < 0.001;
                        if (f1 && f2 && flagLocation) { } else { flagLocation = false; return; }
                    }
                    else
                    { }
                }
              
            }

            
        }  

        public void compareMLM()
        {
            if (mlm != 0) { }  else { return; }
            string str; string[] strArr; bool f = false;
            Dictionary<string, string> dictMLM = new Dictionary<string, string>();
            foreach (KeyValuePair<int, string> kvp in myDic)
            {
                str = kvp.Value.Trim();
                if (str== "+=================+=================+==========================+==============+") { f = true; }
                if (f && str[0]=='|' && str.Contains("FULL-") )
                {
                    strArr = str.Split('|');
                    string layer = strArr[1].Trim();
                    string mlm = strArr[2].Trim();
                    try
                    {
                        dictMLM.Add(layer, mlm.Substring(mlm.Length - 1, 1));
                     

                    }
                    catch (System.Exception exception)
                    {
                        MessageBox.Show("Eror Code" + exception.Message+
                            "\n\n复合版同一层有两种Image设置（非象限差异），请确认"+
                            "\n"+ layer+","+dictMLM[layer]+
                            "\n"+layer+","+ mlm.Substring(mlm.Length - 1, 1)+
                            "\n\n Exit!");
                        flagLocation1 = false;
                        return; 

                       
                    }


                    
                   
                }
                if (f && str== "+-----------------+-----------------+--------------------------+--------------+")
                { break; }
            }

            //比较象限
            DataTable tblTmp;
           
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = " SELECT ppid,MLM FROM TBL_BIASTABLE WHERE PART='" + part + "'"+
                    " AND eqptype='LDI'";

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
                                MessageBox.Show("未查询到数据,请确认产品名");
                                flagLocation1 = false;
                                return;
                            }
                        }
                    }
                }
            }

            //比较MLM
            foreach (DataRow row in tblTmp.Rows)
            {
                if (row[0].ToString()=="ZO" && row[1].ToString().Trim()=="")
                {
                    MessageBox.Show("零层且无象限定义，不对比");
                    continue;
                }

                try
                {
                    if ( dictMLM[row[0].ToString()] == row[1].ToString())
                    {
                        ;
                    }
                    else
                    {
                        MessageBox.Show("复合版程序中象限设置和BiasTable不匹配:\n" +
                              "  Recipe:   \n    " +
                              row[0].ToString() + "," + dictMLM[row[0].ToString()] + "\n" +
                              "  BiasTable:\n    " + row[0].ToString() + "," + row[1].ToString());
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Code:" + exception.Message+
                        "\n\n复合版BiasTable中ASML层次未编辑在曝光程序中(是否是共用零层版)，Exit");
                    flagLocation1 = false;
                    return;
                }
            }

        }



    }
}