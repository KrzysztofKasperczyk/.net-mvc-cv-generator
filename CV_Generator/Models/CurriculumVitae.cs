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
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        // additional personal fields as needed
    }

    public class EducationItem
    {
        [Required]
        public string Institution { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
    }

    public class WorkExperience
    {
        [Required]
        public string Company { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

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
