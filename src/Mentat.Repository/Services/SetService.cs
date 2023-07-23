using Mentat.Repository.Models;
using Mentat.Repository.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mentat.Repository.Services
{
    public class SetService : ISetService
    {
        private readonly IMongoCollection<Set> _sets;
        private readonly IMongoCollection<Card> _cards;

        public SetService(IOptionsMonitor<SetDatabaseOptions> options, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(options.CurrentValue.DatabaseName);

            _sets = database.GetCollection<Set>(options.CurrentValue.SetCollectionName);
            _cards = database.GetCollection<Card>(options.CurrentValue.CardCollectionName);
        }

        public List<Set> GetAllSets()
        {
            List<Set> sets = new List<Set>();
            try
            {
                // Get all sets
                sets = _sets.Find(sets => true).ToList();
            }
            // Add a set marked with error info in case of exception.
            catch (Exception)
            {
                Set s = new Set();
                s.Title = $"There was a problem retrieving the set.";
                sets.Add(s);
            }
            return sets;
        }

        public List<Card> GetSetCards(string setID)
        {
            List<Card> cards = new List<Card>();
            try
            {
                // Get all cards if list of difficulty levels empty.
                cards = _cards.Find(c => c.SetId == setID).ToList();
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

        public Set GetSet(string id)
        {
            Set set = new Set();
            try
            {
                set = _sets.Find(set => set.Id.Equals(id)).SingleOrDefault();
            }
            catch (Exception)
            {
                set.Title = "There was a problem retrieving the set";
            }
            return set;
        }

        public void RemoveSet(string id)
        {
            try
            {
                _sets.DeleteOne(set => set.Id.Equals(id));
            }
            catch (Exception)
            {
                return;
            }
        }

        public void SaveSet(string id, Set set)
        {
            if (id != null)
            {
                set.Id = id;
                if (GetSet(id) == null)
                {
                    throw new Exception("Set ID invalid.");
                }
            }
            else
            {
                set.Id = Guid.NewGuid().ToString();
            }

            try
            {
                _sets.ReplaceOne(s => s.Id.Equals(set.Id), set, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception)
            {
                return;
            }

        }
    }
}

