using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmailSendAndReceive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 邮件发送ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSend frmsend = new frmSend();
            frmsend.MdiParent = this;
            frmsend.Show();
        }

        private void 邮件接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReceive frmreceive = new frmReceive();
            frmreceive.MdiParent = this;
            frmreceive.Show();
        }
    }
}