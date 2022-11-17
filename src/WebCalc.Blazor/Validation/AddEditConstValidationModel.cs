using System.ComponentModel.DataAnnotations;

namespace WebCalc.Blazor.Validation
{
    public class AddEditConstValidationModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(typeof(float), "1", "1000000", ErrorMessage = $"Value should be in range 1-1 000 000")]
        public float? Value { get; set; }

        public string? Description { get; set; }
    }
}
