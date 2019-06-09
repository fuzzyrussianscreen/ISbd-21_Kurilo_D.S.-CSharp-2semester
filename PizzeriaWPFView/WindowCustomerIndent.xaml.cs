﻿using Microsoft.Win32;
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
    /// Логика взаимодействия для WindowCustomerIndent.xaml
    /// </summary>
    public partial class WindowCustomerIndent : Window
    {

        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly IReptService service;

        public WindowCustomerIndent(IReptService service)
        {
            InitializeComponent();
            this.service = service;
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
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                "c " +
               dateTimePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " +
               dateTimePickerTo.SelectedDate.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = service.GetCustomerIndents(new ReptBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate.Value,
                    DateTo = dateTimePickerTo.SelectedDate.Value
                });
                ReportDataSource source = new ReportDataSource("DataSetIndent",
               dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
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
                    service.SaveCustomerIndents(new ReptBindingModel
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
