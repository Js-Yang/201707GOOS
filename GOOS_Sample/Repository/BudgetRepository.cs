using System;
using System.Linq;
using GOOS_Sample.Models;
using GOOS_Sample.Models.DataModels;

namespace GOOS_Sample.Repository
{
    public class BudgetRepository : IRepository<Budget>
    {
        public void Save(Budget budget)
        {
            //using (var dbcontext = new BudgeSystemEntities())
            //{
            //    dbcontext.Budgets.Add(budget);
            //    dbcontext.SaveChanges();
            //}
            
            using (var dbcontext = new BudgeSystemEntities())
            {
                var budgetFromDb = dbcontext.Budgets.FirstOrDefault(x => x.YearMonth == budget.YearMonth);

                if (budgetFromDb == null)
                {
                    dbcontext.Budgets.Add(budget);
                }
                else
                {
                    budgetFromDb.Amount = budget.Amount;
                }

                dbcontext.SaveChanges();
            }
        }

        public Budget Read(Func<Budget, bool> predicate)
        {
            using (var dbcontext = new BudgeSystemEntities())
            {
                var firstBudget = dbcontext.Budgets.FirstOrDefault(predicate);
                return firstBudget;
            }
        }
    }
}