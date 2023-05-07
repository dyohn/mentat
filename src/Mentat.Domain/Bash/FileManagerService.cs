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
    /// <summary>
    /// A convenient class for saving a script. Implements the IFileManagerService interface.
    /// </summary>
    internal class FileManagerService : IFileManagerService
    {
        /// <summary>
        /// Function to save a script with a given name and array of bytes.
        /// </summary>
        /// <param name="script">Holds an array of bytes containing a script.</param>
        /// <param name="scriptName">Holds the name of the script.</param>
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
