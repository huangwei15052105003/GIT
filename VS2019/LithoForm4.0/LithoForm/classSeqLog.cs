using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Data.OleDb;


namespace LithoForm
{
    class classSeqLog
    {
        public static void readLog(ref DataTable dt, ref List<string> list)
        {

            List<string> names = new List<string>();
            list.Clear();
            dt = null;
            dt = new DataTable();


            //chose files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"P:\SEQLOG\";
            openFileDialog.Title = @"选择多个Sequence Log文件";
            openFileDialog.Filter = "Log文件(*)|_20*#20*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var name in openFileDialog.FileNames)
                {
                    names.Add(name);
                }
                names.Sort();
            }
            else
            {
                MessageBox.Show("Files Are Not Selected,Exit");
                return;

            }
            //read files
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var x in names)
            {
                list.AddRange(File.ReadAllLines(x));
            }
            var rt = list.RemoveAll(j => j.Contains("\"EGA MEASUREMENT\""));


            MessageBox.Show(names.Count().ToString() + " Files: " + sw.Elapsed.TotalSeconds.ToString() + " seconds.\n\n" +
                rt.ToString() + " Lines \"EGA MEASUREMENT\" Were Deleted\n\n" +
                "Reading Log Done\n\nTotal Lines: " + list.Count().ToString());
        }

        public static void showLog(ref DataTable dt, ref List<string> log)
        {
            dt = null;
            dt = new DataTable();
            dt.Columns.Add("Content");
            dt.Columns.Add("ToolID");
            foreach (string item in log)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = item;
                dt.Rows.Add(newRow);
            }

        }
        
        


        //=====================================================================================================
        public static void readAllLog(int days, string tool, ref Dictionary<string,DataTable[]> myDic,bool boolSaveRaw,bool[] boolArr)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string path = @"\\10.4.50.16\photo$\ppcs\seqlog\";
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04", "ALII05", "ALII06", "ALII07",
           "ALII08", "ALII09", "ALII10", "ALII11", "ALII12", "ALII13", "ALII14",
           "ALII15", "ALII16", "ALII17", "ALII18", "BLII20", "BLII21", "BLII22","BLII23"};
            string key = DateTime.Now.AddDays(-days).ToString("_yyyy-MM-dd");
            string folder;
            List<string> filelist = new List<string>();
            List<string> log = new List<string>();

            if (tool != "All")
            {
                try
                {
                    folder = path + tool + "\\";
                    filelist.Clear();
                    filelist = GetFileNames(folder, key);
                    log.Clear();


                    log = readSingleTool(filelist);


                    ExtractRawL(tool, ref log, ref myDic, true, boolSaveRaw, boolArr);



                    GC.Collect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Reading Log:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("注意：\n\n一次性读入所有设备的多天Log，耗时较长；\n\n读取结果不包含原始数据，无法显示原始数据及按lotID，关键字进行筛选；");
                foreach (var x in tools)
                {

                    folder = path + x + "\\";
                    filelist.Clear();
                    filelist = GetFileNames(folder, key);
                    log.Clear();
                    log = readSingleTool(filelist);
                    ExtractRawL(x, ref log, ref myDic, false, boolSaveRaw, boolArr);
                    GC.Collect();
                }
            }
            sw.Stop();
            MessageBox.Show("完成读取，共耗时：" + sw.Elapsed.TotalSeconds.ToString() + " 秒");
            GC.Collect();

        }

        private static List<string> GetFileNames(string folder, string key)
        {
            try
            {
                List<string> list = new List<string>();
                string[] files = Directory.GetFiles(folder);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Name.Length>35)
                    {
                        if (fileInfo.Name.StartsWith("_20") &&
                            string.Compare(fileInfo.Name.Substring(0, 11), key) >= 0 &&
                            string.Compare(fileInfo.Name.Substring(1, 17), fileInfo.Name.Substring(19, 17)) < 0)  //logmerge生成的取消
                        {
                            list.Add(fileInfo.FullName);
                        }
                    }
                    else
                    {
                     //   MessageBox.Show("列文件清单：文件名不规范，读取的Log可能不完整;\n\n文件名："+fileInfo.FullName);
                    }
                }
                list.Sort();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("列文件清单异常,文件名可能不规范\n\n" + ex.Message);
                return new List<string>();
            }
            

        }
        private static List<string> readSingleTool(List<string> names)
        {
            List<string> rawL = new List<string>();
      
            foreach (var x in names)
            {
                try
                { 
                    rawL.AddRange(File.ReadAllLines(x));
                }
                catch(Exception ex)
                { 
                    MessageBox.Show("Error Code: \n\n" + ex.Message+"\n\nFileName:"+x+"\n\n数据未完整读取");
                    rawL.RemoveAll(j => j.Contains("\"EGA MEASUREMENT\""));
                    return rawL;
                }
            }
            rawL.RemoveAll(j => j.Contains("\"EGA MEASUREMENT\""));
            return rawL;   
        }

       
        public static void ExtractRawL(string tool,ref List<string> log, ref Dictionary<string,DataTable[]> myDic,bool flag,bool boolSaveRaw,bool[] boolArr)
        {

            #region define variables
            //lot
            string[] lotArr = new string[12];//临时变量
            List<string[]> lotList = new List<string[]>();//保存lot数据
                                                          //assist
            string[] assistArr = new string[4];//临时变量
            List<string[]> assistList = new List<string[]>();//保存assist数据
                                                             //of retry
            string[] ofretryArr = new string[4];//临时变量
            List<string[]> ofretryList = new List<string[]>();//保存assist数据
                                                              //recover error
            string[] recoverErrArr = new string[5];
            List<string[]> recoverErrList = new List<string[]>();

            string[] baselineChkArr = new string[6];
            List<string[]> baselineChkList = new List<string[]>();



            //wfr
            string[] wfrArr = new string[15];
            List<string[]> wfrList = new List<string[]>();
            #endregion

            #region for loop of raw data
            string str;
            int lotcount = 0;

            for (int i = 0; i < log.Count(); ++i)
            {
                str = log[i];
                #region get lot information
                
                try
                {

                    if (str.Contains("\"EXPOSURE CONDITION\""))
                    {
                        Array.Clear(lotArr, 0, 12);
                        lotArr[0] = (str.Trim().Substring(0, 19));// riqi;
                        lotArr[1] = (log[i + 2].Split('"')[1]);//process name
                        lotArr[2] = (log[i + 3].Split('"')[1]);//program name
                        lotArr[3] = (log[i + 5].Split('"')[1]);//LotID
                        lotArr[4] = (log[i + 7].Split('\'')[2].Trim());//EXP MODE
                        lotArr[5] = (log[i + 8].Split('\'')[2].Trim());//Align MODE
                        lotArr[6] = (log[i + 9].Split('\'')[2].Trim());//EXP DOSE 
                        lotArr[7] = (log[i + 12].Split('\'')[2].Trim());//Focus INIT
                        lotArr[8] = (log[i + 31].Split('\'')[2].Trim());//LEVELING MODE
                        lotArr[9] = (log[i + 53].Split('\'')[2].Trim());//SEARCH SENSOR
                        lotArr[10] = (log[i + 54].Split('\'')[2].Trim());//g_EGA SENSOR
                        i += 80;
                        lotArr[11] = (lotcount.ToString());//Index 
                        ++lotcount;
                        lotList.Add(new string[12] { lotArr[0], lotArr[1], lotArr[2], lotArr[3], lotArr[4], lotArr[5], lotArr[6], lotArr[7], lotArr[8], lotArr[9], lotArr[10], lotArr[11] });
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n\nUnable To Get All Lot Information");
                }
                #endregion

                #region Assist
                if (boolArr[1])
                {
                    if (str.Contains("\"ASSIST START\""))
                    {
                        assistArr[0] = lotArr[11];
                        assistArr[1] = str.Trim().Substring(0, 19);
                        continue;
                    }
                    if (str.Contains("\"ASSIST END\""))
                    {
                        if (assistArr[1] != null)
                        {
                            assistArr[2] = str.Trim().Substring(0, 19);
                            assistArr[3] = Convert.ToDateTime(assistArr[2]).Subtract(Convert.ToDateTime(assistArr[1])).TotalSeconds.ToString();
                            assistList.Add(new string[] { assistArr[0], assistArr[1], assistArr[2], assistArr[3] });
                            Array.Clear(assistArr, 0, 4);
                            continue;
                        }
                    }
                }
                #endregion

                #region OF RETRY
                if (boolArr[2])
                {
                    if (str.Contains("\"OF RETRY START\""))
                    {
                        ofretryArr[0] = lotArr[11];
                        ofretryArr[1] = str.Trim().Substring(0, 19);
                        continue;
                    }
                    if (str.Contains("\"OF RETRY END\""))
                    {
                        if (ofretryArr[1] != null)
                        {
                            ofretryArr[2] = str.Trim().Substring(0, 19);
                            ofretryArr[3] = Convert.ToDateTime(ofretryArr[2]).Subtract(Convert.ToDateTime(ofretryArr[1])).TotalSeconds.ToString();
                            ofretryList.Add(new string[] { ofretryArr[0], ofretryArr[1], ofretryArr[2], ofretryArr[3] });
                            Array.Clear(ofretryArr, 0, 4);
                            continue;
                        }
                    }
                }
                #endregion

                #region Recoverable Error
                if (boolArr[3])
                {
                    if (str.Contains("\"RECOVERABLE ERROR\""))
                    {
                        recoverErrArr[0] = lotArr[11];
                        recoverErrArr[1] = str.Trim().Substring(0, 19);
                        ++i;
                        recoverErrArr[4] = log[i].Trim().Substring(70, log[i].Trim().Length - 70);
                        continue;
                    }
                    if (str.Contains("\"ERROR RECOVERED\""))
                    {
                        if (recoverErrArr[1] != null)
                        {
                            recoverErrArr[2] = str.Trim().Substring(0, 19);
                            recoverErrArr[3] = Convert.ToDateTime(recoverErrArr[2]).Subtract(Convert.ToDateTime(recoverErrArr[1])).TotalSeconds.ToString();
                            recoverErrList.Add(new string[] { recoverErrArr[0], recoverErrArr[1], recoverErrArr[2], recoverErrArr[3], recoverErrArr[4] });
                            Array.Clear(recoverErrArr, 0, 5);
                            continue;
                        }
                    }
                }
                #endregion

                #region baselineChk
                if (boolArr[5])
                {
                    if(str.Contains("\"FIA-SET\""))
                    {
                        baselineChkArr[0] = lotArr[11];
                        baselineChkArr[1]= str.Trim().Substring(0, 19);
                        ++i;
                        str = log[i];
                        baselineChkArr[2] = str.Split(new char[] { '(' })[1].Split(new char[] { ',' })[0].Trim();
                        ++i;
                        str = log[i];
                        baselineChkArr[3] = str.Split(new char[] { '(' })[1].Split(new char[] { ',' })[1].Replace(")","").Trim();
                        continue;
                    }
                    if (str.Contains("\"LSA-SET\""))
                    {
                        if(baselineChkArr[1]!=null)
                        {
                            ++i;
                            str = log[i];
                            baselineChkArr[4] = str.Split(new char[] { '(' })[1].Split(new char[] { ',' })[0].Trim();
                            ++i;
                            str = log[i];
                            baselineChkArr[5] = str.Split(new char[] { '(' })[1].Split(new char[] { ',' })[1].Replace(")", "").Trim();
                            baselineChkList.Add(new string[] { baselineChkArr[0], baselineChkArr[1], baselineChkArr[2], baselineChkArr[3], baselineChkArr[4], baselineChkArr[5] });
                            continue;
                        }
                    }
                }
                #endregion


                #region wfr data
                if (boolArr[4])
                {
                    if (str.Contains("\"WAFER CHANGE START\""))
                    {
                        wfrArr[0] = lotArr[11];
                        wfrArr[1] = str.Trim().Substring(0, 19);
                        continue;
                    }
                    if (str.Contains("\"WAFER CHANGE END\""))
                    {
                        wfrArr[2] = str.Trim().Substring(0, 19);
                        wfrArr[3] = Convert.ToDateTime(wfrArr[2]).Subtract(Convert.ToDateTime(wfrArr[1])).TotalSeconds.ToString();
                        continue;
                    }

                    if (str.Contains("\"WAFER ALIGNMENT START\""))
                    {
                        wfrArr[4] = str.Trim().Substring(0, 19);
                        continue;
                    }
                    if (str.Contains("\"WAFER ALIGNMENT END\""))
                    {
                        wfrArr[5] = str.Trim().Substring(0, 19);
                        wfrArr[6] = Convert.ToDateTime(wfrArr[5]).Subtract(Convert.ToDateTime(wfrArr[4])).TotalSeconds.ToString();
                        continue;
                    }
                    if (str.Contains("'EXP TIME AVE'"))
                    {
                        wfrArr[7] = str.Substring(17, 6).Trim();
                        continue;
                    }
                    if (str.Contains("'EXP TIME MAX'"))
                    {
                        wfrArr[8] = str.Substring(17, 6).Trim();
                        continue;
                    }
                    if (str.Contains("'EXP TIME MIN'"))
                    {
                        wfrArr[9] = str.Substring(17, 6).Trim();
                        continue;
                    }
                    if (str.Contains("'EXP TIME 3SIGMA'"))
                    {
                        wfrArr[10] = str.Substring(21, 6).Trim();
                        continue;
                    }
                    if (str.Contains("\"EXPOSURE START\""))
                    {
                        wfrArr[11] = str.Substring(0, 19);
                        continue;
                    }
                    if (str.Contains("\"EXPOSURE END\""))
                    {
                        wfrArr[12] = str.Trim().Substring(0, 19);
                        wfrArr[13] = Convert.ToDateTime(wfrArr[12]).Subtract(Convert.ToDateTime(wfrArr[11])).TotalSeconds.ToString();
                        ++i;
                        wfrArr[14] = log[i].Trim().Replace("'NUMBER'", "").Trim();
                        wfrList.Add(new string[] { wfrArr[0], wfrArr[1], wfrArr[2], wfrArr[3], wfrArr[4], wfrArr[5], wfrArr[6], wfrArr[7], wfrArr[8], wfrArr[9], wfrArr[10], wfrArr[11], wfrArr[12], wfrArr[13], wfrArr[14] });
                        Array.Clear(wfrArr, 0, 15);
                        continue;
                    }
                }

                #endregion

            }
            
            #endregion

            #region conver to datatable
            DataTable dtLot, dtAssist, dtOfretry, dtRecoverErr, dtWfr,dtBaselineChk;
            #region lot data


            dtLot = new DataTable();
            foreach (string item in new string[] { "date", "procName", "progName", "lotID", "expMode", "alignMode", "dose", "focus", "levleMode", "search", "ega", "index" })
            {
                if (item != "index")
                { dtLot.Columns.Add(item, typeof(string)); }
                else
                { dtLot.Columns.Add(item, typeof(int)); }
            }
            foreach (string[] lot in lotList)
            {
                DataRow newRow = dtLot.NewRow();
                for (int i = 0; i < 12; ++i)
                {
                    newRow[i] = lot[i];
                }
                dtLot.Rows.Add(newRow);
            }

            #endregion

            #region assist data

            dtAssist = new DataTable();
            foreach (string item in new string[] { "index", "assistStart", "assistEnd", "secondsElapsed" })
            {
                if (item == "secondsElapsed" || item == "index")
                { dtAssist.Columns.Add(item, typeof(int)); }
                else
                {
                    dtAssist.Columns.Add(item, typeof(string));
                }
            }
            if (boolArr[1])
            {
                foreach (string[] assist in assistList)
                {
                    DataRow newRow = dtAssist.NewRow();
                    for (int i = 0; i < 4; ++i)
                    {
                        if (assist[i] == null)
                        {
                            // newRow[i] = " ";
                        }
                        else
                        {
                            newRow[i] = assist[i];
                        }
                    }
                    dtAssist.Rows.Add(newRow);
                }
                assistList = null;
            }
            #endregion

            #region ofretry data

            dtOfretry = new DataTable();
            foreach (string item in new string[] { "index", "ofretryStart", "ofretryEnd", "secondsElapsed" })
            {
                if (item == "secondsElapsed" || item == "index")
                { dtOfretry.Columns.Add(item, typeof(int)); }
                else
                { dtOfretry.Columns.Add(item, typeof(string)); }
            }
            if (boolArr[2])
            {
                foreach (string[] ofretry in ofretryList)
                {
                    DataRow newRow = dtOfretry.NewRow();
                    for (int i = 0; i < 4; ++i)
                    {
                        if (ofretry[i] == null)
                        {
                            // newRow[i] = "";
                        }
                        else
                        {
                            newRow[i] = ofretry[i];
                        }
                    }
                    dtOfretry.Rows.Add(newRow);
                }
                ofretryList = null;
            }
            #endregion

            #region Recoverable Error

            dtRecoverErr = new DataTable();
            foreach (string item in new string[] { "index", "recoverErrStart", "recoverErrEnd", "secondsElapsed", "error" })
            {
                if (item == "secondsElapsed" || item == "index")
                { dtRecoverErr.Columns.Add(item, typeof(int)); }
                else
                { dtRecoverErr.Columns.Add(item, typeof(string)); }
            }
            if (boolArr[3])
            {
                foreach (string[] err in recoverErrList)
                {
                    DataRow newRow = dtRecoverErr.NewRow();
                    for (int i = 0; i < 5; ++i)
                    {
                        if (err[i] == null)
                        {
                            //newRow[i] = "";
                        }
                        else
                        {
                            newRow[i] = err[i];
                        }
                    }
                    dtRecoverErr.Rows.Add(newRow);
                }
                recoverErrList = null;
            }
            #endregion

            #region baselinechk

            dtBaselineChk = new DataTable();
            dtBaselineChk.Columns.Add("index", typeof(int));
            dtBaselineChk.Columns.Add("chkDate", typeof(string));
            foreach(string item in new string[] { "FIA_X","FIA_Y","LSA_X","LSA_Y"})
            { dtBaselineChk.Columns.Add(item, typeof(double)); }
            if (boolArr[5])
            {
                foreach (string[] chk in baselineChkList)
                {
                    DataRow newRow = dtBaselineChk.NewRow();
                    for (int i = 0; i < 6; ++i)
                    {
                        if (chk[i] == null) { }
                        else
                        { newRow[i] = chk[i]; }
                    }
                    dtBaselineChk.Rows.Add(newRow);
                }
                baselineChkList = null;
            }
            #endregion



            #region wfr data

            dtWfr = new DataTable();
            foreach (string item in new string[] { "index", "chgElapsedSeconds", "alignElapsedSeconds", "expElapsedSeconds", "shotQty" })
            { dtWfr.Columns.Add(item, typeof(int)); }
            foreach (string item in new string[] { "illIntensity", "expAvg", "expMax", "expMin", "exp3Sigma" })
            { dtWfr.Columns.Add(item, typeof(float)); }
            foreach (string item in new string[] { "chgStart", "chgEnd", "alignStart", "alignEnd", "expStart", "expEnd", })
            { dtWfr.Columns.Add(item, typeof(string)); }
            if (boolArr[4])
            {
                foreach (string[] wfr in wfrList)
                {
                    DataRow newRow = dtWfr.NewRow();
                    try
                    { newRow["index"] = wfr[0]; }
                    catch//(Exception ex)
                    {
                        // MessageBox.Show("dtWfr->index:" + ex.Message);
                    }
                    try
                    { newRow["chgElapsedSeconds"] = wfr[3]; }
                    catch//(Exception ex)
                    {
                        //  MessageBox.Show("dtWfr->chgElapsedSeconds:" +ex.Message);
                    }
                    try
                    { newRow["alignElapsedSeconds"] = wfr[6]; }
                    catch //(Exception ex)
                    {
                        // MessageBox.Show("dtWfr->alignElapsedSeconds:" + ex.Message);
                    }
                    try
                    { newRow["expElapsedSeconds"] = wfr[13]; }
                    catch //(Exception ex)
                    {
                        // MessageBox.Show("dtWfr->expElapsedSeconds:" + ex.Message);
                    }
                    try
                    { newRow["illIntensity"] = double.Parse(lotList[int.Parse(wfr[0])][6]) / double.Parse(wfr[7]); }
                    catch// (Exception ex)
                    {
                        //MessageBox.Show("dtWfr->illIntensity:" + ex.Message);
                    }
                    newRow["expAvg"] = wfr[7];
                    newRow["expMax"] = wfr[8];
                    newRow["expMin"] = wfr[9];
                    newRow["exp3Sigma"] = wfr[10];
                    newRow["chgStart"] = wfr[1];
                    newRow["chgEnd"] = wfr[2];
                    newRow["alignStart"] = wfr[4];
                    newRow["alignEnd"] = wfr[5];
                    newRow["expStart"] = wfr[11];
                    newRow["expEnd"] = wfr[12];
                    newRow["shotQty"] = wfr[14];
                    dtWfr.Rows.Add(newRow);


                }
                lotList = null;
                wfrList = null;
            }
            #endregion

            #endregion

            ;
            #region convet to datatable
            //dt Assist with lot info

            foreach (string item in new string[] { "lotStart", "procName", "progName", "lotID" })
            {
                dtAssist.Columns.Add(item);
            }
            if (boolArr[1])
            {
                foreach (DataRow row in dtAssist.Rows)
                {
                    try
                    {
                        row["lotStart"] = dtLot.Rows[int.Parse(row["index"].ToString())]["date"];
                        row["procName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["procName"];
                        row["progName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["progName"];
                        row["lotID"] = dtLot.Rows[int.Parse(row["index"].ToString())]["lotID"];
                    }
                    catch//(Exception ex)
                    {
                        //    MessageBox.Show("Error Code:" + ex.Message + "\n\nLot Data Not Available For Assist/OF_Retry, etc.");
                    }
                }
            }
            // OF_Retry

            foreach (string item in new string[] { "lotStart", "procName", "progName", "lotID" })
            {
                dtOfretry.Columns.Add(item);
            }
            if (boolArr[2])
            {
                foreach (DataRow row in dtOfretry.Rows)
                {
                    try
                    {
                        row["lotStart"] = dtLot.Rows[int.Parse(row["index"].ToString())]["date"];
                        row["procName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["procName"];
                        row["progName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["progName"];
                        row["lotID"] = dtLot.Rows[int.Parse(row["index"].ToString())]["lotID"];
                    }
                    catch //(Exception ex)
                    {
                        //       MessageBox.Show("Error Code:" + ex.Message + "\n\nLot Data Not Available For Assist/OF_Retry, etc.");
                    }
                }
            }
            //recover error
            foreach (string item in new string[] { "lotStart", "procName", "progName", "lotID" })
            {
                dtRecoverErr.Columns.Add(item);
            }
            if (boolArr[3])
            {
                foreach (DataRow row in dtRecoverErr.Rows)
                {
                    try
                    {
                        row["lotStart"] = dtLot.Rows[int.Parse(row["index"].ToString())]["date"];
                        row["procName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["procName"];
                        row["progName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["progName"];
                        row["lotID"] = dtLot.Rows[int.Parse(row["index"].ToString())]["lotID"];
                    }
                    catch //(Exception ex)
                    {
                        //   MessageBox.Show("Error Code:" + ex.Message + "\n\nLot Data Not Available For Assist/OF_Retry, etc.");
                    }
                }
            }
            //baselineCheck
            foreach (string item in new string[] { "lotStart", "procName", "progName", "lotID" })
            {
                dtBaselineChk.Columns.Add(item);
            }
            dtBaselineChk.Columns.Add("lotStart-chkDate", typeof(int));
            if (boolArr[5])
            {
                double fx = 0, fy = 0, lx = 0, ly = 0;
                try
                {
                    fx = Convert.ToDouble(dtBaselineChk.Compute("avg(FIA_X)", ""));
                    fy = Convert.ToDouble(dtBaselineChk.Compute("avg(FIA_Y)", ""));
                    lx = Convert.ToDouble(dtBaselineChk.Compute("avg(LSA_X)", ""));
                    ly = Convert.ToDouble(dtBaselineChk.Compute("avg(LSA_Y)", ""));
                }
                catch (Exception ex)
                {
                    //   MessageBox.Show("Step: Calculate Baseline Mean Value \n\nError Code: " + ex.Message+"\n\n Reason:checkBox->Baseline Not Checked");
                }

                //  dtBaselineChk.Columns.Add("fiaXmove", typeof(double));
                //  dtBaselineChk.Columns.Add("fiaYmove", typeof(double));
                //  dtBaselineChk.Columns.Add("lsaXmove", typeof(double));
                //  dtBaselineChk.Columns.Add("lsaYmove", typeof(double));


                if (fx != 0 && fy != 0 && lx != 0 && ly != 0)
                {
                    for (int i = 0; i < dtBaselineChk.Rows.Count; i++)
                    {
                        dtBaselineChk.Rows[i]["FIA_X"] = Math.Round(fx - Convert.ToDouble(dtBaselineChk.Rows[i]["FIA_X"].ToString()), 3) * 1000;
                        dtBaselineChk.Rows[i]["FIA_Y"] = Math.Round(fy - Convert.ToDouble(dtBaselineChk.Rows[i]["FIA_Y"].ToString()), 3) * 1000;
                        dtBaselineChk.Rows[i]["LSA_X"] = Math.Round(lx - Convert.ToDouble(dtBaselineChk.Rows[i]["LSA_X"].ToString()), 3) * 1000;
                        dtBaselineChk.Rows[i]["LSA_Y"] = Math.Round(ly - Convert.ToDouble(dtBaselineChk.Rows[i]["LSA_Y"].ToString()), 3) * 1000;
                        //      if (i>0)
                        //     {
                        //          dtBaselineChk.Rows[i]["fiaXmove"] = int.Parse(dtBaselineChk.Rows[i]["FIA_X"].ToString()) - int.Parse(dtBaselineChk.Rows[i - 1]["FIA_X"].ToString());
                        //         dtBaselineChk.Rows[i]["fiaYmove"] = int.Parse(dtBaselineChk.Rows[i]["FIA_Y"].ToString()) - int.Parse(dtBaselineChk.Rows[i - 1]["FIA_Y"].ToString());
                        //         dtBaselineChk.Rows[i]["lsaXmove"] = int.Parse(dtBaselineChk.Rows[i]["LSA_X"].ToString()) - int.Parse(dtBaselineChk.Rows[i - 1]["LSA_X"].ToString());
                        //         dtBaselineChk.Rows[i]["lsaYmove"] = int.Parse(dtBaselineChk.Rows[i]["LSA_Y"].ToString()) - int.Parse(dtBaselineChk.Rows[i - 1]["LSA_Y"].ToString());

                        //     }


                    }

                    foreach (DataRow row in dtBaselineChk.Rows)
                    {
                        try
                        {
                            row["lotStart"] = dtLot.Rows[int.Parse(row["index"].ToString())]["date"];
                            row["procName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["procName"];
                            row["progName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["progName"];
                            row["lotID"] = dtLot.Rows[int.Parse(row["index"].ToString())]["lotID"];
                            row["lotStart-chkDate"] = Convert.ToDateTime(row["chkDate"].ToString()).Subtract(Convert.ToDateTime(row["lotStart"].ToString())).TotalSeconds.ToString();


                        }
                        catch //(Exception ex)
                        {
                            //   MessageBox.Show("Error Code:" + ex.Message + "\n\nLot Data Not Available For Assist/OF_Retry, etc.");
                        }
                    }
                }

            }
            //wfr
            foreach (string item in new string[] { "lotStart", "procName", "progName", "lotID" })
            {
                dtWfr.Columns.Add(item);
            }
            if (boolArr[4])
            {
                foreach (DataRow row in dtWfr.Rows)
                {
                    try
                    {
                        row["lotStart"] = dtLot.Rows[int.Parse(row["index"].ToString())]["date"];
                        row["procName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["procName"];
                        row["progName"] = dtLot.Rows[int.Parse(row["index"].ToString())]["progName"];
                        row["lotID"] = dtLot.Rows[int.Parse(row["index"].ToString())]["lotID"];
                    }
                    catch //(Exception ex)
                    {
                        //   MessageBox.Show("Error Code:" + ex.Message + "\n\nLot Data Not Available For Assist/OF_Retry, etc.");
                    }
                }
            }
            //raw data
            DataTable dtRaw = new DataTable();
            dtRaw.Columns.Add("Content");
            if (boolArr[0])
            {
                foreach (var x in log)
                {
                    DataRow newRow = dtRaw.NewRow(); newRow[0] = x; dtRaw.Rows.Add(newRow);
                }
            }
            #endregion


            #region 汇总数据

            ;
            DataTable dtSum= singleSummaryData(new DataTable[] { dtLot, dtAssist, dtOfretry, dtRecoverErr, dtWfr });
            if (boolArr[0]) { } else { dtLot = null;dtLot = new DataTable(); }
            if (boolArr[1]) { } else { dtAssist = null; dtAssist = new DataTable(); }
            if (boolArr[2]) { } else { dtOfretry = null; dtOfretry = new DataTable(); }
            if (boolArr[3]) { } else { dtRecoverErr = null; dtRecoverErr = new DataTable(); }
            if (boolArr[4]) { } else { dtWfr = null;dtWfr = new DataTable(); }
            if (boolArr[5]) { } else { dtBaselineChk = null;dtBaselineChk = new DataTable(); }

            if (flag)  //单台设备抽取
            {
                if (boolSaveRaw)
                {
                    myDic.Add(tool, new DataTable[] { dtRaw, dtLot, dtAssist, dtOfretry, dtRecoverErr, dtWfr, dtSum,dtBaselineChk });
                }
                else
                {
                    myDic.Add(tool, new DataTable[] { new DataTable(), dtLot, dtAssist, dtOfretry, dtRecoverErr, dtWfr, dtSum,dtBaselineChk });
                }
            }
            else //all情况下，无原始数据
            {
                myDic.Add(tool, new DataTable[] { new DataTable(), dtLot, dtAssist, dtOfretry, dtRecoverErr, dtWfr ,dtSum}); 
            }
            dtRaw = dtLot = dtAssist = dtOfretry = dtRecoverErr = dtWfr=dtSum = null;
            #endregion


            lotArr = null; lotList = null; assistArr = null; assistList = null; ofretryArr = null; ofretryList = null;
            recoverErrArr = null; recoverErrList = null; wfrArr = null; wfrList = null;

            GC.Collect();
        }

        private static DataTable singleSummaryData(DataTable[] dts)
        {
            DataTable dtSum = new DataTable();
            DataTable dt, dt1;



            dtSum.Columns.Add("Description", typeof(string));
            dtSum.Columns.Add("Date", typeof(string));
            dtSum.Columns.Add("Count", typeof(double));
            dtSum.Columns.Add("TotalSeconds", typeof(double));

            //lot
            try
            {
                dt = new DataTable();
                dt.Columns.Add("date", typeof(string));
                foreach (DataRow row in dts[0].Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = row[0].ToString().Substring(0, 10);
                    dt.Rows.Add(dr);
                }

                dt1 = dt.DefaultView.ToTable(true, "date");
                foreach (DataRow key in dt1.Rows)
                {
                    DataRow[] drs = dt.Select("date='" + key[0].ToString() + "'");
                    DataRow dr = dtSum.NewRow();
                    dr[0] = "LotCount";
                    dr[1] = key[0].ToString();
                    dr[2] = drs.Length;
                    dtSum.Rows.Add(dr);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Summary_Lot_Data Error: " + ex.Message);
            }
            //wfr
            try
            {
                dt = null; dt = new DataTable();
                dt.Columns.Add("date", typeof(string));
                foreach (DataRow row in dts[4].Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = row["expEnd"].ToString().Substring(0, 10);
                    dt.Rows.Add(dr);
                }
                dt1 = dt.DefaultView.ToTable(true, "date");
                foreach (DataRow key in dt1.Rows)
                {
                    DataRow[] drs = dt.Select("date='" + key[0].ToString() + "'");
                    DataRow dr = dtSum.NewRow();
                    dr[0] = "WfrCount";
                    dr[1] = key[0].ToString();
                    dr[2] = drs.Length;
                    dtSum.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Summary_Wfr_Data Error: " + ex.Message);
            }
            //assist
            try
            {
                if (dts[1].Rows.Count == 0)
                {
                    DataRow dr = dtSum.NewRow();
                    dr[0] = "Assist";
                    dr[1] = "";
                    dr[2] = 0;
                    dr[3] = 0;
                    dtSum.Rows.Add(dr);

                }
                else
                {
                    dt = null; dt = new DataTable();
                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("secondsElapsed", typeof(int));
                    foreach (DataRow row in dts[1].Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = row["assistEnd"].ToString().Substring(0, 10);
                        dr[1] = row["secondsElapsed"].ToString();
                        dt.Rows.Add(dr);
                    }
                    dt1 = dt.DefaultView.ToTable(true, "date");
                    foreach (DataRow key in dt1.Rows)
                    {
                        DataRow[] drs = dt.Select("date='" + key[0].ToString() + "'");
                        DataTable tmp = dt.Clone();
                        foreach (DataRow row in drs)
                        { tmp.Rows.Add(row.ItemArray); }

                        DataRow dr = dtSum.NewRow();
                        dr[0] = "Assist";
                        dr[1] = key[0].ToString();
                        dr[2] = tmp.Compute("count(secondsElapsed)", "");
                        dr[3] = tmp.Compute("sum(secondsElapsed)", "");
                        dtSum.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Summary_Assist_Data Error: " + ex.Message);
            }
            //ofretry
            try
            {
                if (dts[2].Rows.Count == 0)
                {
                    DataRow dr = dtSum.NewRow();
                    dr[0] = "OF_Retry";
                    dr[1] = "";
                    dr[2] = 0;
                    dr[3] = 0;
                    dtSum.Rows.Add(dr);

                }
                else
                {
                    dt = null; dt = new DataTable();
                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("secondsElapsed", typeof(int));
                    foreach (DataRow row in dts[2].Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = row["ofretryEnd"].ToString().Substring(0, 10);
                        dr[1] = row["secondsElapsed"].ToString();
                        dt.Rows.Add(dr);
                    }
                    dt1 = dt.DefaultView.ToTable(true, "date");
                    foreach (DataRow key in dt1.Rows)
                    {
                        DataRow[] drs = dt.Select("date='" + key[0].ToString() + "'");
                        DataTable tmp = dt.Clone();
                        foreach (DataRow row in drs)
                        { tmp.Rows.Add(row.ItemArray); }

                        DataRow dr = dtSum.NewRow();
                        dr[0] = "OF_Retry";
                        dr[1] = key[0].ToString();
                        dr[2] = tmp.Compute("count(secondsElapsed)", "");
                        dr[3] = tmp.Compute("sum(secondsElapsed)", "");
                        dtSum.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Summary_OF-Retry_Data Error: " + ex.Message);
            }
            //error
            try
            {
                if (dts[3].Rows.Count == 0)
                {
                    DataRow dr = dtSum.NewRow();
                    dr[0] = "TotalErr";
                    dr[1] = "";
                    dr[2] = 0;
                    dr[3] = 0;
                    dtSum.Rows.Add(dr);

                }
                else
                {
                    dt = null; dt = new DataTable();
                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("secondsElapsed", typeof(int));
                    dt.Columns.Add("error", typeof(string));
                    foreach (DataRow row in dts[3].Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = row["recoverErrEnd"].ToString().Substring(0, 10);
                        dr[1] = row["secondsElapsed"].ToString();
                        dr[2] = row["error"].ToString().Split(new char[] { ' ' })[3];
                        dt.Rows.Add(dr);
                    }
                    dt1 = dt.DefaultView.ToTable(true, "date");
                    foreach (DataRow key in dt1.Rows)
                    {
                        DataRow[] drs = dt.Select("date='" + key[0].ToString() + "'");
                        DataTable tmp = dt.Clone();
                        foreach (DataRow row in drs)
                        { tmp.Rows.Add(row.ItemArray); }

                        DataRow dr = dtSum.NewRow();
                        dr[0] = "TotalErr";
                        dr[1] = key[0].ToString();
                        dr[2] = tmp.Compute("count(secondsElapsed)", "");
                        dr[3] = tmp.Compute("sum(secondsElapsed)", "");
                        dtSum.Rows.Add(dr);
                    }
                    dt1 = dt.DefaultView.ToTable(true, "date", "error");
                    foreach (DataRow key in dt1.Rows)
                    {
                        DataRow[] drs = dt.Select("date='" + key[0].ToString() + "' and error='" + key[1].ToString() + "'");
                        DataTable tmp = dt.Clone();
                        foreach (DataRow row in drs)
                        { tmp.Rows.Add(row.ItemArray); }
                        DataRow dr = dtSum.NewRow();
                        dr[0] = "Err:" + key[1].ToString();
                        dr[1] = key[0].ToString();
                        dr[2] = tmp.Compute("count(secondsElapsed)", "");
                        dr[3] = tmp.Compute("sum(secondsElapsed)", "");
                        dtSum.Rows.Add(dr);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Summary_Recover-Err_Data Error: " + ex.Message);
            }

            return dtSum;
        }





        public static void queryByLotID(ref DataTable dtShow, ref DataTable dt)
        {
            
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Sequence Txt File Is Not Read Or Data Is Not Saved.\n\nNow,Exit...."); return;
            }
            dtShow = null; dtShow = new DataTable(); dtShow.Columns.Add("Content");
            string str = Interaction.InputBox("请输入LotID", "定义LotID", "", -1, -1);
            str = str.Trim().ToUpper().Replace(".", "_");
            bool flag = false;
            for(int i=0;i < dt.Rows.Count;++i)
            {
     
                if (dt.Rows[i][0].ToString().Contains("'ENTRY NAME' \"" + str + "\""))
                {
                    flag = true;
                    if(i>0)
                    {
                        DataRow toAdd1 = dtShow.NewRow();
                        toAdd1[0] = dt.Rows[i-1][0].ToString();
                        dtShow.Rows.Add(toAdd1);

                    }
                    DataRow toAdd = dtShow.NewRow();
                    toAdd[0] = dt.Rows[i][0].ToString();
                    dtShow.Rows.Add(toAdd);
                }
                if(flag==true && (!(dt.Rows[i][0].ToString().Contains("[OCS_ENTER]"))))
                {
                    DataRow toAdd = dtShow.NewRow();
                    toAdd[0] = dt.Rows[i][0].ToString();
                    dtShow.Rows.Add(toAdd);
                }
                //else if (flag == true && (row[0].ToString().Contains("\"PP ENTRY CREATED\"                         [OCS_ENTER]")))
                else if (flag == true && (dt.Rows[i][0].ToString().Contains("[OCS_ENTER]")))
                {
                    DataRow toAdd = dtShow.NewRow();
                    toAdd[0] = dt.Rows[i][0].ToString();
                    dtShow.Rows.Add(toAdd);
                    return;
                }
            }
           
     
                
            




        }
        public static void queryByKey(ref DataTable dtShow, ref DataTable dt)
        {

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Sequence Txt File Is Not Read Or Data Is Not Saved.\n\nNow,Exit...."); return;
            }
            string str = Interaction.InputBox("请输入查询关键字\n\n\n查询显示log中第一个匹配关键字的前100行和后5000行\n\n\n另，关键字时间格式   2020-06-11 00:00:51  按需求输入前几位即可，其它类似", "定义关键字", "", -1, -1);
            str = str.Trim().ToUpper();

            dtShow = null; dtShow = new DataTable(); dtShow.Columns.Add("Content");

            int s = 0, e = 0;
            for (int i=0;i<dt.Rows.Count;++i)
            {
                if (dt.Rows[i][0].ToString().Contains(str))
                {
                    if(i-100>=0)
                    { s = i - 100; }
                    else
                    { s = 0; }
                    if(i+5000<dt.Rows.Count)
                    { e = i + 5000; }
                    else
                    { e = dt.Rows.Count;}
                    break;
                }
            }

            for (int i=s;i<e;++i)
            {
                DataRow newRow = dtShow.NewRow();
                newRow[0] = dt.Rows[i][0].ToString();
                dtShow.Rows.Add(newRow);
            }


       
        
        }

    }

}