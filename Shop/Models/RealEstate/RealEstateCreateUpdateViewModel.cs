using System.ComponentModel.DataAnnotations;

namespace Shop.Models.RealEstate
{
    public class RealEstateCreateUpdateViewModel
    {
        public Guid? Id { get; set; }

        [Range(1, Double.PositiveInfinity, ErrorMessage = "Value cannot be negative")]
        public double? Area { get; set; }

        [Required]
        public string? Location { get; set; }

        [Range(1, 1000)]
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public List<IFormFile>? Files { get; set; }
        public List<RealEstateImageViewModel> Image { get; set; }
            = new List<RealEstateImageViewModel>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
