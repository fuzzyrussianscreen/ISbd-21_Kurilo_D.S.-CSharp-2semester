﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using PizzeriaServiceDAL.BindingModel;
using PizzaView.API;

namespace PizzaView
{
    public partial class FormPutOnStorage : Form
    {
        public FormPutOnStorage()
        {
            InitializeComponent();
        }
        private void FormPutOnStorage_Load(object sender, EventArgs e)
        {
            try
            {
                List<IngredientViewModel> listC = APICustomer.GetRequest<List<IngredientViewModel>>("api/Ingredient/GetList");
                if (listC != null)
                {
                    comboBoxIngredient.DisplayMember = "IngredientName";
                    comboBoxIngredient.ValueMember = "Id";
                    comboBoxIngredient.DataSource = listC;
                    comboBoxIngredient.SelectedItem = null;
                }
                List<StorageViewModel> listS = APICustomer.GetRequest<List<StorageViewModel>>("api/Storage/GetList");
                if (listS != null)
                {
                    comboBoxStorage.DisplayMember = "StorageName";
                    comboBoxStorage.ValueMember = "Id";
                    comboBoxStorage.DataSource = listS;
                    comboBoxStorage.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                APICustomer.PostRequest<StorageIngredientBindingModel, bool>("api/Main/PutIngredientOnStorage", new StorageIngredientBindingModel
                {
                    IngredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
