// GradingScriptFormModel.cs
// Devin
// 7/3/2022
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mentat.UI.Models
{
    public class GradingScriptFormModel
    {
        public List<Language> languages { get; set; }
        public string file_names { get; set; }
        public string exec_name { get; set; }
        public int duration { get; set; }
        public string diff_cmd { get; set; }
    }

    public class Language
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }
}

