using Mentat.Repository.Models;
using Mentat.Repository.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class CardService : ICardService
    {
        private readonly IMongoCollection<Card> _cards;

        public CardService(IOptionsMonitor<CardDatabaseOptions> options, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(options.CurrentValue.DatabaseName);

            _cards = database.GetCollection<Card>(options.CurrentValue.CardCollectionName);
        }

        public List<Card> GetAllCards()
        {
            return GetFilteredCardsList(null);
        }

        public List<Card> GetFilteredCardsList(List<string> difficultyLevels)
        {
            List<Card> cards = new List<Card>();
            try
            {
                // Get all cards if list of difficulty levels empty.
                if (difficultyLevels == null)
                {
                    cards = _cards.Find(card => true).ToList();
                }
                // Else get the cards of specified difficulty.
                else
                {
                    cards = _cards.Find(c => difficultyLevels.Contains(c.DifficultyLevel)).ToList();
                }
            }
            // Add a card marked with error info in case of exception.
            catch (Exception)
            {
                Card c = new Card();
                c.Answer = $"There was a problem retrieving cards.";
                cards.Add(c);
            }
            return cards;
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
            Card card = new Card();
            try
            {
                card = _cards.Find(card => card.Id.Equals(id)).SingleOrDefault();
            }
            catch (Exception)
            {
                card.Subject = "There was a problem retrieving the card";
            }
            return card;
        }

        public void RemoveCard(string id)
        {
            try
            {
                _cards.DeleteOne(card => card.Id.Equals(id));
            }
            catch (Exception)
            {
                return;
            }
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

            try
            {
                _cards.ReplaceOne(c => c.Id.Equals(card.Id), card, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception)
            {
                return;
            }

        }
    }
}
