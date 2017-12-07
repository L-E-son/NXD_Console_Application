using NXD_Console_Application.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NXD_Console_Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var targetFile = FileUtility.GetFilenameFromArguments(args);

                if (targetFile == null)
                {
                    Console.Out.WriteLine("Please specify a file.");
                    return;
                }

                var parseResults = new UInt32PrimeFactorFileParser().ParseAllLines(targetFile);

                PrintOutput(parseResults);
            }
            catch (Exception e)
            {
                if (!string.IsNullOrWhiteSpace(e.Message))
                    Console.WriteLine(e.Message);

                //Sometimes exceptions can sometimes have a null message. Print stack track instead.
                else
                    Console.Write(e.StackTrace);
            }
            finally
            {
                Console.WriteLine("====================");
                Console.WriteLine("Finished.");
                Console.ReadKey();
            }
        }

        public static void PrintOutput(IEnumerable<ParseResult<uint>> inputs)
        {
            if (inputs?.Any() != true)
                return;

            foreach (var input in inputs)
            {
                if (input.Success)
                {
                    Console.Out.WriteLine($"Input: {input.Input}");
                    Console.Out.WriteLine($"Prime factors: {ConstructFactorOutput(input.Output.ToArray())}");
                }

                else
                    Console.Out.WriteLine($"Invalid input: {input.Input}");

                Console.Out.WriteLine("-----");
            }
        }

        private static readonly string Delimiter = ", ";
        private static string ConstructFactorOutput(uint[] items)
        {
            var sb = new StringBuilder();

            for (uint i = 0; i < items.Length; i++)
            {
                sb.Append(items[i]);

                if (i < items.Length - 1)
                    sb.Append(Delimiter);
            }

            return sb.ToString();
        }
    }
}
