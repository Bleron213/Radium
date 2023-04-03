using MediatR;
using Radium.Products.Rest.Contracts.Response;

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

            public GetProductByIdQueryHandler()
            {
            }

            public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
