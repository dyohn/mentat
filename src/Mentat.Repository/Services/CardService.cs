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

        public Card Create(Card card)
        {
            _cards.InsertOne(card);
            return card;
        }

        public List<Card> Get()
        {
           return _cards.Find(card => true).ToList();
        }

        public Card Get(string id)
        {
            return _cards.Find(card => (string) card._id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _cards.DeleteOne(card => (string)card._id == id);
        }

        public void Update(string id, Card card)
        {
            _cards.ReplaceOne(card => (string)card._id == id, card);
        }
    }
}
