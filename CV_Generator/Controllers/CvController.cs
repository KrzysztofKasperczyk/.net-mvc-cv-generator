using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using CvGenerator.Models;

namespace CvGenerator.Controllers
{
    public class CvController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public CvController(IWebHostEnvironment env)
        {
            _env = env;
        }
        private List<SelectListItem> LoadCountryCodes()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "countryCodes.json");
            var json = System.IO.File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<CountryCodeItem>>(json);
            return items.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
        }

        // GET: /Cv/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CountryCodes = LoadCountryCodes();
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
            ViewBag.CountryCodes = LoadCountryCodes();
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
    public class CountryCodeItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
