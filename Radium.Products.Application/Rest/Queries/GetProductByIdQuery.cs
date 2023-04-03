using MediatR;
using Microsoft.EntityFrameworkCore;
using Radium.Products.Application.Common.Infrastructure.Interfaces;
using Radium.Products.Domain.Exceptions;
using Radium.Products.Rest.Contracts.Response;
using Radium.Shared.Utils.Errors;

namespace Radium.Products.Application.Rest.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public readonly int ProductId;
        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }

        public static GetProductByIdQuery Create(int productId)
        {
            ArgumentNullException.ThrowIfNull(productId);
            return new GetProductByIdQuery(productId);
        }


        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
        {
            private readonly IProductsDbContext _dbContext;

            public GetProductByIdQueryHandler(IProductsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _dbContext.Products
                        .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == request.ProductId);
                
                if (result == null)
                    throw new AppException(ProductErrors.ProductNotFound);

                return new ProductDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    Category = new ProductCategoryDto
                    {
                        CategoryId = result.CategoryId,
                        Name = result.Category.Name
                    }
                };
            }
        }

    }
}
