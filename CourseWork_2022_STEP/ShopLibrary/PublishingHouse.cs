using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class PublishingHouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TownId { get; set; }

        public Town Town { get; set; } = null!;

        public ICollection<Book> Books { get; set; }

        public PublishingHouse()
        {
            Books = new HashSet<Book>();
        }
    }
}
