using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mentat.UI.Services;
using Mentat.UI.Models;

namespace Mentat.UI.Controllers
{
    public class CardController : Controller
    {

        public CardController(ICardService cardService) 
        {
            this.cardService = cardService;
        }

        [HttpGet]  
        // GET: CardController
        public ActionResult Index()
        {
            var cards = cardService.Get();
            return View(cards);
        }

        // GET: CardController/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.FlashCards =
                           Models.MongoHelper.database.GetCollection<Models.Card>("FlashCards");

            var filter = Builders<Models.Card>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.FlashCards.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: CardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {                
                object id = GenRandomId(24);
                cardService.Create(new Models.Card
                {
                  _id = id,
                  notes = collection["notes"]
                });
                
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

        private static Random random = new Random();
        private readonly ICardService cardService;

        private object GenRandomId(int v)
        {
            string strarr = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarr, v).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        // GET: CardController/Edit/5
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.FlashCards =
                           Models.MongoHelper.database.GetCollection<Models.Card>("FlashCards");

            var filter = Builders<Models.Card>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.FlashCards.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.FlashCards =
                               Models.MongoHelper.database.GetCollection<Models.Card>("FlashCards");

                var filter = Builders<Models.Card>.Filter.Eq("_id", id);
                var update = Builders<Models.Card>.Update
                    .Set("notes", collection["notes"]);

                var result = Models.MongoHelper.FlashCards.UpdateOneAsync(filter,update);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CardController/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.FlashCards =
                           Models.MongoHelper.database.GetCollection<Models.Card>("FlashCards");

            var filter = Builders<Models.Card>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.FlashCards.Find(filter).FirstOrDefault();

            return View(result);
        }

        // POST: CardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.FlashCards =
                               Models.MongoHelper.database.GetCollection<Models.Card>("FlashCards");

                var filter = Builders<Models.Card>.Filter.Eq("_id", id);

                var result = Models.MongoHelper.FlashCards.DeleteOneAsync(filter);

                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
