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
            Graphics gp = e.Graphics;         //���廭��
            gp.Clear(Color.Black);
            Pen p = new Pen(Color.White);
            for (int i = 31; i < 155; i += 31)//�������
                gp.DrawLine(p, 1, i, 156, i);
            for (int i = 31; i < 155; i += 31)//��������
                gp.DrawLine(p, i, 1, i, 156);
            //
            //�����������еķ���
            SolidBrush s = new SolidBrush(blockColor);
            for ( int x = 0 ;x < 5 ; x++ )
            {
                for(int y = 0 ; y < 5 ; y++ )
                {
                    if (struArr[x, y])          //�ж��Ƿ�ѡ�У�����
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
            int xPos, yPos;     //�����±�
            xPos = e.X / 31;    //X��Y����д����������൱�ڸ��ؼ���λ��(�����λ��ת��Ϊ�����±�)
            yPos = e.Y / 31;
            struArr[xPos, yPos] = !struArr[xPos, yPos];
            bool b = struArr[xPos, yPos];
            Graphics gp = lblMode.CreateGraphics();//�õ�label1��Graphics
            SolidBrush s = new SolidBrush(b ? blockColor : Color.Black);
            //
            gp.FillRectangle(s, 31 * xPos + 1, 31 * yPos + 1, 30, 30);
            gp.Dispose();       //�ͷ�gp(���ͷŻᡭ��label_Paint()��ôû���ͷţ�)��:CreateGraphics()���ɵı���
            //��ʾ�Ĺر�.
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();//����ɫ�Ի���
            blockColor = colorDialog1.Color;    //�����ɫΪѡ����ɫ
            lblColor.BackColor = blockColor;
            lblMode.Invalidate();//?????????ʹ��lblMode�ػ�����ִ��Print�¼�������ǩ����ڲ�����
        }//�����Զ�����Print()����

        private void butAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;//���Ȳ���ͼ���Ƿ�Ϊ��
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
                MessageBox.Show("ͼ��Ϊ�գ��������������ߴ��ڻ���ͼ����", "��ʾ����",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            //�ѷ���ת���ɶ������ַ�����
            StringBuilder sb = new StringBuilder(25);
            foreach (bool i in struArr)
            {
                sb.Append(i ? "1" : "0");//���ַ�����������ַ� 
            }
            string blockString = sb.ToString();
            //�ټ���Ƿ����ظ�ͼ��
            foreach (ListViewItem item in lsvBlockSet.Items)
            {
                if (item.SubItems[0].Text == blockString)
                {
                    MessageBox.Show("��ͼ���Ѿ����ڣ�","��ʾ����",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                    return;
                }

            }
            ListViewItem myItem = new ListViewItem();
            myItem = lsvBlockSet.Items.Add(blockString);
            myItem.SubItems.Add(Convert.ToString(blockColor.ToArgb()));//�ڵڶ��������ɫ,��Ӧ����myItem.SubItem[1].Add();���ĸ�ʽ��?????

        }

        private void lsvBlockSet_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)//�����ظ�ִ���¼������ж��¼��ǲ��Ǳ�ѡ�У�����:���Itemʱ,��lblmode������ʾѡ�е���Ϣ
            {
                blockColor = Color.FromArgb(int.Parse(e.Item.SubItems[1].Text));//��ȡ��ɫ��
                lblColor.BackColor = blockColor;
                string s = e.Item.SubItems[0].Text;
                for (int i = 0; i < s.Length; i++) //ȡש����ʽ��Ϣ
                {
                    struArr[i / 5, i % 5] = (s[i] == '1') ? true : false;
                }
                lblMode.Invalidate();//�ػ棡����
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)//û�б�ѡ�С�
            {
                MessageBox.Show("�����ұߴ���ѡ��һ����Ŀ����ɾ����","��ʾ����",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                return;
            }
            lsvBlockSet.Items.Remove(lsvBlockSet.SelectedItems[0]);//ɾ����ɾ������Ŀ.�±�0 �������ܱ�ѡ�ж��
            btnClear.PerformClick();//ִ��btnClear_Click�¼�������������
        }

        private void btnClear_Click(object sender, EventArgs e)//��հ�ť
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    struArr[x, y] = false;
                }
            }
            lblMode.Invalidate();//Ϊʲô��ֱ�ӵ���lblMode_Paint()��?????
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lsvBlockSet.SelectedItems.Count == 0)//�ж��Ƿ�ѡ�С�
            {
                MessageBox.Show("�����ұߴ���ѡ��һ����Ŀ����ɾ����", "��ʾ����",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                return;
            }
            bool isEmpty = false;//���Ȳ���ͼ���Ƿ�Ϊ��
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
                MessageBox.Show("ͼ��Ϊ�գ��������������ߴ��ڻ���ͼ����", "��ʾ����",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            StringBuilder sb = new StringBuilder(25);
            foreach (bool i in struArr)//��ͼ����Ϣת��Ϊ��������Ϣ
            {
                sb.Append(i ? "1" : "0");
            }
            lsvBlockSet.SelectedItems[0].SubItems[0].Text = sb.ToString();//�ı�ͼ����Ϣ
            lsvBlockSet.SelectedItems[0].SubItems[1].Text = Convert.ToString(blockColor.ToArgb());//�ı���ɫ��Ϣ

        }

        private void txtContra_KeyDown(object sender, KeyEventArgs e)//��ü��̰����¼���// ��δ��룬���͵ļ����¼�
        {   
            //�ų�һЩ���ʺ�����ݼ��Ŀ����ַ���
            if ((e.KeyValue >= 33 && e.KeyValue <= 36) || (e.KeyValue >= 45 && e.KeyValue <= 46
                ) ||
                (e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 65 && e.KeyValue <= 90) ||
                (e.KeyValue >= 96 && e.KeyValue <= 107) || (e.KeyValue >= 109 && e.KeyValue <= 111) ||
                (e.KeyValue >= 186 && e.KeyValue <= 192) || (e.KeyValue >= 219 && e.KeyValue <= 222))
            {
                //����Ƿ���ڳ�ͻ�Ŀ�ݼ�
                foreach (Control c in gbKeySet.Controls)//��Control���c������һ��groupBox�������Controls��
                {
                    Control TempC = c as TextBox;   //c as TextBox!c��ΪTextBox
                    if (TempC != null && ((TextBox)TempC).Text != "")//textbox��Ϊ��ʱ((TextBox)TempC).Text !=""����Ϊ��ʱ��
                        //��Ϊǿ������ת����ʱ��ת����object��ʱΪ��,��ת����intʱ����ֵ�����Գ���
                    {
                        if (((int)((TextBox)TempC).Tag) == e.KeyValue)//����ǿ������ת����
                        {//ÿ��TextBox����һ��Tag,��������ࡣ
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
            lblMode.Invalidate();//?????????ʹ��lblMode�ػ�����ִ��Print�¼�����(�Լ�û�ж��尡)��ǩ����ڲ�����()
        }

        private void FrmMode_Load(object sender, EventArgs e)
        {
            config.LoadFromXmlFile(); //��ȡxml�ļ�
            InfoArr info = config.Info;
            //��ȡש����ʽ
            ListViewItem myItem = new ListViewItem();
            for (int i = 0; i < info.Length; i++)
            {
                myItem = lsvBlockSet.Items.Add(info[i].GetIdStr());
                myItem.SubItems.Add(info[i].GetColorStr());
            }
            //��ȡ��ݼ�
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
            //��ȡ�������ò���
            txtCoorWidth.Text = config.CoorWidth.ToString();
            txtCoorheight.Text = config.CoorHeight.ToString();
            txtRectPix.Text = config.RectPix.ToString();
            lblBackColor.BackColor = config.BackColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InfoArr info = new InfoArr();
            foreach (ListViewItem item in lsvBlockSet.Items)//��lsvBlackSet�ڶ�ȡש����Ϣ,������info��
            {
                info.Add(item.SubItems[0].Text, item.SubItems[1].Text);
            }
            config.Info = info;//��info����config�����Info����
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
            config.SaveToXmlFile();//�����xml�ļ�
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}