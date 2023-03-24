using Mentat.Repository.Models;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<Users> _users;

        public UserService(IUserDatabaseSettings settings, IMongoClient mongoClient)
        {        
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            // Check if the collection exists, create it if it doesn't
            var collectionExists = database.ListCollectionNames().ToList().Any(collectionName => collectionName == settings.UserCollectionName);
            if (!collectionExists)
            {
                database.CreateCollection(settings.UserCollectionName);
            }
            _users = database.GetCollection<Users>(settings.UserCollectionName);
        }

        public List<Users> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public List<Users> GetFilteredUserList(List<int> Role)
        {
            if( Role == null)
            {
                return GetAllUsers();
            }

            return _users.Find(u =>Role.Contains(u.Role)).ToList();
        }

                public Users GetFirstName( string firstName)
        {
            return _users.Find(user => user.FirstName.Equals(firstName)).SingleOrDefault();
        }

                public Users GetLastName( string lastName)
        {
            return _users.Find(user => user.LastName.Equals(lastName)).SingleOrDefault();
        }

        public Users GetUser( string id)
        {
            return _users.Find(user => user.Id.Equals(id)).SingleOrDefault();
        }

        public void RemoveUser (string id)
        {
            _users.DeleteOne(user => user.Id.Equals(id));
        }

        public void saveUser( string id, Users user)
        {
            if( id != null)
            {
                user.Id = id;
                if( GetUser(id) == null)
                {
                    throw new Exception("User ID invalid.");
                }
            }
            else
            {
                user.Id = Guid.NewGuid().ToString();
            }
            _users.ReplaceOne( u => u.Id.Equals(user.Id), user, new ReplaceOptions{ IsUpsert = true});
        }
    }
}

