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

/// <summary>
/// https://bbs.csdn.net/topics/380053920  datagridview  Ctr+C 乱码
/// </summary>

namespace LithoForm
{


    public partial class Form1 : Form
    {
        // display form to process datagridview1


        public delegate void Form2Delegate(string[] passData);
        public event Form2Delegate Form2Trigger;

        public delegate void Form3Delegate(DataTable dt);
        public event Form3Delegate Form3Trigger;









        DataTable dtShow; //通用DataGridView DataSourece
        DataTable dt1; //filter
        DataTable dt2; //R2R jobin
        public static bool form3Flag = false;
        List<DataTable> listDatatable = new List<DataTable>(); //矢量图，WQ，MCC等
        string connStr;
        string sql;

        float zoom = 1f; //矢量放大
        float zoom1 = 10f;//wafer quality 画圆


        #region  //clear memory
        ///https://www.cnblogs.com/mq0036/p/3707257.html
        ///Clear Memory
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public void ClearMemory()
        {
            GC.Collect();
            GC.SuppressFinalize(this);


            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion


        public Form1()
        {
            InitializeComponent();
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.tabControl1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create c:/temp directory, otherwise GDI+ error will be triggered
            if (!Directory.Exists("C:\\TEMP"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("C:\\TEMP");
                directoryInfo.Create();
            }

            LithoForm.LibF.DosCommand(@"net use P:\_SQLite p:");


        }
        #region // MENU:help
        private void 数据保存位置说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nikon预对位数据路径：\\\\10.4.50.16\\PHOTO$\\PPCS\\_SQLite\\NikonEgaPara.db\r\n\r\n" +
                            "AsmlBatchReport数据路径：\\\\10.4.50.16\\PHOTO$\\PPCS\\_SQLite\\AsmlBatchreport.db\r\n\r\n" +
                            "R2R OVL_CD数据路径：\\\\10.4.50.16\\PHOTO$\\PPCS\\_SQLite\\R2R.db\r\n\r\n" +
                            "Asml Awe数据路径：\\\\10.4.50.16\\PHOTO$\\PPCS\\_SQLite\\AsmlAwe.db\r\n\r\n" +
                            "务必将\\\\10.4.50.16\\photo$\\ppcs映射为P盘");
        }
        private void 复制数据至本地硬盘ToolStripMenuItem_Click(object sender, EventArgs e)
        { LithoForm.Backup.CopyToLocalDisk(); }
        private void 自动复制数据ToolStripMenuItem_Click(object sender, EventArgs e)
        { LithoForm.Backup.AutoCopyAll(); }
        private void 获取表名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"d:\temp\db";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "db文?件t(*.tb)|*.db";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string src = openFileDialog1.FileName.Replace("\\", "\\\\");
                connStr = "data source=" + src;
                dtShow = LithoForm.LibF.GetTabFieldName(connStr);
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;

            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
        }
        private void 备份VS2019目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///https://blog.csdn.net/qq_21747999/article/details/79151910
            ///https://www.cnblogs.com/zhaoshujie/p/10612654.html

            MessageBox.Show("共享目录禁用，此命令无效"); return;


            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            // exep.StartInfo.Arguments = "/C XCOPY D:\\GIT\\VS2019 Y:\\Backup\\VS2019 /Y /E /J ";
            exep.StartInfo.Arguments = "/C XCOPY U:\\VC\\VS2019 Y:\\Backup\\VS2019 /Y /E /J ";



            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
            MessageBox.Show(" DOS COMMAND DONE !! \r\n\r\n" + output);

            //exep.StandardInput.AutoFlush = true;
            //https://bbs.csdn.net/topics/380080340
            //exep.StandardInput.WriteLine("dir ");
            //exep.StandardInput.WriteLine("XCOPY U:\\VC\\VS2019 Y:\\VS2019 /Y /E /J ");



        }
        private void 备份SQLite目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("共享目录禁用，此命令无效"); return;
            MessageBox.Show("AWE文件>5G,复制较慢，请耐心等待");
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/C XCOPY P:\\_SQLite Y:\\Backup\\_SQLite /Y /E /J ";
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
            MessageBox.Show(" DOS COMMAND DONE !! \r\n\r\n" + output);
        }
        private void 备份P盘Script目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("共享目录禁用，此命令无效"); return;
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/C XCOPY P:\\_Script Y:\\Backup\\P_Script /Y /E /J ";
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
            MessageBox.Show(" DOS COMMAND DONE !! \r\n\r\n" + output);

        }
        private void 备份调用vsPythonToolStripMenuItem_Click(object sender, EventArgs e)

        {
            MessageBox.Show("共享目录禁用，此命令无效"); return;
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "cmd.exe";
            exep.StartInfo.Arguments = "/C COPY \\\\10.4.72.150\\share\\VC\\VS2019\\LithoForm3.5\\VsPython.py P:\\_SQLite\\Shell\\python\\VsPython.py /Y  ";
            exep.StartInfo.UseShellExecute = false;
            exep.StartInfo.RedirectStandardInput = true;
            exep.StartInfo.RedirectStandardOutput = true;
            exep.StartInfo.RedirectStandardError = true;
            exep.StartInfo.CreateNoWindow = true;
            exep.Start();
            string output = exep.StandardOutput.ReadToEnd();
            exep.WaitForExit();
            exep.Close();
            MessageBox.Show(" DOS COMMAND DONE !! \r\n\r\n" + output);


        }
        private void 复制ReworkMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\ReworkMove.DB", @"D:\TEMP\DB\ReworkMove.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmAweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"p:\_SQLite\AsmlAwe.DB", @"D:\TEMP\DB\AsmlAwe.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmlBatchReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\AsmlBatchreport.DB", @"D:\TEMP\DB\AsmlBatchreport.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制AsmlJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\AsmlJobinStation.DB", @"D:\TEMP\DB\AsmlJobinStation.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制cDRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\cdRecipe.DB", @"D:\TEMP\DB\cdRecipe.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制FlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\Flow.DB", @"D:\TEMP\DB\Flow.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制NikonEgaParaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\NikonEgaPara.DB", @"D:\TEMP\DB\NikonEgaPara.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制NikonJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\NikonJobinStation.DB", @"D:\TEMP\DB\NikonJobinStation.DB", true);

            MessageBox.Show("复制完成");
        }

        private void 复制R2RCDOVLCDCONFIGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("D:\\TEMP\\DB"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP\\DB");
                directoryInfo.Create();
            }
            File.Copy(@"P:\_SQLite\R2R.DB", @"D:\TEMP\DB\R2R.DB", true);

            MessageBox.Show("复制完成");
        }
        private void 复制LithoFormExposureRecipeFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LithoForm.Backup.BackupLithoformExposurerecipeform();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e) //主要变更说明
        {
            dtShow = new DataTable();
            dtShow.Columns.Add("日期"); dtShow.Columns.Add("主要变更记录");
            for (int i = 1; i < 10; i++)
            {
                DataRow newRow = dtShow.NewRow();
                newRow["日期"] = "";
                dtShow.Rows.Add(newRow);
            }

            int k = 0;
            dtShow.Rows[k][0] = "2020-03-02";
            dtShow.Rows[k][1] = "背面沾污且MaZ逐渐降低->返工工艺代码定义为TB->返工分类归为 Track 设备"; k += 1;
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;

        }

        private void 选择目录备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LithoForm.Backup.BackupFolder();
        }
        private void 备份PSQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //run at 10.4.72.74
            string ip; bool flag;
            ip = LithoForm.LibF.GetIpAddress();
            if (ip != "10.4.72.74") { MessageBox.Show("IP IS NOT 10.4.72.74,Exit");return; }
            //backup P:\_SQLITE folder
            
            string sourceFolder = "P:\\_SQLite\\"; 
            string destinationFolder = "F:\\share\\_SQLite\\";
            flag=LithoForm.Backup.CopyDirectory(sourceFolder, destinationFolder, true);
            
            sourceFolder = "P:\\_SQLite\\";
            destinationFolder = "D:\\Litho\\Backup\\_SQLite";
            flag = LithoForm.Backup.CopyDirectory(sourceFolder, destinationFolder, true);

            //backup P script
            sourceFolder = "P:\\_Script\\";
            destinationFolder = "D:\\Litho\\Backup\\P_Script\\";
            flag = LithoForm.Backup.CopyDirectory(sourceFolder, destinationFolder, true);


        }
        private void 备份PSQLITEPScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //run at 10.4.72.150
            string ip; bool flag;
            ip = LithoForm.LibF.GetIpAddress();
            if (ip != "10.4.72.150") { MessageBox.Show("IP IS NOT 10.4.72.150,Exit"); return; }
            //backup P:\_SQLITE folder

            string sourceFolder = "P:\\_SQLite\\";
            string destinationFolder = "E:\\backup\\P_SQLite\\";
            flag = LithoForm.Backup.CopyDirectory(sourceFolder, destinationFolder, true);       

            //backup P script
            sourceFolder = "P:\\_Script\\";
            destinationFolder = "E:\\Backup\\P_SCRIPT\\";
            flag = LithoForm.Backup.CopyDirectory(sourceFolder, destinationFolder, true);


        }
        #endregion

        #region //NIKON
        private void 更新NikonEgaLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此命令取消，改为Python脚本直接更新");
            return;
            LithoForm.Nikon.UpdateEgaLogDb();
            MessageBox.Show("Nikon Ega Log Updated");
        }

        private void 查看预对位ToolStripMenuItem_Click(object sender, EventArgs e)

        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate, eDate;
            sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.PreAlignment(sDate, eDate, connStr);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 查看对位方式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }

            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }



            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.AlignMethod(sDate, eDate, connStr, ppid);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 查看对位数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入层次名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }

            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.AlignData(sDate, eDate, connStr, ppid);

            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据\r\n\r\n数据较多时显示较慢");
            dataGridView1.DataSource = dtShow; this.tabControl1.SelectedIndex = 0;
        }
        private void 步骤1列出索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }
            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", "")) * 10000;
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", "")) * 10000;
            string str1 = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);
            if (str1.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }

            string str2 = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (str2.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }

            string ppid = "%" + str1.Trim() + "%.%" + str2.Trim() + "%";

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\NikonEgaPara.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\NikonEgaPara.DB"; }



            dtShow = LithoForm.Nikon.VectorIndex(sDate, eDate, connStr, ppid);

            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Ppid", 130, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 100, HorizontalAlignment.Left);
               
               
                listView1.Columns.Add("Datetime", 100, HorizontalAlignment.Left);
                for (int i = 0; i < dtShow.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());
                 
                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }

                                                                            


        }

        
        private  void NikonVectorPlot()
        {

            long str = 0; string str1 = string.Empty;

            if (listView1.SelectedItems.Count == 0)
            { MessageBox.Show("请选择Lot显示，退出"); return; }


            else //multi禁用，==1
            {
                str = Convert.ToInt64(listView1.SelectedItems[0].SubItems[2].Text);
                str1 = listView1.SelectedItems[0].SubItems[0].Text;
            }

            dtShow = LithoForm.Nikon.VectorData(connStr, str1, str);
            dataGridView1.DataSource = dtShow;

            //抽出wferid至列表框中，供后续选中作图；https://blog.csdn.net/qq_28249373/article/details/74905884
            wfrIdBox.Items.Clear();
            int count = 0;
            List<string> list = new List<string>();
            for (int k = 0; k < dataGridView1.Rows.Count - 1; k++) //datagridview1 有一空行，循环减1--》旧程序复制过来的，直接用dtShow即可
            { list.Add(dataGridView1.Rows[k].Cells[1].Value.ToString()); }
            HashSet<string> hs = new HashSet<string>(list);
            list = new List<string>(hs);
            foreach (var x in list)
            {
                this.wfrIdBox.Items.Add(x);
                wfrIdBox.SetItemChecked(count, true);
                count += 1;
            }


            LithoForm.Vector.PlotNikonVector(list, dtShow, 1f);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;





        }
        private void 重新作图_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < wfrIdBox.Items.Count; i++)
            {
                if (wfrIdBox.GetItemChecked(i))
                {
                    // list.Add(checkedListBox1.GetItemText(wfrIdBox.Items[i].ToString()));
                    list.Add(wfrIdBox.GetItemText(wfrIdBox.Items[i].ToString()));
                }
            }

            LithoForm.Vector.PlotNikonVector(list, dtShow, zoom);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;


        }


        #endregion

        #region //ASML BatchReport
        private void 初始化列参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tmp;
            tmp = LithoForm.Asml.ListBatchReportParameter();
            listView1.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;//display full row
            listView1.MultiSelect = true;
            listView1.View = View.Details;
            listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

            listView1.Columns.Add("ParameterName",200, HorizontalAlignment.Left);
       
            foreach (string item in tmp)
            {
                ListViewItem li = new ListViewItem(item);//此处括号内的变量是第一个字段参数
                listView1.Items.Add(li);
            }


            // this.checkedListBox1.Items.Clear();
            //  foreach (var item in tmp)
            //  { this.checkedListBox1.Items.Add(item); }
        }
        private void 多参数查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
                "结束日期\r\n\r\n    统计参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择统计参数\r\n\r\n" +
                "    选择 是（Y） 继续\r\n\r\n" +
                "    选择 否（N） 退出\r\n\r\n\r\n" +
                "注：查询前先选择日期，单击 ‘列参数’命令选择参数", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }


            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;
            string para = string.Empty;

            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }


            if (listView1.SelectedItems.Count>0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                { para += item.SubItems[0].Text+","; }
                para = para.Substring(0, para.Length - 1);
            }
            else
            {
                MessageBox.Show("未选择参数，退出");
                return;
            }




          //  try
          //  {
          //      for (int i = 0; i < checkedListBox1.Items.Count; i++)
          //      {
          //          if (checkedListBox1.GetItemChecked(i))
           //         { para += checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ","; }

          //      }
          //      para = para.Substring(0, para.Length - 1);
          //  }
           // catch
          //  {
           //     MessageBox.Show("未选择参数，退出");
            //    return;
           // }

            dtShow = LithoForm.Asml.QueryPara(connStr, sDate, eDate, para, part, layer);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 单参数计数统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
                "结束日期\r\n\r\n    统计参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择统计参数\r\n\r\n" +
                "    选择 是（Y） 继续\r\n\r\n" +
                "    选择 否（N） 退出\r\n\r\n\r\n" +
                "注：查询前先选择日期，单击 ‘列参数’命令选择参数", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }
            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;
            string para = string.Empty;

            if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                { para += item.SubItems[0].Text + ","; }
                para = para.Substring(0, para.Length - 1);
            }
            else
            {
                MessageBox.Show("只能选择一个参数，退出");
                return;
            }

            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();



            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }



    












         //   try
         //   {
          //      for (int i = 0; i < checkedListBox1.Items.Count; i++)
           //     {
            //        if (checkedListBox1.GetItemChecked(i))
            //        { para += checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ","; break; }

