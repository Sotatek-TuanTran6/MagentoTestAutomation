using MagentoTestAutomation.Drivers;
using TechTalk.SpecFlow;

[Binding]
public class TestHooks
{
    [BeforeScenario]
    public void BeforeScenario() => DriverFactory.Init();

    [AfterScenario]
    public void AfterScenario() => DriverFactory.Cleanup();
}
