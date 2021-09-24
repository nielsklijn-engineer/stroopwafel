using System;
using System.Collections.Generic;

namespace Ordering.Queries
{
    public class QuotesQuery 
    {
        public DateTime WishDate { get; set; }
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public QuotesQuery(IList<KeyValuePair<StroopwafelType, int>> orderLines, DateTime wishDate)
        {
            WishDate = wishDate;
            OrderLines = orderLines;
        }
    }
}
