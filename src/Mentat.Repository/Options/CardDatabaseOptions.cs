using System;
namespace Mentat.Repository.Options
{
    public class CardDatabaseOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string CardCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public CardDatabaseOptions()
        {
        }
    }
}

