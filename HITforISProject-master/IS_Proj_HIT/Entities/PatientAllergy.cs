﻿using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class PatientAllergy
    {
        public long PatientAllergyId { get; set; }
        public int? AllergenId { get; set; }
        public int? ReactionId { get; set; }
        public DateTime LastModified { get; set; }
        public long PatientAlertId { get; set; }

        public virtual Allergen Allergen { get; set; }
        public virtual PatientAlert PatientAlert { get; set; }
        public virtual Reaction Reaction { get; set; }
    }
}
