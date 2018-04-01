using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Code128.Interpreter
{
    public class TestCaseGenerator
    {
        public TestCaseGenerator(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        private static void Generate(string prefix, int lengthLeft, char[] choices, List<string> list)
        {
            list.Add(prefix);
            if (lengthLeft > 0)
            {
                foreach (var c in choices)
                {
                    var newPre = prefix + c;
                    Generate(newPre, lengthLeft - 1, choices, list);
                }
            }
        }

        public static List<string> GeneratePermutations(int maxLength)
        {
            var choices = new[] {'A', '0', '\t', 'a', '€', 'ö'};
            var list = new List<string>();
            Generate(string.Empty, maxLength, choices, list);
            return list;
        }

        [Fact]
        public void GenerateCases()
        {
            var list = GeneratePermutations(6);
            _output.WriteLine($"{list.Count} cases emitted");
        }
    }
}