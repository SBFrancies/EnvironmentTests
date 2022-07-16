using System.ComponentModel.DataAnnotations;

namespace EnvironmentTests.Models.Request
{
    public class CreateProductRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public ICollection<string> Colours { get; set; }

        [Required]
        public string First => Colours?.FirstOrDefault();
    }
}
