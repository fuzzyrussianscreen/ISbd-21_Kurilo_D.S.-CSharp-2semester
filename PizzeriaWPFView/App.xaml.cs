using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceImplement.Implementations;
using PizzeriaServiceImplementDataBase;
using PizzeriaServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            App app = new App();
            var container = BuildUnityContainer();
            app.Run(container.Resolve <MainWindow>());

        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, PizzeriaDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPizzaService, PizzaServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
