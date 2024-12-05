using OpenQA.Selenium;

namespace TrelloAutomation.Pages
{
    public class LoginPage : BasePage
    {
        private By EmailFieldLocator => By.Id("username");
        private By LoginSubmitButtonLocator => By.Id("login-submit");
        private By PasswordFieldLocator => By.Id("password");

        public LoginPage(IWebDriver driver) : base(driver) { }

        private IWebElement EmailField => WaitForElement(EmailFieldLocator);
        private IWebElement LoginSubmitButton => WaitForElement(LoginSubmitButtonLocator);
        private IWebElement PasswordField => WaitForElement(PasswordFieldLocator);

        public void ClickLoginButton()
        {
            LoginSubmitButton.Click();
        }

        public void EnterEmail()
        {
            var email = ConfigHelper.Get("Trello:Email");

            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Trello credentials are not set in the environment variables.");
            }

            EmailField.SendKeys(email);
            ClickLoginButton();
        }

        public void EnterPassword()
        {
            var password = ConfigHelper.Get("Trello:Password");

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Trello credentials are not set.");
            }

            PasswordField.SendKeys(password);
            ClickLoginButton();
        }
    }
}
