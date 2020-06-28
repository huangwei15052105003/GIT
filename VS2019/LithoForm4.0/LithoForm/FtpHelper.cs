using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;







namespace LithoForm
{


    public class FtpHelper
    {
        #region public library
        //基本设置
        //private static string ftppath = @"ftp://" + "192.168.1.1" + "/";
        //private static string username = "username";
        //private static string password = "password";

        public static string ftppath = @"ftp://" + "192.168.1.1" + "/";
        public static string username = "username";
        public static string password = "password";




        //获取FTP上面的文件夹和文件
        public static string[] GetFolderAndFileList(string s)
        {
            string[] getfolderandfilelist;
            FtpWebRequest request;
            StringBuilder sb = new StringBuilder();
            try
            {
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.UseBinary = true;
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    sb.Append(line);
                    sb.Append("\n");
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                sb.Remove(sb.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return sb.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取FTP上面的文件夹和文件：" + ex.Message);
                getfolderandfilelist = null;
                return getfolderandfilelist;
            }
        }

        //获取FTP上面的文件大小
        public static int GetFileSize(string fileName)
        {
            FtpWebRequest request;
            try
            {
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath + fileName));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                int n = (int)request.GetResponse().ContentLength;
                return n;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取FTP上面的文件大小：" + ex.Message);
                return -1;
            }
        }

