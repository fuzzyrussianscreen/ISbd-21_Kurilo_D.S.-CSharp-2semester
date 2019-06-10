using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using Unity;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowPizza.xaml
    /// </summary>
    public partial class WindowPizza : Window
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<PizzaIngredientViewModel> pizzaIngredients;

        public WindowPizza()
        {
            InitializeComponent();
        }


        private void WindowProduct_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PizzaViewModel view = APICustomer.GetRequest<PizzaViewModel>("api/Pizza/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.PizzaName;
                        textBoxPrice.Text = view.Price.ToString();
                        pizzaIngredients = view.PizzaIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            else
            {
                pizzaIngredients = new List<PizzaIngredientViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (pizzaIngredients != null)
                {
                   dataGridView.ItemsSource = null;
                   dataGridView.ItemsSource = pizzaIngredients;
                    dataGridView.Columns[0].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[1].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[3].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[2].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowPizzaIngredient();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.PizzaId = id.Value;
                    }
                    pizzaIngredients.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedCells.Count >= 1)
            {
                var form = new WindowPizzaIngredient();
                form.Model = pizzaIngredients[dataGridView.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    pizzaIngredients[dataGridView.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedCells.Count >= 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        pizzaIngredients.RemoveAt(dataGridView.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (pizzaIngredients == null || pizzaIngredients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                List<PizzaIngredientBindingModel> pizzaIngredientBM = new
               List<PizzaIngredientBindingModel>();
                for (int i = 0; i < pizzaIngredients.Count; ++i)
                {
                    pizzaIngredientBM.Add(new PizzaIngredientBindingModel
                    {
                        Id = pizzaIngredients[i].Id,
                        PizzaId = pizzaIngredients[i].PizzaId,
                        IngredientId = pizzaIngredients[i].IngredientId,
                        Count = pizzaIngredients[i].Count
                    });
                }
                if (id.HasValue)
                {
                    APICustomer.PostRequest<PizzaBindingModel,
                    bool>("api/Pizza/UpdElement", new PizzaBindingModel
                    {
                        Id = id.Value,
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredient = pizzaIngredientBM
                    });
                }
                else
                {
                    APICustomer.PostRequest<PizzaBindingModel,
                    bool>("api/Pizza/AddElement", new PizzaBindingModel
                    {
                        PizzaName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PizzaIngredient = pizzaIngredientBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
