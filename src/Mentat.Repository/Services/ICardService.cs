using Mentat.Repository.Models;

namespace Mentat.Repository.Services
{
    public interface ICardService
    {
        List<Card> Get();
        Card Get(string id);
        Card Create(Card card);
        void Update(string id, Card card);
        void Remove(string id);
    }
}
