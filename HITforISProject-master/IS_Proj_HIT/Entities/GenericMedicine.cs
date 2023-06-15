using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class GenericMedicine
    {
        public GenericMedicine()
        {
            Medicines = new HashSet<Medicine>();
            PhysicianAssessmentMedications = new HashSet<PhysicianAssessmentMedication>();
        }

        public long GenericMedId { get; set; }
        public string GenericName { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<PhysicianAssessmentMedication> PhysicianAssessmentMedications { get; set; }
    }
}
