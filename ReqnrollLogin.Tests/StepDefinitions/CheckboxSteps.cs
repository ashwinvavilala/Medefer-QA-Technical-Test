using FluentAssertions;
using Reqnroll;
using ReqnrollLogin.Tests.Pages;
using ReqnrollLogin.Tests.Support;

namespace ReqnrollLogin.Tests.StepDefinitions;

[Binding]
public class CheckboxSteps
{
    private readonly UiTestContext _testContext;

    public CheckboxSteps(UiTestContext testContext)
    {
        _testContext = testContext;
    }

    [Then(@"checkbox (.*) should be unchecked")]
    public async Task ThenCheckboxShouldBeUnchecked(int checkboxNumber)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var checkboxPage = new CheckboxesPage(page);

        var isChecked = await checkboxPage.IsCheckedAsync(checkboxNumber);
        isChecked.Should().BeFalse();
    }

    [Then(@"checkbox (.*) should be checked")]
    public async Task ThenCheckboxShouldBeChecked(int checkboxNumber)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var checkboxPage = new CheckboxesPage(page);

        var isChecked = await checkboxPage.IsCheckedAsync(checkboxNumber);
        isChecked.Should().BeTrue();
    }

    [When(@"I set checkbox (.*) to checked")]
    public async Task WhenISetCheckboxToChecked(int checkboxNumber)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var checkboxPage = new CheckboxesPage(page);

        await checkboxPage.SetCheckedAsync(checkboxNumber, true);
    }
}