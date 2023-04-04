using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Radium.Products.Application.Common.Behaviors;
using Radium.Products.Application.Common.Infrastructure.Interfaces;
using Radium.Products.Application.Rest.Commands;
using Radium.Products.Application.Rest.Commands.Validators;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Infrastructure.Persistence;
using Radium.Products.Rest.Contracts.Response;
using Radium.Shared.Utils.Responses;
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
            services.AddScoped<IRequestHandler<CreateProductCommand, ProductDto>, CreateProductCommand.CreateProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, Unit>, DeleteProductCommand.DeleteProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, ProductDto>, UpdateProductCommand.UpdateProductCommandHandler>();

            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQuery.GetProductByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductsQuery, PagedResponse<ProductDto>>, GetProductsQuery.GetProductsQueryHandler>();
            #endregion

            #region Validations
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
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
            services.AddScoped<IProductsDbContext>(provider => provider.GetRequiredService<ProductsDbContext>());
            services.AddScoped<ProductsDbContextInitializer>();
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
