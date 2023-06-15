using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Entities
{
    public class ListOrdersViewModel
    {
        public IEnumerable<AdmitOrder> Orders { get; set; }
        public IEnumerable<VisitType> VisitType { get; set; }

        //public IEnumerable<PatientFallRisks> FallRisk { get; set; }
        public string Mrn { get; set; }

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
    }
}
