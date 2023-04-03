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
    public class DeleteProductCommand : IRequest<Unit>
    {
        private readonly int _productId;
        public DeleteProductCommand(int productId)
        {
            _productId = productId;
        }

        public static DeleteProductCommand Create(int productId)
        {
            return new DeleteProductCommand(productId);
        }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
        {
            public Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
