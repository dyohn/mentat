using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        List<User> GetFilteredUserList(List<int> role);

        User GetUser( string id);

        User GetFirstName( string id);

        User GetLastName( string id);

        void RemoveUser (string id);

        void SaveUser( string id, User user);
        
        Task AddUser(User user);

    }
}
