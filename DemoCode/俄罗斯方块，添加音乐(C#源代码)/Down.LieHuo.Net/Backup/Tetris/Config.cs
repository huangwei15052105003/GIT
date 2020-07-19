using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Xml;
using System.IO;
using System.Reflection;
//download by http://down.liehuo.net
namespace Tetris
{
    class Config
    {
        private Keys _downKey; //向下键
        private Keys _dropKey; //丢下键
        private Keys _moveLeftKey;  //左移键
        private Keys _moveRightKey; //右移键
        private Keys _deasilRotateKey;//顺时针旋转键
        private Keys _contraRotateKey;//逆时针旋转键
        private int _coorWidth;     //水平格子数
        private int _coorHeight;   //垂直格子数
        private int _rectPix;       //方块像素
        private Color _backColor;   //背景色
        private InfoArr info = new InfoArr();//砖块信息集合
        #region 私用变量相应的属性
        public Keys DownKey
        {
            get
            {
                return _downKey;
            }
            set
            {
                _downKey = value;
            }
        }
        public Keys DropKey
        {
            get
            {
                return _dropKey;
            }
            set
            {
                _dropKey = value;
            }
        }
        public Keys MoveLeftKey
        {
            get
            {
                return _moveLeftKey;
            }
            set
            {
                _moveLeftKey = value;
            }
        }
        public Keys MoveRightKey
        {
            get
            {
                return _moveRightKey;
            }
            set
            {
                _moveRightKey = value;
            }
        }
        public Keys DeasilRotateKey
        {
            get
            {
                return _deasilRotateKey;
            }
            set
            {
                _deasilRotateKey = value;
            }
        }
        public Keys ContraRotateKey
        {
            get
            {
                return _contraRotateKey;
            }
            set
            {
                _contraRotateKey = value;
            }
        }
        public int CoorWidth
        {
            get
            {
                return _coorWidth;
            }
            set
            {
                if (value >= 10 && value <= 50)
                    _coorWidth = value;
            }
        }
        public int CoorHeight
        {
            get
            {
                return _coorHeight;
            }
            set
            {
                if (value >= 15 && value <= 50)
                    _coorHeight = value;
            }
        }
        public int RectPix
        {
            get
            {
                return _rectPix;                
            }
            set
            {
                if (value >= 50 && value <= 30)
                    _rectPix = value;
            }
        }
        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }
        public InfoArr Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }
        #endregion


        public void LoadFromXmlFile()//从xml读取信息
        {
            XmlTextReader reader;
            if (File.Exists("BlockSet.xml"))
            {   //优先读取外部的BlockSet.xml文件并把信息写入blockInfo内;(外部的是玩家保存的信息)
                reader = new XmlTextReader("BlockSet.xml");
            }
            else
            {   //如果BlockSet.xml不存在,则从嵌入资源内读取BlockSet.xml
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream sm = asm.GetManifestResourceStream ("Tetris.BlockSet.xml");
                reader = new XmlTextReader(sm);
            }
            string key = "";
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "ID")
                        {
                            key = reader.ReadElementString().Trim();
                            info.Add(key, "");
                        }
                        else if (reader.Name == "Color")
                        {
                            info[key] = reader.ReadElementString().Trim();
                        }
                        else if (reader.Name == "DownKey")
                        {
                            _downKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "DropKey")
                        {
                            _dropKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "MoveLeftKey")
                        {
                            _moveLeftKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "MoveRightKey")
                        {
                            _moveRightKey =(Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "DeasilRotateKey")
                        {
                            _deasilRotateKey =(Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "ContraRotateKey")
                        {
                            _contraRotateKey =(Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "CoorWidth")
                        {
                            _coorWidth = Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "CoorHeight")
                        {
                            _coorHeight = Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "RectPix")
                        {
                            _rectPix = Convert.ToInt32(reader.ReadElementString().Trim());
                        }

                        else if (reader.Name == "BackColor")
                        {
                            _backColor = Color.FromArgb(Convert.ToInt32(reader.ReadElementString().Trim()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void SaveToXmlFile() //把信息保存为xml文件----若改从数据库读信息则改这两个函数即可;
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<BlockSet></BlockSet>");
            XmlNode root = doc.SelectSingleNode("BlockSet");
            // 写砖块信息
            for (int i = 0; i < info.Length; i++)
            {
                XmlElement xelType = doc.CreateElement("Type");
                XmlElement xelID = doc.CreateElement("ID");
                xelID.InnerText = ((BlockInfo)info[i]).GetIdStr();
                xelType.AppendChild(xelID);
                XmlElement xelColor = doc.CreateElement("Color");
                xelColor.InnerText = ((BlockInfo)info[i]).GetColorStr();
                xelType.AppendChild(xelColor);
                root.AppendChild(xelType);
            }
            //写快捷键信息
            XmlElement xelKey = doc.CreateElement("Key");

            XmlElement xelDownKey = doc.CreateElement("DownKey");
            xelDownKey.InnerText = Convert.ToInt32(_downKey).ToString();
            xelKey.AppendChild(xelDownKey);

            XmlElement xelDropKey = doc.CreateElement("DropKey");
            xelDropKey.InnerText = Convert.ToInt32(_dropKey).ToString();
            xelKey.AppendChild(xelDropKey);


            XmlElement xelMoveLeftKey = doc.CreateElement("MoveLeftKey");
            xelMoveLeftKey.InnerText = Convert.ToInt32(_moveLeftKey).ToString();
            xelKey.AppendChild(xelMoveLeftKey);

            XmlElement xelMoveRightKey = doc.CreateElement("MoveRightKey");
            xelMoveRightKey.InnerText = Convert.ToInt32(_moveRightKey).ToString();
            xelKey.AppendChild(xelMoveRightKey);

            XmlElement xelDeasilRotateKey = doc.CreateElement("DeasilRotateKey");
            xelDeasilRotateKey.InnerText = Convert.ToInt32(_deasilRotateKey).ToString();
            xelKey.AppendChild(xelDeasilRotateKey);

            XmlElement xelContraRotateKey = doc.CreateElement("ContraRotateKey");
            xelContraRotateKey.InnerText = Convert.ToInt32(_contraRotateKey).ToString();
            xelKey.AppendChild(xelContraRotateKey);

            root.AppendChild(xelKey);
            //写界面信息
            XmlElement xelSurface = doc.CreateElement("Surface");

            XmlElement xelCoorWidth = doc.CreateElement("CoorWidth");
            xelCoorWidth.InnerText = _coorWidth.ToString();
            xelSurface.AppendChild(xelCoorWidth);

            XmlElement xelCoorHeight = doc.CreateElement("CoorHeight");
            xelCoorHeight.InnerText = _coorHeight.ToString();
            xelSurface.AppendChild(xelCoorHeight);

            XmlElement xelRectPix = doc.CreateElement("RectPix");
            xelRectPix.InnerText = _rectPix.ToString();
            xelSurface.AppendChild(xelRectPix);

            XmlElement xelBackColor = doc.CreateElement("BackColor");
            xelBackColor.InnerText = _backColor.ToArgb().ToString();
            xelSurface.AppendChild(xelBackColor);

            root.AppendChild(xelSurface);

            doc.Save("BlockSet.xml");
        }


    }
}
