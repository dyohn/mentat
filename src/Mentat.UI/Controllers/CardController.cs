using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mentat.Repository.Services;
using Mentat.UI.ViewModels;

namespace Mentat.UI.Controllers
{
    public class CardController : Controller
    {
        private static Random random = new Random();
        private readonly ICardService _cardService;

        public CardController(ICardService cardService) 
        {
            _cardService = cardService;
        }

        [HttpGet]  
        // GET: CardController
        public ActionResult Index()
        {
            return View(new CardViewModel(_cardService.GetCards()));
        }

        // GET: CardController/Details/5
        public ActionResult Details(string id)
        {
            var card = _cardService.GetCard(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
        }

        public ActionResult AddEditCard(string id = null)
        {

            if (id == null)

            return View();
        }

        // POST: CardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {                
                var id = Guid.NewGuid().ToString();
                cardService.CreateCard(new Repository.Models.Card
                {
                    Id = id,
                    Notes = collection["notes"],
                    Subject = collection["subject"],
                    Question = collection["question"],
                    Answer = collection["answer"],
                    DifficultyLevel = collection["difficultyLevel"]
                });
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)

            {
                return View(new CardViewModel());
            }

            var card = _cardService.GetCard(id);
            if (card == null)
            {
                return NotFound($"Card with ID = {id} not found");
            }
            return View(new CardViewModel(card));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCard(string id, IFormCollection collection)
        {
            try
            {

                _cardService.SaveCard(id, collection);

                var existingCard = cardService.GetCard(id);
                if (existingCard == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                existingCard.Notes = collection["notes"];
                existingCard.Subject = collection["subject"];
                existingCard.Question= collection["question"];
                existingCard.Answer = collection["answer"];
                existingCard.DifficultyLevel = collection["difficultyLevel"];
                cardService.UpdateCard(id, existingCard);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new Exception("Card not saved");
            }
        }

        // GET: CardController/Delete/5
        public ActionResult Delete(string id)
        {
            var card = _cardService.GetCard(id);
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
                var card = _cardService.GetCard(id);
                if (card == null)
                {
                    return NotFound($"Card with ID = {id} not found");
                }

                _cardService.RemoveCard(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
