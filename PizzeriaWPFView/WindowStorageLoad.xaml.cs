using Microsoft.Win32;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
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
using Unity;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowStorageLoad.xaml
    /// </summary>
    public partial class WindowStorageLoad : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly IReptService service;

        public WindowStorageLoad(IReptService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormStoragesLoad_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                var dict = service.GetStoragesLoad();
                if (dict != null)
                {
                    dataGridView.Items.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Items.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.Ingredients)
                        {
                            dataGridView.Items.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Items.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView.Items.Add(new object[] { });
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
                    service.SaveStoragesLoad(new ReptBindingModel
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
