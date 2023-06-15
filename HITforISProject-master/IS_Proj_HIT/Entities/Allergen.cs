using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class Allergen
    {
        public Allergen()
        {
            PatientAllergies = new HashSet<PatientAllergy>();
            PhysicianAssessmentAllergies = new HashSet<PhysicianAssessmentAllergy>();
        }

        public int AllergenId { get; set; }
        public string AllergenName { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientAllergy> PatientAllergies { get; set; }
        public virtual ICollection<PhysicianAssessmentAllergy> PhysicianAssessmentAllergies { get; set; }
    }
}
