using System;
using System.Collections;
using System.Drawing;
//download by http://down.liehuo.net
namespace Tetris
{
    class BlockGroup
    {
        private InfoArr info;   //�������ש����ʽ��Ϣ
        private Color disapperColor;//����ɫ
        private int rectPix;//��Ԫ������
        public BlockGroup()//���캯��,����Ա������ֵ
        {
            Config config = new Config();
            config.LoadFromXmlFile();
            info = new InfoArr();
            info =config.Info;
            disapperColor=config.BackColor;
            rectPix = config.RectPix;
        }
        public Block GetABlock()//��ש�����������ȡһ��ש����ʽ������
        {
            Random rd = new Random();   //����һ���������������
            int keyOrder = rd.Next(0, info.Length);//����һ�������,
            BitArray ba = info[keyOrder].ID; //�ѳ�ȡ����ש����ʽ����BitArray�����ba
            int struNum = 0;        //ȷ�����ש����ʽ�б���䷽��ĸ���
            foreach (bool b in ba)//����Ҫȷ��Point����ĳ���
            {
                if (b)
                {
                    struNum++;
                }
            }       //��Ϊc#������̬�ı����鳤��,������֮ǰ�ȵõ����鳤��,Ȼ���ٸ�ֵ;
            Point[] structArr = new Point[struNum]; //�½�һ��Point����,��ȷ���䳤��,�Դ����µ�Block
            int k = 0;
            for (int j = 0; j < ba.Length; j++)//y��ѭ����Point ����structArr������ֵ
            {
                if (ba[j])
                {
                    structArr[k].X = j / 5 - 2;
                    structArr[k].Y = 2 - j % 5;
                    k++;
                }
            }
            return new Block(structArr, info[keyOrder].BColor, disapperColor, rectPix);//����һ����ש�鲢����
        }
    }
}
