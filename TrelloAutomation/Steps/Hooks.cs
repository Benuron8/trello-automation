using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TrelloAutomation.Drivers;
using TrelloAutomation.Services;

namespace TrelloAutomation.Steps
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver? Driver { get; set; }
        private static TrelloApiService _trelloApiService = new TrelloApiService();
        public static string BoardId { get; set; }

        [BeforeScenario]
        public static void BeforeScenario(FeatureContext featureContext)
        {
            if (featureContext.FeatureInfo.Tags.Contains("UI"))
            {
                var driverManager = new DriverManager();
                Driver = driverManager.InitializeDriver();
            }

        }

        [AfterScenario]
        public static async Task AfterScenario(FeatureContext featureContext)
        {

            await _trelloApiService.DeleteBoardAsync(BoardId);

            if (featureContext.FeatureInfo.Tags.Contains("UI"))
            {
                Driver.Quit();
                Console.WriteLine("Closing browser after UI tests.");
            }
        }
    }
}
