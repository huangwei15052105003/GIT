using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insigma.Eyes.PSS.WinUI
{
    public class AppParams
    {
        public static Model.UserModel CurrentUser
        {
            get;
            set;
        }
        public static string DllPath { get; set; }
    }
}
