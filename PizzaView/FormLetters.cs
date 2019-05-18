using PizzaView.API;
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
    public partial class FormLetters : Form
    {
        public FormLetters()
        {
            InitializeComponent();
        }

        private void FormLetters_Load(object sender, EventArgs e)
        {
            try
            {
                List<LetterInfoViewModel> list =
               APICustomer.GetRequest<List<LetterInfoViewModel>>("api/LetterInfo/GetList");
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
