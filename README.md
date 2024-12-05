Test Automation for Trello Board Creation

This project consists in a Test Automation Suite with both Web and API tests for creating Trello boards using SpecFlow, Selenium WebDriver, and NUnit. 
The tests are written using BDD principles with Gherkin syntax.

The CI/CD of this project was done using GitHubActions because Azure was not allowing parallelism during this month until January.
**** (last run from last push is failed because its not headless) ****


Prerequisites:

Ensure that you have the following software installed:

- [.NET SDK 7.0 or later](https://dotnet.microsoft.com/download)
- [Visual Studio or Visual Studio Code](https://visualstudio.microsoft.com/)

## Project Setup

1. Clone the repository:

    open a terminal (bash):
		git clone https://github.com/knabnl-incubator/Betina-Pedro-Knab.git
		cd TrelloAutomation

2. Restore the NuGet packages:

    in terminal run:
		dotnet restore

3. Build the project:

    in terminal run:
		dotnet build

## Running Tests

To run the tests, execute the following command:

	in terminal run:
		dotnet test


##improvements###############

## Add mechanism so that I could pass as a parameter using environment variables the headless mode for CI CD remote runs
For now Im just commenting the headless mode when I want to run locally using the browser and when I want to run the ci/cd im taking the comment out. (so last run from last push is failed because its not headless) 

## Mechanism for Cross testing within different browsers
For now I just manually define in Hooks.cs which browser I want to run the tests in line 21 Driver = driverManager.InitializeDriver();
if this method receives a value within (Chrome, Edge, Firefox) it will run the tests in that browser. 
By default if nothing is passed runs in Chrome.
Like for example try to pass that information using the command for running the tests using environment variables and receive it and using it in the initializer.

## Handling Location-Based Email Verification
From time to time, When running the tests you may encounter an email verification step from Trello. 
This happens because Trello detects a login attempt from a new location or device different from the previous. 

Follow the steps below to handle this issue:

	Run the Tests for the First Time:
		Use the command provided in this README to run the tests.
		If you encounter an error during the login step, it may be due to email verification.
	
	Check the Email for a Verification Code:
		Open the email associated with the test account. (Gmail account with the credentials in appsettings)
		Look for an email from Trello requesting verification of the login attempt.
		Note the verification code provided in the email.
		
	Complete the Verification Manually:
		Open Trello in a browser.
		Log in using the test account credentials and enter the verification code when prompted.

	Re-run the Tests:
		Once the manual verification is complete, re-run the tests. Trello will no longer prompt for verification from the same location or device.
		
Notes:
This would be one of my improvements in this testing framework, then I noticed than if I perform the login using API call, this email Verification is not asked.

## Using Logging instead of Console Writelines
Also using some Logging tool to manage logs instead of prints.

## Better exceptions Handling
Better exceptions handling and better prevent for NULLS (I have some warnings in the project)

## Dependency injection instead of creating instances inside classes.


## Better use of scenarioContext instead of creating static variables.
