using MediatR;
using Radium.Products.Rest.Contracts.Response;

namespace Radium.Products.Application.Rest.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
    {
        public readonly Guid ProductId;
        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }

        public static GetProductByIdQuery Create(Guid productId)
        {
            ArgumentNullException.ThrowIfNull(productId);
            return new GetProductByIdQuery(productId);
        }


        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
        {

            public GetProductByIdQueryHandler()
            {
            }

            Task<GetProductByIdResponse> IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>.Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
