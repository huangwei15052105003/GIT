using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;





//using System.ComponentModel;
using System.Data;
using System.Drawing;


using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;











namespace LithoForm
{
    public class R2R
    {

        public static DataTable cdQuery(string tools, string part, string layer, string date1, string date2, string connStr)
        {
            string sql;
            DataTable dt = new DataTable();

            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }


            //make sql
            //==tool
            sql = " SELECT LOTID,DCOLL_TIME,JI_TIME,TYPE,TOOL,CDSEM,PART,LAYER,AVG,JOBIN,OPT,FB FROM TBL_CD ";
            if (tools.Length == 0 || tools.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql += " WHERE TOOL IN  (" + tools + ")"; }
            //==part
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
                            tmp += " or  PART like '%" + str + "%' ";
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
            //==layer
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
                            tmp += " OR layer like '%" + str + "%' ";
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
            //==date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
            {
                sql += " WHERE (DCOLL_TIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }
            else
            {
                sql = sql + " AND (DCOLL_TIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }


            if (MessageBox.Show("查询条件是：\r\n\r\n" + sql + "\r\n\r\n\r\n\r\n选择   是(Y)  -->继续查询\r\n\r\n\r\n\r\n选择   否(N)-->  重新定义条件\r\n\r\n\r\n\r\n注意数据源的选择，尽量复制数据至本机查询", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // MessageBox.Show("YES");
            }
            else
            {
                MessageBox.Show("退出，重新定义条件"); return dt;
            }


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
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            return dt;




        }

        public static DataTable ovlQuery(string tools, string para, string part, string layer, string date1, string date2, string connStr)
        {
            DataTable dt = new DataTable();
            string sql;
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }



            //make sql
            sql = " SELECT LOTID, DCOLL_TIME,JI_TIME,TYPE,TOOL,PARA,PART,LAYER,JOBIN,AVG,OPT,FB FROM TBL_OVL ";
            //==tool
            if (tools.Length == 0 || tools.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql += " WHERE TOOL IN  (" + tools + ")"; }
            //==parameter

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
            //==part
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
                            tmp += " or PART like '%" + str + "%' ";
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
            //==layer
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
                            tmp += " or layer like '%" + str + "%' ";
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
            //==date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 7, 7) == "TBL_OVL")
            {
                sql += " WHERE (DCOLL_TIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }
            else
            {
                sql = sql + " AND (DCOLL_TIME BETWEEN '" + date1 + "' and  '" + date2 + "')";

            }

            sql += " ORDER BY DCOLL_TIME";

            if (MessageBox.Show("查询条件是：\r\n\r\n" + sql + "\r\n\r\n\r\n\r\n选择   是(Y)  -->继续查询\r\n\r\n\r\n\r\n选择   否(N)-->  重新定义条件\r\n\r\n\r\n\r\n注意数据源的选择，尽量复制数据至本机查询", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // MessageBox.Show("YES");
            }
            else
            {
                MessageBox.Show("退出，重新定义条件"); return dt;
            }


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
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            int rowsCount = dt.Rows.Count;


            return dt;
        }

        public static void GenerateJobinStationSql(DataTable dt)  //仅part一个参数
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("c:\\TEMP\\");
            directoryInfo.Create();
            string sql = string.Empty;
            string part = " PART_NAME in ( ";
            int n = 0;
            foreach (DataRow row in dt.Rows)
            {
                n += 1;
                part += "'" + row["part"] + "',";
                if (n > 400)
                {
                    part = part.Substring(0, part.Length - 1) + ") or  PART_NAME in (  ";
                    n = 0;
                }
            }
            //此处需要加判断，若正好是400的倍数，需要去除  or  PART_NAME in ( 。。。。。。。。
            part = " " + part.Substring(0, part.Length - 1) + ") ";
            // https://www.cnblogs.com/ryanzheng/p/10934246.html




            sql = " select * from  (select TMP1.EQ_ID, TMP1.TECH_NAME, TMP1.PART_NAME, TMP1.LAYER_NAME, TMP1.CD_FEEDBACK, TMP1.HOLE_SPACE, TMP1.LOCK_FIXED, TMP1.LOCK_LOT,TMP1.OL_FEEDBACK,  TMP1.PRE_EQ_ID,  TMP1.PRE_LAYER_NAME, TMP1.CD_EXPIRED_HR, TMP1.E_L_VALUE,  TMP1.OL_EXPIRED_HR, TMP1.R_VALUE,TMP1.TARGET_VALUE, tmp1.constrain,   (case when tmp2.dose is null then tmp3.dose_nf else tmp2.dose  end) as DOSE, (case when tmp2.focus is null then tmp3.focus_nf else focus end) as FOCUS, (case when offsetx is null then offsetx_nf else offsetx end) as OFFSETX,  (case when offsety is null then offsety_nf else offsety end) as OFFSETY,(case when scalingx is null then scalingx_nf else scalingx end) as SCALINGX, (case when scalingy is null then scalingy_nf else scalingy end) as SCALINGY,  (case when nonortho is null then nonortho_nf else nonortho end) as NONORTHO, (case when wrotation is null then wrotation_nf else wrotation end) as WROTATION, (case when shotscaling is null then shotscaling_nf else shotscaling end) as SHOTSCALING,(case when SHOTROTATION is null then SHOTROTATION_nf else SHOTROTATION end) as SHOTROTATION,  asymmag_nf AS ASYMMAG, asymrot_nf AS ASYMROT    FROM (select EQ_ID, PART_NAME, LAYER_NAME, PRE_LAYER_NAME,PRE_EQ_ID,DOSE,FOCUS,   OFFSETX,OFFSETY,SCALINGX,SCALINGY,SHOTSCALING,SHOTROTATION,ortho nonortho,rotation wrotation  from r2rph.tool_param_fb_horiz_view t5 where t5.fb_header in  ( select max(t6.id) from R2RRAW.FB_HEADER t6  where  t6.fb_flag = 'Y'   group by t6.part_name,  t6.layer_name, t6.layer_version, t6.eq_id, t6.pre_layer_name, t6.pre_eq_id) ) tmp2,(select EQ_ID,PART_NAME,LAYER_NAME,DOSE_NF,FOCUS_NF, tranx_nf OFFSETX_NF,trany_nf OFFSETY_NF,nonORTHO_NF,  wrotation_nf, wexpanx_nf SCALINGX_NF,  wexpany_nf SCALINGY_NF,  rotation_nf SHOTROTATION_NF, magnification_nf SHOTSCALING_NF,asymmag_nf,asymrot_nf from  r2rph.tool_param_nonfb_horiz_view  ) tmp3, (select * from r2rph.global_conf_overview where eq_id in ( 'ALII01', 'ALII02', 'ALII03', 'ALII04', 'ALII05', 'ALSIB6', 'ALSIB7', 'ALSIB8', 'ALSIB9', 'ALII10', 'ALII11', 'ALII12', 'ALII13', 'ALII14', 'ALII15', 'ALII16', 'ALII17', 'ALII18', 'BLSIBK', 'BLSIBL', 'BLSIE1', 'BLSIE2')) tmp1  where  tmp1.part_name = tmp2.part_name(+)    and tmp1.layer_name = tmp2.layer_name(+)   and tmp1.eq_id = tmp2.eq_id(+)    and tmp1.pre_eq_id = tmp2.pre_eq_id(+)    and tmp1.pre_layer_name = tmp2.pre_layer_name(+)   and tmp1.part_name = tmp3.part_name(+)   and tmp1.layer_name = tmp3.layer_name(+)    and tmp1.eq_id = tmp3.eq_id(+)  union   select TMP1.EQ_ID, TMP1.TECH_NAME, TMP1.PART_NAME, TMP1.LAYER_NAME, TMP1.CD_FEEDBACK, TMP1.HOLE_SPACE, TMP1.LOCK_FIXED, TMP1.LOCK_LOT,TMP1.OL_FEEDBACK,  TMP1.PRE_EQ_ID,  TMP1.PRE_LAYER_NAME, TMP1.CD_EXPIRED_HR, TMP1.E_L_VALUE,  TMP1.OL_EXPIRED_HR, TMP1.R_VALUE,TMP1.TARGET_VALUE, tmp1.constrain,   (case when tmp2.dose is null then tmp3.dose_nf else tmp2.dose  end) as DOSE, (case when tmp2.focus is null then tmp3.focus_nf else focus end) as FOCUS, (case when offsetx is null then offsetx_nf else offsetx end) as OFFSETX,  (case when offsety is null then offsety_nf else offsety end) as OFFSETY,(case when scalingx is null then scalingx_nf else scalingx end) as SCALINGX, (case when scalingy is null then scalingy_nf else scalingy end) as SCALINGY,  (case when nonortho is null then nonortho_nf else nonortho end) as NONORTHO, (case when wrotation is null then wrotation_nf else wrotation end) as WROTATION, (case when shotscaling is null then shotscaling_nf else shotscaling end) as SHOTSCALING,(case when SHOTROTATION is null then SHOTROTATION_nf else SHOTROTATION end) as SHOTROTATION,(CASE WHEN ASYMMAG IS NULL THEN ASYMMAG_NF ELSE ASYMMAG END) AS ASYMMAG , ( CASE WHEN ASYMROT  IS NULL THEN ASYMROT_NF ELSE ASYMROT END ) AS ASYMROT FROM (select  EQ_ID, PART_NAME,LAYER_NAME,PRE_LAYER_NAME,PRE_EQ_ID,DOSE,FOCUS,   TRANX OFFSETX,TRANY OFFSETY,WEXPANX SCALINGX,WEXPANY SCALINGY,MAGNIFICATION SHOTSCALING,ROTATION SHOTROTATION,nonortho, wrotation,ASYMMAG,ASYMROT  from r2rph.tool_param_fb_horiz_view t5 where t5.fb_header in  ( select max(t6.id) from R2RRAW.FB_HEADER t6  where  t6.fb_flag = 'Y'   group by t6.part_name,  t6.layer_name, t6.layer_version, t6.eq_id, t6.pre_layer_name, t6.pre_eq_id) ) tmp2,(select EQ_ID,PART_NAME,LAYER_NAME,DOSE_NF,FOCUS_NF, tranx_nf OFFSETX_NF,trany_nf OFFSETY_NF,nonORTHO_NF,  wrotation_nf, wexpanx_nf SCALINGX_NF,  wexpany_nf SCALINGY_NF,  rotation_nf SHOTROTATION_NF, magnification_nf SHOTSCALING_NF,asymmag_nf,asymrot_nf from  r2rph.tool_param_nonfb_horiz_view  ) tmp3, (select * from r2rph.global_conf_overview where eq_id in ( 'ALDI02', 'ALDI03','ALDI05','ALDI06', 'ALDI07','ALDI09', 'ALDI10', 'ALDI11','ALDI12','BLDI08', 'BLDI13')) tmp1  where  tmp1.part_name = tmp2.part_name(+)    and tmp1.layer_name = tmp2.layer_name(+)   and tmp1.eq_id = tmp2.eq_id(+)    and tmp1.pre_eq_id = tmp2.pre_eq_id(+)    and tmp1.pre_layer_name = tmp2.pre_layer_name(+)   and tmp1.part_name = tmp3.part_name(+)   and tmp1.layer_name = tmp3.layer_name(+)    and tmp1.eq_id = tmp3.eq_id(+)) where  " + part;






            System.IO.File.WriteAllText(@"C:\temp\sql.txt", sql);




        }

        public static void GenerateJobinStationSql(DataTable dt, string layer)  //part和layer两个参数
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("c:\\TEMP\\");
            directoryInfo.Create();
            string sql = string.Empty;


            //   string key = " PART_NAME||LAYER_NAME in ( ";
            //   int n = 0;
            //   foreach (DataRow row in dt.Rows)
            //    {
            //       n += 1;
            //       key += "'" + row[0] +row[1]+ "',";
            //       if (n > 400)
            //       {
            //           key = key.Substring(0, key.Length - 1) + ") or   PART_NAME||LAYER_NAME in  (  ";
            //            n = 0;
            //        }
            //   }

            string key = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                key += " (a02_partid='" + row[0] + "' AND a03_layer='" + row[1] + "') OR ";

            }








            key = " " + key.Substring(0, key.Length - 3) + " ";
            // https://www.cnblogs.com/ryanzheng/p/10934246.html

            sql = " WITH tmp2 as  ( select  EQ_ID,PART_NAME,LAYER_NAME,PRE_LAYER_NAME,PRE_EQ_ID,DOSE,FOCUS,  tranx OFFSETX,  trany OFFSETY,  wexpanx SCALINGX,  wexpany SCALINGY,  wrotation,  nonortho,  magnification SHOTSCALING,  rotation SHOTROTATION ,  asymmag,asymrot  from  r2rph.tool_param_fb_horiz_view t5  where t5.fb_header in  (  select  max(t6.id)  from  R2RRAW.FB_HEADER t6    where  t6.fb_flag = 'Y'  group by  t6.part_name,  t6.layer_name,  t6.layer_version,  t6.eq_id,  t6.pre_layer_name,  t6.pre_eq_id  )  ),tmp3 as  (  select  EQ_ID,PART_NAME,LAYER_NAME,DOSE_NF,FOCUS_NF,  tranx_nf OFFSETX_NF,  trany_nf OFFSETY_NF,  nonORTHO_NF,  wrotation_nf,  wexpanx_nf SCALINGX_NF,  wexpany_nf SCALINGY_NF,  rotation_nf SHOTROTATION_NF,  magnification_nf SHOTSCALING_NF, asymmag_nf,asymrot_nf  from  r2rph.tool_param_nonfb_horiz_view  ),tmp4 as  (  select  EQ_ID,PART_NAME,LAYER_NAME,  PRE_EQ_ID,  DOSE_UP,FOCUS_UP,  tranx_up OFFSETX_UP,  trany_up OFFSETY_UP,  wexpanx_up SCALINGX_UP,  wexpany_up SCALINGY_UP,  magnification_up SHOTSCALING_UP,  rotation_up SHOTROTATION_UP,  wrotation_up,  nonortho_up,  DOSE_LOW,FOCUS_LOW,  tranx_low OFFSETX_low,  trany_low OFFSETY_low,  wexpanx_low SCALINGX_low,  wexpany_low SCALINGY_low,  magnification_low SHOTSCALING_low,  rotation_low SHOTROTATION_low,  wrotation_low,  nonortho_low,  asymmag_up,asymmag_low,asymrot_up,asymrot_low  from  r2rph.tool_param_spec_horiz_view  ),tmp1 as     (  select  *  from  r2rph.global_conf_overview       ),final as  (  select  TMP1.EQ_ID,  TMP1.TECH_NAME,  TMP1.PART_NAME,  TMP1.LAYER_NAME,  TMP1.CD_FEEDBACK,  TMP1.HOLE_SPACE,  TMP1.LOCK_FIXED,  TMP1.LOCK_LOT,  TMP1.OL_FEEDBACK,  TMP1.PRE_EQ_ID,  TMP1.PRE_LAYER_NAME,  TMP1.CD_EXPIRED_HR,  TMP1.E_L_VALUE,  TMP1.OL_EXPIRED_HR,  TMP1.R_VALUE,  TMP1.TARGET_VALUE,  tmp1.constrain,  TMP2.DOSE,TMP2.FOCUS,TMP2.OFFSETX,TMP2.OFFSETY,TMP2.SCALINGX,TMP2.SCALINGY,TMP2.SHOTSCALING,TMP2.SHOTROTATION,  asymmag,asymrot,wrotation,nonortho,  tmp3.DOSE_NF,tmp3.FOCUS_NF,tmp3.OFFSETX_NF,tmp3.OFFSETY_NF,tmp3.nonORTHO_NF,tmp3.SCALINGX_NF,tmp3.SCALINGY_NF,tmp3.SHOTROTATION_NF,tmp3.SHOTSCALING_NF,  wrotation_nf,asymmag_nf,asymrot_nf,  DOSE_UP,DOSE_LOW,FOCUS_UP,FOCUS_LOW,tmp4.OFFSETX_UP, OFFSETX_LOW,OFFSETY_UP,OFFSETY_LOW,SCALINGX_UP,SCALINGX_LOW,SCALINGY_UP,SCALINGY_LOW,  wrotation_up,wrotation_low,nonortho_up,nonortho_low,  SHOTSCALING_UP,SHOTSCALING_LOW,SHOTROTATION_UP,SHOTROTATION_LOW,  asymmag_up,asymmag_low,  asymrot_up,asymrot_low    from  tmp1,TMP2,tmp3,tmp4  WHERE  tmp1.part_name = tmp2.part_name(+)  and tmp1.layer_name = tmp2.layer_name(+)  and tmp1.eq_id = tmp2.eq_id(+)  and tmp1.pre_eq_id = tmp2.pre_eq_id(+)  and tmp1.pre_layer_name = tmp2.pre_layer_name(+)  and tmp1.part_name = tmp3.part_name(+)  and tmp1.layer_name = tmp3.layer_name(+)  and tmp1.eq_id = tmp3.eq_id(+)  and tmp1.part_name = tmp4.part_name(+)  and tmp1.layer_name = tmp4.layer_name(+)  and tmp1.eq_id = tmp4.eq_id(+)  and tmp1.pre_eq_id = tmp4.pre_eq_id(+)  ) select  tech_name as a01_tech,  part_name as a02_partid,  layer_name as a03_layer,  decode(layer_name,' ',' ') as a04_dummy,  translate(ol_feedback,',',';') as a05_ovl_feedback,  decode(layer_name,' ',' ') as a06_dummy,  decode(layer_name,' ',' ') as a07_dummy,  ol_expired_hr as a08_ol_expired_hrs,  cd_feedback as a09_cd_feedback,  decode(layer_name,' ',' ') as a10_dummy,  decode(layer_name,' ',' ') as a11_dummy,  r_value as a12_r_value,  e_l_value as a13_e_l_value,  hole_space as a14_type,  cd_expired_hr as a15_cd_expired_hrs,  pre_eq_id as a16_pre_eqid,  eq_id as a17_eqid,  target_value as a18_cd_target,  (case when dose is null then dose_nf else dose end) as a19_dose,  dose_up as a20_dose_up,  dose_low as a21_dose_low,  (case when focus is null then focus_nf else focus end) as a22_focus,  focus_up as a23_focus_up,  focus_low as a24_focsu_low,  (case when offsetx is null then offsetx_nf else offsetx end) as a25_offsetx,  offsetx_up as a26_offsetx_up,  offsetx_low as a27_offsetx_low,  (case when offsety is null then offsety_nf else offsety end) as a28_offsety,  offsety_up as a29_offsety_up,  offsety_low as a30_offsety_low,  (case when scalingx is null then scalingx_nf else scalingx end) as a31_scalingx,  scalingx_up as a32_scalingx_up,  scalingx_low as a33_scalingx_low,  (case when scalingy is null then scalingy_nf else scalingy end) as a34_scalingy,  scalingy_up as a35_scalingy_up,  scalingY_low as a36_scalingy_low,  (case when nonortho is null then nonortho_nf else nonortho end) as a37_nonortho,  nonortho_up as a38_ortho_up,  nonortho_low as a39_ortho_low,  (case when wrotation is null then wrotation_nf else wrotation end) as a40_wrotation,  wrotation_up as a41_rotation_up,  wrotation_low as a42_rotaion_low,  (case when shotscaling is null then shotscaling_nf else shotscaling end) as a43_shotscaling,  shotscaling_up as a44_shotscaling_up,  shotscaling_low as a45_shotscaling_low,  (case when SHOTROTATION is null then SHOTROTATION_nf else SHOTROTATION end) as a46_SHOTROTATION,  SHOTROTATION_UP as a47_shotrotation_up,  SHOTROTATION_low as a48_shotrotation_low,  (case when asymmag is null then asymmag_nf else asymmag end) as a49_asymmag,  asymmag_UP as a50_asymmag_up,  asymmag_low as a51_asymmag_low,  (case when asymrot is null then asymrot_nf else asymrot end) as a52_asymrot,  asymrot_UP as a53_asymrot_up,  asymrot_low as a54_asymrot_low,  translate(lock_lot,',',';') as a55_lock_unlock,  lock_fixed as a56_fixed,  CONSTRAIN as a57_constrain,  (case when dose is null then 'Non_Feedback' end) as a58_cd_flag,  (case when offsetx is null then 'Non_Feedback' end) as a59_ovl_flag    from  final  where  dose is not null or dose_nf is  not null";



            sql = " WITH tmp2 as  ( select  EQ_ID,PART_NAME,LAYER_NAME,PRE_LAYER_NAME,PRE_EQ_ID,DOSE,FOCUS,  tranx OFFSETX,  trany OFFSETY,  wexpanx SCALINGX,  wexpany SCALINGY,  wrotation,  nonortho,  magnification SHOTSCALING,  rotation SHOTROTATION ,  asymmag,asymrot  from  r2rph.tool_param_fb_horiz_view t5  where t5.fb_header in  (  select  max(t6.id)  from  R2RRAW.FB_HEADER t6    where  t6.fb_flag = 'Y'  group by  t6.part_name,  t6.layer_name,  t6.layer_version,  t6.eq_id,  t6.pre_layer_name,  t6.pre_eq_id  )  ),tmp3 as  (  select  EQ_ID,PART_NAME,LAYER_NAME,DOSE_NF,FOCUS_NF,  tranx_nf OFFSETX_NF,  trany_nf OFFSETY_NF,  nonORTHO_NF,  wrotation_nf,  wexpanx_nf SCALINGX_NF,  wexpany_nf SCALINGY_NF,  rotation_nf SHOTROTATION_NF,  magnification_nf SHOTSCALING_NF, asymmag_nf,asymrot_nf  from  r2rph.tool_param_nonfb_horiz_view  ),tmp4 as  (  select  EQ_ID,PART_NAME,LAYER_NAME,  PRE_EQ_ID,  DOSE_UP,FOCUS_UP,  tranx_up OFFSETX_UP,  trany_up OFFSETY_UP,  wexpanx_up SCALINGX_UP,  wexpany_up SCALINGY_UP,  magnification_up SHOTSCALING_UP,  rotation_up SHOTROTATION_UP,  wrotation_up,  nonortho_up,  DOSE_LOW,FOCUS_LOW,  tranx_low OFFSETX_low,  trany_low OFFSETY_low,  wexpanx_low SCALINGX_low,  wexpany_low SCALINGY_low,  magnification_low SHOTSCALING_low,  rotation_low SHOTROTATION_low,  wrotation_low,  nonortho_low,  asymmag_up,asymmag_low,asymrot_up,asymrot_low  from  r2rph.tool_param_spec_horiz_view  ),tmp1 as     (  select  *  from  r2rph.global_conf_overview       ),final as  (  select  TMP1.EQ_ID,  TMP1.TECH_NAME,  TMP1.PART_NAME,  TMP1.LAYER_NAME,  TMP1.CD_FEEDBACK,  TMP1.HOLE_SPACE,  TMP1.LOCK_FIXED,  TMP1.LOCK_LOT,  TMP1.OL_FEEDBACK,  TMP1.PRE_EQ_ID,  TMP1.PRE_LAYER_NAME,  TMP1.CD_EXPIRED_HR,  TMP1.E_L_VALUE,  TMP1.OL_EXPIRED_HR,  TMP1.R_VALUE,  TMP1.TARGET_VALUE,  tmp1.constrain,  TMP2.DOSE,TMP2.FOCUS,TMP2.OFFSETX,TMP2.OFFSETY,TMP2.SCALINGX,TMP2.SCALINGY,TMP2.SHOTSCALING,TMP2.SHOTROTATION,  asymmag,asymrot,wrotation,nonortho,  tmp3.DOSE_NF,tmp3.FOCUS_NF,tmp3.OFFSETX_NF,tmp3.OFFSETY_NF,tmp3.nonORTHO_NF,tmp3.SCALINGX_NF,tmp3.SCALINGY_NF,tmp3.SHOTROTATION_NF,tmp3.SHOTSCALING_NF,  wrotation_nf,asymmag_nf,asymrot_nf,  DOSE_UP,DOSE_LOW,FOCUS_UP,FOCUS_LOW,tmp4.OFFSETX_UP, OFFSETX_LOW,OFFSETY_UP,OFFSETY_LOW,SCALINGX_UP,SCALINGX_LOW,SCALINGY_UP,SCALINGY_LOW,  wrotation_up,wrotation_low,nonortho_up,nonortho_low,  SHOTSCALING_UP,SHOTSCALING_LOW,SHOTROTATION_UP,SHOTROTATION_LOW,  asymmag_up,asymmag_low,  asymrot_up,asymrot_low    from  tmp1,TMP2,tmp3,tmp4  WHERE  tmp1.part_name = tmp2.part_name(+)  and tmp1.layer_name = tmp2.layer_name(+)  and tmp1.eq_id = tmp2.eq_id(+)  and tmp1.pre_eq_id = tmp2.pre_eq_id(+)  and tmp1.pre_layer_name = tmp2.pre_layer_name(+)  and tmp1.part_name = tmp3.part_name(+)  and tmp1.layer_name = tmp3.layer_name(+)  and tmp1.eq_id = tmp3.eq_id(+)  and tmp1.part_name = tmp4.part_name(+)  and tmp1.layer_name = tmp4.layer_name(+)  and tmp1.eq_id = tmp4.eq_id(+)  and tmp1.pre_eq_id = tmp4.pre_eq_id(+)  ),summary as( select  tech_name as a01_tech,  part_name as a02_partid,  layer_name as a03_layer,  decode(layer_name,' ',' ') as a04_dummy,  translate(ol_feedback,',',';') as a05_ovl_feedback,  decode(layer_name,' ',' ') as a06_dummy,  decode(layer_name,' ',' ') as a07_dummy,  ol_expired_hr as a08_ol_expired_hrs,  cd_feedback as a09_cd_feedback,  decode(layer_name,' ',' ') as a10_dummy,  decode(layer_name,' ',' ') as a11_dummy,  r_value as a12_r_value,  e_l_value as a13_e_l_value,  hole_space as a14_type,  cd_expired_hr as a15_cd_expired_hrs,  pre_eq_id as a16_pre_eqid,  eq_id as a17_eqid,  target_value as a18_cd_target,  (case when dose is null then dose_nf else dose end) as a19_dose,  dose_up as a20_dose_up,  dose_low as a21_dose_low,  (case when focus is null then focus_nf else focus end) as a22_focus,  focus_up as a23_focus_up,  focus_low as a24_focsu_low,  (case when offsetx is null then offsetx_nf else offsetx end) as a25_offsetx,  offsetx_up as a26_offsetx_up,  offsetx_low as a27_offsetx_low,  (case when offsety is null then offsety_nf else offsety end) as a28_offsety,  offsety_up as a29_offsety_up,  offsety_low as a30_offsety_low,  (case when scalingx is null then scalingx_nf else scalingx end) as a31_scalingx,  scalingx_up as a32_scalingx_up,  scalingx_low as a33_scalingx_low,  (case when scalingy is null then scalingy_nf else scalingy end) as a34_scalingy,  scalingy_up as a35_scalingy_up,  scalingY_low as a36_scalingy_low,  (case when nonortho is null then nonortho_nf else nonortho end) as a37_nonortho,  nonortho_up as a38_ortho_up,  nonortho_low as a39_ortho_low,  (case when wrotation is null then wrotation_nf else wrotation end) as a40_wrotation,  wrotation_up as a41_rotation_up,  wrotation_low as a42_rotaion_low,  (case when shotscaling is null then shotscaling_nf else shotscaling end) as a43_shotscaling,  shotscaling_up as a44_shotscaling_up,  shotscaling_low as a45_shotscaling_low,  (case when SHOTROTATION is null then SHOTROTATION_nf else SHOTROTATION end) as a46_SHOTROTATION,  SHOTROTATION_UP as a47_shotrotation_up,  SHOTROTATION_low as a48_shotrotation_low,  (case when asymmag is null then asymmag_nf else asymmag end) as a49_asymmag,  asymmag_UP as a50_asymmag_up,  asymmag_low as a51_asymmag_low,  (case when asymrot is null then asymrot_nf else asymrot end) as a52_asymrot,  asymrot_UP as a53_asymrot_up,  asymrot_low as a54_asymrot_low,  translate(lock_lot,',',';') as a55_lock_unlock,  lock_fixed as a56_fixed,  CONSTRAIN as a57_constrain,  (case when dose is null then 'Non_Feedback' end) as a58_cd_flag,  (case when offsetx is null then 'Non_Feedback' end) as a59_ovl_flag    from  final  where  dose is not null or dose_nf is  not null) select * from summary where " + key;

            System.IO.File.WriteAllText(@"C:\temp\sql.txt", sql);




        }


