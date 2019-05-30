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
using Unity;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public  IUnityContainer Container { get; set; }
        private readonly IMainService service;

        public MainWindow(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<IndentViewModel> list = service.GetList();
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
            var form = Container.Resolve<WindowCustomers>();
            form.ShowDialog();
        }
        private void компонентыToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowIngredients>();
            form.ShowDialog();
        }
        private void изделияToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowPizzas>();
            form.ShowDialog();
        }
        private void buttonCreateIndent_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowCreateIndent>();
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
                    service.TakeOrderInWork(new IndentBindingModel { Id = id });
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
                    service.FinishOrder(new IndentBindingModel { Id = id });
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
                    service.PayOrder(new IndentBindingModel { Id = id });
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
            var form = Container.Resolve<WindowStorages>();
            form.ShowDialog();
        }
        

        private void пополнитьСкладToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowPutOnStorage>();
            form.ShowDialog();
        }
    }
}
