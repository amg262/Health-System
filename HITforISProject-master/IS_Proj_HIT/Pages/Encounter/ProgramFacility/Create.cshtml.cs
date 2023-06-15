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
    public class CreateModelProgramFacility : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelProgramFacility(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FacilityId"] = new SelectList(_context.Facilities, "FacilityId", "Name");
        ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "Name");
            return Page();
        }

        [BindProperty]
        public ProgramFacility ProgramFacility { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProgramFacilities.Add(ProgramFacility);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
