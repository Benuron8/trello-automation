using TechTalk.SpecFlow;
using TrelloAutomation.Helpers;
using TrelloAutomation.Services;

namespace TrelloAutomation.Steps
{
    [Binding]
    public class CreateBoardAPIStepDefinitions
    {
        private readonly TrelloApiService _trelloApiService;
        private string? _boardId;
        private string? _errorMessage;

        public CreateBoardAPIStepDefinitions()
        {
            _trelloApiService = new TrelloApiService();
        }

        [Given(@"I have a valid Trello API key and token")]
        public void GivenIHaveAValidTrelloAPIKeyAndToken()
        {
            Assert.IsFalse(string.IsNullOrEmpty(_trelloApiService.ApiKey), "API Key is null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(_trelloApiService.ApiToken), "API Token is null or empty.");
        }

        [When(@"I create a new board with the title ""([^""]*)""")]
        public async Task WhenICreateANewBoardWithTheTitleAsync(string boardTitle)
        {
            try
            {
                var response = await _trelloApiService.CreateBoardAsync(boardTitle);
                _boardId = ApiHelper.GetBoardIdFromResponse(response);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

        [Then(@"the board should be created successfully")]
        public void ThenTheBoardShouldBeCreatedSuccessfully()
        {
            Console.WriteLine("Board ID: " + _boardId);

            Assert.IsNotNull(_boardId, "Board ID should not be null");
        }

        [When(@"I try to create a new board without a title")]
        public async Task WhenITryToCreateANewBoardWithoutATitleAsync()
        {
            try
            {
                await _trelloApiService.CreateBoardAsync(""); 
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

        [Then(@"I should receive an error message ""([^""]*)""")]
        public void ThenIShouldReceiveAnErrorMessage(string expectedErrorMessage)
        {
            Assert.That(_errorMessage, Is.EqualTo(expectedErrorMessage), "Error message is not the expected");
        }
    }
}
