using API.Middleware;
using Application.Features.Products.Mapping;
using Application.Features.Products.Query;
using Application.Interfaces.Services;
using AutoMapper;
using Infrastructure.Implementations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.Data;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
                opt.UseInMemoryDatabase(nameof(DataContext)));
            services.AddMediatR(typeof(ProductList.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"),
                    true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddTransient<IBasketService, BasketService>();
            services.AddSwaggerGen(opt =>
                opt.SwaggerDoc("v1", new OpenApiInfo{Title = "E-commerce",Version = "v1"}));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/v1/swagger.json",
                "E-commerce API v1"));
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
