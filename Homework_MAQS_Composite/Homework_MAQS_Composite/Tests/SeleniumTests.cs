using CognizantSoftvision.Maqs.BaseDatabaseTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
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
    public class SeleniumTests : BaseSeleniumTest
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
        /// </summary>
        [TestMethod]
        public void OpenLoginPageTest()
        {
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
        }

        /// <summary>
        /// Enter credentials test
        /// </summary>
        [TestMethod]
        public void EnterValidCredentialsTest()
        {
            string username = "Ted";
            string password = "123";

            
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            HomePageModel homepage = page.LoginWithValidCredentials(username, password);
            Assert.IsTrue(homepage.IsPageLoaded());
        }
        /// </summary>
        [TestMethod]
        public void Homework_EnterValidCredentialsTest()
        {
            string userName = GenericWait.WaitFor<string>(GetUserName);
            string password = GenericWait.WaitFor<string>(GetPassword);
            
            
            //timer for test
            this.PerfTimerCollection.StartTimer("Test Timer");

            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            

            //suspend logs 
            this.Log.LogMessage(MessageType.INFORMATION, "INFORMATION - Suspending Logging -- %%% 1 %%%");
            this.Log.SuspendLogging();
            
            //timer to get time to login
            this.PerfTimerCollection.StartTimer("Login Timer");
            
            //perform login action
            HomePageModel homepage = page.LoginWithValidCredentials(userName, password);
           
            //stop login timer
            this.PerfTimerCollection.StopTimer("Login Timer");

            //continue logs  
            this.Log.LogMessage(MessageType.INFORMATION, "INFORMATION - Resuming Logging -- %%% 2 %%%");
            this.Log.ContinueLogging();

            // Show logging level is respected and only logs the level specified in Appsettings.json
            this.Log.LogMessage(MessageType.VERBOSE, "VERBOSE - Will not see in log file.");
            this.Log.LogMessage(MessageType.INFORMATION, "INFORMATION - Successfully Logged in  -- %%% BYRON %%%");
            this.Log.LogMessage(MessageType.GENERIC, "GENERIC - Will see in log file.  -- ### BYRON ###");

            //Stop Test Timer
            //login
            this.PerfTimerCollection.StopTimer("Test Timer");

            Assert.IsTrue(homepage.IsPageLoaded());
            //Assert.Fail("Fail so that we can see the log message in the log file");


        }


        /// <summary>
        /// Enter credentials test
        /// </summary>
        [TestMethod]
        public void EnterInvalidCredentials()
        {
            string username = "NOT";
            string password = "Valid";
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
            Assert.IsTrue(page.LoginWithInvalidCredentials(username, password));
        }

        public static string GetUserName()
        {
            return Config.GetValueForSection("GlobalMaqs", "userName");
        }

        public static string GetPassword()
        {
            return Config.GetValueForSection("GlobalMaqs", "password");
        }
    }
}
