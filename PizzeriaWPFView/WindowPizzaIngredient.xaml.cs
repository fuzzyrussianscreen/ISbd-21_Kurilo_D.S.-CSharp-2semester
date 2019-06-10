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


namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowPizzaIngredient.xaml
    /// </summary>
    public partial class WindowPizzaIngredient : Window
    {
        public PizzaIngredientViewModel Model
        {
            set { model = value; }
            get { return model; }
        }
        private PizzaIngredientViewModel model;

        public WindowPizzaIngredient()
        {
            InitializeComponent();
        }

        
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new PizzaIngredientViewModel
                    {
                        IngredientId = (comboBoxIngredient.SelectedValue as IngredientViewModel).Id,
                        IngredientName = comboBoxIngredient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<IngredientViewModel> list = APICustomer.GetRequest<List<IngredientViewModel>>("api/Ingredient/GetList");
                if (list != null)
                {
                    comboBoxIngredient.DisplayMemberPath = "IngredientName";
                    comboBoxIngredient.ItemsSource = list;
                    comboBoxIngredient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
            if (model != null)
            {
                comboBoxIngredient.IsEnabled = false;
                comboBoxIngredient.SelectedValue = model.IngredientId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
    }
}
