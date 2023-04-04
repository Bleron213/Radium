using Radium.Shared.Utils.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Domain.Exceptions
{
    public class ProductErrors
    {
        public static CustomError ProductNotFound = new(HttpStatusCode.NotFound, "product_not_found", "Product not found");
        public static CustomError CategoryNotFound = new(HttpStatusCode.NotFound, "category_not_found", "Category not found");
    }
}
