using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Timers;
//download by http://down.liehuo.net
namespace Tetris
{
    class Palette
    {
        private int _width = 15; //������
        private int _height = 25;//����߶�
        private Color[,] coorArr;//�̶�ש������
        private Color disapperColor; //������ɫ
        private Graphics gpPalette; //ש������
        private Graphics gpReady; //��һ��ש����ʽ����
        private BlockGroup bGroup;//ש��������
        private Block runBlock; //���ڻ��ש��
        private Block readyBlock;   //��һ��ש��
        private int rectPix;//��Ԫ������

        private System.Timers.Timer timerBlock;//��ʱ��,
        private int timeSpan = 300;//��ʱ��ʱ����

        public Palette(int x, int y, int pix, Color dColor, Graphics gp, Graphics gr)//����
        {
            _width = x;
            _height = y;
            coorArr = new Color[_width, _height];
            disapperColor = dColor;
            gpPalette = gp;
            gpReady = gr;
            rectPix = pix;
        }
        public void Start() //��Ϸ��ʼ
        {
            bGroup = new BlockGroup();//�½�ש��������
            runBlock = bGroup.GetABlock();//ȡһ��ש��
            runBlock.XPos = _width / 2;//�³�ש���ˮƽλ��
            int y = 0; //��ֱλ��
            for (int i = 0; i < runBlock.Length; i++)//��������,Ѱ��y���ֵ
            {
                if (runBlock[i].Y > y)
                {
                    y = runBlock[i].Y;
                }
            }
            runBlock.YPos = y;
            gpPalette.Clear(disapperColor);//��ջ���
            runBlock.Paint(gpPalette);//������ש��
            Thread.Sleep(20);   //���ε���ȡ�����,���ʱ��̫�̵Ļ�,������ͬ,���������̹߳���һ��ʱ��
                                //����������Ҳ���Զ�����������

            readyBlock = bGroup.GetABlock(); //��ȡһ��ש��,����readyBlock
            readyBlock.XPos = 2;//��ʾreadyBlock��ש����5*5��lbl,������2,2
            readyBlock.YPos = 2;
            gpReady.Clear(disapperColor);
            readyBlock.Paint(gpReady);//��lblReady�ϻ����´γ��ֵ�ש��
            //��ʼ����������ʱ��
            timerBlock = new System.Timers.Timer(timeSpan);
            timerBlock.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);//ʹ��ί��,ִ��OnTimedEvent
            timerBlock.AutoReset = true;  //ÿ��timeSpan,��ִ��OnTimedEvent,�������false,ִֻ��һ��,
            timerBlock.Start();
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            CheckAndOverBlock();
            Down();
        }
        public bool Down()
        {
            int xPos = runBlock.XPos;
            int yPox = runBlock.YPos + 1;
            for (int i = 0; i < runBlock.Length; i++)
            {
                if (yPox - runBlock[i].Y > _height - 1)//����������,
                    return false;
                if (!coorArr[xPos+ runBlock[i].X ,yPox- runBlock[i].Y].IsEmpty)//��������ж�����ס,
                    return false;
            }
            runBlock.erase(gpPalette);//����ԭ��λ�õ�ש��
            runBlock.YPos++;
            runBlock.Paint(gpPalette);//����λ���ϻ�ש��
            return true;
        }
        public void Drop()  //����ש��
        {
            timerBlock.Stop();
            while (Down()) ;
            timerBlock.Start();
        }
        public void MoveLeft()
        {
            int xPos = runBlock.XPos - 1;
            int yPos = runBlock.YPos;
            for (int i = 0; i < runBlock.Length; i++)
            {
                if (xPos + runBlock[i].X < 0)
                    return;
                if (!coorArr[xPos + runBlock[i].X, yPos - runBlock[i].Y].IsEmpty)
                    return;
            }
            runBlock.erase(gpPalette);
            runBlock.XPos--;
            runBlock.Paint(gpPalette);
        }
        public void MoveRight()     //������ת
        {
            int xPos = runBlock.XPos + 1;
            int yPos = runBlock.YPos;
            for (int i = 0; i < runBlock.Length; i++)
            {
                if (xPos + runBlock[i].X > _width - 1)  //����������ұ߽�����תʧ��
                    return;
                if (!coorArr[xPos + runBlock[i].X, yPos - runBlock[i].Y].IsEmpty)   
                    return;
            }
            runBlock.erase(gpPalette);
            runBlock.XPos++;
            runBlock.Paint(gpPalette);
        }
        public void DeasilRotate()//˳ʱ����ת
        {
            for (int i = 0; i < runBlock.Length; i++)
            {
                int x = runBlock.XPos + runBlock[i].Y;
                int y = runBlock.YPos + runBlock[i].X;
                if (x < 0 || x > _width - 1)//����������ұ߽�����תʧ��(����ת)
                    return;
                if (y < 0 || y > _height - 1)//����������±߽�����תʧ��
                    return;
                if (!coorArr[x, y].IsEmpty)//�����ת���λ����ש������תʧ��;
                    return;
            }
            runBlock.erase(gpPalette);
            runBlock.DeasilRotate();
            runBlock.Paint(gpPalette);
        }

