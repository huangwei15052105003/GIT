﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
//Download by http://www.codefans.net
namespace Ex09_60
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}