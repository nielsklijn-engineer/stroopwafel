using System;
using Xunit;

namespace Ordering.Tests
{
    //TODO: Add support / tests for business days
    public class SupplierCTests
    {
        [Theory]
        [InlineData("2021-01-01","2021-01-01",false)]
        [InlineData("2021-01-01","2021-01-02",false)]
        [InlineData("2021-01-01","2021-01-03",false)]
        [InlineData("2021-01-01","2021-01-04",false)]
        [InlineData("2021-01-01","2021-01-05",false)]
        [InlineData("2021-01-01","2021-01-06",true)]
        public void CanSupplyAfter5Days(DateTime currentDate, DateTime supplyDate, bool canSupply )
        {
            using (var context = new DateTimeProviderContext(currentDate))
            {
                var expectedValue = canSupply;
                var actualValue = new SupplierC().CanSupplyAt(supplyDate);
                Assert.Equal(expectedValue,actualValue);
            }
        }
    }
}