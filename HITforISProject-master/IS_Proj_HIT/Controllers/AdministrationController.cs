﻿using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;

namespace IS_Proj_HIT.Controllers
{

    [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, HIT Clerk, Nursing Student, Read Only, Registrar")]
    public class AdministrationController : Controller
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WCTCHealthSystemContext _db;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IWCTCHealthSystemRepository repo, WCTCHealthSystemContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repo;
            _db = db;
        }

        /// <summary>
        /// Display the Admin tools index
        /// </summary>
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult Index() => View();

        /// <summary>
        /// Administrator data entry for PCAs
        /// </summary>
        // Used in: Administration Tools - PCA indexes
        #region PCA lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult DataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(PcacommentType).Name,
                typeof(BloodPressureRouteType).Name,
                typeof(O2deliveryType).Name,
                typeof(PulseRouteType).Name,
                typeof(TempRouteType).Name,
                typeof(CareSystemParameter).Name,
                typeof(CareSystemType).Name,
                typeof(PainParameter).Name,
                typeof(PainRating).Name,
                typeof(PainRatingImage).Name,
                typeof(PainScaleType).Name
            };
            return View(entityNames);
        }
        #endregion

        /// <summary>
        /// Administrator data entry for encounters
        /// </summary>
        // Used in: Administration Tools - Encounter indexes
        #region Encounter lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult EncounterDataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(AdmitType).Name,
                typeof(Department).Name,
                typeof(Discharge).Name,
                typeof(EncounterType).Name,
                typeof(Facility).Name,
                typeof(PlaceOfServiceOutPatient).Name,
                typeof(PointOfOrigin).Name,
                typeof(Entities.Program).Name,
                typeof(ProgramFacility).Name
            };
            return View(entityNames);
        }
        #endregion

        /// <summary>
        /// Administrator data entry for physicians
        /// </summary>
        // Used in: Administration Tools - Physician indexes
        #region Physician lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult PhysicianDataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(Physician).Name,
                typeof(PhysicianRole).Name,
                typeof(ProviderType).Name,
                typeof(Specialty).Name
            };
            return View(entityNames);
        }
        #endregion

        /// <summary>
        /// Administrator data entry for Physician Forms stuff
        /// </summary>
        // Used in: Administration Tools - Physician Forms indexes
        #region PForms lookup table management
        [Authorize(Roles = "Administrator")]
        public IActionResult PFormsDataEntry()
        {
            var entityNames = new List<string>
            {
                typeof(NoteType).Name,
                typeof(VisitType).Name
            };
            return View(entityNames);
        }
        #endregion

        /// <summary>
        /// Displays EditRegisterDetails page
        /// </summary>
        // Used when clicked on your e-mail in the nav-bar
        // Used in: Login, Register, Home Page, LoginPartial
        #region User Details
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty, HIT Clerk, Nursing Student, Read Only, Registrar")]
        public IActionResult EditRegisterDetails()
        {
            //find current user
            var id = _userManager.GetUserId(HttpContext.User);

            //select the information I want to display
            var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == id);

            return View(dbUser);
        }

        /// <summary>
        /// Edits and saves Register Details
        /// </summary>
        /// <param name="model">UserTable model</param>
        // Used in: EditUserDetails, EditRegisterDetails
        [HttpPost]
        public IActionResult EditRegisterDetails(UserTable model)
        {
            if (!ModelState.IsValid) return View(model);

            if (string.IsNullOrWhiteSpace(model.AspNetUsersId))
                model.AspNetUsersId = _userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrWhiteSpace(model.Email))
                model.Email = User.Identity?.Name;

            // A quick fix until start date and end date are properly implemented
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
                
            model.LastModified = DateTime.Now;
            if (model.UserId is 0)
                _repository.AddUser(model);
            else
                _repository.EditUser(model);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Roles
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        /// <summary>
        /// Displays Role Manager view
        /// </summary>
        public IActionResult ViewRoles() => View(_roleManager.Roles);


        //Testing Listing the Correct Users - Chris P - 2/25/21
        //Used in: ViewUsers
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult ViewUsers()
        {
            var users = _repository.UserTables;
            return View(users);

        }


        /// <summary>
        /// Retrieves User List
        /// </summary>
        //List users - Chris P - 2/27/21 edited by jason Motl 9-21-21 
        //Used in: Admin Details, Admin Index, ListUsers
        [HttpGet]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public  IActionResult ListUsers()
        {
            //var users = _repository.UserTables;
            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserFacility = _repository.UserFacilities.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var currentUserProgram = _repository.UserPrograms.FirstOrDefault(e => e.UserId == currentUser.UserId);
            var program = _repository.Programs.FirstOrDefault(p => p.ProgramId == currentUserProgram.ProgramId);

            ViewBag.CurrentUserFacility = currentUserFacility;
            var progToDisplay = "";
            
            if (program.Equals("Nursing")) {
                progToDisplay = "Nursing";
            }
            else {
                progToDisplay = "HIT/MCS";
            }

            ViewBag.Program = progToDisplay;

            var isAdmin = User.IsInRole("Administrator");

            var model = _repository.UserTables.Select(u => new UsersPlusViewModel
                {
                    UserId = u.UserId,
                    UserName = u.Email,
                    StartDate = u.StartDate,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    AspNetUsersId = u.AspNetUsersId
                }).OrderByDescending(u => u.UserName).ToList();

            if (isAdmin) {
                model = _repository.UserTables.Select(u => new UsersPlusViewModel
                {
                    UserId = u.UserId,
                    UserName = u.Email,
                    StartDate = u.StartDate,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    AspNetUsersId = u.AspNetUsersId
                }).OrderByDescending(u => u.UserName).ToList();
            }

            return View(model);
        }

        /// <summary>
        /// Resets user password
        /// </summary>
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public async Task<IActionResult> ResetUserPassword(int id)
        {
            var userTable = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            var aspUser = await _userManager.FindByIdAsync(userTable.AspNetUsersId);
            string code = await _userManager.GeneratePasswordResetTokenAsync(aspUser);

            return RedirectToPage($"/Account/ResetPassword", new { area = "Identity", code, Id = id});
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult>  DeleteBatch(List<UsersPlusViewModel> userIdsToDelete)
        {
                foreach (var user in userIdsToDelete.Where(u => u.IsSelected))
                {

                    var selectedUser = _userManager.Users.Single(u => u.UserName == user.UserName);
                    
                    var userId = await _userManager.FindByIdAsync(selectedUser.Id);
                    
                    var result =  await _userManager.DeleteAsync(userId);
                
                }

                return RedirectToAction("ListUsers");
            
        }

        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult CreateRole() => View();

        /// <summary>
        /// View Edit Role
        /// </summary>
        /// <param name="id">Id of unique role</param>
        // Used in: EditUsersInRole, ViewRoles
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = (await _userManager.GetUsersInRoleAsync(role.Name)).Select(u => u.UserName).OrderBy(username => username).ToList()

            };

            return View(model);
        }

        /// <summary>
        /// Displays User details
        /// </summary>
        /// <param name="id">Id of unique user</param>
        // Used in: UserList?
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult Details(int id)
        {

            // grabs the user from the UserTable
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            // grabs the string AspNetUsersId from the AspNetUserRoles table
            var bridgeId = _db.AspNetUserRoles.FirstOrDefault(u => u.UserId == user.AspNetUsersId);

            var roleName = "";
            // try/catch prevents error from being thrown if there is no role assigned to the user
            try{
                roleName = _db.AspNetRoles.FirstOrDefault(u => u.Id == bridgeId.RoleId)?.Name;
            }catch{
                roleName = "Not Assigned";
            }

            if (user != null && user.FirstName.Length == 0)
            {
                user.FirstName = "Not Assigned";
            }
            if (user != null && user.LastName.Length == 0)
            {
                user.LastName = "Not Assigned";
            }

            // Make tweaks if multiple programs per user is added.
            var programName = "No current facility";
            var userProgram = _repository.UserPrograms.FirstOrDefault(uP => uP.UserId == user.UserId);

            if (userProgram != null)
            {
                var currentProgram = _repository.Programs.FirstOrDefault(p => p.ProgramId == userProgram.ProgramId);

                programName = currentProgram?.Name;
            }

            var model = new DetailsViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ProgramName = programName,
                
                RoleName = roleName
            };

            return View(model);
        }

        /// <summary>
        /// View edit users in role
        /// </summary>
        /// <param name="roleId">Id of unique role</param>
        // Used in: EditRole, List/ViewUsers
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);


            var model =  _userManager.Users.AsEnumerable().Select(u => new UserRoleViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                IsSelected = usersInRole.Any(inRole => inRole.UserName == u.UserName)

            }).AsEnumerable().OrderByDescending(u => u.IsSelected).ThenBy(u => u.UserName).ToList();

            foreach (var user in model)
            {
                var dbUser = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == user.UserId);
                user.FullName = dbUser != null ? dbUser.FirstName + " " + dbUser.LastName : "No Name On File";
            }


            return View(model);
        }

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="model">CreateRoleViewModel</param>
        // Used in: ViewRoles
        [HttpPost]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }

        /// <summary>
        /// Edit role
        /// </summary>
        /// <param name="model">EditRoleViewModel</param>
        [HttpPost]
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        /// <summary>
        /// Edit users in role
        /// </summary>
        /// <param name="model">List of UserRoleViewModels</param>
        /// <param name="roleId">Id of unique role to be edited</param>
        // EditUsersInRole with model AND ID
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("NotFound");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var modelUser in model)
            {
                var isCurrentlyInRole = usersInRole.Any(u => u.Id == modelUser.UserId);

                IdentityUser user;
                if (modelUser.IsSelected && !isCurrentlyInRole)
                {
                    user = await _userManager.FindByIdAsync(modelUser.UserId);
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!modelUser.IsSelected && isCurrentlyInRole)
                {
                    user = await _userManager.FindByIdAsync(modelUser.UserId);
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        // Edit user in administration
        // Adds user facility?
        [Authorize(Roles = "Administrator")]
        #endregion
        public IActionResult EditUserDetails(int id)
        {
            var user = _repository.UserTables.FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                var viewModel = new UsersPlusViewModel()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    AspNetUsersId = user.AspNetUsersId,
                };

                var userProgram = _repository.UserPrograms.FirstOrDefault(p => p.UserId == user.UserId);

                // Adds program id if user program isn't null
                if (userProgram != null) viewModel.ProgramId = userProgram.ProgramId;

                ViewBag.ProgramList = new List<SelectListItem>();
                var programs = _repository.Programs;

                // Checks to see if viewModels program id matches
                foreach (var program in programs)
                    if (program.ProgramId == viewModel.ProgramId)
                        ViewBag.ProgramList.Add(new SelectListItem
                            { Text = program.Name, Value = program.ProgramId.ToString(), Selected = true });
                    else
                        ViewBag.ProgramList.Add(new SelectListItem
                            { Text = program.Name, Value = program.ProgramId.ToString() });

                return View(viewModel);
            }

            ModelState.AddModelError(string.Empty, "User not found");

            return View();
        }

        [HttpPost]
        public IActionResult EditUserDetails(UsersPlusViewModel viewModel)
        {
            ViewBag.ProgramList = new List<SelectListItem>();
            var programs = _repository.Programs;

            foreach (var program in programs)
                ViewBag.ProgramList.Add(new SelectListItem { Text = program.Name, Value = program.ProgramId.ToString() });

            if (!ModelState.IsValid) return View(viewModel);

            // Quick fix for now until date implementation
            viewModel.StartDate = DateTime.Now;
            viewModel.EndDate = DateTime.Now;

            viewModel.LastModified = DateTime.Now;

            // User model is needed to change program
            var user = new UserTable()
            {
                UserId = viewModel.UserId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                AspNetUsersId = viewModel.AspNetUsersId,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                LastModified = viewModel.LastModified,
            };

            if (user.UserId is 0)
                _repository.AddUser(user);
            else
                _repository.EditUser(user);

            var hasProgram = _repository.UserPrograms.Any(p => p.UserId == user.UserId);
            // If user already has a program delete it and their active facility
            if (hasProgram)
            {
                var activeUserProgram = _repository.UserPrograms.FirstOrDefault(p => p.UserId == user.UserId);
                _repository.DeleteUserProgram(activeUserProgram);

                var activeUserFacility = _repository.UserFacilities.FirstOrDefault(f => f.UserId == user.UserId);
                if (activeUserFacility != null) _repository.DeleteUserFacility(activeUserFacility);
            }

            var newUserProgram = new UserProgram
            {
                ProgramId = viewModel.ProgramId,
                UserId = viewModel.UserId
            };
            _repository.AddUserProgram(newUserProgram);

            return View(viewModel);
        }

        /// <summary>
        /// Resets security questions
        /// </summary>
        /// <param name="id">Id of unique user</param>
        [Authorize(Roles = "Administrator, Nursing Faculty, HIT Faculty")]
        public IActionResult ResetSecurityQuestions(int id)
        {
            return RedirectToPage("/Account/AddSecurityQuestions", new { area = "Identity", Id = id });
        }
    }
}
