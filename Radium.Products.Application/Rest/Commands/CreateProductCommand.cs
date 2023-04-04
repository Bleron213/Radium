using MediatR;
using Microsoft.EntityFrameworkCore;
using Radium.Products.Application.Common.Infrastructure.Interfaces;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Domain.Exceptions;
using Radium.Products.Entities.Models;
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
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public readonly ProductCreateModel Model;
        public CreateProductCommand(ProductCreateModel model)
        {
            Model = model;
        }

        public static CreateProductCommand Create(ProductCreateModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return new CreateProductCommand(model);
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
        {
            private readonly IProductsDbContext _dbContext;

            public CreateProductCommandHandler(IProductsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if(!(await _dbContext.Category.AnyAsync(x => x.Id == request.Model.CategoryId)))
                    throw new AppException(ProductErrors.CategoryNotFound);

                var product = new Product
                {
                    Name = request.Model.Name,
                    Description = request.Model.Description,
                    Price = request.Model.Price,
                    CategoryId = request.Model.CategoryId,
                };

                product = (await _dbContext.Products.AddAsync(product, cancellationToken)).Entity;
                await _dbContext.SaveChangesAsync(cancellationToken);

                product = await _dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == product.Id);
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
