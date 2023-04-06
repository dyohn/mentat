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

        public List<Card> GetAllCards()
        {
           return _cards.Find(card => true).ToList();
        }

        public List<Card> GetFilteredCardsList(List<string> difficultyLevels)
        {
            if(difficultyLevels == null)
            {
                return GetAllCards();
            }

            return _cards
                .Find(c => difficultyLevels.Contains(c.DifficultyLevel))
                .ToList();
        }

        public List<string> GetAllTags()
        {
            var filter = Builders<Card>.Filter.Empty;

            var documents = _cards.Find(filter).ToList();


            HashSet<string> uniqueTags = new HashSet<string>();
            foreach (var doc in documents)
            {
                if (doc.Tags != null)
                    uniqueTags.UnionWith(doc.Tags);
            }
            return uniqueTags.ToList();

        }

        public Card GetCard(string id)
        {
            return _cards.Find(card => card.Id.Equals(id)).SingleOrDefault();
        }

        public void RemoveCard(string id)
        {
            _cards.DeleteOne(card => card.Id.Equals(id));
        }

        public void SaveCard(string id, Card card)
        {
            if (id != null)
            {
                card.Id = id;
                if (GetCard(id) == null)
                {
                    throw new Exception("Card ID invalid.");
                }
            }
            else
            {
                card.Id = Guid.NewGuid().ToString();
            }

            _cards.ReplaceOne(c => c.Id.Equals(card.Id), card, new ReplaceOptions { IsUpsert = true });

        }
    }
}
