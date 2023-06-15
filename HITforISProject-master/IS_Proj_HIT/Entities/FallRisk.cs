using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class FallRisk
    {
        public FallRisk()
        {
            PatientFallRisks = new HashSet<PatientFallRisk>();
        }

        public byte FallRiskId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PatientFallRisk> PatientFallRisks { get; set; }
    }
}
