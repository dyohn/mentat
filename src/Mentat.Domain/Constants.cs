// Constants.cs
// dyohn
// 6/5/2022

using System;

namespace Mentat.Domain
{
    public struct Constants
    {
        #region ProgrammingLanguages
        /// <summary>
        /// Bash programming language string.
        /// </summary>
        public static readonly string Bash = "Bash";

        /// <summary>
        /// Python programming language string.
        /// </summary>
        public static readonly string Python = "Python";
        #endregion

        #region BashCommands
        /// <summary>
        /// The standard diff command for testing.
        /// </summary>
        public static readonly string DiffCmd = "diff -w -b -B -s -a ";

        /// <summary>
        /// A convenient pause command during scripting.
        /// </summary>
        public static readonly string PauseCmd = "read -n 1 -r -s -p 'Press enter to continue...'";
        #endregion
    }
}
