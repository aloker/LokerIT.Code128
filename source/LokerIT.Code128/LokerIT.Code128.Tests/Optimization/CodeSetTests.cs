using LokerIT.Code128;
using Xunit;

namespace Code128.Optimization
{
    public class CodeSetTests
    {
        [Fact]
        public void Supports_WhenPassingInStringWithOneUnsupportedChar_ReturnsFalse()
        {
            Assert.False(new DefaultMapping().CodeA.Supports("123AbC\t"));
        }

        [Fact]
        public void Supports_WhenPassingInSupportedString_ReturnsTrue()
        {
            Assert.True(new DefaultMapping().CodeA.Supports("123ABC\t"));
        }

        [Fact]
        public void Supports_WhenQueryingSetA_ReturnsTrueForValidChars()
        {
            var set = new DefaultMapping().CodeA;
            Assert.True(set.Supports((byte) ' '));
        }
    }
}