using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code minimum value is 3")]
        [MaxLength(3, ErrorMessage = "Code maximum value is 3")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name maximum characters is 100")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
