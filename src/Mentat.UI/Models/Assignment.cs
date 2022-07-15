using System.ComponentModel.DataAnnotations;

namespace Mentat.UI.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required]
        public string MentorName { get; set; }

        [Required]
        public string AssignmentName { get; set; }

        [Required]
        public string AssignmentType { get; set; }

        [Required]
        public string SampleExecutableName { get; set; }

        [Required]
        public string TestFileNames { get; set; }
    }
}
