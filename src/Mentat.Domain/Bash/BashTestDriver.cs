using System;
using Mentat.Domain.Interfaces;
using System.Text;

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

        public void Build()
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
        }

        public void Configure(ITestConfig config)
        {
            _config = config;
        }
    }
}
