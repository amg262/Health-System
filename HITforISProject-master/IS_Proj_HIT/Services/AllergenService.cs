using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Services
{
    class AllergenService : IAllergenService
    {
        private WCTCHealthSystemContext _context;

        public AllergenService(WCTCHealthSystemContext context) => _context = context;
    
        /// <summary>
        ///     Gets a list of all possible allergens
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Allergen>> GetAllAllergensAsync() =>
            await _context.Allergens
                .ToListAsync();
    }
}