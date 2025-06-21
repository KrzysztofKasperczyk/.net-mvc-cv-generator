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

        public string? PhotoPath { get; set; }

        [StringLength(500, ErrorMessage = "Summary must be at most 500 characters.")]
        [Display(Name = "Summary")]
        public string? Summary { get; set; }
    }

    public class EducationItem
    {
        
        [StringLength(100, ErrorMessage = "Institution must be at most 100 characters.")]
        public string? Institution { get; set; }

        
        [StringLength(80, ErrorMessage = "Degree must be at most 80 characters.")]
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

        /// <summary>
        /// Main fields, that need to be filled so the whole thing can show up in summary.
        /// </summary>
        public bool HasEssentialData =>
            !string.IsNullOrWhiteSpace(Institution)
         || !string.IsNullOrWhiteSpace(Degree);
         
    }

    public class WorkExperience
    {
        [StringLength(50, ErrorMessage = "Company name must be at most 50 characters.")]
        public string? Company { get; set; }

        [StringLength(50, ErrorMessage = "Position name must be at most 50 characters.")]
        public string? Position { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Indicates if the user is still working here
        [Display(Name = "I am currently working here")]
        public bool IsOngoing { get; set; }

        [StringLength(500, ErrorMessage = "Responsibilities must be at most 50 characters.")]
        public string? Responsibilities { get; set; }


        /// <summary>
        /// Main fields, that need to be filled so the whole thing can show up in summary.
        /// </summary>
        public bool HasEssentialData =>
            !string.IsNullOrWhiteSpace(Company)
         || !string.IsNullOrWhiteSpace(Position);
    }

    public class Skill
    {
        public int? Id { get; set; }

        
        public string? Name { get; set; }

        
        [Range(1, 100, ErrorMessage = "Level must be between 1 and 100.")]
        [Display(Name = "Skill Level (1–100)")]
        public int? Level { get; set; } = 1;

        [StringLength(200, ErrorMessage = "Description must be at most 200 characters.")]
        public string? Description { get; set; }

        public bool HasEssentialData =>
            !string.IsNullOrWhiteSpace(Name);
         
    }
}
