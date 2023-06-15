using System.Threading.Tasks;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.Services
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByMrnAsync(string mrn);
    }
}