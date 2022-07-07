using System;
using Mentat.Domain.Interfaces;
using System.IO;

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
            if(_config is null)
            {
                throw new Exception("Config is null");
            }

            // build a script with the configuration stuff
            var script = "";

            // assume the file can be bash or python
            // its python

            var fileExtension = "py";

            var fileName = $"{_config.SampleExecutableName}.{fileExtension}";

            // save the script or whatever

            _fileManagerService.SaveScript(script, fileName);

            // now done
        }

        public void Configure(ITestConfig config)
        {
            _config = config;
        }
    }
}
