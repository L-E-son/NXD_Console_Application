using NUnit.Framework;
using System;
using System.Reflection;

namespace NXD_Console_Application.Utility.Tests
{
    [TestFixture()]
    public class FileUtilityTests
    {
        private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        [TestCase(@"C:", false)]
        [TestCase(@"C:\", false)]
        [TestCase(@"C:\\", false)]
        [TestCase(@"//", false)]
        [TestCase(@"\\", false)]
        [TestCase("./", false)]
        [TestCase("file://test.txt", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("http://google.com/", false)]
        public void IsReadableFileTest(string input, bool expectedOutput)
        {
            Assert.AreEqual(expectedOutput, FileUtility.IsReadableFile(input));
        }

        [Test]
        public void GetFilenameFromArguments_Null()
        {
            Assert.Throws<ArgumentNullException>(
                () => FileUtility.GetFilenameFromArguments(null));
        }

        [Test, TestCaseSource(nameof(ExcessArgumentsCases))]
        public void GetFilenameFromArguments_ExcessArguments(string[] input)
        {
            Assert.Throws<ArgumentException>(
                () => FileUtility.GetFilenameFromArguments(input));
        }

        private static readonly object[] ExcessArgumentsCases =
        {
            new string[] { "one", " " },
            new string[] { "one", "two" },
            new string[] { "one", "two", "three" },
            new string[] { "one", "two", "three", " " },
        };

        [Test, TestCaseSource(nameof(MultipleFilesCases))]
        public void GetFilenameFromArguments_MultipleFiles(string[] input)
        {
            Assert.Throws<ArgumentException>(
                () => FileUtility.GetFilenameFromArguments(input));
        }

        private static readonly object[] MultipleFilesCases =
        {
            new string[] {"TestFile1.txt", "TestFile2.txt"}
        };
    }
}