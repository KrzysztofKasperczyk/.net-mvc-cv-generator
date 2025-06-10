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
            // Prepare an empty model for the view
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // For now, redirect to preview
            return RedirectToAction("Preview", model);
        }

        // GET: /Cv/Preview
        [HttpGet]
        public IActionResult Preview(CurriculumVitae model)
        {
            return View(model);
        }
    }
}
