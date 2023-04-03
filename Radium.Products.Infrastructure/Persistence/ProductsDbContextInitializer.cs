using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radium.Products.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Infrastructure.Persistence
{
    public class ProductsDbContextInitializer
    {
        private readonly ILogger<ProductsDbContextInitializer> _logger;
        private readonly ProductsDbContext _dbContext;

        public ProductsDbContextInitializer(
            ILogger<ProductsDbContextInitializer> logger,
            ProductsDbContext dbContext
            )
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_dbContext.Database.IsSqlServer())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task Seed()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            foreach (var category in Categories)
            {
                var exists = await _dbContext.Products.AnyAsync(x => x.Name == category.Name);
                if(exists) continue;
                await _dbContext.AddAsync(category);
            }

            await _dbContext.SaveChangesAsync();
        }

        #region Static Data
        private static List<Category> Categories = new List<Category>
        {
            new Category()
            {
                Name = "Computer",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Apple MacBook Pro 14",
                        Description = "Performance tailored for professionals. The new MacBook Pro comes with a 14\" display and pushes the imaginative boundaries to a new level with its performance. The significantly improved architecture of the M2 Pro simply has the brute power for all your creative ideas. And what you notice at a glance seen is the elegant design with an emphasis on quality workmanship.",
                        Price = 2500,
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/204545/img/0.jpg"
                    },
                    new Product
                    {
                        Name = "Apple MacBook Air 13.3, M1 8-core",
                        Description = "The new generation of MacBook Air from Apple is a real revolution in the performance of this compact laptop. It offers all your favorite features and functions, but the performance is on another level. With the new MacBook Air, you can edit high-resolution video as well as take advantage of professional applications.",
                        Price = 1000,
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/196948/img/0.jpg"
                    }
                }
            },
            new Category
            {
                Name = "Phone",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Apple iPhone 14 Pro Max",
                        Description = "First-class design, durability, functions and technology, all this and much more is hidden in the Apple iPhone 14 Pro Max model.",
                        Price = 1550,
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/194217/img/0.jpg"
                    },
                    new Product
                    {
                        Name = "Nokia 8210 4G, 2.8",
                        Description = "An improved old design of familiar buttons with the features of tomorrow, this is the Nokia 8210 4G - a classic reborn",
                        Price = 114,
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/194931/img/0.jpg"
                    }
                }
            },
            new Category
            {
                Name = "Photography",
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Fujifilm X-S10",
                        Price = 1559,
                        Description = "This camera features an APS-C X-Trans BSI IV CMOS image sensor and X-Processor. It supports recording to SD / SDHC / SDXC, DCI-4K (4096 x 2160) video resolution and H.264 / MOV 4 video format. In addition, it is characterized by a battery life of up to 325 shots and interfaces: USB-C , micro HDMI, 3.5 mm slot. Package includes XC15-45mm lens.",
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/144447/img/0.jpg"
                    },
                    new Product
                    {
                        Name = "Canon Zoemini S2",
                        Price = 1559,
                        Description = "This camera with 8 MPX sensor, resolution 314 x 600 dpi has a focal length of 22.6 mm. It is suitable for instant image printing with adhesive on the back. It supports ZINK technology and the front mirror and light circle is for selfie photos. It has a compact size that allows you to carry it in your pocket anywhere. Balance and exposure are adjusted as desired. It has a microSD card slot. It also supports Bluetooth for smartphone connectivity.",
                        ImageUrl = "https://hhstsyoejx.gjirafa.net/gj50/img/169395/img/0.jpg"
                    }
                }
            }
        };

        #endregion
    }
}
