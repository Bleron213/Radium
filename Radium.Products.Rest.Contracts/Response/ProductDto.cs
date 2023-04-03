using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Products.Rest.Contracts.Response
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ProductCategoryDto Category { get; set; }
    }

    public class ProductCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
