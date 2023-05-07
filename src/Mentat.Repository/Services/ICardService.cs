using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface ICardService
    {
        List<Card> GetAllCards();

        List<Card> GetFilteredCardsList(List<string> difficultyLevels);

        List<string> GetAllTags();

        Card GetCard(string id);
        
        void RemoveCard(string id);

        void SaveCard(string id, Card card);
    }
}
