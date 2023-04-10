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
    public class BashTestDriver : IBashTestDriver
    {
        public string Language { get; private set; }

        public string Script { get; private set; }

        private ITestConfig _config;

        private readonly IFileManagerService _fileManagerService;

        public BashTestDriver(IFileManagerService managerService)
        {
            _fileManagerService = managerService;
        }

        public FileInfo ExampleBuild()
        {
            // _config.Language is used to determine the compiler
            // _config.TestFileNames has all of the names of the test to run. Each will be ran and output appended to a file. Ex. "myTest.sh >> out.txt"
            // _config.SampleExecutableName contains the name of the mentor/professors version of the assignment. Used as a baseline.
            // _config.DiffCommand variable that will contain "diff" in our case. This is an interface variable that can be set to other languages version. Were only handling bash so we use diff.
            // _config.TimeoutInSeconds how long to wait for an assignment to finish. We don't want our machine to be stuck for an hour because of bad code. Ex: "timeout 1 ./myTest.sh  >> out.txt"
            // _config.ColorText is the boolean to determine if we want to change the color of the bash scripts text.
            // _config.HighlightText is the boolean for highlighting text.
            // _config.ApplyTextModifers is the boolean for applying modifiers such as bold, italics, etc.


            // config has all the details and shouldn't be null
            bool validationFailed = false;
            if (_config is null)
            {
                throw new NullReferenceException();
            }
            var scriptBuilder = new StringBuilder();
            BashColor color;
            if (_config.ColorText)
            {
                color = new BashColor(BashColor.TextColor.Green, null, null);
            } else
            {
                color = new BashColor(null, null, null);
            }
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
            foreach (var testFileName in _config.TestFileNames)
            {
                scriptBuilder.Append("echo \"" + GetColorString(color, "Running the test: " + testFileName ) + "\"\n" +
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
                "read -n 1 -r -s -p \'Press enter to continue...\'\n\t"+
                "echo\n\t");
            if (_config.Language == "c")
            {
                scriptBuilder.Append("gcc -std=c99 -o ${EXECUTABLE} ${SOURCECODE}\n\t");
            } else if(_config.Language == "c++")
            {
                scriptBuilder.Append("g++ -c -std=c++11 ${SOURCECODE}\n\t" +
                    "g++ -o ${EXECUTABLE} -std=c++11 ${STUDENT}.o\n\t");
            } else
            {
                validationFailed = true;
            }
            if (!validationFailed)
            {
                scriptBuilder.Append("mv ${EXECUTABLE} ${TEST_DIR}/assignment\n\t" +
                    "cd ${TEST_DIR}\n\t");

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

                var script = scriptBuilder.ToString();
                if (!string.IsNullOrEmpty(script))
                {
                    _fileManagerService.SaveScript(Encoding.UTF8.GetBytes(script), "Mentat.sh");
                }
            }

            //make sure the file exists and return it or instead return dummy file
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

        public FileInfo Build()
        {
            // config has all the details and shouldn't be null
            if (_config is null)
            {
                throw new NullReferenceException();
            }
            // build a script with the configuration stuff
            // this is just a mock
            var scriptBuilder = new StringBuilder();

            foreach (var testFileName in _config.TestFileNames)
            {
                scriptBuilder.Append(testFileName).AppendLine();
            }

            // TODO: This might be wrong. Need to ask Derek if SampleExecutableName is the name of the output file
            //       OR if SampleExecutableName is the name of the mentors version of the assignment being tested
            //       that will be used in the diff.
            var fileExtension = _config.Language == "python" ? "py" : "cpp";

            var fileName = $"{_config.SampleExecutableName}.{fileExtension}";

            // save the script or whatever
            var script = scriptBuilder.ToString();

            if (!string.IsNullOrEmpty(script))
            {
                _fileManagerService.SaveScript(Encoding.UTF8.GetBytes(script), fileName);
            }

            //make sure the file exists and return it or instead return dummy file
            if (File.Exists(fileName))
            {
                return new FileInfo(fileName);
            }
            else
            {
                string dummyFile = @"file.txt";
                var dummyBuilder = new StringBuilder();
                dummyBuilder.Append(dummyFile).AppendLine();

                using (FileStream fs = File.Create(dummyFile))
                {
                    _fileManagerService.SaveScript(Encoding.UTF8.GetBytes("Hello World"), dummyFile);
                }

                return new FileInfo(dummyFile);
            }
        }

        public void Configure(ITestConfig config)
        {
            _config = config;
        }

        public String GetColorString(BashColor color, String msg)
        {
            return color.Color + msg + BashColor.End;
        }
    }
}