//                }
  //              para = para.Substring(0, para.Length - 1);
    //        }
      //      catch
        //    { MessageBox.Show("未选择参数，退出"); return; }

            dtShow = LithoForm.Asml.SummaryParaByName(connStr, sDate, eDate, para, part, layer);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 单参数计量统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此命令已取消\r\n\r\n\r\n\r\n" +
                "请使用‘多参数查询’命令及表格的‘筛选排序统计’命令");
            return;
            if (MessageBox.Show("使用该查询，需要知道参数此类型\r\n\r\n" +
                "若选择字符型参数会报错\r\n\r\n若不清楚参数类型，使用‘多参数查询’命令及表格的‘筛选排序统计’命令\r\n\r\n\r\n" +
                "    选择 是（Y） 继续\r\n\r\n" +
                "    选择 否（N） 退出\r\n\r\n\r\n" +
                "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }




            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
                "结束日期\r\n\r\n    统计参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择统计参数\r\n\r\n" +
                "    选择 是（Y） 继续\r\n\r\n" +
                "    选择 否（N） 退出\r\n\r\n\r\n" +
                "注：查询前先选择日期，单击 ‘列参数’命令选择参数", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }
            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;
            string para = string.Empty;

            if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                { para += item.SubItems[0].Text + ","; }
                para = para.Substring(0, para.Length - 1);
            }
            else
            {
                MessageBox.Show("只能选择一个参数，退出");
                return;
            }







            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();



            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }








          //  try
           // {
            //    for (int i = 0; i < checkedListBox1.Items.Count; i++)
           //     {
           //         if (checkedListBox1.GetItemChecked(i))
           //         { para += checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ","; break; }

