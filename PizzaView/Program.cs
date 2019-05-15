using PizzaView.API;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceImplement.Implementations;
using PizzeriaServiceImplementDataBase;
using PizzeriaServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APICustomer.Connect();
            LetterCustomer.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}

