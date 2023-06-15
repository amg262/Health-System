using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class PhysicianAssessmentMedication
    {
        public long PhysicianAssessmentId { get; set; }
        public long GenericMedId { get; set; }

        public virtual GenericMedicine GenericMed { get; set; }
        public virtual PhysicianAssessment PhysicianAssessment { get; set; }
    }
}
