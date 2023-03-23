using Mentat.Repository.Models;
using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface IUserService
    {
        List<Users> GetAllUsers();

        List<Users> GetFilteredUserList(List<string> Role);

        Users GetUser( string id);

        void RemoveUser (string id);

        void saveCard( string id, Users user);

    }
}
