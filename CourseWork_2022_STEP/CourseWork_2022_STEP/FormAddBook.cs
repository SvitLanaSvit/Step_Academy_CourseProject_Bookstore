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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;

namespace CourseWork_2022_STEP
{
    public partial class FormAddBook : Form
    {
        private readonly Book book;

        public FormAddBook(Book book)
        {
            InitializeComponent();
            this.book = book;
            txtTitle.Text = book.Title;
            txtPage.Text = book.Page.ToString();
            txtYear.Text = book.Year.ToString();
            txtCost.Text = book.Year.ToString();
            txtCount.Text = book.Count.ToString();
            dateTimePickerDate.Value = book.DateTime;
        }

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";

        private void FormAddBook_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                cmbAuthors.DataSource = null;
                cmbAuthors.DisplayMember = nameof(Author.Fullname);
                cmbAuthors.ValueMember = nameof(Author.Id);
                cmbAuthors.DataSource = context.Authors.ToList();

                cmbAuthors.SelectedValue = book.AuthorId;

                cmbPublishingH.DataSource = null;
                cmbPublishingH.DisplayMember = nameof(PublishingHouse.Name);
                cmbPublishingH.ValueMember = nameof(PublishingHouse.Id);
                cmbPublishingH.DataSource = context.PublishingHouses.ToList();

                cmbPublishingH.SelectedValue = book.PublishingHouseId;

                cmbGenre.DataSource = null;
                cmbGenre.DisplayMember = nameof(Genre.Name);
                cmbGenre.ValueMember = nameof(Genre.Id);
                cmbGenre.DataSource = context.Genres.ToList();

                cmbGenre.SelectedValue = book.GenreId;

                cmbThemes.DataSource = null;
                cmbThemes.DisplayMember = nameof(ThemeOfBook.Name);
                cmbThemes.ValueMember = nameof(ThemeOfBook.Id);
                cmbThemes.DataSource = context.ThemeOfBooks.ToList();

                cmbThemes.SelectedValue = book.ThemeOfBookId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbAuthors.SelectedIndex != -1 && cmbPublishingH.SelectedIndex != -1 && 
                cmbGenre.SelectedIndex != -1 && cmbThemes.SelectedIndex != -1)
            {
                book.Title = txtTitle.Text;

                if (int.TryParse(cmbAuthors.SelectedValue.ToString(), out int idAuthor))
                    book.AuthorId = idAuthor;
                else
                    MessageBox.Show("The author`s id is not right!!!");

                if (int.TryParse(cmbPublishingH.SelectedValue.ToString(), out int idPublish))
                    book.PublishingHouseId = idPublish;
                else
                    MessageBox.Show("The publishing hause`s id is not right!!!");

                if (int.TryParse(txtPage.Text, out int page))
                    book.Page = page;
                else
                    MessageBox.Show("The count of pages are not right!!!");

                if (int.TryParse(cmbGenre.SelectedValue.ToString(), out int idGenre))
                    book.GenreId = idGenre;
                else
                    MessageBox.Show("The genres`s id is not right!!!");

                if (int.TryParse(txtYear.Text, out int year))
                    book.Year = year;
                else
                    MessageBox.Show("The year is not right!!!");

                if (float.TryParse(txtCost.Text, out float cost))
                    book.Cost = cost;
                else
                    MessageBox.Show("The cost is not right!!!");

                if (int.TryParse(txtCount.Text, out int count))
                    book.Count = count;
                else
                    MessageBox.Show("The count of books is not right!!!");

                book.DateTime = dateTimePickerDate.Value;

                if (ckbDilodyTrue.Checked)
                {
                    book.IsDilogy = true;
                }

                if (ckbDilogyFalse.Checked)
                {
                    book.IsDilogy = false;
                }

                if (ckbWrittenOffTrue.Checked)
                {
                    book.IsWrittenOff = true;
                }

                if (ckbWrittenOffFalse.Checked)
                {
                    book.IsWrittenOff = false;
                }

                if (int.TryParse(cmbThemes.SelectedValue.ToString(), out int idTheme))
                    book.ThemeOfBookId = idTheme;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void ckbDilodyTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDilodyTrue.Checked)
                ckbDilogyFalse.Enabled = false;
            else
                ckbDilogyFalse.Enabled = true;
        }

        private void ckbDilogyFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDilogyFalse.Checked)
                ckbDilodyTrue.Enabled = false;
            else
                ckbDilodyTrue.Enabled = true;
        }

        private void ckbWrittenOffTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbWrittenOffTrue.Checked)
                ckbWrittenOffFalse.Enabled = false;
            else
                ckbWrittenOffFalse.Enabled = true;
        }

        private void ckbWrittenOffFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbWrittenOffFalse.Checked)
                ckbWrittenOffTrue.Enabled = false;
            else
                ckbWrittenOffTrue.Enabled = true;
        }
    }
}