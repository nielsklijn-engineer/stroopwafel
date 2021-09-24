using System;
using System.Collections.Generic;

namespace Ordering
{
    public class SupplierB : ISupplier
    {
        private const decimal ShippingCostLimit = 50m;
        private const decimal ShippingCostAboveLimit = 0m;
        private const decimal ShippingCostUnderLimit = 5m;


        private IList<Holiday> _holidays = new List<Holiday>();

        private bool IsPublicHoliday(DateTime date)
        {
            //TODO: Possibly use https://github.com/nager/Nager.Date to check for holidays instead
            List<Holiday> _publicHolidays = new List<Holiday>();
            var christmas = new Holiday(12,25);
            var easter = new Holiday(17, 4);
            _publicHolidays.Add(christmas);
            _publicHolidays.Add(easter);
            
            foreach (var holiday in _publicHolidays)
            {
                var isHoliday = date.Date.Day == holiday.Day && date.Month == holiday.Month;

                if (isHoliday)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanSupplyAt(DateTime wishDeliveryDate)
        {
            if (wishDeliveryDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            if (IsPublicHoliday(wishDeliveryDate))
            {
                return false;
            }

            var firstAvailableDeliveryDate = DateTimeProvider.Today.AddDays(3);
            var canSupplyAtDeliveryDate = wishDeliveryDate >= firstAvailableDeliveryDate;
            return canSupplyAtDeliveryDate;
        }

        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost > ShippingCostLimit ? ShippingCostAboveLimit : ShippingCostUnderLimit;
        }

        public string Name => "Leverancier B";
    }
}
