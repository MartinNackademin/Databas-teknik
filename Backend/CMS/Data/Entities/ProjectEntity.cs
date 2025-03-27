namespace Data.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; } = null!;
        public int CustomerId { get; set; }

        public CustomerEntity Customer { get; set; } = null!;
    }
}
