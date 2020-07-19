using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//www.srcfans.com
namespace MyForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//获取指定目录中的所有文件
            var MyDir = @"C:\Windows";
            var MyInfo = MyDir + "目录下的所有文件包括：";
            foreach (string MyFile in System.IO.Directory.GetFiles(MyDir))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {//获取指定目录中的所有子目录
            var MyDir = @"C:\Windows";
            var MyInfo = MyDir + "目录中的所有子目录包括：";
            foreach (string MyFile in System.IO.Directory.GetDirectories(MyDir))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {//获取指定目录中的子目录和文件
            var MyDir = @"C:\Windows";
            var MyInfo = MyDir + "目录中的所有文件和子目录包括：";
            foreach (string MyFile in System.IO.Directory.GetFileSystemEntries(MyDir))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {//按条件过滤指定目录中的文件
            var MyDir = @"C:\Windows";
            var MyFilter = "*.exe";
            var MyInfo = MyDir + "目录下的所有" + MyFilter + "文件包括：";
            foreach (string MyFile in System.IO.Directory.GetFiles(MyDir, MyFilter))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {//按条件过滤指定子目录中的文件
            var MyDir = @"C:\Program Files\Microsoft SQL Server";
            var MyFilter = "*.dll";
            var MyInfo = MyDir + "目录下的所有" + MyFilter + "文件包括：";
            foreach (string MyFile in System.IO.Directory.GetFiles(MyDir, MyFilter, System.IO.SearchOption.AllDirectories))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button6_Click(object sender, EventArgs e)
        {//按条件过滤指定目录中的子目录
            var MyDir = @"C:\Program Files\Microsoft SQL Server";
            var MyFilter = "?0*.*";
            var MyInfo = MyDir + "目录下的所有" + MyFilter + "子目录包括：";
            foreach (string MyFile in System.IO.Directory.GetDirectories(MyDir, MyFilter))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button7_Click(object sender, EventArgs e)
        {//按条件过滤指定子目录中的子目录
            var MyDir = @"C:\Program Files\Microsoft SQL Server";
            var MyFilter = "?0*.*";
            var MyInfo = MyDir + "目录下的所有" + MyFilter + "子目录包括：";
            foreach (string MyFile in System.IO.Directory.GetDirectories(MyDir, MyFilter, System.IO.SearchOption.AllDirectories))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button8_Click(object sender, EventArgs e)
        {//过滤指定目录中的子目录和文件
            var MyDir = @"C:\Program Files\Microsoft Office";
            var MyFilter = "O*.*";
            var MyInfo = MyDir + "目录下的所有" + MyFilter + "子目录和文件包括：";
            foreach (string MyFile in System.IO.Directory.GetFileSystemEntries(MyDir, MyFilter))
                MyInfo += Environment.NewLine + MyFile;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button9_Click(object sender, EventArgs e)
        {//以不同的方式创建多层目录            
            if (!System.IO.Directory.Exists(@"C:\MyDir\MySubDir1"))
            {//方式一：使用静态方法Directory.CreateDirectory()
                System.IO.Directory.CreateDirectory(@"C:\MyDir\MySubDir1");
            }
            System.IO.DirectoryInfo MyInfo;
            if (!System.IO.Directory.Exists(@"C:\MyDir\MySubDir2"))
            {//方式二：使用DirectoryInfo.CreateSubdirectory()            
                MyInfo = new System.IO.DirectoryInfo(@"C:\MyDir");
                MyInfo.CreateSubdirectory("MySubDir2");
            }            
            if (!System.IO.Directory.Exists(@"C:\MyDir\MySubDir3"))
            {//方式三：使用DirectoryInfo.Create()
                MyInfo = new System.IO.DirectoryInfo(@"C:\MyDir\MySubDir3");
                MyInfo.Create();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {//以不同的方式删除多层目录
            //方式一：使用静态方法Directory.Delete()                
            System.IO.Directory.Delete(@"C:\MyDir\MySubDir1", true);
            //方式二：使用DirectoryInfo.Delete()
            var MyInfo = new System.IO.DirectoryInfo(@"C:\MyDir\MySubDir2");
            MyInfo.Delete(true);            
        }

        private void button11_Click(object sender, EventArgs e)
        {//使用文件对象复制多层目录
            var MySource = @"C:\inetpub";
            var MyTarget = @"F:\inetpub";
            var MyFile = new Scripting.FileSystemObjectClass();
            MyFile.CopyFolder(MySource, MyTarget, true);
            MessageBox.Show("复制操作成功完成！", "信息提示", MessageBoxButtons.OK);
        }

        private void button12_Click(object sender, EventArgs e)
        {//使用递归函数复制多层目录
            var MySource = @"C:\inetpub";
            var MyTarget = @"F:\inetpub";
            var MySourceInfo = new System.IO.DirectoryInfo(MySource);
            var MyTargetInfo = new System.IO.DirectoryInfo(MyTarget);
            CopyDirectory(MySourceInfo, MyTargetInfo);
            MessageBox.Show("复制操作成功完成！", "信息提示", MessageBoxButtons.OK);
        }
        public void CopyDirectory(System.IO.DirectoryInfo MySource, System.IO.DirectoryInfo MyTarget)
        {//递归复制文件及其目录                       
            var MyTargetPath = MyTarget.FullName + "\\" + MySource.Name;
            if (!System.IO.Directory.Exists(MyTargetPath))
                System.IO.Directory.CreateDirectory(MyTargetPath);
            System.IO.FileInfo[]  MyFiles = MySource.GetFiles();
            foreach(var MyFile in MyFiles)
                System.IO.File.Copy(MyFile.FullName, MyTargetPath + "\\" + MyFile.Name, true);
            System.IO.DirectoryInfo[] MySourceDirs= MySource.GetDirectories();
            foreach(var MySourceDir in MySourceDirs)
            {
                var MyTargetDir = new System.IO.DirectoryInfo(MyTargetPath);
                CopyDirectory(MySourceDir, MyTargetDir);
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {//增加指定目录的访问权限：NTFS 2003环境测试
            var MyDirName = "F:\\MyTempDir";
            var MyRight = System.Security.AccessControl.FileSystemRights.FullControl;
            var MyType = System.Security.AccessControl.AccessControlType.Allow;
            var MyAccount = "luobin\\Administrator";
            var MyDirInfo = new System.IO.DirectoryInfo(MyDirName);
            var MyDirSecurity = MyDirInfo.GetAccessControl();
            MyDirSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(MyAccount, MyRight, MyType));
            MyDirInfo.SetAccessControl(MyDirSecurity);
            MessageBox.Show("增加目录访问权限操作成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button14_Click(object sender, EventArgs e)
        {//移除指定目录的访问权限：NTFS 2003环境测试
            var MyDirName = "F:\\MyTempDir";
            var MyRight = System.Security.AccessControl.FileSystemRights.FullControl;
            var MyType = System.Security.AccessControl.AccessControlType.Allow;
            var MyAccount = "luobin\\Administrator";
            var MyDirInfo = new System.IO.DirectoryInfo(MyDirName);
            var MyDirSecurity = MyDirInfo.GetAccessControl();
            MyDirSecurity.RemoveAccessRule(new System.Security.AccessControl.FileSystemAccessRule(MyAccount, MyRight, MyType));
            MyDirInfo.SetAccessControl(MyDirSecurity);
            MessageBox.Show("移除目录访问权限操作成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button15_Click(object sender, EventArgs e)
        {//判断指定目录是否已经存在
            var MyDir1 = @"C:\WINDOWS\SYSTEM";
            String MyInfo = "";
            if (System.IO.Directory.Exists(MyDir1))
                MyInfo += MyDir1 + " 目录已经存在！\n";
            else
                MyInfo += MyDir1 + " 目录不存在！\n";            
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button16_Click(object sender, EventArgs e)
        {//获取指定目录的上级目录
            var MyDir = "C:\\windows\\System32";
            var MyParentDir = System.IO.Directory.GetParent(MyDir).FullName;
            var MyInfo = "当前目录是：" + MyDir;
            MyInfo += "\n上级目录是：" + MyParentDir;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button17_Click(object sender, EventArgs e)
        {//获取全路径名的目录信息
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyDirectoryName = System.IO.Path.GetDirectoryName(MyPathName);
            var MyInfo = "全路径文件名：" + MyPathName + "\n";
            MyInfo += "目录信息：" + MyDirectoryName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);           
        }

        private void button18_Click(object sender, EventArgs e)
        {//获取全路径名的根目录信息
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyInfo = "全路径文件名：" + MyPathName + "\n";
            var MyRootDirectoryName = System.IO.Path.GetPathRoot(MyPathName);
            MyInfo += "根目录信息：" + MyRootDirectoryName;
            //var MyRootDirectoryName = System.IO.Directory.GetDirectoryRoot(MyPathName);
            //MyInfo += "根目录信息：" + MyRootDirectoryName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);        
        }

        private void button19_Click(object sender, EventArgs e)
        {//获取当前工作目录
            var MyPath = "当前工作目录是：";
            MyPath += System.IO.Directory.GetCurrentDirectory();
            MessageBox.Show(MyPath, "信息提示", MessageBoxButtons.OK);    
        }
        private void button20_Click(object sender, EventArgs e)
        {//设置当前工作目录
            var MyInfo = "设置当前工作目录为：";
            var MyPath = "C:\\Windows";
            System.IO.Directory.SetCurrentDirectory(MyPath);
            MyInfo += MyPath + " 操作成功！";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);    
        }

        private void button21_Click(object sender, EventArgs e)
        {//获取和设置指定目录的时间
            var MyDirName = "F:\\Visual C# 2005 编程技巧大全";
            var MyInfo = MyDirName + "目录的时间信息如下：";
            MyInfo += "\n目录创建时间："+ System.IO.Directory.GetCreationTime(MyDirName).ToString();
            MyInfo += "\n目录访问时间：" + System.IO.Directory.GetLastAccessTime(MyDirName).ToString();
            MyInfo += "\n目录修改时间：" + System.IO.Directory.GetLastWriteTime(MyDirName).ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
            System.IO.Directory.SetCreationTime(MyDirName, DateTime.Now);
            System.IO.Directory.SetLastAccessTime(MyDirName, DateTime.Now);
            System.IO.Directory.SetLastWriteTime(MyDirName, DateTime.Now);
            MessageBox.Show("成功设置目录时间属性！", "信息提示", MessageBoxButtons.OK);            
        }

        private void button22_Click(object sender, EventArgs e)
        {//获取指定目录的属性
            var MyDirName = @"F:\MyTempDir";
            string MyInfo = MyDirName + "目录的属性信息如下：";
                var MyAttributes = System.IO.File.GetAttributes(MyDirName);
                if ((MyAttributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                    MyInfo += "\n只读属性为真；";
                if ((MyAttributes & System.IO.FileAttributes.System) == System.IO.FileAttributes.System)
                    MyInfo += "\n系统属性为真；";
                if ((MyAttributes & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden)
                    MyInfo += "\n隐藏属性为真；";
                if ((MyAttributes & System.IO.FileAttributes.Archive) == System.IO.FileAttributes.Archive)
                    MyInfo += "\n归档属性为真；";
                MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button23_Click(object sender, EventArgs e)
        {//设置指定目录的属性
            var MyDirName = @"F:\MyTempDir";
            System.IO.FileAttributes MyAttributes;
            System.IO.File.SetAttributes(MyDirName, System.IO.FileAttributes.Normal);
            MyAttributes = System.IO.File.GetAttributes(MyDirName);
            System.IO.File.SetAttributes(MyDirName, MyAttributes | System.IO.FileAttributes.ReadOnly);
            MyAttributes = System.IO.File.GetAttributes(MyDirName);
            System.IO.File.SetAttributes(MyDirName, MyAttributes | System.IO.FileAttributes.System);
            MyAttributes = System.IO.File.GetAttributes(MyDirName);
            System.IO.File.SetAttributes(MyDirName, MyAttributes | System.IO.FileAttributes.Hidden);
            MyAttributes = System.IO.File.GetAttributes(MyDirName);
            System.IO.File.SetAttributes(MyDirName, MyAttributes | System.IO.FileAttributes.Archive);
            MyAttributes = System.IO.File.GetAttributes(MyDirName);
            MessageBox.Show("成功设置目录属性！", "信息提示", MessageBoxButtons.OK);
        }
        private void button24_Click(object sender, EventArgs e)
        {//取消指定目录的属性
            var MyDirName = @"F:\MyTempDir";
            System.IO.File.SetAttributes(MyDirName, System.IO.FileAttributes.Normal);
            MessageBox.Show("成功取消目录属性！", "信息提示", MessageBoxButtons.OK);
        }

        private void button25_Click(object sender, EventArgs e)
        {//获取启动程序的执行文件目录
            var MyInfo = "启动了应用程序的可执行文件的目录是：\n" + Application.StartupPath;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button26_Click(object sender, EventArgs e)
        {//获取启动程序的执行文件路径
            var MyInfo = "启动了应用程序的可执行文件的路径是：\n" + Application.ExecutablePath;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);   
        }

        private void button27_Click(object sender, EventArgs e)
        {//去掉全路径名的扩展名和路径
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyFileName = System.IO.Path.GetFileNameWithoutExtension(MyPathName);
            var MyInfo = "全路径文件名：" + MyPathName + "\n";
            MyInfo += "无路径和扩展名的文件名：" + MyFileName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);       
        }

        private void button28_Click(object sender, EventArgs e)
        {//获取全路径名的扩展名信息
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyExtensionName = System.IO.Path.GetExtension(MyPathName);
            var MyInfo = "全路径文件名：" + MyPathName + "\n";
            MyInfo += "扩展名信息：" + MyExtensionName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK); 
        }

        private void button29_Click(object sender, EventArgs e)
        {//去掉全路径名的路径信息
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyFileName = System.IO.Path.GetFileName(MyPathName);
            var MyInfo = "全路径文件名：" + MyPathName + "\n";
            MyInfo += "无路径文件名：" + MyFileName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);   
        }

        private void button30_Click(object sender, EventArgs e)
        {//合并两个包含路径的字符串
            var MyPathName = "C:\\Windows\\Notepad.exe";
            var MyNewPath = "F:\\";
            var MyFileName = System.IO.Path.GetFileName(MyPathName);
            var MyDestName = System.IO.Path.Combine(MyNewPath, MyFileName);
            var MyInfo = "源文件名：" + MyPathName + "\n";
            MyInfo += "目标文件名：" + MyDestName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);   
        }

        private void button31_Click(object sender, EventArgs e)
        {//获取路径名禁止使用的字符
            char[] MyChars = System.IO.Path.GetInvalidPathChars();
            var MyInfo = "路径名禁止使用字符包括：\n";
            foreach (char MyChar in MyChars)
                MyInfo += MyChar.ToString() + "\n";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);   
        }

        private void button32_Click(object sender, EventArgs e)
        {//更改指定文件的扩展名
            var MyOldFileName = @"C:\atlog.txt";
            var MyResult = System.IO.Path.ChangeExtension(MyOldFileName, ".dat");
            var MyInfo = String.Format("成功更改文件扩展名：{0} 为： {1}", MyOldFileName, MyResult);
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);    
        }

        private void button33_Click(object sender, EventArgs e)
        {//以不同的方式更名文件
            //复制测试用文件
            System.IO.File.Copy("C:\\Windows\\Notepad.exe", "C:\\Notepad.exe", true);
            //方式一：使用静态方法File.Copy()
            System.IO.File.Copy("C:\\Notepad.exe", "C:\\NotepadTest1.exe", true);
            //方式二：使用FileInfo.MoveTo() 
            var MyInfo = new System.IO.FileInfo("C:\\Notepad.exe");
            if (System.IO.File.Exists("C:\\NotepadTest2.exe"))
                System.IO.File.Delete("C:\\NotepadTest2.exe");
            MyInfo.MoveTo("C:\\NotepadTest2.exe");
            //复制测试用文件
            System.IO.File.Copy("C:\\Windows\\Notepad.exe", "C:\\Notepad.exe", true);
            //方式三：使用静态方法File.Move()
            if (System.IO.File.Exists("C:\\NotepadTest3.exe"))
                System.IO.File.Delete("C:\\NotepadTest3.exe");
            System.IO.File.Move("C:\\Notepad.exe", "C:\\NotepadTest3.exe");
            MessageBox.Show("使用三种方式更名文件操作成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button34_Click(object sender, EventArgs e)
        {//以不同的方式复制文件
            //方式一：使用静态方法File.Copy()
            System.IO.File.Copy("C:\\Windows\\Notepad.exe", "F:\\MyNotepad.exe", true);
            //方式二：使用FileInfo.CopyTo() 
            var MyInfo = new System.IO.FileInfo("C:\\Windows\\Notepad.exe");
            MyInfo.CopyTo("F:\\Notepad.exe", true);
            MessageBox.Show("使用两种方式复制文件操作成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button35_Click(object sender, EventArgs e)
        {//以不同的方式删除文件
            //复制测试用文件
            System.IO.File.Copy("C:\\Windows\\Notepad.exe", "C:\\MyNotepad.exe", true);
            System.IO.File.Copy("C:\\Windows\\Notepad.exe", "C:\\Notepad.exe", true);
            //方式一：使用静态方法File.Delete()
            if (System.IO.File.Exists("C:\\MyNotepad.exe"))
                System.IO.File.Delete("C:\\MyNotepad.exe");
            //方式二：使用FileInfo.Delete() 
            var MyInfo = new System.IO.FileInfo("C:\\Notepad.exe");
            if (System.IO.File.Exists("C:\\Notepad.exe"))
                MyInfo.Delete();
            MessageBox.Show("使用两种方式删除文件操作成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button36_Click(object sender, EventArgs e)
        {//获取指定文件的尺寸大小
            var MyFileName = "C:\\Windows\\NOTEPAD.exe";
            var MyFileInfo = new System.IO.FileInfo(MyFileName);
            var MyInfo = MyFileName + "文件共有：" + MyFileInfo.Length.ToString() + "字节";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button37_Click(object sender, EventArgs e)
        {//计算多层文件夹的总尺寸
            var MyFolder = @"C:\Program Files\Microsoft SQL Server";
            var MyDir = new System.IO.DirectoryInfo(MyFolder);
            var MyInfo = MyFolder + "文件夹的大小是：" + CalculateDirectorySize(MyDir, true).ToString() + "字节。";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);   
        }
        public static long CalculateDirectorySize(System.IO.DirectoryInfo MyDirectory,bool IsSubDirectories)
        {
            long MySize = 0;
            //检查包含的所有文件
            System.IO.FileInfo[] MyFiles =MyDirectory.GetFiles();
            foreach (var MyFile in MyFiles)
                MySize += MyFile.Length;
            //检查包含的所有子文件夹
            if (IsSubDirectories)
            {
                System.IO.DirectoryInfo[] MyDirs = MyDirectory.GetDirectories();
                foreach (var MyDir in MyDirs)
                    MySize += CalculateDirectorySize(MyDir, true);
            }
            return MySize;
        }

        private void button38_Click(object sender, EventArgs e)
        {//获取文件名禁止使用的字符
            char[] MyChars = System.IO.Path.GetInvalidFileNameChars();
            var MyInfo = "文件名禁止使用字符包括：\n";
            foreach (char MyChar in MyChars)
                MyInfo += MyChar.ToString() + "\n";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);               
        }

        private void button39_Click(object sender, EventArgs e)
        {//将长文件名转换成短文件名
            var MyLongName = "F:\\Visual C# 2005 编程技巧大全\\Visual C# 2005 编程技巧大全.doc";
            if (!System.IO.File.Exists(MyLongName))
            {
                MessageBox.Show(MyLongName + "文件不存在！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var MyShortName = new System.Text.StringBuilder(256);
            GetShortPathName(MyLongName, MyShortName, 256);
            var MyInfo = "\n长文件名：" + MyLongName;
            MyInfo += "\n短文件名：" + MyShortName;
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        private static extern Int16 GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, Int16 cchBuffer);

        private void button40_Click(object sender, EventArgs e)
        {//获取和设置指定文件的时间
            var MyFileName = "C:\\atlog.txt";
            var MyFileInfo = new System.IO.FileInfo(MyFileName);
            var MyInfo = MyFileName + "文件的时间信息如下：";
            MyInfo += "\n文件创建时间：" + MyFileInfo.CreationTime.ToString();
            MyInfo += "\n文件访问时间：" + MyFileInfo.LastAccessTime.ToString();
            MyInfo += "\n文件修改时间：" + MyFileInfo.LastWriteTime.ToString();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
            MyFileInfo.CreationTime = DateTime.Now;
            MyFileInfo.LastAccessTime = DateTime.Now;
            MyFileInfo.LastWriteTime = DateTime.Now;
            MessageBox.Show("成功设置文件时间属性！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button41_Click(object sender, EventArgs e)
        {//设置文件属性
            var MyFile = new System.IO.FileInfo("C:\\atlog.txt");
            //只读属性
            MyFile.Attributes = MyFile.Attributes | System.IO.FileAttributes.ReadOnly;
            //隐藏属性
            MyFile.Attributes = MyFile.Attributes | System.IO.FileAttributes.Hidden;
            //归档属性
            MyFile.Attributes = MyFile.Attributes | System.IO.FileAttributes.Archive;
        }
        private void button42_Click(object sender, EventArgs e)
        {//取消文件属性
            var MyFile =new System.IO.FileInfo("C:\\atlog.txt");
            //只读属性
            MyFile.Attributes = MyFile.Attributes & ~System.IO.FileAttributes.ReadOnly;
            //隐藏属性
            MyFile.Attributes = MyFile.Attributes & ~System.IO.FileAttributes.Hidden;
            //归档属性
            MyFile.Attributes = MyFile.Attributes & ~System.IO.FileAttributes.Archive;
        }
        private void button43_Click(object sender, EventArgs e)
        {//获取文件属性
            var MyFileName = "C:\\atlog.txt";
            var MyFile = new System.IO.FileInfo(MyFileName);
            var MyInfo = MyFileName + "文件属性信息如下：" + "\n";
            if ((MyFile.Attributes & System.IO.FileAttributes.ReadOnly) ==
         System.IO.FileAttributes.ReadOnly)
                MyInfo += "只读属性：真" + "\n";
            if ((MyFile.Attributes & System.IO.FileAttributes.Hidden) ==
         System.IO.FileAttributes.Hidden)
                MyInfo += "隐藏属性：真" + "\n";
            if ((MyFile.Attributes & System.IO.FileAttributes.Archive) ==
         System.IO.FileAttributes.Archive)
                MyInfo += "归档属性：真" + "\n";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button44_Click(object sender, EventArgs e)
        {//判断指定文件是否已经存在
            var MyFile = "C:\\atlog.txt";
            var MyInfo = "";
            if (System.IO.File.Exists(MyFile))
                MyInfo += MyFile+ " 文件已经存在！\n";
            else
                MyInfo += MyFile + " 文件不存在！\n";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button45_Click(object sender, EventArgs e)
        {//比较两个文件内容是否相同
            var MyFileName1 = "Compare1.txt";
            var MyFileName2 = "Compare2.txt";
            var MyStream1 = new System.IO.FileStream(MyFileName1, System.IO.FileMode.Open);
            var MyStream2 = new System.IO.FileStream(MyFileName2, System.IO.FileMode.Open);
            var MyCount1 = 0;
            var MyCount2 = 0;
            //逐一比较两个文件的每一个字节组，直到文件结束或有不相同的地方为止
            do
            {//从文件中读取一个字节组
               MyCount1 = MyStream1.ReadByte();
               MyCount2 = MyStream2.ReadByte();
            } while ((MyCount1 == MyCount2) && (MyCount1 != -1));
            if ((MyCount1 == MyCount2) && (MyCount1 != 0))
               MessageBox.Show(MyFileName1 + "和" + MyFileName2 + "的内容完全相同！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
               MessageBox.Show(MyFileName1 + "和" + MyFileName2 + "的内容不相同！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MyStream1.Close();
            MyStream2.Close();            
        }

        private void button46_Click(object sender, EventArgs e)
        {//比较两个文件是否完全相等
            var MyFile1 = "MyText1.txt";
            var MyFile2 = "MyText2.txt";
            var MyHash = System.Security.Cryptography.HashAlgorithm.Create();
            //计算第一个文件的哈希值
            var MyStream1 = new System.IO.FileStream(MyFile1, System.IO.FileMode.Open);
            byte[] MyHashBytes1 = MyHash.ComputeHash(MyStream1);
            MyStream1.Close();
            //计算第二个文件的哈希值
            var MyStream2 = new System.IO.FileStream(MyFile2, System.IO.FileMode.Open);
            byte[] MyHashBytes2 = MyHash.ComputeHash(MyStream2);
            MyStream2.Close();
            //比较两个哈希值
            if (BitConverter.ToString(MyHashBytes1)== BitConverter.ToString(MyHashBytes2))
                MessageBox.Show("两个文件相等。", "信息提示", MessageBoxButtons.OK);
            else
                MessageBox.Show("两个文件不相等。", "信息提示", MessageBoxButtons.OK);
       }

        private void button47_Click(object sender, EventArgs e)
        {//使用缓冲流快速复制文件
            var MySourceFile = "F:\\Northwind.mdb";
            var MyTargetFile = "C:\\NW.mdb";
            byte[] MyBuffer = new byte[1024];
            int MyBytesRead;
            //创建二进制流
            var MyInputStream = System.IO.File.OpenRead(MySourceFile);
            var MyOutputStream = System.IO.File.OpenWrite(MyTargetFile);
            //创建二进制缓冲流
            var MyBufferedInput = new System.IO.BufferedStream(MyInputStream);
            var MyBufferedOutput = new System.IO.BufferedStream(MyOutputStream);
            MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 1024);
            while (MyBytesRead > 0)
            {
              MyBufferedOutput.Write(MyBuffer, 0, MyBytesRead);
              MyBytesRead = MyBufferedInput.Read(MyBuffer, 0, 1024);
            }
            MyBufferedOutput.Flush();
            MyBufferedInput.Close();
            MyBufferedOutput.Close();
            MessageBox.Show("复制文件操作完成！", "信息提示", MessageBoxButtons.OK);            
        }

        private void button48_Click(object sender, EventArgs e)
        {//读取和写入独立存储文件
            var MyStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly();
            //在独立存储中创建一个文件夹
            MyStore.CreateDirectory("MyFolder");
            //创建独立存储文件
            var MyStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("MyFile1.txt", System.IO.FileMode.Create, MyStore);
            //var MyStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("MyFile2.txt", System.IO.FileMode.Create, MyStore);
            //var MyStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("MyFile3.txt", System.IO.FileMode.Create, MyStore);
            //var MyStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("MyFile4.txt", System.IO.FileMode.Create, MyStore);
            //var MyStream = new System.IO.IsolatedStorage.IsolatedStorageFileStream("MyFile5.txt", System.IO.FileMode.Create, MyStore);
            var MyWriter = new System.IO.StreamWriter(MyStream, Encoding.UTF8);
            MyWriter.WriteLine("C:\\文件夹中的文件包括：");
            foreach (string MyFileName in System.IO.Directory.GetFiles("C:\\"))
                MyWriter.WriteLine(MyFileName);
            MyWriter.Flush();
            MyWriter.Close();
            MyStream.Close();
            var MyInfo = "";
            //独立存储区中的信息
            MyInfo += "当前尺寸: " + MyStore.CurrentSize.ToString() + "\n";
            MyInfo += "存储区范围: " + MyStore.Scope.ToString() + "\n";
            MyInfo += "包含的文件有:\n";
            string[] MyFiles = MyStore.GetFileNames("*.*");
            foreach (var MyFile in MyFiles)
                MyInfo += MyFile + "\n";
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button49_Click(object sender, EventArgs e)
        {//读取和写入一个文本文件
            //创建(写入)一个文本文件
            var MyStream = new System.IO.FileStream("MyText.txt", System.IO.FileMode.Create);
            var MyWriter = new System.IO.StreamWriter(MyStream, Encoding.UTF8);
            MyWriter.WriteLine("C:\\文件夹中的文件包括：");
            foreach (var MyFileName in System.IO.Directory.GetFiles("C:\\"))
                MyWriter.WriteLine(MyFileName);
            MyWriter.Flush();
            MyWriter.Close();
            MyStream.Close();
            //读取一个文本文件            
            var MyReader = new System.IO.StreamReader("MyText.txt", Encoding.UTF8);
            string MyInfo = MyReader.ReadToEnd();
            MyReader.Close();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

        private void button50_Click(object sender, EventArgs e)
        {//读取和写入一个二进制文件
            //创建(写入)一个二进制文件
            var MyStream = new System.IO.FileStream("MyBinary.dat", System.IO.FileMode.Create);
            var MyWriter = new System.IO.BinaryWriter(MyStream);
            for (var i = 0; i < 31; i++)
                MyWriter.Write((int)i);
            MyWriter.Close();
            MyStream.Close();
            //读取一个二进制文件
            MyStream = new System.IO.FileStream("MyBinary.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var MyReader = new System.IO.BinaryReader(MyStream);
            var MyInfo = "";
            for (int i = 0; i < 31; i++)
                MyInfo += MyReader.ReadInt32() + "\n";
            MyReader.Close();
            MyStream.Close();
            MessageBox.Show(MyInfo, "信息提示", MessageBoxButtons.OK);
        }

    }
}
