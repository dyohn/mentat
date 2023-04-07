using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mentat.Domain.Interfaces;

namespace Mentat.Domain.Bash
{
    internal class FileManagerService : IFileManagerService
    {
        public void SaveScript(byte[] script, string scriptName)
        {
            try
            {
                using (var fs = new FileStream(scriptName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(script, 0, script.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
        }
    }
}
