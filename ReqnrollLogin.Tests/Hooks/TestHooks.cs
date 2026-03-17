using Microsoft.Playwright;
using ReqnrollLogin.Tests.Support;
using Reqnroll;


namespace ReqnrollLogin.Tests.Hooks;

/*[Binding]
public class TestHooks
{
    private readonly UiTestContext _testContext;

    public TestHooks(UiTestContext testContext)
    {
        _testContext = testContext;
    }

    [BeforeScenario("@ui")]
    public async Task BeforeUiScenario()
    {
        //_testContext.Page = await BrowserFactory.CreatePageAsync();
    }

    [AfterScenario("@ui")]
    public async Task AfterUiScenario()
    {
        if (_testContext.Page != null)
        {
            await _testContext.Page.CloseAsync();
            await _testContext.Page.Context.Browser!.CloseAsync();
        }
    }
}*/

[Binding]
public class TestHooks
{
    private readonly UiTestContext _context;

    public TestHooks(UiTestContext context)
    {
        _context = context;
    }

    [BeforeScenario("@ui")]
    public async Task BeforeUiScenario()
    {
        _context.Playwright = await Playwright.CreateAsync();
        _context.Browser = await _context.Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        _context.Context = await _context.Browser.NewContextAsync();
        _context.Page = await _context.Context.NewPageAsync();
    }

    [AfterScenario("@ui")]
    public async Task AfterUiScenario()
    {
        if (_context.Context != null)
            await _context.Context.DisposeAsync();

        if (_context.Browser != null)
            await _context.Browser.CloseAsync();

        _context.Playwright?.Dispose();
    }
}
