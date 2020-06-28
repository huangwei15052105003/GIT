using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace FemImageNet35
{
    
    public partial class Form1 : Form
    {
        public Fem fem;
        public DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }
        #region pub
        //除去触发SelectedIndexChanged事件以外的选中项都处于未选中状态  
        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index)
                { checkedListBox1.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked); }
            }

        }
        void InitDt()
        {
            dt = new DataTable();
            for (int i = 0; i <= int.Parse(fem.info[3]); ++i) { dt.Columns.Add(i.ToString(), typeof(double)); }
            for (int i = 0; i <= int.Parse(fem.info[4]); ++i)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;

        }
        string GetStructureName()
        {
            string mp=string.Empty;
            foreach (object itemChecked in checkedListBox1.CheckedItems) { mp = "MP-" + itemChecked.ToString().Split(':')[0]; }
            return mp;
        }
        
        void DtSaveCsv(DataTable dt ,string filename)
        {
            string delimiter = ",";
            StreamWriter csvStreamWriter = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            for (int j = 0; j < dt.Rows.Count; ++j)
            {
                string strRowValue = "";

                for (int k = 0; k < dt.Columns.Count; ++k)
                {
                    strRowValue += dt.Rows[j][k].ToString() + delimiter;

                }
                csvStreamWriter.WriteLine(strRowValue);
            }

            csvStreamWriter.Close();
            MessageBox.Show("CD Is Saved\n\n" + filename) ;

        }
        #endregion
        private void ButtonOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog= new OpenFileDialog();
            dialog.InitialDirectory = @"C:\\temp\\";
            dialog.Filter = "msr文档(*.msr)|*.msr|所有文件(*)|*";
            if (dialog.ShowDialog() == DialogResult.OK)
            { 
                string filepath = dialog.FileName;
                fem = new Fem(filepath);
                //基本信息:0_recipeName; 1_lotID; 2_idwName; 3_chipArr col; 4_chipArr row; 5_ MP qty; 6_MP_name
                textBoxRecipe.Text = fem.info[0];
                textBoxLotid.Text = fem.info[1];
                textBoxIdw.Text = fem.info[2];
                textBoxCol.Text = fem.info[3];
                textBoxRow.Text = fem.info[4];
                textBoxMpqty.Text = fem.info[5];
                //init checkedListBox1
                checkedListBox1.Items.Clear();
                for (int i = 0; i < int.Parse(fem.info[5]); ++i) { checkedListBox1.Items.Add(fem.info[6 + i]); }
                InitDt();  
            }
            else
            {
                MessageBox.Show("====未选择要导入的文件，退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                        
        }
        
        private void ButtonDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBoxRow.Text);//row qty
                                                   //choose measurement structure
                string mp = GetStructureName();
                //cleare datatable data
                InitDt();

                //get data
                foreach (string[] x in fem.cdStr)
                {
                    if (x[1] == mp)
                    {
                        int col = int.Parse(x[2].Split(',')[0]);
                        int row = int.Parse(x[2].Split(',')[1]);
                        try
                        { dt.Rows[n - row][col] = double.Parse(x[4]) / 10; }
                        catch (Exception ex)
                        { MessageBox.Show("ErrorCode: " + ex.Message + "\n\n Data Not Available-->Cell " + x[2]); }
                    }
                }
                string savePath = "\\\\10.4.3.130\\ftpdata\\temp\\" + textBoxRecipe.Text + "_" + mp + ".csv";
                DtSaveCsv(dt, savePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\nRun \"Read\" Command Before Extraction.");
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridview1:col/row
            int c = dataGridView1.CurrentCell.ColumnIndex;
            int r = dataGridView1.CurrentRow.Index;
            r = int.Parse(textBoxRow.Text) - r;
            //measurement strucutre
            string mp = GetStructureName();




            string imgNo=string.Empty;
            foreach (string[] x in fem.cdStr)
            {
           
                if (x[2]==c.ToString()+","+r.ToString()  && x[1]==mp)
                {
                    imgNo = x[0];
                }
            }
            
            if (imgNo.Length==2)
            { imgNo = "M00" + imgNo; }
            else if (imgNo.Length==3)
            { imgNo = "M0" + imgNo; }


            string imgPath = fem.path.Replace(".msr", "") + "\\" + fem.slot_no + "_" + imgNo + "-01MS";
            if (File.Exists(imgPath+".jpg"))
            { imgPath += ".jpg"; }
            else if (File.Exists(imgPath+".tif"))
            { imgPath += ".tif"; }
            else
            { MessageBox.Show("Image File Does Not Exist"); }
            try
            { pictureBox1.Load(imgPath); }
            catch (Exception )
            {
               // MessageBox.Show(ex.Message+"\n\n Failed Displaying Image");
            }

        }

        private void ButtonImage_Click(object sender, EventArgs e)
        {
            try
            {
                //get image path
                string mp = GetStructureName();
                Dictionary<string, string[]> imgs = new Dictionary<string, string[]>();
                foreach (var x in fem.cdStr)
                {
                    if (x[1] == mp)
                    {
                        string imgNo = x[0];
                        if (imgNo.Length == 2)
                        { imgNo = "M00" + imgNo; }
                        else if (imgNo.Length == 3)
                        { imgNo = "M0" + imgNo; }
                        string imgPath = fem.path.Replace(".msr", "") + "\\" + fem.slot_no + "_" + imgNo + "-01MS";
                        try
                        {
                            imgs.Add(x[2], new string[] { imgPath, (double.Parse(x[4]) / 10).ToString() + "nm" });
                        }
                        catch
                        {
                            //No CD Data，*
                            imgs.Add(x[2], new string[] { imgPath, x[4] });
                        }
                    }

                }

                int width = 512, height = 512;
                foreach (KeyValuePair<string, string[]> img in imgs)
                {
                    string path = img.Value[0];
                    if (File.Exists(path + ".jpg"))
                    { path += ".jpg"; }
                    else if (File.Exists(path + ".tif"))
                    { path += ".tif"; }
                    if (path.Contains(".jpg") || path.Contains(".tif"))
                    {
                        Image tmpImg = System.Drawing.Image.FromFile(path);
                        width = tmpImg.Width;
                        height = tmpImg.Height;
                        break;
                    }


                }
                //generate merged image
                string savePath = "\\\\10.4.3.130\\ftpdata\\temp\\" + textBoxRecipe.Text + "_" + mp + ".emf";
                using (Bitmap bmp = new Bitmap(width * int.Parse(textBoxCol.Text), height * int.Parse(textBoxRow.Text)))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {

                        System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(savePath, g.GetHdc());
                        Graphics merged = Graphics.FromImage(mf);


                        Pen pen1 = new Pen(Color.Green, 20);
                        int n = int.Parse(textBoxRow.Text);
                        int m = int.Parse(textBoxCol.Text);
                        //merged.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        foreach (KeyValuePair<string, string[]> img in imgs)
                        {



                            string path = img.Value[0];
                            if (File.Exists(path + ".jpg"))
                            { path += ".jpg"; }
                            else if (File.Exists(path + ".tif"))
                            { path += ".tif"; }
                            if (path.Contains(".jpg") || path.Contains(".tif"))
                            {
                                Image tmpImg = System.Drawing.Image.FromFile(path);
                                int col = int.Parse(img.Key.Split(',')[0]);
                                int row = int.Parse(img.Key.Split(',')[1]);
                                row = n - row;
                                merged.DrawImageUnscaled(tmpImg, width * col, height * row);
                                if (path.EndsWith(".tif"))
                                {
                                    merged.DrawRectangle(pen1, width * col, height * row, width, height);
                                }
                                else
                                {
                                    merged.DrawRectangle(pen1, width * col, height * row, width-20, height-20);
                                }
                                Font drawFont = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Millimeter);
                                SolidBrush drawBush = new SolidBrush(Color.Red);
                                if (radioButton1.Checked)
                                {
                                    if (path.EndsWith(".tif"))
                                    { merged.DrawString(img.Value[1], drawFont, drawBush, (float)(width * col + 80), (float)(height * row - 10)); }
                                    else
                                    { merged.DrawString(img.Value[1], drawFont, drawBush, (float)(width * col + 140), (float)(height * row - 10)); }
                                }






                            }

                        }




                        merged.Save();
                        merged.Dispose(); mf.Dispose();




                    }
                }
                pictureBox1.Image = null;
                pictureBox1.Load(savePath);
                MessageBox.Show("Image Is Saved:\n\n" + savePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Code: " + ex.Message + "\n\nRun \"Read\" Command Before Plot.");
            }
            }
    }
}
