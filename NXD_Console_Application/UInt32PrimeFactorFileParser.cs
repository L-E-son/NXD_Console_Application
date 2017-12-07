using NXD_Console_Application.Utility;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NXD_Console_Application
{
    using UInt32ParseResult = ParseResult<uint>;

    public class UInt32PrimeFactorFileParser : IFileParser<uint>
    {
        /// <summary>
        /// Parses a single result as a UInt32
        /// </summary>
        /// <param name="input">A line to read in.</param>
        /// <returns>The result of the parse.</returns>
        public UInt32ParseResult ParseLine(string input)
        {
            if (uint.TryParse(input, NumberStyles.Integer, CultureInfo.CurrentCulture, out uint result))
                return new UInt32ParseResult(input: input, output: MathUtility.GetPrimeFactors(result));

            else
                return new UInt32ParseResult(input: input);
        }

        /// <summary>
        /// Parses all lines in a given file.
        /// </summary>
        /// <param name="targetFile">The target file to read in.</param>
        /// <returns>An IEnumerable of all parse results.</returns>
        public IEnumerable<UInt32ParseResult> ParseAllLines(FileInfo targetFile)
        {
            foreach (var line in File.ReadLines(targetFile.FullName))
            {
                yield return ParseLine(line);
            }
        }
    }
}
