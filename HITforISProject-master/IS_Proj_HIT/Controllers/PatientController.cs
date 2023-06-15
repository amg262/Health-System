﻿using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.Extensions.Primitives;


namespace IS_Proj_HIT.Controllers
{
    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar, HIT Clerk, Nursing Student, Read Only, Physician")]
    public class PatientController : Controller
    {
        private IWCTCHealthSystemRepository repository;
        public int PageSize = 10;
        public PatientController(IWCTCHealthSystemRepository repo) => repository = repo;

        /// <summary>
        /// Displays list of patients found via patient search
        /// </summary>
        /// <param name="searchLast">Last name to search for</param>
        /// <param name="searchFirst">First name to search for</param>
        /// <param name="searchSSN">SSN...</param>
        /// <param name="searchMRN">MRN...</param>
        /// <param name="searchDOB">Date of birth...</param>
        /// <param name="searchDOBBefore">Date before date of birth...</param>
        /// <param name="sortOrder">Order to sort search results</param>
        /// <param name="pageNum">Current page number, may not be used?</param>
        // Used in: PatientSearch
        public ActionResult Index(string searchLast, string searchFirst, string searchSSN,
            string searchMRN, DateTime searchDOB, DateTime searchDOBBefore, string sortOrder, int pageNum = 0)
        {
            ViewData.ModelState.Clear();

            // Put in a wildcard if user didn't search on these fields
            searchLast ??= " ";
            searchFirst ??= " ";
            searchSSN ??= " ";
            searchMRN ??= " ";

            if (searchDOB.GetHashCode() == 0)
            {
                searchDOB = new DateTime(1898, 1, 1);
            }

            if (searchDOBBefore.GetHashCode() == 0)
            {
                searchDOBBefore = new DateTime(2030, 1, 1);
            }

            var patients = repository.Patients
                .Include(p => p.Religion)
                .Include(p => p.Gender)
                .Include(p => p.Ethnicity)
                .Include(p => p.MaritalStatus)
                .Where(p => p.LastName.Contains(searchLast)
                            && p.FirstName.Contains(searchFirst)
                            && p.Ssn.Contains(searchSSN)
                            && p.Mrn.Contains(searchMRN)
                            && p.Dob >= searchDOB
                            && p.Dob <= searchDOBBefore);

            var pArray = patients.ToArray();

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MrnSortParm = sortOrder == "mrn" ? "mrn_desc" : "mrn";
            ViewBag.DobSortParm = sortOrder == "dob" ? "dob_desc" : "dob";

            ViewBag.sortOrder = sortOrder;
            ViewBag.searchLast = searchLast;
            ViewBag.searchFirst = searchFirst;
            ViewBag.searchSSN = searchSSN;
            ViewBag.searchDOB = searchDOB;
            ViewBag.searchDOBBefore = searchDOBBefore;
                        
            int totalResults = pArray.Count();
            ViewBag.TotalResults = totalResults;
            int numberOfPages = (int)Math.Round((double)(totalResults / PageSize), 0, MidpointRounding.AwayFromZero);
            ViewBag.NumberOfPages = numberOfPages;
            var pagesArr = new List<int>{};

            if (pageNum < 0) {
                pageNum = 0;
            }
            if (pageNum > numberOfPages) {
                pageNum = numberOfPages;
            }

            if (totalResults % 10 == 0) {
                for (int i = 1; i < numberOfPages + 1; i++) {
                    pagesArr.Add(i);
                }
            } else {
                for (int i = 1; i <= numberOfPages + 1; i++) {
                    pagesArr.Add(i);
                }
            }
            
            ViewBag.PagesArray = pagesArr;
            ViewBag.pageNum = pageNum;
            ViewBag.currentPage = pageNum + 1;

            switch (sortOrder)
            {
                case "mrn":
                    patients = patients.OrderBy(p => p.Mrn)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    break;
                case "mrn_desc":
                    patients = patients.OrderByDescending(p => p.Mrn)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    break;
                case "dob":
                    patients = patients.OrderBy(p => p.Dob)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    break;
                case "dob_desc":
                    patients = patients.OrderByDescending(p => p.Dob)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    break;
                case "name_desc":
                    patients = patients.OrderByDescending(p => p.LastName)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    break;
                default:
                    patients = patients.OrderBy(p => p.LastName)/*.Skip(pageNum * PageSize).Take(PageSize)*/;
                    ViewBag.sortOrder = "name";
                    break;
            }

            //var model = await PagingList.CreateAsync(patients, PageSize, page);
            return View(new ListPatientsViewModel
            {
                Patients = patients
            });
            //return View(model);
        }
        
