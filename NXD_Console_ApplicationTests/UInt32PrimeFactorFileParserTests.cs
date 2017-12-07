using NUnit.Framework;
using NXD_Console_Application;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXD_Console_Application.Tests
{
    [TestFixture()]
    public class UInt32PrimeFactorFileParserTests
    {
        [TestCase("", false)]
        [TestCase("this is a sentence", false)]
        [TestCase(" ", false)]
        [TestCase("99,999", false)]
        [TestCase("99.999", false)]
        public void ParseLineTest(string input, bool resultIsSuccess)
        {
            var parser = new UInt32PrimeFactorFileParser();

            var result = parser.ParseLine(input);

            Assert.AreEqual(resultIsSuccess, result.Success);
        }

        [TestCase(uint.MinValue, true)]
        [TestCase(uint.MaxValue, true)]
        [TestCase(4294967296, false)] //uint.MaxValue + 1
        [TestCase(-1, false)]
        public void ParseLineTest_Integers(long input, bool resultIsSuccess)
        {
            ParseLineTest(input.ToString(), resultIsSuccess);
        }
    }
}