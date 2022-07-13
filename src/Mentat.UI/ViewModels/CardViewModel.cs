using System;
using System.Collections.Generic;
using Mentat.Repository.Models;

namespace Mentat.UI.ViewModels
{
    public class CardViewModel
    {
        public Object _id { get; set; }

        public string Subject { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool IsCustom { get; set; }

        public string DifficultyLevel { get; set; }

        public string Notes { get; set; }

        public List<Card> Cards { get; set; }
        public CardViewModel(Card card)
        {
            _id = card._id;
            Subject = card.Subject;
            Question = card.Question;
            Answer = card.Answer;
            IsCustom = card.IsCustom;    
            DifficultyLevel = card.DifficultyLevel;   
            Notes = card.Notes; 
        }
        public CardViewModel(List<Card> cards)
        {
            Cards = cards;
        }
    }
}
