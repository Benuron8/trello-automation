using OpenQA.Selenium;

namespace TrelloAutomation.Pages
{
    public class HomePage : BasePage
    {
        private By LoginButtonLocator => By.XPath("//a[contains(@href, 'login?application=trello')]");

        public HomePage(IWebDriver driver) : base(driver) { }

        private IWebElement LoginButton => WaitForElement(LoginButtonLocator);

        public void GoToHomePage()
        {
            _driver.Navigate().GoToUrl("https://trello.com");
        }

        public LoginPage ClickLoginButton()
        {
            LoginButton.Click();

            return new LoginPage(_driver);
        }
    }
}