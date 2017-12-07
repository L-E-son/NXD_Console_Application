using System.Collections.Generic;
using System.Linq;

namespace NXD_Console_Application
{
    public class ParseResult<T> : IParseResult<T>
    {
        public bool Success => Output?.Any() == true;

        public string Input { get; }

        public IEnumerable<T> Output { get; }

        public ParseResult(string input, IEnumerable<T> output = null)
        {
            Input = input;
            Output = output;
        }
    }
}
