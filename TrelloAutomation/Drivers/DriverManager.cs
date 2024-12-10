using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;

namespace TrelloAutomation.Drivers
{
    public class DriverManager
    {
        public IWebDriver InitializeDriver(string browser = "Chrome")
        {
            IWebDriver driver;

            ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");
                //chromeOptions.AddArgument("--no-sandbox");
                //chromeOptions.AddArgument("--disable-gpu");
                //chromeOptions.AddArgument("--start-maximized");

            FirefoxOptions firefoxOptions = new FirefoxOptions();
                //firefoxOptions.AddArgument("--headless");

            EdgeOptions edgeOptions = new EdgeOptions();
                //edgeOptions.AddArgument("--headless");

            switch (browser.ToLower())
            {
                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver(edgeOptions);
                    break;

                case "chrome":
                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver(chromeOptions);
                    break;
            }

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
