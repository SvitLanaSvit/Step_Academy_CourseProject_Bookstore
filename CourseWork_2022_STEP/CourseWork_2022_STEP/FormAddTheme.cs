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
    public partial class FormAddTheme : Form
    {
        private readonly ThemeOfBook themeOfBook;

        public FormAddTheme(ThemeOfBook themeOfBook)
        {
            InitializeComponent();
            this.themeOfBook = themeOfBook;

            txtNameOfTheme.Text = themeOfBook.Name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            themeOfBook.Name = txtNameOfTheme.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }
    }
}
