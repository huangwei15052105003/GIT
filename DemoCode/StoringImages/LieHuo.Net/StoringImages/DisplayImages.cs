using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Data.SQLite;
using Kribo.StoringImages.Model;

namespace Kribo.StoringImages
{
    public partial class DisplayImages : Form
    {
        private dBHelper helper = null;

        public DisplayImages()
        {
            InitializeComponent();
        }

        private void DisplayImages_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            // Determin the ConnectionString
            string connectionString = dBFunctions.ConnectionStringSQLite;

            // Determin the DataAdapter = CommandText + Connection
            string commandText = @"SELECT * FROM ImageStore ORDER BY image_id";

            // Make a new object
            helper = new dBHelper(connectionString);

            // Load the data
            if (helper.Load(commandText, "") == true)
            {
                // Show the data in the datagridview
                dataGridViewImageList.DataSource = helper.DataSet.Tables[0];
            }
        }

        private void dataGridViewImageList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do something on double click, except when on the header.
            if (e.RowIndex == -1)
            {
                return;
            }
            // Have aquired a valided row
            Int32 i = Convert.ToInt32(dataGridViewImageList.CurrentRow.Cells[0].Value);
            UpdatePictureBox(i);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPicture();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 i = Convert.ToInt32(dataGridViewImageList.CurrentRow.Cells[0].Value);
            DeletePicture(i);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 i = Convert.ToInt32(dataGridViewImageList.CurrentRow.Cells[0].Value);
            SavePicture(i);
        }


        private void NewPicture()
        {
            ImageHelper imgHelper = new ImageHelper();
            Int32 i = imgHelper.InsertImage();
            if (imgHelper.GetSucces()) GetData();
            UpdatePictureBox(i);

        }
        private void DeletePicture(Int32 i)
        {
            ImageHelper imgHelper = new ImageHelper();
            imgHelper.DeleteImage(i);
            if (imgHelper.GetSucces()) GetData();
            i = i - 1;
            UpdatePictureBox(i);

        }
        private void SavePicture(Int32 i)
        {
            ImageHelper imgHelper = new ImageHelper();
            imgHelper.SaveAsImage(i);
            if (imgHelper.GetSucces()) GetData();
        }


        private void UpdatePictureBox(Int32 i)
        {
            if (i > 0)
            {
                DataRow dataRow = helper.DataSet.Tables[0].Rows[i - 1];
                byte[] imageBytes = (byte[])dataRow[2];
                MemoryStream ms = new MemoryStream(imageBytes);
                Bitmap bmap = new Bitmap(ms);
                pictureBoxImage.Image = (System.Drawing.Image)bmap;
                labelFileName.Text = labelFileName.Text +" "+ dataRow["imageFileName"].ToString();
            }
            else
            {
                pictureBoxImage.Image = null;
            }
        }




    }
}
