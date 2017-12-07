using System;
using System.IO;
using System.Linq;

namespace NXD_Console_Application.Utility
{
    public static class FileUtility
    {
        /// <summary>
        /// Verifies the string in question is a file (not a directory) and can safely be opened.
        /// </summary>
        /// <param name="file">Full path of file.</param>
        /// <returns></returns>
        public static bool IsReadableFile(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);

                return fileInfo.Exists && !fileInfo.Attributes.HasFlag(FileAttributes.Directory);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Will return a new instance of FileInfo if the input contains a valid file path.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>If successful, a new instance of FileInfo. Otherwise, null.</returns>
        public static FileInfo GetFilenameFromArguments(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("Arguments cannot be null");

            if (args.Length != 1)
                throw new ArgumentException($"Expected one argument, but received {args.Length}");

            var url = args.SingleOrDefault(IsReadableFile);

            if (url == null)
                throw new ArgumentException("Did not find a valid file path in the parameter list.");

            return new FileInfo(url);
        }
    }
}
