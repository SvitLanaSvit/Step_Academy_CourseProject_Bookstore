﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new HashSet<Book>();
        }
    }
}