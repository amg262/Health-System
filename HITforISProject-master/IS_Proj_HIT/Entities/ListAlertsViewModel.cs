﻿using System;
using System.Collections.Generic;
using IS_Proj_HIT.Models.ViewModels;

namespace IS_Proj_HIT.Entities
{
    public class ListAlertsViewModel
    {
        public IEnumerable<PatientAlert> PatientAlerts { get; set; }
        public IEnumerable<AlertType> AlertType { get; set; }

        //public IEnumerable<PatientFallRisks> FallRisk { get; set; }
        public string Mrn { get; set; }

        public int AlertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
