namespace PortfolioWebsite.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Technology { get; set; }

        public string GithubLink { get; set; }
    }
}