using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Products.Entities.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
