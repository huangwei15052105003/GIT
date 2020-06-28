using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.GZip;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Windows.Forms;

namespace LithoForm
{
    class classBatchReport
    {
        public static void QueryIlluminationByProduct(bool flag,ref DataTable dtShow)
        {

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
                "    完整产品名\r\n\r\n" +
                "    两位曝光程序层次名\r\n\r\n\r\n" +
                "选择 是（Y） 继续\r\n\r\n" +
                "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
                "至多列出最新10笔数据", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("退出，请重新选择日期及参数"); return; }




            string part = Interaction.InputBox("该查询要求输入产品名和层次名;\r\n\r\n\r\n" +
                "精确匹配，不支持通配符查询;\r\n\r\n\r\n现在请输入产品名;\r\n\r\n\r\n", "定义产品", "", -1, -1);
            part = part.Trim().ToUpper();
            if (part.Length < 8) { MessageBox.Show("产品名输入不正确，退出"); return; }
            part = "PROD/" + part;
            string layer = Interaction.InputBox("现在请输入层次名;\r\n\r\n\r\n精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义层次", "", -1, -1);
            layer = layer.Trim().ToUpper();
            if (layer.Trim().Length != 2) { MessageBox.Show("层次名输入不正确，退出"); return; }

            string connStr;
            if (flag)
            { connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB"; }
            else
            { connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB"; }

            string sql;
            sql = " SELECT tool,date,jobName part,layer,illuminationmode,aperture,sigmaoutjob,singmainactual,dosejob,focusjob,alignstrategy from tbl_asmlbatchreport ";
            sql += " WHERE Layer='" + layer + "' and jobname='" + part + "'";








            dtShow = LithoForm.Asml.QueryIllumination(connStr, part, layer);
           


        }
        public static void QueryIlluminationByTechRecipe(bool flag,ref DataTable dtShow) 
        {
            MessageBox.Show("根据工艺代码前三位及工艺层次统计照明条件\r\n\r\n\r\n照明条件来自batch report历史记录\r\n\r\n\r\n期间流程更改，统计数据可能有误，仅供参考");

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
              "    工艺代码前三位\r\n\r\n" +
              "    两位曝光程序层次名\r\n\r\n\r\n" +
              "选择 是（Y） 继续\r\n\r\n" +
              "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }

            string tech = Interaction.InputBox("请输入工艺代码前三位;\r\n\r\n\r\n" +
          "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义工艺代码前三位", "", -1, -1);
            tech = tech.Trim().ToUpper();

            string layer = Interaction.InputBox("请输入两位层次名;\r\n\r\n\r\n" +
         "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义两位工艺层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            if (tech.Length != 3 || layer.Length != 2) { MessageBox.Show("工艺代码和层次名格式不对，退出"); }
            string connStr1, connStr;
            if (flag)
            {
                connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB";
                connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
            }
            else
            {
                connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB";
                connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";

            }

            dtShow = LithoForm.Asml.QueryIlluminationTechLayer(connStr, connStr1, tech, layer);

        
      


        }

        public static  void QueryFocus(bool flag,ref DataTable dtShow)
        {
            MessageBox.Show("根据工艺代码前三位及工艺层次统计Focus设定\r\n\r\n\r\nFocus设定来自batch report历史记录\r\n\r\n\r\n期间曝光条件，流程等更改，统计数据可能有误，仅供参考");

            if (MessageBox.Show("查询筛选条件：\r\n\r\n\r\n" +
              "    工艺代码前三位\r\n\r\n" +
              "    两位曝光程序层次名\r\n\r\n\r\n" +
              "选择 是（Y） 继续\r\n\r\n" +
              "选择 否（N） 退出\r\n\r\n\r\n\r\n\r\n\r\n" +
              "", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { }
            else
            { MessageBox.Show("已退出"); return; }

            string tech = Interaction.InputBox("请输入工艺代码前三位;\r\n\r\n\r\n" +
          "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义工艺代码前三位", "", -1, -1);
            tech = tech.Trim().ToUpper();

            string layer = Interaction.InputBox("请输入两位层次名;\r\n\r\n\r\n" +
         "精确匹配，不支持通配符查询;\r\n\r\n\r\n", "定义两位工艺层次", "", -1, -1);
            layer = layer.Trim().ToUpper();

            if (tech.Length != 3 || layer.Length != 2) { MessageBox.Show("工艺代码和层次名格式不对，退出"); }
            string connStr1, connStr;
            if (flag)
            {
                connStr = @"data source=D:\TEMP\DB\AsmlBatchreport.DB";
                connStr1 = @"data source=D:\TEMP\DB\ReworkMove.DB";
            }
            else
            {
                connStr = @"data source=P:\_SQLite\AsmlBatchreport.DB";
                connStr1 = @"data source=P:\_SQLite\ReworkMove.DB";

            }

            dtShow = LithoForm.Asml.QueryFocusTechLayer(connStr, connStr1, tech, layer);

        }

     

        
    }
}
