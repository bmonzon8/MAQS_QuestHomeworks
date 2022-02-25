using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Models
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class AutomationPageModel : BaseSeleniumPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string PageUrl = SeleniumConfig.GetWebSiteBase() + "Automation";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageModel" /> class.
        /// </summary>
        /// <param name="testObject">The test object</param>
        public AutomationPageModel(ISeleniumTestObject testObject) : base(testObject)
        {
        }

        /// <summary>
        /// Gets user name box
        /// </summary>
        private LazyElement GetShowDialog
        {
            get { return this.GetLazyElement(By.CssSelector("#showDialog"), "Show Dialog button"); }
        }

        /// <summary>
       

       
        /// <summary>
        /// Open the login page
        /// </summary>
        public void OpenAutomationPage()
        {
            this.TestObject.WebDriver.Navigate().GoToUrl(PageUrl);
            this.AssertPageLoaded();
        }


        /// <summary>
        /// Assert the login page loaded
        /// </summary>
        public void AssertPageLoaded()
        {
            Assert.IsTrue(
                this.IsPageLoaded(),
                "The web page '{0}' is not loaded",
                PageUrl);
        }

        /// <summary>
        /// Check if the home page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return this.ShowDialogButton.Displayed;
        }

        /// <summary>
        /// Gets Show Dialog button
        /// </summary>
        private LazyElement ShowDialogButton
        {
            get { return this.GetLazyElement(By.CssSelector("#showDialog1"), "Show Dialog button"); }
        }
        private LazyElement CloseDialogButton1

        {
            get { return this.GetLazyElement(By.CssSelector("locatorName"), "Show Dialog button"); }
        }
        /// <summary>
        /// Clicks on a button
        /// </summary>
        private void ClickButton(LazyElement lazyElementIn)
        {
            try
            {
                LazyElement buttonToClick = lazyElementIn;
                lazyElementIn.Click();

            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception Details are: " + e.Message);
            }
            finally { }
        }

        public void ClickShowDialogButton()
        {
            try
            {
                LazyElement buttonToClick = ShowDialogButton;
                this.ClickButton(buttonToClick);
            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception Details are: " + e.Message);
            }
            finally { }
        }
    }
}

