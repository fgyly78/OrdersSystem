using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersSystem.Application.Common.Interfaces;
using OrdersSystem.Application.Products.Queries.GetCustomerProducts;
using OrdersSystem.Domain.Repositories;
using OrdersSystem.Infrastructure.ReadServices;
using OrdersSystem.Infrastructure.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Application.Common.Interfaces.ReadServices;
using OrdersSystem.Infrastructure.Cashing;


namespace OrdersSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerReadService, CustomerReadService>();
            services.AddScoped<IOrderReadService, OrderReadService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddStackExchangeRedisCache(options =>
                {
                    options.ConfigurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                });

            return services;
        }
    }
}
