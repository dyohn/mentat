using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentat.Domain.Interfaces
{
    public interface IFileManagerService
    {
        public void SaveScript(string script, string scriptName);
    }
}
