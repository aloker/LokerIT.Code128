using System.Linq;
using System.Text;
using LokerIT.Code128;
using Xunit;
using Xunit.Abstractions;

namespace Code128
{
    public class Code128EncoderTests
    {
        public Code128EncoderTests(ITestOutputHelper output)
        {
            _output = output;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoder = new Code128Encoder(new DefaultMapping(), EncodedCode128Factory.Default,
                Encoding.GetEncoding(1252));
        }

        private readonly ITestOutputHelper _output;
        private readonly Code128Encoder _encoder;

        [Theory]
        [InlineData("1234", "105 12 34")]
        [InlineData("12", "105 12")]
        [InlineData("123", "103 17 18 19")]
        [InlineData("123A", "103 17 18 19 33")]
        [InlineData("123a", "104 17 18 19 65")]
        [InlineData("X00Y", "103 56 16 16 57")]
        [InlineData("ABC01234", "103 33 34 35 16 99 12 34")]
        [InlineData("098x1234567y23", "104 16 25 24 88 17 99 23 45 67 100 89 18 19")]
        [InlineData("00A", "103 16 16 33")]
        [InlineData("12345A", "105 12 34 101 21 33")]
        [InlineData("ö", "104 100 86")]
        [InlineData("aö", "104 65 100 86")]
        [InlineData("aöaaaa", "104 65 100 86 65 65 65 65")]
        [InlineData("aöaööö", "104 65 100 86 65 100 100 86 86 86")]
        [InlineData("aöaöööa", "104 65 100 86 65 100 86 100 86 100 86 65")]
        [InlineData("aöaöööööa", "104 65 100 86 65 100 100 86 86 86 86 86 100 65")]
        [InlineData("A€", "103 33 101 64")]
        [InlineData("a€", "104 65 101 101 64")]
        [InlineData("a€a", "104 65 100 98 64 65")]
        [InlineData("a\t", "104 65 101 73")]
        [InlineData("a\ta", "104 65 98 73 65")]
        public void Encode_WhenGivingSample_ProducesMinimalLength(string input, string pattern)
        {
            var code = _encoder.Encode(input);
            var parts = pattern.Split(' ').Select(byte.Parse).ToArray();
            Print(code);
            Assert.Equal(parts.Length, code.Data.Count);
            Assert.Equal(parts, code.Data);
        }

        private void Print(IEncodedCode128 code)
        {
            _output.WriteLine(string.Join(", ", code.Data));
        }

        [Fact]
        public void Encode_WhenEncoding2Digits_ShouldReturnCodeC()
        {
            var code = _encoder.Encode("95");
            Assert.Equal(new byte[] {105, 95}, code.Data);
            Assert.Equal(new byte[] {105, 95, 97, 106}, code.FullData);
        }

        [Fact]
        public void Encode_WhenEncoding4Digits_ShouldReturnCodeC()
        {
            var code = _encoder.Encode("9514");
            Assert.Equal(new byte[] {105, 95, 14}, code.Data);
            Assert.Equal(new byte[] {105, 95, 14, 22, 106}, code.FullData);
        }

        [Fact]
        public void Encode_WhenEncoding5Digits_ShouldReturnCodeC()
        {
            var code = _encoder.Encode("95147");
            Assert.Equal(new byte[] {105, 95, 14, 101, 23}, code.Data);
            Assert.Equal(new byte[] {105, 95, 14, 101, 23, 5, 106}, code.FullData);
        }

        [Fact]
        public void Encode_WhenEncodingComplexExample1_ShouldGenerateMinimalLength()
        {
            var code = _encoder.Encode("098x1234567y23");
            Print(code);
            Assert.Equal(new byte[]
            {
                104, // Code B
                16, // 0
                25, // 9
                24, // 8
                88, // x
                17, // 1,
                99, // Code C
                23, // 23, 
                45, // 45, 
                67, // 67,
                100, // Code B
                89, // y,
                18, // 2,
                19 // 3
            }, code.Data);
        }

        [Fact]
        public void Encode_WhenEncodingEmptyString_ReturnsOnlyStartChar()
        {
            var code = _encoder.Encode("");
            Print(code);
            Assert.Equal(new byte[] {103}, code.Data);
            Assert.Equal(new byte[] {103, 0, 106}, code.FullData);
        }

        [Fact]
        public void Encode_WhenUsingEscPosSetup_CanEncodeExample()
        {
            var code = new Code128Encoder(new EscPosMapping(), EncodedCode128Factory.EscPos).Encode("No. 123456");
            Assert.Equal(new byte[] {123, 66, 78, 111, 46, 32, 123, 67, 12, 34, 56}, code.FullData);
        }

        [Fact]
        public void Encode_WhenUsingOneCharFromOtherSet_ShouldUseSwitch()
        {
            var code = _encoder.Encode("aa\taa");
            Assert.Equal(new byte[] {104, 65, 65, 98, 73, 65, 65}, code.Data);
        }
    }
}