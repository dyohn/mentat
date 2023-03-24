using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface IUserService
    {
        List<Users> GetAllUsers();

        List<Users> GetFilteredUserList(List<int> Role);

        Users GetUser( string id);

        Users GetFirstName( string id);

        Users GetLastName( string id);

        void RemoveUser (string id);

        void saveUser( string id, Users user);

    }
}
