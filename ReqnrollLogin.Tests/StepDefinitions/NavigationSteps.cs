using FluentAssertions;
using Reqnroll;
using ReqnrollLogin.Tests.Pages;
using ReqnrollLogin.Tests.Support;

namespace ReqnrollLogin.Tests.StepDefinitions;

[Binding]
public class NavigationSteps
{
    private readonly UiTestContext _testContext;

    public NavigationSteps(UiTestContext testContext)
    {
        _testContext = testContext;
    }

    [Given(@"I open the main page at ""(.*)""")]
    public async Task GivenIOpenTheMainPageAt(string url)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var homePage = new HomePage(page);
        await homePage.OpenAsync(url);
    }

    [When(@"I navigate to page link ""(.*)""")]
    public async Task WhenINavigateToPageLink(string linkText)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var homePage = new HomePage(page);
        await homePage.NavigateByLinkTextAsync(linkText);
    }

    [Then(@"I should be on a page with header ""(.*)""")]
    public async Task ThenIShouldBeOnAPageWithHeader(string expectedHeader)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var commonPage = new CommonPage(page);

        var header = await commonPage.GetHeaderAsync();
        header.Should().Be(expectedHeader);
    }
}