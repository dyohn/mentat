using System.Collections.Generic;

namespace Mentat.Domain.Models
{
    public class StudentVM
    {
        public int SelectedCardIndex { get; set; }
        public int CurrentIndex => SelectedCardIndex;
        public List<FlashCardVM> AvailableCards { get; set; }
        public int NumberOfFlashCards => AvailableCards.Count;
    }

    public class FlashCardVM
    {
        public string CardID { get; set; }
        public string Subject { get; set; }
        public string DifficultyLevel { get; set; }
        public string CardQuestion { get; set; }
        public string HiddenCardAnswer { get; set; }
        public string CardAnswerOverlay => "";
        public string CardColor { get; set; }
        public bool CanEditOrDelete { get; set; }
    }
}