        /// <summary>
        /// Return PatientSearch view
        /// </summary>
        // Used in: Navbar (_Layout), Home Page, PatientSearchIndex, PatientIndex, AddPatient
        public IActionResult PatientSearch() => View();

        /// <summary>
        /// View AddPatient page
        /// </summary>
        // Used in: PatientSearchIndex, PatientContactDetails (Unused)
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty,Registrar, Physician")]
        public IActionResult AddPatient()
        {
            //Run stored procedure from SQL database to generate the MRN number
            using (var context = new WCTCHealthSystemContext())
            {
                //todo changed fromsql to fromsqlraw
                var data = context.Patients.FromSqlRaw("EXECUTE dbo.GetNextMRN").ToList();
                ViewBag.MRN = data.FirstOrDefault()?.Mrn;
            }



            AddDropdowns();

            return View();
        }

        /// <summary>
        /// Creates new patient
        /// </summary>
        /// <param name="model">Patient model to add to database</param>
        // Used in: AddPatient, AddPatientButton
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult AddPatient(Patient model)
        {

 
            if (ModelState.IsValid)
            {
                if (repository.Patients.Any(p => p.Mrn == model.Mrn))
                {
                    ModelState.AddModelError("", "MRN Id must be unique");
                }
                else
                {
                    var languages = Request.Form["language"];
                    if (!StringValues.IsNullOrEmpty(languages))
                    {
                        var languageId = short.Parse(languages[0]);
                        var query = repository.Languages.Where(lang => lang.LanguageId == languageId).ToList();

                        if (query.Any() && query != null)
                        {
                            model.PatientLanguages.Add(
                             new PatientLanguage
                             {
                                 LanguageId = query[0].LanguageId,
                                 Mrn = model.Mrn,
                                 IsPrimary = 1,
                                 LastModified = DateTime.Now

                             });
                        }
                    }
                

                    var races = Request.Form["race"];
                    if (!StringValues.IsNullOrEmpty(races))
                    {
                        var raceId = short.Parse(races[0]);
                        var selectedRace = repository.Races.FirstOrDefault(race => race.RaceId == raceId);
                        if (selectedRace != null)
                        {
                            model.PatientRaces.Add(
                               new PatientRace
                               {
                                   RaceId = selectedRace.RaceId,
                                   Mrn = model.Mrn,
                                   LastModified = DateTime.Now

                               });
                        }
                    }
                  
                    
                    repository.AddPatient(model);
                    TempData["msg"] = "A new patient was successfully created.";
                    string myUrl = "Details/" + model.Mrn;
                    return Redirect(myUrl);
                    //return RedirectToAction("Index");
                }
            }

            return View();
        }

        /// <summary>
        /// Deletes select patient
        /// </summary>
        /// <param name="id">Id of unique patient to delete</param>
        // Used in: PatientDetails
        [Authorize(Roles = "Administrator")]

        public IActionResult DeletePatient(string id)
        {
            ViewBag.PatientAlertExists = repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id);
            if (ViewBag.PatientAlertExists != null)
            {
                TempData["msg1"] = "You cannot delete a patient with patient alerts.";
                return RedirectToRoute(new
                {
                    controller = "Patient",
                    action = "Details",
                    ID = id
                });
            }
            else
            {
                TempData["msg1"] = "The selected patient was deleted.";
                repository.DeletePatient(repository.Patients.FirstOrDefault(b => b.Mrn == id));
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }

