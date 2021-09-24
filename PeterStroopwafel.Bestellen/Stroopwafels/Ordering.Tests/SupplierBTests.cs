using System;
using Xunit;

namespace Ordering.Tests
{
    
    //TODO: Add support / tests for business days
    public class SupplierBTests
    {
        [Theory]
        [InlineData("2021-01-01","2021-01-01",false)]
        [InlineData("2021-01-01","2021-01-02",false)]
        [InlineData("2021-01-01","2021-01-03",false)]
        [InlineData("2021-01-01","2021-01-04",true)]
        [InlineData("2021-01-01","2021-01-05",true)]
        [InlineData("2021-01-01","2021-01-06",true)]
        public void CanSupplyAfter3Days(DateTime currentDate, DateTime supplyDate, bool canSupply )
        {
            using (var context = new DateTimeProviderContext(currentDate))
            {
                var expectedValue = canSupply;
                var actualValue = new SupplierB().CanSupplyAt(supplyDate);
                Assert.Equal(expectedValue,actualValue);
            }
        }
        
        
        [Theory]
        [InlineData("2021-12-01","2021-12-24",true)]
        [InlineData("2021-12-21","2021-12-24",true)]
        [InlineData("2021-12-01","2021-01-25",false)]
        public void CanSupplyWithChristmas(DateTime currentDate, DateTime supplyDate, bool canSupply )
        {
            using (var context = new DateTimeProviderContext(currentDate))
            {
                var expectedValue = canSupply;
                var actualValue = new SupplierB().CanSupplyAt(supplyDate);
                Assert.Equal(expectedValue,actualValue);
            }
        }
        
        
        [Theory]
        [InlineData("2021-04-13","2021-04-17",false)]
        [InlineData("2021-04-13","2021-04-16",true)]
        public void CanSupplyWithEaster(DateTime currentDate, DateTime supplyDate, bool canSupply )
        {
            using (var context = new DateTimeProviderContext(currentDate))
            {
                var expectedValue = canSupply;
                var actualValue = new SupplierB().CanSupplyAt(supplyDate);
                Assert.Equal(expectedValue,actualValue);
            }
        }
        
        
        [Theory]
        [InlineData("2021-04-01","2021-04-04",false)]
        [InlineData("2021-04-01","2021-04-11",false)]
        public void CanSupplyOnSunday(DateTime currentDate, DateTime supplyDate, bool canSupply )
        {
            using (var context = new DateTimeProviderContext(currentDate))
            {
                var expectedValue = canSupply;
                var actualValue = new SupplierB().CanSupplyAt(supplyDate);
                Assert.Equal(expectedValue,actualValue);
            }
        }
    }
}