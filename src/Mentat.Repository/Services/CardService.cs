using Mentat.Repository.Models;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class CardService : ICardService
    {
        private readonly IMongoCollection<Card> _cards;

        public CardService(ICardDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);

            _cards = database.GetCollection<Card>(settings.CardCollectionName);
        }

        public Card AddCard(Card card)
        {
            card.Id = Guid.NewGuid().ToString();

            _cards.InsertOne(card);

            return card;
        }

        public List<Card> GetCards()
        {
           return _cards.Find(card => true).ToList();
        }

        public Card GetCard(string id)
        {
            return _cards.Find(card => card.Id.Equals(id)).SingleOrDefault();
        }

        public void RemoveCard(string id)
        {
            _cards.DeleteOne(card => card.Id.Equals(id));
        }

        public bool TryRemoveCard(string id)
        {
            var result = _cards.DeleteOne(card => card.Id.Equals(id));

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public void UpdateCard(string id, Card card)
        {
            _cards.ReplaceOne(card => card.Id.Equals(id), card);
        }

        public bool TryUpdateCard(string id, Card card)
        {
            var result = _cards.ReplaceOne(card => card.Id.Equals(id), card);

            return result.IsAcknowledged && result.ModifiedCount == 1;
        }
    }
}
