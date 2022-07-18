using System;

namespace Mentat.LoginRegister.Settings
{
    public class MongoDbConfig
    {
        //public string Name { get; init; } = null!;
        //public string Host { get; init; } = null!;
        //public int Port { get; init; }

        public string CollectionName { get; init; } = null!;
        public string ConnectionString { get; init; } = null!;
        public string DatabaseName { get; init; } = null!;
        //public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
