using AngleSharp.Dom;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TrelloAutomation.Pages;

namespace TrelloAutomation.Steps
{
    [Binding]
    public class CreateBoardStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;
        private readonly LoginPage _loginPage;
        private readonly BoardsPage _boardsPage;
        private readonly ScenarioContext _scenarioContext;

        public CreateBoardStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = Hooks.Driver;
            _homePage = new HomePage(_driver);
            _loginPage = new LoginPage(_driver);
            _boardsPage = new BoardsPage(_driver);
            _scenarioContext = scenarioContext;
        }

        [Given(@"the user is at the Home Page")]
        public void GivenTheUserIsAtTheHomePage()
        {
            _driver.Navigate().GoToUrl("https://trello.com");
        }

        [Given(@"the user logs in to Trello")]
        public void GivenTheUserLogsInToTrello()
        {
            _homePage.ClickLoginButton();
            _loginPage.EnterEmail();
            _loginPage.EnterPassword();
        }

        [Given(@"the user is on the Boards section")]
        public void GivenTheUserIsOnTheBoardsSection()
        {
            Assert.IsTrue(_boardsPage.IsBoardsSectionIsOpen());
        }

        [When(@"the user creates a new board named ""([^""]*)""")]
        public void WhenTheUserCreatesANewBoardNamed(string title)
        {
            _boardsPage.CreateBoard(title);
            _scenarioContext.Set(title, "BoardTitle");
            _scenarioContext.Set(_boardsPage.GetVisibilityWhileCreatingBoard(), "Visibility");
        }

        [Then(@"the user sees the board in the Trello Workspace")]
        public void ThenTheUserSeesTheBoardInTheTrelloWorkspace()
        {

            var title = _scenarioContext.Get<string>("BoardTitle");

            Assert.That(_boardsPage.IsBoardName(title), Is.True, $"The board '{title}' was not found.");
            Assert.IsTrue(_boardsPage.IsNewBoardInYourBoardsList(), "New Board was not created");
        }

        [Then(@"confirms that the board has the correct visibility")]
        public void ThenConfirmsThatTheBoardHasTheCorrectVisibility()
        {
            string selectedValue = _scenarioContext.Get<string>("Visibility");
            string visibilityInBoard = _boardsPage.GetVisibilityFromBoard();

            Assert.That(visibilityInBoard, Is.EqualTo($"{selectedValue} visible"), $"Expected visibility '{selectedValue} visible', but got '{visibilityInBoard}'");
        }


        [When(@"the user creates a new board selecting the template ""([^""]*)""")]
        public void WhenTheUserCreatesANewBoardSelectingTheTemplate(string templateName)
        {
            _boardsPage.CreateBoardUsingTemplate(templateName);

            _scenarioContext.Set(templateName, "BoardTitle");
            _scenarioContext.Set(_boardsPage.GetVisibilityWhileCreatingBoard(), "Visibility");
        }



    }
}
