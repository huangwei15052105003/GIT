using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




using System.Data.OleDb;
using System.Data;

using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace exposureRecipeNet3
{
    public static class PingService
    {
        private const int TIME_OUT = 4000;

        private const int PACKET_SIZE = 512;

        private const int TRY_TIMES = 3;

        //检查时间的正则

        private static Regex _reg = new Regex(@"时间=(.*?)ms", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        /// <summary>

        /// 结果集

        /// </summary>

        /// <param name="stringcommandLine">字符命令行</param>

        /// <param name="packagesize">丢包大小</param>

        /// <returns></returns>
        public static string LauchPing(string stringcommandLine, int packagesize)

        {

            Process process = new Process();

            process.StartInfo.Arguments = stringcommandLine;

            process.StartInfo.UseShellExecute = false;

            process.StartInfo.CreateNoWindow = true;

            process.StartInfo.FileName = "ping.exe";

            process.StartInfo.RedirectStandardInput = true;

            process.StartInfo.RedirectStandardOutput = true;

            process.StartInfo.RedirectStandardError = true;

            process.Start();

            return process.StandardOutput.ReadToEnd();//返回结果



        }
        /// <summary>

        /// 转换字节

        /// </summary>

        /// <param name="strBuffer">缓冲字符</param>

        /// <param name="packetSize">丢包大小</param>

        /// <returns></returns>
        private static float ParseResult(string strBuffer, int packetSize)

        {

            if (strBuffer.Length < 1)

                return 0.0F;

            MatchCollection mc = _reg.Matches(strBuffer);

            if (mc == null || mc.Count < 1 || mc[0].Groups == null)

                return 0.0F;

            if (!int.TryParse(mc[0].Groups[1].Value, out int avg))

                return 0.0F;

            if (avg <= 0)

                return 1024.0F;

            return (float)packetSize / avg * 1000 / 1024;

        }

        /// <summary>

        /// 通过Ip或网址检测调用Ping 返回 速度

        /// </summary>,

        /// <param name="strHost"></param>

        /// <returns></returns>

        public static string Test(string strHost, int trytimes, int PacketSize, int TimeOut)

        {

            return LauchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, trytimes, PacketSize, TimeOut), PacketSize);

        }

        /// <summary>

        /// 地址

        /// </summary>

        /// <param name="strHost"></param>

        /// <returns></returns>
        public static string Test(string strHost)

        {

            return LauchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TRY_TIMES, PACKET_SIZE, TIME_OUT), PACKET_SIZE);

        }
        public static Dictionary<string,string> NikonAsml()
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            Dictionary<string, string> tool = new Dictionary<string, string>();
            tool.Add("ALSIB1","10.4.152.230");
            tool.Add("ALSIB2", "10.4.152.231");
            tool.Add("ALSIB3", "10.4.152.229");
            tool.Add("ALSIB4", "10.4.152.228");
            tool.Add("ALSIB5", "10.4.152.227");
            tool.Add("ALSIB6", "10.4.152.226");
            tool.Add("ALSIB7", "10.4.152.225");
            tool.Add("ALSIB8", "10.4.152.224");
            tool.Add("ALSIB9", "10.4.152.223");
            tool.Add("ALSIBA", "10.4.152.222");
            tool.Add("ALSIBB", "10.4.152.221");
            tool.Add("ALSIBC", "10.4.152.220");
            tool.Add("ALSIBD", "10.4.152.219");
            tool.Add("ALSIBE", "10.4.152.218");
            tool.Add("ALSIBF", "10.4.152.217");
            tool.Add("ALSIBG", "10.4.152.216");
            tool.Add("ALSIBH", "10.4.152.210");
            tool.Add("ALSIBI", "10.4.152.211");
            tool.Add("ALSIBJ", "10.4.152.212");
            tool.Add("BLSIBK", "10.4.131.30");
            tool.Add("BLSIBL", "10.4.131.31");
            tool.Add("BLSIE1", "10.4.131.75");
            tool.Add("BLSIE2", "10.4.131.78");
            tool.Add("SerNik", "10.4.72.145");
            tool.Add("ALSD82", "10.4.152.29");
            tool.Add("ALSD83", "10.4.151.64");
            tool.Add("ALSD85", "10.4.152.37");
            tool.Add("ALSD86", "10.4.152.31");
            tool.Add("ALSD87", "10.4.152.46");
            tool.Add("ALSD89", "10.4.152.39");
            tool.Add("ALSD8A", "10.4.152.42");
            tool.Add("ALSD8B", "10.4.152.47");
            tool.Add("ALSD8C", "10.4.152.48");
            tool.Add("BLSD7D", "10.4.131.32");
            tool.Add("BLSD08", "10.4.131.63");
            tool.Add("SerAsm", "10.4.72.253");

            string rValue;
            foreach (KeyValuePair<string, string> kvp in tool)
            {
                rValue = Test(kvp.Value);
                myDic.Add(kvp.Key, rValue);
              
            }
               return myDic;
        }
    }
}
