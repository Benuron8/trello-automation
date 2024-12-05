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

            Console.WriteLine("Board ID: " + boardId); 

            Hooks.BoardId = boardId;

            return boardId;
        }
    }
}
