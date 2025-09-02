namespace Shop.Models.Spaceships
{
    public class SpaceshipIndexViewModel
    {

        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public string? Classification { get; set; }
        public DateTime? BuildDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }
    }
}
