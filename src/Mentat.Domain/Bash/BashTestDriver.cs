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
            if (_config is null)
            {
                throw new NullReferenceException();
            }
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append("#!/usr/bin/env bash\n\nCURRENT_DIR=$( cd -- \"$( dirname -- \"${BASH_SOURCE[0]}\" )\" &> /dev/null && pwd )\n\n");
            scriptBuilder.Append("MENTOR_DIR=${CURRENT_DIR} + \"/mentor\"\nTEST_DIR=${CURRENT_DIR} + \"/tests\"\nREPORT_DIR=${CURRENT_DIR} + \"/reports\"\n");
            scriptBuilder.Append("SUBMIT=${CURRENT_DIR} + \"/submit\"\nBUILD=${CURRENT_DIR} + \"/build\"\nPROCESSED=${CURRENT_DIR} + \"/processed\"\nHEADER=${CURRENT_DIR} + \"/header\"\n");
            scriptBuilder.Append("#Test the mentors build\ncd ${TEST_DIR}\ncp mentor.x ${MENTOR_DIR}/*\n");
            foreach (var testFileName in _config.TestFileNames)
            {
                scriptBuilder.Append(testFileName + " > ./${MENTOR_DIR}/mentor.out\n");
            }
            scriptBuilder.Append("cd ${SUBMIT_DIR}\nfor SOURCECODE in *; do\n\tSTUDENT=\'echo \"${SOURCECODE}\" | cut -d\'.\' -fl\'\n\techo\n\techo -e \"Grading ${STUDENT}");
            scriptBuilder.Append("EXECUTABLE=${STUDENT}.x\nREPORT=${STUDENT}.x\ncp ${HEADER} ${REPORT_DIR}/${REPORT}");

            var script = scriptBuilder.ToString();
            if (!string.IsNullOrEmpty(script))
            {
                _fileManagerService.SaveScript(Encoding.UTF8.GetBytes(script), "SomeFileName.sh");
            }

            //make sure the file exists and return it or instead return dummy file
            if (File.Exists("SomeFileName.sh"))
            {
                return new FileInfo("SomeFileName.sh");
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
