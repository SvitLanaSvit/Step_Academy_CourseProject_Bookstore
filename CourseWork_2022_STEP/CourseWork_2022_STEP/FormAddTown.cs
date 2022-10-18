using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_2022_STEP
{
    public partial class FormAddTown : Form
    {
        private readonly Town town;

        public FormAddTown(Town town)
        {
            InitializeComponent();
            this.town = town;

            txtName.Text = town.Name;
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private void FormAddTown_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                cmbCountries.DataSource = null;
                cmbCountries.DisplayMember = nameof(Country.Name);
                cmbCountries.ValueMember = nameof(Country.Id);
                cmbCountries.DataSource = context.Countries.ToList();

                cmbCountries.SelectedValue = town.CountryId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbCountries.SelectedIndex != -1)
            {
                town.Name = txtName.Text;

                if (int.TryParse(cmbCountries.SelectedValue.ToString(), out int id))
                    town.CountryId = id;
                else
                    MessageBox.Show("The country`s id is not correct!");
            }
            else
                MessageBox.Show("Choose country.");
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }
    }
}