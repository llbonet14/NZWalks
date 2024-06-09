using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name maximum characters is 100")]
        public string Name { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Description maximum characters is 500")]
        public string Description { get; set; }
        [Required]
        [Range(0, 1000)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
