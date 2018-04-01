using LokerIT.Code128;
using Xunit;

namespace Code128
{
    public class Mod103ChecksumCalculatorTests
    {
        public Mod103ChecksumCalculatorTests()
        {
            _calculator = new Mod103ChecksumCalculator();
        }

        private readonly Mod103ChecksumCalculator _calculator;

        [Fact]
        public void CalculateChecksum_WhenDataIsEmpty_Returns0()
        {
            Assert.Equal(0, _calculator.CalculateChecksum(new byte[0]));
        }

        [Fact]
        public void CalculateChecksum_WhenHasData_UsesIndexAsMultiplier()
        {
            Assert.Equal(41, _calculator.CalculateChecksum(new byte[] {1, 2, 3, 4, 5}));
        }

        [Fact]
        public void CalculateChecksum_WhenHasData_UsesIndexAsMultiplierAndUsesModulo()
        {
            Assert.Equal(47, _calculator.CalculateChecksum(new byte[] {200, 150, 160, 170}));
        }

        [Fact]
        public void CalculateChecksum_WhenOnlyHasStart_ReturnsStartModulo()
        {
            Assert.Equal(20, _calculator.CalculateChecksum(new byte[] {123}));
        }

        [Fact]
        public void CalculateChecksum_WhenOnlyHasStartBelow103_ReturnsStart()
        {
            Assert.Equal(102, _calculator.CalculateChecksum(new byte[] {102}));
        }
    }
}