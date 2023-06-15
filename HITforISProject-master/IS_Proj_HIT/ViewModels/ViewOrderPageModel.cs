using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.ViewModels
{
    public class ViewOrderPageModel
    {
        public AdmitOrder Order { get; set; }
        public Patient Patient { get; set; }
        public Encounter Encounter {get; set;}

        // General Constructors
        public ViewOrderPageModel()
        { }
        public ViewOrderPageModel(AdmitOrder order, Patient patient)
        {
            this.Order = order;
            this.Patient = patient;
        }

        // Constructor for order page
        public ViewOrderPageModel(AdmitOrder order, Patient patient, Encounter encounter)
        {
            this.Order = order;
            this.Patient = patient;
            this.Encounter = encounter;
        }
    }
}