using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AnimalAdoption.Web.Portal.FunctionalTests
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(options);
        }

        [TestMethod]
        public void HomePage_LoadPage_LoadsAnimalsIn10Seconds()
        {
            // Remember to set this in the build pipeline
            var url = Environment.GetEnvironmentVariable("ANIMAL_ADOPTION_FUNCTIONAL_TEST_PATH") ?? "https://localhost:9001";            
            _driver.Url = url;
            var xPathToCheck = "//table/tbody/tr[1]/td[1]";            
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var returnedAnimal = wait.Until((d) =>
            {
                return d.FindElement(By.XPath(xPathToCheck));
            });
            Assert.IsNotNull(returnedAnimal?.Text);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
