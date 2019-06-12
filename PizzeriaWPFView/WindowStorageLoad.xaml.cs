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
using System.Windows.Shapes;


namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowStorageLoad.xaml
    /// </summary>
    /// 


    public class Row
    {
        public String StorageName { get; set; }
        public String Ingredientname { get; set; }
        public String Count { get; set; }
    }

    public partial class WindowStorageLoad : Window
    {

        public WindowStorageLoad()
        {
            InitializeComponent();
        }

        private void FormStoragesLoad_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                List<StoragesLoadViewModel> dict = APICustomer.GetRequest<List<StoragesLoadViewModel>>("api/Rept/GetStoragesLoad");
                if (dict != null)
                {
                    dataGridView.Items.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Items.Add(new Row() { StorageName = elem.StorageName, Ingredientname = "", Count = "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGridView.Items.Add(new Row() { StorageName = "", Ingredientname = listElem.Item1.ToString(), Count = listElem.Item2.ToString() });
                        }
                        dataGridView.Items.Add(new Row() { StorageName = "Итого", Ingredientname = "", Count = elem.TotalCount.ToString() });
                        dataGridView.Items.Add(new Row() { StorageName = "", Ingredientname = "", Count = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void buttonSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    APICustomer.PostRequest<ReptBindingModel, bool>("api/Rept/SaveStoragesLoad", new ReptBindingModel
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
    }
}
