using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.ViewModels
{
    public class OrdersViewModel
    {
        // from AdmitOrder
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

        // from VisitType
        public string Name { get; set; }
        public string Description { get; set; }

        // Search field added
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public VisitType VisitType { get; set; }
        public Department Department { get; set; }
        public IEnumerable<VisitType> VisitTypes { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public Encounter Encounter {get; set;}
        public AdmitOrder AdmitOrder {get; set;}

        public OrdersViewModel(){}
        public OrdersViewModel(Encounter encounter)
        {
            this.Encounter = encounter;
        }
        public OrdersViewModel(Encounter encounter, AdmitOrder admitOrder)
        {
            this.Encounter = encounter;
            this.AdmitOrder = admitOrder;
            AdmitOrderId = AdmitOrder.AdmitOrderId;
            EncounterId = AdmitOrder.EncounterId;
            VisitTypeId = AdmitOrder.VisitTypeId;
            DepartmentId = AdmitOrder.DepartmentId;
            OrderDateTime = AdmitOrder.OrderDateTime;
            OrderingPhysicianId = AdmitOrder.OrderingPhysicianId;
            CoOrderingPhysicianId = AdmitOrder.CoOrderingPhysicianId;
            IsOrderComplete = AdmitOrder.IsOrderComplete;
            OrderCompletedDateTime = AdmitOrder.OrderCompletedDateTime;
            OrderCompletedByPhysicianId = AdmitOrder.OrderCompletedByPhysicianId;
            Notes = AdmitOrder.Notes;
        }
    }
}
//