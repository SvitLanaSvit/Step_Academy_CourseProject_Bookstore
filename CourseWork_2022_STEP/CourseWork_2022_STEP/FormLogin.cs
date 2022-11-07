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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private void FormLogin_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        { 
            using(MyBooksShopContext context = new MyBooksShopContext(options))
            {

                Login? loginFromDatabase = await context.Logins.FirstOrDefaultAsync(t => t.Username == txtUserName.Text && t.Password == txtPassword.Text);
                if(loginFromDatabase != null)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Login and password are correct");
                    FormBooksShop.checkLogin = true;
                } 
                else
                {
                    MessageBox.Show("Login or Password is not right. Try again.");
                    txtPassword.Text = "";
                    txtUserName.Text = "";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}