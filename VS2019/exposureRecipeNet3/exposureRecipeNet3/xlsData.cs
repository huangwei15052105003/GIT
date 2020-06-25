using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace exposureRecipeNet3
{
    public class xlsData
    {
        public static string flowPath = "P:\\_flow\\";
        public static string btPath = "P:\\Recipe\\Biastable\\";
        public static string zbPath = "P:\\Recipe\\Coordinate\\";
        public static string recipePath = "P:\\Recipe\\Recipe\\";
        public string part;
        public string fullTech;


        public DataTable dtFlow;// = new DataTable();
        public DataTable dtBt;// = new DataTable();
        public DataTable dtZb;// = new DataTable();
        public DataTable dt0;// = new DataTable();//in between dtFlow / dt;
        public DataTable dt;// = new DataTable();//dtFlow revised
        public DataTable dt1;// = new DataTable();//dtBt revised
        public DataTable dt2; //coordinated
        public bool narrowScribelaneFlag = false;

        public xlsData(string str)
        {
            part = str;
        }
        public xlsData(string str, string dummy)
        {
            part = str;

            string excelStr, sql;
            //readflow
            if (File.Exists(flowPath + part + ".xls"))
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flowPath + part + ".xls" + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
                try
                {
                    using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                    {
                        OleConn.Open();
                        sql = string.Format("SELECT * FROM  [{0}$]", part);

                        using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dtFlow = ds.Tables[0];
                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("没有工作表名为 " + part + " 的表格，请确认");
                }
            }
            else
            {
                MessageBox.Show("流程文件不存在");
            }
            //read biastable
            if (File.Exists(btPath + part + ".xls"))
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + btPath + part + ".xls" + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
                try
                {
                    using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                    {
                        OleConn.Open();
                        sql = string.Format("SELECT * FROM  [{0}$]", part);

                        using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dtBt = ds.Tables[0];


                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("没有工作表名为 " + part + " 的表格，请确认");
                }
            }
            else
            {
                MessageBox.Show("BiasTable 文件不存在");
            }
            //get coordinate


            if (System.IO.File.Exists(zbPath + part + ".txt"))
            {
                dtZb = new DataTable();
                dtZb.Columns.Add("mark");
                dtZb.Columns.Add("layer");
                dtZb.Columns.Add("lines");
                dtZb.Columns.Add("X");
                dtZb.Columns.Add("Y");
                using (StreamReader sr = new StreamReader(zbPath + part + ".txt"))
                {
                    string line, str1;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if ((line.Contains("WY") || line.Contains("WX") || line.Contains("SPM") || line.Contains("NAA157") || line.Contains("NAH") || line.Contains("FIA") || line.Contains("LSA")) && (!line.Contains("DRC")))
                        {
                            var x = line.Split(new char[] { '\t' });
                            DataRow newRow = dtZb.NewRow();
                            str1 = x[0];
                            if (str1.Contains("X"))
                            {
                                str1 = str1.Replace("X", "");
                                str1 = str1 + "X";
                            }
                            else if (str1.Contains("Y"))
                            {
                                str1 = str1.Replace("Y", "");
                                str1 = str1 + "Y";
                            }

                            try
                            {
                                if (x.Length == 7)
                                {
                                    newRow["mark"] = str1;
                                    newRow["layer"] = x[1].Trim();
                                    newRow["lines"] = x[2].Trim();
                                    newRow["X"] = x[4].Trim();
                                    newRow["Y"] = x[6].Trim();
                                }
                                else //DMOS TR60坐标特殊
                                {
                                    newRow["mark"] = str1;
                                    newRow["layer"] = x[1].Trim();
                                    newRow["lines"] = x[2].Trim();
                                    newRow["X"] = x[5].Trim();
                                    newRow["Y"] = x[7].Trim();

                                }
                            }
                            catch
                            {
                                MessageBox.Show("请确认坐标数据格式是否正确");
                                //MessageBox.Show(line);    
                                //MessageBox.Show(strAry.Length.ToString());
                                newRow["mark"] = str1;
                                newRow["layer"] = x[1].Trim();
                                newRow["lines"] = x[2].Trim();

                                int markXY = 0;
                                for (int m = x.Length - 1; m > 0; m--)
                                {
                                    if (x[m].Trim().Length > 0)
                                    {
                                        if (markXY == 0)
                                        {
                                            newRow["Y"] = x[m].Trim();
                                            markXY += 1;
                                        }
                                        else if (markXY == 1)
                                        {
                                            newRow["X"] = x[m].Trim();
                                            break;
                                        }
                                    }
                                }

                            }
                            dtZb.Rows.Add(newRow);


                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("坐标文件不存在，未读取");
            }

        }

        public bool FlowToDataTable()
        {
        

            string eqptype, oldPpid, newPpid, ppid, stage;
            dt = new System.Data.DataTable();
            dt.Columns.Add("stage", Type.GetType("System.String"));
            dt.Columns.Add("eqptype", Type.GetType("System.String"));
            dt.Columns.Add("ppid", Type.GetType("System.String"));
            dt.Columns.Add("track", Type.GetType("System.String"));

            bool stepFlag, stageFlag, recpFlag, ppidFlag;
            stepFlag = stageFlag = recpFlag = ppidFlag = false;
            int no1 = 0; int no2 = 0; int no3 = 0; int no4 = 0;


            foreach (DataRow row in dtFlow.Rows)
            {

                for (int i = 0; i < dtFlow.Columns.Count; i++)
                {
                    if (row[i].ToString().ToUpper().Trim().Contains("步骤")) { stepFlag = true; no1 = i; }
                    if (row[i].ToString().ToUpper().Trim().Contains("STAGE")) { stageFlag = true; no2 = i; }
                    if (row[i].ToString().ToUpper().Trim().Contains("RECPNAME")) { recpFlag = true; no3 = i; }
                    if (row[i].ToString().ToUpper().Trim().Contains("PPID")) { ppidFlag = true; no4 = i; }
                    if (stepFlag && stageFlag && recpFlag && ppidFlag & no1 > 0) { break; }
                }

            }
            if (stepFlag && stageFlag && recpFlag && ppidFlag & no1 > 0)
            { }
            else
            {
                MessageBox.Show("未找到 \"步骤\" \"STAGE\" \"RECPNAME\" \"PPID\"等关键字，退出，请确认流程是否正确");
                return false;

            }

            // MessageBox.Show(no1.ToString() + "," + no2.ToString() + "," + no3.ToString() + "," + no4.ToString());



            foreach (DataRow item in dtFlow.Rows)
            {

                try
                {
                    eqptype = (item[no3].ToString()).Substring(0, 3);

                    if ((eqptype == "LDI" || eqptype == "LII") && (item[no1 - 1].ToString().Length == 0 || item[no1 - 1].ToString().Substring(0, 1) != "删"))
                    {
                        stage = item[no2].ToString().ToUpper().Trim();
                        oldPpid = item[no4].ToString().ToUpper().Trim();

                        newPpid = item[no4 + 1].ToString().ToUpper().Trim();

                        if (newPpid == "")
                        { ppid = oldPpid; }
                        else
                        { ppid = newPpid; }

                        DataRow newRow = dt.NewRow();
                        newRow["stage"] = stage;
                        newRow["eqptype"] = eqptype;

                        newRow["ppid"] = ppid.Split(new char[] { '.' })[1];
                        if (eqptype == "LDI")
                        { newRow["track"] = ppid.Split(new char[] { ';' })[0]; }
                        else
                        { newRow["track"] = ""; }

                        dt.Rows.Add(newRow);




                    }
                }
                catch //eqptye length=0
                {
                }
            }
            DataView dv = dt.DefaultView;
            dt = dv.ToTable("Dist", true, "stage", "eqptype", "ppid", "track");

            dt0 = dt.Copy();//仅作中途显示

            return true;
        }
        public bool BiastableToDataTable() //从excle读取biastable后，选取有用数据
        {





            //读取biastable
            dt1 = new System.Data.DataTable();
            dt1.Columns.Add("code", Type.GetType("System.String"));
            dt1.Columns.Add("ppid", Type.GetType("System.String"));
            dt1.Columns.Add("mask", Type.GetType("System.String"));
            dt1.Columns.Add("mlm", Type.GetType("System.String"));
            dt1.Columns.Add("maskLabel", Type.GetType("System.String"));
            dt1.Columns.Add("eqptype", Type.GetType("System.String"));
            dt1.Columns.Add("ovlto", Type.GetType("System.String"));

            string mask, code, ppid, mlm, maskLabel, eqptype, ovlto;
            int n = 0;
            foreach (DataRow item in dtBt.Rows)
            {
               
                n++;
                try
                {

                    mask = item[4].ToString().Trim().ToUpper();


                    if (((mask.Length == 8 || mask.Length == 12) && mask.Substring(4, 1) == "-") || mask.Contains("ZERO"))
                    {

                        // MessageBox.Show(mask+","+ item[0].ToString().Trim().ToUpper()+","+ item[3].ToString().Trim().ToUpper()+","+ item[5].ToString().Trim().ToUpper()
                        //     +","+ item[6].ToString().Trim().ToUpper()+"," + item[17].ToString().Trim().ToUpper()+","+item[19].ToString().Trim().ToUpper());

                        code = item[0].ToString().Trim().ToUpper();
                        ppid = item[3].ToString().Trim().ToUpper();
                        mlm = item[5].ToString().Trim().ToUpper();
                        maskLabel = item[6].ToString().Trim().ToUpper();

                        if (maskLabel.Contains("-"))
                        { maskLabel = maskLabel.Split(new char[] { '-' })[1].Trim().Substring(0, 2); }
                        if (maskLabel.Length > 3 && mask != "FAB2-ZERO1")
                        { maskLabel = maskLabel.Substring(0, 2); }



                        eqptype = item[17].ToString().Trim().ToUpper();
                        ovlto = item[19].ToString().Trim().ToUpper();



                        DataRow newRow = dt1.NewRow();
                        newRow["code"] = code;
                        newRow["ppid"] = ppid;
                        newRow["mask"] = mask;
                        newRow["mlm"] = mlm;
                        newRow["maskLabel"] = maskLabel;
                        newRow["eqptype"] = eqptype;
                        newRow["ovlto"] = ovlto;
                        dt1.Rows.Add(newRow);

                        fullTech = dt1.Rows[0][0].ToString();

                    }
                   
                   
                    

                    
                   
                   
                    

                    

                }
                catch (Exception ex)
                {
                    ;
                    MessageBox.Show("Error Code: \n\n" + ex.Message + "\n\n\nBias Table未正确读取，退出，请确认");
                    return false;
                }

            }
        
            return true;
            





        }
        public void MergeFlowBiasTable()
        {
            if (dt1.Rows.Count != dt.Rows.Count)
            {
                MessageBox.Show("流程:      " + dt.Rows.Count.ToString() + "层\r\n\r\n" +
                                "BiasTable: " + dt1.Rows.Count.ToString() + "层\r\n\r\n" +
                                "工艺层次数量不匹配,退出,请重新确认"); return;
            }

            dt.Columns.Add("code");
            dt.Columns.Add("mask");
            dt.Columns.Add("mlm");
            dt.Columns.Add("maskLabel");
            dt.Columns.Add("ovlto");
            dt.Columns.Add("toFlag");
            dt.Columns.Add("zaFlag");
            dt.Columns.Add("illumination");
            string toFlag = "N";
            string zaFlag = "N";
            bool flag;//判断flow中的PPID是否和Bias table中的PPID匹配

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                flag = false;

                foreach (DataRow item in dt1.Rows)
                {

                    if (dt.Rows[i]["ppid"].ToString() == item["ppid"].ToString())
                    {
                        dt.Rows[i]["code"] = item["code"].ToString();
                        dt.Rows[i]["mask"] = item["mask"].ToString();
                        dt.Rows[i]["mlm"] = item["mlm"].ToString();
                        dt.Rows[i]["maskLabel"] = item["maskLabel"].ToString();
                        dt.Rows[i]["ovlto"] = item["ovlto"].ToString();
                        dt.Rows[i]["toFlag"] = toFlag;
                        dt.Rows[i]["zaFlag"] = zaFlag;
                        if (dt.Rows[i]["eqptype"].ToString() == "LDI")
                        {
                            dt.Rows[i]["illumination"] = pubfunction.GetNaSigma(item["code"].ToString(), dt.Rows[i]["ppid"].ToString(), dt.Rows[i]["track"].ToString());
                        }
                        else
                        {
                            dt.Rows[i]["illumination"] = "";
                        }
                        if (dt.Rows[i]["ppid"].ToString() == "TO") { toFlag = "Y"; }
                        if (dt.Rows[i]["ppid"].ToString() == "ZA") { zaFlag = "Y"; }
                        flag = true;
                        break;
                    }
                }
                if (flag == false) { MessageBox.Show("Bias Table中的PPID和流程中的PPID不匹配，脚本无法正常运行,退出"); return; }
            }

            // AlignMethod();
            //  dataGridView1.DataSource = dt;



        }

        public void AlignMethod()
        {



            if (dtZb is null)
            {
                MessageBox.Show("坐标文件未成功读取，无法判断产品是否是Narrow Scribelane;\n\n判断标准不再以产品名判断，以对位坐标前缀判断\n\n以下进行人工直接定义：");



                if (MessageBox.Show("坐标文件不存在,请手动设定产品是否为60um划片槽\n\nYes(是）-->是60um划片槽\r\n\r\nNo (否）-->非60um划片槽", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    narrowScribelaneFlag = true;
                }
                else
                { narrowScribelaneFlag = false; }
            }
            else
            {
                foreach (DataRow row in dtZb.Rows)
                {
                    if (row[0].ToString().Substring(0, 1) == "N")
                    {
                        narrowScribelaneFlag = true;
                        break;
                    }
                }

            }







            dt.Columns.Add("method");

            string tech, layer, tool;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                tech = dt.Rows[i]["code"].ToString().Substring(0, 3).Trim().ToUpper();
                layer = dt.Rows[i]["ppid"].ToString().Trim().ToUpper();
                tool = dt.Rows[i]["eqptype"].ToString().Trim().ToUpper();
                if (tool == "LII")
                {
                    #region
                    if (tech.Substring(0, 1) == "T") //DMOS
                    {

                        if (layer == "CP" || layer == "TT") { dt.Rows[i]["method"] = "FIA/FIA"; }
                        else if (layer == "SN" || layer == "SP") { dt.Rows[i]["method"] = "LSA/FIA"; }
                        else { dt.Rows[i]["method"] = "LSA/LSA"; }

                    }
                    else //NonDMOS
                    {
                        if (layer.Substring(0, 1) == "W" || layer.Substring(0, 1) == "A" || layer == "CT")
                        { dt.Rows[i]["method"] = "LSA/FIA"; }
                        else if (layer == "TT")
                        { dt.Rows[i]["method"] = "FIA/FIA"; }
                        else
                        {
                            string[] mylayers = { "CP", "PN", "CF", "PI", "P2" };
                            if (mylayers.Contains(layer))
                            { dt.Rows[i]["method"] = "LSA/LSA"; }
                            else
                            {
                                if (tech == "M52" || tech.Substring(1, 1) == "1" || tech == "CAF")
                                {
                                    if (layer == "RE" || dt.Rows[i]["toFlag"].ToString() == "N" || layer == "TO")
                                    { dt.Rows[i]["method"] = "LSA/LSA"; }
                                    else
                                    {
                                        if (narrowScribelaneFlag)   //narrow mark 用FIA
                                        {
                                            dt.Rows[i]["method"] = "FIA/FIA";
                                        }
                                        else
                                        {
                                            dt.Rows[i]["method"] = "LSA/FIA";
                                        }
                                    }
                                }
                                else
                                { dt.Rows[i]["method"] = "LSA/LSA"; }
                            }

                        }
                    }
                    #endregion
                }
                else //LDI
                {

                    string[] myHoleLayers = { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "WT" };
                    string[] myMetalLayers = { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "AT", "CT", "OE", "OV" };

                    //  if (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") //narrow mark
                    if (narrowScribelaneFlag) //从坐标文件，判断narrow mark
                    {
                        if (tech.Substring(1, 1) == "1" && myHoleLayers.Contains(layer))
                        { dt.Rows[i]["method"] = "AA157"; }
                        else if (tech.Substring(1, 1) == "1" && myMetalLayers.Contains(layer))
                        { dt.Rows[i]["method"] = "AH325374"; }
                        else
                        { dt.Rows[i]["method"] = "AH53"; }
                    }



                    else
                    { dt.Rows[i]["method"] = "AH53"; }
                }

            }


        }
        public  void AlignTo() //定义alignment tree
        {
            dt.Columns.Add("alignto");
            //      T_Nikon= {'RN':'TO','SN':'TR','TT':'W1','CP':'A1'}


            // H_Asml = {"W2":"W1",'W3':'W2','W4':'W3','W5':'W4','W6':'W5','W7':'W6',
            //          "A1":"W1",'A2':'W2','A3':'W3','A4':'W4','A5':'W5','A6':'W6'}
            Dictionary<string, string> myDictN = new Dictionary<string, string>();
            Dictionary<string, string> myDictA = new Dictionary<string, string>();

            myDictN.Add("W2", "A1"); myDictN.Add("W3", "A2"); myDictN.Add("A1", "W1"); myDictN.Add("A2", "W2");
            myDictN.Add("NP", "GT"); myDictN.Add("PP", "GT"); myDictN.Add("W4", "A3"); myDictN.Add("A3", "W3");
            myDictN.Add("A4", "W4"); myDictN.Add("A5", "W5");

            myDictA.Add("W2", "W1"); myDictA.Add("W3", "W2"); myDictA.Add("W4", "W3"); myDictA.Add("W5", "W4");
            myDictA.Add("W6", "W5"); myDictA.Add("W7", "W6"); myDictA.Add("A1", "W1"); myDictA.Add("A2", "W2");
            myDictA.Add("A3", "W3"); myDictA.Add("A4", "W4"); myDictA.Add("A5", "W5"); myDictA.Add("A6", "W6");
            myDictA.Add("A7", "W7");
            //https://www.cnblogs.com/ChenMM/p/9479987.html
            string[] layersN = { "W2", "W3", "A1", "A2", "A3", "A4", "A5", "W4", "NP", "PP" };
            string[] layersA = { "W2", "W3", "W4", "W5", "W6", "W7", "A1", "A2", "A3", "A4", "A5", "A6", "A7" };


            for (int i = 1; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["code"].ToString().Substring(0, 1) == "T") //DMOS
                {
                    #region //DMOS
                    //  if (dt.Rows[i]["ppid"].ToString() == "RN")
                    //  { dt.Rows[i]["alignto"] = "TO"; }
                    if (dt.Rows[i]["ppid"].ToString() == "SN" || dt.Rows[i]["ppid"].ToString() == "W1")
                    { dt.Rows[i]["alignto"] = "TR"; }
                    else if (dt.Rows[i]["ppid"].ToString() == "TT")
                    { dt.Rows[i]["alignto"] = "W1"; }
                    else if (dt.Rows[i]["ppid"].ToString() == "CP")
                    { dt.Rows[i]["alignto"] = "A1"; }
                    else
                    {
                        if (dt.Rows[i]["ovlto"].ToString().Trim().Length > 1)
                        {
                            dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2);
                        }
                        else
                        {

                            MessageBox.Show("DMOS产品Align Tree的层次不完善，请通知更改");
                        }
                    }
                    #endregion
                }

                else //NON-DMOS
                {
                    string str1 = dt.Rows[i]["ppid"].ToString();

                    //MessageBox.Show(str1);

                    if (dt.Rows[i]["eqptype"].ToString() == "LII") //Nikon
                    {
                        #region //NIkon
                        if (layersN.Contains(str1)) //metal,hole,NP,PP
                        { dt.Rows[i]["alignto"] = myDictN[str1]; }
                        else if (str1 == "CT")
                        {
                            if (i >= 1)
                            {
                                if (dt.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 2)
                            {
                                if (dt.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (dt.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "WT")
                        {
                            if (i >= 1)
                            {
                                if (dt.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 2)
                            {
                                if (dt.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (dt.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "A")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "AT" || str1 == "TT")
                        {
                            if (i >= 1)
                            {
                                if (dt.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 2)
                            {
                                if (dt.Rows[i - 2]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                            else if (i >= 3)
                            {
                                if (dt.Rows[i - 3]["ppid"].ToString().Trim().Substring(0, 1) == "W")
                                { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            }
                        }
                        else if (str1 == "CP" || str1 == "CF" || str1 == "PI" || str1 == "P2" || str1 == "PN")
                        {

                            if (dt.Rows[i - 1]["ppid"].ToString().Trim().Substring(0, 1) == "A" || dt.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "T")
                            { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                            else
                            {
                                if (i >= 2)
                                {
                                    if (dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "A" || dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "T")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                            }
                        }
                        //目前没有规定 SN OVL 量到 GT，必须对GT
                        //else if (dt.Rows[i]["ovlto"].ToString().Length > 1)
                        //{ dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString(); }
                        else if (dt.Rows[i]["toFlag"].ToString() == "N" && dt.Rows[i]["zaFlag"].ToString() == "Y")
                        { dt.Rows[i]["alignto"] = "ZA"; }
                        else if (str1 == "TO" && dt.Rows[i]["zaFlag"].ToString() == "N" && dt.Rows[i]["ovlto"].ToString().Length > 0)
                        { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }

                        else if (dt.Rows[i]["toFlag"].ToString() == "Y")
                        { dt.Rows[i]["alignto"] = "TO"; }
                        else
                        {
                            dt.Rows[i]["alignto"] = dt.Rows[0]["maskLabel"].ToString().Trim().Substring(0, 2);
                            MessageBox.Show("无ZA层，TO及之前的层次，统一对位到第一层\r\n\r\nNikon Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                        }
                        #endregion

                    }

                    else //ASML
                    {
                        #region //ADVANCED ASML
                        if (dt.Rows[i]["code"].ToString().Substring(1, 1) == "1")
                        {
                            if (layersA.Contains(str1)) //Asml Metal,Hole       
                            { dt.Rows[i]["alignto"] = myDictA[str1]; }
                            else if (str1 == "AT" || str1 == "TT" || str1 == "WT" || str1 == "CT" || str1 == "OE" || str1 == "OV") //-->OE，OV可能变化
                            {

                                try
                                {
                                    if (dt.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                    else if (dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                    else if (dt.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                {

                                    dt.Rows[i]["alignto"] = "请修改脚本";

                                }

                            }
                            else if (str1 == "W1")
                            {
                                if (dt.Rows[i]["code"].ToString().Substring(0, 3) == "C18")
                                { dt.Rows[i]["alignto"] = "TO"; }
                                else if (dt.Rows[i]["ovlto"].ToString().Length > 1)
                                { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                                else
                                { dt.Rows[i]["alignto"] = "TO"; }
                            }
                            //目前没有规定 SN OVL 量到 GT，必须对GT
                            // else if (dt.Rows[i]["ovlto"].ToString().Length > 1)
                            // { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString(); }
                            else if (dt.Rows[i]["toFlag"].ToString() == "N" && dt.Rows[i]["zaFlag"].ToString() == "Y")
                            { dt.Rows[i]["alignto"] = "ZA"; }
                            else if (str1 == "TO" && dt.Rows[i]["zaFlag"].ToString() == "N" && dt.Rows[i]["ovlto"].ToString().Length > 0)
                            { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                            else if (dt.Rows[i]["toFlag"].ToString() == "Y")
                            { dt.Rows[i]["alignto"] = "TO"; }
                            else
                            {
                                dt.Rows[i]["alignto"] = dt.Rows[0]["maskLabel"].ToString();
                                MessageBox.Show("TO及之前的层次，统一对位到第一层\r\n\r\nAsml Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                            }

                        }
                        #endregion
                        #region //lowend ASML,almost copy from Nikon Setting (HOLE align to Metal)
                        else
                        {


                            if (layersN.Contains(str1)) //metal,hole,NP,PP
                            { dt.Rows[i]["alignto"] = myDictN[str1]; }
                            else if (str1 == "W1")
                            {
                                if (dt.Rows[i]["code"].ToString().Substring(0, 3) == "C18") //copy from advanced process，to be delete
                                { dt.Rows[i]["alignto"] = "TO"; }
                                else if (dt.Rows[i]["ovlto"].ToString().Length > 1)
                                { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString().Trim().Substring(0, 2); }
                                else
                                { dt.Rows[i]["alignto"] = "TO"; }
                            }
                            else if (str1 == "CT")
                            {
                                try
                                {

                                    if (dt.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (dt.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                {
                                    dt.Rows[i]["alignto"] = "请修改脚本";
                                }

                            }
                            else if (str1 == "WT")
                            {
                                try
                                {

                                    if (dt.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "A")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "A")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }



                                    else if (dt.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "A")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                { dt.Rows[i]["alignto"] = "请修改脚本"; }

                            }
                            else if (str1 == "AT" || str1 == "TT")
                            {
                                try
                                {

                                    if (dt.Rows[i - 1]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 1]["maskLabel"].ToString().Trim().Substring(0, 2); }


                                    if (dt.Rows[i - 2]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 2]["maskLabel"].ToString().Trim().Substring(0, 2); }


                                    if (dt.Rows[i - 3]["ppid"].ToString().Substring(0, 1) == "W")
                                    { dt.Rows[i]["alignto"] = dt.Rows[i - 3]["maskLabel"].ToString().Trim().Substring(0, 2); }
                                }
                                catch
                                {
                                    dt.Rows[i]["alignto"] = "请修改脚本";
                                }

                            }
                            //目前没有规定 SN OVL 量到 GT，必须对GT
                            //else if (dt.Rows[i]["ovlto"].ToString().Length > 1)
                            //{ dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString(); }
                            else if (dt.Rows[i]["toFlag"].ToString() == "N" && dt.Rows[i]["zaFlag"].ToString() == "Y")
                            { dt.Rows[i]["alignto"] = "ZA"; }
                            else if (str1 == "TO" && dt.Rows[i]["zaFlag"].ToString() == "N" && dt.Rows[i]["ovlto"].ToString().Length > 0)
                            { dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString(); }
                            else if (dt.Rows[i]["toFlag"].ToString() == "Y")
                            { dt.Rows[i]["alignto"] = "TO"; }
                            else if (dt.Rows[i]["toFlag"].ToString() == "N" && dt.Rows[i]["ovlto"].ToString().Length > 0)
                            {

                                dt.Rows[i]["alignto"] = dt.Rows[i]["ovlto"].ToString();
                            }

                            else
                            {
                                dt.Rows[i]["alignto"] = dt.Rows[0]["maskLabel"].ToString();
                                MessageBox.Show("TO及之前的层次，统一对位到第一层\r\n\r\nAsml Alignment Tree定义不完善，请通知更改\r\n\r\n例如，双零层工艺");
                            }
                        }
                        #endregion
                    }
                }
            }

            MessageBox.Show("除第一层外，AlignTo列必须有被对位层次；\r\n\r\n否则读取坐标时会报错；\r\n\r\n请确认本步骤正确，再读取坐标");

        }
        public bool MatchCoordinate()
        {
            //确认biastable中的对位层次，在坐标文件中存在
            Dictionary<string, int> zbDic = new Dictionary<string, int>();
            foreach (DataRow row in dtZb.Rows)
            {
                try
                { 
                    zbDic.Add(row["layer"].ToString().Substring(0, 2), 1); 
                }
                catch { }
            }



            for (int i = 1; i < dt.Rows.Count; i++)
            {
                
               string tmp = dt.Rows[i]["alignto"].ToString().Substring(0, 2);
                try 
                { 
                    int x = zbDic[tmp];
                }
                catch
                {  
                    return false; 
                }
            }


                #region copied from old script
                string tech, ppid, alignto, method, tool;

             dt2 = new DataTable();
            dt2.Columns.Add("stage");
            dt2.Columns.Add("Mask");
            dt2.Columns.Add("MLM");
            dt2.Columns.Add("ppid");
            dt2.Columns.Add("alignto");

            dt2.Columns.Add("eqptype");
            dt2.Columns.Add("illuminations");
            dt2.Columns.Add("method");


            dt2.Columns.Add("typex");
            dt2.Columns.Add("x1");
            dt2.Columns.Add("y1");
            dt2.Columns.Add("typey");
            dt2.Columns.Add("x2");
            dt2.Columns.Add("y2");





            //将第一层数据，加入到坐标表格中
            DataRow newRow = dt2.NewRow();
            newRow["stage"] = dt.Rows[0]["stage"].ToString();
            newRow["Mask"] = dt.Rows[0]["Mask"].ToString();
            newRow["MLM"] = dt.Rows[0]["MLM"].ToString();
            newRow["ppid"] = dt.Rows[0]["ppid"].ToString();
            newRow["eqptype"] = dt.Rows[0]["eqptype"].ToString();
            if (dt.Rows[0]["eqptype"].ToString() == "LDI") { newRow["illuminations"] = dt.Rows[0]["illumination"].ToString(); }
            newRow["alignto"] = "NA"; newRow["typex"] = "NA"; newRow["method"] = "NA";
            newRow["x1"] = 88888;
            newRow["y1"] = 88888;
            newRow["typey"] = "NA";
            newRow["x2"] = 88888;
            newRow["y2"] = 88888;
            dt2.Rows.Add(newRow);

        


            for (int i = 1; i < dt.Rows.Count; i++)
            {

                tech = dt.Rows[i]["code"].ToString();

                tool = dt.Rows[i]["eqptype"].ToString();

                ppid = dt.Rows[i]["ppid"].ToString().Substring(0, 2);

                alignto = dt.Rows[i]["alignto"].ToString().Substring(0, 2);

                method = dt.Rows[i]["method"].ToString();

                string[] markAll = { "SPM53", "AH325374", "AA157", "NAH53", "NAH325374", "NAA157" };
                string[] markAdvancedHole = { "AA157", "SPM53", "AH325374", "NAA157", "NAH53", "NAH325374" };
                string[] markAdvancedMetal = { "AH325374", "SPM53", "AA157", "NAH325374", "NAH53", "NAA157" };

                string[] markLsaSearch = { "WC", "W", "WD", "NWC", "NW", "NWD" };
                string[] markFiaSearch = { "WFC", "WF", "WFD", "NWFC", "NWF", "NWFD" };
                string[] markLsaEga = { "LSAC", "LSA", "LSAD", "NLSAC", "NLSA", "NLSAD" };
                string[] markFiaEga = { "FIAC", "FIA", "FIAD", "NFIAC", "NFIA", "NFIAD" };

                string[] markLsaSearchD = { "WD", "W", "WC", "NWD", "NW", "NWC" };
                string[] markFiaSearchD = { "WFD", "WF", "WFC", "NWFD", "NWF", "NWFC" };
                string[] markLsaEgaD = { "LSAD", "LSA", "LSAC", "NLSAD", "NLSA", "NLSAC" };
                string[] markFiaEgaD = { "FIAD", "FIA", "FIAC", "NFIAD", "NFIA", "NFIAC" };

                string[] markType = { "" };






                if (tool == "LDI") //asml
                {
                    #region
                    if (tech.Substring(0, 1) == "T") //DMOS
                    {
                        #region DMOS
                        //   if (part.Substring(7, 1) == "N" || part.Substring(7, 1) == "M") //split gate
                        //   {
                        //       string[] strQuery = { i.ToString(), alignto };
                        //      ReadMark(strQuery, markAll, markType);
                        //  }
                        //  else //非 split gate
                        //  {
                        if (alignto == "TR")
                        {
                            string[] strQuery = { i.ToString(), "TR60", "TR" };
                            ReadMark(strQuery, markAll, markType);
                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markAll, markType);
                        }


                        //}
                        #endregion
                    }
                    else
                    {
                        string[] holes = { "W1", "W2", "W3", "W4", "W5", "W6", "W7", "WT" };
                        string[] metals = { "A1", "A2", "A3", "A4", "A5", "A6", "AT", "CT", "OE", "OV" };
                        string[] stack1 = { "TO", "W2", "W4", "W6" };
                        string[] stack2 = { "W1", "W3", "W5", "W7" };
                        if (tech.Substring(1, 1) != "1") //"非先进工艺"
                        {
                            #region lowend
                            // MessageBox.Show(tech + "," + tool + "," + ppid + "," + alignto + "," + method + "," + tech.Substring(1, 1));

                            if (stack1.Contains(alignto))
                            {
                                //alignto + TOW246
                                string[] strQuery = { i.ToString(), "TOW246", alignto };
                                //MessageBox.Show(tech + "," + tool + "," + ppid + "," + alignto + "," + method + "," + tech.Substring(1, 1));

                                ReadMark(strQuery, markAll, markType);

                            }
                            else if (stack2.Contains(alignto))
                            {
                                //alignto + W1357
                                string[] strQuery = { i.ToString(), "W1357", alignto };
                                ReadMark(strQuery, markAll, markType);
                            }
                            else
                            {
                                //alignto
                                string[] strQuery = { i.ToString(), alignto };
                                ReadMark(strQuery, markAll, markType);
                            }
                            #endregion
                        }
                        else //先进工艺
                        {
                            #region highend



                            // if (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") //narrow mark
                            if (narrowScribelaneFlag)

                            {
                                #region narrow mark

                                if (holes.Contains(ppid)) // 孔用157
                                {
                                    #region hole
                                    if (stack1.Contains(alignto))
                                    {
                                        //alignto + TOW246
                                        string[] strQuery = { i.ToString(), "TOW246", alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);

                                    }
                                    else if (stack2.Contains(alignto))
                                    {
                                        //alignto + W1357
                                        string[] strQuery = { i.ToString(), "W1357", alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);
                                    }
                                    else
                                    {
                                        //W1-->GT
                                        string[] strQuery = { i.ToString(), alignto };
                                        ReadMark(strQuery, markAdvancedHole, markType);
                                    }
                                    #endregion
                                }
                                else if (metals.Contains(ppid)) //铝用325374
                                {
                                    #region metal
                                    if (stack1.Contains(alignto))
                                    {
                                        //alignto + TOW246
                                        string[] strQuery = { i.ToString(), "TOW246", alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);
                                    }
                                    else if (stack2.Contains(alignto))
                                    {
                                        //alignto + W1357
                                        string[] strQuery = { i.ToString(), "W1357", alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);
                                    }
                                    else
                                    {
                                        //AT-->WT
                                        string[] strQuery = { i.ToString(), alignto };
                                        ReadMark(strQuery, markAdvancedMetal, markType);

                                    }
                                    #endregion
                                }
                                else  //非孔，铝用53
                                {
                                    if (alignto.Contains("TO"))
                                    { string[] strQuery = { i.ToString(), "TOW246", alignto }; ReadMark(strQuery, markAll, markType); }
                                    else
                                    { string[] strQuery = { i.ToString(), alignto }; ReadMark(strQuery, markAll, markType); };

                                }
                                #endregion
                            }
                            else // 非narrow mark
                            {
                                #region
                                if (stack1.Contains(alignto))
                                {
                                    //alignto + TOW246
                                    string[] strQuery = { i.ToString(), "TOW246", alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                else if (stack2.Contains(alignto))
                                {
                                    //alignto + W1357
                                    string[] strQuery = { i.ToString(), "W1357", alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                else
                                {
                                    //alignto
                                    string[] strQuery = { i.ToString(), alignto };
                                    ReadMark(strQuery, markAll, markType);
                                }
                                #endregion
                            }
                            #endregion
                        }

                    }
                    #endregion

                }
                else //Nikon
                {
                    #region
                    //  if ((ppid == "CP" || ppid == "PN" || ppid == "PI" || ppid == "P2") && tech.Substring(0, 1) != "T") //DMOS PAD
                    //   if (  tech.Substring(1, 1) == "1" &&   (part.Substring(7, 1) == "J" || part.Substring(7, 1) == "K" || part.Substring(7, 1) == "R" || part.Substring(part.Length - 1, 1) == "L") &&        (alignto=="TO")          )



                    if (narrowScribelaneFlag == true && fullTech.Substring(1, 1) == "1")// || alignto == "TO")
                    {
                        if (ppid == "CP" || ppid == "PI" || ppid == "PN" || ppid == "CF" || ppid == "FU")
                        {
                            //   string[] strQuery = { i.ToString(), alignto, "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            //string[] strQuery = { i.ToString(), alignto };// "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            string[] strQuery = { i.ToString(), alignto, "T" + alignto.Substring(1, 1), "A" + alignto.Substring(1, 1) };


                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }

                        else if (alignto == "TO")
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearchD, markType);
                            ReadMark(strQuery, markFiaSearchD, markType);
                            ReadMark(strQuery, markLsaEgaD, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEgaD, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        /*
                        else if (ppid=="RE"||ppid=="CT"||ppid=="TT"|| ppid == "AT"||ppid=="EP"||ppid=="SN"||ppid=="SP"||ppid=="NP"||ppid=="PP")  //其它层次待加入
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });

                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearchD, markType);
                            ReadMark(strQuery, markFiaSearchD, markType);
                            ReadMark(strQuery, markLsaEgaD, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEgaD, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        */
                    }
                    else
                    {
                        if (ppid == "CP" || ppid == "PI" || ppid == "PN" || ppid == "CF" || ppid == "FU")
                        {
                            //   string[] strQuery = { i.ToString(), alignto, "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            //string[] strQuery = { i.ToString(), alignto };// "AT", "TT", "T1", "T2", "T3", "A7", "A6", "A5", "A4", "A3", "A2", "A1" };
                            string[] strQuery = { i.ToString(), alignto, "T" + alignto.Substring(1, 1), "A" + alignto.Substring(1, 1) };

                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                        else
                        {
                            string[] strQuery = { i.ToString(), alignto };
                            ReadMark(strQuery, markLsaSearch, markType);
                            ReadMark(strQuery, markFiaSearch, markType);
                            ReadMark(strQuery, markLsaEga, new string[] { "7", "9" });
                            ReadMark(strQuery, markFiaEga, new string[] { "13", "13/8", "9", "9/12" });
                        }
                    }
                    #endregion

                }

            }
            #endregion


            return true;
        }
        private void ReadMark(string[] layers, string[] prefix, string[] markType)
        {
            string key; DataRow[] drs = null;


            int rowno = Convert.ToInt32(layers[0]);



            for (int i = 1; i < layers.Length; i++) //第一位是dt的行数
            {
                foreach (string item in prefix)
                {
                    foreach (string markLine in markType)
                    {
                        key = "layer='" + layers[i] + "' and mark like '" + item + "%' and lines='" + markLine + "' ";




                        // drs = tblTmp.Select(key, "lines asc,mark asc");

                        drs = dtZb.Select(key, "lines asc,mark asc");

                        //  if (layers[i] == "GT") {
                        //        MessageBox.Show(key + "\n" + drs.Length.ToString()); 
                        //  }

                        if (drs.Length > 0)
                        {
                     
                            DataRow newRow = dt2.NewRow();
                            newRow["stage"] = dt.Rows[rowno]["stage"].ToString();
                            newRow["Mask"] = dt.Rows[rowno]["Mask"].ToString();
                            newRow["MLM"] = dt.Rows[rowno]["MLM"].ToString();
                            newRow["ppid"] = dt.Rows[rowno]["ppid"].ToString();
                            newRow["eqptype"] = dt.Rows[rowno]["eqptype"].ToString();
                            if (dt.Rows[rowno]["eqptype"].ToString() == "LDI")
                            {
                                newRow["illuminations"] = dt.Rows[rowno]["illumination"].ToString();
                            }
                            else
                            {
                                if (item == "WD" || item == "WC" || item == "W" || item == "NWD" || item == "NWC" || item == "NW")
                                {
                                    newRow["illuminations"] = "LsaS坐标";
                                }
                                else if ((item.Length > 1 && item.Substring(0, 2) == "WF") || (item.Length > 2 && item.Substring(0, 3) == "NWF"))
                                {
                                    newRow["illuminations"] = "FiaS坐标";
                                }
                                else if ((item.Length > 2 && item.Substring(0, 3) == "LSA") || (item.Length > 3 && item.Substring(0, 4) == "NLSA"))
                                {
                                    newRow["illuminations"] = "LsaE坐标";
                                }
                                else if ((item.Length > 2 && item.Substring(0, 3) == "FIA") || (item.Length > 3 && item.Substring(0, 4) == "NFIA"))
                                {
                                    newRow["illuminations"] = "FiaE坐标";
                                }

                            }
                            newRow["method"] = dt.Rows[rowno]["method"].ToString();
                            newRow["alignto"] = dt.Rows[rowno]["alignto"].ToString();
                            ///针对stack mark此处应该改为坐标文件的实际标记前缀，
                            newRow["alignto"] = layers[i];





                            if (item.Substring(0, 1) == "W" || item.Substring(0, 2) == "NW")//Search Y Mark在前
                            {
                                newRow["typex"] = drs[drs.Length / 2]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x1"] = drs[drs.Length / 2]["X"].ToString();
                                newRow["y1"] = drs[drs.Length / 2]["Y"].ToString();
                                newRow["typey"] = drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x2"] = drs[0]["X"].ToString();
                                newRow["y2"] = drs[0]["Y"].ToString();

                            }
                            else
                            {
                                newRow["typex"] = drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x1"] = drs[0]["X"].ToString();
                                newRow["y1"] = drs[0]["Y"].ToString();
                                newRow["typey"] = drs[drs.Length / 2]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
                                newRow["x2"] = drs[drs.Length / 2]["X"].ToString();
                                newRow["y2"] = drs[drs.Length / 2]["Y"].ToString();


                            }
                            dt2.Rows.Add(newRow);

                            ///部分坐标文件的X,Y的前缀一样，有误，以下进行判断
                            ///
                          //  MessageBox.Show(newRow["typex"].ToString() + ",  " + newRow["typey"].ToString());
                            if (newRow["typex"].ToString().Length > 1 && newRow["typeY"].ToString().Length > 1)
                            {
                                if (
                                       (newRow["typeY"].ToString().Split(new char[] { '#' })[0].EndsWith("X") && newRow["typeX"].ToString().Split(new char[] { '#' })[0].EndsWith("Y"))
                                    || (newRow["typeY"].ToString().Split(new char[] { '#' })[0].EndsWith("Y") && newRow["typeX"].ToString().Split(new char[] { '#' })[0].EndsWith("X"))
                                    )
                                { }
                                else
                                {
                                    MessageBox.Show("坐标前缀异常" +
                                     "\n\n    " + newRow["ppid"] + "," + newRow["alignto"] + "," + newRow["typeX"].ToString() +
                                      "\n\n    " + newRow["ppid"] + "," + newRow["alignto"] + "," + newRow["typeY"].ToString() +
                                       "\n\n\n\n不是分别以X/Y结尾，请确认");
                                }
                            }





                            return;


                        }
                        else
                        {


                        }


                    }

                }
                // StringBuilder tmp = new StringBuilder();
                // tmp.Append(dt.Rows[rowno]["ppid"].ToString());
                // tmp.Append(":所有标记组合\r\n\r\n");
                // foreach (var x in prefix) { tmp.Append(x); tmp.Append(","); }
                //  tmp.Append("\r\n\r\n未找到对应坐标");
                //  MessageBox.Show(tmp.ToString());







            }
            DataRow newRow1 = dt2.NewRow();
            newRow1["stage"] = dt.Rows[rowno]["stage"].ToString();
            newRow1["Mask"] = dt.Rows[rowno]["Mask"].ToString();
            newRow1["MLM"] = dt.Rows[rowno]["MLM"].ToString();
            newRow1["ppid"] = dt.Rows[rowno]["ppid"].ToString();
            newRow1["eqptype"] = dt.Rows[rowno]["eqptype"].ToString();
            newRow1["method"] = dt.Rows[rowno]["method"].ToString();

            newRow1["typex"] = "#";//drs[0]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
            newRow1["x1"] = "#";//drs[0]["X"].ToString();
            newRow1["y1"] = "#";//drs[0]["Y"].ToString();
            newRow1["typey"] = "#";// drs[1]["mark"].ToString() + "#" + drs[0]["lines"].ToString();
            newRow1["x2"] = "#";//drs[1]["X"].ToString();
            newRow1["y2"] = "#";//drs[1]["Y"].ToString();
            dt2.Rows.Add(newRow1);






        }

    }


}





