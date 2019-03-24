using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;
using System.Threading;
using System.IO;

namespace NUnit.CoreSiteTest
{
    [TestFixture]
    public class TestClass:BaseTestClass
    {
        readonly Dictionary<string, string> UserList = new Dictionary<string, string>
            {
                { "bkepti", "6703175Bk!" },
                { "guest", "123456!" },
                { "nomember", "tyututu" },
            };
        readonly List<string> FileList = new List<string>{"test.docx"};
        [Test]
        public void LoginMethod()
        {
            var test = extent.StartTest("Loging Test");
            test.Log(LogStatus.Info, "Validating the download link");

            IWebDriver driver = new ChromeDriver();
            
            foreach (var login in UserList)
            {
                driver.Url = "http://localhost:57000/home/login";
                driver.Manage().Window.Maximize();
                IWebElement txtusername = driver.FindElement(By.Id("username"));
                IWebElement txtpassword = driver.FindElement(By.Id("password"));
                IWebElement btnlogin = driver.FindElement(By.Id("login"));

                txtusername.SendKeys(login.Key);
                txtpassword.SendKeys(login.Value);

                btnlogin.Click();
                Thread.Sleep(2000);

                

                if (driver.Url.ToLower()== "http://localhost:57000/home/login")
                {
                    test.Log(LogStatus.Fail, "Login Failed");
                    var ss = "D:\\reports\\" + System.Guid.NewGuid().ToString("N") + ".jpeg";
                    TakeScreenshot(ss, driver);
                    test.Log(LogStatus.Info, test.AddScreenCapture(ss));
                    
                    continue;
                }
                else if (driver.Url.ToLower() == "http://localhost:57000/home/index")
                {
                    try
                    {
                        IWebElement btnUpload = driver.FindElement(By.Id("btnUpload"));
                        btnUpload.Click();
                        Thread.Sleep(2000);

                    }
                    catch
                    {
                        test.Log(LogStatus.Fail, "Operator can not upload");
                        var ss = "D:\\reports\\" + System.Guid.NewGuid().ToString("N") + ".jpeg";
                        TakeScreenshot(ss, driver);
                        test.Log(LogStatus.Info, test.AddScreenCapture(ss));
                        
                        continue;
                    }
                }
                if (driver.Url.ToLower() == "http://localhost:57000/home/upload")
                {
                    foreach (var filename in FileList)
                    {
                        var testfile = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestFiles\" + filename);

                        IWebElement fileUpload = driver.FindElement(By.Id("file"));
                        fileUpload.SendKeys(testfile);
                        IWebElement fileName = driver.FindElement(By.Id("filename"));
                        fileName.SendKeys(System.Guid.NewGuid().ToString("N"));

                        IWebElement lblerror = driver.FindElement(By.Id("lblerror"));
                        if (lblerror.Displayed)
                        {
                            test.Log(LogStatus.Fail, "File size is big");
                            var ss = "D:\\reports\\" + System.Guid.NewGuid().ToString("N") + ".jpeg";
                            TakeScreenshot(ss, driver);
                            test.Log(LogStatus.Info, test.AddScreenCapture(ss));
                            
                            continue;
                        }

                        IWebElement btnSaveFile = driver.FindElement(By.Id("btnSaveFile"));
                        btnSaveFile.Click();
                        Thread.Sleep(2000);
                        try
                        {
                            IWebElement lbSuccess = driver.FindElement(By.Id("lbSuccess"));
                            test.Log(LogStatus.Skip, "File Saved");
                            continue;

                        }
                        catch
                        {
                            test.Log(LogStatus.Fail, "Upload fail");
                            var ss = "D:\\reports\\" + System.Guid.NewGuid().ToString("N") + ".jpeg";
                            TakeScreenshot(ss, driver);
                            test.Log(LogStatus.Info, test.AddScreenCapture(ss));
                            
                            continue;
                            
                        }
                       
                    }
                   
                }  
            }
            extent.EndTest(test);
            extent.Flush();
            driver.Quit();
            
        }
    }
}