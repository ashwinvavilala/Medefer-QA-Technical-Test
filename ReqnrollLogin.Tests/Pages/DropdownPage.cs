using Microsoft.Playwright;

namespace ReqnrollLogin.Tests.Pages;

public class DropdownPage
{
    private readonly IPage _page;

    public DropdownPage(IPage page)
    {
        _page = page;
    }

    private ILocator Dropdown => _page.Locator("#dropdown");


    public async Task SelectByTextAsync(string text)
    {
        //await _page.Locator("#dropdownlist").SelectOptionAsync(new[] { text });
         await Dropdown.SelectOptionAsync(new SelectOptionValue { Label = text });

    }

    public async Task<string> GetSelectedTextAsync()
    {
       // var value = await _page.Locator("#dropdownlist").InputValueAsync();
         var selectedValue = await Dropdown.InputValueAsync();

        var selectedOption = _page.Locator($"#dropdown option[value='{selectedValue}']");
        var text = await selectedOption.TextContentAsync();
        return text?.Trim() ?? string.Empty;
    }
}
