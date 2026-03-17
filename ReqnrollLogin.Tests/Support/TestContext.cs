using Microsoft.Playwright;

namespace ReqnrollLogin.Tests.Support
{
    public class UiTestContext
    {
        public IPlaywright? Playwright { get; set; }
        public IBrowser? Browser { get; set; }
        public IBrowserContext? Context { get; set; }
        public IPage? Page { get; set; }
    }
}