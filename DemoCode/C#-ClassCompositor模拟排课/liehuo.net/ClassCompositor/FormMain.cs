using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//download by http://down.liehuo.net
using System.IO;

namespace ClassCompositor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 读取主数据(默认使用UTF-8编码)
            string[] recLines = File.ReadAllLines(Application.StartupPath + "\\classList.txt");

            // 分析数据并存储于listview中
            listView_allClasses.Items.Clear();
            ListViewItem newItem;
            string[] subItems;
            foreach (string rec in recLines)
            {
                if (rec.StartsWith("\t"))
                {
                    subItems = rec.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (subItems.Length == 3)
                    {
                        try
                        {
                            Int32.Parse(subItems[1]);
                            Int32.Parse(subItems[2]);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        newItem = new ListViewItem(subItems);
                        listView_allClasses.Items.Add(newItem);
                    }
                }
            }
        }

        private struct ClassInfo
        {
            public string name;
            public int timeLength;
            public int timeLengthAll;
            public int memCount;
        }
        private struct WeekPane
        {
            public List<string> classQueue;
        }
        private List<ClassInfo> computMD;
        private List<WeekPane> classQueue;
        private void button_start_Click(object sender, EventArgs e)
        {
            // 将数据转换为运算数据类型
            computMD = new List<ClassInfo>();
            ClassInfo newItem;
            foreach (ListViewItem item in listView_allClasses.Items)
            {
                newItem = new ClassInfo();
                newItem.name = item.SubItems[0].Text;
                newItem.timeLength = Int32.Parse(item.SubItems[1].Text);
                newItem.memCount = Int32.Parse(item.SubItems[2].Text);
                newItem.timeLengthAll = newItem.timeLength * ((newItem.memCount + 9) / 10);
                computMD.Add(newItem);
            }

            // 根据课程时间长度排序，时间长的放在前面（当做运算权重使用）
            ClassInfo tmpCI;
            for (int i = computMD.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (computMD[i].timeLengthAll > computMD[j].timeLengthAll)
                    {
                        tmpCI = computMD[i];
                        computMD[i] = computMD[j];
                        computMD[j] = tmpCI;
                    }
                }
            }

            // 计算所有课程需要的周数(如果某课程的时间长度超过预算，则以这个课程的周数为标准)
            int timeAll = 0;
            int weekCount_classMax = 0;
            int weekCount_classMaxTmp = 0;
            foreach (ClassInfo item in computMD)
            {
                timeAll += item.timeLengthAll;
                weekCount_classMaxTmp = item.timeLengthAll / (2 * 2);
                if (weekCount_classMaxTmp > weekCount_classMax)
                {
                    weekCount_classMax = weekCount_classMaxTmp;
                }
            }
            int weekCount = timeAll / (5 * 3 * 2);
            weekCount = (weekCount > weekCount_classMax) ? weekCount : weekCount_classMax;

            // 对内容依次进行排课
            classQueue = new List<WeekPane>();
            //classQueue = new List<WeekPane>(weekCount);
            //WeekPane tmpWP;
            //for (int i = 0; i < classQueue.Count; i++)
            //{
            //    tmpWP = new WeekPane();
            //    tmpWP.classQueue = new string[15];
            //    classQueue[i] = tmpWP;
            //}
            int tmpI;
            //List<string> newCQ;
            WeekPane newWP;
            for (int i = 0; i < computMD.Count; i++)
            {
                tmpI = 0;
                for (int j = computMD[i].timeLengthAll/(2); j >= 0; j--)
                {
                    if((tmpI+1)> classQueue.Count)
                    {
                        newWP = new WeekPane();
                        newWP.classQueue = new List<string>();
                        classQueue.Add(newWP);
                        //newCQ = new List<string>();
                        //classQueue[classQueue.Count - 1].classQueue = newCQ;
                    }
                    if (classQueue[tmpI].classQueue.Count > 15)
                    {
                        tmpI++;
                        i++;
                    }
                    else
                    {
                        classQueue[tmpI].classQueue.Add(computMD[i].name);
                        tmpI++;
                    }
                }
            }

            // 输出结果
            string result = "";
            for (int i = 0; i < classQueue.Count;i++ )
            {
                result += "\r\n第[" + (i + 1) + "]周\r\n";
                for (int j = 0; j < classQueue[i].classQueue.Count; j++)
                {
                    result += classQueue[i].classQueue[j] + "\t";
                    if ((j + 1) % 5 == 0) result += "\r\n";
                }
            }
            textBox_result.Text = result;
        }
    }
}
