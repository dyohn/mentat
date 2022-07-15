using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface ICardService
    {
        List<Card> GetCards();

        Card GetCard(string id);

        Card AddCard(Card card);

        void UpdateCard(string id, Card card);

        public bool TryUpdateCard(string id, Card card);

        void RemoveCard(string id);

        public bool TryRemoveCard(string id);
    }
}
