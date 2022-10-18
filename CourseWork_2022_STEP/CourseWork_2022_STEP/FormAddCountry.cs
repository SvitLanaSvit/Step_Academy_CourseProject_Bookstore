using ShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_2022_STEP
{
    public partial class FormAddCountry : Form
    {
        private readonly Country country;

        public FormAddCountry(Country country)
        {
            InitializeComponent();
            this.country = country;

            txtName.Text = country.Name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            country.Name = txtName.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }
    }
}