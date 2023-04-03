using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Radium.Products.Application.Common;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Infrastructure.Persistence;
using Radium.Products.Rest.Contracts.Response;
using System.Reflection;

namespace Radium.Products.Rest.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            });

            #region Commands

            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQuery.GetProductByIdQueryHandler>();
            #endregion

            #region Validations

            #endregion

            #region Event Handlers

            #endregion

            #region Behaviors

            #endregion
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void ConfigureVersionedSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}
