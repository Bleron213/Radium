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
        DbSet<Product> Products { get; }
        DbSet<Category> Category { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
