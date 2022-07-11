using System;
using System.Collections.Generic;
using System.Linq;
using Mentat.Domain.Interfaces;

namespace Mentat.Domain.Bash
{
    public class BashTestConfig : IBashTestConfig
    {
        public int NumberOfTests => TestFileNames.Count();

        public string Language { get; private set; }

        public IEnumerable<string> TestFileNames { get; private set; }

        public string SampleExecutableName { get; private set; }

        public float TimeoutInSeconds { get; private set; }

        public string DiffCommand { get; private set; }

        public BashTestConfig()
        {
            Language = string.Empty;
            TestFileNames = new List<string>();
            SampleExecutableName = string.Empty;
            TimeoutInSeconds = 1;
            DiffCommand = Constants.PauseCmd;
        }

        public BashTestConfig(string language, IEnumerable<string> testFileNames, string sampleExecutableName, float timeoutInSeconds, string diffCommand)
        {
            Language = language;
            TestFileNames = testFileNames;
            SampleExecutableName = sampleExecutableName;
            TimeoutInSeconds = timeoutInSeconds;
            DiffCommand = diffCommand;
        }

        public BashTestConfig(string language, IEnumerable<string> testFileNames)
        {
            Language = language;
            TestFileNames = testFileNames;
        }

        public BashTestConfig(string language, IEnumerable<string> testFileNames, string sampleExecutableName) : this(language, testFileNames)
        {
            SampleExecutableName = sampleExecutableName;
        }
    }
}
