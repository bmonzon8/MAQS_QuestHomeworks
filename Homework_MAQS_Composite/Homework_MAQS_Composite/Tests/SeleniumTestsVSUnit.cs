using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Models;
using System;
using System.IO;

namespace Tests
{
    /// <summary>
    /// SeleniumTest test class with VS unit
    /// </summary>
    [TestClass]
    public class SeleniumTestsVSUnit : BaseSeleniumTest
    {

        /// <summary>
        /// Using config
        /// </summary>
        [TestMethod]
        public void GrabValuesFromConfig()
        {
            // Grab values from configuration
            string username = Config.GetGeneralValue("Username");
            string password = Config.GetGeneralValue("Password");

            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();

            HomePageModel homepage = page.LoginWithValidCredentials(username, password);
            Assert.IsTrue(homepage.IsPageLoaded());
        }

        /// <summary>
        /// Associate test file
        /// </summary>
        [TestMethod]
        public void AssociateFile()
        {
            // Create file
            string path = Path.Combine(LoggingConfig.GetLogDirectory(), "ImportantFile.txt");
            File.WriteAllText(path, "Important test stuff");

            // Associate file
            this.TestObject.AddAssociatedFile(path);

            Assert.Fail("Make sure the test fails");
        }

        /// <summary>
        /// Use value and object collections
        /// </summary>
        [TestMethod]
        public void UseValueAndObjectCollections()
        {
            // Grab values from configuration and store to value and object collections
            this.TestObject.SetValue("Username", Config.GetGeneralValue("Username"));
            this.TestObject.SetObject("Password", Config.GetGeneralValue("Password"));

            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();

            // Use value and object collections
            HomePageModel homepage = page.LoginWithValidCredentials(this.TestObject.Values["Username"], this.TestObject.Objects["Password"] as string);
            Assert.IsTrue(homepage.IsPageLoaded());
        }

        /// <summary>
        /// Use logger
        /// </summary>
        [TestMethod]
        public void UseLogger()
        {
            // Grab values from configuration and store to value and object collections
            this.TestObject.SetValue("Username", Config.GetGeneralValue("Username"));
            this.TestObject.SetObject("Password", Config.GetGeneralValue("Password"));

            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();

            // Suspend an reenable logging
            this.Log.SuspendLogging();
            page.EnterCredentials("Ted", "123");
            this.Log.ContinueLogging();

           
            Assert.IsTrue(page.IsPageLoaded());
            

            // Show logging level is respected
            this.Log.LogMessage(MessageType.VERBOSE, "VERBOSE - Will not see in log file.");
            this.Log.LogMessage(MessageType.INFORMATION, "INFORMATION - Will see in log file.");
            this.Log.LogMessage(MessageType.GENERIC, "GENERIC - Will see in log file.");

            Assert.Fail("Fail so we see the results in the log file.");
        }

        /// <summary>
        /// Use soft assert
        /// </summary>
        [TestMethod]
        public void UseSoftAssert()
        {

            // Soft assert failure - test will continue but ultimately fail
            this.SoftAssert.Assert(() => Assert.Fail("Early failure"), "Always fail");

            // Soft assert passes
            this.SoftAssert.Assert(() => Assert.IsTrue(true, "Early pass"), "Tautology");
        }

        /// <summary>
        /// Use perf timer
        /// </summary>
        [TestMethod]
        public void UseTimers()
        {
            // Start overall timer
            this.PerfTimerCollection.StartTimer("Total timer");

            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();
          
            // Start login timer
            this.PerfTimerCollection.StartTimer("Login timer");
            HomePageModel homepage = page.LoginWithValidCredentials("Ted", "123");
            Assert.IsTrue(homepage.IsPageLoaded());

            // Stop timers
            this.PerfTimerCollection.StopTimer("Login timer");
            this.PerfTimerCollection.StopTimer("Total timer");
        }

        /// <summary>
        /// Use manager store
        /// </summary>
        [TestMethod]
        public void UseManagerStore()
        {
            // Define named driver
            this.ManagerStore.AddOrOverride("TEST", new SeleniumDriverManager(() =>
            WebDriverFactory.GetBrowserWithDefaultConfiguration(SeleniumConfig.GetBrowserType()), this.TestObject));

            // Open login page with default driver
            LoginPageModel page = new LoginPageModel(this.TestObject);
            page.OpenLoginPage();

            // Nagivate with named driver
            this.ManagerStore.GetDriver<IWebDriver>("TEST").Navigate().GoToUrl("https://www.google.com/");

            // Test default driver
            Assert.IsTrue(page.LoginWithInvalidCredentials("NOT", "Valid"));

            // Test named driver
            Assert.IsTrue(this.ManagerStore.GetDriver<IWebDriver>("TEST").Url.Contains("google"));
        }
    }
}