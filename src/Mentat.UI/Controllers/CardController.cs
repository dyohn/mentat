using System;
using Microsoft.AspNetCore.Mvc;
using Mentat.Repository.Services;
using Mentat.UI.ViewModels;
using Mentat.Repository.Models;
using Microsoft.Extensions.Logging;

namespace Mentat.UI.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
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

            if (card is null)
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
        public ActionResult Create([FromForm] Card card)
        {
            try
            {                
                _cardService.AddCard(card);
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while trying to create card", ex.Message);

                return View();
            }
        }

        // GET: CardController/Edit/5
        // Edit step 1
        public ActionResult Edit(string id)
        {
            var card = _cardService.GetCard(id);

            if (card is null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
        }

        // POST: CardController/Edit/5
        // Edit step 2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, [FromForm] Card card)
        {
            try
            {
                if (_cardService.TryUpdateCard(id, card))
                {
                    return RedirectToAction(nameof(Index));
                }

                return NotFound($"Card with ID = {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while trying to update card {id}", ex.Message);

                return View();
            }
        }

        // GET: CardController/Delete/5
        // Delete Step 1
        public ActionResult Delete(string id)
        {
            var card = _cardService.GetCard(id);

            if (card is null)
            {
                return NotFound($"Card with ID = {id} not found");
            }

            return View(new CardViewModel(card));
        }

        // POST: CardController/Delete/5
        // Delete step 2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, [FromForm] Card card)
        {
            try
            {               
                if (_cardService.TryRemoveCard(id))
                {
                    return RedirectToAction(nameof(Index));
                }

                return NotFound($"Card with ID = {id} not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while trying to delete card {id}", ex.Message);

                return View();
            }
        }
    }
}