            //return RedirectToAction("Index", "Home");
        }



        /*
        /// <summary>
        this is the old code for the patient search
        thi is the old code for the patient search
        it is not used anymore
        still need to delete it
        far too much code to delete
        especially since it is not used
        but it is still here
        it is still here
        /// </summary

        https://www.youtube.com/watch?v=QX4j2mKl8uU
        
        */

        /// <summary>
        /// View EditPatient page
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Used in: PatientDetails
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult Edit(string id)
        {
            var model = repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == id);

            AddDropdowns(model);

            return View(model);
        }

        /// <summary>
        /// Save edits to patient
        /// </summary>
        /// <param name="modle">Patient model to edit</param>
        // Used in: EditPatient
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Registrar")]
        public IActionResult Edit(Patient model)
        {
            //if (!ModelState.IsValid) return View(model.Mrn);

            model.LastModified = DateTime.Now;

            var languageToChange = new PatientLanguage();  
            var languages = Request.Form["language"];
            if (!StringValues.IsNullOrEmpty(languages))
            {
                var languageId = short.Parse(languages[0]);
                var query = repository.Languages.Where(lang => lang.LanguageId == languageId).ToList();

                if (repository.PatientLanguages.Any(l => l.IsPrimary == 1 && l.Mrn == model.Mrn))
                {
                    languageToChange = repository.PatientLanguages.FirstOrDefault(pl => pl.Mrn == model.Mrn && pl.IsPrimary == 1);
                    if (languageId != languageToChange.LanguageId)
                    {
                        languageToChange.IsPrimary = 0;
                    }

                }

                if (query.Any() && query != null && languageToChange.LanguageId != languageId)
                {
                    repository.AddPatientLanguage(
                     new PatientLanguage
                     {
                         LanguageId = query[0].LanguageId,
                         Mrn = model.Mrn,
                         IsPrimary = 1,
                         LastModified = DateTime.Now

                     });
                }

            }



            if (repository.PatientRaces.Any(r => r.Mrn == model.Mrn))
            {
                var raceToChange = repository.PatientRaces.FirstOrDefault(pr => pr.Mrn == model.Mrn);
                repository.DeletePatientRace(raceToChange);

            }

            var races = Request.Form["race"];
            if (!StringValues.IsNullOrEmpty(races))
            { 
                var raceId = short.Parse(races[0]);
                var raceToAdd = repository.Races.FirstOrDefault(race => race.RaceId == raceId);

                if (raceToAdd != null)
                {
                    repository.AddPatientRace(
                       new PatientRace
                       {
                           RaceId = raceToAdd.RaceId,
                           Mrn = model.Mrn,
                           LastModified = DateTime.Now

                       });
                }
            }

            repository.EditPatient(model);
            return RedirectToAction("Details", new {id = model.Mrn});
        }

        /// <summary>
        /// Pick record to send to Details page, might still need to filter encounters by facility?
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Used in: EditPatient, PatientIndex, PatientBanner
        public IActionResult Details(string id)
        {
            var currentUser = repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            ViewBag.CurrentUserFacilityId = currentUserFacility.FacilityId;
            var facilities = repository.Facilities;

            var isAdmin = User.IsInRole("Administrator");
            ViewBag.IsAdmin = isAdmin;

            var model = repository.Patients
                .Include(p => p.PatientAlerts)
                .Include(p => p.Religion)
                .Include(p => p.MaritalStatus)
                .Include(p => p.Sex)
                .Include(p => p.Gender)
                .Include(p => p.Ethnicity)
                .Include(p => p.Encounters).ThenInclude(e => e.Facility)
                .FirstOrDefault(p => p.Mrn == id);

            var primaryLanguageQuery = repository.PatientLanguages.Where(l => l.Mrn == model.Mrn)
                .Join(repository.Languages, pl => pl.LanguageId, lang => lang.LanguageId, (pl, lang)
                => new
                {
                    lang.Name,
                    pl.IsPrimary

                }).ToList();

            if (primaryLanguageQuery.Any() && primaryLanguageQuery != null)
            {
               foreach(var l in primaryLanguageQuery)
                {
                    if (l.IsPrimary == 1)
                    {
                        ViewBag.PrimaryLanguage = l.Name.ToString();
                    }
                }
                
            }

            var patientRaceQuery = repository.PatientRaces.Where(r => r.Mrn == model.Mrn)
                .Join(repository.Races, pr => pr.RaceId, race => race.RaceId, (pr, race)
                => new
                {
                    race.Name

                }).ToList();

            if (patientRaceQuery.Any() && patientRaceQuery != null)
            {
                foreach(var race in patientRaceQuery)
                {
                    ViewBag.PatientRace = race.Name.ToString();
                }
            }

            return View(model);
        }

        /// <summary>
        /// View ListAlerts for selected MRN, includes sort orders
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        /// <param name="sortOrder">Order to sort alerts</param>
        // Used in: EditPatientAlert, ListAlerts, PatientBanner
        public IActionResult ListAlerts(string id, string sortOrder)
        {
            // Remember the user's original request
            ViewBag.ReturnUrl = Request.Headers["Referer"].ToString();

            //ViewBag.CommentSortParm = String.IsNullOrEmpty(sortOrder) ? "byComments" : "byCommentsDesc";
            ViewBag.StartSortParm = sortOrder == "byStartDate" ? "byStartDateDesc" : "byStartDate";
            ViewBag.ActiveSortParm = sortOrder == "byActive" ? "byActiveDesc" : "byActive";
            //ViewBag.LastModifiedTypeSortParm = sortOrder == "byLastModified" ? "byLastModified" : "";
            ViewBag.AlertTypeSortParm = sortOrder == "byAlertType" ? "byAlertTypeDesc" : "byAlertType";

            // Existing
            ViewBag.myMrn = id;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == id);


            if (sortOrder == "byAlertType" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Alert Type Ascending";
            }
            else if (sortOrder == "byAlertTypeDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Alert Type Descending";
            }
            else if (sortOrder == "byStartDate" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Start Date Ascending";
            }

            else if (sortOrder == "byStartDateDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Start Date Descending";
            }

            else if (sortOrder == "byActive" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Active Ascending";
            }

            else if (sortOrder == "byActiveDesc" && repository.PatientAlerts.Where(b => b.Mrn == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Active Descending";
            }

            else
            {
                TempData["msg"] = "";
            }


            if (sortOrder == "byStartDate")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderBy(p => p.StartDate)
                });
            }

            if (sortOrder == "byStartDateDesc")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.StartDate)
                });
            }

            if (sortOrder == "byAlertType")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderBy(p => p.AlertType.Name)
                });
            }

            if (sortOrder == "byAlertTypeDesc")
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.AlertType.Name)
                });
            }

            if (sortOrder == "byActive")
            {
                {
                    sortOrder = "";
                    return View(new ListAlertsViewModel
                    {
                        PatientAlerts = repository.PatientAlerts
                            .Include(p => p.AlertType)
                            .Where(p => p.Mrn == id)
                            .OrderBy(p => p.IsActive)
                    });
                }
            }

            if (sortOrder == "byActiveDesc")
            {
                {
                    sortOrder = "";
                    return View(new ListAlertsViewModel
                    {
                        PatientAlerts = repository.PatientAlerts
                            .Include(p => p.AlertType)
                            .Where(p => p.Mrn == id)
                            .OrderByDescending(p => p.IsActive)
                    });
                }
            }
            else
            {
                sortOrder = "";
                return View(new ListAlertsViewModel
                {
                    PatientAlerts = repository.PatientAlerts
                        .Include(p => p.AlertType)
                        .Where(p => p.Mrn == id)
                        .OrderByDescending(p => p.LastModified)
                });
            }
        }

        /// <summary>
        /// Redirect back to return url.
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Used in: CreateAlert, ListAlerts
        // Is it worth having this method?
        public IActionResult BackToCaller(string id, string returnUrl)
        {
            if (returnUrl.Length > 0)
            {
                return Redirect(returnUrl);
                //return RedirectToAction("Details", "Patient", id);
            }
            else
            {
                return RedirectToAction("Details", "Patient", id);
            }
        }

        /// <summary>
        /// Redirect back to patient details
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Used in: EditPatientAlert
        // Is it worth having this method?
        public RedirectToRouteResult BackToDetails(string id) =>
            RedirectToRoute(new
            {
                controller = "Patient",
                action = "Details",
                ID = id
            });

        /// <summary>
        /// Redirect back to list alerts. Unused
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Is it worth having this method?
        public RedirectToRouteResult BackToListAlerts(string id) =>
            RedirectToRoute(new
            {
                controller = "Patient",
                action = "ListAlerts",
                ID = id
            });

        /// <summary>
        /// View CreateAlert page
        /// </summary>
        /// <param name="id">Id of unique patient</param>
        // Used in: ListAlerts
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Nursing Student, HIT Clerk, Registrar")]
        public IActionResult CreateAlert(string id, string returnUrl)
        {
            ViewBag.myMrn = id;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LastModified = DateTime.Today;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == id);

            if (repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id) == null)
            {
                ViewBag.MRN = id;
            }
            else
            {
                ViewBag.MRN = repository.PatientAlerts.FirstOrDefault(b => b.Mrn == id).Mrn;
            }

            ViewBag.LastModified = @DateTime.Now;

            ViewBag.AlertType = repository.AlertTypes.OrderBy(a => a.Name).Select(a =>
                new SelectListItem
                {
                    Value = a.AlertId.ToString(),
                    Text = a.Name
                }).ToList();

            ViewBag.Restriction = repository.Restrictions.OrderBy(r => r.Name).Include(r => r.PatientRestrictions)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.RestrictionId.ToString(),
                        Text = r.Name
                    }).ToList();

            //var query = repository.FallRisk.Select(r => new { r.FallRiskId, r.FallRisk.Name });
            //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Name", 0);
            ViewBag.PatientFallRisk = repository.FallRisks.OrderBy(r => r.Name).Include(r => r.PatientFallRisks).Select(
                r =>
                    new SelectListItem
                    {
                        Value = r.FallRiskId.ToString(),
                        Text = r.Name
                    }).ToList();


            ViewBag.Allergens = repository.Allergens.OrderBy(r => r.AllergenName).Include(r => r.PatientAllergies)
                .Select(r =>
                    new SelectListItem
                    {
                        Value = r.AllergenId.ToString(),
                        Text = r.AllergenName
                    }).ToList();


            ViewBag.Reactions = repository.Reactions.OrderBy(r => r.Name).Include(r => r.PatientAllergies).Select(r =>
                new SelectListItem
                {
                    Value = r.ReactionId.ToString(),
                    Text = r.Name
                }).ToList();

            return View();
        }

        /// <summary>
        /// Create alert
        /// </summary>
        /// <param name="model">Alert model to be added to database</param>
        // Used in: CreateAlert
        [HttpPost]
        [ActionName("CreateAlert")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Nursing Student, HIT Clerk, Registrar, Physician")]
        public IActionResult CreateAlert(AlertsViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                ViewBag.AlertType = repository.PatientAlerts.Include(p => p.AlertType);
                ViewBag.FallRiskID = repository.PatientAlerts.Include(p => p.PatientFallRisks);
                ViewBag.LastModified = @DateTime.Now;

                ViewBag.AlertType = repository.AlertTypes.Select(a =>
                    new SelectListItem
                    {
                        Value = a.AlertId.ToString(),
                        Text = a.Name
                    }).ToList();

                ViewBag.Restriction = repository.Restrictions.Include(r => r.PatientRestrictions).Select(r =>
                    new SelectListItem
                    {
                        Value = r.RestrictionId.ToString(),
                        Text = r.Name
                    }).ToList();

                //var query = repository.FallRisk.Select(r => new { r.FallRiskId, r.Description });
                //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Description", 0);
                ViewBag.PatientFallRisk = repository.FallRisks.Include(r => r.PatientFallRisks).Select(r =>
                    new SelectListItem
                    {
                        Value = r.FallRiskId.ToString(),
                        Text = r.Name
                    }).ToList();

                repository.AddAlert(model);
                string myUrl = "ListAlerts/" + model.Mrn;
                //                 return Redirect(myUrl);
                //ViewBags for Patient Banner at top of page
                string id = model.Mrn;
                ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts)
                    .FirstOrDefault(b => b.Mrn == model.Mrn);

                return Redirect(ViewBag.ReturnUrl);
            }

            return View();
        }

        /// <summary>
        /// View EditPatientAlert page
        /// </summary>
        /// <param name="id">Id of unique alert</param>
        /// <param name="mrn">Mrn of unique patient</param>
        // Used in: ListAlerts
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Nursing Student, HIT Clerk, Registrar, Physician")]
        public IActionResult EditPatientAlert(int id, string mrn, string returnUrl)
        {
            ViewBag.myMrn = mrn;
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == mrn);

            //ViewBag.LastModified = DateTime.Today.AddYears(-1);
            ViewBag.AlertTypeId = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).AlertTypeId;


            var myAlertType = (from pa in repository.PatientAlerts
                join at in repository.AlertTypes on pa.AlertTypeId equals at.AlertId
                where pa.PatientAlertId == id
                select new
                {
                    alertType = at.Name
                }).FirstOrDefault();

            ViewBag.MyAlertType = myAlertType.alertType;

            ViewBag.Comments = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).Comments;
            ViewBag.StartDate = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).StartDate
                .ToString("MM/dd/yyyy");

            ViewBag.EndDate = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).EndDate;
            //ViewBag.EndDate = checkEndDate != null ? checkEndDate : "N/A";

            var checkActive = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).IsActive;
            ViewBag.Active = checkActive == true ? "Yes" : "No";

            var myFallRisk = (from pa in repository.PatientAlerts
                join pf in repository.PatientFallRisks on pa.PatientAlertId equals pf.PatientAlertId
                join fr in repository.FallRisks on pf.FallRiskId equals fr.FallRiskId
                where pf.PatientAlertId == id
                select new
                {
                    fallrisk = fr.Name
                }).FirstOrDefault();

            if (myFallRisk == null)
            {
                ViewBag.FallRisk = "";
            }
            else
            {
                ViewBag.FallRisk = myFallRisk.fallrisk;
            }


            var myRestriction = (from pa in repository.PatientAlerts
                join pr in repository.PatientRestrictions on pa.PatientAlertId equals pr.PatientAlertId
                join re in repository.Restrictions on pr.RestrictionTypeId equals re.RestrictionId
                where pr.PatientAlertId == id
                select new
                {
                    theRestriction = re.Name
                }).FirstOrDefault();

            if (myRestriction == null)
            {
                ViewBag.RestrictionValue = "";
            }
            else
            {
                ViewBag.RestrictionValue = myRestriction.theRestriction;
            }

            var myAllergen = (from pa in repository.PatientAlerts
                join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                join al in repository.Allergens on pal.AllergenId equals al.AllergenId
                where pal.PatientAlertId == id
                select new
                {
                    allergen = al.AllergenName
                }).FirstOrDefault();

            if (myAllergen == null)
            {
                ViewBag.AllergenValue = "";
            }
            else
            {
                ViewBag.AllergenValue = myAllergen.allergen;
            }

            var myReaction = (from pa in repository.PatientAlerts
                join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                join rea in repository.Reactions on pal.ReactionId equals rea.ReactionId
                where pal.PatientAlertId == id
                select new
                {
                    reaction = rea.Name
                }).FirstOrDefault();

            if (myReaction == null)
            {
                ViewBag.ReactionValue = "";
            }
            else
            {
                ViewBag.ReactionValue = myReaction.reaction;
            }

            ViewBag.AlertType = repository.AlertTypes.OrderBy(a => a.Name).Select(a =>
                new SelectListItem
                {
                    Value = a.AlertId.ToString(),
                    Text = a.Name
                }).ToList();

            return View(repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id));
        }

        /// <summary>
        /// View UpdateAlert page
        /// </summary>
        /// <param name="id">Id of unique alert</param>
        /// <param name="mrn">Mrn of unique patient</param>
        // Used in: EditPatientAlert
        public IActionResult UpdateAlert(int id, string mrn)
        {
         
            var alert = repository.PatientAlerts.FirstOrDefault(a => a.PatientAlertId == id);
            

            if (alert != null)
            {
                var model = alert;
                ViewBag.Patient = repository.Patients.FirstOrDefault(p => p.Mrn == mrn);
                var checkActive = repository.PatientAlerts.FirstOrDefault(p => p.PatientAlertId == id).IsActive;
                ViewBag.Active = checkActive == true ? "Yes" : "No";

                var myAlertType = (from pa in repository.PatientAlerts
                                   join at in repository.AlertTypes on pa.AlertTypeId equals at.AlertId
                                   where pa.PatientAlertId == id
                                   select new
                                   {
                                       alertType = at.Name
                                   }).FirstOrDefault();

                ViewBag.MyAlertType = myAlertType.alertType;

                var myFallRisk = (from pa in repository.PatientAlerts
                                  join pf in repository.PatientFallRisks on pa.PatientAlertId equals pf.PatientAlertId
                                  join fr in repository.FallRisks on pf.FallRiskId equals fr.FallRiskId
                                  where pf.PatientAlertId == id
                                  select new
                                  {
                                      fallrisk = fr.Name
                                  }).FirstOrDefault();

                if (myFallRisk == null)
                {
                    ViewBag.FallRisk = "";
                }
                else
                {
                    ViewBag.FallRisk = myFallRisk.fallrisk;
                }


                var myRestriction = (from pa in repository.PatientAlerts
                                     join pr in repository.PatientRestrictions on pa.PatientAlertId equals pr.PatientAlertId
                                     join re in repository.Restrictions on pr.RestrictionTypeId equals re.RestrictionId
                                     where pr.PatientAlertId == id
                                     select new
                                     {
                                         theRestriction = re.Name
                                     }).FirstOrDefault();

                if (myRestriction == null)
                {
                    ViewBag.RestrictionValue = "";
                }
                else
                {
                    ViewBag.RestrictionValue = myRestriction.theRestriction;
                }

                var myAllergen = (from pa in repository.PatientAlerts
                                  join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                                  join al in repository.Allergens on pal.AllergenId equals al.AllergenId
                                  where pal.PatientAlertId == id
                                  select new
                                  {
                                      allergen = al.AllergenName
                                  }).FirstOrDefault();

                if (myAllergen == null)
                {
                    ViewBag.AllergenValue = "";
                }
                else
                {
                    ViewBag.AllergenValue = myAllergen.allergen;
                }

                var myReaction = (from pa in repository.PatientAlerts
                                  join pal in repository.PatientAllergy on pa.PatientAlertId equals pal.PatientAlertId
                                  join rea in repository.Reactions on pal.ReactionId equals rea.ReactionId
                                  where pal.PatientAlertId == id
                                  select new
                                  {
                                      reaction = rea.Name
                                  }).FirstOrDefault();

                if (myReaction == null)
                {
                    ViewBag.ReactionValue = "";
                }
                else
                {
                    ViewBag.ReactionValue = myReaction.reaction;
                }

                var date = DateTime.Now;
                var day = date.Day.ToString("00");
                ViewBag.currentDate = $"{date.Year}-{date.Month}-{day}";

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Update alert and redirect back to alert list
        /// </summary>
        /// <param name="model">Alert model to be updated</param>
        // Used in: UpdateAlert
        [HttpPost]
        [ActionName("UpdateAlert")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAlert(PatientAlert model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.LastModified = DateTime.Now;
                model.IsActive = model.EndDate == null;

                repository.EditAlert(model);
                return RedirectToAction("ListAlerts", new { id = model.Mrn });
             
            }

            return RedirectToAction("ListAlerts", new {id = model.Mrn });
        }

        /// <summary>
        /// Deletes Alert. Alerts can only be deleted if PCA records don't exist for it.
        /// </summary>
        /// <param name="id">Id of unique alert</param>
        /// <param name="mrn">Mrn of unique patient</param>
        // Used in: ListAlerts
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteAlert(long id, string mrn)
        {
            bool usingExists = repository.PatientFallRisks.Any(p => p.PatientAlertId == id);
            if (usingExists)
            {
                //Console.WriteLine("Fall risk records exist using this record.");
                //ViewData["ErrorMessage"] = "Fall risk records exist using this record. Delete not available. This page will now refresh.";
                return RedirectToAction("ListAlerts", new {id = mrn});
            }

            var alert = repository.PatientAlerts.FirstOrDefault(b => b.PatientAlertId == id);
            repository.DeleteAlert(alert);
            //May not be right redirect
            return RedirectToAction("ListAlerts", new {id = mrn});
        }

        /// <summary>
        /// Add dropdowns to encounter views. Controller method to display dropdowns
        /// </summary>
        public void AddDropdowns(Patient model = null)
        {
            var queryReligion = repository.Religions
                .OrderBy(r => r.Name)
                .Select(r => new {r.ReligionId, r.Name})
                .ToList();
            ViewBag.Religions = new SelectList(queryReligion, "ReligionId", "Name", 0);

            var querySex = repository.Sexes
                .OrderBy(r => r.Name)
                .Select(r => new {r.SexId, r.Name})
                .ToList();
            ViewBag.Sexes = new SelectList(querySex, "SexId", "Name", 0);

            var queryGender = repository.Genders
                .OrderBy(r => r.Name)
                .Select(r => new {r.GenderId, r.Name})
                .ToList();
            ViewBag.Gender = new SelectList(queryGender, "GenderId", "Name", 0);

            var queryEthnicity = repository.Ethnicities
                .OrderBy(r => r.Name)
                .Select(r => new {r.EthnicityId, r.Name})
                .ToList();
            ViewBag.Ethnicity = new SelectList(queryEthnicity, "EthnicityId", "Name", 0);

            var queryMaritalStatus = repository.MaritalStatuses
                .OrderBy(r => r.Name)
                .Select(r => new {r.MaritalStatusId, r.Name})
                .ToList();
            ViewBag.MaritalStatus = new SelectList(queryMaritalStatus, "MaritalStatusId", "Name", 0);

            var queryLanguages = repository.Languages
                .OrderBy(r => r.Name)
                .Select(r => new { r.LanguageId, r.Name })
                .ToList();

            var queryRaces = repository.Races
                .OrderBy(r => r.Name)
                .Select(r => new { r.RaceId, r.Name })
                .ToList();

            // add patient language and race
            if (model != null)
            {
                var patientHasPrimaryLanguage = repository.PatientLanguages.Any(l => l.Mrn == model.Mrn && l.IsPrimary == 1);
                ViewBag.Languages = patientHasPrimaryLanguage ?
                    new SelectList(queryLanguages, "LanguageId", "Name", repository.PatientLanguages.FirstOrDefault(l => l.Mrn == model.Mrn && l.IsPrimary == 1).LanguageId) :
                    new SelectList(queryLanguages, "LanguageId", "Name", 0);

                var patientHasRace = repository.PatientRaces.Any(r => r.Mrn == model.Mrn);
                ViewBag.Races = patientHasRace ?
                    new SelectList(queryRaces, "RaceId", "Name", repository.PatientRaces.FirstOrDefault(r => r.Mrn == model.Mrn).RaceId) :
                    ViewBag.Races = new SelectList(queryRaces, "RaceId", "Name", 0);
            }
            // edit patient language and race
            else
            {
                ViewBag.Languages = new SelectList(queryLanguages, "LanguageId", "Name", 0);
                ViewBag.Races = new SelectList(queryRaces, "RaceId", "Name", 0);
                ViewBag.LastModified = DateTime.Now;
            }
            

        }

    }
}