using FluentAssertions;
using Reqnroll;
using ReqnrollLogin.Tests.Pages;
using ReqnrollLogin.Tests.Support;

namespace ReqnrollLogin.Tests.StepDefinitions;

[Binding]
public class DropdownSteps
{
    private readonly UiTestContext _testContext;

    public DropdownSteps(UiTestContext testContext)
    {
        _testContext = testContext;
    }

    [When(@"I select ""(.*)"" from dropdown")]
    public async Task WhenISelectFromDropdown(string optionText)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var dropdownPage = new DropdownPage(page);

        await dropdownPage.SelectByTextAsync(optionText);
    }

    [Then(@"selected dropdown option should be ""(.*)""")]
    public async Task ThenSelectedDropdownOptionShouldBe(string expectedText)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var dropdownPage = new DropdownPage(page);

        var selected = await dropdownPage.GetSelectedTextAsync();
        selected.Should().Be(expectedText);
    }
}