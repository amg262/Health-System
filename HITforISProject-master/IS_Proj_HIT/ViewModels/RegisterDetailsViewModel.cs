using System;
using System.ComponentModel.DataAnnotations;

namespace IS_Proj_HIT.ViewModels
{
    public class RegisterDetailsViewModel
    {
        // from UserTable
        //public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Instructor { get; set; }
        [Required]
        public string ProgramEnrolledIn { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public int AspNetUserID { get; set; } 
    }
}
