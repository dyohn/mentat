namespace Mentat.Repository.Models
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://cdb:eN9qAv7forAKSnEt@flashcardapp.4yggo3z.mongodb.net/?retryWrites=true&w=majority";

        public string DatabaseName { get; set; } = "fcApp";

        public string UserCollectionName { get; set;} = "UserAccounts";
    }
}
