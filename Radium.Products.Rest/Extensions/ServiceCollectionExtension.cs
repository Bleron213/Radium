using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Rest.Contracts.Response;
using System.Reflection;

namespace Radium.Products.Rest.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            #region Commands

            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>, GetProductByIdQuery.GetProductByIdQueryHandler>();
            #endregion

            #region Validations

            #endregion

            #region Event Handlers

            #endregion

            #region Behaviors

            #endregion
        }
    }
}
