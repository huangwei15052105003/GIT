using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;


namespace LithoForm
{
    class Flow
    {
        public static DataTable EsfRawData(string connStr)
        {

            string sql; DataTable dt = null;

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT * FROM TBL_ESF ORDER BY EQPID,反向1正向0";


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
            MessageBox.Show("光刻ESF限制_" + dt.Rows.Count.ToString() + "_条");
            return dt;
        }
        public static DataTable TrackRecipe(string connStr)
        {

            string sql; DataTable dt = null;
            MessageBox.Show("查询所有流程的涂胶程序，20多万条，最终显示时较慢");
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT * FROM TBL_FLOWTRACK ORDER BY PART,STAGE";


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
            MessageBox.Show("所有流程涂胶程序_" + dt.Rows.Count.ToString() + "_条");
            return dt;
        }
        public static DataTable FlowQuery(string part)
        {

            string sql; DataTable dt;



            sql = "select a.TECH,a.PARTID,a.STAGE,EQPTYPE,a.TITLE,a.PPID,a.MASK,b.STATUS from";
            sql += "(select substr(TECHNOLOGY,1,5) TECH,PARTID,STAGE,EQPTYPE,TITLE,PPID,MASK,SORTNUM from RPTPRD.MFG_VIEW_FLOW";
            sql += " where partid like '%'||'" + part + "'||'%' ) a,(select * from RPTPRD.RMSRETICLE) b where a.mask=b.reticleid(+) order by a.SORTNUM";

            dt = LithoForm.LibF.CsmcOracle(sql);


            return dt;

        }

        public static void UpdateTechCode(string connStr)
        {
            if (MessageBox.Show("是否需要从OPAS的CD COFIGURATION数据更新产品工艺代码?\r\n\r\n请确认已从OPAS下载最新的CSV格式数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //读取
                string filePath;DataTable dt;
                string sql, dbTblName;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"P:\_SQLite\";
                openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
                if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框

                {
                    filePath = openFileDialog1.FileName.Replace("\\", "\\\\");
                    //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    dt = LibF.OpenCsv(filePath);
              
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
                    DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, dbTblName);
                    myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
                    MessageBox.Show("CD Config Update Done");
                  
                }
                else
                {
                    //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    MessageBox.Show("未选择文件，数据未更新");
                }




            }
            else
            {
                MessageBox.Show("Exit，No Update");
            }

        }
        public static void UpdateMask(string connStr)
        {
            if (MessageBox.Show("是否需要更新OPAS的Mask数据?\r\n\r\n请确认已从OPAS下载最新的CSV格式数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //读取
                string filePath; DataTable dt;
                string sql, dbTblName;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"P:\_SQLite\";
                openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
                if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框

                {
                    filePath = openFileDialog1.FileName.Replace("\\", "\\\\");
                    //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    dt = LibF.OpenCsv(filePath);

                    //删除旧数据
                    dbTblName = "tbl_mask";
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
                    DataTableToSQLte myTabInfo = new DataTableToSQLte(dt, dbTblName);
                    myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
                    MessageBox.Show("Mask Information Update Done");

                }
                else
                {
                    //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    MessageBox.Show("未选择文件，数据未更新");
                }




            }
            else
            {
                MessageBox.Show("Exit，No Update");
            }

        }

        public static void UpdateEsfTrack(string connStr)
        {
            MessageBox.Show("Update ESF , Track Recipe,Mask Name");
            string sql,sql1,sql2,sql3,sql4; DataTable dt; DataTableToSQLte myTabInfo; string dbTblName;
            #region
            //===ESF
            sql = " select PARTTITLE TECH, PART, STAGE, RECIPE, PPID, EQPID, FLAG Yes1No0, TYPE 反向1正向0, ACTIVEFLAG, FAILREASON, EXPIRETIME, CREATEUSER, USERDEPT, CREATETIME, REQUESTUSER  from rptprd.processconstraint A where ACTIVEFLAG='Y' and (substr(eqpid,1,4) in('ALCT', 'BLCT', 'ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI', 'ALTI', 'BLTI', 'ALTD', 'BLTD','ALSD','BLSD','ALCD','BLCD','ALOL','BLOL') ) order by eqpid,parttitle,part,stage";
            dt = LithoForm.LibF.CsmcOracle(sql);
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
             myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");

            //ESF for CHECk
            sql = " select '^'||REPLACE(PARTTITLE,'_','[A-Z0-9]{1}') TECH, '^'||REPLACE(PART,'_','[A-Z0-9]{1}') PART,'^'||REPLACE(RECIPE,'_','[A-Z0-9]{1}') RECIPE,'^'||REPLACE(STAGE,'_','[A-Z0-9]{1}') STAGE, EQPID, FLAG, TYPE ,SUBSTR(EQPID,3,1) EQPTYPE,FAILREASON from rptprd.processconstraint  where ACTIVEFLAG='Y' and substr(eqpid,1,4) in('ALDI', 'BLDI', 'ALII','BLII', 'ALSI', 'BLSI') order by eqpid,parttitle,part,stage";
            sql = "SELECT REPLACE(TECH,'%','\\S*')||'$' TECH,REPLACE(PART,'%','\\S*')||'$' PART,REPLACE(RECIPE,'%','\\S*')||'$' RECIPE,REPLACE(STAGE,'%','\\S*')||'$' STAGE,EQPID,FLAG,TYPE,EQPTYPE,FAILREASON FROM (" + sql + ")";
            dt = LithoForm.LibF.CsmcOracle(sql);
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
            myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, @"P:\_SQLite\ReworkMove.DB");
            #endregion

            #region //trackrecipe
            //Nikon offline 流程
            sql1 = "select RECPID,DECODE(EQPTYPE,'LCT','NIKON') AS TOOLTYPE,    STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,PPID  from  RPTPRD.MFG_VIEW_FLOW ";
            sql1 = sql1 + " where eqptype='LCT' and substr(partid,1,1) not in ('8','2','1') ";// order by part";


            sql2 = "select STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,substr(ppid,instr(ppid,'.')+1,2) LAYER,Mask  from  RPTPRD.MFG_VIEW_FLOW WHERE EQPTYPE='LSI'";
            sql3 = "select a.tooltype,a.stage,a.part,a.ppid trackrecipe,b.layer,b.mask from (" + sql1 + ") a,(" + sql2 + ") b where a.part=b.part and a.stage=b.stage";


            //inline flow
            sql4 = "select DECODE(EQPTYPE,'LDI','ASML','LII','NIKON') AS TOOLTYPE,    STAGE,substr(PARTID,1,instr(partid,'.')-1) PART,substr(PPID,1,instr(ppid,';')-1) TrackRecipe,substr(ppid,instr(ppid,'.')+1,2) LAYER,mask from  RPTPRD.MFG_VIEW_FLOW ";
            sql4 = sql4 + " where eqptype in ('LDI','LII') and substr(partid,1,1) not in ('8','2','1')";// order by part";





            sql = "Select distinct * from ( " + sql4 + " union (" + sql3 + ") )   ORDER BY TOOLTYPE,PART ";
            sql = "select tooltype,stage,part,trackrecipe,layer,mask,substr(trackrecipe,1,3) shorttrackrecipe from (" + sql + ") where substr(trackrecipe,1,2) not in ('A0','S0','T1') order by part,layer";



            connStr = "data source=P:\\_SQLite\\ReworkMove.db";
           dbTblName = "tbl_flowtrack";

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }
            //get data from oracle
            dt = LithoForm.LibF.CsmcOracle(sql);
            //save to db
             myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");
#endregion

