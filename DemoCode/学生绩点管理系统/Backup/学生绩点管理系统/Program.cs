using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//download by http://down.liehuo.net
namespace 学生绩点管理系统
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
