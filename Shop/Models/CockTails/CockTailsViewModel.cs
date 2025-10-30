namespace Shop.Models.CockTails
{
    public class CockTailsViewModel
    {
        public string SearchTerm { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public string IsAlcoholic { get; set; }
        public string Tags { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Measures { get; set; }

    }

}
