using RelevantCodes.ExtentReports;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.CoreSiteTest
{
    public class ExtentManager
    {
        public static ExtentReports extentReports;
        public static ExtentReports GetInstance()
        {
            if (extentReports == null)
            {
                
                extentReports = new ExtentReports("D:\\reports\\report_" + string.Format("{0:yyyyMMdd}", DateTime.Today) + ".html");
            }
            return extentReports;
        }
    }
}
