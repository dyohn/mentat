using Mentat.Repository.Models;
using Microsoft.AspNetCore.Http;
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

        public void SaveCard(string id, IFormCollection collection)
        {
            var card = new Card();
            if (id != null)
            {
                card = GetCard(id);
                if (card == null)
                {
                    throw new Exception("Card ID invalid.");
                }
            }

            card.Notes = collection["notes"];
            card.Subject = collection["subject"];
            card.Question = collection["question"];
            card.Answer = collection["answer"];
            card.DifficultyLevel = collection["difficultyLevel"];

            InsertOrUpdateCard(card);
        }

        private void InsertOrUpdateCard(Card card)
        {
            if (card.Id == null)
            {
                card.Id = Guid.NewGuid().ToString();
                _cards.InsertOne(card);
            }
            else
            {
                _cards.ReplaceOne(c => c.Id.Equals(card.Id), card);
            }
        }
    }
}
