using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
