namespace Mentat.Repository.Models
{
    public interface IUserDatabaseSettings
    {
        string UserCollectionName { get; }

        string DatabaseName { get;}

        string ConnectionString { get;} 
    }
}
