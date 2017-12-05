using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXD_Console_Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 1)
                Console.Out.WriteLine("Expected 1 argument. Please specify the file path.");

            else
            {
                var fileInfo = new FileInfo(args[0]);

                if (!fileInfo.Exists || !Uri.TryCreate(fileInfo.FullName, UriKind.RelativeOrAbsolute, out Uri validFileLocation) || validFileLocation.LocalPath == null)
                {
                    Console.Out.WriteLine($"File {fileInfo.FullName} does not exist or is not a valid path.");
                    return;
                }

                var allLines = ParseAllUIntLines(validFileLocation);

                foreach (var line in allLines)
                {
                    var primes = FindPrimes(line);
                    var factors = FindFactors(line);

                    var primeFactors = factors.Where(entry => primes.Contains(entry.Item1));

                    var targetMultiple = primeFactors.Last();

                    Console.Out.WriteLine($"Input: {line}: " + targetMultiple);
                }

                Console.WriteLine("====================");
                Console.WriteLine("Finished.");
                Console.ReadKey();
            }
        }

        //Assumption:  values in target file are 0 <= x <= 4,294,967,295 (between uint.MinValue and uint.MaxValue)
        public static List<uint> ParseAllUIntLines(Uri targetLocation)
        {
            if (targetLocation == null)
                throw new ArgumentNullException(nameof(targetLocation) + " cannot be null");

            if (!targetLocation.IsAbsoluteUri)
                throw new ArgumentException("Uri must be absolute.");

            var result = new List<uint>();

            foreach (var line in File.ReadAllLines(targetLocation.LocalPath))
            {
                if (uint.TryParse(line, NumberStyles.Integer, CultureInfo.CurrentCulture, out var validResult))
                    result.Add(validResult);

                else
                    Console.Out.WriteLine($"Invalid input received: {line}");
            }

            return result;
        }

        //https://stackoverflow.com/a/7992984/5038635
        //Sieve of Eratosthenes -- https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
        public static List<uint> FindPrimes(uint targetInt)
        {
            var primes = new List<uint>();

            //Create a list of consecutive integers from 2 (the smallest prime number) through n
            for (uint i = 2; i <= targetInt; i++)
            {
                var valid = true;

                //Enumerate the multiples of p by counting to n from 2p in increments of p
                foreach (var prime in primes)
                {
                    //Find the first number greater than p in the list that is not marked.
                    //If there is no such number, stop.

                    if (prime * prime > i)
                        break;

                    if (i % prime == 0)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                    primes.Add(i);
            }

            return primes;
        }

        //Return a tuple with the factor and its divisor.
        public static IEnumerable<(uint, uint)> FindFactors(uint numberToFactor)
        {
            for (uint i = 1; i <= numberToFactor; i++)
            {
                if (numberToFactor % i == 0)
                    yield return (i, numberToFactor / i);
            }
        }

        //Decided not to use this because it crowds console output too much for larger numbers. Still works, though.
        private static readonly string Delimiter = ", ";
        private static string ConstructFactorOutput((uint, uint) factorPair)
        {
            var sb = new StringBuilder();

            for (uint i = 0; i < factorPair.Item2; i++)
            {
                sb.Append(factorPair.Item1);

                if (i < factorPair.Item2 - 1)
                    sb.Append(Delimiter);
            }

            return sb.ToString();
        }
    }
}
