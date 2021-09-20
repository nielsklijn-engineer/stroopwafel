using System.Collections.Generic;
using System.Linq;

namespace Ordering
{
    public class CustomerQuote
    {
        private readonly IList<Quote> _supplierQuotes;
        public decimal TotalPrice => PreferredQuote.TotalPrice + ProfitMargin;
        
        public string TotalPricePresentation => TotalPrice.ToString("C");
        public decimal ProfitMargin => TotalQuantityOfProducts;
        private int TotalQuantityOfProducts => PreferredQuote.OrderLines.Sum(x => x.Amount);


        public Quote PreferredQuote { get; }

        

        public CustomerQuote(IList<Quote> supplierQuotes) {
            _supplierQuotes = supplierQuotes;

            PreferredQuote = _supplierQuotes.OrderBy(x => x.TotalPrice).First();
        }
    }
}
