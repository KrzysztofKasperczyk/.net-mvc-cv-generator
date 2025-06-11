using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CvGenerator.Models;

namespace CvGenerator.Controllers
{
    public class CvController : Controller
    {
        // GET: /Cv/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CurriculumVitae
            {
                Personal = new PersonalInfo(),
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
