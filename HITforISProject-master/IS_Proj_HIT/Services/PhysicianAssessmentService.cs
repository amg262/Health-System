using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IS_Proj_HIT.Services
{
    public class PhysicianAssessmentService : IPhysicianAssessmentService
    {
        private WCTCHealthSystemContext _context;

        public PhysicianAssessmentService(WCTCHealthSystemContext context) => _context = context;

        /// <summary>
        ///     Gets a PhysicianAssessment matching a given Id
        /// </summary>
        /// <param name="physicianAssessmentId">The Id of the PhysicianAssessment to find</param>
        /// <returns></returns>
        public async Task<PhysicianAssessment> GetPhysicianAssessmentByIdAsync(long physicianAssessmentId) =>
            await _context.PhysicianAssessments
                .FirstOrDefaultAsync(x => x.PhysicianAssessmentId == physicianAssessmentId);

        /// <summary>
        ///     Gets the History & Physical PhysicianAssessment for a given encounter
        /// </summary>
        /// <param name="encounterId">The Id of the encounter to find the History & Physical for</param>
        /// <returns></returns>
        public async Task<PhysicianAssessment> GetHistoryAndPhysicalForEncounterAsync(long encounterId) =>
            await _context.PhysicianAssessments
                .Where(x => x.EncounterId == encounterId && x.PhysicianAssessmentTypeId == 1)
                .FirstOrDefaultAsync();

        /// <summary>
        ///     Creates a PhysicianAssessmentViewModel, matching properties from a given PhysicianAssessment
        /// </summary>
        /// <param name="physicianAssessment">The PhysicianAssessment to map from</param>
        /// <returns></returns>
        public PhysicianAssessmentViewModel MapPhysicianAssessmentToPhysicianAssessmentViewModel(
            PhysicianAssessment physicianAssessment)
        {
            return new PhysicianAssessmentViewModel
            {
                PhysicianAssessmentId = physicianAssessment.PhysicianAssessmentId,
                PhysicianAssessmentDate = physicianAssessment.PhysicianAssessmentDate ?? DateTime.Today,
                ChiefComplaint = physicianAssessment.ChiefComplaint,
                HistoryOfPresentIllness = physicianAssessment.HistoryOfPresentIllness,
                SocialHistory = physicianAssessment.SocialHistory,
                FamilyHistory = physicianAssessment.FamilyHistory,
                SignificantDiagnosticTests = physicianAssessment.SignificantDiagnosticTests,
                Assessment = physicianAssessment.Assessment,
                Plan = physicianAssessment.Plan,
                AuthoringProviderID = physicianAssessment.AuthoringProvider,
                CoSignatureProviderId = physicianAssessment.CoSignature,
                EncounterId = physicianAssessment.EncounterId,
                PhysicianAssessmentTypeId = physicianAssessment.PhysicianAssessmentTypeId,
                ReferringProviderId = physicianAssessment.ReferringProvider,
                BodySystemAssessments = physicianAssessment.BodySystemAssessments.ToDictionary(x => x.BodySystemTypeId ?? 0),
                PhysicianAssessmentAllergies = physicianAssessment.PhysicianAssessmentAllergies.Select(x => x.AllergenId).ToList(),
                PhysicianAssessmentMedications = physicianAssessment.PhysicianAssessmentMedications
            };
        }

        /// <summary>
        ///     Maps properties from a PhysicianAssessmentViewModel to a PhysicianAssessment
        /// </summary>
        /// <param name="viewModel">The ViewModel to map from</param>
        /// <param name="physicianAssessment">The PhysicianAssessment to map to</param>
        public void MapViewModelToPhysicianAssessment(PhysicianAssessmentViewModel viewModel, PhysicianAssessment physicianAssessment)
        {
            physicianAssessment.PhysicianAssessmentId = viewModel.PhysicianAssessmentId;
            physicianAssessment.EncounterId = viewModel.EncounterId;
            physicianAssessment.PhysicianAssessmentDate = viewModel.PhysicianAssessmentDate;
            physicianAssessment.PhysicianAssessmentTypeId = viewModel.PhysicianAssessmentTypeId;
            physicianAssessment.ReferringProvider = viewModel.ReferringProviderId;
            physicianAssessment.ChiefComplaint = viewModel.ChiefComplaint;
            physicianAssessment.HistoryOfPresentIllness = viewModel.HistoryOfPresentIllness;
            physicianAssessment.SocialHistory = viewModel.SocialHistory;
            physicianAssessment.FamilyHistory = viewModel.FamilyHistory;
            physicianAssessment.SignificantDiagnosticTests = viewModel.SignificantDiagnosticTests;
            physicianAssessment.Assessment = viewModel.Assessment;
            physicianAssessment.Plan = viewModel.Plan;
            physicianAssessment.CoSignature = viewModel.CoSignatureProviderId;
            physicianAssessment.AuthoringProvider = viewModel.AuthoringProviderID;
            physicianAssessment.WrittenDateTime = DateTime.Now;
            physicianAssessment.BodySystemAssessments = viewModel.BodySystemAssessments.Values;
            
            IList<PhysicianAssessmentAllergy> physicianAssessmentAllergies = new List<PhysicianAssessmentAllergy>();
            foreach (var allergy in viewModel.PhysicianAssessmentAllergies)
            {
                physicianAssessmentAllergies.Add(new PhysicianAssessmentAllergy
                {
                    PhysicianAssessment = physicianAssessment,
                    AllergenId = allergy
                });
            }

            physicianAssessment.PhysicianAssessmentAllergies = physicianAssessmentAllergies;
            physicianAssessment.PhysicianAssessmentMedications = viewModel.PhysicianAssessmentMedications;
        }

        /// <summary>
        ///     Saves a new PhysicianAssessment to the database
        /// </summary>
        /// <param name="newAssessment"></param>
        /// <returns></returns>
        public async Task<int> SaveNewPhysicianAssessment(PhysicianAssessment newAssessment)
        {
            _context.Add(newAssessment);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Updates an existing PhysicianAssessment
        /// </summary>
        /// <param name="assessmentToUpdate"></param>
        /// <returns></returns>
        public async Task<int> UpdatePhysicianAssessment(PhysicianAssessment assessmentToUpdate)
        {
            _context.Update(assessmentToUpdate);
            return await _context.SaveChangesAsync();
        }
    }
}