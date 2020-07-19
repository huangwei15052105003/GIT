using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class CommonHelper
    {
        //跨程序集里面怎么会取不到值。抽象工厂那边在测试。
        public static string DllPath { get; set; }
    }
}
