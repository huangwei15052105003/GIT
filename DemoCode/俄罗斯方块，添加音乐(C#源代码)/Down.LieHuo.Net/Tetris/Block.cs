using System;
using System.Drawing;
using System.Text;
using System.Collections;
//download by http://down.liehuo.net
namespace Tetris
{
    class Block
    {
        protected Point[] structArr;    //���ש�������Ϣ����������
        protected int _xPos;    //ש�����ĵ����ڵ�X����
        protected int _yPos;    //ש���������ڵ�y����
        protected Color _blockColor;    //ש����ɫ
        protected Color disapperColor;  //������ɫ
        protected int rectPix;          //ÿ����Ԫ������
        public Block()  //Ĭ�Ϲ��캯��,�����˹��캯����Ϊ�������ܴ���
        {
        }
        public Block(Point[] sa, Color bColor, Color dColor, int pix)
        {       //���ع��캯��,����Ա������ֵ
            _blockColor = bColor;
            disapperColor = dColor;
            rectPix = pix;
            structArr = sa;
        }
        public Point this[int index]
        {
            get
            {
                return structArr[index];
            }
        }
        public int Length   //����,��ʾstructArr�ĳ���
        {
            get
            {
                return structArr.Length;
            }
        }
        #region  �����й�����
        public int XPos
        {
            get
            {
                return _xPos;
            }
            set
            {
                _xPos = value;
            }
        }
        public int YPos
        {
            get
            {
                return _yPos;
            }
            set
            {
                _yPos = value;
            }           
        }
        public Color BlockColor
        {
            get
            {
                return _blockColor;
            }
        }
        #endregion
        public void DeasilRotate()//˳ʱ����ת
        {
            int temp;   //��ת��ʽ:x1 = y     y1=-x
            for (int i = 0; i < structArr.Length; i++)
            {
                temp = structArr[i].X;
                structArr[i].X = structArr[i].Y;
                structArr[i].Y = -temp;
            }
        }
        public void ContraRotate()//��ʱ����ת
        {
            int temp;
            for (int i = 0; i < structArr.Length; i++)
            {
                temp = structArr[i].X;
                structArr[i].X = -structArr[i].Y;
                structArr[i].Y = temp;
            }
        }
        private Rectangle PointToRect(Point p)  //��һ�������ת��Ϊ����������(���ת����)
        {
            return new Rectangle((_xPos + p.X) * rectPix + 1,
                                  (_yPos - p.Y) * rectPix + 1,
                                  rectPix - 2,
                                  rectPix - 2);
        }
        public virtual void Paint(Graphics gp)  //��ָ���������ש��;( �����ƶ�ʱ,��ͣ���ػ� )
        {
            SolidBrush sb = new SolidBrush(_blockColor);
            foreach (Point p in structArr)
            {
                lock (gp)
                {
                    gp.FillRectangle(sb, PointToRect(p));
                }
            }
        }
        public void erase(Graphics gp)//�������� ( �����Ƴ�ʱ,������ʾ����ɫ )
        {
            SolidBrush sb = new SolidBrush(disapperColor);
            foreach (Point p in structArr)
            {
                lock (gp)
                {
                    gp.FillRectangle(sb, PointToRect(p));
                }
            }
            //
        }
    }
}
