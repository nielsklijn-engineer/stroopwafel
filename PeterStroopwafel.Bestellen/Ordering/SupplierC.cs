using System;

namespace Ordering
{
    public class SupplierC : ISupplier
    {
        private const int ShippingCostPercentage = 5;

        public bool CanSupplyAt(DateTime wishDeliveryDate)
        {
            var firstAvailableDeliveryDate = DateTimeProvider.Today.AddDays(5);
            var canSupplyAtDeliveryDate = wishDeliveryDate >= firstAvailableDeliveryDate;
            return canSupplyAtDeliveryDate;
        }

        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost / 100 * ShippingCostPercentage;
        }

        public string Name => "Leverancier C";
    }
}
