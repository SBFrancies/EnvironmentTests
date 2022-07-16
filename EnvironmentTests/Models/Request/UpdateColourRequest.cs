using System.ComponentModel.DataAnnotations;

namespace EnvironmentTests.Models.Request
{
    public class UpdateColourRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [RegularExpression("[a-zA-Z ]+")]
        public string Colour { get; set; }
    }
}
