using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.CustomerQuote
{
    /// <summary>
    /// Get the cheapest quote per stroopwafel type 
    /// </summary>
    public class MultipleSupplierQuoteStrategy : CustomerQuoteStrategyBase, ICustomerQuoteStrategy
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public MultipleSupplierQuoteStrategy(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices) : base(stroopwafelSupplierServices)
        {
        }

        public CustomerQuote GetCustomerQuote(List<KeyValuePair<StroopwafelType, int>> orderLines, DateTime wishdate)
        {
            var availableSuppliers = GetAvailableSuppliers(wishdate);

            var cheapestQuotes = new List<Quote>();
            
            //Select cheapest quote (supplier) per stroopwafel type 
            foreach (var orderLine  in orderLines)
            {
                var quotesPerOrderLine = new List<Quote>();
                foreach (var supplier in availableSuppliers)
                {
                    var supplierQuote = supplier.GetQuote(new List<KeyValuePair<StroopwafelType, int>>() {orderLine});
                    
                    //TODO: In the future add check if other items were ordered with this supplier and combine the quote to save on shipping costs 
                    
                    quotesPerOrderLine.Add(supplierQuote);
                }

                if (quotesPerOrderLine.Any())
                {
                    var cheapestQuote = quotesPerOrderLine.OrderBy(x => x.TotalPrice).First();
                    cheapestQuotes.Add(cheapestQuote);
                }
            }
            
            return new CustomerQuote(cheapestQuotes, wishdate);
        }
    }
}