// ITestConfig.cs
// dyohn
// 6/5/2022

using System;
using System.Collections.Generic;

namespace Mentat.Domain.Interfaces
{
    /// <summary>
    /// The minimum configuration information necessary to build a test driver.
    /// </summary>
    public interface ITestConfig
    {
        /// <summary>
        /// The programming language of the solution domain.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// An enumeration of the names of the test files to be used.
        /// </summary>
        public IEnumerable<string> TestFileNames { get; }

        /// <summary>
        /// The name of the sample executable to be used.
        /// </summary>
        public string SampleExecutableName { get; }

        /// <summary>
        /// How long to wait before terminating student code during each test.
        /// </summary>
        public float TimeoutInSeconds { get; }

        /// <summary>
        /// The diff command to be used for comparing student output to the
        /// output from the sample executable.
        /// </summary>
        public string DiffCommand { get; }
    }
}
