using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIForm
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }
        Form2 frm2 = null;
        Form3 frm3 = null;
        Form4 frm4 = null;
        private void 客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (frm2 == null)
            {
                frm2 = new Form2();
                frm2.MdiParent = this;
                frm2.Show();
            }
            else
            {
                if (frm2.IsDisposed)
                {
                    frm2 = new Form2();
                }
                frm2.MdiParent = this;
                frm2.Show();
                frm2.Focus();
            }
        }

        private void 客户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm3 == null)
            {
                frm3 = new Form3();
                frm3.MdiParent = this;
                frm3.Show();
            }
            else
            {
                if (frm3.IsDisposed)
                {
                    frm3 = new Form3();
                }
                frm3.Show();
                frm3.MdiParent = this;
                frm3.Focus();
            }
        }

        private void 客户维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm4 == null)
            {
                frm4 = new Form4();
                frm4.MdiParent = this;
                frm4.Show();
            }
            else
            {
                if (frm4.IsDisposed)
                {
                    frm4 = new Form4();
                }
                frm4.MdiParent = this;
                frm4.Show();
                frm4.Focus();
            }
        }
    }
}
