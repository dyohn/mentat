namespace Mentat.Repository.Models
{
    public interface ICardDatabaseSettings
    {
        string CardCollectionName { get; set;  }
        
        string DatabaseName { get; set; }

        string ConnectionString { get; set; }
    }
}
