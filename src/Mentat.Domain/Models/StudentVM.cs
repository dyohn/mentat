using System.Collections.Generic;

namespace Mentat.Domain.Models
{
    public class StudentVM
    {
        public long SelectedCardID { get; set; }
        public List<FlashCardVM> AvailableCards { get; set; }
        public int NumberOfFlashCards => AvailableCards.Count;
    }

    public class FlashCardVM
    {
        public long CardID { get; set; }
        public string Subject { get; set; }
        public int DifficultyLevel { get; set; }
        public string CardQuestion { get; set; }
        public string HiddenCardAnswer { get; set; }
        public string CardAnswerOverlay => "";
        public string CardColor { get; set; }
        public List<FlashCardKeywordsVM> CardKeywordsList { get; set; }
        public bool CanEditOrDelete { get; set; }
    }

    public class FlashCardKeywordsVM
    {
        public long CardID { get; set; }
        public List<string> Keywords { get; set; }
    }
}
