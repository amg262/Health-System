﻿using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class PatientMilitaryService
    {
        public int MilitaryServiceId { get; set; }
        public string Mrn { get; set; }
        public bool? IsVeteran { get; set; }
        public bool? CurrentlyInService { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Patient MrnNavigation { get; set; }
    }
}
