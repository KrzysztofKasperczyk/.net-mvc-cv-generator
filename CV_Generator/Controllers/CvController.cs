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

        [HttpGet]
        public IActionResult SearchInstitutions(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                return Json(Array.Empty<string>());

            // Ścieżka do pliku JSON z polskimi uczelniami
            var path = Path.Combine(_env.WebRootPath, "data", "polishUniversities.json");
            if (!System.IO.File.Exists(path))
                return Json(Array.Empty<string>());

            var json = System.IO.File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<List<University>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<University>();

            var matches = list
                .Select(u => u.Name)
                .Where(n => n.Contains(term, StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .Take(10)
                .ToList();

            return Json(matches);
        }

        [HttpGet]
        public IActionResult SearchDegrees(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                return Json(Array.Empty<string>());

            var path = Path.Combine(_env.WebRootPath, "data", "degrees.json");
            if (!System.IO.File.Exists(path))
                return Json(Array.Empty<string>());

            var json = System.IO.File.ReadAllText(path);
            var allDegrees = JsonSerializer.Deserialize<List<string>>(json)
                             ?? new List<string>();

            var matches = allDegrees
                .Where(d => d.Contains(term, StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .Take(10)
                .ToList();

            return Json(matches);
        }

        private class University
        {
            public string Name { get; set; }
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
