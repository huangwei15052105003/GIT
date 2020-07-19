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
                //��ѡ����ʱ,��ȥһ�к�,����,����������,
                //�ػ���¼�û��д��ȷ??????????????????????? �������ôд??
                //������E-mail:myroom110@163.com
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
            //��ȡxml�ļ��еĲ���������Ϣ,��һ�θ���˽�г�Ա����
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
            //���ݻ���ĳ��ȺͿ����Ϣ��̬�ı䴰�弰����Ĵ�С
            this.Width = paletteWidth * rectPix + 186;
            this.Height = paletteHeight * rectPix + 72;
            pbRun.Width = paletteWidth * rectPix;
            pbRun.Height = paletteHeight * rectPix;
        }

        private void FrmTetris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32)//���λس���
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
            if (btnPause.Text == "��ͣ")
            {
                p.Pause();
                btnPause.Text = "����";
            }
            else
            {
                p.EndPause();
                btnPause.Text = "��ͣ";
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "��ͣ")
            {
                btnPause.PerformClick();//???
            }
            using (FrmMode frm = new FrmMode())//???????����ô�����ô��������д��FrmMode����Ƶ����FrmConfig....
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
            ofdialog.Filter = "Mp3(�ļ�*.mp3)|*.mp3|Audio�ļ�(*.avi)|*.avi|VCD�ļ�(*dat)|*.dat|WAV�ļ�(*.wav)|*.wav|�����ļ�(*.*)|*.*";
            ofdialog.DefaultExt = "*.mp3";
            if (ofdialog.ShowDialog() == DialogResult.OK)
            {
                this.axWindowsMediaPlayer1.URL = ofdialog.FileName;
            }
        }

       
    }
}