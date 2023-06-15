using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.Services
{
    public interface IPhysicianService
    {
        Task<IEnumerable<Physician>> GetAllProvidersAsync();

        Task<IEnumerable<Physician>> GetAllProvidersNotRequiringCosignersAsync();
    }
}