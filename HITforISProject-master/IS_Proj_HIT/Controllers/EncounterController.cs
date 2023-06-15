using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IS_Proj_HIT.Services;
using System.Security.Claims;
using ProviderType = IS_Proj_HIT.Entities.Enum.ProviderType;

namespace IS_Proj_HIT.Controllers
{

    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, HIT Clerk, Nursing Student, Physician, Read Only, Registrar")]
    public class EncounterController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly WCTCHealthSystemContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        
        public int PageSize = 8;
        public EncounterController(IWCTCHealthSystemRepository repo, WCTCHealthSystemContext db, UserManager<IdentityUser> userManager) 
        {
            _repository = repo;
            _db = db;
            _userManager = userManager;
        } 

        /// <summary>
        /// Loads checked in encounters screen, filters by facility
        /// </summary>
        // Used in: Navbar (_Layout) and Home Page, ViewDischarge, ViewEncounter (if patient is still checked in?)
        public ViewResult CheckedIn()
        {
            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = _repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var facilities = _repository.Facilities;

            var isAdmin = User.IsInRole("Administrator");
            
            var model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null)
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));

            if (!isAdmin && currentUserFacility == null) {
                model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null && (e.FacilityId == 0))
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));

                ViewBag.ErrorMessage = "You do not currently have an assigned facility.";
            }

            if (!isAdmin && currentUserFacility != null) {

                model = _repository.Encounters
                .Where(e => e.DischargeDateTime == null && e.FacilityId == currentUserFacility.FacilityId)
                .OrderByDescending(e => e.AdmitDateTime)
                .Join(_repository.Patients,
                    e => e.Mrn,
                    p => p.Mrn,
                    (e, p) =>
                        new EncounterPatientViewModel(e.Mrn,
                            e.EncounterId,
                            e.AdmitDateTime,
                            p.FirstName,
                            p.LastName,
                            e.Facility.Name,
                            e.DischargeDateTime.ToString(),
                            e.RoomNumber));
            }

            return View(model);
        }

        /// <summary>
        /// View ProgressNotes page
        /// </summary>
        /// <param name="id">Id of unique encounter</param>
        // Used in: EncounterMenu
        public IActionResult ProgressNotes(long id, string sortOrder){
            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);
            
            

            ViewBag.EncounterId = id;
            // This is how alerts connects
            // This is how alerts connects
            ViewBag.Patient = _repository.Patients
            .Include(p => p.PatientAlerts)
            .FirstOrDefault(b => b.Mrn == desiredEncounter.Mrn);


            //sortable List
            ViewBag.PhysicianSortParm = sortOrder == "byPhysician" ? "byPhysicianDesc" : "byPhysician";
            ViewBag.CoPhysicianSortParm = sortOrder == "byCoPhysician" ? "byCoPhysicianDesc" : "byCoPhysician";
            ViewBag.DateSortParm = sortOrder == "byDate" ? "byDateDesc" : "byDate";
            ViewBag.NoteTypeSortParm = sortOrder == "byNoteType" ? "byNoteTypeDesc"   : "byNoteType";


            if (sortOrder == "byNoteType" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Note Type Ascending";
            }
            else if (sortOrder == "byNoteTypeDesc" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Note Type Descending";
            }
            else if (sortOrder == "byDate" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Date Ascending";
            }

            else if (sortOrder == "byDateDesc" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Date Descending";
            }
            else if (sortOrder == "byPhysician" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Physician Ascending";
            }

            else if (sortOrder == "byPhysicianDesc" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Physician Descending";
            }
            else if (sortOrder == "byCoPhysician" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by CoPhysician Ascending";
            }

            else if (sortOrder == "byCoPhysicianDesc" && _repository.ProgressNotes.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by CoPhysician Descending";
            }

            else
            {
                TempData["msg"] = "";
            }
            //takes the sort order and plugs in the correct sort into the model
            if(sortOrder == "byDate"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderBy(p => p.LastUpdated);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }
            if(sortOrder == "byDateDesc"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderByDescending(p => p.LastUpdated);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }

            if(sortOrder == "byPhysician"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderBy(p => p.Physician.LastName);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }
            if(sortOrder == "byPhysicianDesc"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderByDescending(p => p.Physician.LastName);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }

            if(sortOrder == "byCoPhysician"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderBy(p => p.CoPhysician.LastName);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }
            if(sortOrder == "byCoPhysicianDesc"){
                sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderByDescending(p => p.CoPhysician.LastName);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            }
            
            if(sortOrder == "byNoteType"){
                sortOrder = "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderBy(p => p.NoteType);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            } 

            if(sortOrder == "byNoteTypeDesc"){
                sortOrder = "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderByDescending(p => p.NoteType);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model);
            } else{
                    sortOrder= "";
                    var desiredProgressNotes = _repository.ProgressNotes
                        .Include(e => e.NoteType)
                        .Include(e => e.Physician)
                        .Include(e => e.CoPhysician)
                        .Where(u => u.EncounterId == id)
                        .OrderByDescending(p => p.LastUpdated);
                    var model = new ViewEncounterPageModel(desiredEncounter, desiredPatient, desiredProgressNotes);
                return View(model); 
            }
            
        }

        /// <summary>
        /// View a specific ProgressNote page
        ///</summary>
        /// <param name="id">Id of unique encounter</param>
        // Used in: ProgressNote 
        public IActionResult ViewProgressNote(long id, long pId){ 
            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);

            ViewBag.StartDate = _repository.ProgressNotes.FirstOrDefault(p => p.ProgressNoteId == pId).WrittenDate;

            var desiredProgressNote = _repository.ProgressNotes
                .Include(e => e.NoteType)
                .Include(e => e.Physician)
                .Include(e => e.CoPhysician)
                .Where(u => u.EncounterId == id)
                .FirstOrDefault(u => u.ProgressNoteId == pId);

            ViewBag.Encounter = desiredEncounter;
            ViewBag.Patient = desiredPatient;

            var model = new ProgressNotesViewModel(desiredEncounter, desiredPatient, desiredProgressNote);

            return View(model);
        }

        /// <summary>
        /// View AddProgressNotes page
        /// 
        /// </summary>
        /// <param name="id">Id of unique encounter</param>
        // Used in: ProgressNotes
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Physician, Registrar")]
        public IActionResult AddProgressNotes(long id){
            
            
            ViewBag.EncounterId = id;

            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);

            ViewBag.Patient = desiredPatient;
            ViewBag.Encounter = desiredEncounter;


            var queryProvider = _repository.Physicians
                    .OrderBy(p => p.LastName)
                    .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
                    .ToList();

            ViewBag.Providers = new SelectList(queryProvider,  "PhysicianId", "LastName",  0);


            ////  Query for getting a specific provider type in the Physician table, might be useful for proivder sign off functionality
            // var queryPhysician = _repository.Physicians
            //         .Include(x => x.ProviderType)
            //         .Where(x => x.ProviderTypeId == (int)ProviderType.AttendingPhysician)
            //         .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
            //         .ToList();

            // ViewBag.Physicians = new SelectList(queryPhysician, "PhysicianId", "LastName", 0);        

            var queryNoteType = _repository.NoteTypes
                    .OrderBy(n => n.NoteTypeId)
                    .Select(n => new {n.NoteTypeId, n.NoteType1})
                    .ToList();

            ViewBag.NoteTypes = new SelectList(queryNoteType, "NoteTypeId", "NoteType1", 0);

            var model = new ProgressNotesViewModel();

            model.Encounter = desiredEncounter;
            model.Patient = desiredPatient;


            return View(model);
        }

        /// <summary>
        /// Create new progress note
        /// </summary>
        /// <param name="model">ProgressNotes model to be added to database</param>
        // Used in: AddProgressNotes
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, Registrar, HIT Faculty")]
        public IActionResult AddProgressNotes(ProgressNote model) {
            // validation goes here
            if (_repository.ProgressNotes.Any(p => p.ProgressNoteId == model.ProgressNoteId))
            {
                ModelState.AddModelError("", "Progress Note Id must be unique");
            }
            
            if(ModelState.IsValid) {
                
                model.LastUpdated = DateTime.Now;
                
            
                _repository.AddProgressNote(model);
           
             
            return RedirectToAction("ProgressNotes", new { id = model.EncounterId});
            }
        return View(model);
        }

         /// <summary>
        /// Deletes a progress Note, redirects to patients progress notes
        /// </summary>
        /// <param name="pId">Id of unique Progress note </param>
        /// <param name="id">Id of unique Encounter</param>
        // Used in: ProgressNotes
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteProgressNote(long id, long pId){

            bool exists = _repository.ProgressNotes.Any(p => p.ProgressNoteId == pId);

            if(exists){
                var progressnote = _repository.ProgressNotes.FirstOrDefault(p => p.ProgressNoteId == pId);
                _repository.DeleteProgressNote(progressnote);
            }
            return RedirectToAction("ProgressNotes", new { id = id});
        }    

        /// <summary>
        /// Shows the edit Progress Note page
        /// </summary>
        /// <param name="pId">Id of unique Progress note </param>
        /// <param name="id">Id of unique Encounter</param>
        public IActionResult EditProgressNote(long id, long pId){

            ViewBag.EncounterId = id;

            //passing the current progress note ID to the editprogressnote page
            ViewBag.ProgressNoteId = pId;

            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);

            ViewBag.Patient = desiredPatient;
            ViewBag.Encounter = desiredEncounter;

            // grabbing the original written date and saving it
            var desiredWrittenDate = _repository.ProgressNotes.Where(u => u.EncounterId == id).FirstOrDefault(u => u.ProgressNoteId == pId).WrittenDate;
            
            
            ViewBag.WrittenDate = desiredWrittenDate;

            var desiredProgressNote = _repository.ProgressNotes
                .Include(e => e.NoteType)
                .Include(e => e.Physician)
                .Include(e => e.CoPhysician)
                .Where(u => u.EncounterId == id)
                .FirstOrDefault(u => u.ProgressNoteId == pId);

            ViewBag.ProgressNotes = desiredProgressNote;
            
            //Queries for the select lists
            var queryPhysician = _repository.Physicians
                    .OrderBy(p => p.LastName)
                    .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
                    .ToList();

            ViewBag.Physicians = new SelectList(queryPhysician, "PhysicianId", "LastName", 0);

            var queryNoteType = _repository.NoteTypes
                    .OrderBy(n => n.NoteTypeId)
                    .Select(n => new {n.NoteTypeId, n.NoteType1})
                    .ToList();

            ViewBag.NoteTypes = new SelectList(queryNoteType, "NoteTypeId", "NoteType1", 0);

            

            var model = new ProgressNotesViewModel(desiredEncounter, desiredPatient, desiredProgressNote);


            return View(model);
        }

        /// <summary>
        /// Edit a progress note, redirects to patients progress notes
        /// </summary>
        /// <param name="pId">Id of unique Progress note </param>
        /// <param name="id">Id of unique Encounter</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProgressNotes(ProgressNote model) {

            var user = _db.UserTables
                .Where(x => x.AspNetUsersId == _userManager.GetUserId(User))
                .Select(x => x.UserId)
                .FirstOrDefault();

            model.EditedBy = user;
            
            if(!ModelState.IsValid) {
            return View(model);
            }
            
            
            _repository.EditProgressNote(model);

        return RedirectToAction("ProgressNotes", new { id = model.EncounterId});
        }


        /// <summary>
        /// View Discharge page
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        // Used in: EncounterMenu
        // May not currently work?
        public IActionResult ViewDischarge(long encounterId)
        {
            ViewData["ErrorMessage"] = "";

            var encounter = _repository.Encounters
                .Include(e => e.Facility)
                .Include(e => e.Department)
                .Include(e => e.AdmitType)
                // .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .Include(e => e.PlaceOfService)
                .Include(e => e.PointOfOrigin)
                .Include(e => e.DischargeDispositionNavigation)
                .Include(e => e.Pcarecords)
                .FirstOrDefault(b => b.EncounterId == encounterId);
            if (encounter is null)
                return RedirectToAction("CheckedIn");

            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);

            return View(new ViewDischargePageModel
            {
                Encounter = encounter,
                Patient = patient
            });
        }

        /// <summary>
        /// View page of a specific encounter
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        // Used in: PCAController, CheckedIn, EditEncounter (to return to view), HistoryAndPhysical (currently unused), PatientDetails, View/Create/UpdatePCAAssessment
        public IActionResult ViewEncounter(long encounterId)
        {
            ViewData["ErrorMessage"] = "";

            var id = User.Identity.Name;

            var encounter = _repository.Encounters
                .Include(e => e.Facility)
                .Include(e => e.Department)
                .Include(e => e.AdmitType)
                // .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .Include(e => e.PlaceOfService)
                .Include(e => e.PointOfOrigin)
                .Include(e => e.DischargeDispositionNavigation)
                .Include(e => e.Pcarecords)
                .FirstOrDefault(b => b.EncounterId == encounterId);

            if (encounter is null)
                return RedirectToAction("CheckedIn");

            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);

            return View(new ViewEncounterPageModel
            {
                Encounter = encounter,
                Patient   = patient
            });
        }

        /// <summary>
        /// View AddEncounter page
        /// </summary>
        /// <param name="id">Mrn of patient?</param>
        // Used in: PatientDetails
        [Authorize(Roles = "Administrator, Nursing Faculty, Registrar, HIT Faculty")]
        public IActionResult AddEncounter(string id)
        {
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == id);

            AddDropdowns();
            return View();
        }

        /// <summary>
        /// Deletes encounter, cannot delete if PCA records exist, redirects to PCA (there may be a better redirect here)
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        // Used in: CheckedIn, ViewEncounter
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteEncounter(long encounterId)
        {
            // Check for any PCAs created for this encounter
            bool usingExists = _repository.PcaRecords.Any(p => p.EncounterId == encounterId);
            if (usingExists)
            {
                Console.WriteLine("PCA records exist using this record.");
                ViewData["ErrorMessage"] = "PCA records exist using this record. Delete not available.";
                var model = _repository.Encounters
                    .Where(e => e.DischargeDateTime == null)
                    .OrderByDescending(e => e.AdmitDateTime)
                    .Join(_repository.Patients,
                        e => e.Mrn,
                        p => p.Mrn,
                        (e, p) =>
                            new EncounterPatientViewModel(e.Mrn,
                                e.EncounterId,
                                e.AdmitDateTime,
                                p.FirstName,
                                p.LastName,
                                e.Facility.Name,
                                e.DischargeDateTime.ToString(),
                                e.RoomNumber));

                return View("../Encounter/CheckedIn", model);
            }

            var encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);
            _repository.DeleteEncounter(encounter);
            return RedirectToAction("CheckedIn");
        }

        /// <summary>
        /// View EditEncounter page
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        // Used in: CheckedIn, ViewDischarge, ViewEncounter
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, Physician")]
        public IActionResult EditEncounter(long encounterId)
        {
            var encounter = _repository.Encounters
                .FirstOrDefault(b => b.EncounterId == encounterId);
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == encounter.Mrn);

            AddDropdowns();

            return View(encounter);
        }

        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, Physician")]
        public IActionResult DischargeEncounter(long encounterId)
        {
            var encounter = _repository.Encounters
                .FirstOrDefault(b => b.EncounterId == encounterId);
            ViewBag.Patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(b => b.Mrn == encounter.Mrn);

                AddDropdowns();
            
            return View(encounter);
        }

        /// <summary>
        /// Create new encounter
        /// </summary>
        /// <param name="model">Encounter model to be added to database</param>
        // Used in: AddEncounter
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult AddEncounter(Encounter model)
        {
            if (_repository.Encounters.Any(p => p.EncounterId == model.EncounterId))
            {
                ModelState.AddModelError("", "Encounter Id must be unique");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);
                AddDropdowns();

                return View(model);
            }
            
            model.AdmitDateTime = DateTime.Now;
            model.LastModified = DateTime.Now;
            _repository.AddEncounter(model);

            return RedirectToAction("ViewEncounter", "Encounter", new {encounterId = model.EncounterId});
        }

        /// <summary>
        /// Save edits to patient record from Edit Patients page
        /// </summary>
        /// <param name="model">Encounter model to be edited</param>
        // Used in: EditEncounter
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult EditEncounter(Encounter model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);

                AddDropdowns();

                return View(model);
            }

            model.LastModified = DateTime.Now;
            _repository.EditEncounter(model);
            return RedirectToAction("ViewEncounter",
                new {encounterId = model.EncounterId, allowCheckedInRedirect = true});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult DischargeEncounter(Encounter model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Patient = _repository.Patients
                    .Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);

                AddDropdowns();

                return View(model);
            }

            Encounter tempEncounter = _repository.Encounters.FirstOrDefault(e=> e.EncounterId == model.EncounterId);
            tempEncounter.DischargeDateTime=model.DischargeDateTime;
            tempEncounter.DischargeComment= model.DischargeComment;
            tempEncounter.DischargeDisposition = model.DischargeDisposition;

            tempEncounter.LastModified = DateTime.Now;
            _repository.EditEncounter(tempEncounter);
            return RedirectToAction("ViewEncounter",
                new {encounterId = tempEncounter.EncounterId, allowCheckedInRedirect = true});
        }

      


        /// <summary>
        /// Add dropdowns to encounter views, only user's facility(ies) can be selected. Controller method to display dropdowns
        /// </summary>
        private void AddDropdowns()
        {
            var queryAdmitTypes = _repository.AdmitTypes
                .OrderBy(n => n.Description)
                .Select(n => new {n.AdmitTypeId, n.Description})
                .ToList();
            ViewBag.AdmitTypes = new SelectList(queryAdmitTypes, "AdmitTypeId", "Description", 0);

            var queryDepartments = _repository.Departments
                .OrderBy(n => n.Name)
                .Select(dep => new {dep.DepartmentId, dep.Name})
                .ToList();
            ViewBag.Departments = new SelectList(queryDepartments, "DepartmentId", "Name", 0);
            
            var queryDischarges = _repository.Discharges
                .OrderBy(n => n.Name)
                .Select(dis => new {dis.DischargeId, dis.Name})
                .ToList();
            ViewBag.Discharges = new SelectList(queryDischarges, "DischargeId", "Name", 0);

            var queryEncounterTypes = _repository.EncounterTypes
                .OrderBy(n => n.Name)
                .Select(ent => new {ent.EncounterTypeId, ent.Name})
                .ToList();
            ViewBag.EncounterTypes = new SelectList(queryEncounterTypes, "EncounterTypeId", "Name", 0);

            var queryPlacesOfService = _repository.PlaceOfService
                .OrderBy(n => n.Description)
                .Select(pos => new {pos.PlaceOfServiceId, pos.Description})
                .ToList();
            ViewBag.PlacesOfService = new SelectList(queryPlacesOfService, "PlaceOfServiceId", "Description", 0);

            var queryPointsOfOrigin = _repository.PointOfOrigin
                .OrderBy(n => n.Description)
                .Select(poo => new {poo.PointOfOriginId, poo.Description})
                .ToList();
            ViewBag.PointsOfOrigin = new SelectList(queryPointsOfOrigin, "PointOfOriginId", "Description", 0);

            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = _repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var isAdmin = User.IsInRole("Administrator");
            var queryFacility = _repository.Facilities
                    .OrderBy(n => n.Name)
                    .Select(fac => new {fac.FacilityId, fac.Name})
                    .ToList();
            var facilities = _repository.Facilities;

            if (!isAdmin && currentUserFacility == null) {
                queryFacility = _repository.Facilities
                    .Where(e => e.FacilityId == 0)
                    .OrderBy(n => n.Name)
                    .Select(fac => new {fac.FacilityId, fac.Name})
                    .ToList();

                ViewBag.ErrorMessage = "You do not currently have an assigned facility.";
            }

            if (!isAdmin && currentUserFacility != null) {

                queryFacility = _repository.Facilities
                    .Where(e => e.FacilityId == currentUserFacility.FacilityId)
                    .OrderBy(n => n.Name)
                    .Select(fac => new {fac.FacilityId, fac.Name})
                    .ToList();
            }

            ViewBag.Facility = new SelectList(queryFacility, "FacilityId", "Name", 0);

            var queryEncounterPhysicians = _repository.EncounterPhysicians
                .OrderBy(n => n.Physician.LastName)
                .Select(p => new {p.EncounterPhysiciansId, Name = $"{p.Physician.FirstName} {p.Physician.LastName}"})
                .ToList();
            ViewBag.EncounterPhysicians = new SelectList(queryEncounterPhysicians, "EncounterPhysiciansId", "Name", 0);
        }

        /// <summary>
        /// View PCAIndex page from Encounter Menu
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        public IActionResult PCAIndex(long encounterId)
        {
            ViewData["ErrorMessage"] = "";

            var id = User.Identity.Name;

            var encounter = _repository.Encounters
                .Include(e => e.Facility)
                .Include(e => e.Department)
                .Include(e => e.AdmitType)
                // .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .Include(e => e.PlaceOfService)
                .Include(e => e.PointOfOrigin)
                .Include(e => e.DischargeDispositionNavigation)
                .Include(e => e.Pcarecords)
                .FirstOrDefault(b => b.EncounterId == encounterId);
            if (encounter is null) {
                ViewData["ErrorMessage"] = "No PCA to display.";
                return RedirectToAction("Error");
            }
            
            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);
            

            return View(new ViewEncounterPageModel
            {
                Encounter = encounter,
                Patient   = patient
            });
        }

        /// <summary>
        /// Add physician assessment (ex. History and Physical, Consultation, etc.) Currently doesn't work and only set up for History and Physical
        /// </summary>
        /// <param name="model">PhysicianAssessment model to be added to database</param>
        // NO CURRENT FUNCTION 
        [Authorize(Roles = "Administrator, Nursing Faculty, Registrar, HIT Faculty, Physician")]
        public IActionResult AddPhysicianAssessment(PhysicianAssessment model)
        {
            var assessments = _repository.PhysicianAssessments;

            var paID = assessments.OrderByDescending(u => u.PhysicianAssessmentId).FirstOrDefault();
            model.PhysicianAssessmentId = paID.PhysicianAssessmentId + 1;

            // validation goes here

            model.WrittenDateTime = DateTime.Now;
            model.LastUpdated = DateTime.Now;

            // for testing:
            // Console.WriteLine("ASSESSMENT: " + model.PhysicianAssessmentId + ", " + model.PhysicianAssessmentDate + ", " + model.ChiefComplaint + ", " + model.SignificantDiagnosticTests + ", " + model.Assessment + ", " + model.Plan + ", " + model.AuthoringProvider + ", " + model.CoSignature + ", " + model.EncounterId + ", " + model.WrittenDateTime + ", " + model.LastUpdated);

            //_repository.AddPhysicianAssessment(model);

            return RedirectToAction("ViewEncounter",
                new {encounterId = model.EncounterId, allowCheckedInRedirect = true});
        }

        /// <summary>
        /// Add Physician report. No current functionality. Difference between PhysicianReport and PhysicianAssessment is unclear
        /// </summary>
        public IActionResult AddPhysicianReport()
        {
            throw new NotImplementedException();
        }

        
        /// <summary>
        /// View Physician Discharge Notes page
        /// </summary>
        /// <param name="id">Id of unique encounter</param>
        // Used in:  Encounter Menu
        public IActionResult ViewPhysicianDischarge(long id)
        {
            var desiredPatientEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.FirstOrDefault(u => u.Mrn == desiredPatientEncounter.Mrn);

            ViewEncounterPageModel model = new ViewEncounterPageModel(desiredPatientEncounter,desiredPatient){};

            return View(model);

        }



         /// <summary>
        /// View Physician Discharge Edit Notes page
        /// </summary>
        /// <param name="id">Id of unique encounter</param>
        // Used in:  ViewPhysicianDischarge
         public IActionResult PhysicianDischargeEdit(long id)
        {
            var desiredPatientEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.FirstOrDefault(u => u.Mrn == desiredPatientEncounter.Mrn);

             var queryPhysician = _repository.Physicians
                    .OrderBy(p => p.LastName)
                    .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
                    .ToList();

            ViewBag.Physicians = new SelectList(queryPhysician, "PhysicianId", "LastName", 0);

            ViewEncounterPageModel model = new ViewEncounterPageModel(desiredPatientEncounter,desiredPatient){};

            return View(model);

        }

        //Currently Physician Discharge Edit Page does not save data to the database.

        // /// <summary>
        // /// Save edits to patient record from Physician Discharge Edit page
        // /// </summary>
        // /// <param name="model">Encounter model to be edited to allow user to post data to the encounter table</param>
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        // public IActionResult PhysicianDischargeEdit()
        // {
            
        // }
    }
}