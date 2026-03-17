using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using ReqnrollLogin.Tests.Pages;
using ReqnrollLogin.Tests.Support;

namespace ReqnrollLogin.Tests.StepDefinitions;

[Binding]
public class LoginSteps
{
    private readonly UiTestContext _testContext;
    private readonly IConfiguration _config;

    public LoginSteps(UiTestContext testContext)
    {
        _testContext = testContext;

        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    [Given(@"I open the main page at ""(.*)""")]
    public async Task GivenIOpenTheMainPageAt(string url)
    {
    var page = _testContext.Page!;
    await page.GotoAsync(url);
    }

    [When(@"I login with valid credentials")]
    public async Task WhenILoginWithValidCredentials()
    {
        var page = _testContext.Page!;
        var loginPage = new LoginPage(page);

        var username = _config["Credentials:Valid:Username"];
        var password = _config["Credentials:Valid:Password"];

        await loginPage.LoginAsync(username!, password!);
    }

    [When(@"I login with invalid credentials")]
    public async Task WhenILoginWithInvalidCredentials()
    {
        var page = _testContext.Page!;
        var loginPage = new LoginPage(page);

        var username = _config["Credentials:Invalid:Username"];
        var password = _config["Credentials:Invalid:Password"];

        await loginPage.LoginAsync(username!, password!);
    }

    [Then(@"I should see a flash message containing ""(.*)""")]
    public async Task ThenIShouldSeeAFlashMessageContaining(string expected)
    {
        var page = _testContext.Page!;
        var securePage = new SecureAreaPage(page);

        var message = await securePage.GetFlashMessageAsync();
        message.Should().Contain(expected);
    }

    [When(@"I click the logout button")]
    public async Task WhenIClickTheLogoutButton()
    {
        var page = _testContext.Page!;
        var securePage = new SecureAreaPage(page);

        await securePage.LogoutAsync();
    }
}