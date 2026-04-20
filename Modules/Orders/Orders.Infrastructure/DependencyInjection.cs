using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Messaging;
using Orders.Application.Common.Interfaces;
using Orders.Application.Common.Interfaces.Messaging;
using Orders.Application.Common.Interfaces.ReadServices;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Cashing;
using Orders.Infrastructure.ReadServices;
using Orders.Infrastructure.Repositories;


namespace Orders.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersInfrastructure(
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
            services.AddScoped<IProductReadService, ProductReadService>();
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
