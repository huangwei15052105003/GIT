using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace NikonBat
{
    class ToDriveP
    {
        public static readonly string sourceDir = @"d:\Nikon\transfer\";
        public static readonly string destDir = @"P:\EGALOG\ALII";
        public static readonly string destDir1 = @"P:\Nikondir\ALII";

        public static readonly string destDir2 = @"P:\SEQLOG\sysparam\";
        public static readonly string destDir3 = @"P:\SEQLOG\MacConst\";
        public static readonly string destDir4 = @"P:\SEQLOG\MacAdjust\";
        public static readonly string destDir5 = @"P:\SEQLOG\";



        public static void CopyParameter()
        {
            FileStream fs;
            string riqi = DateTime.Now.ToString("yyyyMMdd");
            using (fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                string[] files;
                string destFile;
                string str;
                byte[] bs;
                foreach (string no in Share.nikonNo)
                {
                    try
                    {
                        files = Directory.GetFiles(sourceDir + no, "*.*si*.1");
                    }
                    catch (Exception ex)
                    {
                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                        files = new string[] { };

                    }

                    foreach (string x in files)
                    {
                        try
                        {
                            if (x.Contains("sys_param"))
                            {
                                destFile = Path.Combine(destDir2, Path.GetFileName(x).Replace(".1","") + "#" + riqi);
                                File.Copy(x, destFile, true);
                                if (File.Exists(destFile))
                                {
                                    File.Delete(x);
                                }
                            }
                            else if (x.Contains("mac_const"))
                            {
                                destFile = Path.Combine(destDir3, Path.GetFileName(x).Replace(".1", "") + "#" + riqi);
                                File.Copy(x, destFile, true);
                                if (File.Exists(destFile))
                                {
                                    File.Delete(x);
                                }
                            }
                            else if (x.Contains("mac_adjust"))
                            {
                                destFile = Path.Combine(destDir4, Path.GetFileName(x).Replace(".1", "") + "#" + riqi);
                                File.Copy(x, destFile, true);
                                if (File.Exists(destFile))
                                {
                                    File.Delete(x);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                            bs = Encoding.UTF8.GetBytes(str);
                            fs.Write(bs, 0, bs.Length);
                        }


                    }
                }
                fs.Close();
                fs.Dispose();
            }
        }
        public static void CopyRecipeList()
        {
            FileStream fs;
            using (fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                string sourceFile;
                string destFile;
                string str;
                byte[] bs;
                foreach (string no in Share.nikonNo)
                {
                    sourceFile = sourceDir + no + "\\alii" + no + ".txt.1";
                    destFile = destDir1 + no + ".txt";

                    try
                    {
                        //File.Move(x, destFile);
                        File.Copy(sourceFile, destFile, true);
                        File.Delete(sourceFile);
                    }
                    catch (Exception ex)
                    {
                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                    }



                }
                fs.Close();
                fs.Dispose();
            }
        }
        public static void CopyEgaLog()
        {
            FileStream fs;
            string[] files;
            string str;
            byte[] bs;

            string destFile;

            using (fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                foreach (string no in Share.nikonNo)
                {
                    try
                    {
                        files = Directory.GetFiles(sourceDir + no, "*.egam.*");
                    }
                    catch (Exception ex)
                    {
                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                        files = new string[] { };

                    }

                    foreach (string x in files)
                    {
                        try
                        {
                            destFile = Path.Combine(destDir + no + "\\", Path.GetFileName(x).Replace(".1", ""));
                            //File.Move(x, destFile);
                            File.Copy(x, destFile, true);
                            if (File.Exists(destFile))
                            {
                                File.Delete(x);
                            }
                        }
                        catch (Exception ex)
                        {
                            str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                            bs = Encoding.UTF8.GetBytes(str);
                            fs.Write(bs, 0, bs.Length);
                        }


                    }
                }
                fs.Close();
                fs.Dispose();
            }
        }
        public static void CopySeqLog()
        {
            string sourceName;
            string destName;
            string str;
            byte[] bs;
            FileStream fs;
            string[] files;
            string destFile;


            //copy merge file
            using (fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                foreach (string no in Share.nikonNo)
                {

                    //删除无用文件
                    try
                    { File.Delete(sourceDir + no + "\\alii" + no + ".seq.1"); }
                    catch
                    { }
                    //
                    string dateStr;
                    sourceName = sourceDir + no + "\\sequencelog.log.1";

                    dateStr = GetDateName(sourceName);
                    Console.WriteLine(dateStr);

                    if (dateStr != "_")
                    {
                        if (no == "20" || no == "21" || no == "22" || no == "23")
                        {
                            destName = destDir5 + "BLII" + no + "\\" + dateStr;
                        }
                        else
                        {
                            destName = destDir5 + "ALII" + no + "\\" + dateStr;
                        }

                        try
                        {
                            Console.WriteLine(sourceName + ",  " + destName);
                            File.Copy(sourceName, destName, true);
                            if (File.Exists(destName))
                            {
                                File.Delete(sourceName);
                            }
                        }
                        catch (Exception ex)
                        {
                            str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                            bs = Encoding.UTF8.GetBytes(str);
                            fs.Write(bs, 0, bs.Length);
                        }
                    }

                }
                fs.Close(); fs.Dispose();
            }

            //copy other files
            using (fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                foreach (string no in Share.nikonNo)
                {


                    try
                    {
                        files = Directory.GetFiles(sourceDir + no);//批量下载的seqlog文件名不规范
                    }
                    catch (Exception ex)
                    {
                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                        files = new string[] { };

                    }


                    foreach (string x in files)
                    {
                        Console.WriteLine(no + ",  " + x);
                        string dateStr;
                        try
                        {
                            dateStr = GetDateName(x);


                            if (no == "20" || no == "21" || no == "22" || no == "23")
                            {
                               
                                destFile = Path.Combine(destDir5 + "BLII"+no + "\\", dateStr);
                            }
                            else
                            {
                                
                                destFile = Path.Combine(destDir5 +"ALII"+ no + "\\", dateStr);
                            }





                            
                            Console.WriteLine(x + ", " + destFile);
                            File.Copy(x, destFile, true);
                            if (File.Exists(destFile))
                            {
                                File.Delete(x);
                            }
                        }
                        catch (Exception ex)
                        {
                            str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                            bs = Encoding.UTF8.GetBytes(str);
                            fs.Write(bs, 0, bs.Length);
                        }


                    }


                }

                fs.Close();
                fs.Dispose();

            }
        }
        public static string GetDateName(string file)
        {
            string newName = "_";
            string[] allLines;
            try
            {
                allLines = File.ReadAllLines(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return newName;
            }

            for (int n = 0; n < allLines.Length - 1; ++n)
            {
                string tmp = allLines[n].Trim();
                if (tmp.Contains("Logging start at"))
                {
                    newName += tmp.Substring(17, 19).Replace(" ", "_").Replace(":", "");
                    break;
                }
            }

            for (int n = allLines.Length - 1; n > 0; --n)
            {
                string tmp = allLines[n].Trim();
                if (tmp.Contains("Logging end at "))
                {
                    newName += "#" + tmp.Substring(15, 19).Replace(" ", "_").Replace(":", "");
                    break;
                }
            }




            return newName;
        }
    }
}