using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ViewUserViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
        public string Id { get; set; }
        [Display(Name = "Họ")]
        public string FirstName { get; set; }
        [Display(Name = "Tên")]
        public string Lastname { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public long? DistrictId { get; set; }
    }
}
