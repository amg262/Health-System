using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Services
{
    class MedicineService : IMedicineService
    {
        private WCTCHealthSystemContext _context;

        public MedicineService(WCTCHealthSystemContext context) => _context = context;
    
        /* Not currently used anywhere as there are too many medications in the database for it to be feasible to use a list of all of them */
        
        /// <summary>
        ///     Gets an enumerable of all medications from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GenericMedicine>> GetAllMedicationsAsync() =>
            await _context.GenericMedicines
                .ToListAsync();
    }
}