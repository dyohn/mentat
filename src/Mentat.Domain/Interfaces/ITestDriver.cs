// ITestDriver.cs
// dyohn
// 6/5/2022

using System;
using System.IO;

namespace Mentat.Domain.Interfaces
{
    /// <summary>
    /// Types which implement this interface represent a test driver for grading
    /// of software submissions. These types may have many properties, but, at
    /// minimum, a test script can be configured, built, and provided.
    /// </summary>
    public interface ITestDriver
    {
        /// <summary>
        /// The language used by the Script.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// The configured and built test script.
        /// </summary>
        public string Script { get; }

        /// <summary>
        /// Configure the parameters for the ITestDriver implementation.
        /// </summary>
        /// <param name="config">
        /// An ITestConfig object with the requested build parameters.
        /// </param>
        public void Configure(ITestConfig config);

        /// <summary>
        /// Build and set the grading Script property.
        /// </summary>
        public FileInfo Build();
    }
}
