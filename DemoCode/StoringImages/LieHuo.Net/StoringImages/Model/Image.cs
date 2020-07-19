﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// 资源来自：烈火下载 http://down.liehuo.net
namespace Kribo.StoringImages.Model
{
    public class Image
    {
        private string fileName = string.Empty;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private byte[] imageData;
        public byte[] ImageData
        {
            get { return imageData; }
            set { imageData = value; }
        }
        private long fileSize = 0;
        public long FileSize
        {
            get { return fileSize; }
            set {fileSize = value; }
        }
        public Image()
        { }
        public Image(string a, byte[] b, long c)
        {
            this.FileName = a;
            this.ImageData = b;
            this.FileSize = c;
        }
    }
}
