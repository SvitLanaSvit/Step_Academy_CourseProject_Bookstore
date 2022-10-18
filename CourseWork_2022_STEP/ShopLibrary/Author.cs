using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;

        [NotMapped]
        public string Fullname => $"{Firstname} {Lastname}";

        public ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new HashSet<Book>();
        }
    }
}