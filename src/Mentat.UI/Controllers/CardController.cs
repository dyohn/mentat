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
        private static Random random = new Random();
        private readonly ICardService cardService;

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
            var card = cardService.Get(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(card);
        }

        // GET: CardController/Create
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
            catch (Exception)
            {
                return View();
            }
        }

        private object GenRandomId(int v)
        {
            string strarr = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarr, v).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        // GET: CardController/Edit/5
        public ActionResult Edit(string id)
        {
            var card = cardService.Get(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(card);
        }

        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var existingCard = cardService.Get(id);
                if (existingCard == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                existingCard.notes = collection["notes"];
                cardService.Update(id, existingCard);

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
            var card = cardService.Get(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(card);
        }

        // POST: CardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var card = cardService.Get(id);
                if (card == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                cardService.Remove(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
