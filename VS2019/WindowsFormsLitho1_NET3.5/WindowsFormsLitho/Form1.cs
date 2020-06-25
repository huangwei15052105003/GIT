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


//datagridview 加筛选功能
//https://www.cnblogs.com/billowliu/articles/5587661.html
//https://blog.csdn.net/mouday/article/details/81049209
//https://blog.csdn.net/phoenix36999/article/details/45916943


namespace WindowsFormsLitho
{
    public partial class MainForm : Form
    {
        public delegate void MainDelegate(DataTable data);  //Nikon 预对位画图
        public event MainDelegate MainTrigger;

        public delegate void MainDelegate1(DataTable data1); // DataTable->datagridview
        public event MainDelegate1 MainTrigger1;

        public delegate void MainDelegate2(); // datagridview-->local file
        public event MainDelegate2 MainTrigger2;


     

        DataTable tblFirst, tblSecond;
        DataTable tblChart;
        private string sql = null;
        private string connStr = null;
        private string strFilter = null;
        public int flag = 0;//子窗体是否打开的标志
        

        public MainForm()
        {
            InitializeComponent();
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
           // this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //放正中，会影响弹出窗口

            //获取当前工作区宽度和高度（工作区不包含状态栏）
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //计算窗体显示的坐标值，可以根据需要微调几个像素
            int x = ScreenWidth - this.Width - 5;
            int y = ScreenHeight - this.Height - 5;
            this.Location = new Point(x, y);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            


            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.TopMost = true;

            label2.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            label1.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");

            MessageBox.Show("数据筛选日期定义为90天，请按需求更改；\n\n筛选界面不要拖到屏幕中央；");
        }

