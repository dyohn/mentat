using System;

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
    }
}
