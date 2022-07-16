using System.ComponentModel.DataAnnotations;

namespace EnvironmentTests.Models.Request
{
    public class CreateColourRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        [RegularExpression("[a-zA-Z ]+")]
        public string Colour { get; set; }
    }
}
