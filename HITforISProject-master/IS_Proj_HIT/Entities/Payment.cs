﻿using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            Encounters = new HashSet<Encounter>();
        }

        public long PaymentId { get; set; }
        public int PrimaryInsuranceId { get; set; }
        public int SecondaryInsuranceId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Insurance PrimaryInsurance { get; set; }
        public virtual Insurance SecondaryInsurance { get; set; }
        public virtual ICollection<Encounter> Encounters { get; set; }
    }
}
