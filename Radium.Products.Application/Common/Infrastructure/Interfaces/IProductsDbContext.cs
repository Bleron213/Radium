using Microsoft.EntityFrameworkCore;
using Radium.Products.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Application.Common.Infrastructure.Interfaces
{
    public interface IProductsDbContext
    {
        public DbSet<Product> Products { get; }
    }
}
