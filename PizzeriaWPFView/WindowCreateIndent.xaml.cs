using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
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


namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowCreateIndent.xaml
    /// </summary>
    public partial class WindowCreateIndent : Window
    {

        public WindowCreateIndent()
        {
            InitializeComponent();
        }


        private void WindowCreateIndent_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = APICustomer.GetRequest<List<CustomerViewModel>>("api/Customer/GetList");
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMemberPath = "CustomerFIO";
                    comboBoxCustomer.ItemsSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<PizzaViewModel> listP = APICustomer.GetRequest<List<PizzaViewModel>>("api/Pizza/GetList");
                if (listP != null)
                {
                    comboBoxPizza.DisplayMemberPath = "PizzaName";
                    comboBoxPizza.ItemsSource = listP;
                    comboBoxPizza.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxPizza.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = (comboBoxPizza.SelectedValue as PizzaViewModel).Id;
                    PizzaViewModel product = APICustomer.GetRequest<PizzaViewModel>("api/Pizza/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = Convert.ToInt32(count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (comboBoxPizza.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                APICustomer.PostRequest<IndentBindingModel,
                bool>("api/Main/CreateIndent", new IndentBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    PizzaId = (comboBoxPizza.SelectedValue as PizzaViewModel).Id,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
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

        private void comboBoxPizza_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcSum();
        }
    }
}
