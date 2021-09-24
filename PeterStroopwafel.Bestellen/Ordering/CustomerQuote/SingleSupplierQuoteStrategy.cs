using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.CustomerQuote
{
    /// <summary>
    /// Gets the cheapest quote for a single suplier 
    /// </summary>
    public class SingleSupplierQuoteStrategy : CustomerQuoteStrategyBase, ICustomerQuoteStrategy
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public SingleSupplierQuoteStrategy(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices) : base(stroopwafelSupplierServices)
        {
        }

        public CustomerQuote GetCustomerQuote(List<KeyValuePair<StroopwafelType, int>> orderLines, DateTime wishdate)
        {
            var quotePerSupplier = GetAvailableSuppliers(wishdate).Select(service => service.GetQuote(orderLines))
                .ToList();

            var quotes = new List<Quote>();
            
            if (quotePerSupplier.Any())
            {
                var cheapestQuote = quotePerSupplier.OrderBy(x => x.TotalPrice).First();
                quotes.Add(cheapestQuote);
            }

            return new CustomerQuote(quotes, wishdate);


        }
    }
}