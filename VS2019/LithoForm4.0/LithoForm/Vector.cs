using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
//using System.IO;
//using System.Windows.Forms;
using MathNet.Numerics.LinearRegression;
using System.IO;
using System.CodeDom;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace LithoForm
{
    class Vector
    {
        #region old

        public static void PlotNikonVector(List<string> list, DataTable dtShow, float zoom)
        {

            float x0, y0, x1, y1;

            //开始作图
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap =
               new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true);
            Pen arrowPen = new Pen(Color.Blue, 4);
            //arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            arrowPen.CustomEndCap = lineCap; //画标尺用

            float z = 5f;

            Bitmap bmp = new Bitmap(1000, 800);
            Graphics gs = Graphics.FromImage(bmp);
            System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile("C:\\temp\\Vector.emf", gs.GetHdc());
            Graphics wfrmap = Graphics.FromImage(mf);
            Pen pen2 = new Pen(Color.Green, 2);
            wfrmap.DrawEllipse(pen2, 0, 0, z * 200, z * 200);
            //pen2.DashStyle = DashStyle.Dash;
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 0, z * 100, z * 200, z * 100);
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 100, z * 0, z * 100, z * 200);
            // wfrmap.DrawLine(arrowPen, z * 100, z * 200, z * 200, z * 200); //100nm标出

            wfrmap.DrawLine(arrowPen, z * 100, z * 200, z * (100 + 100 * zoom), z * 200);//100nm标出

            wfrmap.DrawString("WQ:1/Vector:100nm", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * 105, z * 205);
            wfrmap.DrawEllipse(new Pen(Color.Blue, 1), 0, 0, z * 200, z * 200);

            Pen pen1 = new Pen(Color.Red, 1);
            // pen1.DashStyle = DashStyle.Dash;




            foreach (var row in list)
            {

                DataRow[] d = dtShow.Select("wfr='" + row + "'");
                foreach (var item in d)
                {
                    x0 = (float)Convert.ToDouble(item["x0"].ToString());

                    y0 = (float)Convert.ToDouble(item["y0"].ToString());
                    x1 = (float)Convert.ToDouble(item["x"].ToString());
                    y1 = (float)Convert.ToDouble(item["y"].ToString());
                    //   wfrmap.DrawLine(pen1, z * x0, z * y0, z * x1, z * y1);
                    wfrmap.DrawLine(pen1, z * x0, z * y0, z * ((x1 - x0) * zoom + x0), z * ((y1 - y0) * zoom + y0));


                }
            }


            wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();

        }
        public static void PlotAsmlVector(List<string> list, DataTable dtShow, float zoom)
        {

            float x0, y0, x1, y1;

            //开始作图
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap =
               new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true);
            Pen arrowPen = new Pen(Color.Blue, 4);
            //arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            arrowPen.CustomEndCap = lineCap; //画标尺用

            float z = 5f;

            Bitmap bmp = new Bitmap(1000, 800);
            Graphics gs = Graphics.FromImage(bmp);
            System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile("C:\\temp\\Vector.emf", gs.GetHdc());
            Graphics wfrmap = Graphics.FromImage(mf);
            Pen pen2 = new Pen(Color.Green, 2);
            wfrmap.DrawEllipse(pen2, 0, 0, z * 200, z * 200);
            //pen2.DashStyle = DashStyle.Dash;
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 0, z * 100, z * 200, z * 100);
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 100, z * 0, z * 100, z * 200);
            // wfrmap.DrawLine(arrowPen, z * 100, z * 200, z * 200, z * 200); //100nm标出

            wfrmap.DrawLine(arrowPen, z * 100, z * 200, z * (100 + 100 * zoom), z * 200);//100nm标出

            wfrmap.DrawString("WQ:1/Vector:100nm", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * 105, z * 205);
            wfrmap.DrawEllipse(new Pen(Color.Blue, 1), 0, 0, z * 200, z * 200);

            Pen pen1 = new Pen(Color.Red, 1);
            // pen1.DashStyle = DashStyle.Dash;




            foreach (var row in list)
            {

                DataRow[] d = dtShow.Select("WaferNr='" + row + "'");
                foreach (var item in d)
                {
                    x0 = (float)Convert.ToDouble(item["x0"].ToString());

                    y0 = (float)Convert.ToDouble(item["y0"].ToString());
                    x1 = (float)Convert.ToDouble(item["x"].ToString());
                    y1 = (float)Convert.ToDouble(item["y"].ToString());
                    //   wfrmap.DrawLine(pen1, z * x0, z * y0, z * x1, z * y1);
                    wfrmap.DrawLine(pen1, z * (x0 + 100), z * (y0 + 100), z * ((x1 - x0) * zoom + x0 + 100), z * ((y1 - y0) * zoom + y0 + 100));


                }
            }


            wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();

        }
        public static void PlotAsmlVectorNew(List<string> list, List<string> list1, List<string> list2, List<DataTable> listDatatable, float zoom, float zoom1)
        {
            //list:wfrid
            //list1: vector type,r5_measured,r5_residual,etc;
            //list2: wq,mcc,deltashift
            //listDatatable [0]vector data [1]wq,mcc,delta->X,[2]wq,mcc,delta->Y

            float x0, y0, x1, y1;

            DataTable dtShow = listDatatable[0];
            DataTable dt1 = listDatatable[1];//WQ,MCC,DELTA X
            DataTable dt2 = listDatatable[2];//WQ MCC,DELTA Y




            //开始作图
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap =
               new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true);
            Pen arrowPen = new Pen(Color.Blue, 4);
            //arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            arrowPen.CustomEndCap = lineCap; //画标尺用

            float z = 10f;

            Bitmap bmp = new Bitmap(1000, 800);
            Graphics gs = Graphics.FromImage(bmp);
            System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile("C:\\temp\\Vector.emf", gs.GetHdc());
            Graphics wfrmap = Graphics.FromImage(mf);
            //Pen pen2 = new Pen(Color.Green, 2);
            //wfrmap.DrawEllipse(pen2, 0, 0, z * 200, z * 200);
            //pen2.DashStyle = DashStyle.Dash;
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 0, z * 100, z * 200, z * 100);
            wfrmap.DrawLine(new Pen(Color.Blue, 1), z * 100, z * 0, z * 100, z * 200);
            wfrmap.DrawEllipse(new Pen(Color.Blue, 1), 0, 0, z * 200, z * 200);




            Pen penRed = new Pen(Color.Red, 1);
            Pen penGreen = new Pen(Color.Green, 1);
            Pen penSm = new Pen(Color.Blue, 1);
            Pen penRedResidual = new Pen(Color.DarkViolet, 1);
            //  penRedResidual.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Pen penGreenResidual = new Pen(Color.DarkMagenta, 1);
            //   penGreenResidual.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Pen penSmResidual = new Pen(Color.DarkOrchid, 1);
            //  penSmResidual.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            Dictionary<string, Pen> pen = new Dictionary<string, Pen>() { { "R5_Measured", penRed }, { "R5_Residual", penRedResidual }, { "G5_Measured", penGreen }, { "G5_Residual", penGreenResidual }, { "Sm_Measured", penSm }, { "Sm_Residual", penSmResidual } };

            //做矢量图
            foreach (string vectorType in list1)
            {

                foreach (string wfrid in list)
                {

                    DataRow[] d = dtShow.Select("WaferNr='" + wfrid + "' AND type='" + vectorType + "'");
                    foreach (var item in d)
                    {
                        x0 = (float)Convert.ToDouble(item["x0"].ToString());

                        y0 = (float)Convert.ToDouble(item["y0"].ToString());
                        x1 = (float)Convert.ToDouble(item["x"].ToString());
                        y1 = (float)Convert.ToDouble(item["y"].ToString());
                        //   wfrmap.DrawLine(pen1, z * x0, z * y0, z * x1, z * y1);
                        // wfrmap.DrawLine(pen[vectorType], z * (x0 + 100), z * (y0 + 100), z * ((x1 - x0) * zoom + x0 + 100), z * ((y1 - y0) * zoom + y0 + 100));
                        wfrmap.DrawLine(pen[vectorType], z * (x0 + 100), z * (-y0 + 100), z * ((x1 - x0) * zoom + x0 + 100), z * (-(y1 - y0) * zoom - y0 + 100));



                    }
                }
            }

            //WQ作图

            foreach (string wfrid in list)
            {
                foreach (string wmd in list2)
                {
                    if (wmd.Substring(0, 1).ToString() == "X")
                    {
                        DataRow[] d = dt1.Select("WaferNr='" + wfrid + "'");
                        foreach (var item in d)
                        {
                            //WQ Qualit
                            if (wmd.Contains("WQ"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());

                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0) //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                { x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString()); }
                                //x1 = System.Math.Abs(x1);
                                if (wmd.Contains("Green"))
                                {
                                    wfrmap.DrawEllipse(new Pen(Color.Green, 1), z * (x0 + 100 - x1 * zoom1 / 2), z * (100 - y0 - x1 * zoom1 / 2), z * x1 * zoom1, z * x1 * zoom1);



                                }
                                else
                                {
                                    wfrmap.DrawEllipse(new Pen(Color.Red, 1), z * (x0 + 100 - x1 * zoom1 / 2), z * (100 - y0 - x1 * zoom1 / 2), z * (x1 * zoom1), z * (x1 * zoom1));

                                }


                            }
                            //delta shift
                            if (wmd.Contains("Delta"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());

                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0) //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                {
                                    x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString());
                                    x1 = System.Math.Abs(x1 * 100000000);
                                }
                                // MessageBox.Show(x1.ToString() + "  X");
                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawRectangle(new Pen(Color.Green, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                                else
                                {
                                    wfrmap.DrawRectangle(new Pen(Color.Red, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                            }
                            //MCC
                            if (wmd.Contains("MCC"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());

                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0) //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                {
                                    x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString());
                                    x1 = (1 - x1) * 10000;
                                }
                                // MessageBox.Show(x1.ToString() + "  X");
                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawRectangle(new Pen(Color.Green, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                                else
                                {
                                    wfrmap.DrawRectangle(new Pen(Color.Red, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                            }

                        }
                    }
                    else
                    {
                        DataRow[] d = dt2.Select("WaferNr='" + wfrid + "'");
                        foreach (var item in d)
                        {
                            //WQ Qualit
                            if (wmd.Contains("WQ"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());
                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0)  //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                { x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString()); }
                                //x1 = System.Math.Abs(x1);
                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawEllipse(new Pen(Color.Green, 1), z * (x0 + 100 - x1 * zoom1 / 2), z * (100 - y0 - x1 * zoom1 / 2), z * (x1 * zoom1), z * (x1 * zoom1));
                                }
                                else
                                {
                                    wfrmap.DrawEllipse(new Pen(Color.Red, 1), z * (x0 + 100 - x1 * zoom1 / 2), z * (100 - y0 - x1 * zoom1 / 2), z * (x1 * zoom1), z * (x1 * zoom1));
                                }
                            }

                            //delta
                            if (wmd.Contains("Delta"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());

                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0) //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                {
                                    x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString());
                                    x1 = System.Math.Abs(x1 * 100000000);
                                }
                                // MessageBox.Show(x1.ToString()+"   Y");

                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawRectangle(new Pen(Color.Green, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                                else
                                {
                                    wfrmap.DrawRectangle(new Pen(Color.Red, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                            }
                            //MCC
                            if (wmd.Contains("MCC"))
                            {
                                x0 = (float)Convert.ToDouble(item["x0"].ToString());
                                y0 = (float)Convert.ToDouble(item["y0"].ToString());

                                if (item[wmd.Split(new char[] { '_' })[1]].ToString().Length == 0) //部分点无测试数据，报错
                                { x1 = 0f; }
                                else
                                {
                                    x1 = (float)Convert.ToDouble(item[wmd.Split(new char[] { '_' })[1]].ToString());
                                    x1 = (1 - x1) * 10000;
                                }
                                // MessageBox.Show(x1.ToString() + "  X");
                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawRectangle(new Pen(Color.Green, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                                else
                                {
                                    wfrmap.DrawRectangle(new Pen(Color.Red, 1), z * (x0 + 100 - x1 / 2), z * (100 - y0 - x1 / 2), z * x1, z * x1);
                                }
                            }


                        }
                    }
                }


            }



            #region//reference
            //vector reference
            wfrmap.DrawLine(arrowPen, -z * 70, z * 0, z * (-70 + 100 * zoom), z * 0);//100nm标出
            wfrmap.DrawString("Vector:100nm", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, -z * 70, -z * 6);
            //WQ Reference
            wfrmap.DrawEllipse(new Pen(Color.DarkCyan, 1), z * (-70), z * 2, z * zoom1, z * zoom1);
            wfrmap.FillEllipse(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z * 2, z * zoom1, z * zoom1);
            wfrmap.DrawString("WQ=1", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, -z * 70, z * 12);
            // delta shift
            wfrmap.DrawRectangle(new Pen(Color.DarkCyan, 1), z * (-70), z * (170), z * (30), z * (30));
            wfrmap.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z * (170), z * (30), z * (30));
            wfrmap.DrawString("Delta Shift 0.3um", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * (-70), z * 201);
            // MCC
            wfrmap.DrawRectangle(new Pen(Color.DarkCyan, 1), z * (-70), z * (150), z * 10, z * 10);
            wfrmap.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z * (150), z * 10, z * 10);
            wfrmap.DrawString("(1-MCC)*10000@MCC=0.999", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * (-70), z * 161);

            wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();
            #endregion
        }
        #endregion

        ///===================================================================================================
        ///===================================================================================================
        ///===================================================================================================

        #region new
        public static double[] MultiLinearRegession(double[] Y, double[][] X)
        {
            double[] result = MultipleRegression.QR(X, Y, true);
            result = MultipleRegression.DirectMethod(X, Y, true, MathNet.Numerics.LinearRegression.DirectRegressionMethod.Svd);
            return result;

        }

        public static DataTable DtChooseColumns(ref DataTable dataSource, List<string> cols)
        {

            DataTable result = dataSource.Copy();
            foreach (DataColumn column in dataSource.Columns)
            {
                if (cols.Contains(column.ColumnName))
                { continue; }
                else
                {
                    result.Columns.Remove(result.Columns[column.ColumnName]);
                }
            }
            return result;
        }
        public static DataTable ReadAweFile(string filename)
        {
            DataTable dt = new DataTable();//raw data     
            DataRow[] drs;


            //read data-->dt and get basic data-->basic
            if (true)
            {
                string[] f = File.ReadAllLines(filename);

                foreach (string x in new string[] { "AlignmentStrategy",
"AlignmentPhase",
"MarkValid",
"WaferNr",
"IsRefWafer",
"MarkNr",
"IsRefMark",
"BasicMarkType",
"MarkVariant",
"SegmentID",
"NomPosX",
"NomPosY",
"MeasPosXRecipe",
"MeasPosYRecipe",
"1XRedValid",
"1XRedPos",
"1XRedMCC",
"1XRedWQ",
"2XRedValid",
"2XRedPos",
"2XRedMCC",
"2XRedWQ",
"3XRedValid",
"3XRedPos",
"3XRedMCC",
"3XRedWQ",
"4XRedValid",
"4XRedPos",
"4XRedMCC",
"4XRedWQ",
"5XRedValid",
"5XRedPos",
"5XRedMCC",
"5XRedWQ",
"6XRedValid",
"6XRedPos",
"6XRedMCC",
"6XRedWQ",
"7XRedValid",
"7XRedPos",
"7XRedMCC",
"7XRedWQ",
"88XRedValid",
"88XRedPos",
"88XRedMCC",
"88XRedWQ",
"1XGreenValid",
"1XGreenPos",
"1XGreenMCC",
"1XGreenWQ",
"2XGreenValid",
"2XGreenPos",
"2XGreenMCC",
"2XGreenWQ",
"3XGreenValid",
"3XGreenPos",
"3XGreenMCC",
"3XGreenWQ",
"4XGreenValid",
"4XGreenPos",
"4XGreenMCC",
"4XGreenWQ",
"5XGreenValid",
"5XGreenPos",
"5XGreenMCC",
"5XGreenWQ",
"6XGreenValid",
"6XGreenPos",
"6XGreenMCC",
"6XGreenWQ",
"7XGreenValid",
"7XGreenPos",
"7XGreenMCC",
"7XGreenWQ",
"88XGreenValid",
"88XGreenPos",
"88XGreenMCC",
"88XGreenWQ",
"1YRedValid",
"1YRedPos",
"1YRedMCC",
"1YRedWQ",
"2YRedValid",
"2YRedPos",
"2YRedMCC",
"2YRedWQ",
"3YRedValid",
"3YRedPos",
"3YRedMCC",
"3YRedWQ",
"4YRedValid",
"4YRedPos",
"4YRedMCC",
"4YRedWQ",
"5YRedValid",
"5YRedPos",
"5YRedMCC",
"5YRedWQ",
"6YRedValid",
"6YRedPos",
"6YRedMCC",
"6YRedWQ",
"7YRedValid",
"7YRedPos",
"7YRedMCC",
"7YRedWQ",
"88YRedValid",
"88YRedPos",
"88YRedMCC",
"88YRedWQ",
"1YGreenValid",
"1YGreenPos",
"1YGreenMCC",
"1YGreenWQ",
"2YGreenValid",
"2YGreenPos",
"2YGreenMCC",
"2YGreenWQ",
"3YGreenValid",
"3YGreenPos",
"3YGreenMCC",
"3YGreenWQ",
"4YGreenValid",
"4YGreenPos",
"4YGreenMCC",
"4YGreenWQ",
"5YGreenValid",
"5YGreenPos",
"5YGreenMCC",
"5YGreenWQ",
"6YGreenValid",
"6YGreenPos",
"6YGreenMCC",
"6YGreenWQ",
"7YGreenValid",
"7YGreenPos",
"7YGreenMCC",
"7YGreenWQ",
"88YGreenValid",
"88YGreenPos",
"88YGreenMCC",
"88YGreenWQ"
})
                {
                    if (x.StartsWith("Align") || x.Contains("Mark") || x.StartsWith("Is") || x.StartsWith("Seg") || x.EndsWith("Valid"))
                    { dt.Columns.Add(x, typeof(string)); }
                    else
                    { dt.Columns.Add(x, typeof(double)); }
                }

                if (int.TryParse(f[f.Length - 1].Split(new char[] { '\t' })[3], out int n))
                { }
                else
                {
                    return dt;
                }

                List<string> basic = new List<string>() { "", "", "", "", "", "" };
                bool tmpFlag = false;
                string[] tmpArr;
                foreach (string line in f)
                {

                    //basic information: riqi,shijian,part,layer,tool,lot
                    if (line.Length > 18 && line.Substring(0, 17) == "Date(YYYY/MM/DD)=") { basic[0] = line.Split(new char[] { '=' })[1]; continue; }
                    if (line.Length > 18 && line.Substring(0, 15) == "Time(HR:MM:SS)=") { basic[1] = line.Split(new char[] { '=' })[1]; continue; }
                    if (line.Length > 7 && line.Substring(0, 6) == "JobID=") { basic[2] = line.Split(new char[] { '=' })[1]; continue; }
                    if (line.Length > 8 && line.Substring(0, 8) == "LayerID=") { basic[3] = line.Split(new char[] { '=' })[1]; continue; }
                    if (line.Length > 15 && line.Substring(0, 14) == "MachineNumber=") { basic[4] = line.Split(new char[] { '=' })[1]; continue; }
                    if (line.Length > 8 && line.Substring(0, 8) == "BatchID=") { basic[5] = line.Split(new char[] { '=' })[1]; continue; }
                    //get raw data
                    if (line.Length > 19 && line.Substring(0, 17) == "AlignmentStrategy")
                    {
                        tmpFlag = true;
                        continue;
                    }
                    if (tmpFlag)
                    {
                        DataRow newRow = dt.NewRow();
                        tmpArr = line.Split(new char[] { '\t' });
                        for (int i = 0; i < tmpArr.Length; ++i)
                        { newRow[i] = tmpArr[i]; }
                        dt.Rows.Add(newRow);

                    }
                }
                f = null; tmpArr = null;
            }
      
            //delta shift
            if (true)
            {
                dt.Columns.Add("XRedDelta", typeof(double));
                dt.Columns.Add("XGreenDelta", typeof(double));
                dt.Columns.Add("YRedDelta", typeof(double));
                dt.Columns.Add("YGreenDelta", typeof(double));

                foreach (DataRow row in dt.Rows)
                {
                    try { row["XRedDelta"] = double.Parse(row["1XRedPos"].ToString()) - double.Parse(row["88XRedPos"].ToString()); }
                    catch (Exception ex) { MessageBox.Show("Error Code:" + ex.Message + "\n\n Step: DeltaShift Calculation"); }
                    try { row["XGreenDelta"] = double.Parse(row["1XGreenPos"].ToString()) - double.Parse(row["88XGreenPos"].ToString()); }
                    catch (Exception ex) { MessageBox.Show("Error Code:" + ex.Message + "\n\n Step: DeltaShift Calculation"); }
                    try { row["YRedDelta"] = double.Parse(row["1YRedPos"].ToString()) - double.Parse(row["88YRedPos"].ToString()); }
                    catch (Exception ex) { MessageBox.Show("Error Code:" + ex.Message + "\n\n Step: DeltaShift Calculation"); }
                    try { row["YGreenDelta"] = double.Parse(row["1YGreenPos"].ToString()) - double.Parse(row["88YGreenPos"].ToString()); }
                    catch (Exception ex) { MessageBox.Show("Error Code:" + ex.Message + "\n\n Step: DeltaShift Calculation"); }
                }
            }
            //smoothcolor dynamic data
            if (true)
            {
                dt.Columns.Add("5XSmPos", typeof(double));
                dt.Columns.Add("5YSmPos", typeof(double));
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        double x5Rwq = double.Parse(row["5XRedWQ"].ToString());
                        double x5Gwq = double.Parse(row["5XGreenWQ"].ToString());
                        double x5Rp = double.Parse(row["5XRedPos"].ToString());
                        double x5Gp = double.Parse(row["5XGreenPos"].ToString());
                        if (x5Rwq > 0)
                        {
                            if (x5Gwq / x5Rwq > 10)
                            {
                                row["5XSmPos"] = x5Gp;
                            }
                            else if (x5Gwq / x5Rwq < 0.1)
                            {
                                row["5XSmPos"] = x5Rp;
                            }
                            else
                            {
                                row["5XSmPos"] = x5Gp * x5Gwq / (x5Gwq + x5Rwq) + x5Rp * x5Rwq / (x5Gwq + x5Rwq);
                            }
                        }
                        else
                        {
                            row["5XSmPos"] = x5Gp;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Code: " + ex.Message + "\n\nStep: SmoothColorDynamic Position 5X Calculation");
                    }
                    try
                    {
                        double y5Rwq = double.Parse(row["5YRedWQ"].ToString());
                        double y5Gwq = double.Parse(row["5YGreenWQ"].ToString());
                        double y5Rp = double.Parse(row["5YRedPos"].ToString());
                        double y5Gp = double.Parse(row["5YGreenPos"].ToString());
                        if (y5Rwq > 0)
                        {
                            if (y5Gwq / y5Rwq > 10)
                            {
                                row["5YSmPos"] = y5Gp;
                            }
                            else if (y5Gwq / y5Rwq < 0.1)
                            {
                                row["5YSmPos"] = y5Rp;
                            }
                            else
                            {
                                row["5YSmPos"] = y5Gp * y5Gwq / (y5Gwq + y5Rwq) + y5Rp * y5Rwq / (y5Gwq + y5Rwq);
                            }
                        }
                        else
                        {
                            row["5YSmPos"] = y5Gp;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Code: " + ex.Message + "\n\nStep: SmoothColorDynamic Position 5X Calculation");
                    }
                }
            }
            //summary-->single wafer data,single wafer residual
            if (true)
            {
                foreach (string x in new string[] {"index","5XRedLinear","5XGreenLinear","5XSmLinear",
                                                           "5YRedLinear","5YGreenLinear","5YSmLinear"})
                { dt.Columns.Add(x, typeof(double)); }

                for (int i = 0; i < dt.Rows.Count; ++i) { dt.Rows[i]["index"] = i; }
                string[] paraX = new string[] { "index","NomPosX","NomPosY",
"1XRedPos",  "1XRedMCC", "1XRedWQ", "5XRedPos", "5XRedMCC", "5XRedWQ", "1XGreenPos",
                                         "1XGreenMCC", "1XGreenWQ", "5XGreenPos", "5XGreenMCC", "5XGreenWQ",
                                         "XRedDelta", "XGreenDelta", "5XSmPos","1XRedValid","1XGreenValid","5XRedValid","5XGreenValid"};
                string[] paraY = new string[] { "index","NomPosY","NomPosY",
"1YRedPos",  "1YRedMCC", "1YRedWQ", "5YRedPos", "5YRedMCC", "5YRedWQ", "1YGreenPos",
                                         "1YGreenMCC", "1YGreenWQ", "5YGreenPos", "5YGreenMCC", "5YGreenWQ",
                                         "YRedDelta", "YGreenDelta", "5YSmPos","1YRedValid","1YGreenValid","5YRedValid","5YGreenValid"};
                DataTable dt1 = dt.DefaultView.ToTable(true, "WaferNr");
                foreach (DataRow row in dt1.Rows)
                {
                    SingleWaferData(double.Parse(row[0].ToString()), ref dt);

                }
            }
            //match X，Y -->NormPosX,NormPoxY
            //匹配 XY坐标
            if (true)
            {
                DataTable dtTmp = dt.DefaultView.ToTable(true, "NomPosX", "NomPosY", "BasicMarkType");
                List<double> xx = new List<double>();
                List<double> xy = new List<double>();
                List<double> yx = new List<double>();
                List<double> yy = new List<double>();
                foreach (DataRow row in dtTmp.Rows)
                {
                    if (row[2].ToString().EndsWith("X"))
                    {
                        xx.Add(double.Parse(row[0].ToString()));
                        xy.Add(double.Parse(row[1].ToString()));
                    }
                    else
                    {
                        yx.Add(double.Parse(row[0].ToString()));
                        yy.Add(double.Parse(row[1].ToString()));
                    }
                }
                double ox = 0;
                foreach (double x1 in xx)
                {
                    foreach (double x2 in yx)
                    {
                        if (ox == 0)
                        {
                            ox = x1 - x2;
                        }
                        else
                        {
                            if (Math.Abs(x1 - x2) < Math.Abs(ox))
                            { ox = x1 - x2; }
                        }
                    }
                }
                double oy = 0;
                foreach (double y1 in xy)
                {
                    foreach (double y2 in yy)
                    {
                        if (oy == 0)
                        {
                            oy = y1 - y2;
                        }
                        else
                        {
                            if (Math.Abs(y1 - y2) < Math.Abs(oy))
                            { oy = y1 - y2; }
                        }
                    }
                }

                dt.Columns.Add("xyKey", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    if (row["BasicMarkType"].ToString().EndsWith("-Y"))
                    {
                       // row["NomPosX"] = Math.Round(double.Parse(row["NomPosX"].ToString()) + ox, 6);
                        //row["NomPosY"] = Math.Round(double.Parse(row["NomPosY"].ToString()) + oy, 6);
                       // row["xyKey"] = Math.Round(double.Parse(row["NomPosX"].ToString()) + ox, 6).ToString() + "," + Math.Round(double.Parse(row["NomPosY"].ToString()) + oy, 6).ToString();
                        row["xyKey"] = Math.Round(double.Parse(row["NomPosX"].ToString()) + ox, 6).ToString() + "," + Math.Round(double.Parse(row["NomPosY"].ToString()) + oy, 6).ToString();

                    }
                    else
                    {
                        row["xyKey"] = row["NomPosX"].ToString() + "," + row["NomPosY"].ToString();
                    }
                }
            }


            return dt;

        }

        private static void SingleWfrResidual(ref List<double[]> wfrList)
        {

            int n = wfrList.Count;
            double[] Y = new double[n];
            double[][] X = new double[n][];
            for (int i = 0; i < n; ++i)
            {
                Y[i] = wfrList[i][3];
                X[i] = new double[2] { wfrList[i][1], wfrList[i][2] };
            }
            double[] abc;
            abc = MultiLinearRegession(Y, X);

            foreach (double[] data in wfrList)
            {
                data[4] = data[3] - (abc[0] + abc[1] * data[1] + abc[2] * data[2]);
            }
            ;
        }

        private static void SingleWaferData(double wfrNo, ref DataTable dt)
        {
            List<double[]> rList = new List<double[]>();
            List<double[]> gList = new List<double[]>();
            List<double[]> smList = new List<double[]>();

            foreach (string item in new string[] { "X", "Y" })
            {
                DataRow[] drs = dt.Select("WaferNr=" + wfrNo + " and BasicMarkType like '%-" + item + "'");
                rList.Clear(); gList.Clear(); smList.Clear();
                foreach (DataRow row in drs)
                {
                    if (row["5" + item + "RedValid"].ToString() == "T")
                    { rList.Add(new double[] { double.Parse(row["index"].ToString()), double.Parse(row["NomPosX"].ToString()), double.Parse(row["NomPosY"].ToString()), double.Parse(row["5" + item + "RedPos"].ToString()), 0 }); }

                    if (row["5" + item + "GreenValid"].ToString() == "T")
                    { gList.Add(new double[] { double.Parse(row["index"].ToString()), double.Parse(row["NomPosX"].ToString()), double.Parse(row["NomPosY"].ToString()), double.Parse(row["5" + item + "GreenPos"].ToString()), 0 }); }

                    if (row["5" + item + "GreenValid"].ToString() == "T" || row["5" + item + "GreenValid"].ToString() == "T")
                    { smList.Add(new double[] { double.Parse(row["index"].ToString()), double.Parse(row["NomPosX"].ToString()), double.Parse(row["NomPosY"].ToString()), double.Parse(row["5" + item + "SmPos"].ToString()), 0 }); }

                }
                SingleWfrResidual(ref rList);
                SingleWfrResidual(ref gList);
                SingleWfrResidual(ref smList);
                foreach (double[] x in rList)
                {
                    dt.Rows[int.Parse(x[0].ToString())]["5" + item + "RedLinear"] = x[4];
                }
                foreach (double[] x in gList)
                {
                    dt.Rows[int.Parse(x[0].ToString())]["5" + item + "GreenLinear"] = x[4];
                }
                foreach (double[] x in gList)
                {
                    dt.Rows[int.Parse(x[0].ToString())]["5" + item + "SmLinear"] = x[4];
                }
            }
        }

        public static DataTable SumWqMccDeltaResidual(ref DataTable dt)
        {
            DataTable dtSum = new DataTable();
            dtSum.Columns.Add("WfrNo", typeof(int));
            dtSum.Columns.Add("AlignShotsQty", typeof(int));
            dtSum.Columns.Add("Orientation", typeof(string));
            dtSum.Columns.Add("Color", typeof(string));
            dtSum.Columns.Add("Order", typeof(string));
            dtSum.Columns.Add("Item", typeof(string));
            dtSum.Columns.Add("ValidQty", typeof(int));                      
            dtSum.Columns.Add("Mark", typeof(string));            
            dtSum.Columns.Add("Max", typeof(double));
            dtSum.Columns.Add("Min", typeof(double));
            dtSum.Columns.Add("Avg", typeof(double));
            dtSum.Columns.Add("Variance", typeof(double));
            int WfrQty = int.Parse(dt.Rows[dt.Rows.Count - 1]["WaferNr"].ToString());
            int AlignShotsQty;       
            if (dt.Rows[0]["SegmentID"].ToString().Length > 0)
            {
                AlignShotsQty = dt.Rows.Count / WfrQty / 2 / 3;
            }
            else
            {
                AlignShotsQty = dt.Rows.Count / WfrQty / 2;
            }


            Dictionary<string, string[]> myDic = new Dictionary<string, string[]> {
                {"Red1X",new string[]{"[1XRedValid]='T'","1XRedWQ","1XRedMCC","XRedDelta"} },
                {"Red1Y",new string[]{"[1YRedValid]='T'","1YRedWQ","1YRedMCC","YRedDelta"} },
                {"Green1X",new string[]{"[1XGreenValid]='T'","1XGreenWQ", "1XGreenMCC", "XGreenDelta" } },
                {"Green1Y",new string[]{"[1YGreenValid]='T'","1YGreenWQ","1YGreenMCC","YGreenDelta"} },
                {"Red5X",new string[]{"[5XRedValid]='T'","5XRedWQ","5XRedMCC","5XRedLinear"} },
                {"Red5Y",new string[]{"[5YRedValid]='T'","5YRedWQ","5YRedMCC","5YRedLinear"} },
                {"Green5X",new string[]{"[5XGreenValid]='T'","5XGreenWQ","5XGreenMCC","5XGreenLinear"} },
                {"Green5Y",new string[]{"[5YGreenValid]='T'","5YGreenWQ","5YGreenMCC","5YGreenLinear"} },
                {"Sm5X",new string[]{ "([5XGreenValid]='T' or [5XRedValid]='T') ", "5XSmLinear"} },
                {"Sm5Y",new string[]{ "([5YGreenValid]='T' or [5YRedValid]='T') ", "5YSmLinear"} },


            };
            for (int i = 1; i < WfrQty + 1; ++i)
            {
                foreach (string key in myDic.Keys)
                {
                    DataRow[] drs = dt.Select(myDic[key][0] + "and WaferNr='" + i.ToString() + "'");
             
                    for (int j = 1; j < myDic[key].Length; ++j)
                    {
                        double[] arr = new double[drs.Length];
                        int k = 0;
                        try
                        {
                            foreach (DataRow row in drs)
                            {
                                if (myDic[key][j].Contains("Delta") || myDic[key][j].Contains("Linear"))
                                {
                                    arr[k] = double.Parse(row[myDic[key][j]].ToString()) * 1000000000;
                                    ++k;
                                }
                                else
                                {
                                    arr[k] = double.Parse(row[myDic[key][j]].ToString());
                                    ++k;
                                }
                            }
                            DataRow newRow = dtSum.NewRow();
                            newRow["WfrNo"] = i;
                            newRow["AlignShotsQty"] = AlignShotsQty;
                            newRow["Mark"] = dt.Rows[0]["BasicMarkType"].ToString().Split(new char[] { '-' })[0] + "_" + dt.Rows[0]["MarkVariant"].ToString();
                            newRow["ValidQty"] = drs.Length;
                            if (key.Length == 5)
                            {
                                newRow["Color"] = "Red"; newRow["Order"] = key.Substring(3, 1); newRow["Orientation"] = key.Substring(4, 1);
                            }
                            else if (key.Length == 7)
                            {
                                newRow["Color"] = "Green"; newRow["Order"] = key.Substring(5, 1); newRow["Orientation"] = key.Substring(6, 1);
                            }
                            else
                            {
                                newRow["Color"] = "Dynamic"; newRow["Order"] = key.Substring(2, 1); newRow["Orientation"] = key.Substring(3, 1);
                            }
                            if (myDic[key][j].Contains("WQ"))
                            {
                                newRow["Item"] = "WQ";
                            }
                            else if (myDic[key][j].Contains("MCC"))
                            {
                                newRow["Item"] = "MCC";
                            }
                            else if (myDic[key][j].Contains("Delta"))
                            {
                                newRow["Item"] = "Delta";
                            }
                            else
                            {
                                newRow["Item"] = "Residual";
                            }
                            newRow["Max"] = Math.Round(arr.Max(), 4);
                            newRow["Min"] = Math.Round(arr.Min(), 4);
                            newRow["Avg"] = Math.Round(arr.Average(), 4);
                            newRow["Variance"] = Math.Round(arr.Variance(), 4);
                            dtSum.Rows.Add(newRow);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Code:" + ex.Message + "\n\nStep:Summary -->WQ,MCC,DELTA,SHITFT");
                        }
                    }
                    
                }
            }


            return dtSum;
            
        }
       

        public static DataTable  singleWfrPlot(ref DataTable dt)
        {
            List<string> waferNr = new List<string>();
            foreach (DataRow row in (dt.DefaultView.ToTable(true, new string[] { "WaferNr" })).Rows)
            {
                waferNr.Add(row[0].ToString());               
            }

            List<string> xyKey = new List<string>();
            foreach (DataRow row in (dt.DefaultView.ToTable(true, new string[] { "xyKey" })).Rows)
            {
                xyKey.Add(row[0].ToString());
            }

            DataTable t1 = new DataTable();
            t1.Columns.Add("WaferNr", typeof(string));
            t1.Columns.Add("type", typeof(string));
            t1.Columns.Add("X0", typeof(double));
            t1.Columns.Add("Y0", typeof(double));
            t1.Columns.Add("X", typeof(double));
            t1.Columns.Add("Y", typeof(double));

            Dictionary<string, string[]> myDicItem = new Dictionary<string, string[]> {
                { "R5_Measured" ,new string[]{"5XRedPos","5YRedPos"} },
                { "R5_Residual" ,new string[]{"5XRedLinear","5YRedLinear"} },
                {"G5_Measured"  ,new string[]{"5XGreenPos","5YGreenPos"} },
                {"G5_Residual" ,new string[]{"5XGreenLinear","5YGreenLinear"} },
                { "Sm_Measured" ,new string[]{"5XSmPos","5YSmPos"} },
                {"Sm_Residual",new string[]{"5XSmLinear","5YSmLinear"} }
            };



            foreach (var wfr in waferNr)
            {
                foreach (var key in xyKey)
                {
                    DataRow[] drs = dt.Select("waferNr='" + wfr + "' and xyKey='" + key + "' and (SegmentID='B' or SegmentID='')");
                    foreach (string str in new string[] { "R5_Measured", "R5_Residual", "G5_Measured", "G5_Residual", "Sm_Measured", "Sm_Residual" })
                    {
                        DataRow r = t1.NewRow();
                        r["WaferNr"] = wfr;
                        r["X0"] = double.Parse(key.Split(new char[] { ',' })[0]) * 1000;
                        r["Y0"]= double.Parse(key.Split(new char[] { ',' })[1]) * 1000;
                        r["X"] = double.Parse(drs[0][myDicItem[str][0]].ToString()) * 1000;
                        r["Y"]= double.Parse(drs[1][myDicItem[str][1]].ToString()) * 1000;
                        r["type"] = str;
                        t1.Rows.Add(r);

                    }


                }
            }
               
         




            











           

    
            return t1;
        }
        #endregion
    }
}