        //FTP上传文件
        public static void FileUpLoad(string filePath, string objPath = "")
        {
            try
            {
                string url = ftppath;
                if (objPath != "")
                    url += objPath + "/";
                try
                {
                    FtpWebRequest request = null;
                    try
                    {
                        FileInfo fi = new FileInfo(filePath);
                        using (FileStream fs = fi.OpenRead())
                        {
                            request = (FtpWebRequest)FtpWebRequest.Create(new Uri(url + fi.Name));
                            request.Credentials = new NetworkCredential(username, password);
                            request.KeepAlive = false;
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            request.UseBinary = true;
                            using (Stream stream = request.GetRequestStream())
                            {
                                int bufferLength = 5120;
                                byte[] buffer = new byte[bufferLength];
                                int i;
                                while ((i = fs.Read(buffer, 0, bufferLength)) > 0)
                                {
                                    stream.Write(buffer, 0, i);
                                }
                                Console.WriteLine("FTP上传文件succesful");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FTP上传文件：" + ex.Message);
                    }
                    finally
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FTP上传文件：" + ex.Message);
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP上传文件：" + ex.Message);
            }
        }

        //FTP下载文件 
        public static void FileDownLoad(string fileName)
        {
            FtpWebRequest request;
            try
            {
                string downloadPath = @"D:";
                FileStream fs = new FileStream(downloadPath + "\\" + fileName, FileMode.Create);
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftppath + fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                int bufferLength = 5120;
                int i;
                byte[] buffer = new byte[bufferLength];
                i = stream.Read(buffer, 0, bufferLength);
                while (i > 0)
                {
                    fs.Write(buffer, 0, i);
                    i = stream.Read(buffer, 0, bufferLength);
                }
                stream.Close();
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP下载文件：" + ex.Message);
            }
        }

        //FTP删除文件
        public static void FileDelete(string fileName)
        {
            try
            {
                string uri = ftppath + fileName;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP删除文件：" + ex.Message);
            }
        }

        //FTP新建目录，上一级须先存在
        public static void MakeDir(string dirName)
        {
            try
            {
                string uri = ftppath + dirName;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP新建目录：" + ex.Message);
            }
        }

        //FTP删除目录，上一级须先存在
        public static void DelDir(string dirName)
        {
            try
            {
                string uri = ftppath + dirName;
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(username, password);
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP删除目录：" + ex.Message);
            }
        }

        #endregion

    }
    
    public class Insert_Standard_ErrorLog

    {

        public static void Insert(string x, string y)

        {



        }

    }
    public class AsmlFtp
    {
        public static Dictionary<string, string[]> ipUser = new Dictionary<string, string[]>() {
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


    }
    
   

    public class HitachiFtpWeb
    {
       public static string ftpServerIP;
        public static string ftpRemotePath;
        public static string ftpUserID;
        public static string ftpPassword;
        public static string ftpURI;
               
        public HitachiFtpWeb(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP +"/" + ftpRemotePath + "/";
        }

        public void Upload(string filename)

        {

            FileInfo fileInf = new FileInfo(filename);

            string uri = ftpURI + fileInf.Name;

            FtpWebRequest reqFTP;



            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            reqFTP.KeepAlive = false;

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.UseBinary = true;

            reqFTP.ContentLength = fileInf.Length;

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            FileStream fs = fileInf.OpenRead();

            try

            {

                Stream strm = reqFTP.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)

                {

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                strm.Close();

                fs.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);

            }

        }

        public void Download(string filePath, string fileName)

        {

            FtpWebRequest reqFTP;

            try

            {

                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);



                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));

                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();

                long cl = response.ContentLength;

                int bufferSize = 2048;

                int readCount;

                byte[] buffer = new byte[bufferSize];



                readCount = ftpStream.Read(buffer, 0, bufferSize);

                while (readCount > 0)

                {

                    outputStream.Write(buffer, 0, readCount);

                    readCount = ftpStream.Read(buffer, 0, bufferSize);

                }



                ftpStream.Close();

                outputStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "Download Error --> " + ex.Message);

            }

        }

        public void Delete(string fileName)

        {

            try

            {

                string uri = ftpURI + fileName;

              //  MessageBox.Show(uri);

                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));



                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.KeepAlive = false;

                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;



                string result = String.Empty;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();

                StreamReader sr = new StreamReader(datastream);

                result = sr.ReadToEnd();

                sr.Close();

                datastream.Close();

                response.Close();

            }

            catch (Exception ex)

            {
               // MessageBox.Show(fileName + ",   "+ex.Message);
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + fileName);

            }

        }

        public void RemoveDirectory(string folderName)  //全空

        {

            try

            {

                string uri = ftpURI + folderName;

                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));



                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.KeepAlive = false;

                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;



                string result = String.Empty;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();

                StreamReader sr = new StreamReader(datastream);

                result = sr.ReadToEnd();

                sr.Close();

                datastream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + folderName);

            }

        }

        public string[] GetFilesDetailList(string subFolder)
            
        {            
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI+subFolder));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }

            catch (Exception ex)
            {
                downloadFiles = null;
              //  Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFilesDetailList Error --> " + ex.Message);
                return downloadFiles;
            }

        }
        public string[] GetFileList(string mask)

        {

            string[] downloadFiles;

            StringBuilder result = new StringBuilder();

            FtpWebRequest reqFTP;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = reqFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);



                string line = reader.ReadLine();

                while (line != null)

                {

                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")

                    {



                        string mask_ = mask.Substring(0, mask.IndexOf("*"));

                        if (line.Substring(0, mask_.Length) == mask_)

                        {

                            result.Append(line);

                            result.Append("\n");

                        }

                    }

                    else

                    {

                        result.Append(line);

                        result.Append("\n");

                    }

                    line = reader.ReadLine();

                }

                result.Remove(result.ToString().LastIndexOf('\n'), 1);

                reader.Close();

                response.Close();

                return result.ToString().Split('\n');

            }

            catch (Exception ex)

            {

                downloadFiles = null;

                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")

                {

                    Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileList Error --> " + ex.Message.ToString());

                }

                return downloadFiles;

            }

        }

        
        public string[] GetDirectoryList(string subFolder)

        {

            string[] drectory = GetFilesDetailList(subFolder);

            string m = string.Empty;

            foreach (string str in drectory)

            {

                int dirPos = str.IndexOf("<DIR>");

                if (dirPos > 0)

                {

                    /*判断 Windows 风格*/

                    m += str.Substring(dirPos + 5).Trim() + "\n";

                }

                else if (str.Trim().Substring(0, 1).ToUpper() == "D")

                {

                    /*判断 Unix 风格*/

                    string dir = str.Substring(54).Trim();

                    if (dir != "." && dir != "..")

                    {

                        m += dir + "\n";

                    }

                }

            }



            char[] n = new char[] { '\n' };

            return m.Split(n);

        }
     


        public bool DirectoryExist(string RemoteDirectoryName,string subFolder)

        {

            string[] dirList = GetDirectoryList(subFolder);

            foreach (string str in dirList)

            {

                if (str.Trim() == RemoteDirectoryName.Trim())

                {

                    return true;

                }

            }

            return false;

        }



        public bool FileExist(string RemoteFileName)

        {

            string[] fileList = GetFileList("*.*");

            foreach (string str in fileList)

            {

                if (str.Trim() == RemoteFileName.Trim())

                {

                    return true;

                }

            }

            return false;

        }



        public void MakeDir(string dirName)

        {

            FtpWebRequest reqFTP;

            try

            {

                // dirName = name of the directory to create.

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));

                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "MakeDir Error --> " + ex.Message);

            }

        }


        public long GetFileSize(string filename)

        {

            FtpWebRequest reqFTP;

            long fileSize = 0;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));

                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();

                fileSize = response.ContentLength;



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileSize Error --> " + ex.Message);

            }

            return fileSize;

        }


        public void ReName(string currentFilename, string newFilename)

        {

            FtpWebRequest reqFTP;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));

                reqFTP.Method = WebRequestMethods.Ftp.Rename;

                reqFTP.RenameTo = newFilename;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "ReName Error --> " + ex.Message);

            }

        }



        public void MovieFile(string currentFilename, string newDirectory)

        {

            ReName(currentFilename, newDirectory);

        }



        public void GotoDirectory(string DirectoryName, bool IsRoot)

        {

            if (IsRoot)

            {

                ftpRemotePath = DirectoryName;

            }

            else

            {

                ftpRemotePath += DirectoryName + "/";

            }

            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";

        }


        //如下命令无效
        public static void DeleteOrderDirectory(string ftpServerIP, string folderToDelete, string ftpUserID, string ftpPassword)

        {

            try
            {

                if (!string.IsNullOrEmpty(ftpServerIP) && !string.IsNullOrEmpty(folderToDelete) && !string.IsNullOrEmpty(ftpUserID) && !string.IsNullOrEmpty(ftpPassword))

                {

                    HitachiFtpWeb fw = new HitachiFtpWeb(ftpServerIP, folderToDelete, ftpUserID, ftpPassword);

                    //进入订单目录

                    fw.GotoDirectory(folderToDelete, true);

                    //获取规格目录

                    string[] folders = fw.GetDirectoryList("dummy");

                    foreach (string folder in folders)

                    {

                        if (!string.IsNullOrEmpty(folder) || folder != "")

                        {

                            //进入订单目录

                            string subFolder = folderToDelete + "/" + folder;

                            fw.GotoDirectory(subFolder, true);

                            //获取文件列表

                            string[] files = fw.GetFileList("*.*");

                            if (files != null)

                            {

                                //删除文件

                                foreach (string file in files)

                                {

                                    fw.Delete(file);

                                }

                            }

                            //删除冲印规格文件夹

                            fw.GotoDirectory(folderToDelete, true);

                            fw.RemoveDirectory(folder);

                        }

                    }



                    //删除订单文件夹

                    string parentFolder = folderToDelete.Remove(folderToDelete.LastIndexOf('/'));

                    string orderFolder = folderToDelete.Substring(folderToDelete.LastIndexOf('/') + 1);

                    fw.GotoDirectory(parentFolder, true);

                    fw.RemoveDirectory(orderFolder);

                }

                else

                {

                    throw new Exception("FTP 及路径不能为空！");

                }

            }

            catch (Exception ex)

            {

                throw new Exception("删除订单时发生错误，错误信息为：" + ex.Message);

            }

        }

    }

    public class AsmlFtpWeb
    {
        public static string ftpServerIP;
        public static string ftpRemotePath;
        public static string ftpUserID;
        public static string ftpPassword;
        public static string ftpURI;

        public AsmlFtpWeb(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }

        public bool Upload(string filename)

        {

            FileInfo fileInf = new FileInfo(filename);

            string uri = ftpURI + fileInf.Name;

            FtpWebRequest reqFTP;

           // MessageBox.Show(ftpURI + "\n\n" + ftpURI + fileInf.Name);
            //return;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
          
            reqFTP.KeepAlive = false;

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.UseBinary = true;

            reqFTP.ContentLength = fileInf.Length;

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            FileStream fs = fileInf.OpenRead();
            
            try

            {
              
                Stream strm = reqFTP.GetRequestStream();
               
                contentLen = fs.Read(buff, 0, buffLength);
              
                while (contentLen != 0)

                {

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                strm.Close();

                fs.Close();
                return true;
            }

            catch (Exception ex)

            {
                //  MessageBox.Show(ex.Message);
                //  Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);

                return false;

            }

        }

        public void Download(string filePath, string fileName)
        {            
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               
                Stream ftpStream = response.GetResponseStream();             
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);             
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                ;
               // MessageBox.Show(ex.Message);
               // Insert_Standard_ErrorLog.Insert("FtpWeb", "Download Error --> " + ex.Message);               
            }  

        }

        public void Delete(string fileName)

        {

            try

            {

                string uri = ftpURI + fileName;

                //  MessageBox.Show(uri);

                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));



                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.KeepAlive = false;

                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;



                string result = String.Empty;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();

                StreamReader sr = new StreamReader(datastream);

                result = sr.ReadToEnd();

                sr.Close();

                datastream.Close();

                response.Close();

            }

            catch (Exception ex)

            {
                ;
                //  Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + fileName);

            }

        }

        public void RemoveDirectory(string folderName)  //全空

        {

            try

            {

                string uri = ftpURI + folderName;

                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));



                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.KeepAlive = false;

                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;



                string result = String.Empty;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                long size = response.ContentLength;

                Stream datastream = response.GetResponseStream();

                StreamReader sr = new StreamReader(datastream);

                result = sr.ReadToEnd();

                sr.Close();

                datastream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + folderName);

            }

        }

        public string[] GetFilesDetailList(string subFolder)

        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + subFolder));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }

            catch (Exception ex)
            {
                downloadFiles = null;
                //  Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFilesDetailList Error --> " + ex.Message);
                return downloadFiles;
            }

        }
        public string[] GetFileList(string mask)

        {

            string[] downloadFiles;

            StringBuilder result = new StringBuilder();

            FtpWebRequest reqFTP;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = reqFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);



                string line = reader.ReadLine();

                while (line != null)

                {

                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")

                    {



                        string mask_ = mask.Substring(0, mask.IndexOf("*"));

                        if (line.Substring(0, mask_.Length) == mask_)

                        {

                            result.Append(line);

                            result.Append("\n");

                        }

                    }

                    else

                    {

                        result.Append(line);

                        result.Append("\n");

                    }

                    line = reader.ReadLine();

                }

                result.Remove(result.ToString().LastIndexOf('\n'), 1);

                reader.Close();

                response.Close();

                return result.ToString().Split('\n');

            }

            catch (Exception ex)

            {

                downloadFiles = null;

                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")

                {

                    Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileList Error --> " + ex.Message.ToString());

                }

                return downloadFiles;

            }

        }


        public string[] GetDirectoryList(string subFolder)

        {

            string[] drectory = GetFilesDetailList(subFolder);

            string m = string.Empty;

            foreach (string str in drectory)

            {

                int dirPos = str.IndexOf("<DIR>");

                if (dirPos > 0)

                {

                    /*判断 Windows 风格*/

                    m += str.Substring(dirPos + 5).Trim() + "\n";

                }

                else if (str.Trim().Substring(0, 1).ToUpper() == "D")

                {

                    /*判断 Unix 风格*/

                    string dir = str.Substring(54).Trim();

                    if (dir != "." && dir != "..")

                    {

                        m += dir + "\n";

                    }

                }

            }



            char[] n = new char[] { '\n' };

            return m.Split(n);

        }



        public bool DirectoryExist(string RemoteDirectoryName, string subFolder)

        {

            string[] dirList = GetDirectoryList(subFolder);

            foreach (string str in dirList)

            {

                if (str.Trim() == RemoteDirectoryName.Trim())

                {

                    return true;

                }

            }

            return false;

        }



        public bool FileExist(string RemoteFileName)

        {

            string[] fileList = GetFileList("*.*");

            foreach (string str in fileList)

            {

                if (str.Trim() == RemoteFileName.Trim())

                {

                    return true;

                }

            }

            return false;

        }



        public void MakeDir(string dirName)

        {

            FtpWebRequest reqFTP;

            try

            {

                // dirName = name of the directory to create.

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));

                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "MakeDir Error --> " + ex.Message);

            }

        }


        public long GetFileSize(string filename)

        {

            FtpWebRequest reqFTP;

            long fileSize = 0;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));

                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();

                fileSize = response.ContentLength;



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileSize Error --> " + ex.Message);

            }

            return fileSize;

        }


        public void ReName(string currentFilename, string newFilename)

        {

            FtpWebRequest reqFTP;

            try

            {

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));

                reqFTP.Method = WebRequestMethods.Ftp.Rename;

                reqFTP.RenameTo = newFilename;

                reqFTP.UseBinary = true;

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();



                ftpStream.Close();

                response.Close();

            }

            catch (Exception ex)

            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "ReName Error --> " + ex.Message);

            }

        }



        public void MoveFile(string currentFilename, string newDirectory)

        {

            ReName(currentFilename, newDirectory);

        }



        public void GotoDirectory(string DirectoryName, bool IsRoot)

        {

            if (IsRoot)

            {

                ftpRemotePath = DirectoryName;

            }

            else

            {

                ftpRemotePath += DirectoryName + "/";

            }

            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";

        }


        //如下命令无效
        public static void DeleteOrderDirectory(string ftpServerIP, string folderToDelete, string ftpUserID, string ftpPassword)

        {

            try
            {

                if (!string.IsNullOrEmpty(ftpServerIP) && !string.IsNullOrEmpty(folderToDelete) && !string.IsNullOrEmpty(ftpUserID) && !string.IsNullOrEmpty(ftpPassword))

                {

                    AsmlFtpWeb fw = new AsmlFtpWeb(ftpServerIP, folderToDelete, ftpUserID, ftpPassword);

                    //进入订单目录

                    fw.GotoDirectory(folderToDelete, true);

                    //获取规格目录

                    string[] folders = fw.GetDirectoryList("dummy");

                    foreach (string folder in folders)

                    {

                        if (!string.IsNullOrEmpty(folder) || folder != "")

                        {

                            //进入订单目录

                            string subFolder = folderToDelete + "/" + folder;

                            fw.GotoDirectory(subFolder, true);

                            //获取文件列表

                            string[] files = fw.GetFileList("*.*");

                            if (files != null)

                            {

                                //删除文件

                                foreach (string file in files)

                                {

                                    fw.Delete(file);

                                }

                            }

                            //删除冲印规格文件夹

                            fw.GotoDirectory(folderToDelete, true);

                            fw.RemoveDirectory(folder);

                        }

                    }



                    //删除订单文件夹

                    string parentFolder = folderToDelete.Remove(folderToDelete.LastIndexOf('/'));

                    string orderFolder = folderToDelete.Substring(folderToDelete.LastIndexOf('/') + 1);

                    fw.GotoDirectory(parentFolder, true);

                    fw.RemoveDirectory(orderFolder);

                }

                else

                {

                    throw new Exception("FTP 及路径不能为空！");

                }

            }

            catch (Exception ex)

            {

                throw new Exception("删除订单时发生错误，错误信息为：" + ex.Message);

            }

        }

    }


}









