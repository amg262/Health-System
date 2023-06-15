﻿using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Services
{
    class EncounterService : IEncounterService
    {
        private WCTCHealthSystemContext _context;

        public EncounterService(WCTCHealthSystemContext context) => _context = context;

        /// <summary>
        ///     Gets the Encounter for a given Id
        /// </summary>
        /// <param name="encounterId"></param>
        /// <returns></returns>
        public async Task<Encounter> GetEncounterByIdAsync(long encounterId) =>
            await _context.Encounters
                .FirstOrDefaultAsync(x => x.EncounterId == encounterId);
    }
}