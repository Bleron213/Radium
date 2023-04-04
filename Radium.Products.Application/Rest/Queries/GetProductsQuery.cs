using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Radium.Products.Application.Common.Infrastructure.Interfaces;
using Radium.Products.Entities.Models;
using Radium.Products.Rest.Contracts.Response;
using Radium.Shared.Utils.Requests;
using Radium.Shared.Utils.Responses;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Radium.Products.Application.Rest.Queries
{
    public class GetProductsQuery : IRequest<PagedResponse<ProductDto>>
    {
        private readonly PaginationFilterRequest _paginationFilter;
        public GetProductsQuery(PaginationFilterRequest paginationFilter)
        {
            _paginationFilter = paginationFilter;
        }

        public static GetProductsQuery Create(PaginationFilterRequest paginationFilter)
        {
            ArgumentNullException.ThrowIfNull(paginationFilter);
            return new GetProductsQuery(paginationFilter);
        }


        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResponse<ProductDto>>
        {
            private readonly IProductsDbContext _dbContext;

            public GetProductsQueryHandler(IProductsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<PagedResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Product, bool>> wherePredicate = x => true;

                if (!string.IsNullOrEmpty(request._paginationFilter.Search))
                {
                    wherePredicate = wherePredicate.And(x => EF.Functions.Like(x.Name, $"%{request._paginationFilter.Search}%"));
                }

                var productsQueryable = _dbContext.Products.Where(wherePredicate).AsQueryable();
                var totalCount = await productsQueryable.CountAsync(cancellationToken);

                IOrderedQueryable<Product>? productsOrderable = null;
                if(request._paginationFilter.Sort == Shared.Utils.Enums.SortBy.Ascending)
                {
                    productsOrderable = productsQueryable.OrderBy(x => x.Id);
                }
                else
                {
                    productsOrderable = productsQueryable.OrderByDescending(x => x.Id);
                }

                productsQueryable = productsOrderable
                    .Include(x => x.Category)
                    .Skip((request._paginationFilter.PageNumber - 1) * request._paginationFilter.PageSize)
                    .Take(request._paginationFilter.PageSize)
                    .AsQueryable();

                var query = productsQueryable.ToQueryString();

                var result = await productsQueryable.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = new ProductCategoryDto
                    {
                        CategoryId = x.CategoryId,
                        Name = x.Category.Name
                    }
                }).ToListAsync(cancellationToken);

                return new PagedResponse<ProductDto>(result, totalCount, request._paginationFilter.PageNumber, request._paginationFilter.PageSize);
            
            }
        }

    }
}
