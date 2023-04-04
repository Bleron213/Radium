using MediatR;
using Microsoft.EntityFrameworkCore;
using Radium.Products.Application.Common.Infrastructure.Interfaces;
using Radium.Products.Domain.Exceptions;
using Radium.Products.Rest.Contracts.Requests;
using Radium.Products.Rest.Contracts.Response;
using Radium.Shared.Utils.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Application.Rest.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public readonly int ProductId;
        public readonly ProductUpdateModel Model;
        public UpdateProductCommand(int productId, ProductUpdateModel model)
        {
            ProductId = productId;
            Model = model;
        }

        public static UpdateProductCommand Create(int productId, ProductUpdateModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return new UpdateProductCommand(productId, model);
        }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
        {
            private readonly IProductsDbContext _dbContext;

            public UpdateProductCommandHandler(IProductsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == request.ProductId);
                
                if(product == null) 
                    throw new AppException(ProductErrors.ProductNotFound);

                product.Name = request.Model.Name;
                product.Description = request.Model.Description;
                product.Price = request.Model.Price;

                await _dbContext.SaveChangesAsync();
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new ProductCategoryDto
                    {
                        CategoryId = product.Category.Id,
                        Name = product.Category.Name
                    }
                };
            }
        }
    }
}
