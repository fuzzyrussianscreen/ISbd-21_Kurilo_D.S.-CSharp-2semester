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
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using PizzeriaServiceDAL.BindingModel;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowPutOnStorage.xaml
    /// </summary>
    public partial class WindowPutOnStorage : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly IStorageService serviceS;
        private readonly IIngredientService serviceC;
        private readonly IMainService serviceM;

        public WindowPutOnStorage(IStorageService serviceS, IIngredientService serviceC,
       IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void FormPutOnStorage_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                List<IngredientViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxIngredient.DisplayMemberPath = "IngredientName";
                    comboBoxIngredient.ItemsSource = listC;
                    comboBoxIngredient.SelectedItem = null;
                }
                List<StorageViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxStorage.DisplayMemberPath = "StorageName";
                    comboBoxStorage.ItemsSource = listS;
                    comboBoxStorage.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
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
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.PutIngredientOnStorage(new StorageIngredientBindingModel
                {
                    IngredientId = (comboBoxIngredient.SelectedValue as IngredientViewModel).Id,
                    StorageId = (comboBoxStorage.SelectedValue as StorageViewModel).Id,
                    Count = Convert.ToInt32(textBoxCount.Text)
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
    }
}
