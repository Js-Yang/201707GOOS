using System.Linq;
using FluentAutomation;
using GOOS_Sample.Models;
using Microsoft.Practices.Unity;
using TechTalk.SpecFlow;
using GOOS_Sample.Repository;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using Budget = GOOS_Sample.Models.DataModels.Budget;

namespace GOOS_SampleTests.Steps.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun()]
        public static void RegisterDIContainer()
        {
            UnityContainer = new UnityContainer();
            UnityContainer.RegisterType<IRepository<Budget>, BudgetRepository>();
            UnityContainer.RegisterType<IBudgetService, BudgetService>();
        }

        public static IUnityContainer UnityContainer
        {
            get;
            set;
        }

        [BeforeFeature()]
        [Scope(Tag = "web")]
        public static void SetBrowser()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [BeforeScenario()]
        public void BeforeScenarioCleanTable()
        {
            CleanTableByTags();
        }

        [AfterFeature()]
        public static void AfterFeatureCleanTable()
        {
            CleanTableByTags();
        }

        private static void CleanTableByTags()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags
                .Where(x => x.StartsWith("Clean"))
                .Select(x => x.Replace("Clean", ""));

            if (!tags.Any())
            {
                return;
            }

            using (var dbcontext = new BudgeSystemEntitiesForTest())
            {
                foreach (var tag in tags)
                {
                    dbcontext.Database.ExecuteSqlCommand($"TRUNCATE TABLE [{tag}]");
                }

                dbcontext.SaveChangesAsync();
            }
        }

    }
}
