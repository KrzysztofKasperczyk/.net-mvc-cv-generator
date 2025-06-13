using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CvGenerator.Models
{
    public class CurriculumVitae
    {
        public int Id { get; set; }

        [Required]
        public string Language { get; set; }    // "pl" or "en"

        public PersonalInfo Personal { get; set; }
        public List<EducationItem> Education { get; set; }
        public List<WorkExperience> Experience { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class PersonalInfo
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full Name must be at most 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email Address must be at most 100 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // additional personal fields as needed
    }

    public class EducationItem
    {
        
        [StringLength(100, ErrorMessage = "Institution must be at most 100 characters.")]
        public string? Institution { get; set; }

        
        [StringLength(30, ErrorMessage = "Degree must be at most 30 characters.")]
        public string? Degree { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Indicates if the user is still studying here
        [Display(Name = "I am currently studying here")]
        public bool IsOngoing { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }
    }

    public class WorkExperience
    {
        [Required]
        public string Company { get; set; }

        [Required]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Indicates if the user is still working here
        [Display(Name = "I am currently working here")]
        public bool IsOngoing { get; set; }

        public string Responsibilities { get; set; }
    }

    public class Skill
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Level { get; set; }      // e.g. "Beginner", "Intermediate", "Advanced"

        public int? Rating { get; set; }       // optional rating 1–5
    }
}
