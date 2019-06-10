using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                List<IndentViewModel> list = APICustomer.GetRequest<List<IndentViewModel>>("api/Main/GetList");
                if (list != null)
                {
                   dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[1].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[3].Visibility = Visibility.Collapsed;
                    dataGridView.Columns[5].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void клиентыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowCustomers();
            form.ShowDialog();
        }
        private void компонентыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowIngredients();
            form.ShowDialog();
        }
        private void изделияToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowPizzas();
            form.ShowDialog();
        }
        private void buttonCreateIndent_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowCreateIndent();
            form.ShowDialog();
            LoadData();
        }
        private void buttonTakeIndentInWork_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedCells.Count >= 1)
            {
                int id = (dataGridView.SelectedCells[0].Item as IndentViewModel).Id;
                try
                {
                    APICustomer.PostRequest<IndentBindingModel, bool>("api/Main/TakeIndentInWork", new IndentBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
        private void buttonIndentReady_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                int id = (dataGridView.SelectedCells[0].Item as IndentViewModel).Id;
                try
                {
                    APICustomer.PostRequest<IndentBindingModel, bool>("api/Main/FinishIndent", new IndentBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
        private void buttonPayOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                int id = (dataGridView.SelectedCells[0].Item as IndentViewModel).Id;
                try
                {
                    APICustomer.PostRequest<IndentBindingModel, bool>("api/Main/PayIndent", new IndentBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void складыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowStorages();
            form.ShowDialog();
        }
        

        private void пополнитьСкладToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowPutOnStorage();
            form.ShowDialog();
        }

        private void прайсПиццToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    APICustomer.PostRequest<ReptBindingModel, bool>("api/Main/SavePizzaIndent", new ReptBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
        
        private void заказыКлиентовToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowCustomerIndent();
            form.ShowDialog();
        }

        private void загруженностьСкладовToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowStorageLoad();
            form.ShowDialog();
        }
    }
}
