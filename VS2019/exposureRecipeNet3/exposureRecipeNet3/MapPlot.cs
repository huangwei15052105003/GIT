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

using System.Drawing.Drawing2D;

namespace exposureRecipeNet3
{
   public class MapPlot
    {
        static string connStr = "data source=" + @"p:\_SQLite\Flow.DB";
        public  double wee;
        public double xImg;
         public double stepX;
        public double stepY;
        public double dieX;
        public double dieY;
        public double pricision;
        public string part;
        public string parttype;
        public double areaRatio;
        public bool autoFlag;
        public float ox, oy;


        public DataTable dt = new DataTable();
       public bool fullCover;
        
        public Graphics wfrmap;

        public float k,  sx, sy, dx, dy;


        public MapPlot()
        { }
        public MapPlot(double _wee,double _xImg,double _stepX,double _stepY,double _dieX,double _dieY,double _pricision,string _parttype,string _part,double _areaRatio,bool _autoFlag,float _ox,float _oy)
        {
            wee = _wee;xImg = _xImg;stepX = _stepX;stepY = _stepY;dieX = _dieX;dieY = _dieY;pricision = _pricision;
            parttype = _parttype;
            part = _part;
            areaRatio = _areaRatio;
            autoFlag = _autoFlag;
            ox = _ox;oy = _oy;
        }

