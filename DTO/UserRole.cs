using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class UserRole
    {   
        [Required]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }        
        [Required]
        public string Role { get; set; }
    }
}
