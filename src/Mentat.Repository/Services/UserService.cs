using Mentat.Repository.Models;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IUserDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);

            // Create unique index on usernames in collection:
            var usernameIndex = Builders<User>.IndexKeys.Ascending(u => u.Username);
            var indexModel = new CreateIndexModel<User>(usernameIndex, new CreateIndexOptions { Unique = true });
            _users.Indexes.CreateOne(indexModel);
        }

        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public List<User> GetFilteredUserList(List<int> role)
        {
            if (role == null)
            {
                return GetAllUsers();
            }

            return _users.Find(u => role.Contains(u.Role)).ToList();
        }

        public User GetFirstName(string firstName)
        {
            return _users.Find(user => user.FirstName.Equals(firstName)).SingleOrDefault();
        }

        public User GetLastName(string lastName)
        {
            return _users.Find(user => user.LastName.Equals(lastName)).SingleOrDefault();
        }

        public User GetUser(string id)
        {
            return _users.Find(user => user.Id.Equals(id)).SingleOrDefault();
        }

        public void RemoveUser(string id)
        {
            _users.DeleteOne(user => user.Id.Equals(id));
        }

        public void SaveUser(string id, User user)
        {
            if (id != null)
            {
                user.Id = id;
                if (GetUser(id) == null)
                {
                    throw new Exception("User ID invalid.");
                }
            }
            else
            {
                user.Id = Guid.NewGuid().ToString();
                _users.InsertOne(user);
            }

            _users.ReplaceOne(u => u.Id.Equals(user.Id), user, new ReplaceOptions { IsUpsert = true });
        }

        public async Task AddUser(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
