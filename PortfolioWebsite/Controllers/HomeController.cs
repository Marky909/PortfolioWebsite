using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PortfolioWebsite.Models;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult Index()
        {
            return View(GetPortfolioData());
        }

        public DateTime GETUTCDATE()
        {
            return DateTime.UtcNow;
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm form)
        {
            var portfolio = GetPortfolioData();

            if (!ModelState.IsValid)
            {
                portfolio.Contact = form;
                return View("Index", portfolio);
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO ContactForms(Name,Email,Message,CreatedAt) VALUES (@Name,@Email,@Message,@CreatedAt)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", form.Name);
                command.Parameters.AddWithValue("@Email", form.Email);
                command.Parameters.AddWithValue("@Message", form.Message);
                command.Parameters.AddWithValue("@CreatedAt", form.CreatedAt);
                    
                await connection.OpenAsync();
                command.ExecuteNonQuery();

                ModelState.Clear();

                ViewBag.Message = "Thank you! Your message has been received.";
                portfolio.Contact = new ContactForm();
                
            }

            return View("Index", portfolio);
        }

     
        private HomeViewModel GetPortfolioData()
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
                    Description = "Simple Remainder app with Alarm and Timer functionality.",
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