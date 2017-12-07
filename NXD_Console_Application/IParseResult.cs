using System.Collections.Generic;

namespace NXD_Console_Application
{
    public interface IParseResult<T>
    {
        bool Success { get; }

        string Input { get; }

        IEnumerable<T> Output { get; }
    }
}