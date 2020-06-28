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

namespace TaskExe
{
    class Server10_4_2_112
    {
        public static void RefreshRecipeLatestDate() //asml程序检查
        {
            #region initialize
            Console.WriteLine("因IT限制：\n\n    服务器程序日期在OA端电脑刷新；\n\n    光刻机程序日期在10.4.2.112刷新；");
            string[] allTools;
            string[] dirs;
            AsmlFtpWeb asmlFtpWeb;
            string[] files;
            string riqi1 = DateTime.Now.ToString("yyyy-MM-d") + " 24:59"; //部分日期没有年份
            string part = string.Empty;
            string riqi = string.Empty;
            List<string> list = new List<string>();
            string password;
            string tblName;
            DataRow[] drs;
            string lastYear = DateTime.Now.AddYears(-1).ToString("yyyy");
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=P:\\_SQLite\\AsmlRecipeCheck.mdb";
            string sql = string.Empty;
            DataTable dtShow, dt1;


            //read password
            using (StreamReader sr = new StreamReader("P:\\_SQLite\\password.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }


            Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
            { "ALSD82",new string[] {"sys.4666","10.4.152.29" } },
            { "ALSD83",new string[] {"sys.4730","10.4.151.64" } },
            { "ALSD85",new string[] {"sys.6450","10.4.152.37" } },
            { "ALSD86",new string[] {"sys.8144","10.4.152.31" } },
            { "ALSD87",new string[] {"sys.4142","10.4.152.46" } },
            { "ALSD89",new string[] {"sys.6158","10.4.152.39" } },
            { "ALSD8A",new string[] {"sys.5688","10.4.152.42" } },
            { "ALSD8B",new string[] {"sys.4955","10.4.152.47" } },
            { "ALSD8C",new string[] {"sys.9726","10.4.152.48" } },
            { "BLSD7D",new string[] {"sys.8111","10.4.131.32" } },
            { "BLSD08",new string[] {"sys.3527","10.4.131.63" } },
            { "SERVER",new string[] {"sys.1725","10.4.72.253" } },
            };
            string ip = Share.GetIpAddress();
            if (ip == "fe80::a488:7890:c268:8f28%12" || ip=="10.4.2.112")
            {
                allTools = new string[] { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD7D", "BLSD08" };
                dirs = new string[] { "/user_data/jobs/PROD" };
                password = list[1];
            }
            else
            {
                allTools = new string[] { "SERVER" };
                dirs = new string[] { "/user_data/jobs/PROD850", "/user_data/jobs/PROD700" };
                password = list[0];
            }
            list = null;

            #endregion

            foreach (string tool in allTools)
            {
                foreach (string dir in dirs)
                {
                    //定义表名
                    if (dir.Contains("850"))
                    { tblName = "SVR850"; }
                    else if (dir.Contains("700"))
                    { tblName = "SVR700"; }
                    else
                    { tblName = tool; }
                    //readdata from access


                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        sql = "SELECT * FROM " + tblName;
                        conn.Open();
                        using (OleDbCommand command = new OleDbCommand(sql, conn))
                        {
                            using (OleDbDataAdapter da = new OleDbDataAdapter(command))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    da.Fill(ds);
                                    dtShow = ds.Tables[0];

                                }
                            }
                        }
                    }

                    Console.WriteLine(ipUser[tool][1] + "," + "/usr/asm/data." + ipUser[tool][0].Substring(4, 4) + dir + "," + ipUser[tool][0] + "," + password);
                    asmlFtpWeb = new AsmlFtpWeb(ipUser[tool][1], "/usr/asm/data." + ipUser[tool][0].Substring(4, 4) + dir, ipUser[tool][0], password);



                    files = asmlFtpWeb.GetFilesDetailList("");




               
                    Console.WriteLine("PASSED: "+ tool + "," + dir + "," + files.Length.ToString());

                    dt1 = new DataTable(); dt1 = dtShow.Clone();

                    OleDbConnection connInsert = new OleDbConnection(connStr);
                    connInsert.Open();
                    OleDbCommand commandInsert;



                    foreach (string file in files)
                    {
                        if (file.Substring(0, 1) == "-" && file.Length > 54)
                        {
                            riqi = file.Substring(41, 12).Trim();
                            if (riqi.Contains(":"))
                            {
                                riqi = DateTime.Now.Year.ToString() + " " + riqi;//riqi分两种：带年份，无时间；有时间
                            }
                            riqi = DateTime.Parse(riqi).ToString("yyyy-MM-dd HH:mm");
                            if (string.Compare(riqi, riqi1) > 0)
                            {
                                riqi = lastYear + riqi.Substring(4, riqi.Length - 4);
                            }
                            part = file.Substring(54, file.Length - 54).Trim();


                            DataRow newRow = dt1.NewRow();
                            newRow[0] = part;
                            newRow[1] = riqi;
                            dt1.Rows.Add(newRow);


                            drs = dtShow.Select("Part='" + part + "' AND Riqi='" + riqi + "'");
                            if (drs.Length == 0)
                            {
                                sql = "INSERT INTO " + tblName + " (Part,Riqi)  VALUES ('" + part + "','" + riqi + "')";
                                commandInsert = new OleDbCommand(sql, connInsert);
                                commandInsert.ExecuteNonQuery();
                            }
                        }
                    }
                    commandInsert = null; connInsert.Close(); connInsert = null;

                }
            }
            Console.WriteLine("Refreshing Recipe Date Done");
        }
    }
}
