using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HITforISProject.test
{
  [TestClass]
    public class FirstScriptTest
    {

        protected IWebDriver driver;

        [TestInitialize]
        public void CreateDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void ChromeSession()
        {
            driver.Navigate().GoToUrl("http://localhost");

            var title = driver.Title;
            Assert.AreEqual("Login", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textBox = driver.FindElement(By.Name("inputText"));
            var submitButton = driver.FindElement(By.TagName("button"));
            
            textBox.SendKeys("Updated text");
            submitButton.Click();
            
            var message = driver.FindElement(By.Id("output"));
            var value = message.Text;
            Assert.AreEqual("True", value);
        }
    }
}