        public static DataTable QueryJobinStation(bool sourceFlag)
        {
            string connStr, sql;
            DataTable dt = new DataTable();

            string toolType = Interaction.InputBox("查询按设备类型区分\r\n\r\n\r\n" +
                "现在请输入设备类型\r\n\r\n\r\n" +
                "   NIKON请输入：N\r\n\r\n\r\n" +
                "   ASML请输入： A", "输入设备类型", "", -1, -1);
            toolType = toolType.Trim().ToUpper();

            if (toolType == "A" || toolType == "N")
            {
            }
            else
            {
                MessageBox.Show("设备类型不对，请重新输入，退出"); return dt;
            }

            //    if (MessageBox.Show("查询筛选条件： " +
            //      "设备类型\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认设备类型是否合适\r\n\r\n" +
            //     "    选择 是（Y） 继续\r\n\r\n" +
            //     "    选择 否（N） 退出\r\n\r\n\r\n" +
            //      "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //    { }
            //    else
            //    { MessageBox.Show("退出，请重新选择设备类型"); return; }







            string tblName = string.Empty;
            string tblName1 = string.Empty;
            string connStr1 = string.Empty;
            if (sourceFlag)
            {
                if (toolType == "N")
                {
                    connStr = @"data source=D:\TEMP\DB\NikonJobinStation.DB";
                    connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
                    tblName = "tbl_NikonJobinStation";
                    tblName1 = "tbl_mask";
                }
                else
                {
                    connStr = @"data source=D:\TEMP\DB\AsmlJobinStation.DB";
                    tblName = "tbl_AsmlJobinStation";
                    connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
                    tblName1 = "tbl_mask";
                }
            }
            else
            {
                if (toolType == "N")
                {
                    connStr = @"data source=P:\_SQLite\NikonJobinStation.DB";
                    tblName = "tbl_NikonJobinStation";
                }
                else
                {
                    connStr = @"data source=P:\_SQLite\AsmlJobinStation.DB";
                    tblName = "tbl_AsmlJobinStation";
                }
            }


            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return dt; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return dt; }
            layer = layer.Trim();
            //make sql
            sql = " SELECT * FROM " + tblName;

