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


//datagridview 加筛选功能
//https://www.cnblogs.com/billowliu/articles/5587661.html
//https://blog.csdn.net/mouday/article/details/81049209
//https://blog.csdn.net/phoenix36999/article/details/45916943


namespace WindowsFormsLitho
{
  
    public partial class MainForm : Form
    {
        public delegate void MainDelegate();  //矢量图
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
            label1.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
           // label1.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");

            // MessageBox.Show("数据筛选日期定义为90天，请按需求更改；\n\n筛选界面不要拖到屏幕中央；");
        }

        #region//NIKON 
        private void button1_Click(object sender, EventArgs e)  //Nikon预对位
        {
            long sDate, eDate;
            sDate = Convert.ToInt64(label1.Text.Replace("-", "")) * 10000;
            eDate = Convert.ToInt64(label2.Text.Replace("-", "")) * 10000;
            if (MessageBox.Show("远程数据查询和显示较慢，请确认已选择合适日期，或已将数据源转移到本机\r\n\r\n" +
               "选择-->是（Y），继续；\r\n\r\n选择-->否（N)，退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                openFileDialog1.InitialDirectory = @"D:\TEMP\DB\_SQLite\";
                openFileDialog1.Filter = "db文件(NikonEgaPara.DB)|NikonEgaPara.DB|db文件（*.DB)|*.DB";//
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
                else
                { MessageBox.Show("未选择数据文件"); return; }

            }
            else
            { MessageBox.Show("已选择退出"); return; }
            tblFirst = Nikon.PreAlignment(sDate, eDate, connStr);
            MessageBox.Show("共计筛选到" + tblFirst.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            DisplayTable(tblFirst);
        }  
        private void button3_Click(object sender, EventArgs e)
        {
            string ppid = textBox7.Text.Trim().ToUpper();
            tblFirst = tblSecond = tblChart = null;
            if (MessageBox.Show("远程数据查询和显示较慢，建议将数据源转移到本机\r\n\r\n继续查询请选择是（Y）\r\n\r\n" +
                "选择-->是（Y）,继续；\r\n\r\n选择-->否（N），退出\r\n\r\n\r\n" +
                "查询是按日期和PPID分类，可以输入不完整名称，其格式是 part.layer, 不区分大小写, 例如 D90848.at\r\n\r\n" +
                "Part和Layer之间的半角 '.' 不能缺失\r\n\r\n " +
                "查询系通配符查询，D90848.AT的查询关键字是%D90848%.%AT%\r\n\r\n" +
                "查询所有PPID，文本框输入 %.%\r\n\r\n" +
                "注意：查询不显示日期，但结果按时间排序\r\n\r\n查询系针对所有历史数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                openFileDialog1.InitialDirectory = @"D:\TEMP\DB\_SQLite\";
                openFileDialog1.Filter = "db文件(NikonEgaPara.DB)|NikonEgaPara.DB|db文件（*.DB)|*.DB";//
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
                else
                { MessageBox.Show("未选择数据文件"); return; }
            }
            else
            { MessageBox.Show("已选择退出"); return; }
            tblFirst = Nikon.AlignMethod( connStr,ppid);
            MessageBox.Show("共计筛选到" + tblFirst.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            DisplayTable(tblFirst);

        } //Nikon Search/Ega方式查询
        private void button2_Click(object sender, EventArgs e)
        {
            long sDate, eDate; string ppid;
            ppid = textBox7.Text.Trim().ToUpper();
            sDate = Convert.ToInt64(label1.Text.Replace("-", "")) * 10000; eDate = Convert.ToInt64(label2.Text.Replace("-", "")) * 10000;

            if (MessageBox.Show("远程数据查询和显示较慢，建议将数据源转移到本机\r\n\r\n" +
                "查询是按日期和PPID分类，可以输入不完整名称，其格式是 part.layer,不区分大小写,例如 D90848.at " +
                "Part和Layer之间的半角 '.' 不能缺失\r\n" +
                "查询系通配符查询，D90848.AT的查询关键字是%D90848%.%AT%\r\n\r\n" +
                "继续查询请选择是（Y）\r\n\r\n" +
                "选择-->是（Y）,继续；\r\n\r\n选择-->否（N），退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                openFileDialog1.InitialDirectory = @"D:\TEMP\DB\_SQLite\";
                openFileDialog1.Filter = "db文件(NikonEgaPara.DB)|NikonEgaPara.DB|db文件（*.DB)|*.DB";//
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
                else
                { MessageBox.Show("未选择数据文件"); return; }
            }
            else
            { MessageBox.Show("已选择退出"); return; }
            tblFirst = Nikon.AlignData(sDate, eDate, connStr, ppid);
            MessageBox.Show("共计筛选到" + tblFirst.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            DisplayTable(tblFirst);

        }  //Nikon 对位数据
        private void button29_Click(object sender, EventArgs e) //抽取数据，对应Lotid
        {
            string ppid = textBox7.Text.Trim().ToUpper();
            tblFirst = tblSecond = tblChart = null;
            long sDate = Convert.ToInt64(label1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(label2.Text.Replace("-", "")) * 10000;

            if (MessageBox.Show("远程数据查询和显示较慢，建议将数据源转移到本机\r\n\r\n" +
                "查询是按日期和PPID分类，可以输入不完整名称，其格式是 part.layer,不区分大小写,例如 D90848.at " +
                "Part和Layer之间的半角 '.' 不能缺失\r\n" +
                "查询系通配符查询，D90848.AT的查询关键字是%D90848%.%AT%\r\n\r\n" +
                "查询数据显示在列表框中，双击匹配的设备、时间项，显示矢量图" +
                "继续查询请选择是（Y）\r\n\r\n" +
                "选择-->是（Y）,继续；\r\n\r\n选择-->否（N），退出\r\n\r\n" +
                "注意：Nikon Ega Log可能不能完全或及时下载，部分数据可能缺失", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                openFileDialog1.InitialDirectory = @"D:\TEMP\DB\_SQLite\";
                openFileDialog1.Filter = "db文件(NikonEgaPara.DB)|NikonEgaPara.DB|db文件（*.DB)|*.DB";//
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
                else
                { MessageBox.Show("未选择数据文件"); return; }
            }
            else
            { MessageBox.Show("已选择退出"); return; }

            tblFirst = Nikon.VectorIndex(sDate, eDate, connStr, ppid);
            if (tblFirst.Rows.Count > 0)
            {               
                this.checkedListBox1.Items.Clear();
                for (int i = 0; i < tblFirst.Rows.Count; i++){this.checkedListBox1.Items.Add(tblFirst.Rows[i][0].ToString() + ", " + tblFirst.Rows[i][1].ToString() + ", " + tblFirst.Rows[i][2].ToString() + ", " + tblFirst.Rows[i][3].ToString());}
            }
            else { MessageBox.Show("未查询到数据");      }

        }
        private void button30_Click(object sender, EventArgs e) //抽取矢量原始数据,调用子窗体，所有wfer画图
        {
            MessageBox.Show("该命令只查询第一个选中项的数据，每次只显示一个lot的数据");
            long str=0;  string str1 = string.Empty;
            if (checkedListBox1.Items.Count == 0) { MessageBox.Show("请在列表框中勾选LOTID对应项"); }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        str =Convert.ToInt64(  checkedListBox1.GetItemText(checkedListBox1.Items[i]).Split(new char[] { ',' })[2].Trim());
                        str1 = checkedListBox1.GetItemText(checkedListBox1.Items[i]).Split(new char[] { ',' })[0].Trim();
                        break;
                    }
                }
            }
            tblFirst = Nikon.VectorData(connStr, str1, str);

            DisplayTable(tblFirst);
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                chartForm.ChildTrigger += ShowMain;
                MainTrigger();

                flag += 1;
            }
            else{ MainTrigger(); }

        }
        #endregion












        #region//=======ASML BATCH_REPORT
        private void button8_Click(object sender, EventArgs e)
        {
            tblFirst = tblSecond = tblChart = null;
            //openFileDialog1.InitialDirectory = @"\\10.4.72.74\asml\_SQLite\";
            openFileDialog1.InitialDirectory = @"c:\temp\";

            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
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

            paraArr = null; tblFirst = tblSecond = tblChart = null;
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
                sql = sql + " WHERE substr(date,7,4)||'-'||substr(date,1,2)||'-'||substr(date,4,2) between  '" + label1.Text + "' and  '" + label2.Text + "'";

            }
            else
            {
                sql = sql + " AND substr(date,7,4)||'-'||substr(date,1,2)||'-'||substr(date,4,2) between  '" + label1.Text + "' and  '" + label2.Text + "'";

            }
            MessageBox.Show("查询条件是：\n\n" + sql + "\n\n注意日期区间是否正确；点击日历选择日期，再点击右侧日期标签刷新");


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
        #endregion


        #region //=======REWORK
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




            sql = " select to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'MM') MM,  to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'yyyy')||to_char(to_date(to_char(TIMEREV-7.5/24,'yyyy-mm-dd')||' 07:30:00','yyyy-mm-dd hh:mi:ss'),'iw') WW, HISTDATE, LOTID,PRIORITY Pri,PARTID,STAGE,REWORKQTY QTY,decode(eqptype,'LDI','ASML','NIKON') eqptype , eqpid, PPID,trackintime ,TIMEREV, REWORKCODE CODE, Description, EVVARIANT,lottype from RPTPRD.mfg_tbl_rework  where eqptype IS NOT NULL  AND substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') and HISTDATE >='" + strDate + "' order by TIMEREV";

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
        #endregion


        #region//====R2R
        private void button14_Click(object sender, EventArgs e) //CD REFRESH
        {
            tblFirst = null; tblSecond = null;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }
  
            string strDate;
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
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
            string filePath;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            {  filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }
  
            tblFirst = OpenCsv(filePath);
            DataRow[] drs = tblFirst.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            tblSecond = tblFirst.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    tblSecond.ImportRow(drs[i]);
                }
                tblFirst = null; tblFirst = tblSecond.Copy(); tblSecond = null;
                string dbTblName = "tbl_cd";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
                myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\R2R.DB");
                MessageBox.Show("CD Update Done");
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }

        }
        private void button15_Click(object sender, EventArgs e) //CD CONFIGREFRESH
        {

            tblFirst = null; tblSecond = null;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }
            //读取
            string filePath;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            tblFirst = OpenCsv(filePath);

            //删除旧数据
            string dbTblName = "tbl_config";
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
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\R2R.DB");
            MessageBox.Show("CD Config Update Done");
        }
        private void button16_Click(object sender, EventArgs e)//nikonjobinstation

