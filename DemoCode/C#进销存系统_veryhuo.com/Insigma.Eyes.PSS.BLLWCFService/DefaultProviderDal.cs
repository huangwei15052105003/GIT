using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    public class DefaultProviderDal
    {
        private static AbstractFactory.DalFactory instance = null;
   
        static DefaultProviderDal()
        {

            //string filePath = HttpContext.Current.Server.MapPath("~/DataProvider");
            string filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string dllFileName = System.Configuration.ConfigurationManager.AppSettings["DataProviderDllFile"];
            string dalFactoryClassName = System.Configuration.ConfigurationManager.AppSettings["DataProviderFactoryName"];

            System.Reflection.Assembly dll = System.Reflection.Assembly.LoadFile(filePath + "DataProvider\\" + dllFileName);

            instance = dll.CreateInstance(dalFactoryClassName) as AbstractFactory.DalFactory;
        }
        public DefaultProviderDal()
        {

        }
        public static AbstractFactory.DalFactory DefaultDataProviderFactory
        {
            get {

                return instance;
            }
        }
    }
}