using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class PhysicianAssessmentAllergy
    {
        public long PhysicianAssessmentId { get; set; }
        public int AllergenId { get; set; }

        public virtual Allergen Allergen { get; set; }
        public virtual PhysicianAssessment PhysicianAssessment { get; set; }
    }
}
