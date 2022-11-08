using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using System.Diagnostics;
using Dapper;
using System.Collections;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Net;
using System.Windows.Forms;
using System.Drawing;

namespace CourseWork_2022_STEP
{
    public partial class FormBooksShop : Form
    {
        public FormBooksShop()
        {
            InitializeComponent();
        }
        public static bool checkLogin = false;

        DbContextOptions<MyBooksShopContext> options = null!;
        string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BooksShop_Migration;Integrated Security=True;";
        Dictionary<int, double> books = new Dictionary<int, double>();

        private void FormBooksShop_Load(object sender, EventArgs e)
        {
            DbContextOptionsBuilder<MyBooksShopContext> builder = new DbContextOptionsBuilder<MyBooksShopContext>();
            builder.UseSqlServer(connStr)
                .LogTo(message => Debug.WriteLine(message));
            options = builder.Options;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(name) FROM master.sys.databases Where name = 'InternetShop'";
                int count = connection.ExecuteScalar<int>(query);
                if (count > 0)
                {
                    btnAddDataBase.Enabled = false;
                }
            }

            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAuthors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewGenres.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPublishingH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTowns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCountries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBuyedBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBuyers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDeferredBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTheme.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                cmbThemeOfBook.DataSource = null;
                cmbThemeOfBook.DisplayMember = nameof(ThemeOfBook.Name);
                cmbThemeOfBook.ValueMember = nameof(ThemeOfBook.Id);
                cmbThemeOfBook.DataSource = context.ThemeOfBooks.ToList();
            }

