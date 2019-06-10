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
    /// Логика взаимодействия для WindowPerformer.xaml
    /// </summary>
    public partial class WindowPerformer : Window
    {
        public int Id { set { id = value; } }
        private int? id;

        public WindowPerformer()
        {
            InitializeComponent();
        }

        private void FormPerformer_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PerformerViewModel customer = APICustomer.GetRequest<PerformerViewModel>("api/Performer/Get/" + id.Value);
                    textBoxFIO.Text = customer.PerformerFIO;
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

            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<PerformerBindingModel,
                    bool>("api/Performer/UpdElement", new PerformerBindingModel
                    {
                        Id = id.Value,
                        PerformerFIO = textBoxFIO.Text
                    });
                }
                else
                {
                    APICustomer.PostRequest<PerformerBindingModel,
                    bool>("api/Performer/AddElement", new PerformerBindingModel
                    {
                        PerformerFIO = textBoxFIO.Text
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
