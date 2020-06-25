using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NikonBat
{
    class Program
    {
        static void Main()
        {
            string hh = DateTime.Now.ToString("HH");

            if (int.Parse(hh) < 9)
            {
                ToDriveP.CopyEgaLog();
                ToDriveP.CopyRecipeList();
                ToDriveP.CopyParameter();
                ToDriveP.CopySeqLog();
            }
            else
            {
                FromDriveP.CopyFtpFromP();

            }

            Console.WriteLine("DONE!!!\n\r\n\rPress 'Enter' Key To Exit.");

          
        }
    }
}
