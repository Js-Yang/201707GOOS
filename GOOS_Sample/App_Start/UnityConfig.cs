using System.Web.Mvc;
using GOOS_Sample.Models;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Repository;

namespace GOOS_Sample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IRepository<Budget>, BudgetRepository>();
            container.RegisterType<IBudgetService, BudgetService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}