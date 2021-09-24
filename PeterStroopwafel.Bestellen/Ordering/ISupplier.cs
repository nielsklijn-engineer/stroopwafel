using System;

namespace Ordering
{
    public interface ISupplier
    {

        public bool CanSupplyAt(DateTime wishDeliveryDate);
        decimal GetShippingCost(Quote order);

        string Name { get; }
    }
}