            if (part.Length == 0 && layer.Length == 0)
            { MessageBox.Show("Part/Layer必须至少定义一个,退出"); return dt; }

            ///==part
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
                        tmp = " A02_PARTID like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " or A02_PARTID like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 12, 12) == "JobinStation")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }
            //==layer
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
                        tmp = " A03_LAYER like '%" + str + "%' ";
                    }
                    else
                    {
                        if (str.Trim().Length > 0)
                        {
                            tmp += " or A03_LAYER like '%" + str + "%' ";
                        }
                    }
                }
                tmp = "(" + tmp + ")";

                if ((sql.Trim()).Substring((sql.Trim()).Length - 12, 12) == "JobinStation")
                {
                    sql += " WHERE " + tmp;

                }
                else
                {
                    sql += " AND " + tmp;

                }
            }

            MessageBox.Show(sql);
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
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            int rowsCount = dt.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");
            return dt;



        }

        public static DataTable NikonAsmlQueryJobinStation(bool sourceFlag)
        {
            string connStr, sql;
            DataTable dt = new DataTable();

            string part = Interaction.InputBox("按Part，Layer通配符查询数据\r\n\r\n\r\n" +
                "数据同时显示在表格一和表格二\r\n\r\n\r\n" +
                "利用步骤五或步骤六命令可导出R2R Import格式数据\r\n\r\n\r\n\r\n" +
                "现请输入PartID：", "定义产品", "", -1, -1);
            part = part.Trim().ToUpper();

            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            if (!(sourceFlag))
            {
                connStr = @"data source=P:\_SQLite\JobinCdOvl.DB";
            }
            else
            {
                connStr = @"data source=D:\TEMP\DB\JobinCdOvl.DB";
            }

            sql = "SELECT T2.PART_NAME PART,T2.LAYER_NAME LAYER,T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT,T2.WROTATION ROT,T2.SHOTSCALING SMAG,T2.SHOTROTATION SROT,T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET,T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL,T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE T2.PART_NAME LIKE '%" + part + "%' AND T2.LAYER_NAME LIKE '%" + layer + "%'";








            // MessageBox.Show(sql);
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
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            int rowsCount = dt.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");
            return dt;



        }

        public static DataTable QueryCdByTech(string tools, string bDate, string eDate, bool sourceFlag)
        {
            DataTable dt = new DataTable();
            string sql;
            string sql2;
            string connStr;

            //PICK TECH
            string tech = string.Empty;
            tech = Interaction.InputBox("现在请输入工艺代码\r\n\r\n\r\n" +
                    "  输入三位，表示： 工艺代码前三位\r\n\r\n\r\n" +
                    "  输入两位，表示： 工艺代码第二，三位\r\n\r\n\r\n" +
                    "  输入一位，表示： 工艺代码第一位", "定义工艺代码", "", -1, -1);

            tech = tech.Trim().ToUpper();
            if (tech.Length == 1 || tech.Length == 2 || tech.Length == 3) { }
            else { MessageBox.Show("工艺代码输入不正确，退出"); return dt; }


            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return dt; }
            layer = layer.Trim();

            //==tool
            sql = " SELECT LOTID,DCOLL_TIME,JI_TIME,TYPE,TOOL,CDSEM,PART,LAYER,AVG,JOBIN,OPT,FB FROM TBL_CD ";
            if (tools.Length == 0 || tools.Substring(0, 5) == "'ALL'")
            { }
            else
            { sql += " WHERE TOOL IN  (" + tools + ")"; }
            //==layer
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
                            tmp += " OR layer like '%" + str + "%' ";
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
            //选择工艺

            if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
            { sql += " WHERE "; }
            else
            { sql += " AND "; }

            if (tech.Length == 3)
            {
                sql += " PART IN (SELECT PART FROM TBL_CONFIG WHERE TECH='" + tech + "') ";
                sql2 = " SELECT CD,PART,LAYER,FULLTECH,TECH FROM TBL_CONFIG WHERE TECH='" + tech + "'";
            }


            else if (tech.Length == 1)

            {
                sql += " PART IN (SELECT PART FROM TBL_CONFIG WHERE SUBSTR(TECH,2,1)='" + tech + "') ";
                sql2 = " SELECT CD,PART,LAYER,FULLTECH,TECH FROM TBL_CONFIG WHERE SUBSTR(TECH,2,1)='" + tech + "'";
            }

            else
            {


                sql += " PART IN (SELECT PART FROM TBL_CONFIG WHERE SUBSTR(TECH,2,2)='" + tech + "') ";
                sql2 = " SELECT CD,PART,LAYER,LAYER,FULLTECH,TECH FROM TBL_CONFIG WHERE SUBSTR(TECH,2,2)='" + tech + "'";


            }
            //==date
            if ((sql.Trim()).Substring((sql.Trim()).Length - 6, 6) == "TBL_CD")
            {
                sql += " WHERE (DCOLL_TIME BETWEEN '" + bDate + "' and  '" + eDate + "')";

            }
            else
            {
                sql = sql + " AND (DCOLL_TIME BETWEEN '" + bDate + "' and  '" + eDate + "')";

            }

            //combine cd target
            sql = " SELECT a.LOTID,a.PART,a.LAYER,b.CD as Target, b.FULLTECH,b.TECH,a.DCOLL_TIME,a.JI_TIME,a.TYPE,a.TOOL,a.CDSEM,a.AVG,a.JOBIN,a.OPT,a.FB from (" + sql + ") a,(" + sql2 + ") b WHERE a.PART=b.PART and A.LAYER=b.LAYER";


            // MessageBox.Show(sql);
            if (sourceFlag)
            { connStr = @"data source=D:\TEMP\DB\R2R.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\R2R.DB"; }

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
                            dt = ds.Tables[0];
                        }
                    }
                }
            }


            //合并track recipe
            if (dt.Rows.Count == 0) { MessageBox.Show("No Data Selected,Exit"); return dt; }

            if (sourceFlag)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }


            DataTable dtTmp = dt.DefaultView.ToTable(true, new string[] { "PART", "LAYER" });
            sql = "select distinct tooltype TYPE,part,layer,ShortTrackRecipe,substr(mask,1,4) quename from tbl_flowtrack where (part||layer) in (";
            foreach (DataRow row in dtTmp.Rows)
            {
                sql += "'" + row["PART"].ToString() + row["LAYER"].ToString() + "',";
            }
            sql += "' ')";
            DataTable dtTmp1;//trackrecipe,mask name
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
                            dtTmp1 = ds.Tables[0];
                        }
                    }
                }
            }

            dtTmp = null;
            if (dtTmp1.Rows.Count == 0) { MessageBox.Show("No Track Recipe Matched,Exit"); return dt; }
            //合并mask shop
            dtTmp1.Columns.Add("maskshop");

            for (int i = 0; i < dtTmp1.Rows.Count; i++)
            {
                sql = "select distinct maskshop from tbl_mask where quename='" + dtTmp1.Rows[i]["quename"].ToString() + "'";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            dtTmp1.Rows[i]["maskshop"] = rdr[0].ToString();
                        }
                    }
                }
            }
            //合并所有
            //https://www.cnblogs.com/xuxiaona/p/4000344.html   https://www.cnblogs.com/xuke/p/4049427.html
            //https://blog.csdn.net/the_flying_pig/article/details/79568072 以这个为准
            dtTmp = dt.Clone();
            dtTmp.Columns.Add("ShortTrackRecipe");
            dtTmp.Columns.Add("quename");
            dtTmp.Columns.Add("maskshop");
            var query =
               from rHead in dt.AsEnumerable()
               join rTail in dtTmp1.AsEnumerable()
              // on (rHead.Field<string>("TYPE")+ rHead.Field<string>("PART")+ rHead.Field<string>("LAYER")) equals (rTail.Field<string>("TYPE")+ rTail.Field<string>("PART")+ rTail.Field<string>("LAYER"))
              on new { a = rHead.Field<string>("TYPE"), b = rHead.Field<string>("PART"), c = rHead.Field<string>("LAYER") } equals new { a = rTail.Field<string>("TYPE"), b = rTail.Field<string>("PART"), c = rTail.Field<string>("LAYER") }

               select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

            foreach (var obj in query)
            {
                DataRow dr = dtTmp.NewRow();
                dr.ItemArray = obj.ToArray();
                dtTmp.Rows.Add(dr);
            }
            dt = dtTmp;
            MessageBox.Show("共计筛选到" + dt.Rows.Count.ToString() + "行数据");

            return dt;
        }

        public static void CreateLocalDb(bool sourceFlag)
        {
            string connStr = string.Empty;
            string dbName;
            DataTable dt;
            string sql;
            if (MessageBox.Show("执行本命令前，先确认：\r\n\r\n\r\n" +
                 "    使用‘EXTRA'菜单的‘MakeOpasSql’命令生成SQL\r\n\r\n" +
                 "    复制SQL登录10.4.3.130下载相应CSV格式数据\r\n\r\n\r\n" +
                 "该命令是在本地生成如下SQLITE数据源\n\r\n\r" +
                 "    R2R JobinStation\n\r\n\r" +
                 "    R2R CD/OVL\n\r\n\r\n\r\n\r\n\r\n\r" +
                 "注意主菜单选择本地数据源\n\n\n\n" +
                 "选择 是（Y） 继续\r\n\r\n" +
                 "选择 否（N） 退出r\n" +
                 "", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }
            if (sourceFlag)
            {
                File.Copy(@"P:\_SQLite\BlankJobinCdOvl.DB", @"D:\TEMP\DB\JobinCdOvl.DB", true);
                connStr = @"data source=D:\TEMP\DB\JobinCdOvl.DB";
                dbName = @"D:\TEMP\DB\JobinCdOvl.DB";
            }
            else
            {
                File.Copy(@"P:\_SQLite\BlankJobinCdOvl.DB", @"P:\_SQLite\JobinCdOvl.DB", true);
                connStr = @"data source=P:\_SQLite\JobinCdOvl.DB";
                dbName = @"P:\_SQLite\JobinCdOvl.DB";
            }


            MessageBox.Show("根据提示选择R2R CD OVL.CSV文件");
            string src;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"p:\_SQLite\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "R2R CD OVL(*.csv)|*.csv";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                src = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }

            dt = new DataTable(); //r2r cd/ovl
            dt = LithoForm.LibF.OpenCsvWithComma(src);

            DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, "t1");
            myTabInfo.ImportToSqliteBatch(dt, dbName);
            MessageBox.Show("CD/OVL DB Done");
            dt = null;


            MessageBox.Show("根据提示选择JObinStation.CSV文件");
            openFileDialog1.InitialDirectory = @"p:\_SQLite\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "R2R CD OVL(*.csv)|*.csv";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                src = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }

            dt = new DataTable(); //jobin
            dt = LithoForm.LibF.OpenCsvWithComma(src);
            myTabInfo = new DataTableToSQLte(dt, "t2");
            myTabInfo.ImportToSqliteBatch(dt, dbName);
            MessageBox.Show("JOBIN DB Done");
            dt = null;

            string connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr1))
            {
                sql = " SELECT distinct layer,flowtype,tooltype from tbl_layer";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];

                        }
                    }
                }
            }
            myTabInfo = new DataTableToSQLte(dt, "t3");
            myTabInfo.ImportToSqliteBatch(dt, dbName);



        }


        public static DataTable[] ReadFeolBeolData(string choice, bool sourceFlag, string[] cdFlag, string[] tranFlag, string[] expFlag, string[] ortRotFlag, string[] shotFlag)
        {
            DataTable tb1, tb2, tb3;
            tb1 = new DataTable();
            tb2 = new DataTable();
            tb3 = new DataTable();
            DataTable dtTmp;

            string connStr, sql, sql1;

            if (sourceFlag)
            { connStr = @"data source=D:\TEMP\DB\JobinCdOvl.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\JobinCdOvl.DB"; }




            sql = " SELECT AVG,JOBIN,OPT,FB,TECH,TOOL,TYPE,FlowType,PART,T1.LAYER,LOTID,MET_EQ_ID,DCOLL_TIME,JI_TIME,EQP_TYPE,ITEM ";
            if (choice == "AF")
            {
                sql += " FROM T1,T3 WHERE T1.LAYER=T3.LAYER AND T1.TYPE=T3.ToolType AND T3.FLOWTYPE='FEOL' AND T1.TYPE='ASML' ";
                sql1 = "SELECT T2.PART_NAME PART,T2.LAYER_NAME LAYER,T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT,T2.WROTATION ROT,T2.SHOTSCALING SMAG,T2.SHOTROTATION SROT,T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET,T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL,T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE T2.PART_NAME IN ( SELECT DISTINCT PART FROM ( " + sql + ") ) AND SUBSTR(T2.EQ_ID,3,1 )='D' ";
            }
            else if (choice == "AB")
            {
                sql += " FROM T1,T3 WHERE T1.LAYER=T3.LAYER AND T1.TYPE=T3.ToolType AND  T3.FLOWTYPE='BEOL' AND T1.TYPE='ASML' ";
                sql1 = "SELECT T2.PART_NAME PART,T2.LAYER_NAME LAYER,T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT,T2.WROTATION ROT,T2.SHOTSCALING SMAG,T2.SHOTROTATION SROT,T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET,T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL,T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE T2.PART_NAME IN ( SELECT DISTINCT PART FROM ( " + sql + ")) AND SUBSTR(T2.EQ_ID,3,1 )='D' ";

            }
            else if (choice == "NF")
            {
                sql += " FROM T1,T3 WHERE T1.LAYER=T3.LAYER AND T1.TYPE=T3.ToolType AND  T3.FLOWTYPE='FEOL' AND T1.TYPE='NIKON' ";
                sql1 = "SELECT T2.PART_NAME PART,T2.LAYER_NAME LAYER,T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT,T2.WROTATION ROT,T2.SHOTSCALING SMAG,T2.SHOTROTATION SROT,T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET,T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL,T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE T2.PART_NAME IN ( SELECT DISTINCT PART FROM ( " + sql + ")) AND SUBSTR(T2.EQ_ID,3,1 )!='D' ";
            }
            else
            {
                sql += " FROM T1,T3 WHERE T1.LAYER=T3.LAYER AND T1.TYPE=T3.ToolType AND  T3.FLOWTYPE='BEOL' AND T1.TYPE='NIKON' ";
                sql1 = "SELECT T2.PART_NAME PART,T2.LAYER_NAME LAYER,T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT,T2.WROTATION ROT,T2.SHOTSCALING SMAG,T2.SHOTROTATION SROT,T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET,T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL,T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE T2.PART_NAME IN ( SELECT DISTINCT PART FROM ( " + sql + ")) AND SUBSTR(T2.EQ_ID,3,1 )!='D' ";
            }

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
                            tb1 = ds.Tables[0];
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(sql1, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            tb2 = ds.Tables[0];

                        }
                    }
                }
            }

            tb3 = tb1.DefaultView.ToTable(true, new string[] { "PART", "LAYER", "LOTID" });
            tb3.DefaultView.Sort = "PART,LAYER";
            tb3 = tb3.DefaultView.ToTable();
            //加入CD 判断
            if (cdFlag[0] == "True")
            {
                DataRow[] drs = tb1.Select("Item = 'DOSE'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagCd");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();
                    newRow["FlagCd"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString())) / Convert.ToDouble(row["JOBIN"].ToString())) > Convert.ToDouble(cdFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagCd");

                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //translationX flag
            if (tranFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'tran-x'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagTranX");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagTranX"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(tranFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagTranX");






                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //translationY flag
            if (tranFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'tran-y'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagTranY");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagTranY"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(tranFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagTranY");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //expx flag
            if (expFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'exp-x'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagExpX");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagExpX"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(expFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagExpX");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //expy flag
            if (expFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'exp-y'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagExpY");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagExpY"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(expFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagExpY");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //
            //ort flag
            if (ortRotFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'non-ort'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagOrt");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagOrt"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(ortRotFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagOrt");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //w-rot flag
            if (ortRotFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'w-rot'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagRot");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagRot"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(ortRotFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagRot");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //mag flag
            if (shotFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'mag'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagSMag");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagSMag"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(shotFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagSMag");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项

            //rot flag
            if (shotFlag[0] == "True")
            {

                DataRow[] drs = tb1.Select("Item = 'rot'");
                dtTmp = new DataTable();
                dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagSRot");
                foreach (DataRow row in drs)
                {
                    DataRow newRow = dtTmp.NewRow();
                    newRow["PART"] = row["PART"].ToString();
                    newRow["LAYER"] = row["LAYER"].ToString();
                    newRow["LOTID"] = row["LOTID"].ToString();

                    newRow["FlagSRot"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(shotFlag[1])).ToString();
                    dtTmp.Rows.Add(newRow);
                }
                drs = null;
                var query =
              from rHead in tb3.AsEnumerable()
              join rTail in dtTmp.AsEnumerable()

             on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

              select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                DataTable dtTmp1 = tb3.Clone();
                dtTmp1.Columns.Add("FlagSRot");





                foreach (var obj in query)
                {
                    DataRow dr = dtTmp1.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtTmp1.Rows.Add(dr);
                }
                tb3 = dtTmp1.Copy();
                dtTmp = null; dtTmp1 = null;
            }
            tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项


            if (choice == "AF" || choice == "AB")
            {
                //AMag flag
                if (shotFlag[0] == "True")
                {

                    DataRow[] drs = tb1.Select("Item = 'asym-mag'");
                    dtTmp = new DataTable();
                    dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagAMag");
                    foreach (DataRow row in drs)
                    {
                        DataRow newRow = dtTmp.NewRow();
                        newRow["PART"] = row["PART"].ToString();
                        newRow["LAYER"] = row["LAYER"].ToString();
                        newRow["LOTID"] = row["LOTID"].ToString();

                        newRow["FlagAMag"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(shotFlag[1])).ToString();
                        dtTmp.Rows.Add(newRow);
                    }
                    drs = null;
                    var query =
                  from rHead in tb3.AsEnumerable()
                  join rTail in dtTmp.AsEnumerable()

                 on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

                  select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                    DataTable dtTmp1 = tb3.Clone();
                    dtTmp1.Columns.Add("FlagAMag");





                    foreach (var obj in query)
                    {
                        DataRow dr = dtTmp1.NewRow();
                        dr.ItemArray = obj.ToArray();
                        dtTmp1.Rows.Add(dr);
                    }
                    tb3 = dtTmp1.Copy();
                    dtTmp = null; dtTmp1 = null;
                }
                tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项


                //ARot flag
                if (shotFlag[0] == "True")
                {

                    DataRow[] drs = tb1.Select("Item = 'asym-rot'");
                    dtTmp = new DataTable();
                    dtTmp.Columns.Add("PART"); dtTmp.Columns.Add("LAYER"); dtTmp.Columns.Add("LOTID"); dtTmp.Columns.Add("FlagARot");
                    foreach (DataRow row in drs)
                    {
                        DataRow newRow = dtTmp.NewRow();
                        newRow["PART"] = row["PART"].ToString();
                        newRow["LAYER"] = row["LAYER"].ToString();
                        newRow["LOTID"] = row["LOTID"].ToString();

                        newRow["FlagARot"] = (Math.Abs((Convert.ToDouble(row["OPT"].ToString()) - Convert.ToDouble(row["JOBIN"].ToString()))) > Convert.ToDouble(shotFlag[1])).ToString();
                        dtTmp.Rows.Add(newRow);
                    }
                    drs = null;
                    var query =
                  from rHead in tb3.AsEnumerable()
                  join rTail in dtTmp.AsEnumerable()

                 on new { a = rHead.Field<string>("PART"), b = rHead.Field<string>("LAYER"), c = rHead.Field<string>("LOTID") } equals new { a = rTail.Field<string>("PART"), b = rTail.Field<string>("LAYER"), c = rTail.Field<string>("LOTID") }

                  select rHead.ItemArray.Concat(rTail.ItemArray.Skip(3));

                    DataTable dtTmp1 = tb3.Clone();
                    dtTmp1.Columns.Add("FlagARot");





                    foreach (var obj in query)
                    {
                        DataRow dr = dtTmp1.NewRow();
                        dr.ItemArray = obj.ToArray();
                        dtTmp1.Rows.Add(dr);
                    }
                    tb3 = dtTmp1.Copy();
                    dtTmp = null; dtTmp1 = null;
                }
                tb3 = LithoForm.R2R.GetDistinctRows(tb3); //tb3重复项太多，以下删除重复项
            }
            return (new DataTable[] { tb1, tb2, tb3 });
            //return (new DataTable[] { dtTmp, tb2, tb3 });

        }
        public static DataTable GetDistinctRows(DataTable tb3)
        {
            // MessageBox.Show(tb3.Rows.Count.ToString());
            string str = string.Empty;
            for (int ll = 0; ll < tb3.Columns.Count; ll++)
            {
                str += tb3.Columns[ll].ColumnName.ToString();
                str += ",";
            }
            str = str.Substring(0, str.Length - 1);

            tb3 = tb3.DefaultView.ToTable(true, str.Split(new char[] { ',' }));

            // MessageBox.Show(tb3.Rows.Count.ToString());

            return tb3;
        }

        public static DataTable QueryAbnormalJobinstation(bool sourceFlag, string tool, string s1, string s2, string s3, string s4)
        {
            DataTable dt = new DataTable();
            string sql;
            string connStr;
            if (tool == "A")
            {
                sql = "SELECT T2.PART_NAME PART, T2.LAYER_NAME LAYER, T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT, T2.WROTATION ROT, T2.SHOTSCALING SMAG, T2.SHOTROTATION SROT, T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET, T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL, T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE" +
                    "( " +
                    "  abs(offsetx)> " + s1 +
                    " OR abs(offsetx)> " + s1 +
                    " OR abs(SCALINGX)> " + s2 +
                    " OR abs(SCALINGY)> " + s2 +
                    " OR abs(ORT)> " + s3 +
                    " OR abs(ROT)>" + s3 +
                    " OR abs(SMAG)>" + s4 +
                    " OR abs(SROT)>" + s4 +
                    " OR abs(ASYMMAG)>" + s4 +
                    " OR abs(ASYMROT)>" + s4 +
                    " ) " +
                    " AND substr(EQ_ID,3,1)= 'D'";
            }
            else
            {
                sql = "SELECT T2.PART_NAME PART, T2.LAYER_NAME LAYER, T2.EQ_ID,T2.DOSE,T2.FOCUS,T2.OFFSETX,T2.OFFSETY,T2.SCALINGX,T2.SCALINGY, T2.NONORTHO ORT, T2.WROTATION ROT, T2.SHOTSCALING SMAG, T2.SHOTROTATION SROT, T2.ASYMMAG,T2.ASYMROT,T2.TARGET_VALUE TARGET, T2.PRE_EQ_ID,T2.PRE_LAYER_NAME,T2.CD_FEEDBACK,T2.OL_FEEDBACK,T2.CONSTRAIN,T2.E_L_VALUE EL, T2.HOLE_SPACE,T2.LOCK_FIXED,T2.LOCK_LOT FROM T2 WHERE " +
                    " ( " +
                    "  abs(offsetx)> " + s1 +
                    " OR abs(offsetx)> " + s1 +
                    " OR abs(SCALINGX)> " + s2 +
                    " OR abs(SCALINGY)> " + s2 +
                    " OR abs(ORT)> " + s3 +
                    " OR abs(ROT)>" + s3 +
                    " OR abs(SMAG)>" + s4 +
                    " OR abs(SROT)>" + s4 +
                    " ) " +
                    " AND substr(EQ_ID,3,1)<> 'D'";
            }

          

       


            if (sourceFlag)
            {
                connStr = @"data source=D:\TEMP\DB\JobinCdOvl.DB";
            }
            else
            {
                connStr = @"data source=P:\_SQLite\JobinCdOvl.DB";
               
            }




            // MessageBox.Show(sql);
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
                            dt = ds.Tables[0];
                         
                        }
                    }
                }
            }
            return dt;
        }

    }





}

