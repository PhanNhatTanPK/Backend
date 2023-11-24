using Microsoft.AspNetCore.Mvc.Rendering;

namespace Backend.Models
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
     
    }
}
