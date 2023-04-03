using MediatR;
using Radium.Products.Rest.Contracts.Requests;
using Radium.Products.Rest.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Application.Rest.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        private readonly int _productId;
        private readonly ProductUpdateModel _model;
        public UpdateProductCommand(int productId, ProductUpdateModel model)
        {
            _productId = productId;
            _model = model;
        }

        public static UpdateProductCommand Create(int productId, ProductUpdateModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return new UpdateProductCommand(productId, model);
        }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
        {

            public Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
