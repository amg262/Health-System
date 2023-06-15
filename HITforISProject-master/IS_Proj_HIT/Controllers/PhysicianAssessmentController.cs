 using System;
 using System.Collections.Generic;
 using System.Threading.Tasks;
 using IS_Proj_HIT.Entities;
 using IS_Proj_HIT.Services;
 using IS_Proj_HIT.ViewModels;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Mvc;

 namespace IS_Proj_HIT.Controllers
 {
     public class PhysicianAssessmentController : Controller
     {
         private readonly IPhysicianAssessmentService _physicianAssessmentService;
         private readonly IBodySystemsService _bodySystemsService;
         private readonly IEncounterService _encounterService;
         private readonly IPhysicianService _physicianService;
         private readonly IAllergenService _allergenService;
         private readonly IMedicineService _medicineService;
         private readonly IPatientService _patientService;
         private readonly IUserService _userService;
         private readonly UserManager<IdentityUser> _userManager;
         
         /*
          * These services are injected in the startup file. I added them so common methods can be reused between controllers, since most of the controllers seem to have a lot of business logic in them. This way, instead of piling everything in the controller, the injectable service can handle the business logic and similar methods can live together and be reused in other controllers/services as needed.
          */
         public PhysicianAssessmentController(
             IPhysicianAssessmentService physicianAssessmentPhysicianAssessmentService, 
             IBodySystemsService bodySystemsService,
             IEncounterService encounterService,
             IPhysicianService physicianService,
             IAllergenService allergenService,
             IMedicineService medicineService,
             IPatientService patientService,
             IUserService userService,
             UserManager<IdentityUser> userManager
         )
         {
             _physicianAssessmentService = physicianAssessmentPhysicianAssessmentService;
             _bodySystemsService = bodySystemsService;
             _encounterService = encounterService;
             _physicianService = physicianService;
             _allergenService = allergenService;
             _medicineService = medicineService;
             _patientService = patientService;
             _userService = userService;
             _userManager = userManager;
         }

         /// <summary>
         ///    View render model for History & Physical page
         /// </summary>
         /// <param name="id">The encounter Id to generate a History & Physical form for</param>
         /// <returns></returns>
         public async Task<IActionResult> HistoryAndPhysical(long id)
         {
             PhysicianAssessmentViewModel viewModel;
             
             var encounter = await _encounterService.GetEncounterByIdAsync(id);
             var patient = encounter is not null
                 ? await _patientService.GetPatientByMrnAsync(encounter.Mrn)
                 : new Patient();
             var possibleAllergies = await _allergenService.GetAllAllergensAsync();
             var reviewOfSystemsBodySystemTypes = await _bodySystemsService.GetReviewOfSystemsBodySystemTypeAsync();
             var physicalExamBodySystemTypes = await _bodySystemsService.GetPhysicalExamBodySystemTypeAsync();
             var providers = await _physicianService.GetAllProvidersAsync();
             var physicians = await _physicianService.GetAllProvidersNotRequiringCosignersAsync();
             var physicianAssessment =
                 await _physicianAssessmentService.GetHistoryAndPhysicalForEncounterAsync(id);
             
             if (physicianAssessment is not null)
             {
                 viewModel =
                     _physicianAssessmentService.MapPhysicianAssessmentToPhysicianAssessmentViewModel(
                         physicianAssessment);
             }
             else
             {
                 physicianAssessment = new PhysicianAssessment();
                 viewModel = new PhysicianAssessmentViewModel();
                 viewModel.BodySystemAssessments =
                     await _bodySystemsService.GetBodySystemAssessmentsForNewPhysicianAssessment();
                 viewModel.EncounterId = id;
             }

             viewModel.Encounter = encounter;
             viewModel.Patient = patient;
             viewModel.PossibleAllergies = possibleAllergies;
             viewModel.ReviewOfSystemsBodySystemTypes = reviewOfSystemsBodySystemTypes;
             viewModel.PhysicalExamBodySystemTypes = physicalExamBodySystemTypes;
             viewModel.Providers = providers;
             viewModel.Physicians = physicians;

             return View(viewModel);
         }

         /// <summary>
         ///    Post method to submit History & Physical form 
         /// </summary>
         /// <param name="model">The ViewModel for the PhysicianAssessment</param>
         /// <returns></returns>
         [HttpPost]
         [ValidateAntiForgeryToken]
         [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, Physician")]
         public async Task<IActionResult> SavePhysicianAssessment(PhysicianAssessmentViewModel model)
         {
             //TODO: Finish this post method, so PhysicianAssessments and all related data are saved properly on form submit. Right now, it does save the PhysicianAssessment to the database, but the logic for saving the associated BodySystemAssessments will need to be fixed, so those save properly. I had based this off of the SystemAssessments in the PCA form, but had to make a couple of changes and ran out of time before I could get it working completely. The PCA controller would be a good starting point to find similar logic to properly save the assessments. Once the medication search is complete, that will need to be saved too.
             
             var physicianAssessment =
                 await _physicianAssessmentService.GetPhysicianAssessmentByIdAsync(model.PhysicianAssessmentId) ?? new PhysicianAssessment();
             
             _physicianAssessmentService.MapViewModelToPhysicianAssessment(model, physicianAssessment);
             
             physicianAssessment.WrittenDateTime ??= DateTime.Now;
             physicianAssessment.LastUpdated = DateTime.Now;

             physicianAssessment.EditedBy = await _userService.GetUserIdByAspNetUserIdAsync(_userManager.GetUserId(User));
             physicianAssessment.PhysicianAssessmentTypeId = 1;
             
             if (physicianAssessment.PhysicianAssessmentId > 0)
             {
                 await _physicianAssessmentService.UpdatePhysicianAssessment(physicianAssessment);
             }
             else
             {
                 await _physicianAssessmentService.SaveNewPhysicianAssessment(physicianAssessment);
             }

             return RedirectToAction("ViewEncounter", "Encounter", new { encounterId = physicianAssessment.EncounterId });
         }
         
         //TODO: Consultation form. Aside from one or two fields, it is more or less the same as History & Physical, so a lot of the code can be reused. 
     }
 }

