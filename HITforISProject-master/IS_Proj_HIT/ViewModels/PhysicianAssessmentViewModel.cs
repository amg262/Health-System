using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.ViewModels
{
    public class PhysicianAssessmentViewModel
    {
        public long PhysicianAssessmentId { get; set; } = 0;

        [Display(Name = "Date")] 
        public DateTime PhysicianAssessmentDate { get; set; } = DateTime.Today;

        [Display(Name = "Chief Complaint")] 
        public string ChiefComplaint { get; set; } = "";

        [Display(Name = "History of Present Illness")]
        public string HistoryOfPresentIllness { get; set; } = "";

        [Display(Name = "Social History")] 
        public string SocialHistory { get; set; } = "";
        
        [Display(Name = "Family History")]
        public string FamilyHistory { get; set; } = "";
        
        [Display(Name = "Significant Diagnostic Tests")]
        public string SignificantDiagnosticTests { get; set; } = "";
        
        [Display(Name = "Assessment")]
        public string Assessment { get; set; } = "";
        
        [Display(Name = "Plan")]
        public string Plan { get; set; } = "";

        [Display(Name = "Provider")]
        public int AuthoringProviderID { get; set; }
        
        [Display(Name = "Co-signer")]
        public int? CoSignatureProviderId { get; set; }
        
        public long EncounterId { get; set; }

        public short PhysicianAssessmentTypeId { get; set; }
        
        public int? ReferringProviderId { get; set; }

        [Display(Name = "Allergies")]
        public ICollection<int> PhysicianAssessmentAllergies { get; set; }
        
        public ICollection<PhysicianAssessmentMedication> PhysicianAssessmentMedications { get; set; }
        
        public Encounter? Encounter { get; set; }
        
        public Patient? Patient { get; set; }

        public IEnumerable<Allergen> PossibleAllergies { get; set; }

        public IEnumerable<BodySystemType> ReviewOfSystemsBodySystemTypes { get; set; }
        
        public IEnumerable<BodySystemType> PhysicalExamBodySystemTypes { get; set; }

        public IEnumerable<Physician> Providers { get; set; }
        
        public IEnumerable<Physician> Physicians { get; set; }
        
        public Dictionary<short, BodySystemAssessment> BodySystemAssessments { get; set; }
    }
}