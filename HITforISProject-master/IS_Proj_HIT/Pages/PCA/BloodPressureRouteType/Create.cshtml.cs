using System;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class CreateModelBloodPressureRouteType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelBloodPressureRouteType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BloodPressureRouteType BloodPressureRouteType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BloodPressureRouteType.LastModified = DateTime.Now;
            _context.BloodPressureRouteTypes.Add(BloodPressureRouteType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}