using Data.Entities;

namespace Business.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }

}

