using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT.Controllers
{
    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, HIT Clerk, Nursing Student, Physician, Read Only, Registrar")]
    public class OrderController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly WCTCHealthSystemContext _db;
        public int PageSize = 8;
        public OrderController(IWCTCHealthSystemRepository repo, WCTCHealthSystemContext db) 
        {
            _repository = repo;
            _db = db;
        } 
        //used to display orders for an encounter
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        
        /// <summary>
        /// View page of a specific order
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        /// <param name="orderId">Id of unique order</param>
        /// 
        // Used in: ViewOrder 
        public IActionResult ViewOrder(long encounterId, long orderId)
        {
            ViewData["ErrorMessage"] = "";

            var id = User.Identity.Name;

            var encounter = _repository.Encounters
                // .Include(e => e.EncounterPhysicians.Physician)
                .Include(e => e.EncounterType)
                .FirstOrDefault(b => b.EncounterId == encounterId);
            var order = _repository.AdmitOrders
                .Include(o => o.CoOrderingPhysician)
                .Include(o => o.Encounter)
                .Include(o => o.VisitType)
                .Include(o => o.OrderCompletedByPhysician)
                .Include(o => o.Department) 
                .Include(o => o.OrderingPhysician)
                .FirstOrDefault(b => b.AdmitOrderId == orderId);
            if (encounter is null || order is null)
                return RedirectToAction("CheckedIn");

            var patient = _repository.Patients
                .Include(p => p.PatientAlerts)
                .FirstOrDefault(p => p.Mrn == encounter.Mrn);

            return View(new ViewOrderPageModel
            {
                Order     = order,
                Patient   = patient,
                Encounter = encounter
            });
        }

        /// <summary>
        /// View ListOrders for Encounter, includes sort orders
        /// </summary>
        /// <param name="id">Id of unique encounter</param>
        /// <param name="sortOrder">Order to sort orders</param>
        // Used in: ListOrders
        public IActionResult ListOrders(long id, string sortOrder)
        {
            // Remember the user's original request
            ViewBag.ReturnUrl = Request.Headers["Referer"].ToString();

            //Sort params for to determine sort order
            ViewBag.OrderDateSortParm = sortOrder == "byOrderDate" ? "byOrderDateDesc" : "byOrderDate";
            ViewBag.CompletedDateSortParm = sortOrder == "byCompletedDate" ? "byCompletedDateDesc" : "byCompletedDate";
            ViewBag.VisitTypeSortParm = sortOrder == "byVisitTypeDesc" ? "byVisitType" : "byVisitTypeDesc";
            ViewBag.CompletedSortParm = sortOrder == "byCompleted" ? "byCompletedDesc" : "byCompleted";

            // Existing
            ViewBag.EncounterId = id;
            ViewBag.Encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == id);
            string Mrn = _repository.Encounters.FirstOrDefault(b => b.EncounterId == id).Mrn;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == Mrn);
            ViewBag.Mrn = Mrn;

            if (sortOrder == "byVisitType" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Visit Type Ascending";
            }
            else if (sortOrder == "byVisitTypeDesc" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Visit Type Descending";
            }
            else if (sortOrder == "byOrderDate" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Order Date Ascending";
            }

            else if (sortOrder == "byOrderDateDesc" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Order Date Descending";
            }
            else if (sortOrder == "byCompletedDate" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Completed Date Ascending";
            }

            else if (sortOrder == "byOrderDateDesc" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Completed Date Descending";
            }

            else if (sortOrder == "byCompleted" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Completed Ascending";
            }

            else if (sortOrder == "byCompletedDesc" && _repository.AdmitOrders.Where(b => b.EncounterId == id).Count() > 0)
            {
                TempData["msg"] = "Sort order is by Completed Descending";
            }

            else
            {
                TempData["msg"] = "";
            }

            
            if (sortOrder == "byOrderDate")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderBy(p => p.OrderDateTime)
                });
            }

            else if (sortOrder == "byOrderDateDesc")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderByDescending(p => p.OrderDateTime)
                });
            }

            else if (sortOrder == "byVisitType")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderBy(p => p.VisitType.Name)
                });
            }

            else if (sortOrder == "byVisitTypeDesc")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderByDescending(p => p.VisitType.Name)
                });
            }

            else if (sortOrder == "byCompletionDate")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderBy(p => p.OrderCompletedDateTime)
                });
            }

            else if (sortOrder == "byCompletionDate")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderByDescending(p => p.OrderCompletedDateTime)
                });
            }

            else if (sortOrder == "byCompleted")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderBy(p => p.IsOrderComplete)
                });
            }

            else if (sortOrder == "byCompletedDesc")
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderByDescending(p => p.IsOrderComplete)
                });
            }
            else
            {
                sortOrder = "";
                return View(new ListOrdersViewModel
                {
                    Orders = _repository.AdmitOrders
                        .Include(p => p.VisitType)
                        .Include(p => p.Encounter)
                        .Where(p => p.EncounterId == id)
                        .OrderByDescending(p => p.OrderCompletedDateTime)
                });
            }
        }

        /// <summary>
        /// View CreateOrder page
        /// </summary>
        /// <param name="encounterId">Id of unique encounter</param>
        // Used in: ListOrders
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Nursing Student, HIT Clerk, Registrar")]
        public IActionResult CreateOrder(long encounterId, string returnUrl, string Mrn)
        {
            ViewBag.encounterId = encounterId;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.OrderDateTime = DateTime.Now;
            ViewBag.OrderCompletedDateTime = @DateTime.Now;
            ViewBag.LastModified = @DateTime.Now;
            //ViewBags for Patient Banner at top of page
            ViewBag.Patient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(b => b.Mrn == Mrn);
            ViewBag.Encounter = _repository.Encounters.FirstOrDefault(b => b.EncounterId == encounterId);

            if (_repository.PatientAlerts.FirstOrDefault(b => b.Mrn == Mrn) == null)
            {
                ViewBag.MRN = Mrn;
            }
            else
            {
                ViewBag.MRN = _repository.PatientAlerts.FirstOrDefault(b => b.Mrn == Mrn).Mrn;
            }            

            var queryVisitType = _repository.VisitTypes
                    .OrderBy(p => p.Name)
                    .Select(p => new {p.VisitTypeId, p.Name})
                    .ToList();

            ViewBag.VisitType = new SelectList(queryVisitType, "VisitTypeId", "Name", 0);

            var queryDepartments = _repository.Departments
                    .OrderBy(p => p.Name)
                    .Select(p => new {p.DepartmentId, p.Name})
                    .ToList();

            ViewBag.Departments = new SelectList(queryDepartments, "DepartmentId", "Name", 0);

            var queryPhysician = _repository.Physicians
                    .OrderBy(p => p.LastName)
                    .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
                    .ToList();

            ViewBag.Physicians = new SelectList(queryPhysician, "PhysicianId", "LastName", 0);
            //var query = _repository.FallRisk.Select(r => new { r.FallRiskId, r.FallRisk.Name });
            //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Name", 0);

            var model = new OrdersViewModel(ViewBag.Encounter);

            return View(model);
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="model">Order model to be added to database</param>
        // Used in: CreateOrder
        [HttpPost]
        [ActionName("CreateOrder")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, Nursing Student, HIT Clerk, Registrar, Physician")]
        public IActionResult CreateOrder(OrdersViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                ViewBag.VisitType = _repository.AdmitOrders.Include(p => p.VisitType);
                ViewBag.Department = _repository.Departments;
                ViewBag.OrderDateTime = @DateTime.Now;
                ViewBag.OrderCompletedDateTime = @DateTime.Now;
                ViewBag.LastModified = @DateTime.Now;

                ViewBag.VisitType = _repository.VisitTypes.Select(a =>
                    new SelectListItem
                    {
                        Value = a.VisitTypeId.ToString(),
                        Text = a.Name
                    }).ToList();

                ViewBag.Department = _repository.Departments.Select(r =>
                    new SelectListItem
                    {
                        Value = r.DepartmentId.ToString(),
                        Text = r.Name
                    }).ToList();

                ViewBag.Physicians = _repository.Physicians.Select(r =>
                    new SelectListItem
                    {
                        Value = r.PhysicianId.ToString(),
                        Text = r.LastName+", "+r.FirstName
                    }).ToList();
                //var query = _repository.FallRisk.Select(r => new { r.FallRiskId, r.Description });
                //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Description", 0);

                _repository.AddAdmitOrder(model);
                string myUrl = "ListOrders/" + model.EncounterId;
                return Redirect(myUrl);
                //ViewBags for Patient Banner at top of page
                // long id = model.EncounterId;
                // ViewBag.Encounter = _repository.Encounters.Include(p => p.AdmitOrders)
                //     .FirstOrDefault(b => b.EncounterId == model.EncounterId);

                // return Redirect(ViewBag.ReturnUrl);
            }

            return View();
        }

        /// <summary>
        /// Deletes Order.
        /// </summary>
        /// <param name="id">Id of unique order</param>
        /// <param name="mrn">Mrn of unique patient</param>
        // Used in: ListOrders
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteOrder(long id, long encounterId)
        {
            bool usingExists = _repository.AdmitOrders.Any(p => p.AdmitOrderId==id);
            if (usingExists)
            {
                var order = _repository.AdmitOrders.FirstOrDefault(b => b.AdmitOrderId==id);
                order = _repository.AdmitOrders.FirstOrDefault(b => b.AdmitOrderId==id);
                _repository.DeleteAdmitOrder(order);
            }
            return RedirectToAction("ListOrders", new {id = encounterId});
        }

        /// <summary>
        /// Shows the edit Admit Order page
        /// </summary>
        /// <param name="oId">Id of unique Admit Order </param>
        /// <param name="id">Id of unique Encounter</param>
        public IActionResult EditAdmitOrder(long id, long oId){

            ViewBag.EncounterId = id;
            ViewBag.OrderId = oId;
            ViewBag.LastModified = @DateTime.Now;

            var desiredEncounter = _repository.Encounters.FirstOrDefault(u => u.EncounterId == id);

            var desiredPatient = _repository.Patients.Include(p => p.PatientAlerts).FirstOrDefault(u => u.Mrn == desiredEncounter.Mrn);

            ViewBag.Patient = desiredPatient;
            ViewBag.Encounter = desiredEncounter;
            ViewBag.OrderDate = _repository.AdmitOrders.Where(u => u.EncounterId == id).FirstOrDefault(u => u.AdmitOrderId == oId).OrderDateTime;
            ViewBag.CompletedDate = _repository.AdmitOrders.Where(u => u.EncounterId == id).FirstOrDefault(u => u.AdmitOrderId == oId).OrderCompletedDateTime;

            var desiredOrder = _repository.AdmitOrders
                .Include(e => e.VisitType)
                .Include(e => e.Department)
                .Include(e => e.OrderingPhysician)
                .Include(e => e.CoOrderingPhysician)
                .Include(e => e.OrderCompletedByPhysician)
                .Where(u => u.EncounterId == id)
                .FirstOrDefault(u => u.AdmitOrderId == oId);

            ViewBag.Orders = desiredOrder;
            ViewBag.LastModified = @DateTime.Now;        

            var queryVisitType = _repository.VisitTypes
                    .OrderBy(p => p.Name)
                    .Select(p => new {p.VisitTypeId, p.Name})
                    .ToList();

            ViewBag.VisitType = new SelectList(queryVisitType, "VisitTypeId", "Name", 0);

            var queryDepartments = _repository.Departments
                    .OrderBy(p => p.Name)
                    .Select(p => new {p.DepartmentId, p.Name})
                    .ToList();

            ViewBag.Departments = new SelectList(queryDepartments, "DepartmentId", "Name", 0);

            var queryPhysician = _repository.Physicians
                    .OrderBy(p => p.LastName)
                    .Select(p => new {p.PhysicianId, p.FirstName, p.LastName})
                    .ToList();

            ViewBag.Physicians = new SelectList(queryPhysician, "PhysicianId", "LastName", 0);
            //var query = _repository.FallRisk.Select(r => new { r.FallRiskId, r.FallRisk.Name });
            //ViewBag.PatientFallRisk = new SelectList(query.AsEnumerable(), "FallRiskId", "Name", 0);

            var model = new OrdersViewModel(desiredEncounter, desiredOrder);

            return View(model);
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="model">Order model to be edited in database</param>
        // Used in: EditOrder
        [HttpPost]
        [ActionName("EditAdmitOrder")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditAdmitOrder(AdmitOrder model){
            if(!ModelState.IsValid){
                return View(model);
            }

            _repository.EditAdmitOrder(model);
            return RedirectToAction("ListOrders", new {id = model.EncounterId});
        }
    }
}
