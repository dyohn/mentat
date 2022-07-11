using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mentat.UI.Models
{
    public class Assignment
    {
        public int id { get; set; }
        [Required]
        public string mentorName { get; set; }
        [Required]
        public string assignmentName { get; set; }
        [Required]
        public string assignmentType { get; set; }
        [Required]
        public string sampleExecutableName { get; set; }
        [Required]
        public string testFileNames { get; set; }
    }
}
