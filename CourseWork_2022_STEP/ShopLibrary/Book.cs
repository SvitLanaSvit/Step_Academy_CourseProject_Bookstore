using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public int PublishingHouseId { get; set; }
        public int Page { get; set; }
        public int GenreId { get; set; }
        public int Year { get; set; }
        public double Cost { get; set; }
        [NotMapped]
        public double Price => Cost * 0.20 + Cost;
        public int Count { get; set; }
        public bool IsDilogy { get; set; }
        public bool IsWrittenOff { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ThemeOfBook))]
        public int ThemeOfBookId { get; set; } //new
        public ThemeOfBook ThemeOfBook { get; set; } = null!; //new

        public Author Author { get; set; } = null!;
        public PublishingHouse PublishingHouse { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
        public ICollection<BuyedBook> BuyedBooks { get; set; } = null!; 
        public ICollection<DeferredBook> DeferredBooks { get; set; } = null!;

        public Book() 
        {
            BuyedBooks = new HashSet<BuyedBook>();
            DeferredBooks = new HashSet<DeferredBook>();
        }
    }
}