            if (btnLogin.Text == "Log In")
            {
                gpbWorkWithDatabase.Enabled = false;
                gpbWorkWithClient.Enabled = false;
                gpbWorkWithActions.Enabled = false;
                gpbSearchbook.Enabled = false;
                gpbShowTopOfBuyedBooks.Enabled = false;
                gpbWorkWithBook.Enabled = false;
                btnShowAllData.Enabled = false;

                MessageBox.Show("Log in...");
            }
        }

        private async void btnAddDataBase_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                Author author1 = new Author() { Firstname = "Patrick", Lastname = "Ness" };
                Author author2 = new Author() { Firstname = "Stephen", Lastname = "King" };
                Author author3 = new Author() { Firstname = "Lessja", Lastname = "Ukrajinka" };
                await context.Authors.AddRangeAsync(author1, author2, author3);

                Genre genre1 = new Genre() { Name = "Thriller" };
                Genre genre2 = new Genre() { Name = "Fantasy" };
                Genre genre3 = new Genre() { Name = "Sci-Fi" };
                Genre genre4 = new Genre() { Name = "Mystery" };
                Genre genre5 = new Genre() { Name = "Roman" };
                Genre genre6 = new Genre() { Name = "Westerns" };
                Genre genre7 = new Genre() { Name = "Dystopian" };
                Genre genre8 = new Genre() { Name = "Contemporary" };
                Genre genre9 = new Genre() { Name = "Horror" };
                await context.Genres.AddRangeAsync(genre1, genre2, genre3, genre4, genre5, genre6, genre7, genre8, genre9);

                Country country1 = new Country() { Name = "USA" };
                Country country2 = new Country() { Name = "Ukraine" };
                Country country3 = new Country() { Name = "Germany" };
                Country country4 = new Country() { Name = "France" };
                Country country5 = new Country() { Name = "Great Britain" };

                await context.Countries.AddRangeAsync(country1, country2, country3, country4, country5);
                Town town1 = new Town() { Name = "Kyiv", Country = country2 };
                Town town2 = new Town() { Name = "Lviv", Country = country2 };
                Town town3 = new Town() { Name = "London", Country = country5 };
                Town town4 = new Town() { Name = "New York", Country = country1 };
                Town town5 = new Town() { Name = "Hoboken", Country = country1 };
                Town town6 = new Town() { Name = "Berlin", Country = country3 };
                Town town7 = new Town() { Name = "Paris", Country = country4 };
                await context.Towns.AddRangeAsync(town1, town2, town3, town4, town5, town6, town7);

                PublishingHouse publishingHouse1 = new PublishingHouse() { Name = "Musée du Louvre", Town = town7 };
                PublishingHouse publishingHouse2 = new PublishingHouse() { Name = "Ullstein Buchverlage GmbH.", Town = town6 };
                PublishingHouse publishingHouse3 = new PublishingHouse() { Name = "John Wiley & Sons, Inc.", Town = town5 };
                PublishingHouse publishingHouse4 = new PublishingHouse() { Name = "Random House", Town = town4 };
                PublishingHouse publishingHouse5 = new PublishingHouse() { Name = "Walker", Town = town3 };
                PublishingHouse publishingHouse6 = new PublishingHouse() { Name = "Krajina Mriy Publishing House.", Town = town1 };
                PublishingHouse publishingHouse7 = new PublishingHouse() { Name = "Svichado Publishers Ltd.", Town = town2 };
                await context.PublishingHouses.AddRangeAsync(publishingHouse1, publishingHouse2, publishingHouse3,
                    publishingHouse4, publishingHouse5, publishingHouse6, publishingHouse7);

                Book book1 = new Book()
                {
                    Title = "Black House",
                    Author = author2,
                    PublishingHouse = publishingHouse4,
                    Page = 625,
                    Genre = genre9,
                    Year = 2002,
                    Cost = 5.99
                }; Book book2 = new Book()
                {
                    Title = "The Knife of Never Letting Go",
                    Author = author1,
                    PublishingHouse = publishingHouse5,
                    Page = 496,
                    Genre = genre5,
                    Year = 2008,
                    Cost = 7.59
                };
                await context.Books.AddRangeAsync(book1, book2);
                await context.SaveChangesAsync();
                MessageBox.Show("Done");
            }
        }

        private async void UpdateDataBaseBooks()
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                await context.Books.LoadAsync();
                dataGridViewBooks.DataSource = null;
                dataGridViewBooks.DataSource = context.Books
                    .Select(t => new
                    {
                        t.Id,
                        t.Title,
                        t.Author.Firstname,
                        t.Author.Lastname,
                        Publishing_House = t.PublishingHouse.Name,
                        Pages = t.Page,
                        Genre = t.Genre.Name,
                        t.Year,
                        t.ThemeOfBook.Name,
                        t.Cost,
                        t.Price,
                        t.Count,
                        t.DateTime,
                        t.IsDilogy,
                        t.IsWrittenOff
                    }).ToList();
                dataGridViewBooks.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseAuthors()
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                await context.Authors.LoadAsync();
                dataGridViewAuthors.DataSource = null;
                dataGridViewAuthors.DataSource = context.Authors
                    .Select(t => new
                    {
                        t.Id,
                        t.Firstname,
                        t.Lastname
                    }).ToList();
                dataGridViewAuthors.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseGenres()
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                await context.Genres.LoadAsync();
                dataGridViewGenres.DataSource = null;
                dataGridViewGenres.DataSource = context.Genres
                    .Select(t => new
                    {
                        t.Id,
                        t.Name
                    }).ToList();
                dataGridViewGenres.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBasePublishingHauses()
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                await context.PublishingHouses.LoadAsync();
                dataGridViewPublishingH.DataSource = null;
                dataGridViewPublishingH.DataSource = context.PublishingHouses
                    .Select(t => new
                    {
                        t.Id,
                        Hause_Name = t.Name,
                        Town_Name = t.Town.Name,
                        Country_Name = t.Town.Country.Name
                    }).ToList();
                dataGridViewPublishingH.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseTowns()
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                await context.Towns.LoadAsync();
                dataGridViewTowns.DataSource = null;
                dataGridViewTowns.DataSource = context.Towns
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        Country = t.Country.Name
                    }).ToList();
                dataGridViewTowns.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseCountries()
        {
            using (MyBooksShopContext contex = new MyBooksShopContext(options))
            {
                await contex.Countries.LoadAsync();
                dataGridViewCountries.DataSource = null;
                dataGridViewCountries.DataSource = contex.Countries
                    .Select(t => new
                    {
                        t.Id,
                        t.Name
                    }).ToList();
                dataGridViewCountries.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseBuyedBooks()
        {
            using (MyBooksShopContext contex = new MyBooksShopContext(options))
            {
                await contex.BuyedBooks.LoadAsync();
                dataGridViewBuyedBooks.DataSource = null;
                dataGridViewBuyedBooks.DataSource = contex.BuyedBooks
                    .Select(t => new
                    {
                        t.Id,
                        t.BookId,
                        t.Book.Title,
                        t.Book.Author.Fullname,
                        t.Book.Genre.Name,
                        t.AmountOfBuy,
                        t.DateTimeOfBuy
                    }).ToList();
                dataGridViewBuyedBooks.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseBuyers()
        {
            using (MyBooksShopContext contex = new MyBooksShopContext(options))
            {
                await contex.Buyers.LoadAsync();
                dataGridViewBuyers.DataSource = null;
                dataGridViewBuyers.DataSource = contex.Buyers
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        t.Surname,
                        t.E_Mail,
                        t.TelNummer
                    }).ToList();
                dataGridViewBuyers.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseDeferredBooks()
        {
            using (MyBooksShopContext contex = new MyBooksShopContext(options))
            {
                await contex.DeferredBooks.LoadAsync();
                dataGridViewDeferredBooks.DataSource = null;
                dataGridViewDeferredBooks.DataSource = contex.DeferredBooks
                    .Select(t => new
                    {
                        t.Id,
                        t.Book.Title,
                        t.Book.Author.Fullname,
                        t.Buyer.FullnameOfBuyer,
                        t.CountOfDeferredBook,
                        t.DateDeferredBook
                    }).ToList();
                dataGridViewDeferredBooks.Columns[0].Visible = false;
            }
        }
        private async void UpdateDataBaseThemes()
        {
            using (MyBooksShopContext contex = new MyBooksShopContext(options))
            {
                await contex.ThemeOfBooks.LoadAsync();
                dataGridViewTheme.DataSource = null;
                dataGridViewTheme.DataSource = contex.ThemeOfBooks
                    .Select(t => new
                    {
                        t.Id,
                        Theme = t.Name
                    }).ToList();
                dataGridViewTheme.Columns[0].Visible = false;
            }
        }

        private void btnShowAllData_Click(object sender, EventArgs e)
        {
            UpdateDataBaseBooks();
            UpdateDataBaseAuthors();
            UpdateDataBaseGenres();
            UpdateDataBasePublishingHauses();
            UpdateDataBaseTowns();
            UpdateDataBaseCountries();
            UpdateDataBaseBuyedBooks();
            UpdateDataBaseBuyers();
            UpdateDataBaseDeferredBooks();
            UpdateDataBaseThemes();
        }

        //Insert
        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            FormAddBook formAddBook = new FormAddBook(book);
            if (formAddBook.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    Book? bookFromDataBase = await context.Books.FirstOrDefaultAsync(t => t.Title == book.Title && t.AuthorId == book.AuthorId
                    && t.PublishingHouseId == book.PublishingHouseId && t.Page == book.Page && t.Genre == book.Genre
                    && t.Year == book.Year && t.Cost == book.Cost);
                    if (bookFromDataBase != null)
                    {
                        MessageBox.Show("The book is already exists. The book was added to the total count.");
                        bookFromDataBase.Count += book.Count;
                        await context.SaveChangesAsync();
                        UpdateDataBaseBooks();
                    }
                    else
                    {
                        context.Books.Add(book);
                        await context.SaveChangesAsync();
                        UpdateDataBaseBooks();
                    }
                }
            }
        }
        private async void btnAddAuthor_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            FormAddAuthor formAddAuthor = new FormAddAuthor(author);
            if (formAddAuthor.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    Author? authorFromDataBase = await context.Authors.FirstOrDefaultAsync
                        (t => t.Firstname == author.Firstname && t.Lastname == author.Lastname);
                    if (authorFromDataBase != null)
                    {
                        MessageBox.Show("The author is already exists.");
                    }
                    else
                    {
                        context.Authors.Add(author);
                        await context.SaveChangesAsync();
                        UpdateDataBaseAuthors();
                    }
                }
            }
        }
        private async void btnAddGenres_Click(object sender, EventArgs e)
        {
            Genre genre = new Genre();
            FormAddGenre formAddGenres = new FormAddGenre(genre);
            if (formAddGenres.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    var name = context.Genres.FirstOrDefault(t => t.Name == genre.Name);
                    if (name != null)
                    {
                        MessageBox.Show("The genre is already exists.");
                    }
                    else
                    {
                        context.Genres.Add(genre);
                        await context.SaveChangesAsync();
                        UpdateDataBaseGenres();
                    }
                }
            }
        }
        private async void btnAddPublishingH_Click(object sender, EventArgs e)
        {
            PublishingHouse house = new PublishingHouse();
            FormAddPublishingHouse formAddPublishingHouse = new FormAddPublishingHouse(house);
            if (formAddPublishingHouse.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    PublishingHouse? publishingHouseFromDataBase = await context.PublishingHouses.FirstOrDefaultAsync
                        (t => t.Name == house.Name && t.TownId == house.TownId);
                    if (publishingHouseFromDataBase != null)
                    {
                        MessageBox.Show("The publishing house is already exists.");
                    }
                    else
                    {
                        context.PublishingHouses.Add(house);
                        await context.SaveChangesAsync();
                        UpdateDataBasePublishingHauses();
                    }
                }
            }
        }
        private async void btnAddTown_Click(object sender, EventArgs e)
        {
            Town town = new Town();
            FormAddTown formAddTown = new FormAddTown(town);
            if (formAddTown.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    Town? townFromDataBase = await context.Towns.FirstOrDefaultAsync
                        (t => t.Name == town.Name && t.CountryId == town.CountryId);
                    if (townFromDataBase != null)
                    {
                        MessageBox.Show("The town is already exists.");
                    }
                    else
                    {
                        context.Towns.Add(town);
                        await context.SaveChangesAsync();
                        UpdateDataBaseTowns();
                    }
                }
            }

        }
        private async void btnAddCountry_Click(object sender, EventArgs e)
        {
            Country country = new Country();
            FormAddCountry formAddCountry = new FormAddCountry(country);
            if (formAddCountry.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    var name = await context.Countries.FirstOrDefaultAsync(t => t.Name == country.Name);
                    if (name != null)
                    {
                        MessageBox.Show("The country is already exists.");
                    }
                    else
                    {
                        context.Countries.Add(country);
                        await context.SaveChangesAsync();
                        UpdateDataBaseCountries();
                    }
                }
            }
        }
        private async void btnAddTheme_Click(object sender, EventArgs e)
        {
            ThemeOfBook themeOfBook = new ThemeOfBook();
            FormAddTheme formAddTheme = new FormAddTheme(themeOfBook);
            if (formAddTheme.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    var name = await context.ThemeOfBooks.FirstOrDefaultAsync(t => t.Name == themeOfBook.Name);
                    if (name != null)
                    {
                        MessageBox.Show("The theme is already exists.");
                    }
                    else
                    {
                        context.ThemeOfBooks.Add(themeOfBook);
                        await context.SaveChangesAsync();
                        UpdateDataBaseThemes();
                    }

                    cmbThemeOfBook.DataSource = null;
                    cmbThemeOfBook.DisplayMember = nameof(ThemeOfBook.Name);
                    cmbThemeOfBook.ValueMember = nameof(ThemeOfBook.Id);
                    cmbThemeOfBook.DataSource = context.ThemeOfBooks.ToList();
                }
            }
        }

        //Edit
        private async void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBooks.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Book? book = await context.Books.FindAsync(id);
                        if (book != null)
                        {
                            FormAddBook formAddBook = new FormAddBook(book);
                            if (formAddBook.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The book is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The book`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose book!");
            }
        }
        private async void btnEditAuthor_Click(object sender, EventArgs e)
        {
            if (dataGridViewAuthors.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewAuthors.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Author? author = await context.Authors.FindAsync(id);
                        if (author != null)
                        {
                            FormAddAuthor formAddAuthor = new FormAddAuthor(author);
                            if (formAddAuthor.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseAuthors();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The author is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The author`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose author!");
            }
        }
        private async void btnEditGenres_Click(object sender, EventArgs e)
        {
            if (dataGridViewGenres.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewGenres.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Genre? genre = await context.Genres.FindAsync(id);
                        if (genre != null)
                        {
                            FormAddGenre formAddGenre = new FormAddGenre(genre);
                            if (formAddGenre.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseGenres();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The genre is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The genre`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose genre!");
            }
        }
        private async void btnEditPublishingH_Click(object sender, EventArgs e)
        {
            if (dataGridViewPublishingH.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewPublishingH.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        PublishingHouse? house = await context.PublishingHouses.FindAsync(id);
                        if (house != null)
                        {
                            FormAddPublishingHouse formAddPublishingHouse = new FormAddPublishingHouse(house);
                            if (formAddPublishingHouse.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBasePublishingHauses();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The publish house is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The publish house`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose publish house!");
            }
        }
        private async void btnEditTown_Click(object sender, EventArgs e)
        {
            if (dataGridViewTowns.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewTowns.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Town? town = await context.Towns.FindAsync(id);
                        if (town != null)
                        {
                            FormAddTown formAddTown = new FormAddTown(town);
                            if (formAddTown.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseTowns();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The town is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The town`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose town house!");
            }
        }
        private async void btnEditCountry_Click(object sender, EventArgs e)
        {
            if (dataGridViewCountries.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewCountries.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Country? country = await context.Countries.FindAsync(id);
                        if (country != null)
                        {
                            FormAddCountry formAddCountry = new FormAddCountry(country);
                            if (formAddCountry.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseCountries();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The country is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The country`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose country!");
            }
        }
        private async void btnEditTheme_Click(object sender, EventArgs e)
        {
            if (dataGridViewTheme.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewTheme.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        ThemeOfBook? theme = await context.ThemeOfBooks.FindAsync(id);
                        if (theme != null)
                        {
                            FormAddTheme formAddTheme = new FormAddTheme(theme);
                            if (formAddTheme.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseThemes();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The theme is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The theme`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose theme!");
            }
        }

        //Delete
        private async void btnDelBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBooks.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Book? book = await context.Books.FindAsync(id);
                        if (book != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this book from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.Books.Remove(book);
                                await context.SaveChangesAsync();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The book is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The book`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose book!");
            }
        }
        private async void btnDelAuthor_Click(object sender, EventArgs e)
        {
            if (dataGridViewAuthors.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewAuthors.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Author? author = await context.Authors.FindAsync(id);
                        if (author != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this author from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.Authors.Remove(author);
                                await context.SaveChangesAsync();
                                UpdateDataBaseAuthors();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The author is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The author`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose author!");
            }
        }
        private async void btnDelGenre_Click(object sender, EventArgs e)
        {
            if (dataGridViewGenres.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewGenres.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Genre? genre = await context.Genres.FindAsync(id);
                        if (genre != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this genre from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.Genres.Remove(genre);
                                await context.SaveChangesAsync();
                                UpdateDataBaseGenres();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The genre is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The genre`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose genre!");
            }
        }
        private async void btnDelPublishingH_Click(object sender, EventArgs e)
        {
            if (dataGridViewPublishingH.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewPublishingH.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        PublishingHouse? publishingHouse = await context.PublishingHouses.FindAsync(id);
                        if (publishingHouse != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this publishing house from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.PublishingHouses.Remove(publishingHouse);
                                await context.SaveChangesAsync();
                                UpdateDataBasePublishingHauses();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The publishing house is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The publishing house`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose publishing house!");
            }
        }
        private async void btnDelTown_Click(object sender, EventArgs e)
        {
            if (dataGridViewTowns.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewTowns.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Town? town = await context.Towns.FindAsync(id);
                        if (town != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this town from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.Towns.Remove(town);
                                await context.SaveChangesAsync();
                                UpdateDataBaseTowns();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The town is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The town`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose town!");
            }
        }
        private async void btnDelCountry_Click(object sender, EventArgs e)
        {
            if (dataGridViewCountries.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewCountries.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Country? country = await context.Countries.FindAsync(id);
                        if (country != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this country from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.Countries.Remove(country);
                                await context.SaveChangesAsync();
                                UpdateDataBaseCountries();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The country is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The country`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose country!");
            }
        }
        private async void btnDelTheme_Click(object sender, EventArgs e)
        {
            if (dataGridViewTheme.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewTheme.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        ThemeOfBook? themeOfBook = await context.ThemeOfBooks.FindAsync(id);
                        if (themeOfBook != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this theme from database?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                context.ThemeOfBooks.Remove(themeOfBook);
                                await context.SaveChangesAsync();
                                UpdateDataBaseThemes();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The theme is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The theme`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose theme!");
            }
        }

        //Search
        private void btnSearchByTilte_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.DataSource = null;
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                var book = context.Books.Where(t => t.Title == txtSearchName.Text)
                    .Select(t => new
                    {
                        t.Id,
                        t.Title,
                        t.Author.Firstname,
                        t.Author.Lastname,
                        Publishing_House = t.PublishingHouse.Name,
                        Pages = t.Page,
                        Genre = t.Genre.Name,
                        t.Year,
                        t.Cost,
                        t.Price,
                        t.Count,
                        t.DateTime,
                        t.IsDilogy,
                        t.IsWrittenOff
                    }).ToList();
                if (book != null)
                    dataGridViewBooks.DataSource = book;
                else
                    MessageBox.Show("The book is not found.");
            }
        }
        private void btnSearchByAuthor_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.DataSource = null;
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                var book = context.Books.Where(t => t.Author.Firstname == txtSearchName.Text
                && t.Author.Lastname == txtSearchSurname.Text)
                    .Select(t => new
                    {
                        t.Id,
                        t.Title,
                        t.Author.Firstname,
                        t.Author.Lastname,
                        Publishing_House = t.PublishingHouse.Name,
                        Pages = t.Page,
                        Genre = t.Genre.Name,
                        t.Year,
                        t.Cost,
                        t.Price,
                        t.Count,
                        t.DateTime,
                        t.IsDilogy,
                        t.IsWrittenOff
                    }).ToList();
                if (book != null)
                    dataGridViewBooks.DataSource = book;
                else
                    MessageBox.Show("The book is not found.");
            }
        }
        private void btnSearchByGenre_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.DataSource = null;
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                var book = context.Books.Where(t => t.Genre.Name == txtSearchName.Text)
                    .Select(t => new
                    {
                        t.Id,
                        t.Title,
                        t.Author.Firstname,
                        t.Author.Lastname,
                        Publishing_House = t.PublishingHouse.Name,
                        Pages = t.Page,
                        Genre = t.Genre.Name,
                        t.Year,
                        t.Cost,
                        t.Price,
                        t.Count,
                        t.DateTime,
                        t.IsDilogy,
                        t.IsWrittenOff
                    }).ToList();

                if (book != null)
                    dataGridViewBooks.DataSource = book;
                else
                    MessageBox.Show("The book is not found.");
            }
        }

        //Buy book
        private async void btnBuyBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBooks.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Book? book = await context.Books.FindAsync(id);
                        if (book != null)
                        {
                            FormBuyBook formBuyBook = new FormBuyBook(book);
                            if (formBuyBook.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseBooks();
                                UpdateDataBaseBuyedBooks();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a book to buy.");
            }
        }

        //Write_off_book
        private async void btnBookWriteOff_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBooks.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Book? book = await context.Books.FindAsync(id);
                        if (book != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to write off this book?", "Quetion", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                book.IsWrittenOff = true;
                                book.Count = 0;
                                await context.SaveChangesAsync();
                                UpdateDataBaseBooks();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The book is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The book`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose book!");
            }
        }

        //Show_new_books
        private void btnShowNewBooks_Click(object sender, EventArgs e)
        {
            dataGridViewInfo.DataSource = null;
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                var book = context.Books.Where(t => t.DateTime >= DateTime.Today.AddDays(-5) && t.Count != 0)
                    .Select(t => new
                    {
                        t.Id,
                        t.Title,
                        t.Author.Firstname,
                        t.Author.Lastname,
                        Publishing_House = t.PublishingHouse.Name,
                        Pages = t.Page,
                        Genre = t.Genre.Name,
                        t.Year,
                        t.Cost,
                        t.Price,
                        t.Count,
                        t.DateTime,
                        t.IsDilogy,
                        t.IsWrittenOff
                    }).ToList();
                if (book != null)
                    dataGridViewInfo.DataSource = book;
                else
                    MessageBox.Show("The book is not found.");
            }
        }

        //Show by buyed books
        private void btnTopBuyedBook_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks
                    .GroupBy(t => t.Book.Title).Select(g => new { Title = g.Key, Count = g.Sum(x => x.AmountOfBuy) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }
        private void btnPopularAuthors_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks
                    .GroupBy(t => t.Book.Author.Firstname + " " + t.Book.Author.Lastname).Select(g => new { Author = g.Key, Count = g.Sum(x => x.AmountOfBuy) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }
        private void btnPopularGenresOfDay_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks.Where(t => t.DateTimeOfBuy >= DateTime.Today).Select(t => new { Genre = t.Book.Genre.Name, Amount = t.AmountOfBuy, Date = t.DateTimeOfBuy })
                    .GroupBy(t => t.Genre).Select(g => new { NameOfGenre = g.Key, Count = g.Sum(x => x.Amount) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }
        private void btnPopularGenresOfWeek_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks.Where(t => t.DateTimeOfBuy >= DateTime.Today.AddDays(-7)).Select(t => new { Genre = t.Book.Genre.Name, Amount = t.AmountOfBuy, Date = t.DateTimeOfBuy })
                    .GroupBy(t => t.Genre).Select(g => new { NameOfGenre = g.Key, Count = g.Sum(x => x.Amount) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }
        private void btnPopularGenresOfMonth_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks.Where(t => t.DateTimeOfBuy >= DateTime.Today.AddDays(-30)).Select(t => new { Genre = t.Book.Genre.Name, Amount = t.AmountOfBuy, Date = t.DateTimeOfBuy })
                    .GroupBy(t => t.Genre).Select(g => new { NameOfGenre = g.Key, Count = g.Sum(x => x.Amount) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }
        private void btnPopularGenresOfYear_Click(object sender, EventArgs e)
        {
            using (MyBooksShopContext context = new MyBooksShopContext(options))
            {
                dataGridViewInfo.DataSource = context.BuyedBooks.Where(t => t.DateTimeOfBuy >= DateTime.Today.AddDays(-365)).Select(t => new { Genre = t.Book.Genre.Name, Amount = t.AmountOfBuy, Date = t.DateTimeOfBuy })
                    .GroupBy(t => t.Genre).Select(g => new { NameOfGenre = g.Key, Count = g.Sum(x => x.Amount) })
                    .OrderByDescending(g => g.Count).ToList();
            }
        }

        //Deferred books
        private async void btnDeferBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBooks.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Book? book = await context.Books.FindAsync(id);
                        Buyer buyer = new Buyer();
                        FormDeferredBook formDeferredBook = new FormDeferredBook(buyer, book!);
                        if (formDeferredBook.ShowDialog() == DialogResult.OK)
                        {
                            await context.SaveChangesAsync();
                            UpdateDataBaseBooks();
                            UpdateDataBaseDeferredBooks();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose a book to defer.");
            }
        }

        //Buyer
        private async void btnAddNewBuyer_Click(object sender, EventArgs e)
        {
            Buyer buyer = new Buyer();
            FormAddBuyercs formAddBuyercs = new FormAddBuyercs(buyer);
            if (formAddBuyercs.ShowDialog() == DialogResult.OK)
            {
                using (MyBooksShopContext context = new MyBooksShopContext(options))
                {
                    Buyer? buyerFromDataBase = await context.Buyers.FirstOrDefaultAsync(t => t.Name == buyer.Name && t.Surname == buyer.Surname
                    && t.E_Mail == buyer.E_Mail && t.TelNummer == buyer.TelNummer);
                    if (buyerFromDataBase != null)
                    {
                        MessageBox.Show("The buyer is already exists.");
                    }
                    else
                    {
                        context.Buyers.Add(buyer);
                        await context.SaveChangesAsync();
                        UpdateDataBaseBuyers();
                    }
                }
            }
        }
        private async void btnEditBuyer_Click(object sender, EventArgs e)
        {
            if (dataGridViewBuyers.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBuyers.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Buyer? buyer = await context.Buyers.FindAsync(id);
                        if (buyer != null)
                        {
                            FormAddBuyercs formAddBuyercs = new FormAddBuyercs(buyer);
                            if (formAddBuyercs.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateDataBaseBuyers();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The buyer is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The buyer`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose buyer!");
            }
        }
        private async void btnDeleteBuyer_Click(object sender, EventArgs e)
        {
            if (dataGridViewBuyers.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridViewBuyers.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Buyer? buyer = await context.Buyers.FindAsync(id);
                        if (buyer != null)
                        {
                            DialogResult dialogResult = MessageBox.Show("Do you sure to delete this buyer from database?", "Quetion", MessageBoxButtons.OKCancel);
                            context.Buyers.Remove(buyer);
                            await context.SaveChangesAsync();
                            UpdateDataBaseBuyers();
                        }
                        else
                        {
                            MessageBox.Show("The buyer is not exists.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The buyer`s id is not correct.");
                }
            }
            else
            {
                MessageBox.Show("Choose buyer!");
            }
        }

        //Work with discount
        private async void chbDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDiscount.Checked)
            {
                if (int.TryParse(cmbThemeOfBook.SelectedValue.ToString(), out int id))
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        ThemeOfBook? themeOfBook = await context.ThemeOfBooks.FindAsync(id);
                        var bookDiscount = context.Books.Where(t => t.ThemeOfBookId == id);

                        if (int.TryParse(nudAmountOfDiscount.Value.ToString(), out int discount))
                        {
                            foreach (var book in bookDiscount)
                            {
                                //books = new Dictionary<int, double>();
                                Book? bookFromDataBase = await context.Books.FindAsync(book.Id);
                                books.Add(bookFromDataBase!.Id, bookFromDataBase.Cost);
                                bookFromDataBase!.Cost -= (bookFromDataBase.Cost * discount / 100);
                            }
                            await context.SaveChangesAsync();
                            UpdateDataBaseBooks();
                            cmbThemeOfBook.Enabled = false;
                            nudAmountOfDiscount.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (books.Count > 0)
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        foreach (var book in books)
                        {
                            Book? bookFromDataBase = await context.Books.FindAsync(book.Key);
                            bookFromDataBase!.Cost = book.Value;

                        }
                        await context.SaveChangesAsync();
                        UpdateDataBaseBooks();
                        cmbThemeOfBook.Enabled = true;
                        nudAmountOfDiscount.Enabled = true;
                        books.Clear();
                    }
                }
                else
                {
                    cmbThemeOfBook.Enabled = true;
                    nudAmountOfDiscount.Enabled = true;
                }
            }
        }

        //LogIn
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "Log In")
            {
                Login login = new Login();
                FormRagistration formRagistration = new FormRagistration(login);
                if (formRagistration.ShowDialog() == DialogResult.OK)
                {
                    using (MyBooksShopContext context = new MyBooksShopContext(options))
                    {
                        Login? loginFromDataBase = await context.Logins.FirstOrDefaultAsync(t => t.Username == login.Username);
                        if (loginFromDataBase != null)
                        {
                            MessageBox.Show("The user is already exists.");
                            formRagistration.ShowDialog();
                        }
                        else
                        {
                            context.Logins.Add(login);
                            await context.SaveChangesAsync();
                            MessageBox.Show("The user is registered.");
                        }
                    }
                }

                if (checkLogin)
                {
                    gpbWorkWithDatabase.Enabled = true;
                    gpbWorkWithClient.Enabled = true;
                    gpbWorkWithActions.Enabled = true;
                    gpbSearchbook.Enabled = true;
                    gpbShowTopOfBuyedBooks.Enabled = true;
                    gpbWorkWithBook.Enabled = true;
                    btnShowAllData.Enabled = true;
                    btnLogin.Text = "Log Out";

                    UpdateDataBaseBooks();
                    UpdateDataBaseAuthors();
                    UpdateDataBaseGenres();
                    UpdateDataBasePublishingHauses();
                    UpdateDataBaseTowns();
                    UpdateDataBaseCountries();
                    UpdateDataBaseBuyedBooks();
                    UpdateDataBaseBuyers();
                    UpdateDataBaseDeferredBooks();
                    UpdateDataBaseThemes();
                }
            }
            else
            {
                btnLogin.Text = "Log In";
                gpbWorkWithDatabase.Enabled = false;
                gpbWorkWithClient.Enabled = false;
                gpbWorkWithActions.Enabled = false;
                gpbSearchbook.Enabled = false;
                gpbShowTopOfBuyedBooks.Enabled = false;
                gpbWorkWithBook.Enabled = false;
                btnShowAllData.Enabled = false;
                checkLogin = false;

                dataGridViewBooks.DataSource = null;
                dataGridViewAuthors.DataSource = null;
                dataGridViewGenres.DataSource = null;
                dataGridViewPublishingH.DataSource = null;
                dataGridViewTowns.DataSource = null;
                dataGridViewCountries.DataSource = null;
                dataGridViewInfo.DataSource = null;
                dataGridViewBuyedBooks.DataSource = null;
                dataGridViewBuyers.DataSource = null;
                dataGridViewDeferredBooks.DataSource = null;
                dataGridViewTheme.DataSource = null;
            }
        }
    }
}