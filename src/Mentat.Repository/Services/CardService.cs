﻿using Mentat.Repository.Models;
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
            // Removed the creation of a new list directly
            try
            {
                // Get all cards if list of difficulty levels is empty.
                if (difficultyLevels == null)
                {
                    return _cards.Find(card => true).ToList();
                }
                // Else get the cards of specified difficulty.
                else
                {
                    return _cards.Find(c => difficultyLevels.Contains(c.DifficultyLevel)).ToList();
                }
            }
            // Add a card marked with error info in case of exception.
            catch (Exception)
            {
                Card c = new Card();
                c.Answer = $"There was a problem retrieving cards.";
                // Instead, result of ToList() is called and the returned list is a new instance
                return new List<Card> { c };
            }
        }

        public List<string> GetAllTags()
        {
            HashSet<string> uniqueTags = new HashSet<string>();
            try
            {
                var filter = Builders<Card>.Filter.Empty;
                var documents = _cards.Find(filter).ToList();

                foreach (var doc in documents)
                {
                    if (doc.Tags != null)
                        uniqueTags.UnionWith(doc.Tags);
                }
            }
            catch
            {
                // return empty tag list upon error
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
                if (card.IsPrivate == true)
                {
                    card.IsCustom = true;
                }
                if (card.IsPrivate == false)
                {
                    card.IsCustom = false;
                    card.Owner = "mentat";
                }
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

        public void CowCard(string id, string newOwner)
        {
            string newId = Guid.NewGuid().ToString();
            Card card = GetCard(id);
            card.Id = newId;
            card.Owner = newOwner;
            SaveCard(newId, card);
            return;
        }

    }
}
