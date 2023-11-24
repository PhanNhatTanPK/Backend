using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class UserChangePasswordReq
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public string? TokenResetPass {get; set; }
    }
}
