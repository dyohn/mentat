using System;

namespace Mentat.Repository.Options
{
    /// <summary>
    /// Class for Identity Database Options. Can be accessed with dependency injection
    /// using an IOptionsMonitor<IdentityDatabaseOptions> parameter in a constructor.
    /// Not currently used.
    /// </summary>
    public class IdentityDatabaseOptions
    {
        public string IdentityCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
