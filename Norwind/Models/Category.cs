﻿using System;
using System.Collections.Generic;

namespace Norwind.Models
{
    public partial class Category :BaseModel
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
