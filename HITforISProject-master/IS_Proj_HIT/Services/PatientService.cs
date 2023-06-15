using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Services
{
    class PatientService : IPatientService
    {
        private WCTCHealthSystemContext _context;

        public PatientService(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Gets a patient for a given Mrn (PatientId)
        /// </summary>
        /// <param name="mrn"></param>
        /// <returns></returns>
        public async Task<Patient> GetPatientByMrnAsync(string mrn) =>
            await _context.Patients
                .Where(x => x.Mrn == mrn)
                .FirstOrDefaultAsync();
    }
}