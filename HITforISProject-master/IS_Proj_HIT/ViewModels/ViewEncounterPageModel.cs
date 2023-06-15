using System.Linq;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.ViewModels
{
    public class ViewEncounterPageModel
    {
        public Encounter Encounter { get; set; }
        public Patient Patient { get; set; }
        public IQueryable<ProgressNote> ProgressNotes {get; set;}
        public PhysicianAssessment HistoryAndPhysical {get; set;}
        public Physician AuthoringProviderNavigation { get; set; }
        public int CoPhysicianId { get; set; }
        public int PhysicianId { get; set; }

       

        // General Constructors
        public ViewEncounterPageModel()
        { }
        public ViewEncounterPageModel(Encounter encounter, Patient patient)
        {
            this.Encounter = encounter;
            this.Patient = patient;
        }

        // Constructor for progress notes page
        public ViewEncounterPageModel(Encounter encounter, Patient patient, IQueryable<ProgressNote> progressNotes)
        {
            this.Encounter = encounter;
            this.Patient = patient;
            this.ProgressNotes = progressNotes;
        }
    }
}