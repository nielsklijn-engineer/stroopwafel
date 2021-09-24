using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.CustomerQuote
{
    public abstract class CustomerQuoteStrategyBase 
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public CustomerQuoteStrategyBase(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public IEnumerable<IStroopwafelSupplierService> GetAvailableSuppliers(DateTime wishdate)
        {
            var dayBeforeWishDate = wishdate.AddDays(1);
            return _stroopwafelSupplierServices
                .Where(service => service.IsAvailable
                                  && (service.Supplier.CanSupplyAt(wishdate) ||
                                      (service.Supplier.CanSupplyAt(dayBeforeWishDate))));
        }
    }
}