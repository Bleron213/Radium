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
    public class DeleteProductCommand : IRequest<Unit>
    {
        public readonly int ProductId;
        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }

        public static DeleteProductCommand Create(int productId)
        {
            return new DeleteProductCommand(productId);
        }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
        {
            private readonly IProductsDbContext _productsDbContext;

            public DeleteProductCommandHandler(IProductsDbContext productsDbContext)
            {
                _productsDbContext = productsDbContext;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productsDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId,cancellationToken);

                if (product == null)
                    throw new AppException(ProductErrors.ProductNotFound);

                _productsDbContext.Products.Remove(product);

                await _productsDbContext.SaveChangesAsync();
                return new Unit();
            }
        }
    }
}
