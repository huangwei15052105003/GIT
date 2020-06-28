using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

//using ICSharpCode.SharpZipLib.BZip2;
//using ICSharpCode.SharpZipLib.GZip;
//using ICSharpCode.SharpZipLib.Tar;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Core;

using System.Net.Sockets;
using System.Net; //get computer ip

using System.IO.Compression;
using System.Reflection;



namespace exposureRecipeNet3
{
    public class LibF
    {


        public static DataTable CsmcOracle(string sql) //https://www.cnblogs.com/mq0036/p/3678148.html;https://www.cnblogs.com/worfdream/articles/2938658.html
        {

            string connStr;
            connStr = "Provider=MSDAORA.1;Password=rptlinkpw;User ID=rptlink;Data Source=MFGEXCEL;";
            DataTable dt;

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open(); using (OleDbCommand command = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        { da.Fill(ds); dt = ds.Tables[0]; ; }
                    }
                }
            }
            return dt;

        }

        public static void DosCommand(string cmdLine)
        {
            ///https://blog.csdn.net/qq_21747999/article/details/79151910
            ///https://www.cnblogs.com/zhaoshujie/p/10612654.html

            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments ="/c " + cmdLine;
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
           // MessageBox.Show(" DOS COMMAND DONE !! \r\n\r\n" + output);
        }
        public static DataTable OpenCsvWithComma(string filepath)
        {
            DataTable dt = new DataTable();
            using(TextFieldParser parser =new TextFieldParser(filepath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int n = 0;
                
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    n += 1;
                    if (n==1)
                    {
                        //创建列
                        foreach (string field in fields)
                        {
                            dt.Columns.Add(field);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        for (int cc=0;cc<fields.Length;cc++)
                        {
                            dr[cc] = fields[cc];
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }


            return dt;
        }

        public static DataTable OpenCsv(string filePath)
        {
            ///原始数据中有 “ ，”就会有问题
            //https://www.cnblogs.com/trueideal/archive/2010/03/05/1679351.html
            // https://www.cnblogs.com/allen0118/p/7217941.html 
            // Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            //     if (aryLine != null && aryLine.Length > 0)
            //     {
            //         dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            //      }
            sr.Close();
            fs.Close();
            return dt;
        }
        public static DataTable OpenCsvNew(string filePath)
        {
            ///原始数据中有 “ ，”就会有问题
            //https://www.cnblogs.com/trueideal/archive/2010/03/05/1679351.html
            // https://www.cnblogs.com/allen0118/p/7217941.html 
            // Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;

            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据

            long count = 0;
            while ((strLine = sr.ReadLine()) != null)
            {

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    ///如何 arrLine的长度大于tableHead的长度，tableHead需要重新设置
                    ///字段内容内有逗号，字段内容有双引号
                    if (aryLine.Length > tableHead.Length && count == 1)
                    {

                        bool flag;
                        if (strLine[0].ToString() == "\"") { flag = false; }
                        else { flag = true; }
                        int m = 0; //tableHead索引
                        int n = 0;// 双引号计数
                        int j = 0;//双引号内逗号分割字段计数
                        List<string> tableHeadNew = new List<string>();



                        for (int i = 0; i < strLine.Length - 1; i++)
                        {
                            //MessageBox.Show(strLine[i].ToString()+",   "+ (strLine[i].ToString() == "\"").ToString());
                            if (strLine[i].ToString() == "\"")
                            {
                                if (n == 0) { flag = false; j = 0; n += 1; }
                                else { flag = true; n = 0; }
                            }
                            else
                            {
                                if (strLine[i].ToString() == "," && flag == true)
                                { tableHeadNew.Add(tableHead[m]); m += 1; }
                                else if (strLine[i].ToString() == "," && flag == false)
                                { tableHeadNew.Add(tableHead[m] + j.ToString()); j += 1; }
                            }
                        }

                        ///不论最后一个字符是什么，都加入
                        ///前提是假设标题行，没有逗号
                        tableHeadNew.Add(tableHead[m]);


                        columnCount = 0;
                        dt.Columns.Clear();
                        foreach (var x in tableHeadNew)
                        {
                            DataColumn dc = new DataColumn(x);
                            dt.Columns.Add(dc);
                            columnCount += 1;
                        }








                    }






                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
                count += 1;
            }

            //  if (aryLine != null && aryLine.Length > 0)
            //   {
            //      dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            //   }
            sr.Close();
            fs.Close();
            return dt;
        }
        public static DataTable OpenCsvNew1(string filePath)  //ESF 可作业设备
        {
            DataTable dt = new DataTable();
            List<string> listrow = new List<string>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var eachLineStr = sr.ReadLine();
                    eachLineStr = eachLineStr.Replace("'", "");
                    eachLineStr = eachLineStr.Replace("\"", "");
                    eachLineStr = eachLineStr.Replace("\"", "");
                    eachLineStr = eachLineStr.Replace("{", "");
                    eachLineStr = eachLineStr.Replace("}", "");
                    eachLineStr = eachLineStr.Replace("set()", "NONE");
                    listrow.Add(eachLineStr);

                }

                foreach (string x in listrow[0].Split(new char[] { ',' }))
                { dt.Columns.Add(x); }

                for (int i = 1; i < listrow.Count; i++)
                {
                    string[] tmp = listrow[i].Split(new char[] { ',' });



                    DataRow dr = dt.NewRow();
                    dr["RECPID"] = tmp[0];
                    dr["EQPTYPE"] = tmp[1];
                    dr["STAGE"] = tmp[2];
                    dr["PART"] = tmp[3];
                    dr["TRACKRECIPE"] = tmp[4];
                    dr["LAYER"] = tmp[5];
                    dr["TECH"] = tmp[6];
                    string str = string.Empty;
                    for (int j = 7; j < tmp.Length - 1; j++)
                    {
                        str += (tmp[j] + " ");
                        dr["Available"] = str;
                    }
                    dr["ToolQty"] = tmp[tmp.Length - 1];
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
        public static DataTable OpenCsvNew2(string filePath)  //CD SEM IDP
        {
            DataTable dt = new DataTable();
            List<string> listrow = new List<string>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var eachLineStr = sr.ReadLine();
                    eachLineStr = eachLineStr.Replace("'", "");
                    eachLineStr = eachLineStr.Replace("\"", "");
                    eachLineStr = eachLineStr.Replace("\"", "");
                    eachLineStr = eachLineStr.Replace("{", "");
                    eachLineStr = eachLineStr.Replace("}", "");
                    eachLineStr = eachLineStr.Replace("set()", "NONE");
                    listrow.Add(eachLineStr);

                }

                foreach (string x in listrow[0].Split(new char[] { ',' }))
                { dt.Columns.Add(x); }
                /*
                for (int i = 1; i < listrow.Count; i++)
                {
                    string[] tmp = listrow[i].Split(new char[] { ',' });



                    DataRow dr = dt.NewRow();
                    dr["RECPID"] = tmp[0];
                    dr["EQPTYPE"] = tmp[1];
                    dr["STAGE"] = tmp[2];
                    dr["PART"] = tmp[3];
                    dr["TRACKRECIPE"] = tmp[4];
                    dr["LAYER"] = tmp[5];
                    dr["TECH"] = tmp[6];
                    string str = string.Empty;
                    for (int j = 7; j < tmp.Length - 1; j++)
                    {
                        str += (tmp[j] + " ");
                        dr["Available"] = str;
                    }
                    dr["ToolQty"] = tmp[tmp.Length - 1];
                    dt.Rows.Add(dr);
                }
                */
            }
            return dt;
        }
        static void GetFileList(string filePath)
        {
            List<string> list = new List<string>();
            list = new List<string>();
            DirectoryInfo d = new DirectoryInfo(filePath);
            FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)     //判断是否为文件夹
                {
                    GetFileList(fsinfo.FullName);//递归调用
                }
                else
                {

                    list.Add(fsinfo.FullName);//输出文件的全部路径         
                }
            }
        }
        public static List<string> ExportFileList(string path, List<string> fileList)
        {

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                //int size = Convert.ToInt32(f.Length);                
                //long size = f.Length;
                fileList.Add(f.FullName);//添加文件路径到列表中            
            }
            foreach (DirectoryInfo d in dii)
            {
                ExportFileList(d.FullName, fileList);
            }

            return fileList;


        }

        public static bool CheckTableExist(string connStr, string tblName)
        {
            string sql; DataTable dt;
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select count(*)  from sqlite_master where type = 'table' and name = '" + tblName + "'";
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

            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static public void CreateSqliteTable(string connStr, string tableName, string item)
        {

            SQLiteConnection sqliteConn = new SQLiteConnection(connStr);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqliteConn;
                cmd.CommandText = "CREATE TABLE " + tableName + " " + item;
                cmd.ExecuteNonQuery();
            }
            sqliteConn.Close();
        }

        static public DataTable GetTabFieldName(string connStr)
        {
    
            DataTable dt = new DataTable();
            dt.Columns.Add("TblName");
            StringBuilder tblNames = new StringBuilder();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                // 获取数据库中的所有表名
                string sqlTableNames = "select name from sqlite_master where type='table' order by name;";
                // 创建命令对象
                SQLiteCommand cmd = new SQLiteCommand(sqlTableNames, conn);
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // 表名
                        tblNames.Append(dr["Name"] + ",");
                        DataRow row =dt.NewRow();
                        row["TblName"] = dr["Name"].ToString();dt.Rows.Add(row);
                    }

                }
            }
         
           string[] tblArr = Convert.ToString(tblNames).Split(',');
           string tblName = string.Empty;

           

       
          for (int i=0;i<tblArr.Length-1;i++)
          {
              tblName = tblArr[i];
              using (SQLiteConnection conn = new SQLiteConnection(connStr))
              {
                  conn.Open();
                  if (!string.IsNullOrEmpty(tblName)) //多余，tblNames最后一位是“，”
                  {
                      // 获取表中的所有字段名
                      string sqlfieldName = "Pragma Table_Info(" + tblName + ")";
                      // 创建命令对象
                      SQLiteCommand cmd = new SQLiteCommand(sqlfieldName, conn);
                      using (SQLiteDataReader dr = cmd.ExecuteReader())
                      {
                          int n = 0;
                          string tmp = string.Empty;
                          while (dr.Read())
                          {
                              // 字段名
                              tmp+=(dr["Name"] + ",");
                              n += 1;
                          }
                          if (n >(dt.Columns.Count-1) ) 
                          { 
                              for (int j=dt.Columns.Count;j<=n;j++)
                              {
                                  dt.Columns.Add("字段"+j.ToString());
                              }
                          }
                          for (int k=0;k<n;k++)
                          {
                              dt.Rows[i][k + 1] = tmp.Split(',')[k];
                          }
                      }
                  }
              }


          }

 
            return dt;
        }

        public static string RunDosCommand(string command, int seconds)
        {
            string output = ""; //输出字符串
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;


        }

        #region //datatable merge
        private DataTable unite_on_datatable(DataTable dt1, DataTable dt2, string key) //https://www.cnblogs.com/cysisu/p/10753195.html
        {
            DataTable dt3 = dt1.Clone();
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                if (dt2.Columns[i].ColumnName.ToLower() != key.ToLower())
                {
                    dt3.Columns.Add(dt2.Columns[i].ColumnName);
                }
            }

            //先排序
            dt1 = sort_desc(dt1, key);
            dt2 = sort_desc(dt2, key);

            int count1 = 0, count2 = 0;
            while (true)
            {
                string heatid1 = dt1.Rows[count1][key].ToString();
                string heatid2 = dt2.Rows[count2][key].ToString();
                if (count1 >= (dt1.Rows.Count) || (count2 >= (dt2.Rows.Count)))
                    return dt3;

                if (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) > 0)
                {
                    //找到
                    while (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) > 0)
                    {
                        string heat_id1 = dt1.Rows[count1][key].ToString();
                        string heat_id2 = dt2.Rows[count2][key].ToString();
                        count1++;
                        if (count1 >= dt1.Rows.Count)
                            return dt3;
                    }

                    while (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) < 0)
                    {
                        count2++;
                        if (count2 >= dt2.Rows.Count)
                            return dt3;
                    }
                }

                else if (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) < 0)
                {
                    while (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) < 0)
                    {
                        count2++;
                        if (count2 >= dt2.Rows.Count)
                            return dt3;
                    }

                    //找到
                    while (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) > 0)
                    {
                        count1++;
                        if (count1 >= dt1.Rows.Count)
                            return dt3;
                    }
                }


                if (string.Compare(dt1.Rows[count1][key].ToString(), dt2.Rows[count2][key].ToString()) == 0)
                {
                    //赋值给新的一列
                    DataRow dr = dt3.NewRow();
                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        dr[dt1.Columns[i].ColumnName] = dt1.Rows[count1][dt1.Columns[i].ColumnName];
                    }
                    for (int i = 0; i < dt2.Columns.Count; i++)
                    {
                        dr[dt2.Columns[i].ColumnName] = dt2.Rows[count2][dt2.Columns[i].ColumnName];
                    }
                    dt3.Rows.Add(dr.ItemArray);
                    count1++;
                    count2++;
                }
            }

        }


        //对DataTable排序
        private DataTable sort_desc(DataTable dt1, string key)
        {
            DataTable dt2 = dt1.Clone();
            DataRow[] dr = dt1.Select("", key + " desc");
            for (int i = 0; i < dr.Length; i++)
            {
                dt2.Rows.Add(dr[i].ItemArray);
            }
            return dt2;
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
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


        #endregion

        public static void dataTableToCsv(DataTable table, string file)
        {
            FileInfo fi = new FileInfo(file);
            string path = fi.DirectoryName;
            string name = fi.Name;
            //\/:*?"<>|
            //把文件名和路径分别取出来处理
            name = name.Replace(@"\", "＼");
            name = name.Replace(@"/", "／");
            name = name.Replace(@":", "：");
            name = name.Replace(@"*", "＊");
            name = name.Replace(@"?", "？");
            name = name.Replace(@"<", "＜");
            name = name.Replace(@">", "＞");
            name = name.Replace(@"|", "｜");
            string title = "";

            FileStream fs = new FileStream(path + "\\" + name, FileMode.Create);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += table.Columns[i].ColumnName + ",";
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);

            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    line += row[i].ToString().Replace(",", "_") + ",";
                }
                line = line.Substring(0, line.Length - 1) + "\n";

                sw.Write(line);
            }

            sw.Close();
            fs.Close();
        }

        public static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
          //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            IPAddress localaddr = localhost.AddressList[0];

            return localaddr.ToString();
        }

        public static DataTable ReadExcel(string filename,string sheetname)
        {
            DataTable dt = new DataTable();
            string excelStr = string.Empty;
            string sql;

            if (filename.Substring(filename.Length-3,3).ToUpper()=="XLS")
            {
                excelStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
            }
            else
            {
                excelStr = "Provider=Microsoft.Ace.OleDb.12.0;data source='" + filename + "';Extended Properties='Excel 12.0; HDR=YES; IMEX=1';";//xlsx
            }
            try
            {
                using (OleDbConnection OleConn = new OleDbConnection(excelStr))
                {
                    OleConn.Open();
                    sql = string.Format("SELECT * FROM  [{0}]", sheetname);
             
                    using (OleDbDataAdapter da = new OleDbDataAdapter(sql, excelStr))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dt = ds.Tables[0];
                    }

                }
           
            }
            catch (Exception)
            {
                MessageBox.Show("读取Excel失败");
            }




            return dt;
        }

    }
    /*
    class GzipFile
  
    {
        public static void gZipFile(string filePath, string zipFilePath)
        {
            Stream s = new GZipOutputStream(File.Create(zipFilePath));
            FileStream fs = File.OpenRead(filePath);
            int size;
            byte[] buf = new byte[4096];
            do
            {
                size = fs.Read(buf, 0, buf.Length);
                s.Write(buf, 0, size);
            } while (size > 0);
            s.Close();
            fs.Close();
        }
        public static void gunZipFile(string zipFilePath, string filePath)

        {
            using (Stream inStream = new GZipInputStream(File.OpenRead(zipFilePath)))
            using (FileStream outStream = File.Create(filePath))
            {
                byte[] buf = new byte[4096];
                StreamUtils.Copy(inStream, outStream, buf);
               
            }

            //https://docs.microsoft.com/en-us/dotnet/api/system.io.compression.gzipstream?view=netframework-3.5

        }

    }
    */
    public class DataTableToSQLte   //https://blog.csdn.net/qq_42678477/article/details/81660682
    {
        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private string insertHead;
        public string InsertHead
        {
            get { return insertHead; }
        }
        private string[] separators;
        public string[] Separators
        {
            get { return separators; }
            set { separators = value; }
        }
        private string insertCmdText;
        private int colCount;
        private string[] fields;

        public DataTableToSQLte(DataTable dt, string dbTblName)
        {
            List<string> myFields = new List<string>();
            List<string> mySeparators = new List<string>();
            List<string> valueVars = new List<string>();// insert command text            
            colCount = dt.Columns.Count;
            for (int i = 0; i < colCount; i++)
            {
                string colName = dt.Columns[i].ColumnName;
                myFields.Add(colName);
                mySeparators.Add(GetSeperator(dt.Columns[i].DataType.ToString()));
                valueVars.Add("@" + colName);

            }
            // insertHead = string.Format("insert into {0} ({1})"                
            //    , dt.TableName                
            //   , string.Join(",", myFields.ToArray()));

            insertHead = string.Format("insert into {0} ({1})"
                , dbTblName
                , string.Join(",", myFields.ToArray()));

            separators = mySeparators.ToArray();
            insertCmdText = string.Format("{0} values ({1})", insertHead
                , string.Join(",", valueVars.ToArray()));

            fields = myFields.ToArray();
        }
        private string GetSeperator(string typeName)
        {
            string result = string.Empty;
            switch (typeName)
            {
                case "System.String":
                    result = "'";
                    break;

                default:
                    result = typeName;
                    break;
            }
            return result;
        }
        public string GenInsertSql(DataRow dr)
        {
            List<string> strs = new List<string>();
            for (int i = 0; i < colCount; i++)
            {
                if (DBNull.Value == dr[i])  //null or DBNull                    
                    strs.Add("null");
                else
                    strs.Add(string.Format("{0}{1}{0}", separators[i], dr[i].ToString()));
            }
            return string.Format("{0} values ({1})", insertHead, string.Join(",", strs.ToArray()));


        }
        public void ImportToSqliteBatch(DataTable dt, string dbFullName)
        {
            string strConn = string.Format("data source={0}", dbFullName);

            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                using (SQLiteCommand insertCmd = conn.CreateCommand())
                {
                    insertCmd.CommandText = insertCmdText;
                    conn.Open();
                    SQLiteTransaction tranction = conn.BeginTransaction();
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            object o = null;
                            string paraName = "@" + fields[i];
                            if (DBNull.Value != dr[fields[i]])
                                o = dr[fields[i]];
                            insertCmd.Parameters.AddWithValue(paraName, o);

                        }

                        insertCmd.ExecuteNonQuery();

                    }
                    tranction.Commit();
                }
            }
        }

    }

    public static class PingServicecs

    {

        private const int TIME_OUT = 100;

        private const int PACKET_SIZE = 512;

        private const int TRY_TIMES = 2;

        //检查时间的正则

        private static Regex _reg = new Regex(@"时间=(.*?)ms", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        /// <summary>

        /// 结果集

        /// </summary>

        /// <param name="stringcommandLine">字符命令行</param>

        /// <param name="packagesize">丢包大小</param>

        /// <returns></returns>

        public static string LauchPing(string stringcommandLine, int packagesize)

        {

            Process process = new Process();

            process.StartInfo.Arguments = stringcommandLine;

            process.StartInfo.UseShellExecute = false;

            process.StartInfo.CreateNoWindow = true;

            process.StartInfo.FileName = "ping.exe";

            process.StartInfo.RedirectStandardInput = true;

            process.StartInfo.RedirectStandardOutput = true;

            process.StartInfo.RedirectStandardError = true;

            process.Start();

            return process.StandardOutput.ReadToEnd();//返回结果



        }

        /// <summary>

        /// 转换字节

        /// </summary>

        /// <param name="strBuffer">缓冲字符</param>

        /// <param name="packetSize">丢包大小</param>

        /// <returns></returns>

        private static float ParseResult(string strBuffer, int packetSize)

        {

            if (strBuffer.Length < 1)

                return 0.0F;

            MatchCollection mc = _reg.Matches(strBuffer);

            if (mc == null || mc.Count < 1 || mc[0].Groups == null)

                return 0.0F;

            if (!int.TryParse(mc[0].Groups[1].Value, out int avg))

                return 0.0F;

            if (avg <= 0)

                return 1024.0F;

            return (float)packetSize / avg * 1000 / 1024;

        }

        /// <summary>

        /// 通过Ip或网址检测调用Ping 返回 速度

        /// </summary>,

        /// <param name="strHost"></param>

        /// <returns></returns>

        public static string Test(string strHost, int trytimes, int PacketSize, int TimeOut)

        {

            return LauchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, trytimes, PacketSize, TimeOut), PacketSize);

        }

        /// <summary>

        /// 地址

        /// </summary>

        /// <param name="strHost"></param>

        /// <returns></returns>

        public static string Test(string strHost)

        {

            return LauchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TRY_TIMES, PACKET_SIZE, TIME_OUT), PACKET_SIZE);

        }

    }

    ///https://www.cnblogs.com/mq0036/p/3707257.html

    public class JoinTbl
    {
        /// <summary>
        /// 连接两个表
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="FJC"></param>
        /// <param name="SJC"></param>
        /// <returns></returns>
        public static DataTable JoinTwoTable(DataTable First, DataTable Second, string FJC, string SJC)
        {
            return JoinTwoTable(First, Second, new DataColumn[] { First.Columns[FJC]
}, new DataColumn[] { First.Columns[SJC] });
        }


        /// <summary>
        /// 连接两个表
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="FJC"></param>
        /// <param name="SJC"></param>
        /// <returns></returns>
        protected static DataTable JoinTwoTable(DataTable First, DataTable Second, DataColumn FJC, DataColumn SJC)
        {
            return JoinTwoTable(First, Second, new DataColumn[] { FJC }, new DataColumn[] { SJC });
        }

        /// <summary>
        /// 连接两个Table
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="FJC"></param>
        /// <param name="SJC"></param>
        /// <returns></returns>
        protected static DataTable JoinTwoTable(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
        {

            //创建一个新的DataTable
            DataTable table = new DataTable("Join");

            using (DataSet ds = new DataSet())
            {

                //把DataTable Copy到DataSet中
                DataTable tmp1 = First.Copy();
                DataTable tmp2 = Second.Copy();
                tmp1.TableName = "tbl1";
                tmp2.TableName = "tabl2";
                ds.Tables.AddRange(new DataTable[] { tmp1, tmp2 });
                // ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });


                DataColumn[] parentcolumns = new DataColumn[FJC.Length];

                for (int i = 0; i < parentcolumns.Length; i++)
                {
                    parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
                }

                DataColumn[] childcolumns = new DataColumn[SJC.Length];

                for (int i = 0; i < childcolumns.Length; i++)
                {
                    childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
                }


                //创建关联

                DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
                ds.Relations.Add(r);


                //为关联表创建列
                for (int i = 0; i < First.Columns.Count; i++)
                {
                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                }

                for (int i = 0; i < Second.Columns.Count; i++)
                {
                    //看看有没有重复的列，如果有在第二个DataTable的Column的列明后加_Second
                    if (!table.Columns.Contains(Second.Columns[i].ColumnName))
                        table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
                    else
                        table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);
                }


                table.BeginLoadData();

                foreach (DataRow firstrow in ds.Tables[0].Rows)
                {
                    //得到行的数据
                    DataRow[] childrows = firstrow.GetChildRows(r);

                    if (childrows != null && childrows.Length > 0)
                    {
                        object[] parentarray = firstrow.ItemArray;

                        foreach (DataRow secondrow in childrows)
                        {
                            object[] secondarray = secondrow.ItemArray;

                            object[] joinarray = new object[parentarray.Length + secondarray.Length];

                            Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);

                            Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                            table.LoadDataRow(joinarray, true);
                        }
                    }
                }

                table.EndLoadData();
            }


            return table;

        }
    }



 











}