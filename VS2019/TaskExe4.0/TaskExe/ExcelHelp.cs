using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;

namespace TaskExe
{
    class ExcelHelp
    {
        public void DeleteSheetByName(string excelName,string sheetName)
        {
            //创建 Excel对象
            Application App = new Application();
            //获取缺少的object类型值
            object missing = Missing.Value;
            //打开指定的Excel文件
            Workbook openwb = App.Workbooks.Open(excelName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            App.Visible = false;//显示Excel
            App.DisplayAlerts = false;//不现实提示对话框,
            try
            {
                ((Worksheet)openwb.Worksheets[sheetName]).Delete();
                Console.WriteLine("删除成功！");
                openwb.Save();//保存工作表
                openwb.Close(false, missing, missing);//关闭工作表
            }
            catch
            {
                Console.WriteLine("工作表不存在，删除失败！");
                openwb.Close(false, missing, missing);//关闭工作表
            }
            //创建进程对象
            Process[] ExcelProcess = Process.GetProcessesByName("Excel");
            //关闭进程
            foreach (Process p in ExcelProcess)
            {
                p.Kill();
            }
        }
        public List<string>  ListSheetName(string excelName)
        {
            List<string> sheetsName = new List<string>();
           
            Application App = new Application();  //创建 Excel对象            
            object missing = Missing.Value;//获取缺少的object类型值
            
            Workbook openwb = App.Workbooks.Open(excelName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);//打开指定的Excel文件
            App.Visible = false;//显示Excel
            App.DisplayAlerts = false;//不现实提示对话框,
         
                foreach( Worksheet st in openwb.Sheets)
                {
                    sheetsName.Add(st.Name);
                }
               
                openwb.Close(false, missing, missing);//关闭工作表
           
         
            //创建进程对象
            Process[] ExcelProcess = Process.GetProcessesByName("Excel");
            //关闭进程
            foreach (Process p in ExcelProcess)
            {
                p.Kill();
            }
            return sheetsName;
        }


    }
}
    