//                }
  //              para = para.Substring(0, para.Length - 1);
    //        }
      //      catch
        //    { MessageBox.Show("未选择参数，退出"); return; }

            dtShow = LithoForm.Asml.SummaryParaByValue(connStr, sDate, eDate, para, part, layer);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 单参数唯一项统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    统计参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择统计参数\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出\r\n\r\n\r\n" +
             "注：查询前先选择日期，单击 ‘列参数’命令选择参数", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }
            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;
            string para = string.Empty;

            if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                { para += item.SubItems[0].Text + ","; }
                para = para.Substring(0, para.Length - 1);
            }
            else
            {
                MessageBox.Show("只能选择一个参数，退出");
                return;
            }

            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();



            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }

   


            dtShow = LithoForm.Asml.DistinctParaByValue(connStr, sDate, eDate, para, part, layer);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 更新BatchReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此命令取消，改为Python脚本直接更新");
            return;
            LithoForm.Asml.UpdateBatchReport();

        }
        #endregion

        #region //rework
        private void 查询原始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询参数：\r\n\r\n    起始日期\r\n\r\n    结束日期\r\n\r\n\n\r\n若日期不合适，点击 ‘否（N）’\r\n\r\n\r\n\r\n继续，点击‘是（Y）’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }


            string sDate = dateTimePicker1.Text;
            string eDate = dateTimePicker2.Text;



            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }





            dtShow = LithoForm.Rework.RawData(connStr, sDate, eDate);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 查询统计数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            dtShow = LithoForm.Rework.SummaryData(connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 返工数据更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connStr = @"data source=P:\_SQLite\ReworkMove.DB";
            LithoForm.Rework.UpdateDb(connStr);
        }
        #endregion

        #region //FLOW
        private void toolStripMenuItem4_Click(object sender, EventArgs e) // 查看所有工艺限制
        {
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            dtShow = LithoForm.Flow.EsfRawData(connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)//查看所有涂胶程序
        {
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            dtShow = LithoForm.Flow.TrackRecipe(connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show(dataGridView1.Rows.Count.ToString());

        }
        private void 流程查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string part = Interaction.InputBox("请输入产品名;", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            dtShow = LithoForm.Flow.FlowQuery(part);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("DONE");
        }
        private void 更新工艺代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            connStr = @"data source=P:\_SQLite\ReworkMove.DB";

            LithoForm.Flow.UpdateTechCode(connStr);
        }
        private void 导入MaskInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connStr = @"data source=P:\_SQLite\ReworkMove.DB";

            LithoForm.Flow.UpdateMask(connStr);
        }

        private void 涂胶程序和工艺限制更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connStr = @"data source=P:\_SQLite\ReworkMove.DB";
            LithoForm.Flow.UpdateEsfTrack(connStr);
            MessageBox.Show(" Update Done");
        }
        private void 更新PartVsTechReworkMoveDBToolStripMenuItem_Click(object sender, EventArgs e)
        {

            connStr = "data source=P:\\_SQLite\\ReworkMove.db";
            string dbTblName = "tbl_tech";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string tmpSql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(tmpSql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }

            sql = "select productcode2 part,processcode tech,processtype,processgroup,dienum,chipsizex,chipsizey,partavailible,last_update_time from  rptprd.part_checklist";
            dtShow = LithoForm.LibF.CsmcOracle(sql);
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, "P:\\_SQLite\\ReworkMove.db");
            //dataGridView1.DataSource = dtShow;
            // this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("Update Done");

        }

        #endregion

        #region //ASML AWE
        private void 更新AsmlAweIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此命令取消，改为Python脚本直接更新");
            return;
            LithoForm.Asml.UpdateAweIndex();
        }
        private void 查询索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查看对位标记类型和对位程序\r\n\r\n\r\n\r\n查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出\r\n\r\n\r\n" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }

            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", ""));
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", ""));
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            string connStr1;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; connStr1 = @"data source=P:\_SQLite\RemorkMove.DB"; }



            dtShow = LithoForm.Asml.AweIndexQuery(sDate, eDate, connStr, part, layer, connStr1);
            dataGridView1.DataSource = dtShow;
            MessageBox.Show("数据：" + dtShow.Rows.Count.ToString() + "条");
            this.tabControl1.SelectedIndex = 0;
        }
        private void 更新AsmlAweVecotrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此命令取消，改为Python脚本直接更新");
            return;
            string[] tools = { "ALSD82", "ALSD83", "ALSD85", "ALSD86", "ALSD87", "ALSD89", "ALSD8A", "ALSD8B", "ALSD8C", "BLSD7D", "BLSD08" };
            //string[] tools = { "ALSD85" };

            foreach (string tool in tools)
            {
                LithoForm.Asml.UpdateAweVector(tool);
                ClearMemory();
            }
            MessageBox.Show("Vector Data Update Done");
        }
        private void 步骤一日期PPID索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查看对位标记类型和对位程序\r\n\r\n\r\n\r\n查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
           "结束日期\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适\r\n\r\n" +
           "    选择 是（Y） 继续\r\n\r\n" +
           "    选择 否（N） 退出\r\n\r\n\r\n" +
           "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期"); return; }


            long sDate = Convert.ToInt64(dateTimePicker1.Text.Replace("-", ""));
            long eDate = Convert.ToInt64(dateTimePicker2.Text.Replace("-", ""));
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }



            dtShow = LithoForm.Asml.VectorIndex1(sDate, eDate, connStr, part, layer);
            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Part", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LotID", 85, HorizontalAlignment.Left);
                listView1.Columns.Add("Date", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 50, HorizontalAlignment.Left);
                for (int i=0;i<dtShow.Rows.Count;i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());
                    li.SubItems.Add(dtShow.Rows[i][5].ToString());
                    li.SubItems.Add(dtShow.Rows[i][4].ToString());
                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }




        }
        private void 或步骤一LotID索引ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            string lot = Interaction.InputBox("该查询要求输入LotID名，通配符查询;\r\n\r\n\r\n现在请输入LotID;", "定义LotID", "", -1, -1);

            if (lot.Trim().Length == 0) { MessageBox.Show("LotID输入不正确，退出"); return; }
            lot = lot.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }



            dtShow = LithoForm.Asml.VectorIndex2(connStr, lot);
            if (dtShow.Rows.Count > 0)
            {
                //  this.checkedListBox1.Items.Clear();
                //  for (int i = 0; i < dtShow.Rows.Count; i++) { this.checkedListBox1.Items.Add(dtShow.Rows[i][0].ToString() + ", " + dtShow.Rows[i][1].ToString() + ", " + dtShow.Rows[i][2].ToString() + ", " + dtShow.Rows[i][3].ToString() + ", " + dtShow.Rows[i][4].ToString() + ", " + dtShow.Rows[i][5].ToString()); }

                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择
                listView1.Columns.Add("Tool", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Part", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Layer", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LotID", 85, HorizontalAlignment.Left);
                listView1.Columns.Add("Date", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Index", 50, HorizontalAlignment.Left);
                for (int i = 0; i < dtShow.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dtShow.Rows[i][0].ToString());//
                    li.SubItems.Add(dtShow.Rows[i][1].ToString());
                    li.SubItems.Add(dtShow.Rows[i][2].ToString());
                    li.SubItems.Add(dtShow.Rows[i][3].ToString());
                    li.SubItems.Add(dtShow.Rows[i][5].ToString());
                    li.SubItems.Add(dtShow.Rows[i][4].ToString());
                    listView1.Items.Add(li);

                }





            }
            else { MessageBox.Show("未查询到数据"); }

        }

      
        public  void AsmlVectorPlot()
        {


            long str = 0; string str1 = string.Empty;

            if (listView1.SelectedItems.Count == 0)
            { MessageBox.Show("请选择Lot显示，退出"); return; }
          

            else //multi禁用，==1
           {
                str = Convert.ToInt64(listView1.SelectedItems[0].SubItems[5].Text);
                str1 = listView1.SelectedItems[0].SubItems[0].Text;
            }
      
            listDatatable = LithoForm.Asml.VectorData(connStr, str1, str);

            dataGridView1.DataSource = listDatatable[2];



            //抽出wferid至列表框中，供后续选中作图；https://blog.csdn.net/qq_28249373/article/details/74905884
            wfrIdBox.Items.Clear();
            int count = 0;
            List<string> list = new List<string>();
            for (int k = 0; k < dataGridView1.Rows.Count - 1; k++) //datagridview1 有一空行，循环减1--》旧程序复制过来的，直接用dtShow即可
            { list.Add(dataGridView1.Rows[k].Cells[1].Value.ToString()); }
            HashSet<string> hs = new HashSet<string>(list);
            list = new List<string>(hs);
            foreach (var x in list)
            {
                this.wfrIdBox.Items.Add(x);
                wfrIdBox.SetItemChecked(count, true);
                count += 1;
            }



            List<string> typeList = new List<string>();
            for (int i = 0; i < vectorTypeList.Items.Count; i++)
            {

                typeList.Add(vectorTypeList.GetItemText(vectorTypeList.Items[i].ToString()));

            }
            List<string> wqmccList = new List<string>();
            wqmccList.Add("X_RedWQ5");

            LithoForm.Vector.PlotAsmlVectorNew(list, typeList, wqmccList, listDatatable, zoom, zoom1);
            pictureBox1.Image = null;
            try
            {
                //若直接引用，同名文件修改后无法正常显示；
                FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("文件显示失败");
            }
            this.tabControl1.SelectedIndex = 1;


        }

        private void 重新ASML作图_Click(object sender, EventArgs e)
        {
            {
                List<string> wfrList = new List<string>();
                for (int i = 0; i < wfrIdBox.Items.Count; i++)
                {
                    if (wfrIdBox.GetItemChecked(i))
                    {
                        //wfrList.Add(checkedListBox1.GetItemText(wfrIdBox.Items[i].ToString()));
                        wfrList.Add(wfrIdBox.GetItemText(wfrIdBox.Items[i].ToString()));
                    }
                }

                List<string> typeList = new List<string>();
                for (int i = 0; i < vectorTypeList.Items.Count; i++)
                {
                    if (vectorTypeList.GetItemChecked(i))
                    {
                        typeList.Add(vectorTypeList.GetItemText(vectorTypeList.Items[i].ToString()));
                    }
                }

                List<string> wqmccList = new List<string>();

                for (int i = 0; i < wqMccBox.Items.Count; i++)
                {
                    if (wqMccBox.GetItemChecked(i))
                    {
                        wqmccList.Add(wqMccBox.GetItemText(wqMccBox.Items[i].ToString()));
                    }
                }


                //   LithoForm.Vector.PlotAsmlVector()





                LithoForm.Vector.PlotAsmlVectorNew(wfrList, typeList, wqmccList, listDatatable, zoom, zoom1);
                pictureBox1.Image = null;
                try
                {
                    //若直接引用，同名文件修改后无法正常显示；
                    FileStream fs = new FileStream("C:\\temp\\Vector.emf", FileMode.Open, FileAccess.Read);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show("文件显示失败");
                }
                this.tabControl1.SelectedIndex = 1;


            }
        }
        private void 统计WQMCCDeltaShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询不区分日期，速度极慢，尽量按完整产品名查询\r\n\r\n是（Y），继续\r\n\r\n否（N），退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { return; }

            string tech = string.Empty;
            string part = string.Empty;
            string layer = string.Empty;



            if (MessageBox.Show("此命令是按工艺，产品，层次统计对位相关的WQ，MCC，DELTA SHIT数据\r\n\r\n后续按提示输入，若查询所有，不要输入内容\r\n\r\n注意：查询数据需提前复制到D:\\TEMP\\DB目录\r\n\r\n是（Y），继续\r\n\r\n否（N），退出", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { return; }
            tech = Interaction.InputBox("请输入工艺代码\r\n\r\n若按产品查询，直接回车，不用输入内容", "", "", -1, -1);
            part = Interaction.InputBox("请输入产品名\r\n\r\n若已输入工艺代码，直接回车，不用输入内容", "", "", -1, -1);
            layer = Interaction.InputBox("请输入层次名\r\n\r\n必须且只能输入两位层次代码", "", "", -1, -1);
            tech = tech.Trim().ToUpper(); part = part.Trim().ToUpper(); layer = layer.Trim().ToUpper();
            if (layer.Length != 2)
            { MessageBox.Show("Layer必须为两位，退出，请确认"); return; }
            if (tech.Length == 0 && part.Length == 0)
            { MessageBox.Show("Part和Tech不能同时为空，退出，请确认"); return; }

            dtShow = LithoForm.Asml.SummaryWqMccDelta(tech, part, layer);
            dataGridView1.DataSource = dtShow;
            MessageBox.Show("数据 " + dtShow.Rows.Count.ToString() + " 行");
        }

        #endregion

        #region  //杂项


        private void 选择全部圆片_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < wfrIdBox.Items.Count; i++) { wfrIdBox.SetItemChecked(i, true); }
        }
        private void 取消选择_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < wfrIdBox.Items.Count; i++) { wfrIdBox.SetItemChecked(i, false); }
        }
       
        private void 释放内存重置_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
            //https://blog.csdn.net/qq_39216397/article/details/80139030
        }
        private void 释放_Click(object sender, EventArgs e)
        {
            ClearMemory();
            /*
            string  sqlx, sqly, sqlzx, sqlzy, sqlz;
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlAwe.DB"; }
            connStr = @"data source=D:\TEMP\DB\AsmlAwe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sqlx = " SELECT * FROM TBL_ALSD82_WQMCCDELTA WHERE XY='X' AND ID=477831  ";
                sqly = " SELECT * FROM TBL_ALSD82_WQMCCDELTA WHERE XY='Y' AND ID=477831  ";
                sqlz = " SELECT a.id,a.WaferNr,a.MarkNrX,a.MarkNrY,a.type,b.X as X0,b.Y as Y0,a.X as X,a.Y as Y FROM " +
                    " TBL_ALSD82_MEASURED a,TBL_REFERENCE b WHERE " +
                    " a.MarkNrX=b.MarkNrX AND  a.ID = b.ID AND  a.ID=477831";



                sqlzx = " SELECT z1.id,z1.WaferNr,z1.X0,z1.Y0, " +
                       " x1.RedMCC1 RedMCC1X, x1.RedWQ1 RedWQ1X " +
                    " FROM (" + sqlz + ") z1,(" + sqlx + ") x1 WHERE " +
                    " z1.MarkNrX=x1.MarkNr and z1.WaferNr=x1.WaferNr  and z1.TYPE='R5_Measured'";

                sqlzy = " SELECT z2.id,z2.WaferNr,z2.X0,z2.Y0, " +
                       " y1.RedMCC1 RedMCC1Y, y1.RedWQ1 RedWQ1Y " +
                    " FROM (" + sqlz + ") z2,(" + sqly + ") y1 WHERE " +
                    "  z2.MarkNrY=y1.MarkNr and z2.WaferNr=y1.WaferNr and z2.TYPE='R5_Measured'";

                sql = sqlz;









                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];
                            //tblSecond = ds.Tables[1];

                        }
                    }
                }

            }

            dataGridView1.DataSource = dtShow;
            */
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            zoom = ScrollBar1.Value;
            zoom = (float)(zoom / 10);
        }

        private void ScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            zoom1 = ScrollBar2.Value;
            zoom1 = (float)(zoom1);
        }



        #endregion

        #region //table
        private void 表格一筛选排序统计_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 x = dtShow.Rows.Count;
            }
            catch
            {
                MessageBox.Show("表格未绑定数据，退出"); return;
            }

            StringBuilder passData0 = new StringBuilder();
            StringBuilder passData1 = new StringBuilder();
            if (dtShow != null && dtShow.Columns.Count > 0)
            {
                for (int i = 0; i < dtShow.Columns.Count; i++)
                {

                    passData0.Append((dtShow.Columns[i].ColumnName.ToString() + ","));
                    passData1.Append((dtShow.Columns[i].DataType.Name.ToString() + ","));

                }

            }
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.Show();
            form2.ChildEvent1 += StatData;//统计
            form2.ChildEvent2 += SortData;  //排序
            form2.ChildEvent3 += FilterData;//筛选
            if (Form2Trigger == null)
            { MessageBox.Show("NULL"); }
            else
            {

                Form2Trigger(new string[] { passData0.ToString(), passData1.ToString() });
            }
        }
        private void StatData(string sumKey, string groupKey)
        {
            DataTable dtGroup = new DataTable();
            DataTable dtResult = new DataTable();

            string[] str1 = groupKey.Split(new char[] { ',' });
            string[] str2 = sumKey.Split(new char[] { ',' });
            string selectKey;
            string tmp;

            //dtResult
            foreach (var str in str1)
            { dtResult.Columns.Add(str); }
            foreach (var str in str2)
            {
                tmp = dtShow.Columns[str].DataType.Name.ToString().ToUpper();
                if (tmp.Contains("DOUBLE") || tmp.Contains("INT") || tmp.Contains("DECIMAL"))
                {
                    dtResult.Columns.Add((str + "_COUNT"));
                    dtResult.Columns.Add((str + "_SUM"));
                    dtResult.Columns.Add((str + "_MAX"));
                    dtResult.Columns.Add((str + "_MIN"));
                    dtResult.Columns.Add((str + "_AVG"));


                }
                else
                {
                    dtResult.Columns.Add((str + "_COUNT"));
                }
            }

            //  for (int i=0;i<dtResult.Columns.Count;i++)
            //   { MessageBox.Show(dtResult.Columns[i].ColumnName.ToString()); }






            if (str1.Length == 1)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0]); }
            else if (str1.Length == 2)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1]); }
            else if (str1.Length == 3)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2]); }
            else if (str1.Length == 4)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3]); }
            else if (str1.Length == 5)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4]); }
            else if (str1.Length == 6)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5]); }
            else if (str1.Length == 7)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6]); }
            else if (str1.Length == 8)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7]); }
            else if (str1.Length == 9)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7], str1[8]); }
            else if (str1.Length == 10)
            { dtGroup = dtShow.DefaultView.ToTable(true, str1[0], str1[1], str1[2], str1[3], str1[4], str1[5], str1[6], str1[7], str1[8], str1[9]); }

            else
            { MessageBox.Show("最多选择10个分类项目，现已超过10个，请重新选择"); return; }

            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                selectKey = string.Empty;
                for (int j = 0; j < str1.Length; j++)
                {
                    selectKey += (str1[j] + "='" + dtGroup.Rows[i][j] + "' and ");
                }
                selectKey = selectKey.Substring(0, selectKey.Length - 4);
                // MessageBox.Show(selectKey);
                DataRow[] drs = dtShow.Select(selectKey);
                //MessageBox.Show(drs.Length.ToString());

                DataTable dtTmp = dtShow.Clone();
                foreach (DataRow row in drs)
                { dtTmp.ImportRow(row); }



                DataRow dr = dtResult.NewRow();
                for (int j = 0; j < str1.Length; j++)
                {
                    dr[j] = dtGroup.Rows[i][j].ToString();
                }
                foreach (var str in str2)
                {
                    tmp = dtShow.Columns[str].DataType.Name.ToString().ToUpper();
                    if (tmp.Contains("DOUBLE") || tmp.Contains("INT") || tmp.Contains("DECIMAL"))
                    {
                        dr[str + "_COUNT"] = dtTmp.Compute("count(" + str + ")", "");
                        dr[str + "_SUM"] = dtTmp.Compute("sum(" + str + ")", "");
                        dr[str + "_MAX"] = dtTmp.Compute("max(" + str + ")", "");
                        dr[str + "_MIN"] = dtTmp.Compute("min(" + str + ")", "");
                        dr[str + "_AVG"] = dtTmp.Compute("avg(" + str + ")", "");
                    }
                    else
                    {
                        dr[str + "_COUNT"] = dtTmp.Compute("count(" + str + ")", "");
                    }
                }
                dtResult.Rows.Add(dr);






                // break;
            }
            dtShow = dtResult.Copy(); dtResult = null;
            dataGridView1.DataSource = dtShow;





            /*
            var query = from t in dtShow.AsEnumerable()
                        group t by new
                        {
                            t1 = t.Field<string>("FlowType"),
                            t2 = t.Field<string>("MM")
                        }
                        into m
                        select new
                        {
                            FlowType = m.Key.t1,
                            MM = m.Key.t2,
                            QTY = m.Sum(n => n.Field<double>("QTY"))
                        };
            MessageBox.Show(query.ToList().Count.ToString());
            */
        }

        private void SortData(string sortKey) //排序
        {
            DataView dv = dtShow.DefaultView;
            dv.Sort = sortKey;
            dataGridView1.DataSource = dv.ToTable();
        }
        private void FilterData(string filterKey) //筛选
        {
            DataRow[] drs = dtShow.Select(filterKey);
            DataTable dt = dtShow.Clone();
            foreach (var row in drs)
            { dt.ImportRow(row); }
            dtShow = dt.Copy();
            dt = null;
            dataGridView1.DataSource = dtShow;


        }

        #endregion



        #region //shell

        private void 检查光刻机是否联机ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //https://blog.csdn.net/qq_21747999/article/details/79151910

            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "P:\\_SQLite\\Shell\\dos\\checkTool.bat";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();
        }

        private void 调用Python更细AWEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "c:\\anaconda3\\python.exe";
            exep.StartInfo.Arguments = "P:\\_SQLite\\VsPython.py";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();


        }
        private void 运行曝光程序维护脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "P:\\_SQLite\\EXE\\exposurerecipe\\WindowsFormsExposureRecipe.exe";

            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();
        }

        #endregion

        #region //view

        private void 查看ESF每日修改记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = @"p:\_DailyCheck\ESF\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "csv文件(result*.csv)|result*.csv";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
            dtShow = LithoForm.LibF.OpenCsvNew(filename);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;

        }
        private void 产看ESF可作业设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = @"P:\_DailyCheck\ESF\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "csv文件(ESF_TOOL_AVAILABLE*.csv)|ESF_TOOL_AVAILABLE*.csv";//
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
            dtShow = LithoForm.LibF.OpenCsvNew1(filename);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看CD检查结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = @"P:\_DailyCheck\HITACHI\CHECK\dailycheck.csv";

            dtShow = LithoForm.LibF.OpenCsvNew(filename);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看所有IDP数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connStr = @"data source=P:\_SQLite\cdRecipe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select * from tbl_idp";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看所有AMP数据DBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Part/Layer名字在最后几列");
            connStr = @"data source=P:\_SQLite\cdRecipe.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "select * from tbl_amp";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }




            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 运行脚本查看MetalTopDown图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("运行此命令\r\n\r\n\r\n\r\n" +
                "1)删除C:\\temp目录，再重建；\r\n\r\n" +
                "2)命令调用python.exe,请确认本机已安装；\r\n\r\n" +
                "3)后续运行窗口会提示选择python.exe;\r\n\r\n" +

                "继续请选择-->是（Y）；\r\n\r\n" +
                "退出请选择-->否（N）；", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string filename;
                openFileDialog1.InitialDirectory = "c:\\anaconda3\\";
                openFileDialog1.Title = "选择源文件";
                openFileDialog1.Filter = "文件(python.exe)|python.exe";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName.Replace("\\", "\\\\");
                }
                else
                { MessageBox.Show("未选择数据文件"); return; }



                System.Diagnostics.Process exep = new System.Diagnostics.Process();
                exep.StartInfo.FileName = filename;
                exep.StartInfo.Arguments = "P:\\_SQLite\\VsPython.py";
                exep.StartInfo.CreateNoWindow = false;
                exep.StartInfo.UseShellExecute = true;
                exep.Start();
                exep.WaitForExit();
            }
            else
            {
                MessageBox.Show("EXIT");
            }
        }
        private void 确认NikonSeqLog是否正常下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04" ,"ALII05","ALII06","ALII07","ALII08","ALII09",
                               "ALII10","ALII11", "ALII12", "ALII13" ,"ALII14","ALII15","ALII16","ALII17","ALII18",
                                "BLII20","BLII21","BLII22","BLII23"};
            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("LatestSeqLog");
            dtShow.Columns.Add("SeqFlag");

            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string str1 = string.Empty;
                string str2 = string.Empty;
                string path = @"p:\SEQLOG\" + tool;
                DirectoryInfo root = new DirectoryInfo(path);
                FileInfo[] files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    if (str1.Substring(0, 6) == "_2020-" && String.Compare(str1, str2) == 1) { str2 = str1; }

                }
                DataRow dr = dtShow.NewRow();
                dr["ToolId"] = tool;
                dr["LatestSeqLog"] = str2;
                //string riqi = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                if (String.Compare(str2.Substring(1, 10), DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")) == 1)
                {
                    dr["SeqFlag"] = "";
                }
                else
                {
                    dr["SeqFlag"] = "False";
                }

                dtShow.Rows.Add(dr);




            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;


        }

        private void 确认NikonSeqLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tools = { "ALII01", "ALII02", "ALII03", "ALII04" ,"ALII05","ALII06","ALII07","ALII08","ALII09",
                               "ALII10","ALII11", "ALII12", "ALII13" ,"ALII14","ALII15","ALII16","ALII17","ALII18",
                                "BLII20","BLII21","BLII22","BLII23"};
            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("LatestSeqLog");
            dtShow.Columns.Add("SeqFlag");
            dtShow.Columns.Add("LatestEgaLog");
            dtShow.Columns.Add("EgaFlag");
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string str1 = string.Empty;
                string str2 = string.Empty;
                string path = @"p:\SEQLOG\" + tool;
                DirectoryInfo root = new DirectoryInfo(path);
                FileInfo[] files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    if (str1.Substring(0, 6) == "_2020-" && String.Compare(str1, str2) == 1) { str2 = str1; }

                }
                DataRow dr = dtShow.NewRow();
                dr["ToolId"] = tool;
                dr["LatestSeqLog"] = str2;
                //string riqi = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                if (String.Compare(str2.Substring(1, 10), DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")) == 1)
                {
                    dr["SeqFlag"] = "";
                }
                else
                {
                    dr["SeqFlag"] = "False";
                }

                dtShow.Rows.Add(dr);



                tool = "A" + tool.Substring(1, 5);
                str1 = string.Empty;
                str2 = string.Empty;
                path = @"P:\EgaLog\" + tool;
                root = new DirectoryInfo(path);
                files = root.GetFiles();
                foreach (var x in files)
                {
                    str1 = x.Name;
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }

                }

                dtShow.Rows[i]["LatestEgaLog"] = str2;
                if (str2.Substring(0, 7) == "ega_202" && String.Compare(str2.Substring(4, 9), DateTime.Now.AddDays(-2).ToString("yyyyMMdd")) == 1)
                {
                    dtShow.Rows[i]["EgaFlag"] = "";
                }
                else
                {
                    dtShow.Rows[i]["EgaFlag"] = "False";
                }


            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;


        }
        private void 确认Asml下载是否正常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] tools = { "7D", "08", "82", "83", "85", "86", "87", "89", "8A", "8B", "8C" };
            string line;
            int n = 0;

            dtShow = new DataTable();
            dtShow.Columns.Add("ToolId");
            dtShow.Columns.Add("OldErrLogStartDate");
            dtShow.Columns.Add("ErrLogStartDate");
            dtShow.Columns.Add("AweDate");
            dtShow.Columns.Add("BatchReportDate");
            dtShow.Columns.Add("DateNow");


            for (int i = 0; i < tools.Length; i++)
            {

                string tool = tools[i];
                DataRow newRow = dtShow.NewRow();
                newRow["ToolID"] = tool;
                dtShow.Rows.Add(newRow);
                string root = @"p:\_AsmlDownload\";
                string path = root + @"\AsmlErrlog\" + tool + @"\ER_Event_log";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            try
                            {
                                if (line.Substring(2, 1) == "/" && line.Substring(5, 3) == "/20")
                                {
                                    dtShow.Rows[i]["ErrLogStartDate"] = line.Substring(0, 20);
                                    break;
                                }
                            }
                            catch
                            {
                                //line字符串，substring命令出错
                            }
                        }
                    }
                }
                else
                { }

            }
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string root = @"P:\_AsmlDownload\";
                string path = root + @"\AsmlErrlog\" + tool + @"\ER_Event_log.old";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Substring(2, 1) == "/" && line.Substring(5, 3) == "/20")
                            {



                                dtShow.Rows[i]["OldErrLogStartDate"] = line.Substring(0, 20);
                                break;
                            }
                        }
                    }
                }
                else
                { }






            }

            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string path = @"P:\_AsmlDownload\AWE\" + tool;
                string str1 = string.Empty;
                string str2 = string.Empty;
                DirectoryInfo dirinfo = new DirectoryInfo(path);
                FileInfo[] files = dirinfo.GetFiles();
                foreach (var file in files)
                {
                    str1 = file.Name; str1 = str1.Substring(str1.Length - 19, 12);
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }
                }
                dtShow.Rows[i]["AweDate"] = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + " " + str2.Substring(8, 2) + ":" + str2.Substring(10, 2);

            }
            for (int i = 0; i < tools.Length; i++)
            {
                string tool = tools[i];
                string path = @"P:\_AsmlDownload\BatchReport\" + tool;
                string str1 = string.Empty;
                string str2 = string.Empty;
                DirectoryInfo dirinfo = new DirectoryInfo(path);
                FileInfo[] files = dirinfo.GetFiles();
                foreach (var file in files)
                {
                    str1 = file.Name; str1 = str1.Substring(str1.Length - 19, 12);
                    if (String.Compare(str1, str2) == 1) { str2 = str1; }
                }
                try
                {
                    dtShow.Rows[i]["BatchReportDate"] = str2.Substring(0, 4) + "-" + str2.Substring(4, 2) + "-" + str2.Substring(6, 2) + " " + str2.Substring(8, 2) + ":" + str2.Substring(10, 2);
                    
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error Code: " + ex.Message + "\n\n目录中无下载文件");
                    dtShow.Rows[i]["BatchReportDate"] = "目录中无下载文件";
                }
                dtShow.Rows[i]["DateNow"] = DateTime.Now.ToString();
            }
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
        }
        private void 查看AsmlErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = @"P:\_AsmlDownload\AsmlErrlog\";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "所有文件(*.*)|*.*";//
            dtShow = new DataTable();
            dtShow.Columns.Add("No"); dtShow.Columns.Add("Content");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");
                if (filename.Contains("ER_event_log"))
                {

                    int n = 0;
                    string line;
                    using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Trim().Length > 0)
                            {
                                DataRow row = dtShow.NewRow();
                                row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                            }
                        }
                    }

                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo("D:\\TEMP");
                    directoryInfo.Create();
                    string newFile = DateTime.Now.ToString("yyyyMMddhhmmss");

                    newFile = @"D:\temp\" + newFile;
                    LithoForm.GzipFile.gunZipFile(filename, newFile);
                    int n = 0;
                    string line;
                    using (StreamReader sr = new StreamReader(newFile, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Trim().Length > 0)
                            {
                                DataRow row = dtShow.NewRow();
                                row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                            }
                        }
                    }
                    File.Delete(newFile);

                }
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }

        }
        private void 查看已下载NikonSeqLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = @"P:\SEQLOG";
            openFileDialog1.Title = "选择源文件";
            openFileDialog1.Filter = "所有文件(*.*)|*.*";//
            dtShow = new DataTable();
            dtShow.Columns.Add("No"); dtShow.Columns.Add("Content");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName.Replace("\\", "\\\\");

                int n = 0;
                string line;
                using (StreamReader sr = new StreamReader(filename, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Trim().Length > 0)
                        {
                            DataRow row = dtShow.NewRow();
                            row["No"] = n; row["Content"] = line; dtShow.Rows.Add(row); n += 1;
                        }
                    }
                }

                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未选择数据文件"); return; }
        }
        private void 查看已下载NikonSeqLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = "P:\\_SQLite\\Shell\\exe\\NIKON LOG分析.exe";
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = true;
            exep.Start();
            exep.WaitForExit();
        }
        private void 查看工艺代码层次TrackRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("按2位层次名，工艺代码前三位或工艺代码第二位，TrackRecipe查询所有流程调用的Track Recipe\r\n\r\n\r\r\n" +
                "工艺层次和工艺代码是精确匹配；TrackRecipe是模糊匹配\r\n\r\n\r\r\n" +

              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及设备"); return; }


            //pick layer
            string layer = Interaction.InputBox("请输入层次名；\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim().ToUpper();
            //track
            string track = Interaction.InputBox("请输入Track Recipe；\r\n\r\n\r\n若查询所有Track Recipe，输入%", "定义Track Recipe", "", -1, -1);

            if (track.Trim().Length == 0) { MessageBox.Show("工艺代码输入不正确，退出"); return; }
            track = track.Trim().ToUpper();

            //tech
            string tech = Interaction.InputBox("请输入工艺代码前三位或第二位；\r\n\r\n\r\n若查询所有工艺，输入%", "定义工艺", "", -1, -1);

            if (tech.Trim().Length == 0 || !(tech.Trim().Length == 1 || tech.Trim().Length == 3))
            {
                MessageBox.Show("工艺代码输入不正确;\r\n\r\n\r\n" +
                  "只能输入 1）工艺代码第二位;\r\n\r\n\r\n" +
                  "         2）工艺代码前三位;\r\n\r\n\r\n" +
                  "         3）通配符 '%';\r\n\r\n\r\n" +
                  "退出"); return;
            }
            tech = tech.Trim().ToUpper();



            string sql1;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }

            sql1 = " Select * from tbl_flowtrack where ";

            if (layer.Length == 1)
            { sql1 += " layer like '%'"; }
            else
            { sql1 += " layer='" + layer + "' "; }

            if (track == "%")
            { }
            else
            { sql1 += " and trackrecipe like '%" + track + "%' "; }


            string sql2;

            sql2 = "Select part,tech,processtype,processgroup from tbl_tech ";
            if (tech == "%")
            { }
            else if (tech.Length == 1)
            {
                sql2 += " Where substr(tech,2,1)='" + tech + "'";
            }
            else if (tech.Length == 3)
            {
                sql2 += " Where substr(tech,1,3)='" + tech + "'";
            }





            sql = "select b.tech TECH,a.part,a.layer,a.trackrecipe,a.mask,a.tooltype, substr(a.part,3,4) MaskQueue from (" + sql1 + ") a, (" + sql2 + ") b where   a.part=b.part order by tech, substr(a.part,3,4) desc";


            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];
                        }
                    }
                }
            }











            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("Done");
        }
        #endregion

        #region //R2R
        private void 导入R2RCD数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string strDate;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_cd";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr[0].ToString();
                    }
                }
            }
            //读取导入最新数据
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            DataTable dtTmp = LithoForm.LibF.OpenCsvNew(filePath);
            //判断导入文件中的日期是否有小于 数据库最新日期的，以保证数据连续性

            MessageBox.Show(dtTmp.Rows[0]["DCOLL_TIME"].ToString() + "," + strDate + "," + string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate).ToString());

            if (string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate) == 1)

            { MessageBox.Show("新数据和数据库数据无日期交叠，请选择更早的日期下载OPAS CD数据"); return; }



            DataRow[] drs = dtTmp.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            dtShow = dtTmp.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    dtShow.ImportRow(drs[i]);
                }

                string dbTblName = "tbl_cd";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
                MessageBox.Show("CD Update Done");
                dataGridView1.DataSource = dtShow;
                dtShow = null;
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }

            MessageBox.Show((DateTime.Now - tmpDate).ToString());

        }

        private void 导入R2ROVL数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strDate;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //列出最新日期，
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "SELECT max(DCOLL_TIME) FROM tbl_ovl";//first record select English from graduate_phrase limit 0,1
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        strDate = rdr[0].ToString();
                    }
                }
            }
            //读取导入最新数据
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            DataTable dtTmp = LithoForm.LibF.OpenCsvNew(filePath);
            //判断导入文件中的日期是否有小于 数据库最新日期的，以保证数据连续性

            MessageBox.Show(dtTmp.Rows[0]["DCOLL_TIME"].ToString() + "," + strDate + "," + string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate).ToString());

            if (string.Compare(dtTmp.Rows[0]["DCOLL_TIME"].ToString(), strDate) == 1)

            { MessageBox.Show("新数据和数据库数据无日期交叠，请选择更早的日期下载OPAS CD数据"); return; }



            DataRow[] drs = dtTmp.Select("DCOLL_TIME>'" + strDate + "'", "DCOLL_TIME asc");
            dtShow = dtTmp.Clone();//Clone 复制结构，Copy 复制结构和数据
            if (drs.Length > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    dtShow.ImportRow(drs[i]);
                }



                string dbTblName = "tbl_ovl";
                DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
                MessageBox.Show("OVL Update Done");
                dataGridView1.DataSource = dtShow;
                dtShow = null;
            }
            else
            { MessageBox.Show("无新数据，请从OPAS重新下载数据"); }



        }
        private void 导入CDCONFIG数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtShow = null;
            connStr = "data source=P:\\_SQLite\\R2R.db";
            //读取
            string filePath;
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(*.csv)|*.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            dtShow = LithoForm.LibF.OpenCsvNew(filePath);

            //删除旧数据
            string dbTblName = "tbl_config";
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
            DataTableToSQLte myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\R2R.DB");
            MessageBox.Show("CD Config Update Done");
            dataGridView1.DataSource = dtShow;
            dtShow = null;

        }
        private void 导入短流程工艺代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TO BE COMPLETED");
        }
        private void 导入NikonJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string filePath;
            connStr = "data source=P:\\_SQLite\\NikonJobinStation.db";
            string dbTblName = "tbl_nikonjobinstation";
            //读取
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(NikonJobinStation.csv)|NikonJobinStation.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }






            bool isFirst = true;
            string strLine;
            int n = 0;
            int rowCount = 0;
            string newLine = string.Empty;
            dtShow = new DataTable();
            DataTable dtTmp = new DataTable();
            int stepCount = 0;
            DataTableToSQLte myTabInfo;

            StreamReader sr = new StreamReader(filePath);
            while ((strLine = sr.ReadLine()) != null)
            {
                if (strLine.Contains("\""))   //将逗号改为分号，
                {
                    n = 0; newLine = "";
                    foreach (char x in strLine)
                    {
                        if (x.ToString() != "\"" && n % 2 == 0)
                        {
                            newLine += x.ToString();
                        }
                        else if (x.ToString() == "\"" && n % 2 == 0)
                        {
                            n += 1;
                        }
                        else if (x.ToString() != "\"" && n % 2 == 1)
                        {
                            if (x.ToString() != ",")
                            {
                                newLine += x.ToString();
                            }
                            else
                            {
                                newLine += ";";
                            }

                        }

                    }
                    strLine = newLine;
                }

                if (isFirst == true)
                {
                    isFirst = false;
                    // MessageBox.Show(strLine);
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {

                        dtShow.Columns.Add(colAry[i]);
                    }
                    dtTmp = dtShow.Clone();
                }
                else
                {
                    rowCount += 1;
                    stepCount += 1;
                    DataRow newRow = dtShow.NewRow();
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {
                        newRow[i] = colAry[i];
                    }
                    dtShow.Rows.Add(newRow);

                    if (stepCount > 50000)
                    {

                        myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\NikonJobinStation.DB");
                        stepCount = 0;
                        dtShow.Rows.Clear();
                        dtShow = null;
                        ClearMemory();
                        dtShow = dtTmp.Clone();

                    }



                }
            }
            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\NikonJobinStation.DB");
            MessageBox.Show((DateTime.Now - tmpDate).ToString());
            MessageBox.Show("导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();
        }
        private void 导入AsmlJobinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tmpDate = DateTime.Now;
            string filePath;
            connStr = "data source=P:\\_SQLite\\AsmlJobinStation.db";
            string dbTblName = "tbl_asmljobinstation";
            //读取
            openFileDialog1.InitialDirectory = @"P:\_SQLite\";
            openFileDialog1.Filter = "csv文件(AsmlJobinStation.csv)|AsmlJobinStation.csv|所有文件(*)|*";//筛选文件
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//弹出打开文件对话框
            { filePath = openFileDialog1.FileName.Replace("\\", "\\\\"); }
            else
            { MessageBox.Show("未选择文件，退出"); return; }

            //删除旧数据
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "DELETE  FROM " + dbTblName;
                using (SQLiteCommand com = new SQLiteCommand(sql, conn))
                {
                    com.ExecuteNonQuery();
                }
            }






            bool isFirst = true;
            string strLine;
            int n = 0;
            int rowCount = 0;
            string newLine = string.Empty;
            dtShow = new DataTable();
            DataTable dtTmp = new DataTable();
            int stepCount = 0;
            DataTableToSQLte myTabInfo;

            StreamReader sr = new StreamReader(filePath);
            while ((strLine = sr.ReadLine()) != null)
            {
                if (strLine.Contains("\""))   //将逗号改为分号，
                {
                    n = 0; newLine = "";
                    foreach (char x in strLine)
                    {
                        if (x.ToString() != "\"" && n % 2 == 0)
                        {
                            newLine += x.ToString();
                        }
                        else if (x.ToString() == "\"" && n % 2 == 0)
                        {
                            n += 1;
                        }
                        else if (x.ToString() != "\"" && n % 2 == 1)
                        {
                            if (x.ToString() != ",")
                            {
                                newLine += x.ToString();
                            }
                            else
                            {
                                newLine += ";";
                            }

                        }

                    }
                    strLine = newLine;
                }

                if (isFirst == true)
                {
                    isFirst = false;
                    MessageBox.Show(strLine);
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {

                        dtShow.Columns.Add(colAry[i]);
                    }
                    dtTmp = dtShow.Clone();
                }
                else
                {
                    rowCount += 1;
                    stepCount += 1;
                    DataRow newRow = dtShow.NewRow();
                    string[] colAry = strLine.Split(new char[] { ',' });
                    for (int i = 0; i < colAry.Length; i++)
                    {
                        newRow[i] = colAry[i];
                    }
                    dtShow.Rows.Add(newRow);

                    if (stepCount > 10000)
                    {

                        myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
                        myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\AsmlJobinStation.DB");
                        stepCount = 0;
                        dtShow.Rows.Clear();

                        dtShow = null;
                        ClearMemory();
                        dtShow = dtTmp.Clone();

                    }



                }
            }
            myTabInfo = new DataTableToSQLte(dtShow, dbTblName);
            myTabInfo.ImportToSqliteBatch(dtShow, @"P:\_SQLite\AsmlJobinStation.DB");
            MessageBox.Show((DateTime.Now - tmpDate).ToString());
            MessageBox.Show("导入数据记录: " + rowCount.ToString() + "条");
            sr.Close();

        }


        #endregion


        #region //R2R Query
        private void 清除设备选项_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++) { checkedListBox2.SetItemChecked(i, false); }
        }
        private void 清除参数选项_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox3.Items.Count; i++) { checkedListBox3.SetItemChecked(i, false); }
        }
        private void 查询CDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
              "结束日期\r\n\r\n    设备名\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备\r\n\r\n" +
              "    选择 是（Y） 继续\r\n\r\n" +
              "    选择 否（N） 退出\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及设备"); return; }



            //pick tool
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }
            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\R2R.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\R2R.DB"; }

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;
            dtShow = LithoForm.R2R.cdQuery(tools, part, layer, date1, date2, connStr);
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            int rowsCount = dtShow.Rows.Count;
            MessageBox.Show("共计筛选到" + rowsCount.ToString() + "行数据");
        }

        private void 查询OVERLAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    设备名\r\n\r\n    OVL参数\r\n\r\n    产品名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期是否合适，是否已选择设备，OVL参数\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出\r\n\r\n\r\n" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期，设备，OVL参数"); return; }

            //pick tools
            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            //pick parameter
            string para = "";
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    if (para == "")
                    { para = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    else
                    { para = para + ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                }
            }




            //pick part
            string part = Interaction.InputBox("该查询要求输入产品名和层次名，通配符查询;\r\n\r\n\r\n现在请输入产品名，不同产品名间以空格区分;\r\n\r\n\r\n若查询所有产品，输入%", "定义产品", "", -1, -1);

            if (part.Trim().Length == 0) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = part.Trim();

            //pick layer
            string layer = Interaction.InputBox("现在请输入层次名，不同层次名间以空格区分;\r\n\r\n\r\n若查询所有层次，输入%", "定义层次", "", -1, -1);

            if (layer.Trim().Length == 0) { MessageBox.Show("层次名输入不正确，退出"); return; }
            layer = layer.Trim();

            string date1 = dateTimePicker1.Text;
            string date2 = dateTimePicker2.Text;

            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\R2R.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\R2R.DB"; }
            dtShow = LithoForm.R2R.ovlQuery(tools, para, part, layer, date1, date2, connStr);
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;
            MessageBox.Show("共计筛选到" + dtShow.Rows.Count.ToString() + "行数据");

        }

        private void 按工艺查询CDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("查询筛选条件：\r\n\r\n    起始日期\r\n\r\n    " +
             "结束日期\r\n\r\n    设备名\r\n\r\n    工艺名\r\n\r\n    层次名\r\n\r\n\r\n请确认日期，设备选择是否合适\r\n\r\n\r\n\r\n" +
             "    选择 是（Y） 继续\r\n\r\n" +
             "    选择 否（N） 退出" +
             "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期，设备"); return; }

            string bDate = dateTimePicker1.Text;
            string eDate= dateTimePicker2.Text;

            string sql2 = string.Empty; //-->tbl_config, cd target
            //pick tool

            string tools = string.Empty;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (tools == "")
                    { tools = "'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                    else
                    { tools = tools + ",'" + checkedListBox2.GetItemText(checkedListBox2.Items[i]) + "'"; }
                }
            }
            if (tools.Length == 0)
            { MessageBox.Show("未选择任何设备，默认全选"); }

            bool sourceFlag = radioButton2.Checked;


            dtShow = LithoForm.R2R.QueryCdByTech(tools,bDate,eDate, sourceFlag);


            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;
                 }
        private void 查询JobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sourceFlag = radioButton2.Checked;
            dtShow = LithoForm.R2R.QueryJobinStation(sourceFlag);
            dataGridView1.DataSource = dtShow;
            tabControl1.SelectedIndex = 0;

          

        }




        private void 查看产品工艺代码DieQtyChipSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入Part名查询\r\n\r\n\r\n输入空格或%查询所有", "", "", -1, -1);
            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.db"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.db"; }


            sql = "select * from tbl_tech where part like '%" + str + "%'";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];
                        }
                    }
                }
            }


            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未查询到数据"); }
        }
        private void 查看光刻版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入光刻版名前四位\r\n\r\n\r\n输入%查询所有", "", "", -1, -1);

            if (str.Trim().Length == 0) { MessageBox.Show("No Input,Exit"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.db"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.db"; }


            sql = "select * from tbl_mask where Mask_MPW like '" + str + "%'";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];
                        }
                    }
                }
            }
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
            }
            else
            { MessageBox.Show("未查询到数据"); }
        }
        #endregion

        #region //extra
        private void 单个产品的照明条件查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
                "    完整产品名\r\n\r\n" +
                "    两位曝光程序层次名\r\n\r\n\r\n" +
                "选择 是（Y） 继续\r\n\r\n" +
                "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
                "至多列出最新10笔数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }




            string part = Interaction.InputBox("该查询要求输入产品名和层次名;\r\n\r\n\r\n" +
                "精确匹配，不支持通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n", "定义产品", "", -1, -1);
            part = part.Trim().ToUpper();
            if (part.Length < 8) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = "PROD/" + part;
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义层次", "", -1, -1);
            layer = layer.Trim().ToUpper();
            if (layer.Trim().Length != 2) { MessageBox.Show("层次名输入不正确，退出"); return; }


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }

            // layer = "SI";
            //part = "PROD/F11410DJ";
            sql = " SELECT tool,date,jobName part,layer,illuminationmode,aperture,sigmaoutjob,singmainactual,dosejob,focusjob,alignstrategy from tbl_asmlbatchreport ";
            sql += " WHERE Layer='" + layer + "' and jobname='" + part + "'";








            dtShow = LithoForm.Asml.QueryIllumination(connStr, part, layer);
            if (dtShow.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtShow;
                this.tabControl1.SelectedIndex = 0;
                MessageBox.Show("DONE");
            }
            {
                MessageBox.Show("No Data Available");
            }


        }
        private void 按工艺和TrackRecipe统计照明条件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("根据工艺代码前三位及工艺层次统计照明条件\r\n\r\n\r\n照明条件来自batch report历史记录\r\n\r\n\r\n期间流程更改，统计数据可能有误，仅供参考");

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
              "    工艺代码前三位\r\n\r\n" +
              "    两位曝光程序层次名\r\n\r\n\r\n" +
              "选择 是（Y） 继续\r\n\r\n" +
              "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }

            string tech = Interaction.InputBox("请输入工艺代码前三位;\r\n\r\n\r\n" +
          "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义工艺代码前三位", "", -1, -1);
            tech = tech.Trim().ToUpper();

            string layer = Interaction.InputBox("请输入两位层次名;\r\n\r\n\r\n" +
         "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义两位工艺层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            if (tech.Length != 3 || layer.Length != 2) { MessageBox.Show("工艺代码和层次名格式不对，退出"); }
            string connStr1;
            if (radioButton2.Checked)
            {
                connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB";
                connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
            }
            else
            {
                connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB";
                connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";

            }

            dtShow = LithoForm.Asml.QueryIlluminationTechLayer(connStr, connStr1, tech, layer);

            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("DONE");


        }
        private void 按工艺设备TrackRecipe统计FocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("根据工艺代码前三位及工艺层次统计Focus设定\r\n\r\n\r\nFocus设定来自batch report历史记录\r\n\r\n\r\n期间曝光条件，流程等更改，统计数据可能有误，仅供参考");

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
              "    工艺代码前三位\r\n\r\n" +
              "    两位曝光程序层次名\r\n\r\n\r\n" +
              "选择 是（Y） 继续\r\n\r\n" +
              "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }

            string tech = Interaction.InputBox("请输入工艺代码前三位;\r\n\r\n\r\n" +
          "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义工艺代码前三位", "", -1, -1);
            tech = tech.Trim().ToUpper();

            string layer = Interaction.InputBox("请输入两位层次名;\r\n\r\n\r\n" +
         "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义两位工艺层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            if (tech.Length != 3 || layer.Length != 2) { MessageBox.Show("工艺代码和层次名格式不对，退出"); }
            string connStr1;
            if (radioButton2.Checked)
            {
                connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB";
                connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
            }
            else
            {
                connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB";
                connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";

            }

            dtShow = LithoForm.Asml.QueryFocusTechLayer(connStr, connStr1, tech, layer);

            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            MessageBox.Show("DONE");

        }
        private void 在线WIPJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("生成 OPAS Oracle SQL\r\n\r\n\r\n\r\n查询Inline WIP Job-In-Station的值\r\n\r\n\r\n\r\nsql在10.4.3.130服务器使用\r\n\r\n\r\n\r\n数据格式符合R2R Import命令格式，按需修改数据后直接导入" +
                "\r\n\r\n\r\n\r\n另在线WIP需要访问MFG_DB提前刷新；未及时更新数据可能有误");


            if (radioButton2.Checked)
            { connStr = @"data source=D:\TEMP\DB\ReworkMove.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\ReworkMove.DB"; }
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = "SELECT * FROM TBL_REALWIP";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }
            LithoForm.R2R.GenerateJobinStationSql(dtShow);
            MessageBox.Show("文件 C:\\temp\\sql.txt 已保存\r\n\r\n\r\n使用sql在服务器10.4.3.130导出数据约需4分钟\r\n\r\n\r\n文件约50M");
        }
        private void 全新新品JobinStationToolStripMenuItem_Click(object sender, EventArgs e)  //to be removed
        {
            //起始日期
            MessageBox.Show("全新新品定义为需要重新计算Wafer Map的产品\r\n\r\n\r\n\r\n" +
                "列出n天内新建新品清单\r\n\r\n\r\n\r\n" +
                "抽取新品的R2R JobinStation值供批量维护\r\n\r\n\r\n\r\n");
            long days;
            try
            {
                days = Convert.ToInt32(Interaction.InputBox("请输入倒数？天天数;", "定义天数", "", -1, -1).Trim());
            }
            catch
            {
                MessageBox.Show("请确认输入字符是否正确，退出"); return;
            }
            string riqi = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
            MessageBox.Show("开始的统计日期是：" + riqi);
            //列出Part
            connStr = @"data source=P:\_SQLite\Flow.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT DISTINCT PART FROM TBL_MAP WHERE PART NOT LIKE '%NIKON' AND  RIQI>='" + riqi + "'";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }
            LithoForm.R2R.GenerateJobinStationSql(dtShow);

            MessageBox.Show("文件 C:\\temp\\sql.txt 已保存");



        }

        private void 已作业全新新品的JobinStationToolStripMenuItem_Click(object sender, EventArgs e) //to be removed
        {
            MessageBox.Show("全新新品定义为需要重新计算Wafer Map的产品\r\n\r\n\r\n\r\n" +
               "列出n天内新建新品清单\r\n\r\n\r\n\r\n" +
               "抽取一周内已作业新品的R2R JobinStation值供批量维护\r\n\r\n\r\n\r\n");
            long days;
            try
            {
                days = Convert.ToInt32(Interaction.InputBox("请输入倒数？天天数;", "定义天数", "", -1, -1).Trim());
            }
            catch
            {
                MessageBox.Show("请确认输入字符是否正确，退出"); return;
            }
            string riqi = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
            MessageBox.Show("开始的统计日期是：" + riqi);
            //列出Part
            connStr = @"data source=P:\_SQLite\Flow.DB";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                sql = " SELECT DISTINCT PART FROM TBL_MAP WHERE PART NOT LIKE '%NIKON' AND  RIQI>='" + riqi + "'";
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dtShow = ds.Tables[0];

                        }
                    }
                }
            }

            if (dtShow.Rows.Count > 0)
            { }
            else
            { MessageBox.Show("期间无全新新品，退出；"); return; }

            sql = "SELECT DISTINCT PARTNAME ,SUBSTR(EQP_PPID,INSTR(EQP_PPID,'.')+1,10) LAYER  FROM RPTPRD.MFG_VIEW_STEP_MOVE WHERE substr(lottype, 1, 1) in ('M', 'N', 'P', 'E','S') AND SUBSTR(EQPID,2,2) IN ('LD','LI','LS') AND STAGE<>'RUN-STA' AND TRACKOUTTIME >sysdate-7";

            DataTable dt = LithoForm.LibF.CsmcOracle(sql);


            DataTable newDt;
            DataRow[] drs;
            newDt = dt.Clone();
            foreach (DataRow dr in dtShow.Rows)
            {
                drs = dt.Select("PARTNAME='" + dr[0].ToString() + "'");

                if (drs.Length > 0)
                {
                    foreach (DataRow x in drs)
                    {
                        newDt.ImportRow(x);
                    }
                }
            }

            dtShow = newDt.Copy();
            dataGridView1.DataSource = dtShow;
            this.tabControl1.SelectedIndex = 0;
            newDt = null; dt = null; ClearMemory();
            LithoForm.R2R.GenerateJobinStationSql(dtShow, "layer");
        }
        private void 最近天R2RCDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("最近n天内R2R CD数据\r\n\r\n\r\n\r\n" +
             "" +
             "");
            long days;
            try
            {
                days = Convert.ToInt32(Interaction.InputBox("请输入倒数？天天数;", "定义天数", "", -1, -1).Trim());
            }
            catch
            {
                MessageBox.Show("请确认输入字符是否正确，退出"); return;
            }
            string riqi = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
            MessageBox.Show("开始的统计日期是：" + riqi);
            sql = " SELECT " +
                  " to_char(DCOLL_TIME, 'yyyy-MM-dd HH24:mi:ss') DCOLL_TIME,to_char(JI_TIME, 'yyyy-MM-dd HH24:mi:ss') JI_TIME," +
                  " LOT_ID LotID, PART_NAME PART, TECH_NAME TECH, LAYER_NAME LAYER,	PROC_EQ_ID TOOL, MET_EQ_ID,	GROUP_TYPE TYPE," +
                  " EQP_TYPE,MET_AVG AVG,PARAM_VALUE JOBIN, OPT_VALUE OPT,	RESULT_VALUE FB,MET_PARAM_NAME ITEM " +
                  " FROM  r2rph.proc_dcoll_fb_view WHERE  RESULT_VALUE IS NOT NULL   AND " +
                  " DCOLL_TIME > SYSDATE - " + days + " ORDER BY  EQP_TYPE, DCOLL_TIME ASC ";

            System.IO.File.WriteAllText(@"C:\temp\sql.txt", sql);
            MessageBox.Show("文件 C:\\temp\\sql.txt 已保存");
        }











        #endregion

        //#region //extra1
        private void 步骤一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.调用 EXTRA 主菜单下的 MakeOpasSql 子菜单\r\n\n\r\n" +
                "2.复制生成的SQL，登录10.4.3.130下载CSV格式数据");
        }

        private void 步骤二SQLITEDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sourceFlag = radioButton2.Checked;
            LithoForm.R2R.CreateLocalDb(sourceFlag);
          
        }

        private void 步骤三按设备前后段查询CDOVLJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region
            dtShow = null; dt1 = null; dt2 = null; ClearMemory();
            bool sourceFlag = radioButton2.Checked;

            string choice = Interaction.InputBox("数据查询按Nikon、ASML前后段区分\r\n\r\n\r\n" +
                "请按提示输入选项\r\n\r\n\r\n" +
                "   ASML FEOL， 请输入 AF\r\n\r\n\r\n" +
                "   ASML BEOL， 请输入 AB\r\n\r\n\r\n" +
                "   NIKON FEOL，请输入 NF\r\n\r\n\r\n" +
                "   NIKON BEOL，请输入 NB\r\n\r\n\r\n", "定义DUV/Iline 前后段", "", -1, -1);
            choice = choice.Trim().ToUpper();
            if (choice == "AF" || choice == "AB" || choice == "NB" || choice == "NF")
            { }
            else
            { MessageBox.Show("输入不正确，退出"); return; }

            //定义其它筛选参数
            bool optionCd = radioCd.Checked;
            string paraOvl = string.Empty;
            if (radioOvl.Checked)
            {
                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    if (checkedListBox3.GetItemChecked(i))
                    {
                        if (paraOvl == "")
                        { paraOvl = "'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                        else
                        { paraOvl += ",'" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                    }
                }
            }

            string[] cdFlag = new string[] { checkBoxFlagCD.Checked.ToString(), textBox1.Text.Trim() };
            string[] tranFlag = new string[] { checkBoxFlagTran.Checked.ToString(), textBox2.Text.Trim() };
            string[] expFlag = new string[] { checkBoxFlagExp.Checked.ToString(), textBox3.Text.Trim() };
            string[] ortRotFlag = new string[] { checkBoxFlagOrtRot.Checked.ToString(), textBox4.Text.Trim() };
            string[] shotFlag = new string[] { checkBoxFlagShot.Checked.ToString(), textBox5.Text.Trim() };




            DataTable[] dtTables;
            dtTables = LithoForm.R2R.ReadFeolBeolData(choice, sourceFlag, cdFlag, tranFlag, expFlag, ortRotFlag, shotFlag);

            // MessageBox.Show(dtTables.Length.ToString());

            dtShow = dtTables[0];
            dt2 = dtTables[1];
            dt1 = dtTables[2];
            #endregion

            //筛选项加入listview

            if (dt1.Rows.Count > 0)
            {
                listView1.Clear();
                listView1.GridLines = true;
                listView1.FullRowSelect = true;//display full row
                listView1.MultiSelect = false;
                listView1.View = View.Details;
                listView1.HoverSelection = false;//鼠标停留数秒后，自动选择

                listView1.Columns.Add("PART", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("LAYER", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("LOTID", 80, HorizontalAlignment.Left);

                if (dt1.Columns.Count > 2)
                {
                    for (int i = 3; i < dt1.Columns.Count; i++)

                    { listView1.Columns.Add(dt1.Columns[i].ColumnName.ToString(), 80, HorizontalAlignment.Left); }
                }
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ListViewItem li = new ListViewItem(dt1.Rows[i][0].ToString());//
                    for (int j = 1; j < dt1.Columns.Count; j++)
                    {
                        li.SubItems.Add(dt1.Rows[i][j].ToString());
                    }

                    listView1.Items.Add(li);

                }


                dt1 = null;ClearMemory();


            }
            else { MessageBox.Show("未查询到数据"); }
                                 

            //以下form3子窗体不再调用
            //  if (form3Flag == false)
            //  {  Form3.choiceNo = 888; Form3 form3 = new Form3();form3.Owner = this; form3.Form3Event1 += r2rQuery;  form3.Show(); form3Flag = true;   }
            //    if (Form3Trigger == null) { MessageBox.Show("NULL"); }  else { Form3Trigger(dt1);}

        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)  //全新新品筛选
        {
            int no;
            try
            {
                no = Convert.ToInt32(Interaction.InputBox("在完成步骤三的前提下，筛选出最新的新品供查询;\r\n\r\n\r\n" +
                   "现在请输入新品数量", "定义新品数量", "", -1, -1));
            }
            catch
            {
                MessageBox.Show("Input Is Not Correct,Exit"); return;
            }
            connStr = @"data source=P:\_SQLite\Flow.DB";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = " SELECT DISTINCT PART FROM TBL_CHECK2 WHERE PART IS NOT NULL";

                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];
                            if (dt1.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件"); return;

                            }

                        }
                    }
                }
            }

            string str = string.Empty;

            for (int i = dt1.Rows.Count - 1; i > dt1.Rows.Count - no - 1; i--)
            {
                if (i > -1)
                { str += dt1.Rows[i][0].ToString() + ","; }
            }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            foreach (ListViewItem item in listView1.Items)
            {

                if (!(str.Contains(item.SubItems[0].Text)))
                { listView1.Items.Remove(item); }
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e) //衍生新品
        {
            int no;
            try
            {
                no = Convert.ToInt32(Interaction.InputBox("在完成步骤三的前提下，筛选出最新的衍生新品供查询;\r\n\r\n\r\n" +
                   "现在请输入新品数量", "定义新品数量", "", -1, -1));
            }
            catch
            {
                MessageBox.Show("Input Is Not Correct,Exit"); return;
            }
            connStr = @"data source=P:\_SQLite\Flow.DB";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                sql = "select distinct part from tbl_biastable where part not in (SELECT DISTINCT PART FROM TBL_CHECK2 where part is not null ) and part is not null"; //where part is not null,否则not in 查询数据为空
               
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            dt1 = ds.Tables[0];
                            if (dt1.Rows.Count == 0)
                            {
                                MessageBox.Show("未查询到数据，请重新输入查询条件"); return;

                            }

                        }
                    }
                }
            }


            string str = string.Empty;

            for (int i = dt1.Rows.Count - 1; i > dt1.Rows.Count - no - 1; i--)
            {
                if (i > -1)
                { str += dt1.Rows[i][0].ToString() + ","; }
            }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            foreach (ListViewItem item in listView1.Items)
            {

                if (!(str.Contains(item.SubItems[0].Text)))
                { listView1.Items.Remove(item); }
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

        }
        private void DailyR2rReview()
        {
            ClearMemory();
            DataRow[] drs; DataTable dt11 = dtShow.Clone(); DataTable dt22 = dt2.Clone();
            //定义参数
            string items = string.Empty;
            if (radioCd.Checked)
            {
                items = " ITEM='DOSE' ";
            }
            else
            {
                if (checkedListBox3.GetItemChecked(0))
                {
                    items = " ITEM<>'DOSE' ";
                }
                else
                {

                    for (int i = 1; i < checkedListBox3.Items.Count; i++)
                    {

                        if (checkedListBox3.GetItemChecked(i))
                        {
                            if (items == "")
                            { items += " ITEM = '" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                            else
                            { items += " OR ITEM = '" + checkedListBox3.GetItemText(checkedListBox3.Items[i]) + "'"; }
                        }
                    }


                }
                if (items.Length == 0)
                { items = " ITEM<>'DOSE' "; } //一个不选，默认全选
                else
                { items = " ( " + items + " ) "; }





            }


            // MessageBox.Show(items);
            if (radioByPartLayer.Checked)  //by part layer
            {
                drs = dtShow.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and " + items);


            }
            else //by lot
            {
                drs = dtShow.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and LOTID = '" + listView1.SelectedItems[0].SubItems[2].Text + "' and " + items);
            }
            // MessageBox.Show("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'  and " + items);


            foreach (var row in drs)
            { dt11.ImportRow(row); }
            dt11.DefaultView.Sort = "DCOLL_TIME ASC";

            dataGridView1.DataSource = dt11;

            dt22 = dt2.Clone();
            drs = dt2.Select("PART='" + listView1.SelectedItems[0].SubItems[0].Text + "' and LAYER='" + listView1.SelectedItems[0].SubItems[1].Text + "'");
            foreach (var row in drs)
            { dt22.ImportRow(row); }
            dataGridView2.DataSource = dt22;


        }
        private void 步骤四可选仅列出CDOVL异常LOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
     

            if (listView1.Items[0].SubItems.Count==3)
            { MessageBox.Show("No CD/OVL Flag, Exit");return; }

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            bool f;
            foreach (ListViewItem item in listView1.Items)
            {
                f = false;
                for (int i=3;i<item.SubItems.Count;i++)
                {
                    if (item.SubItems[i].Text=="True")
                    {
                        f = true;   
                        break;
                    }
                }
                if (f == false){ listView1.Items.Remove(item); }
            }
                                                                    
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。  
        }  
        private void 步骤五可选JobInStation导出为Nikon格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dt1 = LithoForm.LibF.GetDgvToTable(dataGridView2);

            DataTable dt = new DataTable();
            string[] col = new string[] {"Tech",
"PartID","Layer","AlignLayer","FeedbackOvl","ReservedOvl","ReservedPartOvl","ExpiredHoursOvl","FeedbackCd","ReservedCd","ReservedPartCd","R_Value","E.L_Value","TypeCD","ExpiredHoursCd","Pre_EqId",              "EqId","CD_Target","Dose_Value","Dose_Up","Dose_Low","Focus_Value","Focus_Up","Focus_Low","TrX_Value","TrX_Up","TrX_Low","TrY_Value","TrY_Up","TrY_Low","ExpX_Value","ExpX_Up","ExpX_Low","ExpY_Value","ExpY_Up","ExpY_Low","Ort_Value","Ort_Up","Ort_Low","Rot_Value","Rot_Up","Rot_Low","SMag_Value","SMag_Up","SMag_Low","SRot_Value","SRot_Up","SRot_Low","Lock/Unlock","Fix","Constrain","Pre_Layer_Name"};

            foreach (string x in col)
            { dt.Columns.Add(x); }

            for (int count = 0; count < dataGridView2.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                dr["PartID"] = Convert.ToString(dataGridView2.Rows[count].Cells[0].Value);
                dr["Layer"] = Convert.ToString(dataGridView2.Rows[count].Cells[1].Value);
                dr["EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[2].Value);
                dr["Dose_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[3].Value);
                dr["Focus_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[4].Value);
                dr["TrX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[5].Value);
                dr["TrY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[6].Value);
                dr["ExpX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[7].Value);
                dr["ExpY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[8].Value);
                dr["Ort_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[9].Value);
                dr["Rot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[10].Value);
                dr["SMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[11].Value);
                dr["SRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[12].Value);
                dr["CD_Target"] = Convert.ToString(dataGridView2.Rows[count].Cells[15].Value);
                dr["Pre_EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[16].Value);
                dr["Pre_Layer_Name"] = Convert.ToString(dataGridView2.Rows[count].Cells[17].Value);
                dr["FeedbackCd"] = Convert.ToString(dataGridView2.Rows[count].Cells[18].Value);
                dr["FeedbackOvl"] = Convert.ToString(dataGridView2.Rows[count].Cells[19].Value);
                dr["Constrain"] = Convert.ToString(dataGridView2.Rows[count].Cells[20].Value);
                dr["E.L_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[21].Value);
                dr["TypeCD"] = Convert.ToString(dataGridView2.Rows[count].Cells[22].Value);
                dr["Fix"] = Convert.ToString(dataGridView2.Rows[count].Cells[23].Value);
                dr["Lock/Unlock"] = Convert.ToString(dataGridView2.Rows[count].Cells[24].Value);


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            LithoForm.LibF.dataTableToCsv(dt, "d:\\temp\\import.csv");
            MessageBox.Show("Save As D:\\TEMP\\IMPORT.CSV");
                                                                              
        }
        private void 步骤六可选JobInStation导出为Asml格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //dt1 = LithoForm.LibF.GetDgvToTable(dataGridView2);

            DataTable dt = new DataTable();
            string[] col = new string[] {"Tech",
"PartID","Layer","AlignLayer","FeedbackOvl","ReservedOvl","ReservedPartOvl","ExpiredHoursOvl","FeedbackCd","ReservedCd","ReservedPartCd","R_Value","E.L_Value","TypeCD","ExpiredHoursCd","Pre_EqId",              "EqId","CD_Target","Dose_Value","Dose_Up","Dose_Low","Focus_Value","Focus_Up","Focus_Low","TrX_Value","TrX_Up","TrX_Low","TrY_Value","TrY_Up","TrY_Low","ExpX_Value","ExpX_Up","ExpX_Low","ExpY_Value","ExpY_Up","ExpY_Low","Ort_Value","Ort_Up","Ort_Low","Rot_Value","Rot_Up","Rot_Low","SMag_Value","SMag_Up","SMag_Low","SRot_Value","SRot_Up","SRot_Low","ASMag_Value","ASMag_Up","ASMag_Low","ASRot_Value","ASRot_Up","ASRot_Low","Lock/Unlock","Fix","Constrain","Pre_Layer_Name"};

            foreach (string x in col)
            { dt.Columns.Add(x); }

            for (int count = 0; count < dataGridView2.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                dr["PartID"] = Convert.ToString(dataGridView2.Rows[count].Cells[0].Value);
                dr["Layer"] = Convert.ToString(dataGridView2.Rows[count].Cells[1].Value);
                dr["EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[2].Value);
                dr["Dose_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[3].Value);
                dr["Focus_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[4].Value);
                dr["TrX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[5].Value);
                dr["TrY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[6].Value);
                dr["ExpX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[7].Value);
                dr["ExpY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[8].Value);
                dr["Ort_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[9].Value);
                dr["Rot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[10].Value);
                dr["SMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[11].Value);
                dr["SRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[12].Value);
                dr["ASMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[13].Value);
                dr["ASRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[14].Value);


                dr["CD_Target"] = Convert.ToString(dataGridView2.Rows[count].Cells[15].Value);
                dr["Pre_EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[16].Value);
                dr["Pre_Layer_Name"] = Convert.ToString(dataGridView2.Rows[count].Cells[17].Value);
                dr["FeedbackCd"] = Convert.ToString(dataGridView2.Rows[count].Cells[18].Value);
                dr["FeedbackOvl"] = Convert.ToString(dataGridView2.Rows[count].Cells[19].Value);
                dr["Constrain"] = Convert.ToString(dataGridView2.Rows[count].Cells[20].Value);
                dr["E.L_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[21].Value);
                dr["TypeCD"] = Convert.ToString(dataGridView2.Rows[count].Cells[22].Value);
                dr["Fix"] = Convert.ToString(dataGridView2.Rows[count].Cells[23].Value);
                dr["Lock/Unlock"] = Convert.ToString(dataGridView2.Rows[count].Cells[24].Value);


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            LithoForm.LibF.dataTableToCsv(dt, "d:\\temp\\import.csv");
            MessageBox.Show("Save As D:\\TEMP\\IMPORT.CSV");
        }
        private void 步骤七可选列出TestWaferPerLotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("LOCK_FIXED='Y'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }
            
            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }
        private void 步骤八可选列出限制作业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("CONSTRAIN='Y'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }

            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }
        private void 步骤九可选列出反馈失败ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow[] drs = dt2.Select("LOCK_LOT like  '%反馈%'");
            DataTable dt = dt2.Clone();

            foreach (var row in drs)
            { dt.ImportRow(row); }

            dataGridView1.DataSource = dt;
            MessageBox.Show("Done! 数据_‘不支持表格一筛选排序统计'_命令");
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.Columns[0].Text == "PART" && listView1.Columns[1].Text == "LAYER")
            { DailyR2rReview(); }
            else if (listView1.Columns[1].Text == "Ppid" && listView1.Columns[0].Text == "Tool")
            { NikonVectorPlot(); }
            else if (listView1.Columns[4].Text == "Date")
            { AsmlVectorPlot(); }
            else
            { MessageBox.Show("当前不适用鼠标双击命令"); }

        }
        private void r2rQuery(string[] key)
        {
            //dtShow:r2r cd ovl, dt2:jobin dt1:distinct dtShow,part,layer,lotid
            ClearMemory();
            //MessageBox.Show(key[0]+","+key[1]+","+key[2]+","+key[3]+","+key[4]);
            DataRow[] drs;
            DataTable dt11 = dtShow.Clone();
            DataTable dt22 = dt2.Clone();
            if (key[4] == "ByLotID")
            {

                if (key[3] != "all")
                {
                    drs = dtShow.Select("PART='" + key[0] + "' and LAYER='" + key[1] + "' and LOTID='" + key[2] + "' and ITEM='" + key[3] + "'");
                }
                else
                {
                    drs = dtShow.Select("PART='" + key[0] + "' and LAYER='" + key[1] + "' and LOTID='" + key[2] + "' and ITEM <> 'DOSE'");
                }
            }
            else if (key[4] == "ByPartLayer")
            {
                if (key[3] != "all")
                {
                    drs = dtShow.Select("PART='" + key[0] + "' and LAYER='" + key[1] + "'  and ITEM='" + key[3] + "'");
                }
                else
                {
                    drs = dtShow.Select("PART='" + key[0] + "' and LAYER='" + key[1] + "' and ITEM <> 'DOSE'");
                }
            }
            else
            {
                drs = null;
            }

            foreach (var row in drs)
            { dt11.ImportRow(row); }
            dt11.DefaultView.Sort = "DCOLL_TIME ASC";

            dataGridView1.DataSource = dt11;

            dt22 = dt2.Clone();
            drs = dt2.Select("PART='" + key[0] + "' and LAYER='" + key[1] + "'");
            foreach (var row in drs)
            { dt22.ImportRow(row); }
            dataGridView2.DataSource = dt22;

            this.tabControl1.SelectedIndex = 0;



        }   //obselete called by form3

        private void 测试用_Click(object sender, EventArgs e)
        {
            //dt1 = LithoForm.LibF.GetDgvToTable(dataGridView2);

            DataTable dt = new DataTable();
            string[] col = new string[] {"Tech",
"PartID","Layer","AlignLayer","FeedbackOvl","ReservedOvl","ReservedPartOvl","ExpiredHoursOvl","FeedbackCd","ReservedCd","ReservedPartCd","R_Value","E.L_Value","TypeCD","ExpiredHoursCd","Pre_EqId",              "EqId","CD_Target","Dose_Value","Dose_Up","Dose_Low","Focus_Value","Focus_Up","Focus_Low","TrX_Value","TrX_Up","TrX_Low","TrY_Value","TrY_Up","TrY_Low","ExpX_Value","ExpX_Up","ExpX_Low","ExpY_Value","ExpY_Up","ExpY_Low","Ort_Value","Ort_Up","Ort_Low","Rot_Value","Rot_Up","Rot_Low","SMag_Value","SMag_Up","SMag_Low","SRot_Value","SRot_Up","SRot_Low","Lock/Unlock","Fix","Constrain","Pre_Layer_Name"};

            foreach (string x in col)
            { dt.Columns.Add(x); }

            for (int count = 0; count < dataGridView2.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                dr["PartID"] = Convert.ToString(dataGridView2.Rows[count].Cells[0].Value);
                dr["Layer"] = Convert.ToString(dataGridView2.Rows[count].Cells[1].Value);
                dr["EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[2].Value);
                dr["Dose_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[3].Value);
                dr["Focus_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[4].Value);
                dr["TrX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[5].Value);
                dr["TrY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[6].Value);
                dr["ExpX_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[7].Value);
                dr["ExpY_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[8].Value);
                dr["Ort_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[9].Value);
                dr["Rot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[10].Value);
                dr["SMag_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[11].Value);
                dr["SRot_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[12].Value);
                dr["CD_Target"] = Convert.ToString(dataGridView2.Rows[count].Cells[15].Value);
                dr["Pre_EqId"] = Convert.ToString(dataGridView2.Rows[count].Cells[16].Value);
                dr["Pre_Layer_Name"] = Convert.ToString(dataGridView2.Rows[count].Cells[17].Value);
                dr["FeedbackCd"] = Convert.ToString(dataGridView2.Rows[count].Cells[18].Value);
                dr["FeedbackOvl"] = Convert.ToString(dataGridView2.Rows[count].Cells[19].Value);
                dr["Constrain"] = Convert.ToString(dataGridView2.Rows[count].Cells[20].Value);
                dr["E.L_Value"] = Convert.ToString(dataGridView2.Rows[count].Cells[21].Value);
                dr["TypeCD"] = Convert.ToString(dataGridView2.Rows[count].Cells[22].Value);
                dr["Fix"] = Convert.ToString(dataGridView2.Rows[count].Cells[23].Value);
                dr["Lock/Unlock"] = Convert.ToString(dataGridView2.Rows[count].Cells[24].Value);


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            LithoForm.LibF.dataTableToCsv(dt2, "d:\\temp\\import.csv");
            MessageBox.Show("Save As D:\\TEMP\\IMPORT.CSV");
                    



        }

        private void 查询在线WIPJobinStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sourceFlag = radioButton2.Checked;
            dtShow = LithoForm.R2R.NikonAsmlQueryJobinStation(sourceFlag);
            dataGridView1.DataSource = dtShow;
            dataGridView2.DataSource = dtShow;

        }

        private void 查询JobinStation套刻补值是否异常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tool = Interaction.InputBox("按设备类型查询当前WIP是否有异常补值\r\n\r\n\r\n" +
               "查询前请先设定如下参数规范：\r\n" +
               "    FlagTran：  Grid Translation\r\n" +
               "    FlagEXP：   Grid Expansion\r\n" +
               "    FlagOrtRot：Grid Ort & Rot\r\n" +
               "    FlagShot:   Shot Mag/Rot/ASMag/AsRot\r\n\r\n\r\n" +
               "输入 N 查询Nikon设备\r\n\r\n" +
               "输入 A 查询ASML设备\r\n\r\n" +
               "输入其它字符，退出（重新设定规范）" +
               "", "定义设备类型", "", -1, -1);
            tool = tool.Trim().ToUpper();
            if (tool == "N" || tool == "A") {  }
            else { MessageBox.Show("Input Is Not 'A' OR 'N', EXIT"); return; }
            string s1, s2, s3, s4;
            s1 = textBox2.Text;  //tran
            s2 = textBox3.Text;  //exp
            s3 = textBox4.Text;  //rot,ort
            s4 = textBox5.Text;  //shot
 
            bool sourceFlag = radioButton2.Checked;
            dtShow=LithoForm.R2R.QueryAbnormalJobinstation(sourceFlag,tool, s1,s2, s3, s4);
            dataGridView1.DataSource = dtShow;
        }

       
    }
}
