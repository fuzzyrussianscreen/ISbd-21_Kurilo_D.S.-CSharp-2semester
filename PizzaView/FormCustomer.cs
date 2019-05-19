using PizzaView.API;
using PizzeriaServiceDAL.BindingModel;
using PizzeriaServiceDAL.Interfaces;
using PizzeriaServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaView
{
    public partial class FormCustomer : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CustomerViewModel customer = APICustomer.GetRequest<CustomerViewModel>("api/Customer/Get/" + id.Value);
                    textBoxFIO.Text = customer.CustomerFIO;
                    textBoxPost.Text = customer.Post;
                    dataGridView.DataSource = customer.Letters;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
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
            string fio = textBoxFIO.Text;
            string post = textBoxPost.Text;
            if (string.IsNullOrEmpty(post))
            {
                if (!Regex.IsMatch(post, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-
!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9az][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                        MessageBox.Show("Неверный формат для электронной почты", "Ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<CustomerBindingModel,
                    bool>("api/Customer/UpdElement", new CustomerBindingModel
                    {
                        Id = id.Value,
                        CustomerFIO = fio,
                        Post = post
                    });
                }
                else
                {
                    APICustomer.PostRequest<CustomerBindingModel,
                    bool>("api/Customer/AddElement", new CustomerBindingModel
                    {
                        CustomerFIO = fio,
                        Post = post
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
