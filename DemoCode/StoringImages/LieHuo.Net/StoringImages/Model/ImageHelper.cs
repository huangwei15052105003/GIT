using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
// 资源来自：烈火下载 http://down.liehuo.net
namespace Kribo.StoringImages.Model
{
    public class ImageHelper
    {
        private dBHelper helper = null;
        private string fileLocation = string.Empty;
        private bool isSucces = false;
        private int maxImageSize = 2097152;

        //2MB   - 2097152
        //5MB   - 5242880
        //10MB  - 10485760

        /*  Conversion
         *  1 Byte = 8 Bit
         *  1 Kilobyte = 1024 Bytes
         *  1 Megabyte = 1048576 Bytes
         *  1 Gigabyte = 1073741824 Bytes
         * */

        private string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
            }
        }
        public Boolean GetSucces()
        {
            return isSucces;
        }

        private Image LoadImage()
        {
            //Create an instance of the Image Class/Object
            //so that we can store the information about the picture an send it back for
            //processing into the database.
            Image image = null;


            //Ask user to select Image
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"C:\\";
            dlg.Title = "Select Image File";
            //dlg.Filter = "Tag Image File Format (*.tiff)|*.tiff";
            //dlg.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
            //dlg.Filter += "|Portable Network Graphic Format (*.png)|*.png";
            //dlg.Filter += "|Joint Photographic Experts Group Format (*.jpg)|*.jpg";
            //dlg.Filter += "|Joint Photographic Experts Group Format (*.jpeg)|*.jpeg";
            //dlg.Filter += "|Nikon Electronic Format (*.nef)|*.nef";
            //dlg.Filter += "|All files (*.*)|*.*";
            dlg.Filter = "Image Files  (*.jpg ; *.jpeg ; *.png ; *.gif ; *.tiff ; *.nef)|*.jpg;*.jpeg;*.png;*.gif;*.tiff;*.nef";
            dlg.ShowDialog();

            this.FileLocation = dlg.FileName;

            if (fileLocation == null || fileLocation == string.Empty)
                return image;

            if (FileLocation != string.Empty && fileLocation != null)
            {
                Cursor.Current = Cursors.WaitCursor;

                //Get file information and calculate the filesize
                FileInfo info = new FileInfo(FileLocation);
                long fileSize = info.Length;

                //reasign the filesize to calculated filesize
                maxImageSize = (Int32)fileSize;

                if (File.Exists(FileLocation))
                {
                    //Retreave image from file and binary it to Object image
                    using (FileStream stream = File.Open(FileLocation, FileMode.Open))
                    {
                        BinaryReader br = new BinaryReader(stream);
                        byte[] data = br.ReadBytes(maxImageSize);
                        image = new Image(dlg.SafeFileName, data, fileSize);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            return image;
        }

        public Int32 InsertImage()
        {
            DataRow dataRow = null;
            isSucces = false;

            Image image = LoadImage();

            //if no file was selected and no image was created return 0
            if (image == null) return 0;

            if (image != null)
            {
                // Determin the ConnectionString
                string connectionString = dBFunctions.ConnectionStringSQLite;

                // Determin the DataAdapter = CommandText + Connection
                string commandText = "SELECT * FROM ImageStore WHERE 1=0";

                // Make a new object
                helper = new dBHelper(connectionString);
                {
                    // Load Data
                    if (helper.Load(commandText, "image_id") == true)
                    {
                        // Add a row and determin the row
                        helper.DataSet.Tables[0].Rows.Add(helper.DataSet.Tables[0].NewRow());
                        dataRow = helper.DataSet.Tables[0].Rows[0];

                        // Enter the given values
                        dataRow["imageFileName"] = image.FileName;
                        dataRow["imageBlob"] = image.ImageData;
                        dataRow["imageFileSizeBytes"] = image.FileSize;

                        try
                        {
                            // Save -> determin succes
                            if (helper.Save() == true)
                            {
                                isSucces = true;

                            }
                            else
                            {
                                isSucces = false;
                                MessageBox.Show("Error during Insertion");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Show the Exception --> Dubbel Id/Name ?
                            MessageBox.Show(ex.Message);
                        }

                    }//END IF
                }
            }
           //return the new image_id
           return Convert.ToInt32(dataRow[0].ToString());
        }

        public void DeleteImage(Int32 imageID)
        {
            //Set variables
            isSucces = false;

            // Determin the ConnectionString
            string connectionString = dBFunctions.ConnectionStringSQLite;

            // Determin the DataAdapter = CommandText + Connection
            string commandText = "SELECT * FROM ImageStore WHERE image_id=" + imageID;

            // Make a new object
            helper = new dBHelper(connectionString);
            {
                // Load Data
                if (helper.Load(commandText, "image_id") == true)
                {
                    // Determin if the row was found
                    if (helper.DataSet.Tables[0].Rows.Count == 1)
                    {
                        // Found, delete row
                        helper.DataSet.Tables[0].Rows[0].Delete();
                        try
                        {
                            // Save -> determin succes
                            if (helper.Save() == true)
                            {
                                isSucces = true;
                            }
                            else
                            {
                                isSucces = false;
                                MessageBox.Show("Delete failed");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Show the Exception --> Dubbel ContactId/Name ?
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }

        }

        public void SaveAsImage(Int32 imageID)
        {
            //set variables
            DataRow dataRow = null;
            Image image = null;
            isSucces = false;

            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = @"C:\\";
            dlg.Title = "Save Image File";
            //1
            dlg.Filter = "Tag Image File Format (*.tiff)|*.tiff";   
            //2
            dlg.Filter += "|Graphics Interchange Format (*.gif)|*.gif";
            //3
            dlg.Filter += "|Portable Network Graphic Format (*.png)|*.png";
            //4
            dlg.Filter += "|Joint Photographic Experts Group Format (*.jpg)|*.jpg";
            //5
            dlg.Filter += "|Joint Photographic Experts Group Format (*.jpeg)|*.jpeg";
            //6
            dlg.Filter += "|Bitmap Image File Format (*.bmp)|*.bmp";
            //7
            dlg.Filter += "|Nikon Electronic Format (*.nef)|*.nef";
            dlg.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (dlg.FileName != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                //making shore only one of the 7 is being used if not added the default extention to the filename
                string defaultExt = ".png";
                int pos = -1;
                string[] ext = new string[7] {".tiff", ".gif", ".png", ".jpg", ".jpeg", ".bmp", ".nef"};
                string extFound = string.Empty;
                string filename = dlg.FileName.Trim();
                for (int i = 0; i < ext.Length; i++)
                {
                    pos = filename.IndexOf(ext[i], pos + 1);
                    if (pos > -1)
                    {
                        extFound = ext[i];
                        break;
                    }
                }
                if (extFound == string.Empty) filename = filename + defaultExt;
               


                // Determin the ConnectionString
                string connectionString = dBFunctions.ConnectionStringSQLite;

                // Determin the DataAdapter = CommandText + Connection
                string commandText = "SELECT * FROM ImageStore WHERE image_id=" + imageID;

                // Make a new object
                helper = new dBHelper(connectionString);

                // Load the data
                if (helper.Load(commandText, "") == true)
                {
                    // Show the data in the datagridview
                    dataRow = helper.DataSet.Tables[0].Rows[0];
                    image = new Image(
                                      (string)dataRow["imageFileName"],
                                      (byte[])dataRow["imageBlob"],
                                      (long)dataRow["imageFileSizeBytes"]
                                      );

                    // Saves the Image via a FileStream created by the OpenFile method.
                    using (FileStream stream = new FileStream(filename, FileMode.Create))
                    {
                        BinaryWriter bw = new BinaryWriter(stream);
                        bw.Write(image.ImageData);
                        isSucces = true;
                    }
                }
                Cursor.Current = Cursors.Default;
            }

            if (isSucces)
            {
                MessageBox.Show("Save succesfull");
            }
            else
            {
                MessageBox.Show("Save failed");
            }
        }



    }
}
