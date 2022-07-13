using System;
using System.Collections.Generic;
using Mentat.Repository.Models;

namespace Mentat.UI.ViewModels
{
    public class CardViewModel
    {
        public Object _id { get; set; }
        public string subject { get; set; }

        public string question { get; set; }

        public string answer { get; set; }

        public bool isCustom { get; set; }

        public string difficulty_level { get; set; }

        public string notes { get; set; }

        public List<Card> cards { get; set; }
        public CardViewModel(Card card)
        {
            _id = card._id;
            subject = card.subject;
            question = card.question;
            answer = card.answer;
            isCustom = card.isCustom;    
            difficulty_level = card.difficulty_level;   
            notes = card.notes; 
        }
        public CardViewModel(List<Card> cards)
        {
            this.cards = cards;
        }
    }
}
