using System;
using System.Collections.Generic;
using Mentat.Repository.Models;

namespace Mentat.UI.ViewModels
{
    public class CardViewModel
    {
        public Object Id { get; set; }

        public string Subject { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool IsCustom { get; set; }

        public string DifficultyLevel { get; set; }

        public string Notes { get; set; }

        public List<Card> Cards { get; set; }

        public CardViewModel(Card card)
        {
            Id = card._id;
            Subject = card.subject;
            Question = card.question;
            Answer = card.answer;
            IsCustom = card.isCustom;    
            DifficultyLevel = card.difficulty_level;   
            Notes = card.notes; 
        }

        public CardViewModel(List<Card> cards)
        {
            Cards = cards;
        }
    }
}