        //============NIKON PRE_ALIGNMENT
        //所有数据
        private void button1_Click(object sender, EventArgs e)
        {
            tblFirst = tblSecond = tblChart = null;
            string sDate = label1.Text; string eDate = label2.Text;
           
            openFileDialog1.InitialDirectory = @"c:\temp\";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择数据文件"); return; }
            #region
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                string sql1, sql2;
                sql1 = "(select No, tool,Date,ppid,Search,EGA from tbl_index where Date between '" + sDate + "' and '" + eDate + "')";
                sql2 = "(select No,max(rot) as maxrot ,min(rot) as minrot  ,(max(rot)-min(rot)) as rotrange,count(rot) as qty  from tbl_parameter group by No)";
                sql = " select a.*,b.maxrot,minrot,rotrange,qty from " + sql1 + " a ," + sql2 + " b where a.No=b.No;" +
                     " select ppid,count(case when tool='ALSIB1' then tool  end) ALSIB1,count(case when tool='ALSIB2' then tool  end) ALSIB2,count(case when tool='ALSIB3' then tool  end) ALSIB3,count(case when tool='ALSIB4' then tool  end) ALSIB4,count(case when tool='ALSIB5' then tool  end) ALSIB5,count(case when tool='ALSIB6' then tool  end) ALSIB6,count(case when tool='ALSIB7' then tool  end) ALSIB7,count(case when tool='ALSIB8' then tool  end) ALSIB8,count(case when tool='ALSIB9' then tool  end) ALSIB9,count(case when tool='ALSIBA' then tool  end) ALSIBA,count(case when tool='ALSIBB' then tool  end) ALSIBB,count(case when tool='ALSIBC' then tool  end) ALSIBC,count(case when tool='ALSIBD' then tool  end) ALSIBD,count(case when tool='ALSIBE' then tool  end) ALSIBE,count(case when tool='ALSIBF' then tool  end) ALSIBF,count(case when tool='ALSIBG' then tool  end) ALSIBG,count(case when tool='ALSIBH' then tool  end) ALSIBH,count(case when tool='ALSIBI' then tool  end) ALSIBI,count(case when tool='BLSIBK' then tool  end) BLSIBK,count(case when tool='BLSIBL' then tool  end) BLSIBL,count(case when tool='BLSIE1' then tool  end) BLSIE1,count(case when tool='BLSIE2' then tool  end) BLSIE2 from   (select a.*,b.maxrot,minrot,rotrange,qty from " + sql1 + " a ," + sql2 + " b where a.No=b.No) group by ppid";

                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblFirst = ds.Tables[0];
                            tblSecond = ds.Tables[1];
                        }
                    }
                }
            }
            #endregion

            this.checkedListBox1.Items.Clear();
            this.checkedListBox1.Items.Add("_ALL");
            for (int i = 0; i < tblSecond.Rows.Count; i++)
            {  this.checkedListBox1.Items.Add(tblSecond.Rows[i][0].ToString());  }

            this.checkedListBox2.Items.Clear();
            this.checkedListBox2.Items.Add("_ALL");
            for (int i = 1; i < tblSecond.Columns.Count; i++)
            {   this.checkedListBox2.Items.Add(tblSecond.Columns[i].ColumnName);     }

            int rowsCount= tblFirst.Rows.Count;

            MessageBox.Show("共计筛选到"+rowsCount.ToString()+"行数据");

        }
    /*
        //作图
        private void button6_Click(object sender, EventArgs e)
        {
            
            if (flag==0)
            {

              ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                MainTrigger(tblChart);
                chartForm.ChildTrigger += ShowMain;

               flag += 1;
            }
            else
            {
            
                MainTrigger(tblChart);
            }
        }
*/

        //=======ASML BATCH_REPORT
        private void button8_Click(object sender, EventArgs e)
        {
            tblFirst = tblSecond = tblChart = null;
            //openFileDialog1.InitialDirectory = @"\\10.4.72.74\asml\_SQLite\";
            openFileDialog1.InitialDirectory = @"c:\temp\";
            
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\");}
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select  distinct layer from tbl_asmlbatchreport  order by layer;select distinct substr(jobName,6,20) part from tbl_asmlbatchreport order by substr(jobName,6,20);select distinct tool from tbl_asmlbatchreport order by tool";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblFirst = ds.Tables[0];
                            tblSecond = ds.Tables[1];
                            tblChart = ds.Tables[2];
                        }
                    }
                }
            }
            this.checkedListBox3.Items.Add("ALL");
            foreach (DataRow dr in tblFirst.Rows)
            {
                if (dr[0].ToString().Length > 0)
                    this.checkedListBox3.Items.Add(dr[0].ToString());
            }

            this.checkedListBox4.Items.Add("ALL");
            foreach (DataRow dr in tblSecond.Rows)
            {
                if (dr[0].ToString().Length > 0)
                    this.checkedListBox4.Items.Add(dr[0].ToString());
            }
            this.checkedListBox5.Items.Add("ALL");
            foreach (DataRow dr in tblChart.Rows)
            {
                if (dr[0].ToString().Length > 0)
                    this.checkedListBox5.Items.Add(dr[0].ToString());
            }



            #region
            string[] paraArr ={"tool",

"time",
"lotid",
"jobName",
"jobModified",
"layer",
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
            #endregion

            foreach (var para in paraArr)
            {
                this.checkedListBox6.Items.Add(para);
            };

            paraArr = null; tblFirst= tblSecond = tblChart = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tblFirst = tblSecond = tblChart = null;
            #region
            //pick tool
            string tool = "";
            for (int i = 0; i < checkedListBox5.Items.Count; i++)
            {
                if (checkedListBox5.GetItemChecked(i))
                {
                    if (tool == "")
                    { tool = "'" + checkedListBox5.GetItemText(checkedListBox5.Items[i]) + "'"; }
                    else
                    { tool = tool + ",'" + checkedListBox5.GetItemText(checkedListBox5.Items[i] + "'"); }
                }
            }


            //pick part
            string part = "";
            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                if (checkedListBox4.GetItemChecked(i))
                {
                    if (part == "")
                    { part = "'" + checkedListBox4.GetItemText(checkedListBox4.Items[i]) + "'"; }
                    else
                    { part = part + ",'" + checkedListBox4.GetItemText(checkedListBox4.Items[i] + "'"); }
                }
            }


            //pick 
            string layer = "";
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    if (layer == "")
                    { layer = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    else
                    { layer = layer + ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i] + "'"); }
                }
            }


            //pick parameter
            string parameter = "";
            for (int i = 0; i < checkedListBox6.Items.Count; i++)
            {
                if (checkedListBox6.GetItemChecked(i))
                {
                    if (parameter == "")
                    { parameter = checkedListBox6.GetItemText(checkedListBox6.Items[i]); }
                    else
                    { parameter = parameter + "," + checkedListBox6.GetItemText(checkedListBox6.Items[i]); }
                }
            }

            #endregion
            #region
            if (parameter.Length == 0)
            { MessageBox.Show("未选择参数，退出"); return; }
            // pick tool
            sql = " SELECT substr(date,7,4)||'-'||substr(date,1,2)||'-'||substr(date,4,2) riqi, " + parameter + " FROM tbl_asmlbatchreport  ";
          
            if (tool.Length == 0 || tool.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql = sql + " WHERE TOOL IN  (" + tool + ")"; }
            // pick part
            if (part.Length == 0 || part.Substring(0, 5) == "'ALL'")
            { }
            else
            {
                if ((sql.Trim()).Substring((sql.Trim()).Length - 15, 15) == "asmlbatchreport")
                {
                    sql = sql + " WHERE substr(jobName,6,20) IN  (" + part + ") ";

                }
                else
                {
                    sql = sql + " AND substr(jobName,6,20) IN  (" + part + ") ";

                }
            }
            //pick layer

            if (layer.Length == 0 || layer.Substring(0, 3) == "'AL")
            { }
            else
            {
                if ((sql.Trim()).Substring((sql.Trim()).Length - 15, 15) == "asmlbatchreport")
                {
                    sql = sql + " WHERE layer IN  (" + layer + ") ";

                }
                else
                {
                    sql = sql + " AND LAYER IN  (" + layer + ") ";

                }



            }

            //define date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 15, 15) == "asmlbatchreport")
            {
                sql = sql + " WHERE substr(date,7,4)||'-'||substr(date,1,2)||'-'||substr(date,4,2) between  '" + label1.Text + "' and  '"+label2.Text+"'";

            }
            else
            {
                sql = sql + " AND substr(date,7,4)||'-'||substr(date,1,2)||'-'||substr(date,4,2) between  '" + label1.Text + "' and  '" + label2.Text + "'";

            }
            MessageBox.Show("查询条件是：\n\n" + sql+"\n\n注意日期区间是否正确；点击日历选择日期，再点击右侧日期标签刷新");


            #endregion
            #region
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
                            tblFirst = ds.Tables[0];

                        }
                    }
                }
            }
            #endregion

            int rowsCount = tblFirst.Rows.Count;

      
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据\n\n点击右侧_\"显示原始数据\"_按钮,在表格中查看数据");
        }


        private void button5_Click(object sender, EventArgs e) //referesh rework
        {
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            string strDate;
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

            //删除最新一天数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM tbl_rework where HISTDATE='" + strDate + "'";
                //sql = "DELETE FROM tbl_move";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

   
       

            sql = " select to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'MM') MM,  to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'iw') WW, HISTDATE, LOTID,PRIORITY Pri,PARTID,STAGE,REWORKQTY QTY,decode(eqptype,'LDI','ASML','NIKON') eqptype , eqpid, PPID,trackintime ,TIMEREV, REWORKCODE CODE, Description, EVVARIANT,lottype from RPTPRD.mfg_tbl_rework  where eqptype IS NOT NULL  AND substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') and HISTDATE >='"+strDate +"' order by TIMEREV";

            CsmcOracle(sql);
            string dbTblName = "tbl_rework";
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\ReworkMove.DB");
            MessageBox.Show("Rework Update Done");

        }

        private void button6_Click(object sender, EventArgs e) //refresh move
        {
            connStr = "data source=z:\\_SQLite\\ReworkMove.DB";


            string strDate;
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


            CsmcOracle(sql);
            //MessageBox.Show("共有记录 " + tblFirst.Rows.Count.ToString() + " 条");

            string dbTblName = "tbl_move";
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, "z:\\_SQLite\\ReworkMove.DB");
            MessageBox.Show("Move Update Done");
        }












        //======辅助项
        #region
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Text; //DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        }
        private void label2_Click(object sender, EventArgs e)
        { 
            label2.Text = dateTimePicker1.Text;
  
        }
    
 
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            tblFirst=tblSecond = null; sql = connStr = null;
            GC.Collect();

        }
        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("选择远程源文件");
            string src,dest;
         
            openFileDialog1.InitialDirectory = @"\\10.4.72.74\asml\_SQLite\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                src = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
         

            MessageBox.Show("选择本机文件保存");
            saveFileDialog1.InitialDirectory = @"c:\temp";
            saveFileDialog1.Title = "选择目标文件";
            saveFileDialog1.Filter = "db文件t(*.db)|*.db";//
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dest = saveFileDialog1.FileName.Replace("\\", "\\\\");
                File.Copy(src, dest, true);
                MessageBox.Show("文件复制完毕");
            }
            else
            { MessageBox.Show("未选择目标文件，复制未成功"); return; }

           
        }
        private void ShowMain()
        {
            this.Visible = true;
            this.BringToFront();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                chartForm.ChildTrigger += ShowMain;
                MainTrigger1(tblFirst);

                flag += 1;
            }
            else
            {

                MainTrigger1(tblFirst);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                MainTrigger1(tblSecond);
                chartForm.ChildTrigger += ShowMain;
                flag += 1;
            }
            else
            {

                MainTrigger1(tblSecond);
            }
        }
      
        private void button7_Click(object sender, EventArgs e)
        {
            if (flag>0)
            {
                this.Visible = false;
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                chartForm.ChildTrigger += ShowMain;
                MainTrigger1(tblFirst);

                flag += 1;
            }
            else
            {

                MainTrigger1(tblFirst);
            }
        }
        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nikon预对位数据路径：\\\\10.4.72.74\\asml\\_SQLite\\NikonEgaPara.db\n\nAsmlBatchReport数据路径：\\\\10.4.72.74\\asml\\_SQLite\\AsmlBatchreport.db");
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                MainTrigger2();
                chartForm.ChildTrigger += ShowMain;

                flag += 1;
            }
            else
            {

                MainTrigger2();
            }
        }
        private void CsmcOracle(string sql) //https://www.cnblogs.com/mq0036/p/3678148.html;https://www.cnblogs.com/worfdream/articles/2938658.html
        {

            string connStr;
            connStr = "Provider=MSDAORA.1;Password=rptlinkpw;User ID=rptlink;Data Source=MFGEXCEL;";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open(); using (OleDbCommand command = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        { da.Fill(ds); tblFirst = ds.Tables[0]; ; }
                    }
                }
            }
           
        }
        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                chartForm.ChildTrigger += ShowMain;
                MainTrigger1(tblFirst);

                flag += 1;
            }
            else
            {

                MainTrigger1(tblFirst);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DB Path: //10.4.72.74/_SQLite/ReworkMove.DB\n\nTable Name:tbl_layer__FEOL/BEOL定义\n\nTable Name:tbl_rework__返工数据\n\nTable Name:tbl_move(仅每日汇总数据，无细节）__MOVE数据\n\n数据更新需权限访问MFGEXECL数据库\n\nMFG数据库中MOVE数据不足一年，2019-1/2月份数据缺失，3月不完整");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sql = " select A.PARTTITLE, A.PART, A.STAGE, A.RECIPE, A.PPID, A.EQPID, A.FLAG, A.TYPE, A.ACTIVEFLAG, A.FAILREASON, A.EXPIRETIME, A.CREATEUSER, A.USERDEPT, A.CREATETIME, A.REQUESTUSER  from rptprd.processconstraint A where ACTIVEFLAG='Y' and (substr(A.eqpid,1,4) in('ALCT', 'BLCT', 'ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI', 'ALTI', 'BLTI', 'ALTD', 'BLTD','ALSD','BLSD','ALCD','BLCD','ALOL','BLOL') ) order by eqpid,parttitle,part,stage";

            CsmcOracle(sql);

            int i = tblFirst.Rows.Count;
            MessageBox.Show("光刻ESF限制合计_" + i.ToString() + "_条");
        }
  
        #endregion

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("右键点击我的电脑，映射Z盘为\\\\10.4.72.74\\asml\n\n默认查询周期为三个月，可点击主窗体日历及_起始_，_结束_修改日期");
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                //sql = "SELECT MM,WW,DAY,TOOL,STAGE,CAST(LOTQTY AS double) as LOTQTY,CAST(WFRQTY AS double) as WFRQTY FROM tbl_move";

                sql = " SELECT B.FlowType,A.* FROM (SELECT * FROM tbl_rework WHERE HISTDATE BETWEEN '" + label1.Text + "' AND '" + label2.Text + "') A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.EQPTYPE=B.TOOLTYPE";

               
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblFirst = ds.Tables[0];

                        }
                    }
                }
            }
            MessageBox.Show("返工数据_" + tblFirst.Rows.Count.ToString() + "_条\n\n选择_Display_菜单显示数据");
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
            
            
            
            connStr = "data source=" + "z:\\_SQLite\\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                conn.Open();
                //工艺按FEOL/BEOL
                string sql1, sql2, sql12;

                sql1 = " SELECT B.FlowType,A.* FROM tbl_rework A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.EQPTYPE=B.TOOLTYPE";
                sql1 = " SELECT FLOWTYPE,'MM'||MM AS RIQI, EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM (" + sql1 + ") WHERE CODE not in ( 'Q2','Q1','Q3','Z7') GROUP BY FLOWTYPE,MM,EQPTYPE UNION SELECT FLOWTYPE,'WW'||WW AS RIQI, EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM (" + sql1 + ") WHERE CODE not in ( 'Q2','Q1','Q3','Z7') GROUP BY FLOWTYPE,WW,EQPTYPE  ";//两组Select不能分别加括号
                sql1 = "SELECT * FROM (" + sql1 + ") ORDER BY RIQI,FLOWTYPE,EQPTYPE";


                sql2 = " SELECT B.FlowType,A.* FROM tbl_move A,tbl_layer B WHERE A.STAGE=B.STAGE AND A.TOOL=B.TOOLTYPE";
                sql2 = " SELECT FLOWTYPE,'MM'||MM AS RIQI,TOOL AS EQPTYPE,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM (" + sql2 + ") GROUP BY FLOWTYPE,MM,TOOL UNION  SELECT FLOWTYPE,'WW'||WW AS RIQI,TOOL AS EQPTYPE,SUM(LOTQTY) AS LOTQTY,SUM(WFRQTY) AS WFRQTY FROM (" + sql2 + ") GROUP BY FLOWTYPE,WW,TOOL";
                sql2 = "SELECT * FROM (" + sql2 + ") ORDER BY RIQI,FLOWTYPE,EQPTYPE";

                sql12 = "SELECT T1.*,T2.LOTQTY AS MOVELOTQTY,T2.WFRQTY AS MOVEWFRQTY,T1.LOTQTY/T2.LOTQTY*100 AS LOTRATIO,T1.WFRQTY/T2.WFRQTY*100 WFRRATIO FROM (" + sql1 + ") T1,(" + sql2 + ") T2 WHERE T1.RIQI=T2.RIQI AND T1.FLOWTYPE=T2.FLOWTYPE AND T1.EQPTYPE=T2.EQPTYPE ORDER BY RIQI,FLOWTYPE,EQPTYPE";
                sql12 = " SELECT FLOWTYPE,RIQI,EQPTYPE,CAST(LOTQTY AS double) LOTQTY,WFRQTY,MOVELOTQTY, MOVEWFRQTY,ROUND(LOTRATIO,2) LOTRATIO,ROUND(WFRRATIO,2) WFRRATIO FROM (" + sql12 + ")";


                //设备返工

                string sql3, sql4, sql34;

                sql3 = " SELECT (case code when 'Q1' then '_ScannerStepper' when 'Q2' then '_Track' else '_Metrology' end ) FLOWTYPE, 'MM'||MM AS RIQI,(case code when 'Q1' then ''   else '' end ) EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK WHERE CODE IN ('Q1','Q2','Q3') GROUP BY MM,(case code when 'Q1' then '_ScannerStepper' when 'Q2' then '_Track' else '_Metrology' end ),(case code when 'Q1' then ''   else '' end ) UNION SELECT (case code when 'Q1' then '_ScannerStepper' when 'Q2' then '_Track' else '_Metrology' end ) FLOWTYPE, 'WW'||WW AS RIQI,(case code when 'Q1' then ''   else '' end ) EQPTYPE,COUNT(QTY) AS LOTQTY,SUM(QTY) AS WFRQTY FROM TBL_REWORK WHERE CODE IN ('Q1','Q2','Q3') GROUP BY WW,(case code when 'Q1' then '_ScannerStepper' when 'Q2' then '_Track' else '_Metrology' end ),(case code when 'Q1' then ''   else '' end )";


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
             




                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblFirst = ds.Tables[0];

                        }
                    }
                }
            }




            MessageBox.Show("返工数据_" + tblFirst.Rows.Count.ToString() + "_条\n\n选择_Display_菜单显示数据");




        }
                
   


     
       

        

        


   

     

        

        

        

       

     

      

        

       

    

     

   

        

   

        }
}


