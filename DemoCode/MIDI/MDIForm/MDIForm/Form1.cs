using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Download by http://www.codesc.net
namespace MDIForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();//实例化Form2
            frm2.MdiParent = this;//设置MdiParent属性，将当前窗体作为父窗体
            frm2.Show();//使用Show方法打开窗体
            Form3 frm3 = new Form3();//实例化Form3
            frm3.MdiParent = this;//设置MdiParent属性，将当前窗体作为父窗体
            frm3.Show();//使用Show方法打开窗体
            Form4 frm4 = new Form4();//实例化Form4
            frm4.MdiParent = this;//设置MdiParent属性，将当前窗体作为父窗体
            frm4.Show();//使用Show方法打开窗体
        }
        private void 水平平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);//使用MdiLayout枚举实现水平平铺
        }
        private void 垂直平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);//使用MdiLayout枚举实现垂直平铺
        }
        private void 层叠排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);//使用MdiLayout枚举实现层叠排列
        }
    }
}
