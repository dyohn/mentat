using System.ComponentModel;

namespace Mentat.UI.Models
{
    public class GradingScriptFormModel
    {
        [DisplayName("Language")]
        public string language { get; set; }

        [DisplayName("File Names")]
        public string file_names { get; set; }

        [DisplayName("Executable Name")]
        public string exec_name { get; set; }

        [DisplayName("Time Limit")]
        public string duration { get; set; }

        [DisplayName("Diff Command")]
        public string diff_cmd { get; set; }
    }
}

