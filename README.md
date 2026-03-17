# Medefer QA Technical Test

⚠️ **THIS VERSION CONTAINS INTENTIONAL BUGS FOR INTERVIEW TESTING** ⚠️

## Your Task

Write login test scenarios and fix all issues to get all tests passing. See [INTERVIEW_TASKS.md](INTERVIEW_TASKS.md) for detailed instructions.

## Quick Start

```powershell
dotnet test
```

You will see errors. Your job is to fix them all.

---

# Original Framework Documentation

Simple BDD login test framework using:

- .NET 9
- NUnit
- Reqnroll
- Microsoft Playwright

## Project structure

- `ReqnrollLogin.Tests/Features` - Gherkin scenarios
- `ReqnrollLogin.Tests/StepDefinitions` - Step bindings
- `ReqnrollLogin.Tests/Pages` - Page objects
- `ReqnrollLogin.Tests/Hooks` - Scenario setup/teardown
- `ReqnrollLogin.Tests/Drivers` - WebDriver factory
- `ReqnrollLogin.Tests/Support` - Shared test context

## Run tests

From the solution folder:

```powershell
dotnet test
```

## Run headless

```powershell
$env:HEADLESS = "true"
dotnet test
```

## Notes

- The sample test target is `https://the-internet.herokuapp.com`
- Scenarios include login flows and multi-page navigation tests
- Framework uses Page Object Model design pattern

## Configuration (Credentials Required)

This project requires login credentials to access the Medefer test environment. These credentials were provided as part of the technical assessment and must not be committed to source control.

1. Create an appsettings.json file
   Add a new file named appsettings.json in the project root, using the structure below:
   {
   "Login": {
   "Username": "ADD_USERNAME_HERE",
   "Password": "ADD_PASSWORD_HERE"
   }
   }

Insert the credentials supplied in the assessment email.

2. Why this file is not included in the repository
   The appsettings.json file is excluded via .gitignore to ensure no sensitive information is stored in version control. This follows secure development best practices and prevents accidental exposure of credentials.

3. Running the tests
   Once the appsettings.json file is created with valid credentials, the tests can be executed normally using your preferred .NET test runner or via the command line:
   dotnet test
