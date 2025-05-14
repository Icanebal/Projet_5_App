using Projet_5.Core.Models.Entities;

namespace Projet_5_Tests
{
    public class CarForSaleTests
    {
        [Fact]
        public void EffectiveAvailability_ShouldBeTrueWhenAvailableAndDateIsNull()
        {
            var car = new CarForSale
            {
                IsAvailable = true,
                AvailabilityDate = null
            };

            Assert.True(car.EffectiveAvailability);
        }

        [Fact]
        public void EffectiveAvailability_ShouldBeTrueWhenAvailableAndDateIsToday()
        {
            var car = new CarForSale
            {
                IsAvailable = true,
                AvailabilityDate = DateOnly.FromDateTime(DateTime.Today)
            };

            Assert.True(car.EffectiveAvailability);
        }

        [Fact]
        public void EffectiveAvailability_ShouldBeFalseWhenAvailableAndDateIsInFuture()
        {
            var car = new CarForSale
            {
                IsAvailable = true,
                AvailabilityDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
            };

            Assert.False(car.EffectiveAvailability);
        }

        [Fact]
        public void EffectiveAvailability_ShouldBeFalseWhenNotAvailable()
        {
            var car = new CarForSale
            {
                IsAvailable = false,
                AvailabilityDate = null
            };

            Assert.False(car.EffectiveAvailability);
        }
    }
}
