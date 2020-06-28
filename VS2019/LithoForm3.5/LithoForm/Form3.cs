using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LithoForm
{

    public partial class Form3 : Form
    {
        public delegate void Form3Delegate1(string[] queryArr);
        public event Form3Delegate1 Form3Event1;


        public static int choiceNo;
        public Form3()
        {
            InitializeComponent();
            this.ControlBox = false;
            if (false)
            {
               
                if (choiceNo == 888)
                {

                    ListView listview = new ListView();
                    listview.Size = new Size(380, 96);
                    listview.Location = new Point(2, 24);
                    listview.Name = "listview";
                    this.Controls.Add(listview);

                    Button btn1 = new Button();
                    btn1.Size = new Size(62, 21);
                    btn1.Location = new Point(2, 126);
                    btn1.Name = "btn1";
                    btn1.Text = "btn1";
                    this.Controls.Add(btn1);

                    Button btn2 = new Button();
                    btn2.Size = new Size(62, 21);
                    btn2.Location = new Point(74, 126);
                    btn2.Name = "btn2";
                    btn2.Text = "btn2";
                    this.Controls.Add(btn2);

                    Button btn3 = new Button();
                    btn3.Size = new Size(62, 21);
                    btn3.Location = new Point(146, 126);
                    btn3.Name = "btn3";
                    btn3.Text = "btn3";
                    this.Controls.Add(btn3);

                    Button btn4 = new Button();
                    btn4.Size = new Size(62, 21);
                    btn4.Location = new Point(218, 126);
                    btn4.Name = "btn4";
                    btn4.Text = "btn4";
                    this.Controls.Add(btn4);

                    Button btn5 = new Button();
                    btn5.Size = new Size(62, 21);
                    btn5.Location = new Point(290, 126);
                    btn5.Name = "btn5";
                    btn5.Text = "btn5";
                    this.Controls.Add(btn5);
                    //https://zhidao.baidu.com/question/84797720.html
                    // https://blog.csdn.net/a200507002/article/details/73198398?locationNum=6&fps=1





                    int m = 0;//添加5个主菜单每个主菜单有10个子菜单
                    foreach (string a in new string[] { "保留1", "保留2", "保留3" })

                    {
                        ToolStripMenuItem i = new ToolStripMenuItem();
                        i.Name = a;
                        i.Text = a;
                        i.Tag = a;
                        this.menuStrip1.Items.Add(i);
                        for (int z = 0; z < 5; z++)
                        {
                            ToolStripMenuItem i1 = new ToolStripMenuItem();
                            i1.Name = a + m.ToString();
                            i1.Text = a + "_" + z.ToString();
                            i1.Tag = a + "_" + z.ToString();
                            i1.Click += new EventHandler(i1_Click);


                            i.DropDownItems.Add(i1);
                            m++;
                        }
                    }




                }
                else if (choiceNo == 999)
                {
                    ListView listview = new ListView();
                    listview.Size = new Size(380, 96);
                    listview.Location = new Point(2, 24);
                    listview.Name = "listview";
                    this.Controls.Add(listview);
                }
                else
                {
                    ListView listview = new ListView();
                    listview.Size = new Size(380, 96);
                    listview.Location = new Point(2, 24);
                    listview.Name = "listview";
                    this.Controls.Add(listview);
                }
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1 = (Form1)this.Owner;
            form1.Form3Trigger += fillListView;
            // this.FormBorderStyle = FormBorderStyle.None;
        }

        private void fillListView(DataTable dt)
        {  //http://www.360doc.com/content/19/0427/09/6889381_831772020.shtml

            listView1.Clear();

            //添加列名
            ColumnHeader c1 = new ColumnHeader();
            c1.Width = 100; c1.Text = "PART";
            c1.TextAlign = HorizontalAlignment.Left;
            ColumnHeader c2 = new ColumnHeader();
            c2.Width = 50; c2.Text = "LAYER";
            c2.TextAlign = HorizontalAlignment.Left;
            ColumnHeader c3 = new ColumnHeader();
            c3.Width = 80; c3.Text = "LOTID";
            c3.TextAlign = HorizontalAlignment.Left;

            //设置属性
            listView1.GridLines = true;
            listView1.FullRowSelect = true;//display full row
            listView1.MultiSelect = false;
            listView1.View = View.Details;
            listView1.HoverSelection = true;//鼠标停留数秒后，自动选择
                                            //add colune name to listview
                                            //listView.Columns.Add("No", 50, HorizontalAlignment.Left); //alternative 
            listView1.Columns.Add(c1);
            listView1.Columns.Add(c2);
            listView1.Columns.Add(c3);


            //add items
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem li = new ListViewItem(row["PART"].ToString());//此处括号内的变量是第一个字段参数
                li.SubItems.Add(row["LAYER"].ToString());
                li.SubItems.Add(row["LOTID"].ToString());
                listView1.Items.Add(li);
            }
            listView1.Items[0].Selected = true;//默认选中第一行，若无选中行的话，后续会报错，额外增加判断 }
        }
        private void i1_Click(object sender, EventArgs e)

        {
            MessageBox.Show("备用菜单项");
        }



       

       

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)    //关闭form3
        {
            ActiveForm.Close();
            LithoForm.Form1.form3Flag = false;

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string item = string.Empty;
            if (ByLot.Checked)
            {
               
                if (radioCd.Checked) { item = "DOSE"; }
                else if (radioOvl.Checked) { item = "all"; }
                else if (radioTrx.Checked) { item = "tran-x"; }
                else if (radioTry.Checked) { item = "tran-y"; }
                else if (radioExpX.Checked) { item = "exp-x"; }
                else if (radioExpY.Checked) { item = "exp-y"; }
                else if (radioOrt.Checked) { item = "non-ort"; }
                else if (radioRot.Checked) { item = "w-rot"; }
                else if (radioSMag.Checked) { item = "mag"; }
                else if (radioSRot.Checked) { item = "rot"; }
                else if (radioAMag.Checked) { item = "asym-mag"; }
                else if (radioARot.Checked) { item = "asym-rot"; }

                string[] queryArr = new string[] { listView1.SelectedItems[0].SubItems[0].Text, listView1.SelectedItems[0].SubItems[1].Text, listView1.SelectedItems[0].SubItems[2].Text, item, "ByLotID" };
                Form3Event1(queryArr);
            }

            if (ByPartLayer.Checked)
            {
                
                if (radioCd.Checked) { item = "DOSE"; }
                else if (radioOvl.Checked) { item = "all"; }
                else if (radioTrx.Checked) { item = "tran-x"; }
                else if (radioTry.Checked) { item = "tran-y"; }
                else if (radioExpX.Checked) { item = "exp-x"; }
                else if (radioExpY.Checked) { item = "exp-y"; }
                else if (radioOrt.Checked) { item = "non-ort"; }
                else if (radioRot.Checked) { item = "w-rot"; }
                else if (radioSMag.Checked) { item = "mag"; }
                else if (radioSRot.Checked) { item = "rot"; }
                else if (radioAMag.Checked) { item = "asym-mag"; }
                else if (radioARot.Checked) { item = "asym-rot"; }

                string[] queryArr = new string[] { listView1.SelectedItems[0].SubItems[0].Text, listView1.SelectedItems[0].SubItems[1].Text, listView1.SelectedItems[0].SubItems[2].Text, item, "ByPartLayer" };
                Form3Event1(queryArr);

            }

        }
    }
}
