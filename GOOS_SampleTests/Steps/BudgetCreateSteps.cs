using System;
using FluentAutomation;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GOOS_SampleTests.Steps
{
    [Binding]
    [Scope(Feature = "BudgetCreate")]
    public class BudgetCreateSteps : FluentTest
    {
        private BudgetCreatePage _budgetCreatePage;

        public BudgetCreateSteps()
        {
            this._budgetCreatePage = new BudgetCreatePage(this);
        }
        
        [Given(@"go to adding budget page")]
        public void GivenGoToAddingBudgetPage()
        {
            this._budgetCreatePage.Go();
        }
        
        [When(@"I add a buget (.*) for ""(.*)""")]
        public void WhenIAddABugetFor(int amount, string yearMonth)
        {
            this._budgetCreatePage
                .Amount(amount)
                .Month(yearMonth)
                .AddBudget();
        }
        
        [Then(@"it should display ""(.*)""")]
        public void ThenItShouldDisplay(string message)
        {
            this._budgetCreatePage.ShouldDisplay(message);
        }

        [Given(@"Budget table existed budgets")]
        public void GivenBudgetTableExistedBudgets(Table table)
        {
            var budgets = table.CreateSet<Budget>();
            using (var dbcontext = new BudgeSystemEntitiesForTest())
            {
                dbcontext.Budgets.AddRange(budgets);
                dbcontext.SaveChanges();
            }
        }
    }

    internal class BudgetCreatePage : PageObject<BudgetCreatePage>
    {

        public BudgetCreatePage(FluentTest test) : base(test)
        {
            this.Url = $"{PageContext.Domain}/budget/add";
        }

        public BudgetCreatePage Amount(int amount)
        {
            I.Enter(amount.ToString()).In("#amount");
            return this;
        }

        public BudgetCreatePage Month(string yearMonth)
        {
            I.Enter(yearMonth).In("#month");
            return this;
        }

        public void AddBudget()
        {
            I.Click("input[type=\"submit\"]");
        }

        public void ShouldDisplay(string message)
        {
            I.Assert.Text(message).In("#message");
        }
    }
}
