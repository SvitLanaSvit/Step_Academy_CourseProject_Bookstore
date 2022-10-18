using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Town
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;

        public ICollection<PublishingHouse> PublishingHouses { get; set; }

        public Town()
        {
            PublishingHouses = new HashSet<PublishingHouse>();
        }
    }
}
