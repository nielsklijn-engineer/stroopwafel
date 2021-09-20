using System;
using System.Collections.Generic;

namespace Ordering.Commands
{
    public class CustomerOrderCommand
    {
        public string CustomerName { get; set; }
        
        public DateTime WishDate { get; set; }

        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; } =
            new List<KeyValuePair<StroopwafelType, int>>();
        

        public CustomerOrderCommand(string customerName,DateTime wishDate, IList<KeyValuePair<StroopwafelType, int>> orderLines) {
            WishDate = wishDate;
            CustomerName = customerName;
            OrderLines = orderLines;
        }
    }
}
