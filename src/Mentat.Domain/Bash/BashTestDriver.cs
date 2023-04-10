using System;
using Mentat.Domain.Interfaces;
using System.Text;
using System.IO;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ZstdSharp.Unsafe;
using System.Security;

namespace Mentat.Domain.Bash
{
    /// <summary>
    /// Class for generating a Bash Script Driver for mentors to grade assignments.
    /// </summary>
    public class BashTestDriver : IBashTestDriver
    {
        /// <value>Holds the string containing the language of the assignment.</value>
        public string Language { get; private set; }
        /// <value>Holds the string containing the script.</value>
        public string Script { get; private set; }
        /// <value>Holds the configurations to be used during Bash script generation.</value>
        private ITestConfig _config;
        /// <value>Holds the service used to save a script.</value>
        private readonly IFileManagerService _fileManagerService;

        /// <summary>
        /// Constructor of type BashTestDriver. Initializes a FileManagerService.
        /// </summary>
        /// <param name="managerService">FileManagerService used to save scripts.</param>
        public BashTestDriver(IFileManagerService managerService)
        {
            _fileManagerService = managerService;
        }

        /// <summary>
        /// Generates a Bash Test Driver using the user sumbitted info.
        /// </summary>
        public FileInfo Build()
        {
            /// <value>Flag for if invalid information is found.</value>
            bool validationFailed = false;
            if (_config is null)
            {
                throw new NullReferenceException();
            }
            /// <value>Holds the generated script.</value>
            var scriptBuilder = new StringBuilder();
            /// <value>Holds color settings.</value>
            BashColor color;
            if (_config.ColorText)
            {
                color = new BashColor(BashColor.TextColor.Green, null, null);
            }
            else
            {
                color = new BashColor(null, null, null);
            }
            // Begin generating script.
            scriptBuilder.Append("#!/usr/bin/env bash\n\n" +
                "CURRENT_DIR=$( cd -- \"$( dirname -- \"${BASH_SOURCE[0]}\" )\" &> /dev/null && pwd )\n\n" +
                "MENTOR_DIR=${CURRENT_DIR} + /mentor\n" +
                "TEST_DIR=${CURRENT_DIR} + /tests\n" +
                "REPORT_DIR=${CURRENT_DIR} + /reports\n" +
                "DIFF_DIR=${REPORT_DIR}/diffs\n" +
                "SUBMIT_DIR=${CURRENT_DIR} + /submit\n" +
                "BUILD_DIR=${CURRENT_DIR} + /build\n" +
                "PROCESSED_DIR=${CURRENT_DIR} + /processed\n" +
                "HEADER=${CURRENT_DIR} + /header\n" +
                "SPACER=\"--------------------------------------------------------------------------\n" +
                    "-************************************************************************-\n" +
                    "--------------------------------------------------------------------------\"\n\n" +
                "#Test the mentors build\n" +
                "echo \"" + GetColorString(color, "Beginning running test against mentors sample: " + _config.SampleExecutableName) + "\"\n" +
                "cd ${TEST_DIR}\n" +
                "MENTOR =`echo \"" + _config.SampleExecutableName + "\" | cut -d'.' -f1`\n" +
                "cp " + _config.SampleExecutableName + " assignment\n");
            // Loop over and insert test for each test file name for the mentor.
            foreach (var testFileName in _config.TestFileNames)
            {
                scriptBuilder.Append("echo \"" + GetColorString(color, "Running the test: " + testFileName) + "\"\n" +
                    testFileName + " >> ./${MENTOR_DIR}/${MENTOR}.txt\n" +
                    "echo ${SPACER} >> ./${MENTOR_DIR}/${MENTOR}.txt\n");
            }
            scriptBuilder.Append("mv assignment ${MENTOR_DIR}/" + _config.SampleExecutableName + "\n\n" +
                "echo \"" + GetColorString(color, "Mentor testing complete.") + "\"\n" +
                "echo \"" + GetColorString(color, "Beginning student testing.") + "\"\n" +
                "# Compile and move into test dir, setting up reports along the way.\n" +
                "cd ${SUBMIT_DIR}\n" +
                "for SOURCECODE in *; do\n\t" +
                "STUDENT=\'echo \"${SOURCECODE}\" | cut -d\'.\' -f1\'\n\t" +
                "echo\n\t" +
                "echo \"" + GetColorString(color, "Compiling ${STUDENT}'s assignmnet...") + "\"\n\t" +
                "EXECUTABLE=${STUDENT}.x\n\t" +
                "REPORT=${STUDENT}.txt\n\t" +
                "cp ${HEADER} ${REPORT_DIR}/${REPORT}\n\t" +
                "read -n 1 -r -s -p \'Press enter to continue...\'\n\t" +
                "echo\n\t");
            // Configure compiler settings.
            if (_config.Language == "c")
            {
                scriptBuilder.Append("gcc -std=c99 -o ${EXECUTABLE} ${SOURCECODE}\n\t");
            }
            else if (_config.Language == "c++")
            {
                scriptBuilder.Append("g++ -c -std=c++11 ${SOURCECODE}\n\t" +
                    "g++ -o ${EXECUTABLE} -std=c++11 ${STUDENT}.o\n\t");
            }
            else
            {
                validationFailed = true;
            }
            if (!validationFailed)
            {
                scriptBuilder.Append("mv ${EXECUTABLE} ${TEST_DIR}/assignment\n\t" +
                    "cd ${TEST_DIR}\n\t");
                // Loop and insert test for student code for each test file name.
                foreach (var testFileName in _config.TestFileNames)
                {
                    scriptBuilder.Append("echo \"" + GetColorString(color, "Running the test: " + testFileName) + "\"\n\t" +
                        "timeout " + _config.TimeoutInSeconds.ToString() + " " + testFileName + " >> ${REPORT_DIR}/${REPORT}\n\t" +
                        "echo ${SPACER} >> ${REPORT_DIR}/${REPORT}\n\t");
                }
                scriptBuilder.Append("echo \"" + GetColorString(color, "Student testing complete.") + "\"\n\t" +
                    "echo \"" + GetColorString(color, "Beginning clean up.") + "\"\n\t" +
                    "\n\t# Clean up\n\t" +
                    "echo \"" + GetColorString(color, "Moving assignment to ${BUILD_DIR}/${EXECUTABLE}...") + "\"\n\t" +
                    "mv assignment ${BUILD_DIR}/${EXECUTABLE}\n\t" +
                    "echo \"" + GetColorString(color, "Moving #{SUBMIT_DIR}/${SOURCECODE} ${PROCESSED_DIR}/${SOURCECODE}...") + "\"\n\t" +
                    "mv ${SUBMIT_DIR}/${SOURCECODE} ${PROCESSED_DIR}/${SOURCECODE}\n\t" +
                    _config.DiffCommand + " -u ${MENTOR_DIR}/${MENTOR}.txt ${REPORT_DIR}/${REPORT} > ${DIFF_DIR}/${STUDENT}_diff.txt\n\t" +
                    "cd ${SUBMIT_DIR}\n\t" +
                    "echo\n\t" +
                    "echo \"" + GetColorString(color, "Completing grading ${STUDENT}...") + "\"\n\t" +
                    "echo\n" +
                    "done");

                // Save the resulting script.
                var script = scriptBuilder.ToString();
                if (!string.IsNullOrEmpty(script))
                {
                    _fileManagerService.SaveScript(Encoding.UTF8.GetBytes(script), "Mentat.sh");
                }
            }

            // Make sure the file exists and return it or instead return dummy file.
            if (File.Exists("Mentat.sh"))
            {
                return new FileInfo("Mentat.sh");
            }
            else
            {
                string dummyFile = @"file.txt";
                var dummyBuilder = new StringBuilder();
                dummyBuilder.Append(dummyFile).AppendLine();

                using (FileStream fs = File.Create(dummyFile))
                {
                    _fileManagerService.SaveScript(Encoding.UTF8.GetBytes("Failed to create bash script! Please Try again!"), dummyFile);
                }

                return new FileInfo(dummyFile);
            }
        }

        /// <summary>
        /// Sets the configuration settings of the BuildTestDriver object.
        /// </summary>
        /// <param name="config">Contains the configuration settings.</param>
        public void Configure(ITestConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Decorates a string with a Bash color.
        /// </summary>
        /// <param name="color">Holds the color to be used.</param>
        /// <param name="msg">Holds the message to be decorated.</param>
        public String GetColorString(BashColor color, String msg)
        {
            return color.Color + msg + BashColor.End;
        }
    }
}
