using System.Collections.Generic;
using Mentat.Domain.IService;
using Mentat.Domain.Models;
using Mentat.Repository.Services;

namespace Mentat.Domain.Service
{
    public class StudentService : IStudentService
    {
        private readonly ICardService _cardService;

        public StudentService(
            ICardService cardService
            )
        {
            _cardService = cardService;
        }

        public StudentVM GetStudentVM(List<string> selectedDifficulties)
        {
            var cards = _cardService.GetFilteredCardsList(selectedDifficulties);
            var tags = _cardService.GetAllTags();
            var vm = new StudentVM
            {
                AvailableCards = new List<FlashCardVM>(),
                SelectedCardIndex = 1,
                FilterCount = selectedDifficulties?.Count ?? 0,
                SelectedDifficulties = selectedDifficulties,
                AllUniqueTags = tags
            };
            
            foreach (var card in cards)
            {
                var flashCard = new FlashCardVM
                {
                    CardID = card.Id,
                    Subject = card.Subject,
                    CardQuestion = card.Question,
                    HiddenCardAnswer = card.Answer,
                    DifficultyLevel = card.DifficultyLevel,
                    // this will need permissions heavier than this when that part of the project is finished, like card.IsCustom && userID == loggedInUser
                    CanEditOrDelete = card.IsCustom,
                    Tags = card.Tags
                };
                vm.AvailableCards.Add(flashCard);
            }

            return vm;
        }
    }
}
