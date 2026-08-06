namespace PortfolioWebsite.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            About = new About();
            Skills = new List<Skill>();
            Projects = new List<Project>();
            Contact = new ContactForm();
        }

        public About About { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Project> Projects { get; set; }

        public ContactForm Contact { get; set; }
    }
}