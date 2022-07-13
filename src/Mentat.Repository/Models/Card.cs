using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mentat.Repository.Models
{
    public class Card
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
