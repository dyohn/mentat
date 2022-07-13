using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface ICardService
    {
        List<Card> GetCards();

        Card GetCard(string id);

        Card CreateCard(Card card);

        void UpdateCard(string id, Card card);
        
        void RemoveCard(string id);
    }
}
