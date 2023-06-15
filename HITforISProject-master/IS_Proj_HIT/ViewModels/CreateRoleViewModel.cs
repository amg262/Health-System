using System.ComponentModel.DataAnnotations;

namespace IS_Proj_HIT.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
