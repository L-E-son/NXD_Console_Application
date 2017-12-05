using NUnit.Framework;
using NXD_Console_Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NXD_Console_ApplicationTests
{
    [TestFixture()]
    public class ProgramTests
    {
        /// <summary>
        /// All of these test cases can be improved using TestCaseSource.
        /// For the sake of brevity, I did one for each.
        /// </summary>

        private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        //TODO:  improve this case by having two files - one with original input and one with correct input.
        [Test]
        public void ParseAllLinesTest()
        {
            var assemblyLocation = Path.GetDirectoryName(CurrentAssembly.Location);
            var testFileLocation = Path.Combine(assemblyLocation, "SampleInput.txt");
            var resourceUri = new Uri(testFileLocation);

            var results = Program.ParseAllUIntLines(resourceUri);
            Assert.IsTrue(results.Count() == 14);
        }

        [Test]
        public void ParseAllUIntLinesTest_NullUri()
        {
            Assert.Throws<ArgumentNullException>(
                () => Program.ParseAllUIntLines(null));
        }

        [Test]
        public void ParseAllUIntLinesTest_RelativeUri()
        {
            Assert.Throws<ArgumentException>(
                () => Program.ParseAllUIntLines(new Uri("fakeFile.txt", UriKind.Relative)));
        }

        //TODO:  also find a better way to do this.
        [Test]
        public void FindPrimesTest_2()
        {
            var result = Program.FindPrimes(2).Single();
            Assert.IsTrue(result == 2);
        }

        //TODO:  improve this testcase by using TestCaseSource to compare collections
        [Test]
        public void FindFactorsTest_2()
        {
            var testData = new(uint, uint)[] { (1, 2), (2, 1) };
            var result = Program.FindFactors(2).ToArray();

            CollectionAssert.AreEquivalent(testData, result);
        }
    }
}