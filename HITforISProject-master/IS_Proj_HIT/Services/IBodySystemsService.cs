using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.Services
{
    public interface IBodySystemsService
    {
        public Task<IEnumerable<BodySystemType>> GetReviewOfSystemsBodySystemTypeAsync();

        public Task<IEnumerable<BodySystemType>> GetPhysicalExamBodySystemTypeAsync();

        public Task<Dictionary<short, BodySystemAssessment>> GetBodySystemAssessmentsForNewPhysicianAssessment();
    }
}