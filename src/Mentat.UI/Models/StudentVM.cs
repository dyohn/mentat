using System.Collections.Generic;

namespace Mentat.UI.Models
{
    //todo: These VMs will be moved to the Domain when I have merged down and have access to the Model folder seen in the Professor's solution
    public class StudentVM
    {
        public long SelectedCardID { get; set; }
        public List<FlashCardVM> AvailableCards { get; set; }
    }

    public class FlashCardVM
    {
        public long CardID { get; set; }
        public string CardQuestion { get; set; }
        public string HiddenCardAnswer { get; set; }
        public string CardAnswer { get; set; }
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
