using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace LithoForm
{
    class classHelp
    {
        public static DataTable changeDescription()
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("日期");
            dt.Columns.Add("主要变更记录");
            for (int i = 1; i < 10; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["日期"] = "";
                dt.Rows.Add(newRow);
            }

            int k = 0;
            dt.Rows[k][0] = "2020-03-02";
            dt.Rows[k][1] = "背面沾污且MaZ逐渐降低->返工工艺代码定义为TB->返工分类归为 Track 设备";
            k += 1;

            return dt;
        }
        public static DataTable savePathDescription()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            for (int i = 0; i < 10; ++i) { DataRow newRow = dt.NewRow(); newRow[0] = ""; dt.Rows.Add(newRow); }
            int k = 0;
            dt.Rows[k][0] = @"务必将\\10.4.50.16\photo$\ppcs映射为P盘"; ++k;
            dt.Rows[k][0] = @"Nikon预对位数据路径：         \\10.4.50.16\PHOTO$\PPCS\_SQLite\NikonEgaPara.db"; ++k;
            dt.Rows[k][0] = @"AsmlBatchReport数据路径：    \\10.4.50.16\PHOTO$\PPCS\_SQLite\AsmlBatchreport.db"; ++k;
            dt.Rows[k][0] = @"R2R OVL_CD数据路径：        \\10.4.50.16\PHOTO$\PPCS\_SQLite\\R2R.db"; ++k;
            dt.Rows[k][0] = @"Asml Awe数据路径：          \\10.4.50.16\PHOTO$\PPCS\_SQLite\AsmlAwe.db"; ++k;
            dt.Rows[k][0] = @"Asml程序日期检查：          \\10.4.50.16\PHOTO$\PPCS\_SQLite\AsmlRecipeCheck.mdb"; ++k;
            dt.Rows[k][0] = @"返工数据：                   \\10.4.50.16\PHOTO$\PPCS\_SQLite\ReworkMove.db"; ++k;
            dt.Rows[k][0] = @"R2R Charting Data：         \\10.4.50.16\PHOTO$\PPCS\_SQLite\ChartRawData.db"; ++k;


            return dt;
        }
        public static bool CopyToLocalDisk()
        {

            MessageBox.Show("选择远程源文件");
            string src, dest;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            openFileDialog1.InitialDirectory = @"\\10.4.50.16\photo$\PPCS\_SQLite\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                src = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return false; }


            MessageBox.Show("选择本机文件保存");
            saveFileDialog1.InitialDirectory = @"c:\temp";
            saveFileDialog1.Title = "选择目标文件";
            saveFileDialog1.Filter = "db文件t(*.db)|*.db";//
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dest = saveFileDialog1.FileName.Replace("\\", "\\\\");
                File.Copy(src, dest, true);
                MessageBox.Show("文件复制完毕");
                return true;
            }
            else
            { MessageBox.Show("未选择目标文件，复制未成功"); return false; }
        }
        public static bool AutoCopyToLocalDisk()
        {
            MessageBox.Show("自动将数据复制到本机 D:\\TEMP\\DB 目录\n\n\n数据较大，耗时较长");
            try
            {
                if (!Directory.Exists("D:\\TEMP\\DB"))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                    directoryInfo.Create();
                }

                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\AsmlBatchreport.DB", @"D:\TEMP\DB\AsmlBatchreport.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\AsmlAwe.DB", @"D:\TEMP\DB\AsmlAwe.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\AsmlJobinStation.DB", @"D:\TEMP\DB\AsmlJobinStation.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\Flow.DB", @"D:\TEMP\DB\Flow.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\NikonJobinStation.DB", @"D:\TEMP\DB\NikonJobinStation.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\R2R.DB", @"D:\TEMP\DB\R2R.DB", true);
                File.Copy(@"\\10.4.50.16\photo$\ppcs\_SQLite\ChartRawData", @"D:\TEMP\DB\ChartRawData", true);

                return true;

            }
            catch
            { return false; }
        }

        public static DataTable  GetTableFieldName()
        {
            DataTable dt = new DataTable();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"d:\temp\db";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "db文件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string src = openFileDialog1.FileName.Replace("\\", "\\\\");
                string connStr = "data source=" + src;
                dt = LithoForm.LibF.GetTabFieldName(connStr);
                return dt;
            }
            else
            { MessageBox.Show("未选择数据文件"); return dt; }
        }
        public static void DuplicateExeFiles()
        {
            try
            {
                MessageBox.Show("Copy LithoForm,WindowsExposureRecipe,etc");
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

                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\LithoForm4.0\\LithoForm\\bin\\debug  P:\\_SQLite\\EXE\\LithoForm4.0 /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();

                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\LithoForm4.0\\LithoForm\\bin\\debug  P:\\_SQLite\\Shell\\EXE\\LithoForm4.0 /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();


                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\TaskExe4.0\\TaskExe\\bin\\debug  P:\\_SQLite\\EXE\\TaskExe /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();

                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\TaskExe4.0\\TaskExe\\bin\\debug  P:\\_SQLite\\Shell\\EXE\\TaskExe /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();


                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\exposureRecipeNet3\\exposureRecipeNet3\\bin\\debug  P:\\_SQLite\\Shell\\EXE\\RevisedExposureRecipeForm /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();

                exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019\\exposureRecipeNet3\\exposureRecipeNet3\\bin\\debug  P:\\_SQLite\\EXE\\RevisedExposureRecipeForm /Y /E /J ";
                exep.Start();
                output += exep.StandardOutput.ReadToEnd();
                exep.WaitForExit();
                exep.Close();




                MessageBox.Show(output);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                bool flag = CopyDirectory(sourcePath, destPath, true);
                MessageBox.Show("Duplication Done?: " + flag.ToString());
            }
            else
            { MessageBox.Show("Exit"); }
        }
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

        public static void Backup_10_4_72_150()
        {
            //run at 10.4.72.150
            string ip; bool flag;
            ip = LithoForm.LibF.GetIpAddress();
            if (ip != "10.4.72.150") { MessageBox.Show("IP IS NOT 10.4.72.150,Exit"); return; }
            //backup P:\_SQLITE folder

            string sourceFolder = "P:\\_SQLite\\";
            string destinationFolder = "E:\\backup\\P_SQLite\\";
            flag =CopyDirectory(sourceFolder, destinationFolder, true);

            //backup P script
            sourceFolder = "P:\\_Script\\";
            destinationFolder = "E:\\Backup\\P_SCRIPT\\";
            flag = CopyDirectory(sourceFolder, destinationFolder, true);




        }
    }
}
