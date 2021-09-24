using System;

namespace Ordering
{
    public class SupplierA : ISupplier
    {
        public bool CanSupplyAt(DateTime wishDeliveryDate)
        {
            var firstAvailableDeliveryDate = DateTimeProvider.Today.AddDays(4);

            var canSupplyAtDeliveryDate = wishDeliveryDate >= firstAvailableDeliveryDate;

            return canSupplyAtDeliveryDate;
        }

        public decimal GetShippingCost(Quote order)
        {
            return 5m;
        }

        public string Name => "Leverancier A";
    }
}
