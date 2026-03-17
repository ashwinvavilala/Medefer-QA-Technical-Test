using FluentAssertions;
using Reqnroll;
using ReqnrollLogin.Tests.Pages;
using ReqnrollLogin.Tests.Support;

namespace ReqnrollLogin.Tests.StepDefinitions;

[Binding]
public class InputsSteps
{
    private readonly UiTestContext _testContext;

    public InputsSteps(UiTestContext testContext)
    {
        _testContext = testContext;
    }

    [When(@"I enter ""(.*)"" into the number input")]
    public async Task WhenIEnterIntoTheNumberInput(string value)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var inputsPage = new InputsPage(page);

        await inputsPage.EnterValueAsync(value);
    }

    [Then(@"the number input value should be ""(.*)""")]
    public async Task ThenTheNumberInputValueShouldBe(string expectedValue)
    {
        var page = _testContext.Page ?? throw new InvalidOperationException("Page was not initialized. Ensure the scenario uses the @ui tag.");
        var inputsPage = new InputsPage(page);

        var value = await inputsPage.GetValueAsync();
        value.Should().Be(expectedValue);
    }
}