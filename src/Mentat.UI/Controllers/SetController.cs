using System;
using Microsoft.AspNetCore.Mvc;
using Mentat.Repository.Services;
using Mentat.Repository.Models;
using Mentat.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Mentat.Domain.Models;
using Mentat.Domain.IService;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mentat.UI.Controllers
{
    public class SetController : Controller
    {
        private readonly ISetService _setService;
        private readonly ICardService _cardService;
        private readonly IMongoClient _mongoClient;

        public SetController(ISetService setService, ICardService cardService, IMongoClient mongoClient)
        {
            _setService = setService;
            _cardService = cardService;
            _mongoClient = mongoClient;
        }

        [HttpGet]
        // GET: SetController
        public ActionResult Index()
        {
            return View(new SetViewModel(_setService.GetAllSets()));
        }

        // GET: Set/StudySet
        public ActionResult StudySet(string id)
        {
            var cards = _cardService.GetCardsBySet(id);
            var vm = new CardViewModel { SetId = id, Cards = cards };

            return View(vm);
        }

        public ActionResult AddSet(string id = null)
        {
            if (id == null)
            {
                return View(new SetViewModel());
            }

            var set = _setService.GetSet(id);
            if (set == null)
            {
                return NotFound($"Set with ID = {id} not found");
            }
            return View(new SetViewModel(set));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSet(string id, [FromForm] Set set, [FromForm] List<Card> cards, [FromForm] string tagList)
        {
            try
            {
                var flashcardDB = _mongoClient.GetDatabase("fcApp");
                var flashcardCollection = flashcardDB.GetCollection<BsonDocument>("FlashCards");

                _setService.SaveSet(id, set);

                // Log the Set information
                Console.WriteLine($"Set ID: {set.Id}, Title: {set.Title}");

                foreach (var card in cards)
                {
                    // Generate a new unique Id for the card
                    card.Id = Guid.NewGuid().ToString();

                    // Set the SetId for the card
                    card.SetId = set.Id;

                    // Log the Card information
                    Console.WriteLine($"Card ID: {card.Id}, SetId: {card.SetId}, Question: {card.Question}");

                    // Save the card to the FC database
                    var cardDocument = card.ToBsonDocument();
                    flashcardCollection.InsertOne(cardDocument);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex}");
                throw new Exception("Set not saved");
            }
        }

        // GET: SetController/Delete/5
        public ActionResult Delete(string id)
        {
            var set = _setService.GetSet(id);
            if (set == null)
            {
                return NotFound($"Set with ID = {id} not found");
            }

            return View(new SetViewModel(set));
        }

        // POST: SetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var card = _setService.GetSet(id);
                if (card == null)
                {
                    return NotFound($"Set with ID = {id} not found");
                }

                _setService.RemoveSet(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteSet(string setID)
        {
            _setService.RemoveSet(setID);
            return Content("ok");
        }
    }
}

