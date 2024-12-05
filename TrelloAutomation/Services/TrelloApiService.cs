namespace TrelloAutomation.Services
{
    public class TrelloApiService
    {
        private readonly HttpClient _httpClient;
        public string ApiKey = ConfigHelper.Get("Trello:ApiKey");
        public string ApiToken = ConfigHelper.Get("Trello:ApiToken");

        public TrelloApiService()
        {
            _httpClient = new HttpClient();
        }

        //CREATE
        public async Task<string> CreateBoardAsync(string boardTitle)
        {
            if (string.IsNullOrWhiteSpace(boardTitle))
                throw new ArgumentException("Board name cannot be empty", nameof(boardTitle));

            var createUrl = $"https://api.trello.com/1/boards/?name={Uri.EscapeDataString(boardTitle)}&key={ApiKey}&token={ApiToken}";

            var response = await _httpClient.PostAsync(createUrl, null);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        //DELETE
        public async Task DeleteBoardAsync(string boardId)
        {
            var closeUrl = $"https://api.trello.com/1/boards/{boardId}/closed?key={ApiKey}&token={ApiToken}&value=true";
            var deleteUrl = $"https://api.trello.com/1/boards/{boardId}?key={ApiKey}&token={ApiToken}";

            try
            {
                var closeResponse = await _httpClient.PutAsync(closeUrl, null);

                if (closeResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Board closed successfully.");
                }
                else
                {
                    var errorMessage = await closeResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to close board.");
                    return;
                }

                var deleteResponse = await _httpClient.DeleteAsync(deleteUrl);

                if (deleteResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Board deleted successfully.");
                }
                else
                {
                    var errorMessage = await deleteResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to delete board");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting the board: {ex.Message}");
            }
        }
    }
}
