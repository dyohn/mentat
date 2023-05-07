using System.Collections.Generic;
using Mentat.Repository.Models;

namespace Mentat.UI.ViewModels
{
    public class CardViewModel
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool IsCustom { get; set; }

        public string DifficultyLevel { get; set; }

        public string Notes { get; set; }

        public List<Card> Cards { get; set; }

        public List<string> Tags { get; set; }

        public CardViewModel(Card card)
        {
            Id = card.Id;
            Subject = card.Subject;
            Question = card.Question;
            Answer = card.Answer;
            IsCustom = card.IsCustom;    
            DifficultyLevel = card.DifficultyLevel;   
            Notes = card.Notes;
            Tags = card.Tags;
        }

        public CardViewModel(List<Card> cards)
        {
            Cards = cards;
        }

        public CardViewModel()
        {
        }
    }
}
