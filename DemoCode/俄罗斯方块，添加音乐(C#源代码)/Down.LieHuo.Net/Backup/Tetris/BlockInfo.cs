using System;
using System.Collections;
using System.Text;
using System.Drawing;
//download by http://down.liehuo.net
namespace Tetris
{
    class BlockInfo//存放Block信息,
    {
        private BitArray _id;//存放砖块样式信息
        private Color _bColor;//存放颜色信息
        public BlockInfo(BitArray id, Color bColor)//构造函数
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
        public string GetIdStr()//把样式转换成字符串
        {
            StringBuilder s = new StringBuilder(25);
            foreach (bool b in _id)
            {
                s.Append(b ? "1" : "0");
            }
            return s.ToString();
        }
        public string GetColorStr()//获得颜色的字符串信息
        {
            return Convert.ToString(_bColor.ToArgb());
        }
            
    }
}
