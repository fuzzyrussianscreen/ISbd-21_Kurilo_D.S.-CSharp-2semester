using PizzeriaServiceDAL.BindingModel;
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
    /// Логика взаимодействия для WindowPerformers.xaml
    /// </summary>
    public partial class WindowPerformers : Window
    {
        public WindowPerformers()
        {
            InitializeComponent();
        }

        private void FormPerformers_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<PerformerViewModel> list = APICustomer.GetRequest<List<PerformerViewModel>>("api/Performer/GetList");
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
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new WindowPerformer();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedCells.Count >= 1)
            {
                var form = new WindowPerformer();
                form.Id = (dataGridView.SelectedItems[0] as PerformerViewModel).Id;
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
                        APICustomer.PostRequest<PerformerBindingModel, bool>("api/Performer/DelElement", new PerformerBindingModel { Id = id });
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
