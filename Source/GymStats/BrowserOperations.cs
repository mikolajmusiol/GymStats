using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace GymStats
{
    public class BrowserOperations
    {
        private IWebDriver _driver = new ChromeDriver();

        public bool LogIn(string email, string password)
        {
            try
            {
                _driver.Navigate().GoToUrl("https://justgym.pl/");
                _driver.Manage().Window.Maximize();
                Thread.Sleep(1000);
                _driver.FindElement(By.Id("wt-cli-accept-all-btn")).Click();
                Thread.Sleep(2000);
                _driver.FindElement(By.Id("my-account")).Click();
                Thread.Sleep(1000);
                _driver.FindElement(By.Name("log")).SendKeys(email);
                Thread.Sleep(500);
                _driver.FindElement(By.Name("pwd")).SendKeys(password);
                Thread.Sleep(500);
                _driver.FindElement(By.Name("login-submit")).Click();
                Thread.Sleep(2000);
            }
            catch
            {
                return false;
            }
            return IsLoggedIn();
        }

        public string GetEntriesSource()
        {
            _driver.FindElement(By.Id("menu-item-3073")).Click();
            Thread.Sleep(1000);
            _driver.FindElement(By.Id("js-entries-tab")).Click();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("date_from")).Clear();
            _driver.FindElement(By.Name("date_from")).SendKeys("01.01.2000");
            Thread.Sleep(1000);
            _driver.FindElement(By.XPath("//*[@id='js-entries-dates']/div[3]/div/button")).Click();
            Thread.Sleep(1000);
            return Regex.Replace(_driver.PageSource.ToString(), @"\s+", "");
        }

        public string GetGymBroSource()
        {
            _driver.FindElement(By.Id("menu-item-3072")).Click();
            Thread.Sleep(1000);
            return Regex.Replace(_driver.PageSource.ToString(), @"\s+", "");
        }

        public void ShutDownBrowser()
        {
            Console.WriteLine("Shutting down browser...");
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

        private bool IsLoggedIn()
        {
            try
            {
                return _driver.FindElement(By.XPath("/html/body/section[7]/div/div/h1")).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}