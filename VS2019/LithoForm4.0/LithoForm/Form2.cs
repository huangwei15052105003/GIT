using System;
using System.Windows.Forms;

namespace LithoForm
{
    public partial class Form2 : Form
    {
        public delegate void ChildDelegate1(string sumKey,string groupKey); //统计
        public event ChildDelegate1 ChildEvent1;

        public delegate void ChildDelegate2(string str); //排序
        public event ChildDelegate2 ChildEvent2;

        public delegate void ChildDelegate3(string str); //筛选
        public event ChildDelegate3 ChildEvent3;




        public Form2()
        {
            InitializeComponent();
           // this.ControlBox = false;
        }





     

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1 = (Form1)this.Owner;
            form1.Form2Trigger += FillList;
        

        }

      

        private void FillList(string[] passData)
        {

            try
            {
           
                listView1.Items.Clear();
            }
            catch
            {

            }

            //http://www.360doc.com/content/19/0427/09/6889381_831772020.shtml
            //添加列名
            ColumnHeader c1 = new ColumnHeader();
            c1.Width = 150;c1.Text = "FieldName";
            c1.TextAlign = HorizontalAlignment.Left;
            ColumnHeader c2 = new ColumnHeader();
            c2.Width = 80; c2.Text = "FieldType";
            c2.TextAlign = HorizontalAlignment.Left;
            //设置属性
            listView1.GridLines = true;
            listView1.FullRowSelect = true;//display full row
            listView1.MultiSelect = false;
            listView1.View = View.Details;
            listView1.HoverSelection = true;//鼠标停留数秒后，自动选择
                                            //add colune name to listview
            listView1.Columns.Add("No", 50, HorizontalAlignment.Left); //alternative 
            listView1.Columns.Add(c1);
            listView1.Columns.Add(c2);
          

            //add items
            string[] tmp0 = passData[0].Split(new char[] { ',' });
            string[] tmp1 = passData[1].Split(new char[] { ',' });
            for (int i = 0;i< tmp0.Length - 1;i++)
            {
                ListViewItem li = new ListViewItem(i.ToString());//此处括号内的变量是第一个字段参数
                li.SubItems.Add(tmp0[i]);
                li.SubItems.Add(tmp1[i]);
                listView1.Items.Add(li);

            }
            listView1.Items[0].Selected = true;//默认选中第一行，若无选中行的话，后续会报错，额外增加判断
        }

     

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
       



        }

        private void button4_Click(object sender, EventArgs e) //清空 clear textbox content
        {
            listBox2.Items.Clear();
        }

       

      

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + ",分类项");
        }

        private void button7_Click(object sender, EventArgs e) //加入排序
        {
            if (radioButton1.Checked)
            { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + ",升序"); }
            else
            { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + ",降序"); }
        }
        private void button2_Click(object sender, EventArgs e) //执行排序
        {
            string sortKey = string.Empty;
            string str;
            if (listBox2.Items.Count == 0) { MessageBox.Show("请定义排序项目"); return; }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                str = listBox2.Items[i].ToString();
                if (!(str.Contains("升序") || str.Contains("降序")))
                { MessageBox.Show("排序含有统计，筛选内容，退出"); return; }
                str = str.Replace(",", " ");

                str = str.Replace("升序", "asc");

                str = str.Replace("降序", "desc");
                sortKey += (str + ",");

            }
            sortKey = sortKey.Substring(0, sortKey.Length - 1);
            ChildEvent2(sortKey);
            ActiveForm.Close();

        }
        private void button5_Click(object sender, EventArgs e)//加入统计项
        {
            listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + ",统计项");
        }
        private void button1_Click(object sender, EventArgs e)  //执行统计
        {
            //https://www.cnblogs.com/sydeveloper/archive/2013/03/29/2988669.html
            string sumKey = string.Empty;
            string groupKey = string.Empty;
            string str;
            if (listBox2.Items.Count == 0) { MessageBox.Show("请定义统计项目"); return; }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                str = listBox2.Items[i].ToString();
                if (!(str.Contains("统计项") || str.Contains("分类项")))
                { MessageBox.Show("排序含有排序，筛选内容，退出"); return; }
               if (str.Contains("统计项"))
                { sumKey += (str.Split(new char[] { ',' })[0]+","); }
               else if (str.Contains("分类项"))
                { groupKey+= (str.Split(new char[] { ',' })[0]+","); }

            }
            if (sumKey.Length > 0) { sumKey = sumKey.Substring(0, sumKey.Length - 1); }
            if (groupKey.Length > 0) { groupKey = groupKey.Substring(0, groupKey.Length - 1); }

            ChildEvent1(sumKey,groupKey);
           ActiveForm.Close();

        }
        private void button8_Click(object sender, EventArgs e) //加入筛选项
        {
            string bj;
            if (textBox1.Text.Trim().Length == 0)
            { MessageBox.Show("请先在文本框中输入比较参数值"); return; }
            if (listBox2.Items.Count == 0)
            {
                try
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text == "String")
                    {
                
                        listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + ",'" + textBox1.Text.Trim() + "'" ); 
                    }
                    else
                    {
                      
                        listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + "," + textBox1.Text.Trim() ); 
                      
                    }
                }
                catch
                { MessageBox.Show("请选择比较运算符"); }
            }
            else
            {
                try
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text == "String")
                    {
                        if (radioButton3.Checked == true)
                        { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + ",'" + textBox1.Text.Trim() + "'" + ",AND"); }
                        else
                        { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + ",'" + textBox1.Text.Trim() + "'" + ",OR"); }
                    }
                    else
                    {
                        if (radioButton3.Checked == true)
                        { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + "," + textBox1.Text.Trim() + ",AND"); }
                        else
                        { listBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text + "," + listBox1.SelectedItem.ToString() + "," + textBox1.Text.Trim() + ",OR"); }
                    }
                }
                catch
                { MessageBox.Show("请选择比较运算符"); }
            }
           


        }
        private void button3_Click(object sender, EventArgs e)  //执行筛选
        {
            string filterKey = string.Empty;
            string str;
            string[] strArr;
            if (listBox2.Items.Count == 0) { MessageBox.Show("请定义筛选项目"); return; }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                str = listBox2.Items[i].ToString();
                strArr = str.Split(new char[] { ',' });
                if (str.Contains("升序") || str.Contains("降序") || str.Contains("统计项")|| str.Contains("分类项"))
                { MessageBox.Show("排序含有统计，排序内容，退出"); return; }
                
                if (strArr.Length==3)
                {
                    if (strArr[1] == "大于")
                    {  filterKey += strArr[0] + " > " + strArr[2];    }
                    else if (strArr[1] == "小于")
                    {   filterKey += strArr[0] + " < " + strArr[2];    }
                    else if (strArr[1] == "等于")
                    { filterKey += strArr[0] + " = " + strArr[2]; }
                    else if (strArr[1] == "不等于")
                    { filterKey += strArr[0] + " <> " + strArr[2]; }
                    else if (strArr[1] == "包含")
                    { filterKey += strArr[0] + " like '%" + strArr[2].Substring(1, strArr[2].Length-2)+"%'"; }
                    else if (strArr[1] == "起始")
                    { filterKey += strArr[0] + " like '" + strArr[2].Substring(1, strArr[2].Length - 2) + "%'"; }
                    else if (strArr[1] == "止于")
                    { filterKey += strArr[0] + " like '%" + strArr[2].Substring(1, strArr[2].Length - 2) + "'"; }
                    else if (strArr[1] == "不包含")
                    { filterKey += strArr[0] + " not like '%" + strArr[2].Substring(1, strArr[2].Length - 2) + "%'"; }





                }
                else
                {
                    if (strArr[1] == "大于")
                    { filterKey +=" "+ strArr[3] + " " + strArr[0] + " > " + strArr[2];   }
                    else if (strArr[1] == "小于")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " < " + strArr[2]; }
                    else if (strArr[1] == "等于")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " = " + strArr[2]; }
                    else if (strArr[1] == "不等于")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " <> " + strArr[2]; }
                    else if (strArr[1] == "包含")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " like '%" + strArr[2].Substring(1, strArr[2].Length - 2) + "%'"; }
                    else if (strArr[1] == "起始")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " like '" + strArr[2].Substring(1, strArr[2].Length - 2) + "%'"; }
                    else if (strArr[1] == "止于")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " like '%" + strArr[2].Substring(1, strArr[2].Length - 2) + "'"; }
                    else if (strArr[1] == "不包含")
                    { filterKey += " " + strArr[3] + " " + strArr[0] + " not like '%" + strArr[2].Substring(1, strArr[2].Length - 2) + "%'"; }

                   



                }
                

              
                

            }
        
            ChildEvent3(filterKey);
            ActiveForm.Close();
        }

       
    }
}
