using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokerIT.Code128;
using LokerIT.Code128.Interpreter;
using Xunit;
using Xunit.Abstractions;

namespace Code128.Interpreter
{
    public class Code128InterpreterTests
    {
        public Code128InterpreterTests(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _interpreter = new Code128Interpreter(encoding: Encoding.GetEncoding(1252));
        }

        private readonly ITestOutputHelper _output;
        private readonly Code128Interpreter _interpreter;

        private IEnumerable<byte> BuildInput(params string[] inputs)
        {
            var chars = inputs.SelectMany(c => c).Select(c => (byte) (c - '0'));
            return chars;
        }

        [Theory]
        [InlineData("211412 142112 142112 2331112", "\t")]
        [InlineData("211214 121124 121421 2331112", "a")]
        public void Interpret_WhenInputIsProvidedAsBars_ReturnsCorrectData(string data, string expected)
        {
            var input = BuildInput(data.Split(' '));

            var result = _interpreter.Interpret(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("€ö\t")]
        [InlineData("€a€ö")]
        [InlineData("00")]
        [InlineData("€ö00000€")]
        public void Interpret_WhenParsingSpecialCase_InterpretsCorrectly(string input)
        {
            var encoder = new Code128Encoder(new DefaultMapping(), EncodedCode128Factory.Default,
                Encoding.GetEncoding(1252));
            var generated = encoder.Encode(input);
            var interpreted = _interpreter.Interpret(generated.ToPatternList());
            Assert.Equal(input, interpreted);
        }

        [Theory]
        [InlineData("€ö\t")]
        [InlineData("€a€ö")]
        [InlineData("00")]
        [InlineData("€ö00000€")]
        public void Interpret_WhenParsingSpecialCaseReverse_InterpretsCorrectly(string input)
        {
            var encoder = new Code128Encoder(new DefaultMapping(), EncodedCode128Factory.Default,
                Encoding.GetEncoding(1252));
            var generated = encoder.Encode(input);
            var patternList = generated.ToPatternList();
            var interpreted = _interpreter.Interpret(Invert(patternList));
            Assert.Equal(input, interpreted);
        }

        private IEnumerable<byte> Invert(IEnumerable<string> patternList)
        {
            return BuildInput(patternList.ToArray()).Reverse();
        }

        [Fact]
        public void Interpret_WhenFeedingGeneratedCodes_InterpretsCorrectly()
        {
            var encoder = new Code128Encoder(new DefaultMapping(), EncodedCode128Factory.Default,
                Encoding.GetEncoding(1252));
            var success = true;

            var content = TestCaseGenerator.GeneratePermutations(6);
            var sw = Stopwatch.StartNew();
            var slice = content.Count / 8;
            Parallel.For(0, 8, i =>
            {
                foreach (var str in content.Skip(i * slice).Take(slice))
                {
                    try
                    {
                        //_output.WriteLine($"Testing {str}");
                        var generated = encoder.Encode(str);
                        var interpreted = _interpreter.Interpret(generated.ToPatternList());
                        Assert.Equal(str, interpreted);
                    }
                    catch (Exception e)
                    {
                        _output.WriteLine($"FAILED: {str}: {e}");
                        success = false;
                    }
                }
            });
            _output.WriteLine(
                $"{content.Count} tests run in {sw.Elapsed}. {sw.Elapsed.TotalMilliseconds / content.Count}ms per test");

            Assert.True(success, "One or more cases failed.");
        }
    }
}