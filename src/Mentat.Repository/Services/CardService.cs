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

        public Card CreateCard(Card card)
        {
            _cards.InsertOne(card);
            return card;
        }

        public List<Card> GetCards()
        {
           return _cards.Find(card => true).ToList();
        }

        public Card GetCard(string id)
        {
            return _cards.Find(card => card.Id as string == id).FirstOrDefault();
        }

        public void RemoveCard(string id)
        {
            _cards.DeleteOne(card => card.Id as string == id);
        }

        public void UpdateCard(string id, Card card)
        {
            _cards.ReplaceOne(card => card.Id as string == id, card);
        }
    }
}
