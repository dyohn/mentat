using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using Mentat.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Mentat.UI.Models
{
    public class Assignment
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Id")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Assignment Id must be digits 0-9 only")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Mentor Name")]
        [StringLength(80, ErrorMessage = "Mentor name may not exceed 80 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Mentor name cannot be blank")]
        public string MentorName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Name")]
        [StringLength(30, ErrorMessage = "Assignment name may not exceed 30 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Assignment name cannot be blank")]
        public string AssignmentName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Type")]
        public string AssignmentType { get; set; }

        [Display(Name = "Color Text")]
        public bool ColorText { get; set; }

        [Display(Name = "Highlight Text")]
        public bool HighlightText { get; set; }

        [Display(Name = "Apply Text Modifiers")]
        public bool ApplyTextModifiers { get; set; }

        public string TestFiles { get; set; }

        [Required]
        [Display(Name = "Sample File Name")]
        [StringLength(255, ErrorMessage = "Sample file name may not exceed 255 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Sample executable cannot be blank")]
        public string SampleExecutableName { get; set; }

        [Display(Name = "Test File Name")]
        public string TestFileName { get; set; }
    }
}
