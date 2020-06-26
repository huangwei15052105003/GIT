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
            // basic alignment data-->Not Needed
            /*
            if (true)
            {

                DataTable validNo = DtChooseColumns(ref dt, new List<string>() { "WaferNr", "MarkNr", "NomPosX", "NomPosY", "BasicMarkType" });
                //validNo=validNo.DefaultView.ToTable(true, new string[] { "WaferNr", "MarkNr", "NomPosX", "NomPosY", "BasicMarkType" });
                drs = validNo.Select("BasicMarkType like '%-X'");
                DataTable validX = validNo.Clone();
                foreach (DataRow row in drs) { validNo.ImportRow(row); }
                drs = validNo.Select("BasicMarkType like '%-Y'");
                DataTable validY = validNo.Clone();
                foreach (DataRow row in drs) { validNo.ImportRow(row); }
            }
            */
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
            //summary
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

        public static DataTable SumWqMccDelta(ref DataTable dt)
        {
            DataTable dtSum = new DataTable();
            dtSum.Columns.Add("WfrQty", typeof(int));
            dtSum.Columns.Add("AlignShotsQty", typeof(int));
            dtSum.Columns.Add("Type", typeof(string));
            dtSum.Columns.Add("ValidShotsQty", typeof(int));
            dtSum.Columns.Add("SuccessRatio", typeof(double));
            dtSum.Columns.Add("Order", typeof(string));
            dtSum.Columns.Add("Mark", typeof(string));
            dtSum.Columns.Add("Color", typeof(string));
            dtSum.Columns.Add("Max", typeof(double));
            dtSum.Columns.Add("Min", typeof(double));
            dtSum.Columns.Add("Avg", typeof(double));
            dtSum.Columns.Add("Sigma", typeof(double));


            int WfrQty = int.Parse(dt.Rows[dt.Rows.Count - 1]["WaferNr"].ToString());
            int AlignShotsQty;
            double SuccessRatio;
            if (dt.Rows[0]["SegmentID"].ToString().Length > 0)
            {
                AlignShotsQty = dt.Rows.Count / WfrQty / 2 / 3;
            }
            else
            {
                AlignShotsQty = dt.Rows.Count / WfrQty / 2;
            }

            foreach (string order in new string[] { "1", "5" })
            {
                foreach (string category in new string[] { "WQ", "MCC", "Delta" })
                {
                    foreach (string mark in new string[] { "X", "Y" })
                    {

                        foreach (string color in new string[] { "Red", "Green" })
                        {
                            string sKey = string.Empty;
                            string sValid = "[" + order + mark + color + "Valid]";
                            if (category == "Delta")
                            {
                                if (order == "1")
                                {
                                    sKey = mark + color + category;
                                }
                            }
                            else
                            {
                                sKey = order + mark + color + category;
                            }
                            DataRow[] drs = dt.Select(sValid + "='T'");
                            int j = drs.Length;
                            SuccessRatio = Math.Round(100.0 * j / WfrQty / AlignShotsQty, 0);
                            double[] wq = new double[j];
                            int k = 0;
                            try
                            {
                                foreach (DataRow row in drs)
                                {
                                    if (sKey.EndsWith("Delta"))
                                    {
                                        wq[k] = double.Parse(row[sKey].ToString()) * 1000000;
                                        ++k;
                                    }
                                    else
                                    {
                                        wq[k] = double.Parse(row[sKey].ToString());
                                        ++k;
                                    }
                                }
                                DataRow newRow = dtSum.NewRow();
                                newRow["WfrQty"] = WfrQty;
                                newRow["AlignShotsQty"] = AlignShotsQty;
                                newRow["Type"] = sKey;
                                newRow["ValidShotsQty"] = j;
                                newRow["SuccessRatio"] = SuccessRatio;
                                newRow["Order"] = order;
                                newRow["Mark"] = dt.Rows[0]["BasicMarkType"].ToString() + "_" + dt.Rows[0]["MarkVariant"].ToString();
                                newRow["Color"] = color;
                                newRow["Max"] = Math.Round(wq.Max(), 4);
                                newRow["Min"] = Math.Round(wq.Min(), 4);
                                newRow["Avg"] = Math.Round(wq.Average(), 4);
                                newRow["Sigma"] = Math.Round(wq.Variance(), 4);


                                dtSum.Rows.Add(newRow);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error Code:" + ex.Message + "\n\nStep:Summary -->WQ,MCC,DELTA,SHITFT");
                            }
                        }
                    }
                }



               
            }
            return dtSum;
            
        }
        public static DataTable SumResidual(ref DataTable dt)
        {
            DataTable sumDt = new DataTable();
            DataTable dtTmp = dt.DefaultView.ToTable(true, "NomPosX","NomPosY","BasicMarkType");
            List<double> xx = new List<double>();
            List<double> xy = new List<double>();
            List<double> yx = new List<double>();
            List<double> yy = new List<double>();
            foreach(DataRow row in dtTmp.Rows)
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
            foreach(double x1 in xx)
            {
                foreach(double x2 in yx)
                {
                    if (ox==0)
                    {
                        ox = x1 - x2;
                    }
                    else
                    {
                        if (Math.Abs(x1-x2)<Math.Abs(ox))
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

            foreach(DataRow row in dt.Rows)
            {
                if (row["BasicMarkType"].ToString().EndsWith("-Y"))
                {
                    row["NomPosX"] = double.Parse(row["NomPosX"].ToString()) + ox;
                    row["NomPosY"] = double.Parse(row["NomPosY"].ToString()) + oy;

                }
            }



            return dtTmp;
        }
        #endregion
    }
}
