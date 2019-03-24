using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.CoreSiteTest
{
    public class BaseTestClass
    {
        public ExtentReports extent = ExtentManager.GetInstance();

        public static void TakeScreenshot(string filename, IWebDriver webDriver)
        {
            ITakesScreenshot screenshotdriver = webDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotdriver.GetScreenshot();
            screenshot.SaveAsFile(filename, ScreenshotImageFormat.Jpeg);
        }
    }
}
