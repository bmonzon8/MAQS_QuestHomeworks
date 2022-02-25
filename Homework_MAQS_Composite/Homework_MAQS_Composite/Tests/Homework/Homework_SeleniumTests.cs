using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Data;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Composite Selenium test class
    /// </summary>
    [TestClass]
    public class Homework_SeleniumTests : BaseSeleniumTest
    {
        /// <summary>
        /// Do database setup for test run
        /// </summary>
        // [ClassInitialize] - Disabled because this step will fail as the template does not include access to a test database
        public static void TestSetup(TestContext context)
        {
            // Do database setup
            using (DatabaseDriver wrapper = new DatabaseDriver(DatabaseConfig.GetProviderTypeString(), DatabaseConfig.GetConnectionString()))
            {
                var result = wrapper.Query("getStateAbbrevMatch", new { StateAbbreviation = "MN" }, commandType: CommandType.StoredProcedure);
                Assert.AreEqual(1, result.Count(), "Expected 1 state abbreviation to be returned.");
            }
        }
        

        /// <summary>
        /// Do post test run web service cleanup
        /// </summary>
        [ClassCleanup]
        public static void TestCleanup()
        {
            // Do web service post run cleanup
            WebServiceDriver client = new WebServiceDriver(new Uri(WebServiceConfig.GetWebServiceUri()));
            string result = client.Delete("/api/String/Delete/1", "text/plain", true);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Open page test
       /// Did the Automation Page Load
        /// </summary>
        [TestMethod]
        public void OpenAutomationPageTest()
        { 
            try
            {
                AutomationPageModel page = new AutomationPageModel(this.TestObject);
                page.OpenAutomationPage();
                page.AssertPageLoaded();

            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception details are:" + e.Message);
                throw;
            }
            finally { }


        }

       
        [TestMethod]
        public void VerifyShowDialogButton()
        {
            try
            {
                AutomationPageModel page = new AutomationPageModel(this.TestObject);
                page.OpenAutomationPage();
                page.ClickShowDialogButton();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception details are:" + e.Message);
                throw;
            }
            finally { }


        }
        public static string GetUserName()
        {
            return Config.GetValueForSection("GlobalMaqs", "userName");
        }
    }
}
