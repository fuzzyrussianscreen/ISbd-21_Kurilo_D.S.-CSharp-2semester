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
    /// Логика взаимодействия для WindowIngredients.xaml
    /// </summary>
    public partial class WindowIngredients : Window
    {

        public WindowIngredients()
        {
            InitializeComponent();
        }


        private void WindowIngredients_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<IngredientViewModel> list = APICustomer.GetRequest<List<IngredientViewModel>>("api/Ingredient/GetList");
                if (list != null)
                {
                   dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[1].Width = DataGridLength.Auto;
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
            var form = new WindowIngredient();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedCells.Count >= 1)
            {
                var form = new WindowIngredient();
                form.Id = (dataGridView.SelectedItems[0] as IngredientViewModel).Id;
                if (form.ShowDialog() == true)
                {
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
                    int id = Convert.ToInt32(dataGridView.SelectedCells[0].Item);
                    try
                    {
                        APICustomer.PostRequest<IngredientBindingModel, bool>("api/Ingredient/DelElement", new IngredientBindingModel { Id = id });
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
    }
}
