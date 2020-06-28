using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;
using System.IO;
//using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.Net; //get computer ip
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using System.Reflection;



namespace TaskExe
{
    class Share
    {
        //访问CSMC MFG ORACLE
        public static DataTable CsmcOracle(string sql)
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

        //运行DOS命令
        public static void DosCommand(string cmdLine)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/c " + cmdLine;
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
            //  Console.WriteLine(" DOS COMMAND DONE !! \r\n\r\n" + output);
        }

        //打开CSV文件
        public static DataTable OpenCsvWithComma(string filepath)
        {
            DataTable dt = new DataTable();
            using (TextFieldParser parser = new TextFieldParser(filepath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int n = 0;

                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    n += 1;
                    if (n == 1)
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
                        for (int cc = 0; cc < fields.Length; cc++)
                        {
                            dr[cc] = fields[cc];
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        //递归列出文件清单
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

        public static void DataTableToCsv(DataTable table, string file)
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

        //获得本机IP地址
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostName);    //可以获取IPv6的地址
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        //检查SQLITE中表是否存在
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

        //创建SQLITE表
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

        public static DataTable ReadExcel(string filename, string sheetname)
        {
            DataTable dt = new DataTable();
            string excelStr = string.Empty;
            string sql;

            if (filename.Substring(filename.Length - 3, 3).ToUpper() == "XLS")
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
                System.Console.WriteLine("读取Excel失败");
            }
            return dt;
        }

        //压缩文件
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

        //解压缩文件
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

        //DataTable Join--》New DataTable
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
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

        // FileCopy With Datetime Compare
        public static void DateTimeFileCopy(string src, string dest)
        {
            FileInfo srcFi = new FileInfo(src);
            FileInfo destFi = new FileInfo(dest);
            Console.WriteLine(srcFi.LastWriteTime.ToString()+" , "+ destFi.LastWriteTime.ToString());
      
            if (string.Compare(srcFi.LastWriteTime.ToString(), destFi.LastWriteTime.ToString()) > 0)
            {
                try
                {
                     File.Copy(src, dest, true);
                    Console.WriteLine( src + "===   ->  ===" + dest + " ##Done##\n");
                }
                catch
                {
                    Console.WriteLine("没有权限读写P:\\_SQLITE目录");
                }
            }
            else
            {
                Console.WriteLine("==="+src + "===IS NOT COPIED\n");
            }

        }

        public static bool FileDateCompare(string csvFileName,string dbFileName)
        {
            ///比较CSV文件和DB文件日期
            ///CSV文件+1天，如果小于DB日期，不更新
            ///获取文件日期 srcFi.LastWriteTime.ToString()
            FileInfo csvFi;
            FileInfo dbFi;
            string csvDate;
            string dbDate;
            

            csvFi = new FileInfo(csvFileName);
            dbFi = new FileInfo(dbFileName);
            csvDate = csvFi.LastWriteTime.AddDays(+1).ToString("yyyy-MM-dd HH:mm:ss");
            dbDate = dbFi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.Compare(csvDate, dbDate) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
        //复制目录
     

        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination, bool copySubDirs)
        {
            if (!destination.Exists)
            {
                destination.Create(); //目标目录若不存在就创建
            }
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                Console.WriteLine("开始复制： "+Path.Combine(destination.FullName, file.Name));
                try
                {
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), true); //复制目录中所有文件
                }
                catch (Exception ex)
                {                 
                   Console.WriteLine( ex.Message);  

            }
        }
            if (copySubDirs)
            {
                DirectoryInfo[] dirs = source.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    string destinationDir = Path.Combine(destination.FullName, dir.Name);
                    CopyDirectory(dir, new DirectoryInfo(destinationDir), copySubDirs); //复制子目录
                    
                }
            }
        }

    }

}
