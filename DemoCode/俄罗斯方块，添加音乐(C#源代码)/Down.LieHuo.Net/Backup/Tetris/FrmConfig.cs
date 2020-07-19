using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//download by http://down.liehuo.net
namespace Tetris
{
    public partial class FrmMode : Form
    {
        public FrmMode()
        {
            InitializeComponent();
        }

        private bool[,] struArr = new bool[5, 5];
        private Color blockColor = Color.Red;
        private Config config = new Config();

        private void lblMode_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;         //定义画笔
            gp.Clear(Color.Black);
            Pen p = new Pen(Color.White);
            for (int i = 31; i < 155; i += 31)//画横白线
                gp.DrawLine(p, 1, i, 156, i);
            for (int i = 31; i < 155; i += 31)//画竖白线
                gp.DrawLine(p, i, 1, i, 156);
            //
            //下面填充矩阵中的方块
            SolidBrush s = new SolidBrush(blockColor);
            for ( int x = 0 ;x < 5 ; x++ )
            {
                for(int y = 0 ; y < 5 ; y++ )
                {
                    if (struArr[x, y])          //判断是否被选中！！！
                    {
                        gp.FillRectangle(s, x * 31 + 1, y * 31 + 1, 30, 30);
                    }
                }
            }
        }

        private void lblMode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            int xPos, yPos;     //数组下标
            xPos = e.X / 31;    //X、Y（大写），鼠标点击相当于父控件的位置(鼠标点击位置转换为数组下标)
            yPos = e.Y / 31;
            struArr[xPos, yPos] = !struArr[xPos, yPos];
            bool b = struArr[xPos, yPos];
            Graphics gp = lblMode.CreateGraphics();//得到label1的Graphics
            SolidBrush s = new SolidBrush(b ? blockColor : Color.Black);
            //
            gp.FillRectangle(s, 31 * xPos + 1, 31 * yPos + 1, 30, 30);
            gp.Dispose();       //释放gp(不释放会……label_Paint()怎么没有释放？)答:CreateGraphics()生成的必须
            //显示的关闭.
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();//打开颜色对话框
            blockColor = colorDialog1.Color;    //填充颜色为选中颜色
            lblColor.BackColor = blockColor;
            lblMode.Invalidate();//?????????使用lblMode重画，即执行Print事件？？标签类的内部方法
        }//它会自动调用Print()方法

        private void butAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;//首先查找图案是否为空
            foreach (bool i in struArr)
            {
                if (i)
                {
                    isEmpty = true;//
                    break;
                }
            }
            if (!isEmpty)
            {
                MessageBox.Show("图案为空，请先用鼠标点击左边窗口绘制图案！", "提示窗口",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            //把方块转换成二进制字符串流
            StringBuilder sb = new StringBuilder(25);
            foreach (bool i in struArr)
            {
                sb.Append(i ? "1" : "0");//向字符串里面添加字符 
            }
            string blockString = sb.ToString();
            //再检查是否有重复图案
            foreach (ListViewItem item in lsvBlockSet.Items)
            {
                if (item.SubItems[0].Text == blockString)
                {
                    MessageBox.Show("该图案已经存在！","提示窗口",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                    return;
                }

            }
            ListViewItem myItem = new ListViewItem();
            myItem = lsvBlockSet.Items.Add(blockString);
            myItem.SubItems.Add(Convert.ToString(blockColor.ToArgb()));//在第二列添加颜色,不应该是myItem.SubItem[1].Add();样的格式吗?????

        }

        private void lsvBlockSet_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)//避免重复执行事件……判断事件是不是被选中，作用:点击Item时,在lblmode上能显示选中的信息
            {
                blockColor = Color.FromArgb(int.Parse(e.Item.SubItems[1].Text));//获取颜色，
                lblColor.BackColor = blockColor;
                string s = e.Item.SubItems[0].Text;
                for (int i = 0; i < s.Length; i++) //取砖块样式信息
                {
                    struArr[i / 5, i % 5] = (s[i] == '1') ? true : false;
                }
                lblMode.Invalidate();//重绘！！！
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)//没有被选中。
            {
                MessageBox.Show("请在右边窗口选择一个条目进行删除！","提示窗口",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                return;
            }
            lsvBlockSet.Items.Remove(lsvBlockSet.SelectedItems[0]);//删除被删除的项目.下标0 表明不能被选中多个
            btnClear.PerformClick();//执行btnClear_Click事件》？？？？？
        }

        private void btnClear_Click(object sender, EventArgs e)//清空按钮
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    struArr[x, y] = false;
                }
            }
            lblMode.Invalidate();//为什么不直接调用lblMode_Paint()呢?????
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)//判断是否被选中。
            {
                MessageBox.Show("请在右边窗口选择一个条目进行删除！", "提示窗口",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                return;
            }
            bool isEmpty = false;//首先查找图案是否为空
            foreach (bool i in struArr)
            {
                if (i)
                {
                    isEmpty = true;
                    break;
                }
            }
            if (!isEmpty)
            {
                MessageBox.Show("图案为空，请先用鼠标点击左边窗口绘制图案！", "提示窗口",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            StringBuilder sb = new StringBuilder(25);
            foreach (bool i in struArr)//把图案信息转换为二进制信息
            {
                sb.Append(i ? "1" : "0");
            }
            lsvBlockSet.SelectedItems[0].SubItems[0].Text = sb.ToString();//改变图案信息
            lsvBlockSet.SelectedItems[0].SubItems[1].Text = Convert.ToString(blockColor.ToArgb());//改变颜色信息

        }

        private void txtContra_KeyDown(object sender, KeyEventArgs e)//获得键盘按键事件；// 这段代码，典型的键盘事件
        {   
            //排除一些不适合做快捷键的控制字符，
            if ((e.KeyValue >= 33 && e.KeyValue <= 36) || (e.KeyValue >= 45 && e.KeyValue <= 46
                ) ||
                (e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 65 && e.KeyValue <= 90) ||
                (e.KeyValue >= 96 && e.KeyValue <= 107) || (e.KeyValue >= 109 && e.KeyValue <= 111) ||
                (e.KeyValue >= 186 && e.KeyValue <= 192) || (e.KeyValue >= 219 && e.KeyValue <= 222))
            {
                //检查是否存在冲突的快捷键
                foreach (Control c in gbKeySet.Controls)//用Control类的c遍历第一个groupBox里的所有Controls。
                {
                    Control TempC = c as TextBox;   //c as TextBox!c作为TextBox
                    if (TempC != null && ((TextBox)TempC).Text != "")//textbox不为空时((TextBox)TempC).Text !=""，若为空时，
                        //因为强制类型转换的时候，转换成object类时为空,再转换成int时，无值，所以出错
                    {
                        if (((int)((TextBox)TempC).Tag) == e.KeyValue)//两次强制类型转换，
                        {//每个TextBox都有一个Tag,用来存放类。
                            ((TextBox)TempC).Text = "";
                            ((TextBox)TempC).Tag = Keys.None;
                        }
                    }
                }
                ((TextBox)sender).Text = e.KeyCode.ToString();
                ((TextBox)sender).Tag = (Keys)e.KeyCode;
            }
        }

        private void lblBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();//                        
            lblBackColor.BackColor = colorDialog1.Color;
            lblMode.Invalidate();//?????????使用lblMode重画，即执行Print事件？？(自己没有定义啊)标签类的内部方法()
        }

        private void FrmMode_Load(object sender, EventArgs e)
        {
            config.LoadFromXmlFile(); //读取xml文件
            InfoArr info = config.Info;
            //读取砖块样式
            ListViewItem myItem = new ListViewItem();
            for (int i = 0; i < info.Length; i++)
            {
                myItem = lsvBlockSet.Items.Add(info[i].GetIdStr());
                myItem.SubItems.Add(info[i].GetColorStr());
            }
            //读取快捷键
            txtDown.Text = ((Keys)config.DownKey).ToString();
            txtDown.Tag = config.DownKey;       //???????????????????
            txtDrop.Text = ((Keys)config.DropKey).ToString();
            txtDrop.Tag = config.DropKey;
            txtLeft.Text = ((Keys)config.MoveLeftKey).ToString();
            txtLeft.Tag = config.MoveLeftKey;
            txtRight.Text = ((Keys)config.MoveRightKey).ToString();
            txtRight.Tag = config.MoveRightKey;
            txtDeasil.Text = ((Keys)config.DeasilRotateKey).ToString();
            txtDeasil.Tag = config.DeasilRotateKey;
            txtContra.Text = ((Keys)config.ContraRotateKey).ToString();
            txtContra.Tag = config.ContraRotateKey;
            //读取环境设置参数
            txtCoorWidth.Text = config.CoorWidth.ToString();
            txtCoorheight.Text = config.CoorHeight.ToString();
            txtRectPix.Text = config.RectPix.ToString();
            lblBackColor.BackColor = config.BackColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InfoArr info = new InfoArr();
            foreach (ListViewItem item in lsvBlockSet.Items)//从lsvBlackSet内读取砖块信息,并存入info内
            {
                info.Add(item.SubItems[0].Text, item.SubItems[1].Text);
            }
            config.Info = info;//把info赋给config对象的Info属性
            config.DownKey = (Keys)txtDown.Tag;
            config.DropKey = (Keys)txtDrop.Tag;
            config.MoveLeftKey = (Keys)txtLeft.Tag;
            config.MoveRightKey = (Keys)txtRight.Tag;
            config.DeasilRotateKey = (Keys)txtDeasil.Tag;
            config.ContraRotateKey = (Keys)txtContra.Tag;
            config.CoorWidth = int.Parse(txtCoorWidth.Text);
            config.CoorHeight = int.Parse(txtCoorheight.Text);
            config.RectPix = int.Parse(txtRectPix.Text);
            config.BackColor = lblBackColor.BackColor;
            config.SaveToXmlFile();//保存成xml文件
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}