        public void CalculatOffset()
        {

            float mOX = ox;
            float mOY = oy;


            if (parttype == "largeField")
            {
                double tmp = stepX;
                stepX = stepY;
                stepY = 2 * tmp;
                tmp = dieX;
                dieX = dieY;
                dieY = tmp;
            }



            int col1 = Convert.ToInt16(System.Math.Truncate(wee / stepX));
            int row1 = Convert.ToInt16(System.Math.Truncate(wee / stepY));
            int col2 = Convert.ToInt16(System.Math.Truncate(stepX / dieX));
            int row2 = Convert.ToInt16(System.Math.Truncate(stepY / dieY));
            int shotDie = Convert.ToInt32(stepX / dieX * stepY / dieY);
            double b1 = stepX;
            double b2 = stepY;
            double E, E1, E2, H, dH, offX, offY;
            double shiftX1_min = 88888;
            double shiftX1_max = 88888;
            double shiftX2_min = 88888;
            double shiftX2_max = 88888;
            double shiftY1_min = 88888;
            double shiftY1_max = 88888;
            double shiftY2_min = 88888;
            double shiftY2_max = 88888;

            double K1, K2, K3, D1, D2, XcellA1, XcellA2, XcellB1, XcellB2, XBmin, XBmax, XAmin, XAmax;
            int totalDie = 0, fullShot = 0, partialShot = 0, partialShotDie = 0;
            double llx, lly, sx, sy;
            Boolean f1, f2, f3, f4, f5, f6;
            List<double> l = new List<double>();
            List<double> l1 = new List<double>();
            
            dt.Columns.Add("shiftX", Type.GetType("System.Double"));
            dt.Columns.Add("shiftY", Type.GetType("System.Double"));
            dt.Columns.Add("dieQty", Type.GetType("System.Int32"));
            dt.Columns.Add("fullShot", Type.GetType("System.Int32"));
            dt.Columns.Add("partialShot", Type.GetType("System.Int32"));
            dt.Columns.Add("totalShot", Type.GetType("System.Int32"));
            dt.Columns.Add("leftPartialSize", Type.GetType("System.Double"));
            dt.Columns.Add("rightPartialSize", Type.GetType("System.Double"));
            dt.Columns.Add("miniSizeX", Type.GetType("System.Double"));
            dt.Columns.Add("leftFlag", Type.GetType("System.Boolean"));
            dt.Columns.Add("rightFlag", Type.GetType("System.Boolean"));
            dt.Columns.Add("delta", Type.GetType("System.Double"));

            //===========================================
            Boolean laserY_Flag = false;//打标区是否曝光

            List<double> l2 = new List<double>();
            double laserY, Xcell, Ycell;
            List<double> distinctList;

            laserY = (95 - stepY / 2) % stepY;
            if (laserY > stepY / 2) { laserY = laserY - stepY; }
            //===========================================




            #region calculate offset
            if (Convert.ToInt16(184 / b1 + 1) < 194 / b1)                        //'NonebyLS is unavoidable.
            {
                if (194 - Convert.ToInt16(194 / b1) * b1 + 2.5 > b1 / 2)         //'(B) NonebyLS exposable in one side when the other side field edge touch FEC
                {
                    K2 = 97 - (2 * Convert.ToInt16(194 / 2 / b1) + 1) * b1 / 2;           //# 'Field edge touch FEC
                    if (xImg > 0)
                    { XcellA1 = K2 + xImg; }                         //#'Field edge touch right FEC
                    else
                    { XcellA1 = -K2 + xImg; }                        //#'Field edge touch left FEC
                }
                else
                {
                    K3 = 100 - 0.5 - Convert.ToInt16(100 / b1 + 1 / 2) * b1;     //'Scan center touch wafer edge (99.5 mm from wafer center)
                    if (100 - b1 * Convert.ToInt16(200 / b1) / 2 < 4.25)         //#    '(D) Left and right NonebyLS => symmetric map
                    { K3 = (Convert.ToInt16(100 / b1) - Convert.ToInt16(100 / b1 - 1 / 2)) * b1 / 2; }                  //'(C)'
                    if (xImg > 0)
                    { XcellA1 = -K3 + xImg; }                            //'Scan center touch left wafer edge
                    else
                    { XcellA1 = K3 + xImg; }                             //'Scan center touch right wafer edge
                }
                shiftX1_min = XcellA1;

            }
            else                                                 //'(A)
            {
                D1 = 92 - b1 * (Convert.ToInt16(184 / b1 + 1) - 1) / 2;           //#'Reticle center touch NonebyLS
                D2 = b1 * (Convert.ToInt16(184 / b1 + 1) / 2) - 97;              // #''Field edge touch FEC
                if (D1 < D2)
                { K1 = D1; }
                else
                { K1 = D2; }

                if (Convert.ToInt16(184 / b1 + 1) - Convert.ToInt16(Convert.ToInt16(184 / b1 + 1) / 2) * 2 == 1)    //  #'Odd columns within ZbyLS
                {
                    if (xImg == 0)
                    { XcellA1 = -K1; XcellA2 = K1; }
                    else
                    { XcellA1 = xImg; XcellA2 = xImg - xImg / System.Math.Abs(xImg) * K1; }
                }
                else
                {
                    if (xImg == 0)                                                 //# 'Even columns within ZbyLS
                    {
                        XcellA1 = -b1 / 2;
                        XcellA2 = -b1 / 2 + K1;
                        XcellB1 = b1 / 2;
                        XcellB2 = b1 / 2 - K1;

                        if (XcellB1 > XcellB2)
                        { XBmin = XcellB2; XBmax = XcellB1; }
                        else
                        { XBmin = XcellB1; XBmax = XcellB2; }
                        shiftX2_min = XBmin;
                        shiftX2_max = XBmax;
                    }
                    else
                    {
                        XcellA1 = xImg - xImg / System.Math.Abs(xImg) * b1 / 2;
                        XcellA2 = xImg - xImg / System.Math.Abs(xImg) * (b1 / 2 - K1);
                    }

                    if (XcellA1 > XcellA2)
                    {
                        XAmin = XcellA2;
                        XAmax = XcellA1;
                    }
                    else
                    {
                        XAmin = XcellA1;
                        XAmax = XcellA2;
                    }
                    shiftX1_min = XAmin;
                    shiftX1_max = XAmax;
                }
            }
            #endregion
            #region //get x offset
            l = new List<double>();

            if (shiftX1_max == 88888 && shiftX2_min == 88888 && shiftX2_max == 88888)
            { l.Add(shiftX1_min); }
            else
            {
                try
                {
                    for (int i = 0; i < Convert.ToInt16((shiftX1_max - shiftX1_min) / pricision) + 1; i++)
                    { l.Add(shiftX1_min + i * pricision); }
                }
                catch
                { }
                try
                {
                    for (int i = 0; i < Convert.ToInt16((shiftX2_max - shiftX2_min) / pricision) + 1; i++)
                    { l.Add(shiftX1_min + i * pricision); }
                }
                catch
                { }
            }
            #endregion
            //_l_中有重复数据,需要去除
            distinctList = new List<double>(l.Distinct());
            l = distinctList;

            foreach (var tmpXcell in l)
            {
                Xcell = System.Math.Round(tmpXcell, 3);
                l1 = new List<double>();
                l2 = new List<double>();
                #region //get y offset
                shiftX1_max = shiftX1_min = shiftX2_max = shiftX2_min = 88888;
                E1 = (100 - Xcell + xImg) - Convert.ToInt16((92 - Xcell + xImg) / b1) * b1;       //#'Right side scan center
                E2 = (100 + Xcell - xImg) - Convert.ToInt16((92 + Xcell - xImg) / b1) * b1;       //# 'Left side scan center
                if (E1 > E2)
                { E = E2; }
                else
                { E = E1; }
                H = System.Math.Pow(97 * 97 - System.Math.Pow((100 - E), 2), 0.5);
                dH = H - 13 - 6 - 5;
                if (dH <= 0)
                {
                    shiftY1_min = -b2 / 2; shiftY2_min = b2 / 2;
                    l1.Add(shiftY1_min); l1.Add(shiftY2_min);
                }
                else
                {
                    if (dH < b2 / 2)
                    {
                        shiftY1_min = -b2 / 2;
                        shiftY1_max = -b2 / 2 + dH;
                        shiftY2_min = b2 / 2 - dH;
                        shiftY2_max = b2 / 2;
                        for (int i = 0; i < Convert.ToInt16((shiftY1_max - shiftY1_min) / pricision) + 1; i++)
                        { l1.Add(shiftY1_min + i * pricision); }
                        for (int i = 0; i < Convert.ToInt16((shiftY1_max - shiftY1_min) / pricision) + 1; i++)
                        { l1.Add(shiftY2_min + i * pricision); }
                    }
                    else
                    {
                        shiftY1_min = -b2 / 2;
                        shiftY1_max = b2 / 2;
                        for (int i = 0; i < Convert.ToInt16((shiftY1_max - shiftY1_min) / pricision) + 1; i++)
                            l1.Add(shiftY1_min + i * pricision);
                    }
                }
                //========================

                if (parttype != "largeField")
                {
                    l2 = new List<double>();

                    foreach (var i in l1)
                    {

                        if (System.Math.Abs(laserY - i) <= 0.1)
                        { l2.Add(i); }
                    }
                    if (l2.Count() > 0)
                    {
                        laserY_Flag = true;
                        l1 = new List<double>(l2);//https://www.cnblogs.com/guxin/p/csharp-list-collection-deep-copy.html

                    }
                    else
                    {
                        l1.Clear();
                    }
                    if (laserY_Flag == true && l2.Count() == 0)
                    { l1.Clear(); l1.Add(888); }
                }
                else
                {
                    // MessageBox.Show("largeFileld, no full shot under lasermark");
                    if (l1.Count == 0) { l1.Add(888); }
                }






                //=============================





                #endregion
                foreach (var tmpYcell in l1)
                {
                    Ycell = System.Math.Round(tmpYcell, 3);
                    offX = Xcell; offY = Ycell; totalDie = 0; fullShot = 0; partialShot = 0;
                    for (int i = -col1 - 1; i < col1 + 2; i++)
                    {
                        #region /calculation
                        for (int j = -row1 - 1; j < row1 + 2; j++)
                        {
                            #region//计算

                            llx = i * stepX - stepX / 2 + offX; lly = j * stepY - stepY / 2 + offY;
                            f1 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly, 2)) < System.Math.Pow(wee, 2);

                            f2 = (System.Math.Pow(llx + stepX, 2) + System.Math.Pow(lly, 2)) < System.Math.Pow(wee, 2);

                            f3 = (System.Math.Pow(llx + stepX, 2) + System.Math.Pow(lly + stepY, 2)) < System.Math.Pow(wee, 2);

                            f4 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly + stepY, 2)) < System.Math.Pow(wee, 2);



                            if (parttype != "largeField")
                            {
                                //laser mark

                                f5 = !(LaserMarkCheck1(llx, lly) ||
                                    LaserMarkCheck1(llx + stepX, lly) ||
                                    LaserMarkCheck1(llx + stepX, lly + stepY) ||
                                    LaserMarkCheck1(llx, lly + stepY));
                                //notch
                                f6 = !(NotchCheck1(llx, lly) ||
                                         NotchCheck1(llx + stepX, lly) ||
                                         NotchCheck1(llx + stepX, lly + stepY) ||
                                         NotchCheck1(llx, lly + stepY));
                            }
                            else
                            {
                                f5 = !(LaserMarkCheck1_Large(llx, lly) ||
                                    LaserMarkCheck1_Large(llx + stepX, lly) ||
                                    LaserMarkCheck1_Large(llx + stepX, lly + stepY) ||
                                    LaserMarkCheck1_Large(llx, lly + stepY));
                                //notch
                                f6 = !(NotchCheck1_Large(llx, lly) ||
                                         NotchCheck1_Large(llx + stepX, lly) ||
                                         NotchCheck1_Large(llx + stepX, lly + stepY) ||
                                         NotchCheck1_Large(llx, lly + stepY));

                            }
                            #endregion

                            #region //full shot 判断
                            if (f1 && f2 && f3 && f4 && f5 && f6)//与Python不同，多了f5
                            {
                                totalDie = totalDie + shotDie;
                                fullShot = fullShot + 1;


                            }
                            #endregion

                            #region //有一点的 shot判断
                            else if (f1 || f2 || f3 || f4) //还有点都不在园内，但shot单边与圆相割的，小管芯应该有影响
                            {


                                partialShotDie = 0;
                                partialShot = partialShot + 1;
                                for (int k = 0; k < col2; k++)
                                {
                                    for (int m = 0; m < row2; m++)
                                    {
                                        sx = llx + k * dieX; sy = lly + m * dieY;
                                        f1 = (System.Math.Pow(sx, 2) + System.Math.Pow(sy, 2)) < System.Math.Pow(wee, 2);
                                        f2 = (System.Math.Pow(sx + dieX, 2) + System.Math.Pow(sy, 2)) < System.Math.Pow(wee, 2);
                                        f3 = (System.Math.Pow(sx + dieX, 2) + System.Math.Pow(sy + dieY, 2)) < System.Math.Pow(wee, 2);
                                        f4 = (System.Math.Pow(sx, 2) + System.Math.Pow(sy + dieY, 2)) < System.Math.Pow(wee, 2);

                                        if (parttype != "largeField")
                                        {
                                            //laser mark

                                            f5 = !(LaserMarkCheck1(sx, sy) ||
                                                LaserMarkCheck1(sx + dieX, sy) ||
                                                LaserMarkCheck1(sx + dieX, sy + dieY) ||
                                                LaserMarkCheck1(sx, sy + dieY));
                                            //notch
                                            f6 = !(NotchCheck1(sx, sy) ||
                                                     NotchCheck1(sx + dieX, sy) ||
                                                     NotchCheck1(sx + dieX, sy + dieY) ||
                                                     NotchCheck1(sx, sy + dieY));
                                        }
                                        else
                                        {
                                            f5 = !(LaserMarkCheck1_Large(sx, sy) ||
                                                LaserMarkCheck1_Large(sx + dieX, sy) ||
                                                LaserMarkCheck1_Large(sx + dieX, sy + dieY) ||
                                                LaserMarkCheck1_Large(sx, sy + dieY));
                                            //notch
                                            f6 = !(NotchCheck1_Large(sx, sy) ||
                                                     NotchCheck1_Large(sx + dieX, sy) ||
                                                     NotchCheck1_Large(sx + dieX, sy + dieY) ||
                                                     NotchCheck1_Large(sx, sy + dieY));

                                        }















                                        if (f1 && f2 && f3 && f4 && f5 && f6)
                                        { partialShotDie += 1; }

                                    }
                                }
                                totalDie = totalDie + partialShotDie;

                            }
                            #endregion


                            #region //四个点都不在园内，相切;
                            // else if ((llx > -97 && llx + stepX < 97) && (llx + stepX >-30 ) &&( (lly < -97 && lly + stepY > -97) || (lly < 97 && lly + stepY > 97)  )  )
                            else if (1 == 2)
                            {
                                partialShotDie = 0;
                                partialShot = partialShot + 1;
                                for (int k = 0; k < col2; k++)
                                {
                                    for (int m = 0; m < row2; m++)
                                    {
                                        sx = llx + k * dieX; sy = lly + m * dieY;
                                        f1 = (System.Math.Pow(sx, 2) + System.Math.Pow(sy, 2)) < System.Math.Pow(wee, 2);
                                        f2 = (System.Math.Pow(sx + dieX, 2) + System.Math.Pow(sy, 2)) < System.Math.Pow(wee, 2);
                                        f3 = (System.Math.Pow(sx + dieX, 2) + System.Math.Pow(sy + dieY, 2)) < System.Math.Pow(wee, 2);
                                        f4 = (System.Math.Pow(sx, 2) + System.Math.Pow(sy + dieY, 2)) < System.Math.Pow(wee, 2);
                                        if (parttype != "largeField")
                                        {
                                            f5 = !(((sy + dieY) > 92 || sy > 92) && ((sx + dieX < 13 && sx + dieX > -13) || (sx < 13 && sx > -13)));
                                            f6 = !((sy < -94 || sy + dieY < -94) && ((sx + dieX < 14 && sx + dieX > -14) || (sx < 14 && sx > -14)));
                                        }
                                        else
                                        {
                                            f5 = !(((sx + dieX) > 92 || sx > 92) && ((sy + dieY < 13 && sy + dieY > -13) || (sy < 13 && sy > -13)));
                                            f6 = !((sx < -94 || sx + dieX < -94) && ((sy + dieY < 14 && sy + dieY > -14) || (sy < 14 && sy > -14)));





                                        }
                                        if (f1 && f2 && f3 && f4 && f5 && f6)
                                        { partialShotDie += 1; }

                                    }
                                }
                                totalDie = totalDie + partialShotDie;
                            }
                            #endregion


                        }

