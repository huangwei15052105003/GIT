using System;
using System.Collections;
using System.Text;
using System.Drawing;
//download by http://down.liehuo.net
namespace Tetris
{
    class BlockInfo//���Block��Ϣ,
    {
        private BitArray _id;//���ש����ʽ��Ϣ
        private Color _bColor;//�����ɫ��Ϣ
        public BlockInfo(BitArray id, Color bColor)//���캯��
        {
            _id = id;
            _bColor = bColor;
        }
        public BitArray ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public Color BColor
        {
            get
            {
                return _bColor;
            }
            set
            {
                _bColor = value;
            }        
        }
        public string GetIdStr()//����ʽת�����ַ���
        {
            StringBuilder s = new StringBuilder(25);
            foreach (bool b in _id)
            {
                s.Append(b ? "1" : "0");
            }
            return s.ToString();
        }
        public string GetColorStr()//�����ɫ���ַ�����Ϣ
        {
            return Convert.ToString(_bColor.ToArgb());
        }
            
    }
}
