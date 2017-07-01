using FluentAutomation;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.Steps.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeFeature()]
        [Scope(Tag = "web")]
        public static void SetBrowser()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }
    }
}
