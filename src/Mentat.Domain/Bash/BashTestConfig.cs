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
        public bool ColorText { get;  }
        public bool HighlightText { get; } 
        public bool ApplyTextModifiers { get; }


        public BashTestConfig()
        {
            Language = string.Empty;
            TestFileNames = new List<string>();
            SampleExecutableName = string.Empty;
            TimeoutInSeconds = 1;
            DiffCommand = Constants.PauseCmd;
            ColorText = false;
            HighlightText = false;
            ApplyTextModifiers = false;
        }

        public BashTestConfig(string language, IEnumerable<string> testFileNames, string sampleExecutableName, float timeoutInSeconds, string diffCommand, bool colorText, bool hightlightText, bool applyTextModifiers)
        {
            Language = language;
            TestFileNames = testFileNames;
            SampleExecutableName = sampleExecutableName;
            TimeoutInSeconds = timeoutInSeconds;
            DiffCommand = diffCommand;
            ColorText = colorText;
            HighlightText = hightlightText;
            ApplyTextModifiers = applyTextModifiers;
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

        public BashTestConfig(string language, IEnumerable<string> testFileNames, bool colorText, bool highlightText, bool applyTextModifiers)
        {
            Language = language;
            TestFileNames = testFileNames;
            ColorText = colorText;
            HighlightText = highlightText;
            ApplyTextModifiers = applyTextModifiers;
        }

        public BashTestConfig(string language, IEnumerable<string> testFileNames, string sampleExecutableName, bool colorText, bool highlightText, bool applyTextModifiers) : this(language, testFileNames, colorText, highlightText, applyTextModifiers)
        {
            SampleExecutableName = sampleExecutableName;
        }
    }
}
