using Microsoft.Extensions.DependencyInjection;
using Ordering.Commands;
using Ordering.CustomerQuote;
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


            //services.AddTransient<ICustomerQuoteStrategy, SingleSupplierQuoteStrategy>();
            services.AddTransient<ICustomerQuoteStrategy, MultipleSupplierQuoteStrategy>();
            
            services.AddTransient<ICommandHandler<OrderCommand>, OrderCommandHandler>();
            services.AddTransient<ICommandHandler<CustomerOrderCommand>, CustomerOrderCommandHandler>();

            services.AddTransient<QuotesQueryHandler, QuotesQueryHandler>();
            services.AddTransient<CustomerQuotesQueryHandler, CustomerQuotesQueryHandler>();
            
            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
            
            
            using(var client = new OrderContext())
            {
                client.Database.EnsureCreated();
            }

            return services;
        }
    }
}