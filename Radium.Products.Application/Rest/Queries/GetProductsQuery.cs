using MediatR;
using Radium.Products.Rest.Contracts.Response;

namespace Radium.Products.Application.Rest.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public GetProductsQuery()
        {
        }

        public static GetProductsQuery Create()
        {
            return new GetProductsQuery();
        }


        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
        {

            public GetProductsQueryHandler()
            {
            }

            public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
