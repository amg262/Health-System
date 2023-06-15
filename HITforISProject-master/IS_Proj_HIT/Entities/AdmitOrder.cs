using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class AdmitOrder
    {
        public long AdmitOrderId { get; set; }
        public long EncounterId { get; set; }
        public short VisitTypeId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderingPhysicianId { get; set; }
        public int? CoOrderingPhysicianId { get; set; }
        public bool IsOrderComplete { get; set; }
        public DateTime? OrderCompletedDateTime { get; set; }
        public int? OrderCompletedByPhysicianId { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Physician CoOrderingPhysician { get; set; }
        public virtual Department Department { get; set; }
        public virtual Encounter Encounter { get; set; }
        public virtual Physician OrderCompletedByPhysician { get; set; }
        public virtual Physician OrderingPhysician { get; set; }
        public virtual VisitType VisitType { get; set; }
    }
}
