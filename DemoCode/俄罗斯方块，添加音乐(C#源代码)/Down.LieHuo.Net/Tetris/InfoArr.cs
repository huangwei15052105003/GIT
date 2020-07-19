using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
//download by http://down.liehuo.net
namespace Tetris
{
    class InfoArr
    {
        private ArrayList info = new ArrayList();//��Ŷ��BlockInfo������鳤
        private int _length = 0;//���ArrayList�ĳ���,�Թ�����
        public int Length
        {
            get
            {
                return _length;
            }
        }
        public BlockInfo this[int index]    //������,�����±�,����һ��BlockInfo
        {
            get
            {
                return (BlockInfo)info[index];
            }
        }
        public string this[string id]   //������,����һ���ַ�����idֵ�±�,����Ӧid����ɫ��ֵ
        {
            set 
            {
                if (value == "")
                {
                    return;
                }
                for (int i = 0;i<info.Count;i++)
                {
                    if(((BlockInfo)info[i]).GetIdStr() == id)    //???
                    {
                        try
                        {
                            ((BlockInfo)info[i]).BColor = Color.FromArgb(Convert.ToInt32(value));
                        }
                        catch (System.FormatException)
                        {
                            MessageBox.Show("��ɫ��Ϣ����!��ɾ��BlockSet.xml�ļ�,��������������","������Ϣ",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private BitArray StrToBit(string id)//���ַ���ת��ΪBitArry (����)
        {
            if (id.Length !=25)
            {
                throw new System.FormatException("ש����ʽ��Ϣ���Ϸ�����ɾ���£����ӣ����������ļ�����������������");
            }
            BitArray ba = new BitArray(25);
            for (int i= 0;i<25;i++)
            {
                ba[i] = (id[i] == '0')? false : true;
            }
            return ba;
        }
       public void Add(BitArray id, Color bColor) //���һ��ש����Ϣ 
       {
           if (id.Length != 25 )
           {
               throw new System.FormatException("ש����ʽ��Ϣ���Ϸ�����ɾ���£����ӣ����������ļ�����������������");
           }
           info.Add(new BlockInfo (id,bColor));//����̬����info���һ��ש����Ϣ
           _length ++;
       }
        public void Add(string id,string bColor)//����Add,��������(�����ַ���,����ש�������Ϣ)������
        {
            Color temp;
            if (! (bColor == ""))
            {
                temp = Color.FromArgb(Convert.ToInt32(bColor));//���ַ���ת��Ϊ��ɫ��
            }
            else 
            {
                temp = Color.Empty;
            }
            info.Add(new BlockInfo(StrToBit(id), temp));//���ַ���ת��ΪBitArray��
            _length++;

        }
    }
}
