using System;
using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Models
{
    public class BudgetService : IBudgetService
    {
        public event EventHandler Created;
        public event EventHandler Updated;
        private IRepository<Budget> _budgetRepository;
        
        public BudgetService(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Create(BudgetAddViewModel model)
        {
            //var budget = new Budget() { Amount = model.Amount, YearMonth = model.Month };
            //this._budgetRepository.Save(budget);
            
            var budget = this._budgetRepository.Read(x => x.YearMonth == model.Month);
            if (budget == null)
            {
                this._budgetRepository.Save(new Budget() { Amount = model.Amount, YearMonth = model.Month });

                var handler = this.Created;
                handler?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                budget.Amount = model.Amount;
                this._budgetRepository.Save(budget);

                var handler = this.Updated;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}