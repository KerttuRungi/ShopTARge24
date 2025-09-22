namespace Shop.Models.Kindergarten
{
    public class KindergartenDeleteViewModel
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public int ChidlrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
