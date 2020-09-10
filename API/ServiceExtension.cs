using System;
using Application.Features.Products.Mapping;
using Application.Features.Products.Query;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence.Data;

namespace API
{
    public static class ServiceExtension
    {
        private static IServiceProvider _serviceProvider;

        public static TService Resolve<TService>()
        {
            if (_serviceProvider == null) ConfigureDependencies(new ServiceCollection());

            return _serviceProvider.GetService<TService>();
        }

        public static TService ResolveRequired<TService>()
        {
            if (_serviceProvider == null) ConfigureDependencies(new ServiceCollection());

            return _serviceProvider.GetRequiredService<TService>();
        }

        public static void ConfigureDependencies( IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
                opt.UseInMemoryDatabase(nameof(DataContext)));
            services.AddMediatR(typeof(ProductList.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSwaggerGen(opt =>
                opt.SwaggerDoc("v1", new OpenApiInfo{Title = "E-commerce",Version = "v1"}));
            services.AddCors();
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
