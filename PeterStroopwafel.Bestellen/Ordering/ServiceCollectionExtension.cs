using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
﻿using Microsoft.Extensions.DependencyInjection;
using Ordering.Commands;
using Ordering.Database;
using Ordering.Queries;
using Ordering.Services;

namespace Ordering {
    
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddOrdering(this IServiceCollection services) {
            services.AddEntityFrameworkSqlite().AddDbContext<OrderContext>();
            services.AddTransient<IStroopwafelSupplierService, StroopwafelSupplierAService>();
            services.AddTransient<IStroopwafelSupplierService, StroopwafelSupplierBService>();
            services.AddTransient<IStroopwafelSupplierService, StroopwafelSupplierCService>();


            services.AddTransient<ICommandHandler<OrderCommand>, OrderCommandHandler>();
            services.AddTransient<QuotesQueryHandler, QuotesQueryHandler>();
            
            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

            return services;
        }
    }
}