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
    public partial class FormAddAuthor : Form
    {
        private readonly Author author;

        public FormAddAuthor(Author author)
        {
            InitializeComponent();
            this.author = author;

            txtFirstname.Text = author.Firstname;
            txtLastname.Text = author.Lastname;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            author.Firstname = txtFirstname.Text;
            author.Lastname = txtLastname.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}