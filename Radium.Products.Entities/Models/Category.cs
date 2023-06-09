﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Products.Entities.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }
}
