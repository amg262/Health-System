using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;

namespace IS_Proj_HIT.Services

{
    public interface IPhysicianAssessmentService
    {
        Task<PhysicianAssessment> GetPhysicianAssessmentByIdAsync(long physicianAssessmentId);
        Task<PhysicianAssessment> GetHistoryAndPhysicalForEncounterAsync(long encounterId);

        PhysicianAssessmentViewModel MapPhysicianAssessmentToPhysicianAssessmentViewModel(
            PhysicianAssessment physicianAssessment);

        void MapViewModelToPhysicianAssessment(PhysicianAssessmentViewModel viewModel, PhysicianAssessment physicianAssessment);

        Task<int> SaveNewPhysicianAssessment(PhysicianAssessment newAssessment);

        Task<int> UpdatePhysicianAssessment(PhysicianAssessment assessmentToUpdate);
    }
}