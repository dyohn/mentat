using System.IO;
using Mentat.Domain.Interfaces;

namespace Mentat.UI.Services
{
    public class FileManagerService : IFileManagerService
    {
        public void SaveScript(byte[] script, string scriptName)
        {
            File.WriteAllBytes(scriptName, script);
        }
    }
}
