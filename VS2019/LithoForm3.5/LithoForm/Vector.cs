using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
//using System.IO;
//using System.Windows.Forms;
namespace LithoForm
{
    class Vector
    {

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
        public static void PlotAsmlVectorNew(List<string> list, List<string> list1, List<string> list2, List<DataTable> listDatatable, float zoom,float zoom1)
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
                                    wfrmap.DrawEllipse(new Pen(Color.Green, 1), z * (x0 + 100-x1*zoom1/2), z * (100 - y0-x1*zoom1/2), z * x1 * zoom1, z * x1 * zoom1);
                                  
                                 

                                }
                                else
                                {
                                    wfrmap.DrawEllipse(new Pen(Color.Red, 1), z * (x0 + 100 - x1* zoom1 / 2), z * (100 - y0 -x1 * zoom1 / 2), z * ( x1 * zoom1), z * ( x1 * zoom1));
                                   
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
                                    x1 =System.Math.Abs( x1 * 100000000);
                                }
                               // MessageBox.Show(x1.ToString() + "  X");
                                if (wmd.Contains("Green"))
                                {

                                    wfrmap.DrawRectangle(new Pen(Color.Green, 1), z * (x0 + 100 - x1/  2), z * (100 - y0 - x1 /2), z * x1, z * x1);
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
                                    x1 = (1-x1)*10000;
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
                               
                                    wfrmap.DrawEllipse(new Pen(Color.Green, 1), z * (x0 + 100 - x1 * zoom1 / 2), z * (100 - y0 - x1 * zoom1 / 2),  z * (x1 * zoom1), z * (x1 * zoom1));
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
            wfrmap.DrawString("Vector:100nm", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, -z *70 , -z * 6);
            //WQ Reference
            wfrmap.DrawEllipse(new Pen(Color.DarkCyan, 1), z * (-70 ), z * 2, z * zoom1, z * zoom1);
            wfrmap.FillEllipse(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z *2 , z * zoom1, z * zoom1);
            wfrmap.DrawString("WQ=1", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, -z * 70, z * 12);
            // delta shift
            wfrmap.DrawRectangle(new Pen(Color.DarkCyan, 1), z * (-70), z * (170), z * (30), z * (30));
            wfrmap.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z * (170), z * (30), z * (30));
            wfrmap.DrawString("Delta Shift 0.3um", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * (-70), z * 201);
            // MCC
            wfrmap.DrawRectangle(new Pen(Color.DarkCyan, 1), z * (-70), z * (150), z * 10, z * 10);
            wfrmap.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.DarkCyan)), z * (-70), z * (150), z * 10, z * 10);
            wfrmap.DrawString("(1-MCC)*10000@MCC=0.999", new Font("宋体", z * 5f, FontStyle.Bold), Brushes.Black, z * (-70), z*161);

            wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();
            #endregion
        }
    }
}
