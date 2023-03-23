namespace Mentat.Repository.Models
{
    public interface IUserDatabaseSettings
    {
        string UserCollectionName { get; set; }

        string DatabaseName { get; set; }

        string ConnectionString { get ; set;} 
    }
}
