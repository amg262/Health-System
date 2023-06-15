using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class BodySystemType
    {
        public BodySystemType()
        {
            BodySystemAssessments = new HashSet<BodySystemAssessment>();
        }

        public short BodySystemTypeId { get; set; }
        public string Name { get; set; }
        public string NormalLimitsDescription { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual ICollection<BodySystemAssessment> BodySystemAssessments { get; set; }
    }
}
