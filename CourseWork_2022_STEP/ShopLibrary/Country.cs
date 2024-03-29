﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        public ICollection<Town> Towns { get; set; }
        public Country()
        {
            Towns = new HashSet<Town>();
        }
    }
}