        {

            tblFirst = null; tblSecond = null;
            MessageBox.Show("选择R2R SQLite DB");
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }
            //读取
            MessageBox.Show("选择Nikon Jobin Station CSV文件");
            string filePath;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            tblFirst = OpenCsv(filePath);

            //删除旧数据
            string dbTblName = "tbl_nikonjobinstation";
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
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\R2R.DB");
            MessageBox.Show("Nikon Jobin Station Update Done");



        }
        private void button17_Click(object sender, EventArgs e)//Asmljobinstation
        {

            tblFirst = null; tblSecond = null;
            MessageBox.Show("选择R2R SQLite DB");
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }
            //读取
            MessageBox.Show("选择Asml Jobin Station CSV文件");
            string filePath;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            tblFirst = OpenCsv(filePath);

            //删除旧数据
            string dbTblName = "tbl_asmljobinstation";
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
            DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\R2R.DB");
            MessageBox.Show("Asml Jobin Station Update Done");

        }
        private void button18_Click(object sender, EventArgs e)//ovl更新
        {
            tblFirst = null; tblSecond = null;
            MessageBox.Show("选择R2R SQLite DB");
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "db文件(*.db)|*.db|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            string strDate;
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_ovl";//first record select English from graduate_phrase limit 0,1
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
            MessageBox.Show("选择OVERLAY CSV文件");
            string filePath;
            openFileDialog1.InitialDirectory = @"z:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            tblFirst = OpenCsv(filePath);
            DataRow[] drs = tblFirst.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            tblSecond = tblFirst.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    tblSecond.ImportRow(drs[i]);
                }
                tblFirst = null; tblFirst = tblSecond.Copy(); tblSecond = null;
                string dbTblName = "tbl_ovl";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
                myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\R2R.DB");
                MessageBox.Show("Overlay Update Done");
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }
        }
        private void button20_Click(object sender, EventArgs e) //CD QUERY
        {
            tblFirst = tblSecond = tblChart = null;

            #region
            //pick tool
            string tool = "";
            for (int i = 0; i < checkedListBox7.Items.Count; i++)
            {
                if (checkedListBox7.GetItemChecked(i))
                {
                    if (tool == "")
                    { tool = "'" + checkedListBox7.GetItemText(checkedListBox7.Items[i]) + "'"; }
                    else
                    { tool = tool + ",'" + checkedListBox7.GetItemText(checkedListBox7.Items[i] + "'"); }
                }
            }
          
            //pick part
            string part = textBox1.Text;


            //pick layer 
            string layer = textBox2.Text;



            #endregion
            #region
            sql = " SELECT DCOLL_TIME,JI_TIME,TYPE,TOOL,CDSEM,PART,LAYER,AVG,JOBIN,OPT,FB FROM TBL_CD ";

            if (tool.Length == 0 || tool.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql = sql + " WHERE TOOL IN  (" + tool + ")"; }

            //pick part
            if (part.Trim().Length == 0)
            { }
            else
            {
                string[] partArr = part.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in partArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " PART like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " || PART like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }


            //pick layer
            if (layer.Trim().Length == 0)
            { }
            else
            {
                string[] layerArr = layer.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in layerArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " layer like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " || layer like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }

            //define date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
            {
                sql += " WHERE (DCOLL_TIME BETWEEN '" + label1.Text + "' and  '" + label2.Text + "')";

            }
            else
            {
                sql = sql + " AND (DCOLL_TIME BETWEEN '" + label1.Text + "' and  '" + label2.Text + "')";

            }



            #endregion
            MessageBox.Show("查询条件是：\n\n" + sql + "\n\n注意日期区间是否正确；点击日历选择日期，再点击右侧日期标签刷新");


            openFileDialog1.InitialDirectory = @"在z:\_SQLITE\";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择数据文件"); return; }
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
            int rowsCount = tblFirst.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");

        }
        private void button19_Click(object sender, EventArgs e) //OVL QUERY
        {
            tblFirst = tblSecond = tblChart = null;

            #region
            //pick tool
            string tool = "";
            for (int i = 0; i < checkedListBox7.Items.Count; i++)
            {
                if (checkedListBox7.GetItemChecked(i))
                {
                    if (tool == "")
                    { tool = "'" + checkedListBox7.GetItemText(checkedListBox7.Items[i]) + "'"; }
                    else
                    { tool = tool + ",'" + checkedListBox7.GetItemText(checkedListBox7.Items[i] + "'"); }
                }
            }
            //pick parameter
            string para = "";
            for (int i = 0; i < checkedListBox8.Items.Count; i++)
            {
                if (checkedListBox8.GetItemChecked(i))
                {
                    if (para == "")
                    { para = "'" + checkedListBox8.GetItemText(checkedListBox8.Items[i]) + "'"; }
                    else
                    { para = para + ",'" + checkedListBox8.GetItemText(checkedListBox8.Items[i] + "'"); }
                }
            }

            //pick part
            string part =textBox1.Text;
            

            //pick layer 
            string layer = textBox2.Text;
           


            #endregion
            #region
            sql = " SELECT DCOLL_TIME,JI_TIME,TYPE,TOOL,PARA,PART,LAYER,JOBIN,AVG,OPT,FB FROM TBL_OVL ";

            if (tool.Length == 0 || tool.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql = sql + " WHERE TOOL IN  (" + tool + ")"; }

            // pick para
            if (para.Length == 0 || para.Substring(0, 5) == "'ALL'")
            { }
            else
            {
                if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
                {
                    sql = sql + " WHERE PARA IN  (" + para + ") ";

                }
                else
                {
                    sql = sql + " AND PARA IN  (" + para + ") ";

                }
            }

            //pick part
            if (part.Trim().Length == 0)
            { }
            else
            {
                string[] partArr = part.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in partArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " PART like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " || PART like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
                {                   
                    sql +=  " WHERE " + tmp ;

                }
                else
                {
                    sql +=  " AND " + tmp;

                }
            }


            //pick layer
            if (layer.Trim().Length == 0)
            { }
            else
            {
                string[] layerArr = layer.Split(new char[] { ' ' });
                string tmp = "";
                foreach (string str in layerArr)
                {
                    if (tmp.Length == 0)
                    {
                        tmp = " layer like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " || layer like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }






           




       

            //define date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
            {
                sql += " WHERE (DCOLL_TIME BETWEEN '" + label1.Text + "' and  '" + label2.Text + "')";

            }
            else
            {
                sql = sql + " AND (DCOLL_TIME BETWEEN '" + label1.Text + "' and  '" + label2.Text + "')";

            }
           


            #endregion
            MessageBox.Show("查询条件是：\n\n" + sql + "\n\n注意日期区间是否正确；点击日历选择日期，再点击右侧日期标签刷新");
                    
            openFileDialog1.InitialDirectory = @"在z:\_SQLITE\";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { connStr = "data source=" + openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择数据文件"); return; }
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
            int rowsCount = tblFirst.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");
        }
        #endregion

        #region//===MFGEXCEL

        private void button4_Click(object sender, EventArgs e) //ESF查询
        {
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_ESF ORDER BY EQPID,反向1正向0";
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
            MessageBox.Show("光刻ESF限制_" + tblFirst.Rows.Count.ToString() + "_条\n\n选择_Display_菜单显示数据");



        }
        private void button26_Click(object sender, EventArgs e)//涂胶程序
        {
            MessageBox.Show("查询所有流程的涂胶程序，20多万条，最终点击Display显示时较慢");
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_FLOWTRACK ORDER BY PART,STAGE";
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
            MessageBox.Show("所有流程涂胶程序_" + tblFirst.Rows.Count.ToString() + "_条\n\n选择_Display_菜单显示数据");


        }

        private void button21_Click(object sender, EventArgs e) //显示excel表格名
        {
         


      
            string filepath = "";
            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
            file.Filter = "文档(*.xls)|*.xls|文档(*.xlsx)|*.xlsx";
            if (file.ShowDialog() == DialogResult.OK) filepath = file.FileName;
         //   string fileNameWithoutExtension = System.IO.Path.GetDirectoryName(filepath);// 没有扩展名的文件名 “Default”

            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         
            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"
            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值
            // 3、判断是xls还是xlsx
           // string[] sArray = filepath.Split('.');//有问题,路径名带"."

            string strTmp = filepath.Trim().Substring(filepath.Length - 3,3);

            connStr = string.Empty;
            if (strTmp=="xls")
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意

            }

            else if (strTmp == "lsx")
            {

                connStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filepath + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';";

            }

  
            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(connStr))
                {
                    OleConn.Open();
                    DataTable sheetsName = OleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    listBox1.Items.Clear();
                    for (int i = 0; i < sheetsName.Rows.Count; i++)
                    {
                        strTmp = sheetsName.Rows[i][2].ToString();
                        if (strTmp.Substring(strTmp.Length - 1, 1) == "$")
                        {
                            listBox1.Items.Add(sheetsName.Rows[i][2].ToString());
                        }

                    }
                }
            }
            catch (Exception)

            {
                MessageBox.Show("ERROR");
            }
       




