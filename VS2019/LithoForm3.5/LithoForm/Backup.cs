using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Data.SQLite;
//using Microsoft.VisualBasic;
//using System.Diagnostics;
//using ICSharpCode.SharpZipLib.GZip;













namespace LithoForm
{
    class Backup
    {
        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;



        }

        public static void CopyToLocalDisk()
        {
            MessageBox.Show("选择远程源文件");
            string src, dest;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                src = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }


            MessageBox.Show("选择本机文件保存");
            saveFileDialog1.InitialDirectory = @"c:\temp";
            saveFileDialog1.Title = "选择目标文件";
            saveFileDialog1.Filter = "db文件t(*.db)|*.db";//
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dest = saveFileDialog1.FileName.Replace("\\", "\\\\");
                File.Copy(src, dest, true);
                MessageBox.Show("文件复制完毕");
            }
            else
            { MessageBox.Show("未选择目标文件，复制未成功"); return; }


        }

        public static void AutoCopyAll()
        {
            MessageBox.Show("自动将数据复制到本机 D:\\TEMP\\DB 目录");
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB", true);
            File.Copy(@"P:\_SQLite\AsmlBatchreport.DB", @"D:\TEMP\DB\AsmlBatchreport.DB", true);
            File.Copy(@"P:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);
            File.Copy(@"P:\_SQLite\AsmlAwe.DB", @"D:\TEMP\DB\AsmlAwe.DB", true);
            File.Copy(@"P:\_SQLite\AsmlJobinStation.DB", @"D:\TEMP\DB\AsmlJobinStation.DB", true);
            File.Copy(@"P:\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB", true);
            File.Copy(@"P:\_SQLite\Flow.DB", @"D:\TEMP\DB\Flow.DB", true);
            File.Copy(@"P:\_SQLite\NikonJobinStation.DB", @"D:\TEMP\DB\NikonJobinStation.DB", true);
            File.Copy(@"P:\_SQLite\R2R.DB", @"D:\TEMP\DB\R2R.DB", true);
            MessageBox.Show("DONE");
        }
        public static void BackupLithoformExposurerecipeform()
        {
            MessageBox.Show("Copy LithoForm,WindowsExposureRecipe");
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\LithoForm3.5\\LithoForm\\bin\\Debug P:\\_SQLite\\Shell\\exe\\LithoForm /Y /E /J ";
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            //exep.Close();

            //exep = new System.Diagnostics.Process();
            //exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\WindowsFormsExposureRecipe_NET3.5\\WindowsFormsExposureRecipe\\bin\\Debug P:\\_SQLite\\Shell\\exe\\ExposureRecipeForm /Y /E /J ";
            //exep.StartInfo.UseShellExecute = false;
            //exep.StartInfo.RedirectStandardInput = true;
            //exep.StartInfo.RedirectStandardOutput = true;
            //exep.StartInfo.RedirectStandardError = true;
            //exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            output += exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();


            exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\WindowsFormsExposureRecipe_NET3.5\\WindowsFormsExposureRecipe\\bin\\Debug  P:\\_SQLite\\EXE\\ExposureRecipe /Y /E /J ";
            exep.Start();
            output += exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();

            exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\LithoForm3.5\\LithoForm\\bin\\debug  P:\\_SQLite\\EXE\\LithoForm /Y /E /J ";
            exep.Start();
            output += exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();

            MessageBox.Show(output);
        }
        public static void BackupFolder()
        {
            string sourcePath, destPath;
            FolderBrowserDialog dialog1 = new FolderBrowserDialog();
            dialog1.Description = "===Source Folder===";
            dialog1.RootFolder = Environment.SpecialFolder.MyComputer;

            if (dialog1.ShowDialog() == DialogResult.OK)
            {
                sourcePath = dialog1.SelectedPath;
            }
            else
            { MessageBox.Show("未选择Source Path"); return; }



            dialog1.Description = "===Destination Folder===";
            dialog1.RootFolder = Environment.SpecialFolder.MyComputer;

            if (dialog1.ShowDialog() == DialogResult.OK)
            {
                destPath = dialog1.SelectedPath;
            }
            else
            { MessageBox.Show("未选择Source Path"); return; }

            if (MessageBox.Show("" +
                "Source Folder:      " + sourcePath + "\r\n\r\n\r\n" +
                "Destination Folder: " + destPath + "\r\n\r\n\r\n" +
                "点击‘是（Y）’,继续；\r\n\r\n\r\n" +
                "点击‘否（N）’,退出；", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                bool flag = LithoForm.Backup.CopyDirectory(sourcePath, destPath, true);
                MessageBox.Show("Duplication Done?: " + flag.ToString());
            }
            else
            { MessageBox.Show("Exit"); }
        }
    }
}

