using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TrelloAutomation.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver _driver;
        protected WebDriverWait _wait;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public IWebElement WaitForElement(By locator)
        {
            try
            {
                var element = _wait.Until(driver => driver.FindElement(locator));

                _wait.Until(driver => element.Displayed && element.Enabled);

                return element;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Element with locator {locator} not found within the specified timeout.");
            }
            catch (StaleElementReferenceException)
            {
                return _wait.Until(driver => driver.FindElement(locator));
            }
        }

        public List<IWebElement> WaitForElements(By locator)
        {
            try
            {
                var elements = _wait.Until(driver => driver.FindElements(locator));

                _wait.Until(driver => elements.All(element => element.Displayed && element.Enabled));

                return elements.ToList();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Elements with locator {locator} not found within the specified timeout.");
            }
            catch (StaleElementReferenceException)
            {
                var elements = _wait.Until(driver => driver.FindElements(locator));
                _wait.Until(driver => elements.All(element => element.Displayed && element.Enabled));

                return elements.ToList();
            }
        }

    }
}
