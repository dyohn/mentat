using System;
using Mentat.Domain.Interfaces;
using System.Text;
using System.IO;
using MongoDB.Bson.Serialization.Serializers;

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
    }
}
