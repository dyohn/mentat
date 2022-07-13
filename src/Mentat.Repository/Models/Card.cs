using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mentat.Repository.Models
{
    public class Card
    {
        public Object? _id { get; set; }

        public string? Subject { get; set; }

        public string? Question { get; set; }

        public string? Answer { get; set; }

        public bool IsCustom { get; set; }

        public string? DifficultyLevel { get; set; }

        public string? Notes { get; set; }
    }
}
