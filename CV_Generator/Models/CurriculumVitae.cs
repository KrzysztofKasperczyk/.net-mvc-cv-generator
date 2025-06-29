using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CvGenerator.Models
{
    public class CurriculumVitae : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Language { get; set; }    // "pl" or "en"

        public PersonalInfo Personal { get; set; }
        public List<EducationItem> Education { get; set; }
        public List<WorkExperience> Experience { get; set; }
        public List<Skill> Skills { get; set; }

        // here comes our validation for Languages:
        public List<LanguageItem> Languages { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // enforce at least one language
            if (!Languages.Any(l => l.HasEssentialData))
            {
                yield return new ValidationResult(
                    "Add at least one language.",
                    new[] { nameof(Languages) }
                );
            }
        }

        public List<CustomSection> CustomSections { get; set; } = new List<CustomSection>();
    }

    public class CustomSection
    {
        [StringLength(100)]
        public string? Title { get; set; }

        // one definition per field they create
        public List<CustomFieldDefinition> Fields { get; set; } = new();

        // the values they type, keyed by the field’s unique name
        public Dictionary<string, string?> Values { get; set; } = new();

    }

    public class CustomFieldDefinition
    {
        public string Id { get; set; }  // e.g. “field-0”, “field-1”
        public string Label { get; set; }  // what shows on the form
        public FieldType Type { get; set; }  // enum: Text, TextArea, Number, Date, etc.
        public int? MaxLength { get; set; }  // if it’s a text field
                                             
    }
    public enum FieldType
    {
        Text,
        TextArea,
        Number,
        Date
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
    public class LanguageItem
    {
        public string? Name { get; set; }
        public string? Level { get; set; }

        public static readonly string[] Levels =
          { "A1","A2","B1","B2","C1","C2","Native" };

        public bool HasEssentialData => !string.IsNullOrWhiteSpace(Name);
    }


}
