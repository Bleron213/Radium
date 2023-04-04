using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Products.Rest.Contracts.Requests
{
    public class ProductUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
