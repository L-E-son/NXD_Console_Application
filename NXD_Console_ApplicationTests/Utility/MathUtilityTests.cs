using NUnit.Framework;
using System.Linq;

namespace NXD_Console_Application.Utility.Tests
{
    [TestFixture()]
    public class MathUtilityTests
    {
        [Test, TestCaseSource(nameof(PrimeFactorCases))]
        public void GetPrimeFactorsTest(params uint[] data)
        {
            var input = data[0];
            var output = data.Skip(1).ToArray();

            var result = MathUtility.GetPrimeFactors(input);

            CollectionAssert.AreEqual(result, output);
        }

        /// <summary>
        /// Format:
        /// array[0] is the input
        /// The remainer of the array are expected outputs (sorted)
        /// </summary>
        private static readonly object[] PrimeFactorCases =
        {
            new uint[] { 315, 3, 3, 5, 7 },
            new uint[] { 4, 2, 2 },
            new uint[] { 6, 2, 3 },
            new uint[] { 8, 2, 2, 2 },
            new uint[] { 22, 2, 11 }
        };
    }
}