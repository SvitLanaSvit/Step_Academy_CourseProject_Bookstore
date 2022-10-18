using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string E_Mail { get; set; } = null!;
        public string TelNummer { get; set; } = null!;
        [NotMapped]
        public string FullnameOfBuyer => $"{Name} {Surname}";

        public ICollection<DeferredBook> DeferredBooks { get; set; } = null!;
        public Buyer()
        {
            DeferredBooks = new HashSet<DeferredBook>();
        }
    }
}
