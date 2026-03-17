using Microsoft.Playwright;

namespace ReqnrollLogin.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

 public async Task LoginAsync(string username, string password)
    {
        await _page.FillAsync("#username", username);
        await _page.FillAsync("#password", password);
        await _page.ClickAsync("button[type='submit']");
    }
}

