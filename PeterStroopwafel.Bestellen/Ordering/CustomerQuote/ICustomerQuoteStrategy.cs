using System;
using System.Collections.Generic;

namespace Ordering.CustomerQuote
{
    public interface ICustomerQuoteStrategy
    {
        public CustomerQuote GetCustomerQuote(List<KeyValuePair<StroopwafelType, int>> orderLines, DateTime wishdate);
    }
}