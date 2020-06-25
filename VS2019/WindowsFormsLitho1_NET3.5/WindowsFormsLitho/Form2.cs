using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
//using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsFormsLitho
{
    public partial class ChartForm : Form
    {
        public delegate void ChildDelegate();
        public event ChildDelegate ChildTrigger;
        DataTable dt, bak;

        
    
        string mainTitle = "";
        string xTitle = "";
        string yTitle = "";
        string y1Title = "";
        string xValue = "";
        string yValue = "";
        string y1Value = "";
        string ySpec = "";


        public ChartForm()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            // this.ControlBox = false;
            // this.TopMost = false;
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Owner;
            //mainForm.MainTrigger += PlotChart;
            mainForm.MainTrigger1 += ShowData;
            mainForm.MainTrigger2 += ExportDataGridTable;
        }
        /*
        private void PlotChart(DataTable data)
        {
            if (data == null)
            {
                chart1.Titles.Clear();
                var tmpTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "无数据，请重新筛选",
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
                tmpTitle.Alignment = ContentAlignment.TopCenter;
                chart1.Titles.Add(tmpTitle);
                return;
            }
            //排序，不在SQL中完成，以后提供界面选择字段
            data.DefaultView.Sort = "tool";
            DataTable tblTmp = data.DefaultView.ToTable();
            data.Clear(); data.Merge(tblTmp); tblTmp = null;

            //Title
            chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "Nikon预对位：Lot内Rotation Range",
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);

            chart1.ChartAreas[0].AxisX.Title = "Tool";  //X轴标题
            chart1.ChartAreas[0].AxisY.Title = "LotRange of Rot";    //Y轴标题

            #region 图表样式

            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.BackSecondaryColor = System.Drawing.Color.Yellow;//设置背景的辅助颜色
            chart1.BorderlineColor = System.Drawing.Color.Yellow;//设置图像边框的颜色
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 2;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;//设置图像的边框外观样式
            chart1.BackColor = System.Drawing.Color.Yellow;//设置图表的背景颜色
            #endregion

            #region 数据样式
            this.chart1.DataSource = data;
            this.chart1.Series[0].XValueMember = "tool";//设置X轴的数据源
            this.chart1.Series[0].YValueMembers = "rotRange";//设置Y轴的数据源
            this.chart1.DataBind();

            chart1.Series[0].Color = System.Drawing.Color.Blue;//设置颜色
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;//设置图表的类型(饼状、线状等等)
            chart1.Series[0].IsValueShownAsLabel = false;//设置是否在Chart中显示坐标点值
            chart1.Series[0].BorderColor = System.Drawing.Color.Blue;//设置数据边框的颜色
            chart1.BackColor = System.Drawing.Color.Cyan;//设置图表的背景颜色
            chart1.Series[0].Color = System.Drawing.Color.Black;//设置数据的颜色
            chart1.Series[0].Name = "RotRangWithinLot";//设置数据名称
            //chart1.Series[0].ShadowOffset = 1;//设置阴影偏移量
            //chart1.Series[0].ShadowColor = System.Drawing.Color.PaleGreen;//设置阴影颜色
            #endregion

            #region 图表区域样式

            chart1.ChartAreas[0].Name = "C1";
            //chart1.ChartAreas["ChartArea1"].Name = "C1";

            chart1.ChartAreas["C1"].Position.Auto = true;//设置是否自动设置合适的图表元素
            chart1.ChartAreas["C1"].ShadowColor = System.Drawing.Color.YellowGreen;//设置图表的阴影颜色
            //chart1.ChartAreas["C1"].Position.X = 2F;//设置图表元素左上角对应的X坐标
            //chart1.ChartAreas["C1"].Position.Y = 2F;//设置图表元素左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Position.Height = 86.76062F;//设置图表元素的高度
            //chart1.ChartAreas["C1"].Position.Width = 88F;//设置图表元素的宽度



            chart1.ChartAreas["C1"].InnerPlotPosition.Auto = true;//设置是否在内部绘图区域中自动设置合适的图表元素
            //chart1.ChartAreas["C1"].InnerPlotPosition.Height = 85F;//设置图表元素内部绘图区域的高度
            //chart1.ChartAreas["C1"].InnerPlotPosition.Width = 86F;//设置图表元素内部绘图区域的宽度
            //chart1.ChartAreas["C1"].InnerPlotPosition.X = 8.3969F;//设置图表元素内部绘图区域左上角对应的X坐标
            //chart1.ChartAreas["C1"].InnerPlotPosition.Y = 3.63068F;//设置图表元素内部绘图区域左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Area3DStyle.Inclination = 10;//设置三维图表的旋转角度
            //chart1.ChartAreas["C1"].Area3DStyle.IsClustered = true;//设置条形图或柱形图的的数据系列是否为簇状
            //chart1.ChartAreas["C1"].Area3DStyle.IsRightAngleAxes = true;//设置图表区域是否使用等角投影显示
            //chart1.ChartAreas["C1"].Area3DStyle.LightStyle =  System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;//设置图表的照明类型(色调随旋转角度改变而改变，不应用照明，色调不改变)
            //chart1.ChartAreas["C1"].Area3DStyle.Perspective = 50;//设置三维图区的透视百分比
            //chart1.ChartAreas["C1"].Area3DStyle.Rotation = 60;//设置三维图表区域绕垂直轴旋转的角度
            //chart1.ChartAreas["C1"].Area3DStyle.WallWidth = 0;//设置三维图区中显示的墙的宽度
            //chart1.ChartAreas["C1"].Area3DStyle.Enable3D = true;//设置是否显示3D效果
            //chart1.ChartAreas["C1"].BackColor = System.Drawing.Color.Green;//设置图表区域的背景颜色
            //chart1.ChartAreas["C1"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.ChartAreas["C1"].BackSecondaryColor = System.Drawing.Color.White;//设置图表区域的辅助颜色
            chart1.ChartAreas["C1"].BorderColor = System.Drawing.Color.White;//设置图表区域边框颜色
            chart1.ChartAreas["C1"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)



            chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 5;//设置主网格线与次要网格线的间隔
            chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 5;//设置刻度线的间隔



            //chart1.ChartAreas["C1"].AxisX.LabelStyle.Interval = 10D;//设置标签间隔的大小            
            //chart1.ChartAreas["C1"].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置间隔大小的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置主网格线与次网格线的间隔的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置刻度线的间隔的度量单位

            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性




            //chart1.ChartAreas["C1"].AxisY.LineColor = System.Drawing.Color.DarkBlue;//设置轴的线条颜色
            //chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;//设置网格线颜色
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Snow;//设置网格线的颜色
            //chart1.ChartAreas["C1"].AxisX.LineColor = System.Drawing.Color.White;//设置X轴的线条颜色
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，


            var axisYmax = (double)data.Compute("Max(rotRange)", "true");
            if (axisYmax > 1000)
            { chart1.ChartAreas["C1"].AxisY.Maximum = 1000; }
            else
            { chart1.ChartAreas["C1"].AxisY.Maximum = axisYmax; }

            // chart1.ChartAreas["C1"].AxisY.Maximum = getmax() + 100;//设置Y轴最大值
            //chart1.ChartAreas["C1"].AxisY.Minimum = 0;//设置Y轴最小值



            #endregion

            #region 图例样式
            chart1.Legends[0].Enabled = false;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].BackColor = System.Drawing.Color.Pink;//设置图例的背景颜色
            //chart1.Legends[0].DockedToChartArea = "ChartArea1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)
            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;//设置显示图例项方式(多列一行、一列多行、多列多行)
            #endregion




            this.tabControl1.SelectedIndex = 0;

        }
        */
        private void ShowData(DataTable data1)
        {

            {
                dt = data1.Copy();
                bak = data1.Clone();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = dt;
                this.tabControl1.SelectedIndex = 1;

                GetType(data1);//将datatable中的数据类型按 string/double分开显示，用于作图
            }
        }
        private void ExportDataGridTable()
        {

            MessageBox.Show("1.需要先显示数据\n\n2.导出功能是将表格中数据导出为文本文件保存，字段以Tab分割");
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt文件(*.txt)|*.txt|所有文件(*)|*";//保存文件类型
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选定文件
            { DataTableToTxt(dataGridView1, saveFileDialog1.FileName, '\t'); }
            else
            {
                MessageBox.Show("请指定文件");
                return;
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChildTrigger();
        }

        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //从文本输入框获得参数名
            string[] paraArr;
            string tmpStr = textBox1.Text;
            if (tmpStr.Contains(",") & tmpStr.Contains("，"))
            { MessageBox.Show("同时输入了全角和半角逗号分割符，请修改"); return; }


            if (tmpStr.Contains(","))
            {
                paraArr = textBox1.Text.Split(new char[] { ',' });

            }
            else
                if (tmpStr.Contains("，"))
                {
                    paraArr = textBox1.Text.Split(new char[] { '，' });
                }
                else
                { MessageBox.Show("无逗号分割符，假设只输入一个参数，请修改"); return; }
            //判断参数名输入是否准确
            bool[] dataType = CheckPar(paraArr);
            if (dataType[0] == true)
            { MessageBox.Show("输入的参数名不正确，请修改"); return; }


            int paraCount = paraArr.Length;

            switch (paraCount)
            {
                case 2:
                    {
                        #region
                        {
                            string input1 = paraArr[0].Trim();
                            string output1 = paraArr[1].Trim();

                          
                            try
                            {
                                if (dataType[1] == true)
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1)
                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    dataSUM = m.Sum(n => n.Field<double>(output1)),
                                                    dataMAX = m.Max(n => n.Field<double>(output1)),
                                                    dataMIN = m.Min(n => n.Field<double>(output1)),
                                                    dataAVG = m.Average(n => n.Field<double>(output1)),
                                                    dataCount = m.Count()




                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }
                                else
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1)

                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    dataCount = m.Count()
                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }

                            }

                            catch (System.InvalidCastException)
                            {
                                MessageBox.Show("数据列有空值，无法统计,删除空值行或填充其它值\n\n或数据转换错误，待解决");

                            }

                            break;
                        }
                        #endregion
                    }
                case 3:
                    #region
                    {
                        string input1 = paraArr[0].Trim();
                        string input2 = paraArr[1].Trim();
                        string output1 = paraArr[2].Trim();


                        try
                        {
                            if (dataType[1] == true)
                            {
                                var query = from t in dt.AsEnumerable()
                                            group t by new
                                            {
                                                t1 = t.Field<string>(input1),
                                                t2 = t.Field<string>(input2)
                                            } into m
                                            select new
                                            {
                                                t1 = m.Key.t1,
                                                t2 = m.Key.t2,
                                                dataSUM = m.Sum(n => n.Field<double>(output1)),
                                                dataMAX = m.Max(n => n.Field<double>(output1)),
                                                dataMIN = m.Min(n => n.Field<double>(output1)),
                                                dataAVG = m.Average(n => n.Field<double>(output1)),
                                                dataCount = m.Count()




                                            };

                                DataTable dtTmp = LINQToDataTable(query);
                                dt = null;
                                dt = dtTmp.Copy();
                                dtTmp = null;
                                dataGridView1.DataSource = dt;
                            }
                            else
                            {
                                var query = from t in dt.AsEnumerable()
                                            group t by new
                                            {
                                                t1 = t.Field<string>(input1),
                                                t2 = t.Field<string>(input2)
                                            } into m
                                            select new
                                            {
                                                t1 = m.Key.t1,
                                                t2 = m.Key.t2,

                                                dataCount = m.Count()





                                            };

                                DataTable dtTmp = LINQToDataTable(query);
                                dt = null;
                                dt = dtTmp.Copy();
                                dtTmp = null;
                                dataGridView1.DataSource = dt;
                            }

                        }

                        catch (System.InvalidCastException)
                        {
                            MessageBox.Show("数据列有空值，无法统计,删除空值行或填充其它值");

                        }

                        break;
                    }
                    #endregion
                case 4:
                    {
                        #region
                        {
                            string input1 = paraArr[0].Trim();
                            string input2 = paraArr[1].Trim();
                            string input3 = paraArr[2].Trim();
                            string output1 = paraArr[3].Trim();


                            try
                            {
                                if (dataType[1] == true)
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3)
                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    dataSUM = m.Sum(n => n.Field<double>(output1)),
                                                    dataMAX = m.Max(n => n.Field<double>(output1)),
                                                    dataMIN = m.Min(n => n.Field<double>(output1)),
                                                    dataAVG = m.Average(n => n.Field<double>(output1)),
                                                    dataCount = m.Count()




                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }
                                else
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3)
                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    dataCount = m.Count()
                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }

                            }

                            catch (System.InvalidCastException)
                            {
                                MessageBox.Show("数据列有空值，无法统计,删除空值行或填充其它值");
                            }

                            break;
                        }
                        #endregion
                    }
                case 5:
                    {
                        #region
                        {
                            string input1 = paraArr[0].Trim();
                            string input2 = paraArr[1].Trim();
                            string input3 = paraArr[2].Trim();
                            string input4 = paraArr[3].Trim();
                            string output1 = paraArr[4].Trim();


                            try
                            {
                                if (dataType[1] == true)
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3),
                                                    t4 = t.Field<string>(input4)

                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    t4 = m.Key.t4,
                                                    dataSUM = m.Sum(n => n.Field<double>(output1)),
                                                    dataMAX = m.Max(n => n.Field<double>(output1)),
                                                    dataMIN = m.Min(n => n.Field<double>(output1)),
                                                    dataAVG = m.Average(n => n.Field<double>(output1)),
                                                    dataCount = m.Count()




                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }
                                else
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3),
                                                    t4 = t.Field<string>(input4)
                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    t4 = m.Key.t4,
                                                    dataCount = m.Count()
                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }

                            }

                            catch (System.InvalidCastException)
                            {
                                MessageBox.Show("数据列有空值，无法统计,删除空值行或填充其它值");
                            }

                            break;
                        }
                        #endregion
                    }
                case 6:
                    {
                        #region
                        {
                            string input1 = paraArr[0].Trim();
                            string input2 = paraArr[1].Trim();
                            string input3 = paraArr[2].Trim();
                            string input4 = paraArr[3].Trim();
                            string input5 = paraArr[4].Trim();
                            string output1 = paraArr[5].Trim();


                            try
                            {
                                if (dataType[1] == true)
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3),
                                                    t4 = t.Field<string>(input4),
                                                    t5 = t.Field<string>(input5)

                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    t4 = m.Key.t4,
                                                    t5 = m.Key.t5,
                                                    dataSUM = m.Sum(n => n.Field<double>(output1)),
                                                    dataMAX = m.Max(n => n.Field<double>(output1)),
                                                    dataMIN = m.Min(n => n.Field<double>(output1)),
                                                    dataAVG = m.Average(n => n.Field<double>(output1)),
                                                    dataCount = m.Count()




                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }
                                else
                                {
                                    var query = from t in dt.AsEnumerable()
                                                group t by new
                                                {
                                                    t1 = t.Field<string>(input1),
                                                    t2 = t.Field<string>(input2),
                                                    t3 = t.Field<string>(input3),
                                                    t4 = t.Field<string>(input4),
                                                    t5 = t.Field<string>(input5)
                                                } into m
                                                select new
                                                {
                                                    t1 = m.Key.t1,
                                                    t2 = m.Key.t2,
                                                    t3 = m.Key.t3,
                                                    t4 = m.Key.t4,
                                                    t5 = m.Key.t5,
                                                    dataCount = m.Count()
                                                };

                                    DataTable dtTmp = LINQToDataTable(query);
                                    dt = null;
                                    dt = dtTmp.Copy();
                                    dtTmp = null;
                                    dataGridView1.DataSource = dt;
                                }

                            }

                            catch (System.InvalidCastException)
                            {
                                MessageBox.Show("数据列有空值，无法统计,删除空值行或填充其它值");
                            }

                            break;
                        }
                        #endregion
                    }
                default:
                    {
                        MessageBox.Show("输入了7个以上字段，程序不支持6个以上的分组计算"); 
                        break;
                    }
            }
            listBox1.Items.Clear(); listBox2.Items.Clear();

            GetType(dt);//分类和把int/decimal转为double，datatable统计时对原int/的词mal仍报错

        }


        private void mENU1ToolStripMenuItem1_Click(object sender, EventArgs e) //执行sort
        {
            DataView dv = new DataView(dt);
            dv.Sort = textBox1.Text;
            dt = null;
            dt = dv.ToTable();
            dataGridView1.DataSource = dt;
        

           // dt.DefaultView.Sort = textBox1.Text;
           // dataGridView1.DataSource = dt;
        }
        /*
        private void 画图ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            #region 可删除
            //https://docs.microsoft.com/zh-cn/previous-versions/dd489216(v=vs.140)?redirectedfrom=MSDN
            //https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.forms.datavisualization.charting?redirectedfrom=MSDN&view=netframework-4.8
            //https://blog.csdn.net/qq_36731538/article/details/80772618
            //https://blog.csdn.net/cherry123678/article/details/47281149
            //设置图表显示样式
            //    this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            //   this.chart1.ChartAreas[0].AxisY.Maximum = 100;
            //   this.chart1.ChartAreas[0].AxisX.Interval = 5;
            //   this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //   this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;


            if (dt == null)
            {

                this.chart1.Titles.Clear();
                var tmpTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "无数据，请重新筛选",
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
                tmpTitle.Alignment = ContentAlignment.TopCenter;
                this.chart1.Titles.Add(tmpTitle);
                return;
            }

            #endregion

            #region X/Y字段选择判断
            MessageBox.Show("图表标题:" + mainTitle + "\nX轴标题:" + xTitle + "\nY轴标题:" + yTitle + "\nX轴参数:" + xValue + "\nY轴参数:" + yValue);

            string xyTmp;//最后一个逗号删除
            xyTmp = xValue.Trim();
            if (xyTmp.Substring(xyTmp.Length - 1, 1) == "," || xyTmp.Substring(xyTmp.Length - 1, 1) == "，")
            { xValue = xyTmp.Substring(0, xyTmp.Length - 1); }
            xyTmp = yValue.Trim();
            if (xyTmp.Substring(xyTmp.Length - 1, 1) == "," || xyTmp.Substring(xyTmp.Length - 1, 1) == "，")
            { yValue = xyTmp.Substring(0, xyTmp.Length - 1); }



            if (xValue.Trim().Length == 0 || yValue.Trim().Length == 0)
            { MessageBox.Show("未选择X/Y轴字段"); return; }

            bool[] dataType; string[] xArr; string[] yArr;

            if (xValue.Contains(","))
            { xArr = xValue.Split(new char[] { ',' }); }
            else if (xValue.Contains("，"))
            { xArr = xValue.Split(new char[] { '，' }); }
            else
            { xArr = xValue.Split(new char[] { ',' }); }
            dataType = CheckPar(xArr);
            if (dataType[0] == true)
            { MessageBox.Show("X轴参数名不对"); return; }

            if (yValue.Contains(","))
            { yArr = yValue.Split(new char[] { ',' }); }
            else if (yValue.Contains("，"))
            { yArr = yValue.Split(new char[] { '，' }); }
            else
            { yArr = yValue.Split(new char[] { ',' }); }
            dataType = CheckPar(yArr);
            if (dataType[0] == true)
            { MessageBox.Show("Y轴参数名不对"); return; }
            #endregion

            #region 图表样式
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.BackSecondaryColor = System.Drawing.Color.Yellow;//设置背景的辅助颜色
            chart1.BorderlineColor = System.Drawing.Color.Yellow;//设置图像边框的颜色
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 2;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;//设置图像的边框外观样式
            chart1.BackColor = System.Drawing.Color.Yellow;//设置图表的背景颜色
            #endregion

            #region 数据样式

            //定义图表区域
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart1.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器

            this.chart1.Series.Clear();
            this.chart1.DataSource = dt;


            if (yArr.Length > 0)
            {
                Series S1 = new Series();
                S1.ChartArea = "C1";
                this.chart1.Series.Add(S1);
                if (radioButton1.Checked)
                { S1.ChartType = SeriesChartType.Column; }
                else if (radioButton2.Checked)
                { S1.ChartType = SeriesChartType.Point; }
                else if (radioButton3.Checked)
                { S1.ChartType = SeriesChartType.Line; }

                S1.Color = Color.Red;
                S1.Points.Clear();
                
                S1.XValueMember = xArr[0];
                S1.YValueMembers = yArr[0];
                
                S1.XAxisType = 0;//0主轴，1副轴
                S1.Name = yArr[0];//设置数据名称
            }

            if (yArr.Length > 1)
            {
                Series S2 = new Series();
                S2.ChartArea = "C1";
                this.chart1.Series.Add(S2);
                if (radioButton1.Checked)
                { S2.ChartType = SeriesChartType.Column; }
                else if (radioButton2.Checked)
                { S2.ChartType = SeriesChartType.Point; }
                else if (radioButton3.Checked)
                { S2.ChartType = SeriesChartType.Line; }
                S2.Points.Clear();
                S2.Color = Color.Blue;
               
                S2.XValueMember = xArr[0];
                S2.YValueMembers = yArr[1];
                // S2.XAxisType = 1;// 报错？？
                S2.Name = yArr[1];
            }


            if (yArr.Length > 2)
            {
                Series S3 = new Series();
                S3.ChartArea = "C1";
                this.chart1.Series.Add(S3);
                if (radioButton1.Checked)
                { S3.ChartType = SeriesChartType.Column; }
                else if (radioButton2.Checked)
                { S3.ChartType = SeriesChartType.Point; }
                else if (radioButton3.Checked)
                { S3.ChartType = SeriesChartType.Line; }
                S3.Points.Clear();
                S3.Color = Color.Green;
                S3.XValueMember = xArr[0];
                S3.YValueMembers = yArr[2];
                // S2.XAxisType = 1;// 报错？？
                S3.Name = yArr[2];
            }

            if (yArr.Length > 3)
            {
                Series S4 = new Series();
                S4.ChartArea = "C1";
                this.chart1.Series.Add(S4);
                if (radioButton1.Checked)
                { S4.ChartType = SeriesChartType.Column; }
                else if (radioButton2.Checked)
                { S4.ChartType = SeriesChartType.Point; }
                else if (radioButton3.Checked)
                { S4.ChartType = SeriesChartType.Line; }
                S4.Points.Clear();
                S4.Color = Color.Purple;
                S4.XValueMember = xArr[0];
                S4.YValueMembers = yArr[2];
                //S4.YAxisType = Secondary;// 报错？？
                S4.Name = yArr[3];
            }
           if (xArr.Length > 1)
            {
                Series x2 = new Series();
                x2.ChartArea = "C1";
                this.chart1.Series.Add(x2);
             //   x2.XValueMember = xArr[1];
               

               // chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
               // chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
              //  chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
              //  chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
             //   chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 5;//设置主网格线与次要网格线的间隔
               // chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 5;//设置刻度线的间隔
            
           }
    
            this.chart1.DataBind();









            //绑定数据 方式https://blog.csdn.net/dannyiscoder/article/details/70225163
            //  s1.Points.AddXY(i, r.Next(20, 30));
            //s2.Points.AddXY(i, r.Next(10, 30));
            // s3.Points.AddXY(i, r.Next(20, 30));


            // （2）DataTable方式  https://blog.csdn.net/dannyiscoder/article/details/70225163

            // Series dataTable3Series = new Series("dataTable3"); 
            //dataTable3Series.Points.DataBind(dataTable3.AsEnumerable(), "日期", "日发展", "");
            //dataTable3Series.XValueType= ChartValueType.DateTime;//设置X轴类型为时间
            //dataTable3Series.ChartType = SeriesChartType.Line;  //设置Y轴为折线
            //chart1.Series.Add(dataTable3Series);//加入你的chart1




            //  chart1.Series[0].Color = System.Drawing.Color.Blue;//设置颜色
            //  chart1.Series[0].ChartType = SeriesChartType.Column;//设置图表的类型(饼状、线状等等)
            // chart1.Series[0].IsValueShownAsLabel = false;//设置是否在Chart中显示坐标点值
            // chart1.Series[0].BorderColor = System.Drawing.Color.Blue;//设置数据边框的颜色
            // chart1.BackColor = System.Drawing.Color.Cyan;//设置图表的背景颜色
            // chart1.Series[0].Color = System.Drawing.Color.Black;//设置数据的颜色
            //S1.Name = "SERIES 1 NAME";//设置数据名称
            //chart1.Series[0].ShadowOffset = 1;//设置阴影偏移量
            //chart1.Series[0].ShadowColor = System.Drawing.Color.PaleGreen;//设置阴影颜色
            #endregion
            #region 图表区域样式

            //chart1.ChartAreas[0].Name = "C1";
            //chart1.ChartAreas["ChartArea1"].Name = "C1";

            chart1.ChartAreas["C1"].Position.Auto = true;//设置是否自动设置合适的图表元素
            chart1.ChartAreas["C1"].ShadowColor = System.Drawing.Color.YellowGreen;//设置图表的阴影颜色
            //chart1.ChartAreas["C1"].Position.X = 2F;//设置图表元素左上角对应的X坐标
            //chart1.ChartAreas["C1"].Position.Y = 2F;//设置图表元素左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Position.Height = 86.76062F;//设置图表元素的高度
            //chart1.ChartAreas["C1"].Position.Width = 88F;//设置图表元素的宽度



            chart1.ChartAreas["C1"].InnerPlotPosition.Auto = true;//设置是否在内部绘图区域中自动设置合适的图表元素
            //chart1.ChartAreas["C1"].InnerPlotPosition.Height = 85F;//设置图表元素内部绘图区域的高度
            //chart1.ChartAreas["C1"].InnerPlotPosition.Width = 86F;//设置图表元素内部绘图区域的宽度
            //chart1.ChartAreas["C1"].InnerPlotPosition.X = 8.3969F;//设置图表元素内部绘图区域左上角对应的X坐标
            //chart1.ChartAreas["C1"].InnerPlotPosition.Y = 3.63068F;//设置图表元素内部绘图区域左上角对应的Y坐标
            //chart1.ChartAreas["C1"].Area3DStyle.Inclination = 10;//设置三维图表的旋转角度
            //chart1.ChartAreas["C1"].Area3DStyle.IsClustered = true;//设置条形图或柱形图的的数据系列是否为簇状
            //chart1.ChartAreas["C1"].Area3DStyle.IsRightAngleAxes = true;//设置图表区域是否使用等角投影显示
            //chart1.ChartAreas["C1"].Area3DStyle.LightStyle =  System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;//设置图表的照明类型(色调随旋转角度改变而改变，不应用照明，色调不改变)
            //chart1.ChartAreas["C1"].Area3DStyle.Perspective = 50;//设置三维图区的透视百分比
            //chart1.ChartAreas["C1"].Area3DStyle.Rotation = 60;//设置三维图表区域绕垂直轴旋转的角度
            //chart1.ChartAreas["C1"].Area3DStyle.WallWidth = 0;//设置三维图区中显示的墙的宽度
            //chart1.ChartAreas["C1"].Area3DStyle.Enable3D = true;//设置是否显示3D效果
            //chart1.ChartAreas["C1"].BackColor = System.Drawing.Color.Green;//设置图表区域的背景颜色
            //chart1.ChartAreas["C1"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
            chart1.ChartAreas["C1"].BackSecondaryColor = System.Drawing.Color.White;//设置图表区域的辅助颜色
            chart1.ChartAreas["C1"].BorderColor = System.Drawing.Color.White;//设置图表区域边框颜色
            chart1.ChartAreas["C1"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)



            chart1.ChartAreas["C1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置X轴下方的提示信息的字体属性
            chart1.ChartAreas["C1"].AxisX.LabelStyle.Format = "";//设置标签文本中的格式字符串
            chart1.ChartAreas["C1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisX.MajorGrid.Interval = 10;//设置主网格线与次要网格线的间隔
            chart1.ChartAreas["C1"].AxisX.MajorTickMark.Interval = 10;//设置刻度线的间隔


      



            //chart1.ChartAreas["C1"].AxisX.LabelStyle.Interval = 10D;//设置标签间隔的大小            
            //chart1.ChartAreas["C1"].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置间隔大小的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置主网格线与次网格线的间隔的度量单位           
            //chart1.ChartAreas["C1"].AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;//设置刻度线的间隔的度量单位

            chart1.ChartAreas["C1"].AxisY.IsLabelAutoFit = true;//设置是否自动调整轴标签
            chart1.ChartAreas["C1"].AxisY.IsStartedFromZero = true;//设置是否自动将数据值均为正值时轴的最小值设置为0，存在负数据值时，将使用数据轴最小值
            chart1.ChartAreas["C1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);//设置Y轴左侧的提示信息的字体属性




            //chart1.ChartAreas["C1"].AxisY.LineColor = System.Drawing.Color.DarkBlue;//设置轴的线条颜色
            //chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;//设置网格线颜色
            //chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Snow;//设置网格线的颜色
            //chart1.ChartAreas["C1"].AxisX.LineColor = System.Drawing.Color.White;//设置X轴的线条颜色
            chart1.ChartAreas["C1"].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，纵向的线条颜色  
            chart1.ChartAreas["C1"].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);//数据区域，


            //  var axisYmax = (double)dt.Compute("Max(rotRange)", "true");
            //  if (axisYmax > 1000)
            //  { chart1.ChartAreas["C1"].AxisY.Maximum = 1000; }
            //  else
            //  { chart1.ChartAreas["C1"].AxisY.Maximum = axisYmax; }

            if (ySpec == "")
            { }
            else
            {
                double H, L;
                try
                {
                    L = Convert.ToDouble(ySpec.Split(new char[] { ',' })[0]);
                    H = Convert.ToDouble(ySpec.Split(new char[] { ',' })[1]);
                    chart1.ChartAreas["C1"].AxisY.Maximum = H;//设置Y轴最大值
                    chart1.ChartAreas["C1"].AxisY.Minimum = L;//设置Y轴最小值
                  
                }
               catch
                {
                    MessageBox.Show("请确认输入的Y轴规范是否正确，不能输入字符");
                }
            }
            // 
            //



            #endregion
            #region Title
            //Title
            this.chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(mainTitle,
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 24F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);
            // this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            //this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            chartArea1.AxisX.Title = xTitle;  //X轴标题
            chartArea1.AxisY.Title = yTitle;    //Y轴标题
    
            #endregion
            #region 图例样式
            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].BackColor = System.Drawing.Color.Pink;//设置图例的背景颜色
            //chart1.Legends[0].DockedToChartArea = "ChartArea1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)
            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;//设置显示图例项方式(多列一行、一列多行、多列多行)
            #endregion




            this.tabControl1.SelectedIndex = 0;
        }
        */





       















        #region
        private void GetType(DataTable dt)////将datatable中的数据类型按 string/double分开显示，用于作图
        {
            #region 此段将int/decimal强制转换为double
            /* 

            //更改datatable列数据类型，int/double改为double
            DataTable newDt = new DataTable();
            if (dt.Rows.Count > 0)
            { newDt = dt.Clone(); }
            else
            { MessageBox.Show("No Data,Quit"); return; }
            //新表中的列数据类型为Decmail的改为string 
            foreach (DataColumn col in newDt.Columns) 
            {
              
                if (col.DataType.FullName == "System.Decimal"||col.DataType.FullName.Substring(0,10) == "System.Int")                                    {                                        
                    col.DataType = Type.GetType("System.Double");
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                DataRow newDtRow = newDt.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                    newDtRow[column.ColumnName] = row[column.ColumnName];
                }
                newDt.Rows.Add(newDtRow);
            }
            dt = null; dt = newDt.Copy(); newDt = null;
           
            dataGridView1.DataSource = dt;
*/
            #endregion

            //double,string类型分别导入；datetime类型不导入
            listBox1.Items.Clear(); listBox2.Items.Clear();

            foreach (DataColumn k in dt.Columns)
            {
                
                if (k.DataType.ToString() == "System.String")

                { listBox1.Items.Add(k.ColumnName.ToString()); }

                else if (k.DataType.ToString() == "System.Double")
                { listBox2.Items.Add(k.ColumnName.ToString()); }
                else
                { MessageBox.Show(k.ColumnName.ToString()+"--DateTime/Int/Decimal型字段未转换和导入"); }
       
            }

            //if (list.ToArray().Length > 0)
      





        }
        public static bool DataTableToTxt(DataGridView gridview, string fileName, char strSplit)
        {
            if (gridview == null || gridview.Rows.Count == 0)
                return false;
            string filename = fileName;


            try
            {
                if (File.Exists(filename))
                { File.Delete(filename); }
            }
            catch
            {
                MessageBox.Show("同名文件存在，且无法删除，手动删除后再导出");
                return false;
            }

            FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

            StringBuilder strBuilder = new StringBuilder();

            try
            {
                for (int i = 0; i < gridview.Rows.Count; i++)
                {
                    strBuilder = new StringBuilder();
                    for (int j = 0; j < gridview.Columns.Count; j++)
                    {
                        strBuilder.Append(gridview.Rows[i].Cells[j].Value.ToString() + strSplit);
                    }
                    //strBuilder.Remove(strBuilder.Length - 1, 1); // 将最后添加的一个strSplit删除掉
                    streamWriter.WriteLine(strBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }

            return true;
        }
        protected override void WndProc(ref Message m) //禁用关闭按钮
        {
            const int WM_SYSCOMMAND = 0x0112;//定义将要截获的消息类型
            const int SC_CLOSE = 0xF060;//定义关闭按钮对应的消息值
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SC_CLOSE))//当鼠标单击关闭按钮时
            {
                return;//直接返回，不进行处理
            }
            base.WndProc(ref m);//传递下一条消息
        }

        private void 排序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = ("参数1 ASC, 参数2 DESC, 参数3 ASC");
            MessageBox.Show("AS表示升序，DESC表示降序，参数间用半角逗号分割，最后一个参数后不能有逗号");
        }

        private void 筛选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = ("字符参数1 != 'abcd' and 字符参数2 like '%ABC%' or 数字参数 > 888");
            MessageBox.Show("字符变量后的比较字符必须带单引号；数字变量不需要；");

        }
     
        private void 删除空值行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row[textBox1.Text.Trim()] == DBNull.Value)
                {
                    row.Delete();
                }
            }
            dt.AcceptChanges();
            dataGridView1.DataSource = dt;
        }
        //通过一个公共类将LINQ数据集转换为datatable
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
    
        private bool[]  CheckPar(string[] paraArr)
        {
            bool dataType = false;
            bool dataType1 = false;
            foreach (var item in paraArr)
            {
                if (listBox1.Items.Contains(item.Trim()) || listBox2.Items.Contains(item.Trim()))
                { dataType1 = listBox2.Items.Contains(paraArr[paraArr.Length - 1]); }
                else
                { dataType = true; }
            
            }
            bool[] tmpBool=new bool[] {dataType,dataType1};
        
            return tmpBool;
        }

        private void 统计ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("点击文本输入框，依次输入分组字段和统计字段；\r\n用逗号分割；\r\n分组字段最多5个，统计字段只能设置一个；\r\n最多累计设置六个字段；\r\n分组字段只支持字符型变量，统计字段无限制；后续更改程序：分组字段支持数值字段");

        }

        private void 三合一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂不支持，需要在文本框中编写命令；请按结合排序，筛选功能完成");


        }

        private void 获取图表标题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainTitle = textBox1.Text;
        }

        private void 获取X轴标题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xTitle = textBox1.Text;
        }

        private void 获取YToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yTitle = textBox1.Text;
        }

        private void 获取YToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            y1Title = textBox1.Text;
        }

        private void 选取字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xValue = textBox1.Text;
        }

        private void 选取字段ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            yValue = textBox1.Text;
        }

        private void 选取字段ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            y1Value = textBox1.Text;
        }
        private void 获得最大最小值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("在文本框中，依次输入最小值，最大值，用半角逗号分割，热后点击本菜单选项;\n\n如果自动定义最大，最小值，清空文本框内容");
            ySpec = textBox1.Text.Trim();
        }
        private void 作图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmp = textBox1.Text;
            //tmp = tmp.Trim().Substring(0, tmp.Length - 1);

            // DataRow[] drs = dt.Select("tool='ALSD83'");
            DataRow[] drs = dt.Select(tmp);
            dt = null;
            dt = bak.Clone();//Clone 复制结构，Copy 复制结构和数据

            if (drs.Length > 0)
            {



                for (int i = 0; i < drs.Length; i++)
                {
                    dt.ImportRow(drs[i]);
                    dataGridView1.DataSource = dt;
                }
            }
            else
            { MessageBox.Show("w未筛选到数据，表格显示未变更"); }
        }

        #endregion

    
       

        
    
  

       
      

     
     

  
 

  







        //https://www.cnblogs.com/knowledgesea/p/3897665.html
        // https://www.cnblogs.com/elves/p/3944220.html
        //https://blog.csdn.net/livening/article/details/5610978
        //https://blog.csdn.net/Reforn/article/details/80117039
        //https://blog.csdn.net/Roy_cl/article/details/8904777
        // https://blog.csdn.net/duan1311/article/details/51769119
        //https://blog.csdn.net/davinciyxw/article/details/8930063
        //https://www.cnblogs.com/elves/p/3944220.html
     

       

    //   private void tabPage2_Click(object sender, EventArgs e)
      // {

     //   }

      
    

   

        

        

      

   
 

       

       


     
   

    }
}
