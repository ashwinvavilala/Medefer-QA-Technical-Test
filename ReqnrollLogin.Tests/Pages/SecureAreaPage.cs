using System.Threading.Tasks;
using Microsoft.Playwright;

namespace ReqnrollLogin.Tests.Pages;

public class SecureAreaPage
{
    private readonly IPage _page;

    public SecureAreaPage(IPage page)
    {
        _page = page;
    }

    public async Task<string> GetHeaderAsync()
    {
        var header = _page.Locator("h2");
        await header.WaitForAsync(new LocatorWaitForOptions 
        { 
            State = WaitForSelectorState.Visible, 
            Timeout = 5000 
        });

        var text = await header.TextContentAsync();
        return text?.Trim() ?? string.Empty;
    }

    public async Task<string> GetFlashMessageAsync()
    {
        var flash = _page.Locator("#flash");
        await flash.WaitForAsync(new LocatorWaitForOptions 
        { 
            State = WaitForSelectorState.Visible, 
            Timeout = 5000 
        });

        var text = await flash.TextContentAsync();
        return text?.Trim() ?? string.Empty;
    }

    public async Task LogoutAsync()
    {
        await _page.ClickAsync("a[href='/logout']");
    }
}