using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NikonBat
{
    public class FromDriveP
    {
        public static readonly string destPath = @"d:\NIKON\transfer\ftp\";
        public static readonly string srcPath = @"P:\SEQLOG\FTP\";
        public static void CopyFtpFromP()
        {
            using (FileStream fs = File.Open(Share.logPath, FileMode.Append, FileAccess.Write))
            {
                string sB, sD, sP, dB, dD, dP, str;
                byte[] bs;
                foreach (var tool in Share.nikonTool)
                {
                    sB = srcPath + tool + "_bak.ftp";
                    sD = srcPath + tool + "_delete.ftp";
                    sP = srcPath + tool + "_put.ftp";
                    dB = destPath + tool + "_bak.ftp";
                    dD = destPath + tool + "_delete.ftp";
                    dP = destPath + tool + "_put.ftp";


                    try
                    {
                        File.Copy(sB, dB, true);
                    }
                    catch (Exception ex)
                    {
                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                    }
                    try
                    {
                        File.Copy(sD, dD, true);
                    }
                    catch (Exception ex)
                    {

                        str = DateTime.Now.ToString() + " " + ex.Message + "\n\r";
                        bs = Encoding.UTF8.GetBytes(str);
                        fs.Write(bs, 0, bs.Length);
                    }
                    try
                    {
                        File.Copy(sP, dP, true);
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
    }
}