/*
               





                
              
 




/// 
 2 /// 写入Excel文档
 3 /// 
 4 public bool SaveFP2toExcel(string filePathath)
 5 {
 6     try
 7     {
 8         string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ filePathath +";Extended Properties=Excel 8.0;";
 9         OleDbConnection conn = new OleDbConnection(strConn);
10         conn.Open();  
11         System.Data.OleDb.OleDbCommand cmd=new OleDbCommand ();
12         cmd.Connection =conn;
13 
14         for(int i=0;i0].RowCount -1;i++)
15         {
16             if(fp2.Sheets [0].Cells[i,0].Text!="")
17             {
18                 cmd.CommandText ="INSERT INTO [sheet1$] (工号,姓名,部门,职务,日期,时间) VALUES('"+fp2.Sheets [0].Cells[i,0].Text+ "','"+
19                 fp2.Sheets [0].Cells[i,1].Text+"','"+fp2.Sheets [0].Cells[i,2].Text+"','"+fp2.Sheets [0].Cells[i,3].Text+
20                 "','"+fp2.Sheets [0].Cells[i,4].Text+"','"+fp2.Sheets [0].Cells[i,5].Text+"')";
21                 cmd.ExecuteNonQuery ();
22             }
23         }
24         
25         conn.Close ();
26         return true;
27     }
28     catch(System.Data.OleDb.OleDbException ex)
29     {
30         Console.WriteLine ("写入Excel发生错误："+ex.Message );
31         return false;
32     }
33 }
————————————————
版权声明：本文为CSDN博主「彭世瑜」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/mouday/article/details/81049212


*/











        }
        private void button22_Click(object sender, EventArgs e)  //显示Excel表格数据
        {
            //https://www.cnblogs.com/ammy714926/p/4905026.html
            string sheetName;


            if (listBox1.SelectedItems.Count > 0)
            {
                sheetName = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("请选择查阅的Excel表"); return;
            }


            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(connStr))
                {
                    OleConn.Open();
                    sql = string.Format("SELECT * FROM  [{0}]", sheetName);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        tblFirst = ds.Tables[0];
                    }

                }
                MessageBox.Show("数据绑定Excel成功，点击_Display_菜单显示");
            }
            catch (Exception)

            {
                MessageBox.Show("数据绑定Excel失败");
            }




        }


        private void button23_Click(object sender, EventArgs e)
        {  
            sql = "select a.TECH,a.PARTID,a.STAGE,EQPTYPE,a.TITLE,a.PPID,a.MASK,b.STATUS from";
            sql += "(select substr(TECHNOLOGY,1,5) TECH,PARTID,STAGE,EQPTYPE,TITLE,PPID,MASK,SORTNUM from RPTPRD.MFG_VIEW_FLOW";
            sql += " where partid like '%'||'" +textBox3.Text + "'||'%' ) a,(select * from RPTPRD.RMSRETICLE) b where a.mask=b.reticleid(+) order by a.SORTNUM";
            CsmcOracle(sql);

            int i = tblFirst.Rows.Count;
            MessageBox.Show("流程查询结束，点击Display菜单查看");

        }

        private void button24_Click(object sender, EventArgs e) //WIP vs ESF
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            // 10.57.170.26
            tblFirst = tblSecond = tblChart = null;
            //extract data
            #region
            sql = "  SELECT a.TECHNOLOGY, a.PARTID, a.LOTID, a.STATUS,a.EQPID,a.PPID, a.STAGE,a.EQPTYPE,a.LOTTYPE,a.PRIORITY,b.MFG_PRIORITY ,a.QTY,a.LASTDAY_TR,a.TR,a.RTIME, a.HTIME, a.QTIME, a.HOLDCOMMENT ";
            sql += " FROM RPTPRD.sdb_view_info_wip a, rptprd.mfg_tbl_internal_priority b ";
            sql += " WHERE  a.lotid=b.lotid(+) and (a.LOTTYPE Not In ('CM','MW','ZZ')) AND (a.STATUS Not In ('SCHED','COMPLT','FINISH','LHLD','TRAN'))   AND (a.LOTID Not Like 'F%') and (a.stage like '%-PH' or a.holdcomment like '%PHPE%') ORDER BY a.EQPTYPE,a.STAGE";
            //以上是MFGSQL,j简化如下
            sql = " SELECT  SUBSTR(PARTID,1,LENGTH(PARTID)-3) PART,SUBSTR(RECIPE,1,5) RECIPE,  STAGE,EQPTYPE,PPID,SUM( QTY)   WFRQTY,COUNT(QTY) LOTQTY FROM RPTPRD.sdb_view_info_wip WHERE   (LOTTYPE Not In ('CM','MW','ZZ')) AND (STATUS  In ('WAIT','HELD')) and STAGE like '%-PH' and EQPTYPE in ('LDI','LII','LSI') GROUP BY SUBSTR(PARTID,1,LENGTH(PARTID)-3),SUBSTR(RECIPE,1,5),RECIPE,  STAGE,EQPTYPE,PPID ORDER BY EQPTYPE,STAGE";



            // sql = " select A.PARTTITLE, A.PART, A.STAGE, A.RECIPE, A.PPID, A.EQPID, A.FLAG, A.TYPE, A.ACTIVEFLAG, A.FAILREASON, A.EXPIRETIME, A.CREATEUSER, A.USERDEPT, A.CREATETIME, A.REQUESTUSER  from rptprd.processconstraint A where ACTIVEFLAG='Y' and (substr(A.eqpid,1,4) in('ALCT', 'BLCT', 'ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI', 'ALTI', 'BLTI', 'ALTD', 'BLTD','ALSD','BLSD','ALCD','BLCD','ALOL','BLOL') ) order by eqpid,parttitle,part,stage";
            CsmcOracle(sql);


            tblSecond = tblFirst.Copy(); tblFirst = null;//CsmcOracle(sql),结果只进tblFirst,以下继续查询Constrain
            sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

            sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE FROM (" + sql + ")";

            CsmcOracle(sql);


            sql = "SELECT distinct PART,FULLTECH TECH FROM TBL_CONFIG WHERE FULLTECH IS NOT NULL";
            connStr = "data source=" + "Z:\\_SQLite\\R2R.db";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds); tblChart = ds.Tables[0];
                        }
                    }
                }
            }

            //tblFirst: Constrain    tblSecond:WIP   tblChart:TECE



            //   foreach (DataColumn k in tblSecond.Columns)
            //  {
            //    MessageBox.Show(k.ColumnName.ToString());
            //  MessageBox.Show(k.DataType.ToString());
            //            }
            #endregion
            //asmlWip,nikonWip,asmlPart,asmlTech,nikonPart,nikonTech
            #region

            //为WIP并入TECH
            //https://www.cnblogs.com/xuxiaona/p/4000344.html

            var query =
                from t1 in tblSecond.AsEnumerable()
                from t2 in tblChart.AsEnumerable()
                where t1.Field<string>("PART") == t2.Field<string>("PART")
                select new
                {
                    TECH = t2.Field<string>("TECH"),
                    PART = t1.Field<string>("PART"),
                    RECIPE = t1.Field<string>("RECIPE"),
                    STAGE = t1.Field<string>("STAGE"),
                    EQPTYPE = t1.Field<string>("EQPTYPE"),
                    WFRQTY = t1.Field<decimal>("WFRQTY"),
                    LOTQTY = t1.Field<decimal>("LOTQTY")
                };

            tblSecond = LINQToDataTable(query);


            //分出NikonWIP，AsmlWIP
            DataRow[] drs = tblSecond.Select("EQPTYPE = 'LDI' ");
            DataTable asmlWip;
            asmlWip = tblSecond.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                asmlWip.ImportRow(drs[i]);

            }

            drs = tblSecond.Select("EQPTYPE <> 'LDI' ");

            DataTable nikonWip;
            nikonWip = tblSecond.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                nikonWip.ImportRow(drs[i]);

            }


            //限制分类
            drs = tblFirst.Select("TYPE = '0' and EQPTYPE = 'D' ");
            DataTable asmlPart;
            asmlPart = tblFirst.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                asmlPart.ImportRow(drs[i]);

            }

            drs = tblFirst.Select("TYPE = '1' and EQPTYPE = 'D' ");
            DataTable asmlTech;
            asmlTech = tblFirst.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                asmlTech.ImportRow(drs[i]);

            }

            drs = tblFirst.Select("TYPE = '0' and EQPTYPE <> 'D' ");
            DataTable nikonPart;
            nikonPart = tblFirst.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                nikonPart.ImportRow(drs[i]);

            }

            drs = tblFirst.Select("TYPE = '1' and EQPTYPE <> 'D' ");
            DataTable nikonTech;
            nikonTech = tblFirst.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                nikonTech.ImportRow(drs[i]);

            }

            tblFirst = tblSecond = tblChart = null;
            #endregion


            //MessageBox.Show(asmlWip.Rows.Count.ToString());
            //MessageBox.Show(nikonWip.Rows.Count.ToString());
            //MessageBox.Show(asmlPart.Rows.Count.ToString());
            //MessageBox.Show(asmlTech.Rows.Count.ToString());
            //MessageBox.Show(nikonPart.Rows.Count.ToString());
            //MessageBox.Show(nikonTech.Rows.Count.ToString());

            //比较的准备工作
            string[] asmlTools = new string[] { "ALDI02", "ALDI03", "ALDI05", "ALDI06", "ALDI07", "ALDI09", "ALDI10", "ALDI11", "ALDI12", "BLDI08", "BLDI13" };
            string[] nikonTools = new string[] { "ALII01", "ALII02", "ALII03", "ALII04", "ALII05", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14", "ALII15", "ALII16", "ALII17", "ALII18", "ALSIB6", "ALSIB7", "ALSIB8", "ALSIB9", "BLSIBK", "BLSIBL", "BLSIE1", "BLSIE2" };

           
            
     
            bool techFlag = false;
            string partStr;
            tblFirst = null; tblFirst = new DataTable();
   
            tblFirst.Columns.Add("TECH", Type.GetType("System.String"));
            tblFirst.Columns.Add("PART", Type.GetType("System.String"));
            tblFirst.Columns.Add("RECIPE", Type.GetType("System.String"));
            tblFirst.Columns.Add("STAGE", Type.GetType("System.String"));
            tblFirst.Columns.Add("EQPTYPE", Type.GetType("System.String"));
            tblFirst.Columns.Add("WFRQTY", Type.GetType("System.Double"));
            tblFirst.Columns.Add("LOTQTY", Type.GetType("System.Double"));
            tblFirst.Columns.Add("TOOL", Type.GetType("System.String"));







            //Asml 开始比较
            #region
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
                                DataRow newRow = tblFirst.NewRow();
                                newRow["TECH"] = x["TECH"].ToString();
                                newRow["PART"] = x["PART"].ToString();
                                newRow["RECIPE"] = x["RECIPE"].ToString();
                                newRow["STAGE"] = x["STAGE"].ToString();
                                newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                newRow["TOOL"] = tool;
                                tblFirst.Rows.Add(newRow);

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

            //Nikon 开始比较
            #region
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
                                    DataRow newRow = tblFirst.NewRow();
                                    newRow["TECH"] = x["TECH"].ToString();
                                    newRow["PART"] = x["PART"].ToString();
                                    newRow["RECIPE"] = x["RECIPE"].ToString();
                                    newRow["STAGE"] = x["STAGE"].ToString();
                                    newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                    newRow["WFRQTY"] = x["WFRQTY"].ToString();
                                    newRow["LOTQTY"] = x["LOTQTY"].ToString();
                                    newRow["TOOL"] = tool;
                                    tblFirst.Rows.Add(newRow);

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
            stopwatch.Stop();
 
            MessageBox.Show("用时"+ stopwatch.ElapsedMilliseconds.ToString()+" msec\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n另短流程（-RD）无法从OPAS R2R导出完整工艺代码，不判断");


        }

        private void button25_Click(object sender, EventArgs e) //refresh track recipe and ESF constraints===ReworkMove.DB
        {
           


            tblFirst = tblSecond = tblChart = null;
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            StringBuilder sqlBuilder = new StringBuilder();
            string dbTblName;
            DataTableToSQLte myTabInfo;

            //更新产品、工艺代码
            #region
            tblFirst = null; tblSecond = null;
            // if ((int)MessageBox.Show("是否需要从OPAS的CD COFIGURATION数据更新产品工艺代码?\r\n\r\n请确认已从OPAS下载最新的CSV格式数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == 6)//6 Yes 7 No
            if (MessageBox.Show("是否需要从OPAS的CD COFIGURATION数据更新产品工艺代码?\r\n\r\n请确认已从OPAS下载最新的CSV格式数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
               



                //读取
                string filePath;
                openFileDialog1.InitialDirectory = @"z:\_SQLite\";
                openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
                if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
                {
                    filePath = openFileDialog1.FileName.Replace("\\", "\\\\");

                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    tblFirst = OpenCsv(filePath);
                    //删除旧数据
                    dbTblName = "tbl_cdconfig";
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
                    myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
                    myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\ReworkMove.DB");
                    MessageBox.Show("CD Config Update Done");
                    tblFirst = null;
                }
                else
                { this.Cursor = System.Windows.Forms.Cursors.WaitCursor; }//MessageBox.Show("未选择文件，数据未更新");





            }


            #endregion

           // MessageBox.Show("继续实时更新所有TrackRecipe和ESF限制");
         

            //ESF
            #region
            sql = " select PARTTITLE TECH, PART, STAGE, RECIPE, PPID, EQPID, FLAG Yes1No0, TYPE 反向1正向0, ACTIVEFLAG, FAILREASON, EXPIRETIME, CREATEUSER, USERDEPT, CREATETIME, REQUESTUSER  from rptprd.processconstraint A where ACTIVEFLAG='Y' and (substr(eqpid,1,4) in('ALCT', 'BLCT', 'ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI', 'ALTI', 'BLTI', 'ALTD', 'BLTD','ALSD','BLSD','ALCD','BLCD','ALOL','BLOL') ) order by eqpid,parttitle,part,stage";

            


            CsmcOracle(sql);
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
            myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\ReworkMove.DB");
            #endregion
            //ESF for CHECk
            #region
            sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE,FAILREASON from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";

            sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE,FAILREASON FROM (" + sql + ")";

            CsmcOracle(sql);
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
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
            myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\ReworkMove.DB");
            #endregion

            
            //光刻WIP SQL，DengSB
            #region
            

            sqlBuilder.Append(" select * from(select b.stage_location LOC ,nvl(d.processtype_master,'F2OTHER')  TECH,c.PROCESSTYPE,a.partname part,c.VFLAG,a.lotid,");
            sqlBuilder.Append("  a.PRIORITY P,a.qty Q,a.status,decode(a.lottype,'ET','FAB','EP','FAB','CUST') CUST ,a.lottype LT,");
            sqlBuilder.Append(" a.stage,a.eqptype,a.eqpid,a.ppid,round((sysdate-a.stateentrytime)*24,1) DUR,");
            sqlBuilder.Append(" c.tot_layers TL,c.remain_layers RL,c.remain_stages RS,");
            sqlBuilder.Append(" c.starttime WS ,");
            sqlBuilder.Append(" c.reqdtime CVP,");
            sqlBuilder.Append(" c.foredate FST,");
            sqlBuilder.Append(" a.dept1||a.owner owner,");
            sqlBuilder.Append(" a.HOLDCOMMENT");
            sqlBuilder.Append(" from rptprd.sdb_view_info_wip a,");
            sqlBuilder.Append(" rptprd.FAB2_RPT_MFG_STAGE_MAINTAIN b,");
            sqlBuilder.Append(" rptprd.pc_wip_forecast C,");
            sqlBuilder.Append(" rptprd.pc_processtype_master d");
            sqlBuilder.Append(" where substr(a.lottype,1,1) in ('M','N','P','E')");
            sqlBuilder.Append(" and a.lottype!='MW'");
            sqlBuilder.Append(" and a.STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')");
            sqlBuilder.Append(" and a.LOCATION Not like '%BANK%'");
            sqlBuilder.Append(" and a.location='PHOTO'");
            sqlBuilder.Append(" and a.stage like '%PH'");
            sqlBuilder.Append(" and a.eqptype in ('LDI','LSI','LII')");
            sqlBuilder.Append(" and a.status <> 'RUN'");

            sqlBuilder.Append(" and a.stage=b.stageid(+)");
            sqlBuilder.Append(" and a.lotid=C.lotID(+)");
            sqlBuilder.Append(" and a.technology=d.processtype(+))");



            sql = sqlBuilder.ToString();

            #endregion
            //RECIPE，STAGE，EQPTYPE，PPID，PART
            #region
            //sqlBuilder.Remove(0,sqlBuilder.Length);
            sqlBuilder = new StringBuilder();


            //sql = " SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE  SUBSTR(PPID,1,INSTR(PPID,';')-1) IS NOT NULL AND  EQPTYPE IN ('LDI','LII','LSI') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1') AND ROWNUM < 1000 ORDER BY PART"; //应该和去掉is not null，‘LSI'等价，少7行数据

            //sql = " SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE    EQPTYPE IN ('LDI','LII') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1')  ";//AND ROWNUM < 1000 ORDER BY PART"; //







            //sql = " SELECT SUBSTR(RECPID,1,INSTR(RECPID,'.')-1) RECIPE,EQPTYPE,STAGE, SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,PPID  FROM RPTPRD.MFG_VIEW_FLOW  WHERE   EQPTYPE='LCT'       AND SUBSTR(PARTID,1,1) NOT IN  ('8','2','1') AND ROWNUM < 1000 ";

            //sql = " SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE  SUBSTR(PPID,1,INSTR(PPID,';')-1) IS  NULL AND  EQPTYPE IN ('LDI','LII','LSI') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1') AND ROWNUM < 1000 ORDER BY PART";


            //sql = " SELECT A.RECIPE,A.EQPTYPE,A.STAGE,A.PART,B.PPID AS TRACK,A.LAYER FROM (SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE  SUBSTR(PPID,1,INSTR(PPID,';')-1) IS  NULL AND  EQPTYPE IN ('LDI','LII','LSI') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1') AND ROWNUM < 1000 ORDER BY PART) A,(SELECT SUBSTR(RECPID,1,INSTR(RECPID,'.')-1) RECIPE,EQPTYPE,STAGE, SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,PPID  FROM RPTPRD.MFG_VIEW_FLOW  WHERE   EQPTYPE='LCT'       AND SUBSTR(PARTID,1,1) NOT IN  ('8','2','1') AND ROWNUM < 1000 ) B WHERE A.PART=B.PART AND A.STAGE=B.STAGE";

            //INLINE
            sqlBuilder.Append(" SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE  SUBSTR(PPID,1,INSTR(PPID,';')-1) IS NOT NULL AND  EQPTYPE IN ('LDI','LII','LSI') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1')  ");
            //OFFLINE
            sqlBuilder.Append(" UNION ALL ");
            sqlBuilder.Append(" SELECT A.RECIPE,A.EQPTYPE,A.STAGE,A.PART,B.PPID AS TRACK,A.LAYER FROM (SELECT SUBSTR( RECPID,1,INSTR( RECPID,'.')-1) RECIPE,EQPTYPE,STAGE,SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,SUBSTR(PPID,1,INSTR(PPID,';')-1) TRACK,SUBSTR(PPID,INSTR(PPID,'.')+1,2) LAYER FROM  RPTPRD.MFG_VIEW_FLOW  WHERE  SUBSTR(PPID,1,INSTR(PPID,';')-1) IS  NULL AND  EQPTYPE IN ('LDI','LII','LSI') AND SUBSTR(PARTID,1,1) NOT IN ('8','2','1') ORDER BY PART) A,(SELECT SUBSTR(RECPID,1,INSTR(RECPID,'.')-1) RECIPE,EQPTYPE,STAGE, SUBSTR(PARTID,1,INSTR(PARTID,'.')-1) PART,PPID  FROM RPTPRD.MFG_VIEW_FLOW  WHERE   EQPTYPE='LCT'       AND SUBSTR(PARTID,1,1) NOT IN  ('8','2','1')  ) B WHERE A.PART=B.PART AND A.STAGE=B.STAGE");
            sql = sqlBuilder.ToString();
            sql = "SELECT DISTINCT * FROM (" + sql + ") WHERE SUBSTR(TRACK,1,2) NOT IN ('A0','S0') ORDER BY PART,STAGE";
            //query
            CsmcOracle(sql);
            //清空数据
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE FROM tbl_flowtrack";
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            dbTblName = "tbl_flowtrack";
             myTabInfo = new DataTableToSQLte(tblFirst, dbTblName);
            myTabInfo.ImportToSqliteBatch(tblFirst, @"z:\_SQLite\ReworkMove.DB");
            #endregion


            MessageBox.Show("TrackRecipe和ESF限制更新完成");

            this.Cursor = System.Windows.Forms.Cursors.Default;
            

        }

        private void button28_Click(object sender, EventArgs e)//单个产品，层次查询可作业设备
        {
            MessageBox.Show("\r\nPART名尽量输入完整名字,或不输入（留空）;\r\n\r\n若指定工艺代码，只能输入前三位进行通配符匹配;");

            string tech = textBox4.Text.ToUpper().Trim();
            string part = textBox5.Text.ToUpper().Trim();
            string layer = textBox6.Text.ToUpper().Trim();
            if (tech == "TECH" || part == "PART" || layer == "LAYER") { MessageBox.Show("请正确输入工艺代码\r\n若不清楚，删除文本框所有内容"); return; }
            if (part == "" && layer == "" && tech == "") { MessageBox.Show("文本框至少选择一个输入,尽量减少通配符查询；"); return; }


            tblFirst = tblSecond = tblChart = null;
            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            StringBuilder sqlBuilder = new StringBuilder();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            //查询STAGE,RECIPE....,数据保存到  tblChart
            #region


            if (MessageBox.Show("若匹配全部工艺代码查询，选择“是（Y）”；\r\n\r\n若仅匹配前三位工艺代码,选择“否（N）”；\r\n\r\n若不指定PART，全匹配工艺代码查询较慢； ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {


                if (part == "")
                {
                    sqlBuilder.Append("SELECT DISTINCT B.FULLTECH TECH, A.RECIPE,A.STAGE,(CASE WHEN A.EQPTYPE='LDI' THEN 'D' ELSE 'I' END) EQPTYPE FROM  TBL_FLOWTRACK A,TBL_CDCONFIG B WHERE A.PART=B.PART AND B.FULLTECH<>'' AND SUBSTR(B.FULLTECH,1,3) LIKE '%" + tech + "%'");
                    sqlBuilder.Append(" AND A.LAYER LIKE '%" + layer + "%' ORDER BY EQPTYPE");
                }
                else
                {
                    sqlBuilder.Append("SELECT DISTINCT B.FULLTECH TECH,A.PART, A.RECIPE,A.STAGE,(CASE WHEN A.EQPTYPE='LDI' THEN 'D' ELSE 'I' END) EQPTYPE FROM  TBL_FLOWTRACK A,TBL_CDCONFIG B WHERE A.PART=B.PART AND B.FULLTECH<>'' AND SUBSTR(B.FULLTECH,1,3) LIKE '%" + tech + "%'");
                    sqlBuilder.Append(" AND A.LAYER LIKE '%" + layer + "%'");
                    sqlBuilder.Append(" AND A.PART LIKE '%" + part + "%'  ORDER BY EQPTYPE");
                }

            }
            else
            {
                if (part == "")
                {
                    sqlBuilder.Append("SELECT DISTINCT SUBSTR( B.FULLTECH,1,3) TECH, A.RECIPE,A.STAGE,(CASE WHEN A.EQPTYPE='LDI' THEN 'D' ELSE 'I' END) EQPTYPE FROM  TBL_FLOWTRACK A,TBL_CDCONFIG B WHERE A.PART=B.PART AND B.FULLTECH<>'' AND SUBSTR(B.FULLTECH,1,3) LIKE '%" + tech + "%'");
                    sqlBuilder.Append(" AND A.LAYER LIKE '%" + layer + "%' ORDER BY EQPTYPE");
                }
                else
                {
                    sqlBuilder.Append("SELECT DISTINCT SUBSTR(B.FULLTECH,1,3) TECH,A.PART, A.RECIPE,A.STAGE,(CASE WHEN A.EQPTYPE='LDI' THEN 'D' ELSE 'I' END) EQPTYPE FROM  TBL_FLOWTRACK A,TBL_CDCONFIG B WHERE A.PART=B.PART AND B.FULLTECH<>'' AND SUBSTR(B.FULLTECH,1,3) LIKE '%" + tech + "%'");
                    sqlBuilder.Append(" AND A.LAYER LIKE '%" + layer + "%'");
                    sqlBuilder.Append(" AND A.PART LIKE '%" + part + "%'  ORDER BY EQPTYPE");
                }
            }





            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = sqlBuilder.ToString();
                MessageBox.Show(sql);
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tblChart = ds.Tables[0];//查询结果部分带Part列，部分不带
                        }
                    }
                }
            }

            if (tblChart.Rows.Count == 0)
            { MessageBox.Show("未查寻到相应产品的工艺代码；\r\n\r\n请确认产品名正确；\r\n\r\n否则刷新相关离线数据;\r\n\r\n退出"); return; }
            #endregion
            //导出工艺限制,esfTech反向，esfPart正向
            #region
            DataTable esfTech, esfPart;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT * FROM TBL_ESF1 WHERE TYPE='1';SELECT * FROM TBL_ESF1 WHERE TYPE='0'";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            esfTech = ds.Tables[0];
                            esfPart = ds.Tables[1];
                        }
                    }
                }
            }

            #endregion

            //===比较====
            //定义tblFirst Column
            #region
            tblFirst = null; tblFirst = new DataTable();

            tblFirst.Columns.Add("TECH", Type.GetType("System.String"));
            tblFirst.Columns.Add("PART", Type.GetType("System.String"));
            tblFirst.Columns.Add("RECIPE", Type.GetType("System.String"));
            tblFirst.Columns.Add("STAGE", Type.GetType("System.String"));
            tblFirst.Columns.Add("EQPTYPE", Type.GetType("System.String"));
            tblFirst.Columns.Add("EQPID", Type.GetType("System.String"));
            tblFirst.Columns.Add("FAILREASON", Type.GetType("System.String"));
            tblFirst.Columns.Add("TYPE", Type.GetType("System.String"));

            tblFirst.Columns.Add("_TECH", Type.GetType("System.String"));
            tblFirst.Columns.Add("_PART", Type.GetType("System.String"));
            tblFirst.Columns.Add("_RECIPE", Type.GetType("System.String"));
            tblFirst.Columns.Add("_STAGE", Type.GetType("System.String"));
            #endregion

            foreach (DataRow x in tblChart.Rows)
            {

                //反向
                #region
                foreach (DataRow y in esfTech.Rows)
                {

                    if (x["EQPTYPE"].ToString() == y["EQPTYPE"].ToString())
                    {
                        if (y["TECH"].ToString() == "^$" || Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                        {
                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                            {
                                if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                {
                                    if (y["PART"].ToString() == "^$" || (part == "" && (y["PART"].ToString() == "^$" || y["PART"].ToString() == "^\\S*$")) || (part != "" && Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString())))
                                    {

                                        //反向限制

                                        DataRow newRow = tblFirst.NewRow();
                                        newRow["TECH"] = x["TECH"].ToString();
                                        if (part == "")
                                        { newRow["PART"] = ""; }
                                        else
                                        {
                                            newRow["PART"] = x["PART"].ToString();
                                        }
                                        newRow["RECIPE"] = x["RECIPE"].ToString();
                                        newRow["STAGE"] = x["STAGE"].ToString();
                                        newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                        newRow["EQPID"] = y["EQPID"].ToString();
                                        newRow["FAILREASON"] = y["FAILREASON"].ToString();
                                        newRow["TYPE"] = "反向";

                                        sql = y["TECH"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_TECH"] = sql;
                                        sql = y["PART"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_PART"] = sql;
                                        sql = y["RECIPE"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_RECIPE"] = sql;
                                        sql = y["STAGE"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_STAGE"] = sql;



                                        tblFirst.Rows.Add(newRow);
                                    }

                                }
                            }
                        }
                    }



                }
                #endregion
                //正向
                #region
                foreach (DataRow y in esfPart.Rows)
                {
                //    if ((x["STAGE"].ToString() == "TM-PH") && (y["STAGE"].ToString() == "^TM-PH$") && (y["EQPID"].ToString() == "ALDI10") && (y["PART"].ToString() == "^AM0037\\S*$"))
                    
                  //  {
                    //    MessageBox.Show(x["TECH"].ToString() + "," + x["PART"].ToString() + "," + x["STAGE"].ToString() + "," + x["RECIPE"].ToString()+","+x["EQPTYPE"].ToString());
                      //  MessageBox.Show(y["TECH"].ToString() + "," + y["PART"].ToString() + "," + y["STAGE"].ToString() + "," + y["RECIPE"].ToString() + "," + y["EQPTYPE"].ToString());
                        //MessageBox.Show(Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()).ToString());
                       // MessageBox.Show((
                         //               y["PART"].ToString() == "^$" ||
                           //             (part == "" && (y["PART"].ToString() == "^$" || y["PART"].ToString() == "^\\S*$")) ||
                             //           (part != "" && Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                               //         ).ToString());
                    //}




                    if (x["EQPTYPE"].ToString() == y["EQPTYPE"].ToString())
                    {
                        if (y["TECH"].ToString()=="^$"||Regex.IsMatch(x["TECH"].ToString(), y["TECH"].ToString()))
                        {
                            if (y["RECIPE"].ToString() == "^$" || Regex.IsMatch(x["RECIPE"].ToString(), y["RECIPE"].ToString()))
                            {
                                if (y["STAGE"].ToString() == "^$" || Regex.IsMatch(x["STAGE"].ToString(), y["STAGE"].ToString()))
                                {

                                    if (
                                        y["PART"].ToString() == "^$" || 
                                        (part == "" && (y["PART"].ToString() == "^$" || y["PART"].ToString() == "^\\S*$")) ||
                                        (part != "" && Regex.IsMatch(x["PART"].ToString(), y["PART"].ToString()))
                                        )
                                    {
                                     
                                        //正向限制

                                        DataRow newRow = tblFirst.NewRow();
                                        newRow["TECH"] = x["TECH"].ToString();
                                        if (part == "")
                                        { newRow["PART"] = ""; }
                                        else
                                        {
                                            newRow["PART"] = x["PART"].ToString();
                                        }
                                        newRow["RECIPE"] = x["RECIPE"].ToString();
                                        newRow["STAGE"] = x["STAGE"].ToString();
                                        newRow["EQPTYPE"] = x["EQPTYPE"].ToString();
                                        newRow["EQPID"] = y["EQPID"].ToString();
                                        newRow["FAILREASON"] = y["FAILREASON"].ToString();
                                        newRow["TYPE"] = "正向";

                                        sql = y["TECH"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_TECH"] = sql;
                                        sql = y["PART"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_PART"] = sql;
                                        sql = y["RECIPE"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_RECIPE"] = sql;
                                        sql = y["STAGE"].ToString(); sql = ((sql.Substring(1, sql.Length - 2)).Replace("\\S*", "%")).Replace("[A-Z0-9]{1}", "_");
                                        newRow["_STAGE"] = sql;

                                        tblFirst.Rows.Add(newRow);
                                    }

                                }
                            }
                        }
                    }
                }
                #endregion
            }
           
            stopwatch.Stop();
            long t = (stopwatch.ElapsedMilliseconds) / 1000;
            tblFirst.DefaultView.Sort = "TECH,PART,STAGE,RECIPE,EQPTYPE,EQPID,TYPE";
            tblFirst = tblFirst.DefaultView.ToTable();

            MessageBox.Show("离线数据查询，比较，排序用时" + t.ToString() + " 秒；\r\n\r\n点击_Display_菜单显示数据并统计\r\n\r\n数据合计 " + tblFirst.Rows.Count.ToString() + " 行");




        }

        #endregion



        #region//======菜单项，辅助项
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        // https://blog.csdn.net/shan1774965666/article/details/98483275
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;



            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others    will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
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
            tblFirst = tblSecond = null; sql = connStr = null;
            GC.Collect();

        }
        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("选择远程源文件");
            string src, dest;

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
       
       

        private void button7_Click(object sender, EventArgs e)
        {
            if (flag > 0)
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
            MessageBox.Show("Nikon预对位数据路径：\\\\10.4.72.74\\asml\\_SQLite\\NikonEgaPara.db\n\nAsmlBatchReport数据路径：\\\\10.4.72.74\\asml\\_SQLite\\AsmlBatchreport.db\n\nR2R OVL_CD数据路径：\n\n\\\\10.4.72.74\\asml\\_SQlite\\R2R.db\n\n\n\n务必将\\\\10.4.72.74\\asml映射为Z盘");
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
               // this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                MainTrigger1(tblFirst);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DB Path: //10.4.72.74/_SQLite/ReworkMove.DB\n\nTable Name:tbl_layer__FEOL/BEOL定义\n\nTable Name:tbl_rework__返工数据\n\nTable Name:tbl_move(仅每日汇总数据，无细节）__MOVE数据\n\n数据更新需权限访问MFGEXECL数据库\n\nMFG数据库中MOVE数据不足一年，2019-1/2月份数据缺失，3月不完整");
        }
        public DataTable OpenCsv(string filePath)
        {
            //https://www.cnblogs.com/trueideal/archive/2010/03/05/1679351.html
            // https://www.cnblogs.com/allen0118/p/7217941.html 
            // Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }
            sr.Close();
            fs.Close();
            return dt;
        }


        #endregion


        #region //菜单项
       private void DisplayTable(DataTable dt)
        {
            if (flag == 0)
            {

                ChartForm chartForm = new ChartForm();
                chartForm.Owner = this;
                chartForm.Show();
                chartForm.ChildTrigger += ShowMain;
                MainTrigger1(dt);

                flag += 1;
            }
            else
            {
                // this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                MainTrigger1(dt);
            }
        }
        private void button27_Click(object sender, EventArgs e)//表格显示数据
        {
           
            DisplayTable(tblFirst);
            /*
            MessageBox.Show(Regex.IsMatch("C1813D5S0014", "^[A-Z0-9]{1}1\\S*$").ToString());
            MessageBox.Show(("^$"=="^$").ToString());
            MessageBox.Show(Regex.IsMatch("LDIGT", "^LDI\\S*$").ToString());
            MessageBox.Show(Regex.IsMatch("PO1-PH", "^PO1-PH$").ToString());



            connStr = "data source=" + @"z:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT * FROM TBL_FLOWTRACK ORDER BY PART,STAGE LIMIT 0,1000";
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
            */
        }
        private void 数据保存位置说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nikon预对位数据路径：\\\\10.4.72.74\\asml\\_SQLite\\NikonEgaPara.db\n\nAsmlBatchReport数据路径：\\\\10.4.72.74\\asml\\_SQLite\\AsmlBatchreport.db\n\nR2R OVL_CD数据路径：\n\n\\\\10.4.72.74\\asml\\_SQlite\\R2R.db\n\n\n\n务必将\\\\10.4.72.74\\asml映射为Z盘");
        }

        private void 导出文本格式ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cDSEMToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string filepath = "";

            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
            file.InitialDirectory = @"\\10.4.72.74\litho\hitachi\check";


            file.Filter = "CSV文档(*.csv)|*.csv";//|文档(*.xls)|**.xls";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                tblFirst = ClassToCall.OpenCSV(filepath);
                DisplayTable(tblFirst);

            }


            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void nikonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = "";

            OpenFileDialog file = new OpenFileDialog(); //导入本地文件  
            file.InitialDirectory = @"Z:\_DailyCheck\NikonRecipe\";


            file.Filter = "CSV文档(*.csv)|*.csv";//|文档(*.xls)|**.xls";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filepath = file.FileName;
                tblFirst = ClassToCall.OpenCSV(filepath);
                DisplayTable(tblFirst);
                
            }


            if (file.FileName.Length == 0)//判断有没有文件导入  
            {
                MessageBox.Show("请选择要导入的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void asml曝光文件更改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("待完成");
        }

        private void nikonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //读取现有数据库中的最新数据
            MessageBox.Show("TO BE COMPLETED");
            connStr = "data source=" + @"z:\_SQLite\NikonEgaPara.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                //sql = "SELECT MM,WW,DAY,TOOL,STAGE,CAST(LOTQTY AS double) as LOTQTY,CAST(WFRQTY AS double) as WFRQTY FROM tbl_move";

                sql = " SELECT * FROM TBL_INDEX LIMIT 0,1";
                
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

            DisplayTable(tblFirst);















            //将10.4.3.130的NikonEgaLog数据导入
            /*
            string y;
            tblFirst = OpenCsv(@"\\10.4.3.130\ftpdata\litho\excelcsvfile\nikonega\index.csv");
            tblFirst.Columns.Add("IntNo", Type.GetType("System.Int64"));
            foreach(DataRow x in tblFirst.Rows)
            {
                y = x["No"].ToString();
                y = y.Substring(2, y.Length - 2);
                x["IntNo"] = Convert.ToInt32(y);
            }


            DataRow[] drs = tblFirst.Select("IntNo>278000");
            tblSecond = tblFirst.Clone();
            for (int i = 0; i < drs.Length; i++)
            {
                tblSecond.ImportRow(drs[i]);
            }


             DisplayTable(tblSecond);
            */
        }

        private void asmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //读取现有数据库中的最新数据
            MessageBox.Show("TO BE COMPLETED");
        }

        private void 自动复制数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("自动将数据复制到本机 D:\\TEMP\\DB 目录");
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"\\10.4.72.74\asml\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB", true);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void 复制数据至本地硬盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("选择远程源文件");
            string src, dest;

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
        #endregion

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