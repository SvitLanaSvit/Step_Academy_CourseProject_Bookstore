using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class ThemeOfBook
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = null!;

        public ThemeOfBook()
        {
            Books = new HashSet<Book>();
        }
    }
}
