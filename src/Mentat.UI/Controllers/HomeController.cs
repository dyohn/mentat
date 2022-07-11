using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mentat.UI.Models;
using Mentat.UI.Services;
using Mentat.UI.ViewModels;

namespace Mentat.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICardService cardService;

        public HomeController(ILogger<HomeController> logger, ICardService cardService)
        {
            _logger = logger;
            this.cardService = cardService;
        }

        static List<Card> GetRandomCards(List<Card> cards, int numCards)
        {
            if (cards.Count < numCards)
            {
                return cards;
            }

            HashSet<int> indices = new HashSet<int>();
            Random random = new Random();

            List<Card> randomCards = new List<Card>();
            while (indices.Count < numCards)
            {
                int index = random.Next(cards.Count);
                if (!indices.Contains(index))
                {
                    indices.Add(index);
                    randomCards.Add(cards[index]);
                }
            }

            return randomCards;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cards = cardService.Get();
            var randomCards = GetRandomCards(cards, 5);
            return View(new CardViewModel(randomCards));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
