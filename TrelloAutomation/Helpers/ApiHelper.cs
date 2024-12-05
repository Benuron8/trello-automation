using Newtonsoft.Json.Linq;
using TrelloAutomation.Steps;

namespace TrelloAutomation.Helpers
{
    internal class ApiHelper
    {
        public static string GetBoardIdFromResponse(string responseContent)
        {
            JObject jsonResponse = JObject.Parse(responseContent);

            string? boardId = jsonResponse["id"]?.ToString();

            if (string.IsNullOrEmpty(boardId))
            {
                throw new InvalidOperationException("The 'id' field is null.");
            }

            Console.WriteLine("Board ID: " + boardId); 

            Hooks.BoardId = boardId;

            return boardId;
        }
    }
}
