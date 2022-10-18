using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class DeferredBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int CountOfDeferredBook { get; set; }
        public DateTime DateDeferredBook { get; set; }

        public Book Book { get; set; } = null!;
        public Buyer Buyer { get; set; } = null!;
    }
}
