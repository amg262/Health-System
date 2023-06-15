using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.Services
{
    public interface IMedicineService
    {
        Task<IEnumerable<GenericMedicine>> GetAllMedicationsAsync();
    }
}