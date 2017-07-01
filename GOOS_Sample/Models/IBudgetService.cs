using System;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Models
{
    public interface IBudgetService
    {
        void Create(BudgetAddViewModel model);
        event EventHandler Created;
        event EventHandler Updated;
    }
}