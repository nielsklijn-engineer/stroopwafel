using System.Collections.Generic;

namespace Ordering.Queries
{
    public class CustomerQuotesQuery 
    {
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public CustomerQuotesQuery(IList<KeyValuePair<StroopwafelType, int>> orderLines)
        {
            OrderLines = orderLines;
        }
    }
}
