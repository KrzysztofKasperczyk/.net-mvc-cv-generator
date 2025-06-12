using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CvGenerator.Models;

namespace CvGenerator.Controllers
{
    public class CvController : Controller
    {
        private List<SelectListItem> GetCountryCodes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "PL +48", Value = "+48" },
                new SelectListItem { Text = "US +1", Value = "+1" },
                new SelectListItem { Text = "UK +44", Value = "+44" }
            };
        }

        // GET: /Cv/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CountryCodes = GetCountryCodes();
            var model = new CurriculumVitae
            {
                Personal = new PersonalInfo { CountryCode = "+48" },
                Education = new List<EducationItem> { new EducationItem() },
                Experience = new List<WorkExperience> { new WorkExperience() },
                Skills = new List<Skill> { new Skill() }
            };

            return View(model);
        }

        // POST: /Cv/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CurriculumVitae model)
        {
            ViewBag.CountryCodes = GetCountryCodes();
            // For preview purposes, bypass validation and directly render the Preview view
            return View("Preview", model);
        }

        // GET: /Cv/Preview
        [HttpGet]
        public IActionResult Preview(CurriculumVitae model)
        {
            return View(model);
        }
    }
}
