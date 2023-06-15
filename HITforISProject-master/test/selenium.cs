using OpenQA.Selenium.Chrome;

namespace SeleniumDocs.Hello
{
    public class HelloSelenium
    {
        public static void Main()
        {
            var driver = new ChromeDriver();
            
            driver.Navigate().GoToUrl("https://localhost:8080");
            
            driver.Quit();
        }
    }
}