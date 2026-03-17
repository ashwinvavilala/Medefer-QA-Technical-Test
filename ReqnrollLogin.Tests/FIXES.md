Medefer QA Technical Test – Debugging & Fix Summary
Author: Ashwin Vavilala

## Summary and Fixes applied

This document describes the issues I encountered while working through the Medefer QA Technical Test, how I diagnosed them, and the fixes I applied to get the entire suite of scenarios passing. I’ve written this in a natural, narrative style to reflect my actual debugging process and thought flow.

## Fixing the Foundation

Getting the Project to Build and Run
When I first opened the project and ran dotnet build, the solution didn’t compile cleanly. Some namespaces were mismatched, and a few step definition classes were missing the [Binding] attribute, which prevented Reqnroll from discovering them. I corrected the namespaces and ensured all step definition classes were properly decorated so the framework could load them. Once these were fixed, the project built successfully and I could move on to running the tests.

## Step Binding and Framework Wiring Issues

After resolving the configuration problem, I noticed that some steps weren’t being executed correctly. This was due to missing [Binding] attributes and inconsistent namespaces across step definition files. Reqnroll relies heavily on these attributes to discover and bind steps, so I went through each step definition class and ensured they were correctly annotated and placed in the right namespace. Once this was done, all steps bound correctly and the tests could proceed further.

## Step Definition Restructuring for Better Binding and Maintainability

To resolve the binding issues and improve the structure of the framework, I split the original NavigationSteps.cs file into four separate step definition classes. The original file contained multiple unrelated step groups, which made step discovery inconsistent and harder to maintain. Reqnroll relies on clear namespaces and [Binding] attributes, so separating the steps into logical files ensured each class was correctly discovered. This also improved readability and aligned the project with standard BDD practices.

## The First Major Blocker: Configuration File Not Found

The very first test run failed immediately, and the error pointed to the constructor of 'LoginSteps'. The framework was trying to load 'appsettings.json', but the file wasn’t found in the output directory. The error message made it clear that the file was expected at:

ReqnrollLogin.Tests\bin\Debug\net9.0\appsettings.json

Initially, I assumed the file was missing entirely, but after checking the folder structure, I realised the file existed — it just wasn’t being copied during the build. The root cause turned out to be a combination of two issues:

- The file was originally named appsetting.json (missing the “s”).
- The .csproj file was not configured to copy it to the output directory.
  Once I renamed the file correctly and updated the .csproj with:

<None Update="appsettings.json">
  <CopyToOutputDirectory>Always</CopyToOutputDirectory>
</None>

the configuration finally loaded, and the login steps could execute.
This was a critical fix because without configuration, the entire login flow fails before any UI interaction even begins.

## Selector Bugs

Page Object Model Bugs: Incorrect Selectors
Once the tests started running, the next set of failures came from Playwright timeouts. These errors indicated that the framework was trying to interact with elements that didn’t exist or couldn’t be found on the page.
The most significant example was in the successful login scenario. After logging in, the test expected to see a header with the text “Secure Area”. The page object, however, was looking for an <h3> element:
Page.Locator("h3")

But the actual page uses an <h2> tag for the header. This mismatch caused Playwright to wait for five seconds before timing out. Updating the locator to target <h2> fixed the issue immediately.
I also reviewed other page objects and found similar issues — incorrect IDs, outdated selectors, or overly strict locators. After correcting these, the Checkbox, Dropdown, and Inputs scenarios all ran smoothly.

## Async/Await and Timing Problems

Some of the earlier failures were caused by missing await keywords, which meant certain actions were being triggered before the page was ready. This led to intermittent failures and race conditions. I reviewed the async flow across the page objects and step definitions, ensuring that all Playwright interactions were properly awaited. This stabilised the test execution and eliminated the flakiness.

## Repository Cleanup and .gitignore Fixes

The initial repository included build artifacts such as bin/, obj/,.dll , and .pdb files. These should not be committed because they are machine‑generated and change on every build. I added a proper .gitignore file at the repository root and cleaned the tracked files using:

git rm -r --cached .
git add .

This ensured the repository only contains source code and documentation, making it cleaner and easier for reviewers to clone and run.

## Test Stability Improvements

After fixing selectors and async issues, I validated the suite across multiple runs to ensure deterministic behaviour. Playwright’s auto‑waiting mechanisms were leveraged consistently, and unnecessary waits were removed. The result was a stable, repeatable test suite with no intermittent failures.

## Final Outcome

After addressing all of the above issues — configuration loading, step binding, selector corrections, and async timing — the entire suite ran cleanly. The final test run produced:
Test summary: total: 5, failed: 0, succeeded: 5, skipped: 0
Build succeeded

This confirmed that the framework was functioning as intended and that the login scenarios were implemented correctly.

## Reflection

This exercise was a good representation of real-world QA automation work: dealing with broken frameworks, debugging step bindings, fixing selectors, and ensuring async flows behave predictably. The issues were layered in a way that required careful reading of stack traces and a methodical approach to debugging. By resolving each problem one at a time, the framework became stable and the tests passed consistently.

## How to Run the Tests

To make it easy for anyone reviewing the project to run the test suite, here is the complete set of steps required. These instructions assume that the .NET 9 SDK is already installed.

- Navigate to the root of the project (the folder containing the .sln file):
  cd ReqnrollLogin.Tests
- Restore all dependencies:
  dotnet restore
- Install the Playwright browsers (only required the first time on a new machine):
  playwright install
- Run the full test suite:
  dotnet test
  The tests will launch a Playwright browser instance, execute all scenarios, and display a summary at the end. A successful run should show all five tests passing with no failures.

## Future Enhancements

If the framework were to be extended further, I would consider:

- Adding data-test attributes to the AUT for more stable selectors.
- Introducing dependency injection for cleaner context sharing.
- Running tests in parallel to reduce execution time.
- Adding CI/CD integration with Playwright reporting.
- Implementing API‑level login to bypass UI login for non‑UI scenarios.
  These enhancements would make the framework more scalable and production‑ready.