            #region //inline wip


            sql = " Select distinct partname part from rptprd.sdb_view_info_wip";
            sql += " where substr(lottype,1,1) in ('M','N','P','E')";
            sql += "  and STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')";
            sql += " and location not like '%bank'";
            sql += " and substr(partname,1,1) not in ('1','2','8')";


            //wip
            dbTblName = "tbl_realwip";
            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            dt = LithoForm.LibF.CsmcOracle(sql);
            //save to db
            myTabInfo = new DataTableToSQLte(dt, dbTblName);
            myTabInfo.ImportToSqliteBatch(dt, "P:\\_SQLite\\ReworkMove.db");


            #endregion

            /*
             WIP

                sql = "select  distinct (PARTID || '_'|| STAGE||'_'||PPID) as key from  RPTPRD.MFG_VIEW_FLOW Where ( EQPTYPE like '%'||'LII'||'%' or EQPTYPE like '%'||'LDI'||'%')"

    sql = " Select distinct partname from rptprd.sdb_view_info_wip"
    
    
    
    
sql = " select * from(select b.stage_location LOC ,nvl(d.processtype_master,'F2OTHER')  TECH,c.PROCESSTYPE,a.partname part,c.VFLAG,a.lotid,"
sql = sql & "  a.PRIORITY P,a.qty Q,a.status,decode(a.lottype,'ET','FAB','EP','FAB','CUST') CUST ,a.lottype LT,"
sql = sql & " a.stage,a.eqptype,a.eqpid,a.ppid,round((sysdate-a.stateentrytime)*24,1) DUR,"
sql = sql & " c.tot_layers TL,c.remain_layers RL,c.remain_stages RS,"
sql = sql & " c.starttime WS ,"
sql = sql & " c.reqdtime CVP,"
sql = sql & " c.foredate FST,"
sql = sql & " a.dept1||a.owner owner,"
sql = sql & " a.HOLDCOMMENT"
sql = sql & " from rptprd.sdb_view_info_wip a,"
sql = sql & " rptprd.FAB2_RPT_MFG_STAGE_MAINTAIN b,"
sql = sql & " rptprd.pc_wip_forecast C,"
sql = sql & " rptprd.pc_processtype_master d"
sql = sql & " where substr(a.lottype,1,1) in ('M','N','P','E')"
sql = sql & " and a.lottype!='MW'"
sql = sql & " and a.STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')"
'sql = sql & " and a.LOCATION Not like '%BANK%'"
sql = sql & " and a.location='PHOTO'"
sql = sql & " and a.stage like '%PH'"
sql = sql & " and a.eqptype in ('LDI','LSI','LII')"
sql = sql & " and a.status <> 'RUN'"

sql = sql & " and a.stage=b.stageid(+)"
sql = sql & " and a.lotid=C.lotID(+)"
sql = sql & " and a.technology=d.processtype(+))"



sql = " Select distinct partname part from rptprd.sdb_view_info_wip"
sql = sql & " where substr(lottype,1,1) in ('M','N','P','E')"
sql = sql & "  and STATUS Not In ('COMPLT', 'TRAN', 'FINISH', 'SCHED')"
sql = sql & " and location not like '%bank'"
sql = sql & " and substr(partname,1,1) not in ('1','2','8')"
            */


        }
    }
}
