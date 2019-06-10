using Microsoft.Win32;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
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
using System;

using Microsoft.Reporting.WinForms;
using System.Diagnostics;
using PizzeriaServiceDAL.ViewModel;

namespace PizzeriaWPFView
{
    /// <summary>
    /// Логика взаимодействия для WindowCustomerIndent.xaml
    /// </summary>
    public partial class WindowCustomerIndent : Window
    {

        public WindowCustomerIndent()
        {
            InitializeComponent();
        }

        private void buttonMake_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate.Value.Date >= dateTimePickerTo.SelectedDate.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                List<CustomerIndentViewModel> response = APICustomer.PostRequest<ReptBindingModel,
                List<CustomerIndentViewModel>>("api/Rept/GetCustomerIndents", new ReptBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate.Value,
                    DateTo = dateTimePickerTo.SelectedDate.Value
                });

                ReportDataSource source = new ReportDataSource("DataSetOrders",
               response);
                reportViewer.LocalReport.DataSources.Add(source);

                reportViewer.LocalReport.ReportPath = "Rept.rdlc";

                ReportParameter parameter = new ReportParameter("ReptParameterPeriod",
                "c " +
               dateTimePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " +
               dateTimePickerTo.SelectedDate.Value.ToShortDateString());
                this.reportViewer.LocalReport.SetParameters(parameter);
                
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void buttonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate.Value.Date >= dateTimePickerTo.SelectedDate.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    APICustomer.PostRequest<ReptBindingModel, bool>("api/Rept/SaveCustomerOrders", new ReptBindingModel
                    {
                        FileName = sfd.FileName,
                        DateFrom = dateTimePickerFrom.SelectedDate.Value,
                        DateTo = dateTimePickerTo.SelectedDate.Value
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

        private void FormCustomerIndent_Load(object sender, RoutedEventArgs e)
        {
            this.reportViewer.RefreshReport();
        }
    }
}
