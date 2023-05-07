using System;
namespace Mentat.Repository.Options
{
    /// <summary>
    /// Tightly coupled class for containing Card Database connection options.
    /// Accessed in CardServices via IOptionsMonitor pattern.
    /// </summary>
    public class CardDatabaseOptions
    {
        public string CardCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

