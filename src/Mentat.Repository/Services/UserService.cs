using Mentat.Repository.Models;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<Users> _Users;

        public UserService(IUserDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _Users =database.GetCollection<Users>(settings.UserCollectionName);
        }

        public List<Users> GetAllUsers()
        {
            return _Users.Find(user => true).ToList();
        }

        public List<Users> GetFilteredUserList(List<string> Role)
        {
            if( Role == null)
            {
                return GetAllUsers();
            }

            return _Users.Find(u =>Role.Contains(u.Role)).ToList();
        }

        public Users GetUser( string id)
        {
            return _Users.Find(user => user.Id.Equals(id)).SingleOrDefault();
        }

        public void RemoveUser (string id)
        {
            _Users.DeleteOne(user => user.Id.Equals(id));
        }

        public void saveCard( string id, Users user)
        {
            if( id != null)
            {
                user.Id = id;
                if( GetUser(id) == null)
                {
                    throw new Exception("Card ID invalid.");
                }
            }
            else
            {
                user.Id = Guid.NewGuid().ToString();
            }
            _Users.ReplaceOne( u => u.Id.Equals(user.Id), user, new ReplaceOptions{ IsUpsert = true});
        }
    }
}

