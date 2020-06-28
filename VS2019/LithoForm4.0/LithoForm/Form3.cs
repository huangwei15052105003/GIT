using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LithoForm
{

    public partial class Form3 : Form
    {
        public delegate void Form3Delegate1(string[] queryArr);
        public event Form3Delegate1 Form3Event1;


        public static int choiceNo;
        public Form3()
        {
            InitializeComponent();
            this.ControlBox = false;

        }
        private void Form3_Load(object sender, EventArgs e)
        {
    
            Form1 form1 = new Form1();
            form1 = (Form1)this.Owner;
            form1.Form3Trigger += PlotChart;
            // this.FormBorderStyle = FormBorderStyle.None;
        }

        private void PlotChart(DataTable dt)
        {
            c1(dt);

        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)    //关闭form3
        {
            ActiveForm.Dispose();
           // ActiveForm.Hide();
             
            

        }

        private void c1(DataTable dt)
        {
      
                




            #region 图表样式
            chart1.BackColor = System.Drawing.Color.Cyan;//设置图表的背景颜色
            chart1.BackSecondaryColor = System.Drawing.Color.Yellow;//设置背景的辅助颜色
            chart1.BackSecondaryColor = System.Drawing.Color.Yellow;//设置背景的辅助颜色
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;//指定图表元素的渐变样式(中心向外，从左到右，从上到下等等)
                                                                                                               //chart1.BorderlineColor = System.Drawing.Color.Yellow;//设置图像边框的颜色           
                                                                                                               // chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;//设置图像边框线的样式(实线、虚线、点线)
            chart1.BorderlineWidth = 0;//设置图像的边框宽度
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;//设置图像的边框外观样式
            #endregion


            #region 图表标题
            chart1.Titles.Clear();
            var chartTitle = new System.Windows.Forms.DataVisualization.Charting.Title(
                "Nikon预对位：Lot内Rotation Range",
                System.Windows.Forms.DataVisualization.Charting.Docking.Top,
                new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold),
                System.Drawing.Color.Blue);
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chart1.Titles.Add(chartTitle);

            chart1.ChartAreas[0].AxisX.Title = "Tool";  //X轴标题
            chart1.ChartAreas[0].AxisY.Title = "LotRange of Rot";    //Y轴标题
            #endregion

            #region 数据绑定
            this.chart1.DataSource = dt;
            this.chart1.Series[0].XValueMember = "RIQI";//设置X轴的数据源
            this.chart1.Series[0].YValueMembers = "LOTRATIO";//设置Y轴的数据源
            this.chart1.DataBind();
            #endregion

            #region 数据样式
            chart1.Series[0].Color = System.Drawing.Color.Blue;//设置颜色
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;//设置图表的类型(饼状、线状等等)
            chart1.Series[0].IsValueShownAsLabel = false;//设置是否在Chart中显示坐标点值
            chart1.Series[0].BorderColor = System.Drawing.Color.Blue;//设置数据边框的颜色
            chart1.Series[0].Color = System.Drawing.Color.Black;//设置数据的颜色
            #endregion

            #region 图例样式
            chart1.Series[0].Name = "ReworkRatio";//设置数据名称
            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chart1.Legends[0].BackColor = System.Drawing.Color.Pink;//设置图例的背景颜色
            //chart1.Legends[0].DockedToChartArea = "ChartArea1";//设置图例要停靠在哪个区域上 <asp:ChartArea Name="ChartArea1"
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;//设置停靠在图表区域的位置(底部、顶部、左侧、右侧)
            chart1.Legends[0].Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);//设置图例的字体属性
            chart1.Legends[0].IsTextAutoFit = true;//设置图例文本是否可以自动调节大小
            chart1.Legends[0].LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;//设置显示图例项方式(多列一行、一列多行、多列多行)
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


            // var axisYmax = (double)data.Compute("Max(rotRange)", "true");
            //if (axisYmax > 1000)
            //{ chart1.ChartAreas["C1"].AxisY.Maximum = 1000; }
            //else
            //{ chart1.ChartAreas["C1"].AxisY.Maximum = axisYmax; }

            // chart1.ChartAreas["C1"].AxisY.Maximum = getmax() + 100;//设置Y轴最大值
            //chart1.ChartAreas["C1"].AxisY.Minimum = 0;//设置Y轴最小值



            #endregion

        }


    }
}
