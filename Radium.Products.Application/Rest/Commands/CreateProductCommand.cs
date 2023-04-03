using MediatR;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Rest.Contracts.Requests;
using Radium.Products.Rest.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Application.Rest.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        private readonly ProductCreateModel _model;
        public CreateProductCommand(ProductCreateModel model)
        {
            _model = model;
        }

        public static CreateProductCommand Create(ProductCreateModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return new CreateProductCommand(model);
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
        {

            public Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
