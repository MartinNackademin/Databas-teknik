using Data.Entities;

namespace Business.Models
{
    public class ProjectRegistrationModel
    {
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;

        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; } = null!;

    }

}

