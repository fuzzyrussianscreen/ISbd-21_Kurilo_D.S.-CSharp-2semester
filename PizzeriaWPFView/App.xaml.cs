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
            APICustomer.Connect();
            app.Run(new MainWindow());

        }
    }
}
