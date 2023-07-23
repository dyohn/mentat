using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface ISetService
    {
        List<Set> GetAllSets();

        List<Card> GetSetCards(string id);

        Set GetSet(string id);

        void RemoveSet(string id);

        void SaveSet(string id, Set set);
    }
}

