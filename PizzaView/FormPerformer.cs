using PizzaView.API;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaView
{
    public partial class FormPerformer : Form
    {

        public int Id { set { id = value; } }
        private int? id;

        public FormPerformer()
        {
            InitializeComponent();
        }

        private void FormPerformer_Load(object sender, EventArgs e)
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
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
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

