using System.Collections.Generic;
using System.Linq;

namespace Ordering.Queries
{
    public class CustomerQuotesQueryHandler 
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public CustomerQuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public CustomerQuote Handle(QuotesQuery query)
        {
            var quotes = _stroopwafelSupplierServices
                .Where(service => service.IsAvailable)
                .Select(service => service.GetQuote(query.OrderLines))
                .ToList();

            return new CustomerQuote(quotes);
        }
    }
}