                        #endregion


                    }
                    DataRow newRow = dt.NewRow();
                    newRow["shiftX"] = Xcell;
                    newRow["shiftY"] = Ycell;
                    newRow["dieQty"] = totalDie;
                    newRow["fullShot"] = fullShot;
                    newRow["partialShot"] = partialShot;
                    newRow["totalShot"] = (fullShot + partialShot);
                    //3mm Coverage
                    newRow["leftPartialSize"] = System.Math.Round((99.5 - stepX / 2 + Xcell) % stepX, 3);
                    newRow["rightPartialSize"] = System.Math.Round((99.5 - stepX / 2 - Xcell) % stepX, 3);
                    newRow["miniSizeX"] = System.Math.Round(0.5 * stepX - 3.3999, 3);
                    newRow["leftFlag"] = ((99.5 - stepX / 2 + Xcell) % stepX) == 0 || System.Math.Abs(((99.5 - stepX / 2 + Xcell) % stepX) - (0.5 * stepX - 3.3999)) < 0.001 || ((99.5 - stepX / 2 + Xcell) % stepX) > (0.5 * stepX - 3.3999);
                    newRow["rightFlag"] = ((99.5 - stepX / 2 - Xcell) % stepX) == 0 || System.Math.Abs(((99.5 - stepX / 2 - Xcell) % stepX) - (0.5 * stepX - 3.3999)) < 0.001 || ((99.5 - stepX / 2 - Xcell) % stepX) > (0.5 * stepX - 3.3999);
                    newRow["delta"] = System.Math.Round(System.Math.Abs(Ycell - laserY), 3);//#只要有一个cellx可以运算，laserY_Flag=True,当其它cellx无法计算时，因flag=True，offsetY填入888，后续需剔除
                    dt.Rows.Add(newRow);




                }
            }

            dt.DefaultView.Sort = "totalShot asc,dieQty desc,delta ASC";
            DataRow[] drs = dt.Select("shiftY <> 888 and leftFlag=true and rightFlag=true");
            if (drs.Length > 0)
            {
                offX = Convert.ToDouble(drs[0]["shiftX"].ToString());

                //  MessageBox.Show(offX.ToString() + "," + stepX.ToString());
                //  if (offX>stepX/2)
                //  { offX = stepX - offX; }
                //  else if (offX<-stepX/2)
                //  { offX = stepX + offX; }
                //  MessageBox.Show(offX.ToString() + "," + stepX.ToString());

                offY = Convert.ToDouble(drs[0]["shiftY"].ToString());
                laserY_Flag = true;
            }
            else
            {
                laserY_Flag = false;//#partial shot无法曝光，默认X方向无法曝光
                //  MessageBox.Show("No OffsetX/Y Available For Normal Field");

            }

            if (laserY_Flag == false)
            {

                offY = laserY;
                offX = (99.5 - stepX / 2) % stepX; //#chang 97-->99.5
                if (offX > stepX / 2)
                { offX = offX - stepX; } //#右侧完整die
                double left = (99.5 - stepX / 2 + offX) % stepX;

                if (left < (0.5 * stepX - 3.3999))  // # 如果左侧不完整shot无法曝光
                {
                    offX = 0.5 * stepX - 3.3999 - left + offX;

                    if (offX > stepX / 2)
                    { offX = offX - stepX; }
                    else if (-offX > stepX / 2)
                    { offX = offX + stepX; }    //#left size is supposed to be checked again,skippend,revise in case .....


                }



                dt.Clear();
                DataRow newRow = dt.NewRow();
                newRow["shiftX"] = System.Math.Round(offX, 4);
                newRow["shiftY"] = System.Math.Round(offY, 4);
                newRow["dieQty"] = totalDie;

                //3mm Coverage
                newRow["leftPartialSize"] = System.Math.Round((99.5 - stepX / 2 + offX) % stepX, 4);
                newRow["rightPartialSize"] = System.Math.Round((99.5 - stepX / 2 - offX) % stepX, 4);
                newRow["miniSizeX"] = System.Math.Round(0.5 * stepX - 3.3999, 3);
               
                newRow["leftFlag"] = ((99.5 - stepX / 2 + offX) % stepX) == 0 || System.Math.Abs(((99.5 - stepX / 2 + offX) % stepX) - (0.5 * stepX - 3.3999)) < 0.001 || ((99.5 - stepX / 2 + offX) % stepX) > (0.5 * stepX - 3.3999);
                newRow["rightFlag"] = ((99.5 - stepX / 2 - offX) % stepX) == 0 || System.Math.Abs(((99.5 - stepX / 2 - offX) % stepX) - (0.5 * stepX - 3.3999)) < 0.001 || ((99.5 - stepX / 2 - offX) % stepX) > (0.5 * stepX - 3.3999);
                newRow["delta"] = System.Math.Round(System.Math.Abs(offY - laserY), 3);//#只要有一个cellx可以运算，laserY_Flag=True,当其它cellx无法计算时，因flag=True，offsetY填入888，后续需剔除

                dt.Rows.Add(newRow);





            }



            if (autoFlag)
            { }
            else
            {
                ox = mOX;
                oy = mOY;
                dt.Rows[0]["shiftX"] = mOX;
                dt.Rows[0]["shiftY"] = mOY;

            }

            

            //return offX,offY,largeFlag,laserY_Flag
        }

        public void plotAsmlMap()  //预置参数，调用 DieQty类画图
        {

            string filePath = "C:\\temp\\" + part + "_Asml.emf";
            #region define Full Cover
            if (parttype == "normalField")
            {

                if (part.Substring(0, 2) == "XU" ||
                    part.Substring(0, 2) == "XV" ||
                    part.Substring(0, 2) == "UF" ||
                    part.Substring(0, 2) == "D6" ||
                    part.Substring(0, 2) == "O1" ||
                    part.Substring(0, 7) == "M" ||
                    part.Substring(0, 7) == "N")
                {
                    fullCover = true;
                }
                else
                {
                    fullCover = false;
                }

            }
            else if (parttype == "largeField" || parttype == "spliteGate")
            {
                fullCover = true;  //大视场，ASML MAP
            }
            else if (parttype == "mpw")
            { fullCover = false; }
            else // splitgate 及Nikon大视场
            { parttype = "spliteGate"; fullCover = true; }

            #endregion




            k = 20f;//如果不放大，picturebox1图片失真；过大，windows画板无法看；实际图片较大，打印时进行缩放

            sx = (float)stepX;////使用float是因为作图参数不接受double，原因待澄清
            sy = (float)stepY;
            dx = (float)dieX;
            dy = (float)dieY;

            try
            {
                Bitmap bmp = new Bitmap(1169, 827); //实际三楼打印机的设置
                Graphics gs = Graphics.FromImage(bmp);
                System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                wfrmap = Graphics.FromImage(mf);

                //ASML的offset及正常产品Nikon


                if (true) //取消是否自动计算差别；若是手动的，自动计算后，dt中的值改写为手动的
                {
                    ox = ((float)Convert.ToDouble(dt.Rows[0]["shiftX"].ToString()));
                    oy = ((float)Convert.ToDouble(dt.Rows[0]["shiftY"].ToString()));


                    //计算die qty时，大视场的sx,sy,dx,dy已重新更改，ox、oy不要分开单独计算


                    if (ox > sx / 2) { ox = sx - ox; }
                    else if (ox < -sx / 2) { ox = sx + ox; }

                    if (oy > sy / 2) { oy = sy - oy; }
                    else if (oy < -sy / 2) { oy = sy + oy; }


                  

                }


                           
                Plot(false); wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();
                              

                SaveMap(false);

                //大视场Nikon
                
                if (parttype == "largeField")
                {
                    float x;x = sy;sy = sx;sx = x / 2;
                    x = dx;dx = dy;dy = x;

                    if (true) //取消是否自动计算差别；若是手动的，自动计算后，dt中的值改写为手动的
                    {
                        oy = ((float)Convert.ToDouble(dt.Rows[0]["shiftX"].ToString()));
                        float tmp = ((float)Convert.ToDouble(dt.Rows[0]["shiftY"].ToString()));
                        tmp += sx / 2;
                        if (tmp >= -sx / 2 && tmp <= sx / 2)
                        { ox = -tmp; }
                        else
                        {
                            if (tmp < 0) { ox = sx + tmp; }
                            else { ox = sx - tmp; }
                        }
                    }
                    filePath = "C:\\temp\\" + part + "_Nikon.emf";
                    bmp = new Bitmap(1169, 827); //实际三楼打印机的设置
                    gs = Graphics.FromImage(bmp);
                    mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                    wfrmap = Graphics.FromImage(mf);
                    Plot(true); wfrmap.Save(); wfrmap.Dispose();  mf.Dispose();
                    SaveMap(true);




                }

                
            }


            catch
            {
                MessageBox.Show("作图失败，请检查输入参数是否正常；或程序是否异常");
                return;
            }
            
        }
        public void plotAsmlMap(string print)  //预置参数，调用 DieQty类画图
        {



            if (!(print.EndsWith("_Nikon")))
            {
                string filePath = "C:\\temp\\" + part + "_Asml.emf";
                if (parttype == "largeField")
                {
                    float tmp;
                    tmp = sx;
                    sx = sy; sy = 2 * tmp;
                    tmp = dx;
                    dx = dy; dy = tmp;
                }
                using (Bitmap bmp = new Bitmap(1169, 827))//实际三楼打印机的设置
                {
                    using (Graphics gs = Graphics.FromImage(bmp))
                    {
                        System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                        wfrmap = Graphics.FromImage(mf);
                        Plot(false); wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();
                        // SaveMap(false);
                    }
                }
            }
            else
            {

             string filePath = "C:\\temp\\" + part + "_Nikon.emf";

                using (Bitmap bmp = new Bitmap(1169, 827))  //实际三楼打印机的设置

                {
                    using (Graphics gs = Graphics.FromImage(bmp))
                    {
                        System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(filePath, gs.GetHdc());
                        wfrmap = Graphics.FromImage(mf);
                        Plot(true); wfrmap.Save(); wfrmap.Dispose(); mf.Dispose();
                        //SaveMap(true);
                    }
                }
            }

                  

        }
        public  void Plot(bool nikonFlag)
        {
            #region 参数和变量

            //计算offset时，已更改大视场相应sx，sy，dx，dy，
      
            bool s1, s2, s3, s4, s5, s6; bool d1, d2, d3, d4, d5, d6;


            int totalDie = 0; int totalShot = 0; int fullShot = 0; int partialshot = 0; int partialshotdie = 0;
            int n = Convert.ToInt32(sx / dx * sy / dy);
            int ncol = Convert.ToInt32(sx / dx);
            int nrow = Convert.ToInt32(sy / dy);

            //shot左下角坐标，坐标原点中心，正常坐标系；窗体图片坐标原点在屏幕左上角，Y轴向下；画图时需要坐标转换
            float llx, lly;
            Font font = new Font("宋体", k * 5f, FontStyle.Bold);
            Font font1 = new Font("宋体", k * 4f, FontStyle.Bold);


            // HatchBrush b = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.Black, Color.White);
            HatchBrush b = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.FromArgb(150, Color.Black), Color.FromArgb(0, Color.White));
            Brush b1 = new SolidBrush(Color.FromArgb(80, Color.Blue));
            Brush b2 = new SolidBrush(Color.FromArgb(95, Color.Black));
            Brush b3 = new SolidBrush(Color.FromArgb(30, Color.Black));

            Pen pen1 = new Pen(Color.Red, 1);
            pen1.DashStyle = DashStyle.Dash;
            StringBuilder strBuilder = new StringBuilder();

            #endregion

            #region row,column No, TXT FILE initialize
            ///wfrmap.TranslateTransform(k * 25, k * 32); 
            ///画圆（wafer）时，中心100，100，部分partial shot的坐标会为负；打印窗体图片时，负值似乎无法打印？？用坐标转换命令转换，值取Shot最大值25*32
            ///以上方法未用


            //作图用，行列的循环整数
            int y1 = Convert.ToInt16((100 - sy / 2 - oy) / sy + 1); //+ ,y rows
            int y2 = -Convert.ToInt16((100 - sy / 2 + oy) / sy + 1); //- ,y rows
            int x1 = -Convert.ToInt16((100 - sx / 2 + ox) / sx + 1); // - ,x columns
            int x2 = Convert.ToInt16((100 - sx / 2 - ox) / sx + 1); //+,x columns

            ///数组用于输出文本格式
            ///数组所有值给0，因为作图时，部分shot完全在200mm圆之外，不会进行循环判断并在数组中写入0
            string[,] txtFile = new string[(y1 - y2 + 1) * nrow, (x2 - x1 + 1) * ncol];
            for (int xx = 0; xx < (y1 - y2 + 1) * nrow; ++xx)
            {
                for (int yy = 0; yy < (x2 - x1 + 1) * ncol; ++yy)
                { txtFile[xx, yy] = "0"; }
            }
            #endregion

            #region Plot
            for (int row = y1; row >= y2; --row)
            {
                for (int col = x1; col <= x2; ++col)
                {
                    #region calculation Full SHOT Flag & LaserMark Flag
                    llx = col * sx - sx / 2 + ox; lly = row * sy - sy / 2 + oy;
                    s1 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly, 2)) < 97 * 97;
                    s2 = (System.Math.Pow(llx + sx, 2) + System.Math.Pow(lly, 2)) < 97 * 97;
                    s3 = (System.Math.Pow(llx + sx, 2) + System.Math.Pow(lly + sy, 2)) < 97 * 97;
                    s4 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly + sy, 2)) < 97 * 97;
                    ///大视场和普通视场的的LaserMark位置不同，分开计算
                    if (!(parttype == "largeField" && nikonFlag==false))
                    {
                        s5 = !(LaserMarkCheck(llx + 100, 100 - lly) ||
                             LaserMarkCheck(llx + sx + 100, 100 - lly) ||
                             LaserMarkCheck(llx + sx + 100, 100 - (lly + sy)) ||
                             LaserMarkCheck(llx + 100, 100 - (lly + sy)));
                        s6 = !(NotchCheck(llx + 100, 100 - lly) ||
                             NotchCheck(llx + sx + 100, 100 - lly) ||
                             NotchCheck(llx + sx + 100, 100 - (lly + sy)) ||
                             NotchCheck(llx + 100, 100 - (lly + sy)));
                    }
                    else
                    {
                        s5 = !(LaserMarkCheckLarge(llx + 100, 100 - lly) ||
                             LaserMarkCheckLarge(llx + sx + 100, 100 - lly) ||
                             LaserMarkCheckLarge(llx + sx + 100, 100 - (lly + sy)) ||
                             LaserMarkCheckLarge(llx + 100, 100 - (lly + sy)));
                        s6 = !(NotchCheckLarge(llx + 100, 100 - lly) ||
                             NotchCheckLarge(llx + sx + 100, 100 - lly) ||
                             NotchCheckLarge(llx + sx + 100, 100 - (lly + sy)) ||
                             NotchCheckLarge(llx + 100, 100 - (lly + sy)));
                    }
                    #endregion

                    #region Check Whether Is Full Shot; s5,s6==True, not in Laser Mark Area;
                    if (s1 && s2 && s3 && s4 && s5 && s6)
                    {
                        totalShot += 1;
                        fullShot += 1;
                        totalDie += n;
                        //坐标变换后，顶点顺序变了，额外减去sy，包括shot内die作图
                        wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);

                        //txt file
                        try
                        {
                            for (int yn = 0; yn < nrow; ++yn)
                            {
                                for (int xn = 0; xn < ncol; ++xn)
                                {
                                    txtFile[(y1 - row) * nrow + (nrow - yn), (col - x1) * ncol + xn] = "1";
                                }

                            }
                        }

                        catch
                        {
                            // MessageBox.Show("1"+y2.ToString()+","+x2.ToString()+","+nrow.ToString()+","+ncol.ToString()+","+row.ToString()+","+col.ToString());
                        }

                    }
                    #endregion

                    #region partial shot
                    else if (s1 || s2 || s3 || s4)
                    {
                        partialshotdie = 0;
                        if (fullCover || AreaCalculation(s1, s2, s3, s4, llx, lly, sx, sy, areaRatio))  // partial shot exposure
                        {
                            totalShot += 1;
                            partialshot += 1;

                            //draw shot rectangle
                            bool isDrawPartialShot = true;
                            if (fullCover == false && lly >= 94 &&
                                    (
                                         (llx < -9 && (llx + sx) > -9 && (llx + sx) < 9) ||
                                         ((llx + sx) > 9 && llx > -9 && llx < 9) ||
                                         (llx > -13 && (llx + sx < 13))
                                     )
                                )
                            {
                                //normal field,打标处不曝光
                                totalShot -= 1;
                                partialshot -= 1;

                                wfrmap.DrawString("X", font, Brushes.Black, k * (llx + 100 + 5), k * (100 - lly - sy + 5));

                                wfrmap.DrawRectangle(new Pen(Color.LightCyan, 1), k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);
                                isDrawPartialShot = false;

                            }
                            else
                            {
                                wfrmap.FillRectangle(b3, k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);
                                wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);

                            }






                            for (int row1 = nrow - 1; row1 >= 0; row1--)
                            {
                                for (int col1 = 0; col1 < ncol; col1++)
                                {


                                    #region calculate flag
                                    d1 = (System.Math.Pow(llx + col1 * dx, 2) + System.Math.Pow(lly + row1 * dy, 2)) < 97 * 97;
                                    d2 = (System.Math.Pow(llx + col1 * dx + dx, 2) + System.Math.Pow(lly + row1 * dy, 2)) < 97 * 97;
                                    d3 = (System.Math.Pow(llx + col1 * dx + dx, 2) + System.Math.Pow(lly + row1 * dy + dy, 2)) < 97 * 97;
                                    d4 = (System.Math.Pow(llx + col1 * dx, 2) + System.Math.Pow(lly + row1 * dy + dy, 2)) < 97 * 97;

                                     if (!(parttype == "largeField" && nikonFlag == false))
                                        {
                                        d5 = !(LaserMarkCheck(llx + col1 * dx + 100, 100 - (lly + row1 * dy)) ||
                                                LaserMarkCheck(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy)) ||
                                                LaserMarkCheck(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy + dy)) ||
                                                LaserMarkCheck(llx + col1 * dx + 100, 100 - (lly + row1 * dy + dy)));
                                        d6 = !(NotchCheck(llx + col1 * dx + 100, 100 - (lly + row1 * dy)) ||
                                                NotchCheck(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy)) ||
                                                NotchCheck(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy + dy)) ||
                                                NotchCheck(llx + col1 * dx + 100, 100 - (lly + row1 * dy + dy)));
                                    }
                                    else
                                    {
                                        d5 = !(LaserMarkCheckLarge(llx + col1 * dx + 100, 100 - (lly + row1 * dy)) ||
                                                LaserMarkCheckLarge(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy)) ||
                                                LaserMarkCheckLarge(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy + dy)) ||
                                                LaserMarkCheckLarge(llx + col1 * dx + 100, 100 - (lly + row1 * dy + dy)));
                                        d6 = !(NotchCheckLarge(llx + col1 * dx + 100, 100 - (lly + row1 * dy)) ||
                                                NotchCheckLarge(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy)) ||
                                                NotchCheckLarge(llx + col1 * dx + dx + 100, 100 - (lly + row1 * dy + dy)) ||
                                                NotchCheckLarge(llx + col1 * dx + 100, 100 - (lly + row1 * dy + dy)));
                                    }
                                    #endregion

                                    if (d1 && d2 && d3 && d4 && d5 && d6)
                                    {
                                        if (isDrawPartialShot)
                                        {
                                            totalDie += 1;
                                        }
                                        partialshotdie += 1;
                                        //Partial shot中的完整管芯不画，管芯太小会影响显示
                                        // wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (llx + col1 * dx + 100), k * (100 - (lly + row1 * dy) - dy), k * dx, k * dy);

                                        //txt file
                                        try
                                        {
                                            if (isDrawPartialShot)
                                            {
                                                txtFile[(y1 - row) * nrow + nrow - row1, (col - x1) * ncol + col1] = "1";
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (d1 || d2 || d3 || d4)
                                    {

                                        if (isDrawPartialShot)
                                        {
                                            wfrmap.FillRectangle(b, k * (llx + col1 * dx + 100), k * (100 - (lly + row1 * dy) - dy), k * dx, k * dy);

                                            wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (llx + col1 * dx + 100), k * (100 - (lly + row1 * dy) - dy), k * dx, k * dy);

                                            //txt file
                                            try
                                            {
                                                ///取消，客户有歧义
                                                //// txtFile[(y1 - row) * nrow + nrow - row1, (col - x1) * ncol + col1] = "P"; 
                                            }
                                            catch
                                            { }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        //txt file
                                        {
                                            txtFile[(y1 - row) * nrow + nrow - row1, (col - x1) * ncol + col1] = "0";
                                        }
                                        catch
                                        { }
                                    }

                                }

                            }















                        }
                        else
                        {
                            //画图打X
                            wfrmap.DrawString("X", font, Brushes.Black, k * (llx + 100 + 5), k * (100 - lly - sy + 5));

                            wfrmap.DrawRectangle(new Pen(Color.LightCyan, 1), k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);
                            try
                            {
                                for (int yn = 0; yn < nrow; yn++)
                                {
                                    for (int xn = 0; xn < ncol; xn++)
                                    {
                                        txtFile[(y1 - row) * nrow + nrow - yn, (col - x1) * ncol + xn] = "0";
                                    }

                                }
                            }
                            catch
                            {
                                //MessageBox.Show("5" + y2.ToString() + "," + x2.ToString() + "," + nrow.ToString() + "," + ncol.ToString() + "," + row.ToString() + "," + col.ToString()); 
                            }
                        }

                        //Partial Shot标注有效管芯
                        wfrmap.DrawString(partialshotdie.ToString(), font, Brushes.Black, k * (llx + 100), k * (100 - lly - sy));
                        /*
                           if (llx < 0)
                           {
                               if (lly < 0)
                               {
                                   wfrmap.DrawString(partialshotdie.ToString(), font, Brushes.Black, k * (llx + 100), k * (100 - lly - sy+sy/2));
                               }
                               else
                               {
                                   wfrmap.DrawString(partialshotdie.ToString(), font, Brushes.Black, k * (llx + 100), k * (100 - lly - sy ));
                               }
                           }
                           else
                           {
                               if (lly < 0)
                               {
                                   wfrmap.DrawString(partialshotdie.ToString(), font, Brushes.Black, k * (llx + 100 + sx / 3), k * (100 - lly - sy+sy/2));
                               }
                               else
                               {
                                   wfrmap.DrawString(partialshotdie.ToString(), font, Brushes.Black, k * (llx + 100+sx/3), k * (100 - lly - sy));
                               }
                           }
                         */
                    }
                    #endregion

                    ///以下部分忽略，部分覆盖中有 fullcover=True的判断
                    ///原来应该是为TXT map准备的
                    ///数组全部赋值为零，应可以取消以下步骤？？待确定
                    #region//全覆盖                                        
                    else

                    {
                        try
                        {
                            for (int yn = 0; yn < nrow; yn++)
                            {
                                for (int xn = 0; xn < ncol; xn++)
                                {
                                    txtFile[(y1 - row) * nrow + nrow - yn, (col - x1) * ncol + xn] = "0";
                                }

                            }
                        }
                        catch
                        {
                            // MessageBox.Show("9" + y2.ToString() + "," + x2.ToString() + "," + nrow.ToString() + "," + ncol.ToString() + "," + row.ToString() + "," + col.ToString()); 
                        }

                        if (fullCover == true)
                        {
                            s1 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly, 2)) < 10000;
                            s2 = (System.Math.Pow(llx + sx, 2) + System.Math.Pow(lly, 2)) < 10000;
                            s3 = (System.Math.Pow(llx + sx, 2) + System.Math.Pow(lly + sy, 2)) < 10000;
                            s4 = (System.Math.Pow(llx, 2) + System.Math.Pow(lly + sy, 2)) < 10000;
                            if (s1 || s2 || s3 || s4)
                            {
                                totalShot += 1;
                                partialshot += 1;

                                wfrmap.FillRectangle(b3, k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);
                                wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (llx + 100), k * (100 - lly - sy), k * sx, k * sy);

                            }
                            else
                            {

                            }

                        }

                        else
                        {

                        }
                    }
                    #endregion


                }
            }
            #endregion





            //数组写入文本文件
            string txtFileName;
            if(nikonFlag)
            {
                txtFileName = "C:\\temp\\" + part + "_nikon.txt";
            }
            else
            {
                txtFileName = "C:\\temp\\" + part + "_asml.txt";
            }
                
                
            System.IO.FileStream fs = new System.IO.FileStream(txtFileName, System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            for (int i = 0; i < (y1 - y2 + 1) * nrow; i++)
            {

                for (int j = 0; j < (x2 - x1 + 1) * ncol; j++)
                {
                    // https://zhidao.baidu.com/question/630357715450926644.html
                    // https://www.cnblogs.com/lili-work/p/6106670.html
                    sw.Write(txtFile[i, j]);
                }
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
            fs.Close();




            #region 网格和圆
            if (! (parttype == "largeField" && nikonFlag == false))
            {

                //laser mark

                wfrmap.FillRectangle(b1, k * (100 - 13), k * 0, k * 26, k * 8);
                // wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (100 - 13), k * 0, k * 26, k * 8);
                //laser 
                wfrmap.FillRectangle(b1, k * (100 - 14), k * 194, k * 28, k * 6);
                //  wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (100 - 14), k * 194, k * 28, k * 6);

                //notch
                PointF[] curve = new PointF[] { new PointF(k * 100, k * 196), new PointF(k * 98, k * 200), new PointF(k * 102, k * 200) };
                wfrmap.FillPolygon(b2, curve);
                // wfrmap.DrawPolygon(new Pen(Color.Black, 1), curve);

            }
            else
            {
                //laser mark

                wfrmap.FillRectangle(b1, k * (200 - 8), k * (100 - 13), k * 8, k * 26);
                // wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * (200 - 8), k * (100 - 13), k * 8, k * 26);
                //laser 
                wfrmap.FillRectangle(b1, k * 0, k * (100 - 14), k * 6, k * 28);
                //  wfrmap.DrawRectangle(new Pen(Color.Black, 1), k * 0, k * (100-14), k * 6, k * 28);

                //notch
                PointF[] curve = new PointF[] { new PointF(k * 4, k * 100), new PointF(k * 0, k * 98), new PointF(k * 0, k * 102) };
                wfrmap.FillPolygon(b2, curve);
                // wfrmap.DrawPolygon(new Pen(Color.Black, 1), curve);
            }




            //画圆
            wfrmap.DrawEllipse(new Pen(Color.Black, 1), 0, 0, k * 200, k * 200);
            wfrmap.DrawEllipse(pen1, k * 3, k * 3, k * 194, k * 194);

            #endregion

            #region Map Information

            //ox,oy只保留四位小数点
            string strOx, strOy;
            try 
            { strOx = ox.ToString().Split('.')[0] + "." + ox.ToString().Substring(ox.ToString().IndexOf('.') + 1, 4); }
            catch
            {
                strOx = ox.ToString();
            }
            try
            { strOy = oy.ToString().Split('.')[0] + "." + oy.ToString().Substring(oy.ToString().IndexOf('.') + 1, 4); }
            catch
            {
                strOy = oy.ToString();
            }


            wfrmap.DrawString(
                "Part         " + part + "\n\n" +
                "StepX:       " + sx.ToString() + "\n\n" +

                "StepY:       " + sy.ToString() + "\r\n\r\n" +
                "DieX:        " + dx.ToString() + "\r\n\r\n" +
                "DieY:        " + dy.ToString() + "\r\n\r\n" +
                "OffsetX:     " +strOx + "\r\n\r\n" +
                "OffsetY:     " + strOy + "\r\n\r\n" +

                "FullShot:    " + fullShot.ToString() + "\n" +

                "PartialShot: " + partialshot.ToString() + "\n" +

                "TotlaShot:   " + totalShot.ToString() + "\n" +

                "TotalDie:    " + totalDie.ToString(), font, Brushes.Black, k * 60, k * 30);
            // Comment
            strBuilder.Append("产品是：" + part + "\r\n\r\n");
            if (nikonFlag == false)
            {
                strBuilder.Append("普通产品MAP，或大视场产品ASML/CD/OVL Map\r\n\r\n");

            }
            else
            {
                strBuilder.Append("大视场产品NIKON MAP\r\n\r\n");

            }


            if (parttype == "largeField" && nikonFlag == false)
            {
                strBuilder.Append("大视场产品，KLA Rotation选“0”度；Locking Corner标记“A”\r\n\r\n");
                strBuilder.Append("大视场产品，KLA Locking Corner标记“A”\r\n\r\n");
                strBuilder.Append("大视场打标IMAGE-X:" + System.Math.Round((100 - 5.5 - sx / 2 - ox) % sx, 5).ToString() + "\r\n\r\n");
                strBuilder.Append("大视场打标IMAGE-X Center Shift:" + System.Math.Round(((100 - 5.5 - sx / 2 - ox) % sx - sx) / 2, 5).ToString() + "\r\n\r\n");



            }
            else
            {
                strBuilder.Append("普通视场打标IMAGE-Y:" + System.Math.Round((100 - 5.5 - sy / 2 - oy) % sy, 5).ToString() + "\r\n\r\n");
                strBuilder.Append("普通视场打标IMAGE-Y Center Shift:" + System.Math.Round(((100 - 5.5 - sy / 2 - oy) % sy - sy) / 2, 5).ToString() + "\r\n\r\n");
                double tmpsize = (100 - 5.5 - sy / 2 - oy) % sy;
                strBuilder.Append("普通视场打标BLIND -Y:" + (System.Math.Round(-sy / 2, 5) - 0.032).ToString() + "\r\n\r\n");
                strBuilder.Append("普通视场打标BLIND +Y:" + (System.Math.Round(-sy / 2 + tmpsize, 5)).ToString() + "\r\n\r\n");
                strBuilder.Append("普通视场打标BLIND Y SHIFT:" + (System.Math.Round(-(tmpsize - sy) / 2, 5) * 1000).ToString() + "\r\n\r\n");

            }
            // strBuilder.Append("Nikon TO/GT/PC SHIFT FOCUS-->YES\r\n");
            // strBuilder.Append("PAD EGA SHOT-->6过4\r\n");
            // strBuilder.Append("W1对位以bias table为准\r\n");
            // strBuilder.Append("孔，CP/FU，非关键层次打标处不曝光\r\n");
            // strBuilder.Append("ASML层次，OVL>45nm层次，除TO/GT/W1/C18_SI\r\n");
            // strBuilder.Append("     Wafer Alignment:Y\r\n");
            // strBuilder.Append("     Reduce Reticle Alignments:Y\r\n");
            // strBuilder.Append("     Maximum Drift:10\r\n");
            // strBuilder.Append("     Maximum Interval(wafers):25\r\n");
            // strBuilder.Append("M52/1%工艺TO前非RE层NIKON参数设置\r\n");
            // strBuilder.Append("     EGA为FIA;Shot的数为8个;\r\n");
            // strBuilder.Append("     Reject：Yes;Wafer Alignment:Y\r\n");
            // strBuilder.Append("     EGA Limitation:0.65,EGA Requisite shot:6\r\n");
            // strBuilder.Append("     Result Allowance：1.5um\r\n");
            // strBuilder.Append("Nikon 60um划片槽:\r\n");
            // strBuilder.Append("      非TT Search用LSA 11\r\n");
            // strBuilder.Append("      CP层EGA用FIA\r\n");

            /*
               if 'ZA' in list(self.dfB['Layer']) or 'ZO' in list(self.dfB['Layer']) :
                count = count +1
                sheet.write_merge(count, count, 0, 8, '__(双)零层工艺，请注意对位顺序及对位坐标，以及NIKON对位方法',style)
            if '2.0' in list(self.dfB['MLM']):
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__复合版,若四合一版，Metal层的focus disable range设置为6000', style)
            if self.fulltech[1]=='1' or self.fulltech[0:3]=='M52':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__M52/.18以下工艺TO前非关键层(除RE）NIKON参数设置:EGA为FIA,Shot的数为8个，EGA Reject：Yes;', style)
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__(续上一行）EGA Limitation：0.65，EGA Requisite shot：6，Result Allowance：1.5um', style)
            if 'PI' in list(self.dfB['Layer']):
                count = count +1
                sheet.write_merge(count, count, 0, 8, '__PI层次的菜单，请将focus offset 0.2 直接加进菜单，并通知朱龙飞设置R2R',style)
            if  self.part[0:2]=='D6':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除打标处，所有层次partial shot必须都要曝光', style)
            if  self.part[0:2]=='O1':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除打标处，所有层次partial shot必须都要曝光', style)
            if  self.part[0:2]=='XV' or self.part[0:2]=='XU' or self.part[0:2]=='UF':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除了注入、孔层打标处不曝光以外其他边缘全部曝光', style)
            if self.part[0:2] == 'XV' or self.part[0:2] == 'XU' or self.part[0:2] == 'UF':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__除了注入、孔层打标处不曝光以外其他边缘全部曝光', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT 的PAD层search 、EGA都用FIA,算法41(工艺代码第一位为 “T" 或 "K")', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT新品菜单新建时，SN/SP层次默认EGA对位方式设置为FIA', style)

            if self.fulltech[0]=='T' or self.fulltech[0]=='K':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__DOMS/IGBT新品菜单新建时，DMOS产品新建，Nikon前段对位固定使用TR层次mark', style)

            if self.fulltech[0:3]=='B52':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__B52有PX layer的,对位PX', style)

            if self.part[-1]=='M' or self.part[-1]=='N':
                count = count + 1
                sheet.write_merge(count, count, 0, 8, '__M/N结尾的产品,重新模拟map,按照新品重新建立曝光菜单,无需去除打标处及边缘无效shot，全部曝光', style)
          */









            wfrmap.DrawString(strBuilder.ToString(), font, Brushes.Black, k * 230, k * 5);




            #endregion






        }
        void SaveMap(bool nikonFlag)
        {
            if (true)//自动作图数据保存-->不再区分
            {
                string a;



                if(nikonFlag)
                { a = "保存大视场产品Nikon Map"; }
                else
                { a = "保存Asml Map；普通产品无Nikon Map"; }
                if (MessageBox.Show(a+"\n\n\n\n" +
                    "请确认是Part名等参数是否和Map数据匹配!!\n\n" +
                              "   同名Part旧数据会被覆盖!!\n\n" ,
                              "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

               
                    using (SQLiteConnection conn = new SQLiteConnection(connStr))
                    {
                        string sql;
                        conn.Open();
                        if (!nikonFlag)
                        {
                            sql = " DELETE  FROM TBL_MAP WHERE PART='" + part + "'";
                        }
                        else
                        {
                            sql = " DELETE  FROM TBL_MAP WHERE PART='" + part + "_Nikon'";
                        }
                        using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                        {
                            com.ExecuteNonQuery();
                        }
                    }
                    //添加
                    using (SQLiteConnection conn = new SQLiteConnection(connStr))
                    {
                        string sql;
                        conn.Open();
                        if (parttype == "largeField")
                        {
                            if (!nikonFlag)
                            {
                                sql = " INSERT  INTO TBL_MAP (k,StepX,StepY,DieX,DieY,OffsetX,OffsetY,AreaRatio,FullCover,PartType,NikonFlag,Part,Riqi) " +
                                   "VALUES (" +
                                   k + "," + sy / 2 + "," + sx + "," + dx + "," + dy + "," + ox + "," + oy + "," + areaRatio + "," + fullCover + ",'" + parttype + "'," + nikonFlag + ",'" + part + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                  + ")";
                            }
                            else
                            {
                                sql = " INSERT  INTO TBL_MAP (k,StepX,StepY,DieX,DieY,OffsetX,OffsetY,AreaRatio,FullCover,PartType,NikonFlag,Part,Riqi) " +
                                   "VALUES (" +
                                   k + "," + sx + "," + sy + "," + dx + "," + dy + "," + ox + "," + oy + "," + areaRatio + "," + fullCover + ",'" + parttype + "'," + nikonFlag + ",'" + part + "_Nikon','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                  + ")";

                            }
                        }
                        else
                        {
                            sql = " INSERT  INTO TBL_MAP (k,StepX,StepY,DieX,DieY,OffsetX,OffsetY,AreaRatio,FullCover,PartType,NikonFlag,Part,Riqi) " +
                                  "VALUES (" +
                                  k + "," + sx + "," + sy + "," + dx + "," + dy + "," + ox + "," + oy + "," + areaRatio + "," + fullCover + ",'" + parttype + "'," + nikonFlag + ",'" + part + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'"
                                 + ")";
                        }

                   
                        using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                        {
                            com.ExecuteNonQuery();
                        }
                    }

                }
             


            }
       




        }


















        private bool LaserMarkCheck1(double x, double y)
        {

            if (x > -13 && x < 13 && y > 92 && y < 99.15)
            { return true; }
            else
            { return false; }

        }
        private  bool NotchCheck1_Large(double x, double y)
        {

            if (y > -14 && y < 14 && x < -94 && x > -99.01)
            { return true; }
            else
            { return false; }

        }
        private  bool LaserMarkCheck1_Large(double x, double y)
        {

            if (y > -13 && y < 13 && x > 92 && x < 99.15)
            { return true; }
            else
            { return false; }

        }
        private  bool NotchCheck1(double x, double y)
        {

            if (x > -14 && x < 14 && y < -94 && y > -99.01)
            { return true; }
            else
            { return false; }

        }



        private  bool LaserMarkCheck(double x, double y)
        {

            if (x > 87 && x < 113 && y > 0 && y < 8)
            { return true; }
            else
            { return false; }

        }
        private  bool NotchCheck(double x, double y)
        {

            if (x > 86 && x < 114 && y > 194 && y < 200)
            { return true; }
            else
            { return false; }

        }
        //大视场，原点左上角，画图用
        private  bool LaserMarkCheckLarge(double x, double y)
        {

            if (y > 87 && y < 113 && x > 192 && x < 199.15)
            { return true; }
            else
            { return false; }

        }
        private  bool NotchCheckLarge(double x, double y)
        {

            if (y > 86 && y < 114 && x > 0 && x < 6)
            { return true; }
            else
            { return false; }

        }
        private  bool AreaCalculation(bool s1, bool s2, bool s3, bool s4, double llx, double lly, double sx, double sy, double areaRatio)
        {

            double x, y, x1, y1, x2, y2;
            StringBuilder str = new StringBuilder();
            bool[] checkFlag = { s1, s2, s3, s4 };
            for (int i = 0; i < 4; i++)
            { if (checkFlag[i] == true) { str.Append(i.ToString()); } }
            if (str.ToString().Length == 1)//只有一个点落在圆片内，计算面积；类似都不落在圆内，相割的，不考虑
            {
                if (str.ToString() == "0")
                { x = llx + 100; y = 100 - lly; }
                else if (str.ToString() == "1")
                { x = llx + sx + 100; y = 100 - lly; }
                else if (str.ToString() == "2")
                { x = llx + sx + 100; y = 100 - lly - sy; }
                else
                { x = llx + 100; y = 100 - lly - sy; }

                x1 = 100 + System.Math.Pow(100 * 100 - (y - 100) * (y - 100), 0.5);
                x2 = 100 - System.Math.Pow(100 * 100 - (y - 100) * (y - 100), 0.5);
                y1 = 100 + System.Math.Pow(100 * 100 - (x - 100) * (x - 100), 0.5);
                y2 = 100 - System.Math.Pow(100 * 100 - (x - 100) * (x - 100), 0.5);

                if (System.Math.Abs(x - x1) > System.Math.Abs(x - x2)) { x1 = x2; }
                if (System.Math.Abs(y - y1) > System.Math.Abs(y - y2)) { y1 = y2; }
                //顶点，xy, 弧x1y,xy1

                double area1 = System.Math.Abs((x - x1) * (y - y1) / 2); //小三角形面积
                double t1 = System.Math.Pow((x - x1) * (x - x1) + (y - y1) * (y - y1), 0.5);
                double t2 = (100 + 100 + t1) / 2;
                double area2 = System.Math.Pow(t2 * (t2 - 100) * (t2 - 100) * (t2 - t1), 0.5);//大三角形面积
                double a1 = System.Math.Acos((100 * 100 + 100 * 100 - t1 * t1) / (2 * 100 * 100)); //角度，
                double area3 = a1 * 100 * 100 / 2; //扇形面积

                if ((area3 - area2 + area1) / System.Math.PI / 100 / 100 > (areaRatio / 100))
                { return true; }
                else
                { return false; }


            }
            else
            {
                return true;
            }

        }



    }
}
