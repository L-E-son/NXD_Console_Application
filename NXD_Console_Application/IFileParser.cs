using System.Collections.Generic;
using System.IO;

namespace NXD_Console_Application
{
    public interface IFileParser<T>
    {
        ParseResult<T> ParseLine(string input);

        IEnumerable<ParseResult<T>> ParseAllLines(FileInfo targetFile);
    }
}
