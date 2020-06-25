using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FemImageNet35
{
    public class Fem
    {
        public string path;
        //基本信息:0_recipeName; 1_lotID; 2_idwName; 3_chipArr col; 4_chipArr row; 5_ MP qty; 6_MP_name
        public List<string> info = new List<string>(); 
        public List<string> raw = new List<string>(); //MSR文件              
        public List<string[]> cdStr = new List<string[]>();// CD 数据
        public string slot_no;

        public Fem(string _path)
        {
            path = _path;
            ReadFile();
            ReadBasic();
          
               
        }
        // read MSR file
        void ReadFile()
        {
            string[] allStr = File.ReadAllLines(path);
            foreach (var item in allStr)
            {
                if (item.Trim().Length > 0) { raw.Add(item.Trim()); }
            }

        }
        void ReadBasic()
        {
            string str;         
            foreach (string x in raw)
            {
                str = x.Trim();
                if (x.StartsWith("/recipe_name ")) { info.Add( x.Split(' ')[1].Trim()); }
                if (x.StartsWith("/lot_id      ")) { info.Add( x.Split('\"')[1].Trim()); }
                if(x.StartsWith("~idw_info    idw_name    :")) { info.Add(x.Split(':')[1].Trim()); }
                if (x.StartsWith("~idw_info    chip_array")) 
                {  
                    info.Add( x.Split(':')[1].Trim().Split(',')[0]);
                    info.Add(x.Split(':')[1].Trim().Split(',')[1]);

                }
                if (x.StartsWith("~total_of_mp_name"))
                { info.Add(x.Substring(18, x.Length-18).Trim()); }

                if (x.StartsWith("~mp_name"))
                { info.Add(x.Substring(8,x.Length-8).Trim().Replace(" " ,"").Replace("\"","")); }

                if (x.StartsWith("$width "))
                {
                    cdStr.Add(x.Substring(6, x.Length - 6).Trim().Split(' '));
                    //01 MP - 1 3,3 14332420,16216900 2444
                }
                if(x.StartsWith("@slot_no      "))
                {
                    slot_no = x.Substring(10, x.Length - 10).Trim();
                    if (slot_no.Length==1)
                    {
                        slot_no = "S0" + slot_no;
                    }
                    else
                    {
                        slot_no = "S" + slot_no;
                    }
                }
            }
        }
            
        

        
       
    }
}
