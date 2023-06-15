using System;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "Address1");
            return Page();
        }

        [BindProperty]
        public Facility Facility { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Facility.LastModified = DateTime.Now;
            _context.Facilities.Add(Facility);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}