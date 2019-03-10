using PizzeriaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaServiceImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Customer> Customers { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Indent> Indents { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<PizzaIngredient> PizzaIngredient { get; set; }
        public List<Storage> Storages { get; set; }
        public List<StorageIngredient> StorageIngredients { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Ingredients = new List<Ingredient>();
            Indents = new List<Indent>();
            Pizzas = new List<Pizza>();
            PizzaIngredient = new List<PizzaIngredient>();
            Storages = new List<Storage>();
            StorageIngredients = new List<StorageIngredient>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
