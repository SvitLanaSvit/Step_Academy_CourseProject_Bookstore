using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class BuyedBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AmountOfBuy { get; set; }
        public DateTime DateTimeOfBuy { get; set; } = DateTime.Now;

        public Book Book { get; set; } = null!;
    }
}
