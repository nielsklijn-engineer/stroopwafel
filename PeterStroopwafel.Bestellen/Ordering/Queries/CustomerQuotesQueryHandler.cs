using System.Collections.Generic;
using System.Linq;
using Ordering.CustomerQuote;

namespace Ordering.Queries
{
    public class CustomerQuotesQueryHandler 
    {
        private readonly IEnumerable<ICustomerQuoteStrategy> _quoteStrategies;

        public CustomerQuotesQueryHandler(IEnumerable<ICustomerQuoteStrategy> quoteStrategies)
        {
            _quoteStrategies = quoteStrategies;
        }

        public CustomerQuote.CustomerQuote Handle(QuotesQuery query)
        {
            var wishdate = query.WishDate;
            var orderlines = query.OrderLines;

            var quoteStrategies = _quoteStrategies.Select(x => x.GetCustomerQuote(orderlines.ToList(), wishdate)).ToList();
            
            var cheapestQuote = quoteStrategies.OrderBy(x=>x.TotalPrice).First();

            return cheapestQuote;
        }
    }
}