        public void ContraRotate()  //˳ʱ����ת
        {
            for (int i = 0; i < runBlock.Length; i++)
            {
                int x = runBlock.XPos - runBlock[i].Y;
                int y = runBlock.YPos - runBlock[i].X;
                if (x < 0 || x > _width - 1)
                    return;
                if (y < 0 || y > _height - 1)
                    return;
                if (!coorArr[x, y].IsEmpty)
                    return;
            }
            runBlock.erase(gpPalette);
            runBlock.ContraRotate();
            runBlock.Paint(gpPalette);
        }
        private void PaintBackground(Graphics gp)//�ػ�����ı���
        {
            gp.Clear(Color.Black);//������ջ���
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (!coorArr[j, i].IsEmpty)
                    {
                        SolidBrush sb = new SolidBrush(coorArr[j, i]);
                        gp.FillRectangle(sb, j * rectPix + 1,
                            i * rectPix + 1,
                            rectPix - 2,
                            rectPix - 2);
                    }
                }
            }
        }

        public void PaintPalette(Graphics gp)   //�ػ���������
        {
            PaintBackground(gp);    //�Ȼ�����

            if (runBlock != null)   //�ٻ����ש��
            {
                runBlock.Paint(gp);
            }
        }

        public void PaintReady(Graphics gp)//��lblReady�ػ���һ��ש��
        {
            if (readyBlock != null)
            {
                readyBlock.Paint(gp);
            }
        }
          public void CheckAndOverBlock()
         {
             bool over = false;
             for (int i = 0; i < runBlock.Length; i++)
             {
                 int x = runBlock.XPos + runBlock[i].X;
                 int y = runBlock.YPos - runBlock[i].Y;
                 if (y == _height - 1)
                 {
                     over = true;
                     break;
                 }
                 if (!coorArr[x, y + 1].IsEmpty)
                 {
                     over = true;
                     break;
                 }
             }
             if (over)//���ȷ����ǰש���ѽ���
             {
                 for (int i = 0; i < runBlock.Length; i++)  //�ѵ�ǰש�����coordinateArr�̶�
                 {
                     coorArr[runBlock.XPos + runBlock[i].X, runBlock.YPos - runBlock[i].Y] = runBlock.BlockColor;
                 }
                  //����Ƿ����������,�������ɾ��
                 CheckAndDelFullRow();
                //�����µ�ש��
                 runBlock = readyBlock; //�µ�ש��Ϊ׼���õ�ש��
                 runBlock.XPos = _width / 2;    //
                 int y = 0;
                 for (int i = 0; i < runBlock.Length; i++)
                 {
                     if (runBlock[i].Y > y)
                     {
                         y = runBlock[i].Y;
                     }
                 }
                 runBlock.YPos = y;
                 //�����Ǽ���³���ש����ռ�õĵط��Ƿ�����������ש��,���������Ϸ����,
                 for (int i = 0; i < runBlock.Length; i++)
                 {
                     if (!coorArr[runBlock.XPos + runBlock[i].X, runBlock.YPos - runBlock[i].Y].IsEmpty)
                     {
                         StringFormat drawFormat = new StringFormat();
                         drawFormat.Alignment = StringAlignment.Center;
                         gpPalette.DrawString("GAME OVER",
                             new Font("Arial Black", 25f),
                             new SolidBrush(Color.White),
                             new RectangleF(0, _height * rectPix / 2 - 100, _width * rectPix, 100),
                             drawFormat);
                         timerBlock.Stop();//��Ϸ����,�رն�ʱ��
                         return;
                     }
                 }
                 runBlock.Paint(gpPalette);
                 //��ȡ�µ�׼��ש��
                 readyBlock = bGroup.GetABlock();
                 readyBlock.XPos = 2;
                 readyBlock.YPos = 2;
                 gpReady.Clear(Color.Black);
                 readyBlock.Paint(gpReady);
             }
         }

         private void CheckAndDelFullRow()  //��鲢��������
         {
                //�ҳ���ǰש�������з�Χ
             int lowRow = runBlock.YPos - runBlock[0].Y;//lowRow����ǰש���y����Сֵ
             int highRow = lowRow;      //highRow����ǰש���y������ֵ
             for (int i = 1; i < runBlock.Length; i++)  //�ҳ���ǰש����ռ�еķ�Χ,����low/high������
             {
                 int y = runBlock.YPos - runBlock[i].Y;
                 if (y < lowRow)
                 {
                     lowRow = y;
                 }
                 if (y > highRow)
                 {
                     highRow = y;
                 }
             }
             bool repaint = false;//��ɾ�е��ػ���־
             for (int i = lowRow; i <= highRow; i++)    //�������,�����,��ɾ֮,
             {
                 bool rowFull = true;
                 for (int j = 0; j < _width; j++)   //�ж��Ƿ�����
                 {
                     if (coorArr[j, i].IsEmpty) //�����һ����Ԫ���,��˵������,
                     {
                         rowFull = false;
                         break;
                     }
                 }
                 if (rowFull)   //if��,��ɾ֮
                 {
                     repaint = true;    //ɾ���Ļ�.�����,�ػ�,
                     for (int k = i; k > 0; k--)    //�ѵ�n�е�ֵ��n-1������,(�³�һ��)
                     {
                         for (int j = 0; j < _width; j++)
                         {
                             coorArr[j, k] = coorArr[j, k - 1];
                         }

                     }
                     for (int j = 0; j < _width; j++)   //��յ�0��
                     {
                         coorArr[j, 0] = Color.Empty;
                     }
                     //����������Ϊ����ӵĴ����¼�����
                   //  sea.Score += 100; //���¼����������Score�ֶ�����100��
                   //  OnScoreInc(); //�������������¼�
                    // timeeblock();
                 }

             }
             if (repaint)   //�ػ��־true.��^��֮(��֮ǰ�����,�Ѹĺ���Գ�!)
             {
                 PaintBackground(gpPalette);
             }
         }

         public void Pause()//��ͣ,
         {
             if (timerBlock.Enabled == true)
             {
                 timerBlock.Enabled = false;
             }
         }

         public void EndPause()
         {
             if (timerBlock.Enabled == false)
             {
                 timerBlock.Enabled = true;
             }
         }

         public void Close()//�ر�
         {
             timerBlock.Close();//�رն�ʱ��
             gpPalette.Dispose();//�ͷŻ���
             gpReady.Dispose();
         }
        
    }
}
        