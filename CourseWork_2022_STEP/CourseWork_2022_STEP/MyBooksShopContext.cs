using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;

namespace CourseWork_2022_STEP
{
    public class MyBooksShopContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<PublishingHouse> PublishingHouses { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Town> Towns { get; set; } = null!;

        public DbSet<BuyedBook> BuyedBooks { get; set; } = null!; 
        public DbSet<Buyer> Buyers { get; set; } = null!; 
        public DbSet<DeferredBook> DeferredBooks { get; set; } = null!;
        public DbSet<ThemeOfBook> ThemeOfBooks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Login> Logins { get; set; } = null!;

        public MyBooksShopContext(DbContextOptions<MyBooksShopContext> options) : base(options) { }
    }
}