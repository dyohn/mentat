using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mentat.Repository.Services;
using Mentat.UI.ViewModels;

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
            return View(new CardViewModel(cardService.GetCards()));
        }

        // GET: CardController/Details/5
        public ActionResult Details(string id)
        {
            var card = cardService.GetCard(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
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
                object id = Guid.NewGuid();
                cardService.CreateCard(new Repository.Models.Card
                {
                  _id = id,
                  notes = collection["notes"],
                  subject = collection["subject"],
                  question = collection["question"],
                  answer = collection["answer"],
                  difficulty_level = collection["difficulty_level"]

                });
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: CardController/Edit/5
        public ActionResult Edit(string id)
        {
            var card = cardService.GetCard(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
        }

        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var existingCard = cardService.GetCard(id);
                if (existingCard == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                existingCard.notes = collection["notes"];
                existingCard.subject = collection["subject"];
                existingCard.question = collection["question"];
                existingCard.answer = collection["answer"];
                existingCard.difficulty_level = collection["difficulty_level"];
                cardService.UpdateCard(id, existingCard);

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
            var card = cardService.GetCard(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
        }

        // POST: CardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var card = cardService.GetCard(id);
                if (card == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                cardService.RemoveCard(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
