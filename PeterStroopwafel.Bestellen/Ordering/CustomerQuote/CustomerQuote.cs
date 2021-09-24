using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Commands;
using Ordering.Services;

namespace Ordering.CustomerQuote
{
    public class CustomerQuote
    {
        public readonly IList<Quote> SupplierQuotes;
        
        public DateTime WishDate { get; set; }
        public decimal TotalPrice => SupplierQuotes.Sum(x=>x.TotalPrice) + ProfitMargin;
        
        public string TotalPricePresentation => TotalPrice.ToString("C");
        public decimal ProfitMargin => TotalQuantityOfProducts;
        private int TotalQuantityOfProducts => SupplierQuotes.Sum(x => x.OrderLines.Sum(x => x.Amount));


        public IList<OrderCommand> GetOrderCommands()
        {
            var orderCommands = new List<OrderCommand>();

            foreach (var quote in SupplierQuotes)
            {
                var lines = new List<KeyValuePair<StroopwafelType, int>>();
                lines.AddRange(quote.OrderLines.Select(x=> new KeyValuePair<StroopwafelType, int>(x.Stroopwafel.Type,x.Amount)));
                var orderCommand = new OrderCommand(lines, quote.Supplier.Name);
                orderCommands.Add(orderCommand);
            }

            return orderCommands;
        }
        
        public CustomerQuote(IList<Quote> supplierQuotes, DateTime wishdate) {
            SupplierQuotes = supplierQuotes;
        }
    }
}
