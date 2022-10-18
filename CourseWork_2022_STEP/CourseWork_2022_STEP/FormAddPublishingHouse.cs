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
    public partial class FormAddPublishingHouse : Form
    {
        private readonly PublishingHouse publishingHouse;

        public FormAddPublishingHouse(PublishingHouse publishingHouse)
        {
            InitializeComponent();
            this.publishingHouse = publishingHouse;

            txtName.Text = publishingHouse.Name;
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private void FormAddPublishingHouse_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                cmbTown.DataSource = null;
                cmbTown.DisplayMember = nameof(Town.Name);
                cmbTown.ValueMember = nameof(Town.Id);
                cmbTown.DataSource = context.Towns.ToList();

                cmbTown.SelectedValue = publishingHouse.TownId;
            }            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbTown.SelectedIndex != -1)
            {
                publishingHouse.Name = txtName.Text;

                if (int.TryParse(cmbTown.SelectedValue.ToString(), out int id))
                    publishingHouse.TownId = id;
                else
                    MessageBox.Show("The town`s id is not correct!");
            }
            else
                MessageBox.Show("Choose town!");
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}