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
    public partial class FormBuyBook : Form
    {
        private readonly Book book;

        public FormBuyBook(Book book)
        {
            InitializeComponent();
            this.book = book;

            txtTitle.Text = book.Title;
            txtPrice.Text = String.Format("{0:0.00}",book.Price);
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private async void FormBuyBook_Load(object sender, EventArgs e)
        {
            txtTotalStuck.ReadOnly = true;

            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                Author? author = await context.Authors.FindAsync(book.AuthorId);
                txtAuthor.Text = author!.Fullname.ToString();

                txtTotalStuck.Text = book.Count.ToString();

                if (book.Count == 0)
                {
                    numUpDAmount.Enabled = false;
                    btnOk.Enabled = false;
                }
            }

            if (int.TryParse(numUpDAmount.Value.ToString(), out int amount))
            {
                lblTotalPrice.Text = String.Format("{0:0.00}", (amount * book.Price));
            }
        }

        private void numUpDAmount_ValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(numUpDAmount.Value.ToString(), out int amount))
            {
                lblTotalPrice.Text = String.Format("{0:0.00}", (amount * book.Price));
            }

            txtTotalStuck.Text = book.Count.ToString();

            if (int.TryParse(txtTotalStuck.Text, out int count))
            {
                if (numUpDAmount.Value > count)
                    btnOk.Enabled = false;
                else
                    btnOk.Enabled = true;
            }
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                BuyedBook buyedBook = new BuyedBook();
                buyedBook.BookId = book.Id;
                if (int.TryParse(numUpDAmount.Value.ToString(), out int amount))
                {
                    buyedBook.AmountOfBuy = amount;
                }

                book.Count -= buyedBook.AmountOfBuy;
                await context.BuyedBooks.AddAsync(buyedBook);
                await context.SaveChangesAsync();
            }
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}