using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using IS_Proj_HIT.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT.Services
{
    public class BodySystemsService : IBodySystemsService
    {
        private WCTCHealthSystemContext _context;

        public BodySystemsService(WCTCHealthSystemContext context) => _context = context;
        
        //There are enums for the BodySystemTypes for each kind of assessment, which these lists reflect, so the BodySystemAssessments for the PhysicianAssessment can be created with the corresponding types.

        private IEnumerable<int> reviewOfSystemTypes = Enum
            .GetValues(typeof(ReviewOfSystemsBodySystemTypes))
            .OfType<ReviewOfSystemsBodySystemTypes>()
            .Cast<int>();
    
        private IEnumerable<int> physicalExamTypes = Enum
            .GetValues(typeof(PhysicalExamBodySystemTypes))
            .OfType<PhysicalExamBodySystemTypes>()
            .Cast<int>();

        /// <summary>
        ///     Gets a list of all BodySystemTypes for the Review Of Systems section of the PhysicianAssessments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BodySystemType>> GetReviewOfSystemsBodySystemTypeAsync() =>
            await _context.BodySystemTypes
                .Where(x => reviewOfSystemTypes.Contains(x.BodySystemTypeId))
                .ToListAsync();


        /// <summary>
        ///     Gets a list of all BodySystemTypes for the Physical Exam section of the PhysicianAssessments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BodySystemType>> GetPhysicalExamBodySystemTypeAsync() =>
            await _context.BodySystemTypes
                .Where(x => physicalExamTypes.Contains(x.BodySystemTypeId))
                .ToListAsync();

        /// <summary>
        ///     Gets a dictionary containing all BodySystemAssessments for a PhysicianAssessment, with the Id as the key and the BodySystemAssessment as the value
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<short, BodySystemAssessment>> GetBodySystemAssessmentsForNewPhysicianAssessment()
        {
            Dictionary<short, BodySystemAssessment> bodySystemAssessments = new Dictionary<short, BodySystemAssessment>();
            var reviewOfSystemsBodySystemTypes = await GetReviewOfSystemsBodySystemTypeAsync();
            var physicalExamBodySystemTypes = await GetPhysicalExamBodySystemTypeAsync();

            foreach (var system in reviewOfSystemsBodySystemTypes)
            {
                bodySystemAssessments.Add(system.BodySystemTypeId, new BodySystemAssessment
                {
                    BodySystemTypeId = system.BodySystemTypeId,
                    BodySystemType = system
                });
            }

            foreach (var system in physicalExamBodySystemTypes)
            {
                bodySystemAssessments.Add(system.BodySystemTypeId, new BodySystemAssessment
                {
                    BodySystemTypeId = system.BodySystemTypeId,
                    BodySystemType = system
                });
            }

            return bodySystemAssessments;
        }
    }
}