#region DataTable to SQLite CLASS

public class DataTableToSQLte   //https://blog.csdn.net/qq_42678477/article/details/81660682
    {        
        private string tableName;         
        public string TableName        
        {            
            get { return tableName;}            
            set { tableName = value; }
        }        
        private string insertHead;         
        public string InsertHead        
        {            
            get { return insertHead; }       
        }         
        private string[] separators;         
        public string[] Separators        
        {            
            get { return separators; }            
            set { separators = value; }        
        }         
        private string insertCmdText;         
        private int colCount;        
        private string[] fields;    
     
        public DataTableToSQLte(DataTable dt,string dbTblName)        
        {            
            List<string> myFields = new List<string>();            
            List<string> mySeparators = new List<string>();            
            List<string> valueVars = new List<string>();// insert command text            
            colCount = dt.Columns.Count;             
            for (int i = 0; i < colCount; i++)            
            {                
                string colName = dt.Columns[i].ColumnName;                
                myFields.Add(colName);                
                mySeparators.Add(GetSeperator(dt.Columns[i].DataType.ToString()));                
                valueVars.Add("@" + colName);
                
            }            
           // insertHead = string.Format("insert into {0} ({1})"                
            //    , dt.TableName                
             //   , string.Join(",", myFields.ToArray()));

            insertHead = string.Format("insert into {0} ({1})"
                , dbTblName
                , string.Join(",", myFields.ToArray()));
        
            separators = mySeparators.ToArray();             
            insertCmdText = string.Format("{0} values ({1})", insertHead                
                , string.Join(",", valueVars.ToArray()));
        
            fields = myFields.ToArray();         
        }         
        private string GetSeperator(string typeName)        
        {            
            string result = string.Empty;            
            switch (typeName)            
            {                
                case "System.String":                    
                    result = "'";                    
                    break;                 
                
                default:                    
                    result = typeName;                    
                    break;            
            }
            return result;        
        }          
        public string GenInsertSql(DataRow dr)        
        {            
            List<string> strs = new List<string>();            
            for (int i = 0; i < colCount; i++)            
            {                
                if (DBNull.Value == dr[i])  //null or DBNull                    
                    strs.Add("null");                
                else                    
                    strs.Add(string.Format("{0}{1}{0}", separators[i], dr[i].ToString()));            
            }
            return string.Format("{0} values ({1})", insertHead, string.Join(",", strs.ToArray()));      
  

        }         
        public void ImportToSqliteBatch(DataTable dt, string dbFullName)        
        {            
            string strConn = string.Format("data source={0}", dbFullName);
           
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {         
                using (SQLiteCommand insertCmd = conn.CreateCommand())                
                {                    
                    insertCmd.CommandText = insertCmdText;                    
                    conn.Open(); 
                    SQLiteTransaction tranction = conn.BeginTransaction();                    
                    foreach (DataRow dr in dt.Rows)                    
                    {                        
                        for (int i = 0; i < colCount; i++)                        
                        {                            
                            object o = null;                            
                            string paraName = "@" + fields[i];                            
                            if (DBNull.Value != dr[fields[i]])                                
                                o = dr[fields[i]];                            
                            insertCmd.Parameters.AddWithValue(paraName, o);
                            
                        }
                       
                        insertCmd.ExecuteNonQuery();
                       
                    }                    
                    tranction.Commit();                
                }            
            }        
        }          
   
    }
#endregion