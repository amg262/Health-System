using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.EntityFrameworkCore;
using ProviderType = IS_Proj_HIT.Entities.Enum.ProviderType;

namespace IS_Proj_HIT.Services
{
    class PhysicianService : IPhysicianService
    {
        private WCTCHealthSystemContext _context;

        public PhysicianService(WCTCHealthSystemContext context) => _context = context;

        /// <summary>
        ///     Gets an enumerable with all possible providers from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Physician>> GetAllProvidersAsync() =>
            await _context.Physicians
                .Include(x => x.ProviderType)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();

        /// <summary>
        ///     Gets a list of all providers that do not require a cosigner
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Physician>> GetAllProvidersNotRequiringCosignersAsync() => 
            await _context.Physicians
                .Include(x => x.ProviderType)
                .Where(x => x.ProviderType.CosignRequired == false)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();
    }
}