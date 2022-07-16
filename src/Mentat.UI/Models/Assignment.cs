using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mentat.UI.Models
{
    public class Assignment
    {
        // ASSIGNMENT ID
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Id")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Assignment Id must be digits 0-9 only")]
        public int Id { get; set; }

        // MENTOR NAME
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Mentor Name")]
        [StringLength(80, ErrorMessage = "Mentor name may not exceed 80 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Mentor name cannot be blank")]
        public string MentorName { get; set; }

        // ASSIGNMENT NAME
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Name")]
        [StringLength(30, ErrorMessage = "Assignment name may not exceed 30 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Assignment name cannot be blank")]
        public string AssignmentName { get; set; }

        // ASSIGNMENT TYPE
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Assignment Type")]
        public string AssignmentType { get; set; }

        // SAMPLE EXECUTABLE FILE NAME
        [Required]
        [Display(Name = "Sample File Name")]
        [StringLength(255, ErrorMessage = "Sample file name may not exceed 255 characters")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Sample executable cannot be blank")]
        public string SampleExecutableName { get; set; }

        // TEST FILES NAME
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Test Files Names")]
        [RegularExpression(".*\\S.*", ErrorMessage = "Test File field cannot be blank. Enter a single file name by line")]
        public string[] TestFileNames { get; set; }




    }





}
