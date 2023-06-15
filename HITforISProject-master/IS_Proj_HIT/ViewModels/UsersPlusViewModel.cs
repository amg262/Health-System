﻿using System;

namespace IS_Proj_HIT.ViewModels
{
    public class UsersPlusViewModel
    {
        // from Facility
        public int FacilityId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public DateTime LastModified { get; set; }

        // from Program
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }

        // from UserTable
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProgramEnrolledIn { get; set; }
        public string Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsSelected { get; set; }

        public string RoleName {get; set;}
        public string AspNetUsersId { get; set; }
    }
}
