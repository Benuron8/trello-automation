using OpenQA.Selenium;
using TrelloAutomation.Steps;

namespace TrelloAutomation.Pages
{
    public class BoardsPage : BasePage
    {
        private string boardId;

        private By BoardsSectionSelectorLocator => By.XPath("//a[@href='/u/usertestassign/boards']");
        private By CreateButtonLocator => By.XPath("//button[@data-testid='header-create-menu-button']");
        private By CreateBoardButtonLocator => By.XPath("//button[@data-testid='header-create-board-button']");
        private By StartWithTemplateButtonLocator => By.XPath("//button[@data-testid='header-create-board-from-template-button']");
        private By BoardTitleFieldLocator => By.XPath("//input[@data-testid='create-board-title-input']");
        private By CreateBoardSubmittButtonLocator => By.XPath("//button[@data-testid='create-board-submit-button']");
        private By BoardNameDisplayLocator => By.XPath("//h1[@data-testid='board-name-display']");
        private By BoardBodyLocator => By.XPath("//ol[@id='board']");
        private By BoardVisibilityLocator => By.XPath("//button[@data-testid='board-visibility-option-org']");

        public BoardsPage(IWebDriver driver) : base(driver) { }

        private IWebElement BoardsSection => WaitForElement(BoardsSectionSelectorLocator);
        private IWebElement CreateButton => WaitForElement(CreateButtonLocator);
        private IWebElement CreateBoardButton => WaitForElement(CreateBoardButtonLocator);
        private IWebElement CreateBoardTemplateButton => WaitForElement(StartWithTemplateButtonLocator);

        private IWebElement BoardTitleField => WaitForElement(BoardTitleFieldLocator);
        private IWebElement CreateBoardSubmitButton => WaitForElement(CreateBoardSubmittButtonLocator);
        private IWebElement BoardNameDisplay => WaitForElement(BoardNameDisplayLocator);
        private IWebElement BoardBody => WaitForElement(BoardBodyLocator);
        private IWebElement BoardVisibility => WaitForElement(BoardVisibilityLocator);
        
        public bool IsBoardsSectionIsOpen()
        {
            return BoardsSection.Displayed;
        }

        public void CreateBoard(string title)
        {
            CreateButton.Click();
            CreateBoardButton.Click();
            EnterBoardTitle(title);
            GetVisibilityWhileCreatingBoard();
            SubmitBoardCreation();
        }

        public string GetVisibilityWhileCreatingBoard()
        {
            var visibilityDropdown = WaitForElement(By.XPath("//div[@data-testid='create-board-select-vi" +
                "sibility']"));
            string visibility = visibilityDropdown.FindElement(By.XPath("(.//div)[3]")).Text;

            return visibility;
        }
        public void EnterBoardTitle(string title)
        {
            BoardTitleField.SendKeys(title);
        }

        public void SubmitBoardCreation()
        {
            CreateBoardSubmitButton.Click();
        }

        public bool IsBoardName(string expectedBoardName)
        {
            return BoardNameDisplay.Text.Equals(expectedBoardName, StringComparison.OrdinalIgnoreCase);
        }

        public string GetBoardIdFromUrl()
        {
            try
            {
                var currentUrl = _driver.Url;
                var parts = currentUrl.Split('/');
                return parts[4];
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to extract the board ID from the URL.", ex);
            }
        }

        public void SetBoardId()
        {
            boardId = GetBoardIdFromUrl();
            Hooks.BoardId = boardId;
        }

        public bool IsNewBoardInYourBoardsList()
        { 
            SetBoardId();

            string boardXPath = $"//a[contains(@href, '/b/{boardId}')]";

            IWebElement newBoardElement = WaitForElement(By.XPath(boardXPath));

            return newBoardElement != null;
        }

        public string GetVisibilityFromBoard()
        {
            var visibility = BoardVisibility.FindElement(By.XPath(".//span[2]")).Text;
            return visibility;
        }

        public void CreateBoardUsingTemplate(string templateSelected)
        {
            CreateButton.Click();
            CreateBoardTemplateButton.Click();
            SelectTemplate(templateSelected);
            GetVisibilityWhileCreatingBoard();
            SubmitBoardCreation();
        }

        public void SelectTemplate(String template)
        {
            IWebElement menuItem = WaitForElement(By.XPath("//button[.//div[contains(text(),'" + template + "')]]"));

            if (menuItem != null)
            {
                menuItem.Click();
            }
        }
    }
}
