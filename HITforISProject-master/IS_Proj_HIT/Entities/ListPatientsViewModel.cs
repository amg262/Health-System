using System.Collections.Generic;
using IS_Proj_HIT.Models.ViewModels;

namespace IS_Proj_HIT.Entities
{
    public class ListPatientsViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
        public int ReligionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Religion> Religions { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public Patient patientvm { get; set; }
        public Religion religionvm { get; set; }

    }
}
