using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
//download by http://down.liehuo.net
namespace Tetris
{
    public partial class FrmTetris : Form
    {
        public FrmTetris()
        {
            InitializeComponent();
        }

        private Palette p;
        private Keys downKey;
        private Keys dropKey;
        private Keys moveLeftKey;
        private Keys moveRightKey;
        private Keys deasilRotateKey;
        private Keys contraRotateKey;
        private int paletteWidth;
        private int paletteHeight;
        private Color paletteColor;
        private int rectPix;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (p != null)
            {
                p.Close();
            }
            p = new Palette(paletteWidth, paletteHeight, rectPix, paletteColor,
                            Graphics.FromHwnd(pbRun.Handle),
                            Graphics.FromHwnd(lblReady.Handle));
            p.Start();

        }       
        private void pbRun_Paint(object sender, PaintEventArgs e)
        {
            if (p != null)
            {
                p.PaintPalette(e.Graphics);
                //自选背景时,消去一行后,出错,可能是这里,
                //重绘的事件没有写正确??????????????????????? 具体改怎么写??
                //告诉我E-mail:myroom110@163.com
            }
        }

        private void lblReady_Paint(object sender, PaintEventArgs e)
        {
            if (p != null)
            {
                p.PaintReady(e.Graphics);
            }
        }

        private void FrmTetris_Load(object sender, EventArgs e)
        {
            //读取xml文件中的参数配置信息,并一次赋给私有成员变量
            Config config = new Config();
            config.LoadFromXmlFile();
            downKey = config.DownKey;
            dropKey = config.DropKey;
            moveLeftKey = config.MoveLeftKey;
            moveRightKey = config.MoveRightKey;
            deasilRotateKey = config.DeasilRotateKey;
            contraRotateKey = config.ContraRotateKey;
            paletteWidth = config.CoorWidth;
            paletteHeight = config.CoorHeight;
            paletteColor = config.BackColor;
            rectPix = config.RectPix;
            //根据画板的长度和宽度信息动态改变窗体及画板的大小
            this.Width = paletteWidth * rectPix + 186;
            this.Height = paletteHeight * rectPix + 72;
            pbRun.Width = paletteWidth * rectPix;
            pbRun.Height = paletteHeight * rectPix;
        }

        private void FrmTetris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32)//屏蔽回车键
            {
                e.Handled = true;
            }
            if (e.KeyCode == downKey)
            {
                p.Down();
            }
            else if (e.KeyCode == dropKey)
            {
                p.Drop();
            }
            else if (e.KeyCode == moveLeftKey)
            {
                p.MoveLeft();
            }
            else if (e.KeyCode == moveRightKey)
            {
                p.MoveRight();
            }
            else if (e.KeyCode == deasilRotateKey)
            {
                p.DeasilRotate();
            }
            else if (e.KeyCode == contraRotateKey)
            {
                p.ContraRotate();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (p == null)
            {
                return;
            }
            if (btnPause.Text == "暂停")
            {
                p.Pause();
                btnPause.Text = "继续";
            }
            else
            {
                p.EndPause();
                btnPause.Text = "暂停";
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "暂停")
            {
                btnPause.PerformClick();//???
            }
            using (FrmMode frm = new FrmMode())//???????我怎么把配置窗体的名字写成FrmMode而视频上是FrmConfig....
            {
                frm.ShowDialog();
                
                
            }
        }

        private void FrmTetris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p != null)
            {
                p.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdialog = new OpenFileDialog();
            ofdialog.AddExtension = true;
            ofdialog.CheckFileExists = true;
            ofdialog.CheckPathExists = true;
            ofdialog.Filter = "Mp3(文件*.mp3)|*.mp3|Audio文件(*.avi)|*.avi|VCD文件(*dat)|*.dat|WAV文件(*.wav)|*.wav|所有文件(*.*)|*.*";
            ofdialog.DefaultExt = "*.mp3";
            if (ofdialog.ShowDialog() == DialogResult.OK)
            {
                this.axWindowsMediaPlayer1.URL = ofdialog.FileName;
            }
        }

       
    }
}