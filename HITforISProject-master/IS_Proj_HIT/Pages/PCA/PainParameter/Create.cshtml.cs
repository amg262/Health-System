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
    public class CreateModelPainParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelPainParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PainScaleTypeId"] = new SelectList(_context.PainScaleTypes, "PainScaleTypeId", "PainScaleTypeName");
            return Page();
        }

        [BindProperty]
        public PainParameter PainParameter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PainParameter.LastModified = DateTime.Now;
            _context.PainParameters.Add(PainParameter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}