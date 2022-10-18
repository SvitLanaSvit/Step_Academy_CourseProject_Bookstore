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
    public partial class FormDeferredBook : Form
    {
        private readonly Buyer buyer;
        private readonly Book book;

        public FormDeferredBook(Buyer buyer, Book book)
        {
            InitializeComponent();
            this.buyer = buyer;
            this.book = book; 
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private async void FormDeferredBook_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                cmbBuyers.DataSource = null;
                cmbBuyers.DisplayMember = nameof(Buyer.FullnameOfBuyer);
                cmbBuyers.ValueMember = nameof(Buyer.Id);
                cmbBuyers.DataSource = context.Buyers.ToList();

                Author? author = await context.Authors.FindAsync(book.AuthorId);
                txtAuthor.Text = author!.Fullname;
                txtBook.Text = book.Title;
            }

            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Text = book.Count.ToString();
            txtBook.ReadOnly = true;
            txtAuthor.ReadOnly = true;

            if (txtTotalAmount.Text == "0")
            {
                btnOK.Enabled = false;
                nudAmount.Enabled = false;
            }
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                if (cmbBuyers.SelectedIndex != -1)
                {
                    DeferredBook deferredBook = new DeferredBook();
                    if (int.TryParse(cmbBuyers.SelectedValue.ToString(), out int id))
                        deferredBook.BuyerId = id;
                    else
                        MessageBox.Show("The buyer`s id is not right!!!");

                    deferredBook.BookId = book.Id;

                    if (int.TryParse(nudAmount.Value.ToString(), out int amount))
                    {
                        deferredBook.CountOfDeferredBook = amount;
                    }

                    book.Count -= deferredBook.CountOfDeferredBook;
                    await context.DeferredBooks.AddAsync(deferredBook);
                    await context.SaveChangesAsync();
                }
                else
                {
                    MessageBox.Show("Choose buyer!");
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtTotalAmount.Text, out int amount))
            {
                if (nudAmount.Value > amount)
                    btnOK.Enabled = false;
                else
                    btnOK.Enabled = true;   
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}