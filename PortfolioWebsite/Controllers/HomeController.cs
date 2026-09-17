using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var portfolio = await GetPortfolioData();

            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm form)
        {
            var portfolio = await GetPortfolioData();

            if (!ModelState.IsValid)
            {
                portfolio.Contact = form;
                return View("Index", portfolio);
            }

            form.CreatedAt = DateTime.UtcNow;

            _context.ContactForms.Add(form);
            await _context.SaveChangesAsync();

            ModelState.Clear();

            ViewBag.Message = "Thank you! Your message has been received.";

            portfolio.Contact = new ContactForm();

            return View("Index", portfolio);
        }

        private async Task<HomeViewModel> GetPortfolioData()
        {
            var about = new About
            {
                Name = "Mark",
                Role = "Junior Backend Developer",
                Description = "Passionate about building web applications using ASP.NET Core MVC and C#.",
                Education = "BSc CSIT Student currently in 6th sem"
            };

            var skills = new List<Skill>
            {
                new Skill { Name = "C#", Level = "Newbie" },
                new Skill { Name = "ASP.NET Core", Level = "Learning" },
                new Skill { Name = "HTML", Level = "Learning" },
                new Skill { Name = "CSS", Level = "Learning" },
                new Skill { Name = "JavaScript", Level = "Learning" }
            };

            var projects = new List<Project>
            {
                new Project
                {
                    Title = "Bank Management System",
                    Description = "Desktop application developed in C#.",
                    Technology = "C#",
                    GithubLink = "https://github.com/Marky909/BankManagement"
                },

                new Project
                {
                    Title = "Portfolio Website",
                    Description = "Personal portfolio built with ASP.NET Core MVC.",
                    Technology = "ASP.NET Core MVC",
                    GithubLink = "https://github.com/Marky909/PortfolioWebsite"
                },

                new Project
                {
                    Title = "Kronos App",
                    Description = "Simple Reminder app with Alarm and Timer functionality.",
                    Technology = "HTML",
                    GithubLink = "https://github.com/Marky909/remainder-vibe"
                }
            };

            var model = new HomeViewModel();

            model.About = about;
            model.Skills = skills;
            model.Projects = projects;

            return model;
        }
    }
}