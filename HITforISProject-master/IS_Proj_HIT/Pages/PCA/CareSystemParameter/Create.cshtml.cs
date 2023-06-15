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
    public class CreateModelCareSystemParameter : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public CreateModelCareSystemParameter(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CareSystemTypeId"] = new SelectList(_context.CareSystemTypes, "CareSystemTypeId", "Name", "NormalLimitsDescription");
            return Page();
        }

        [BindProperty]
        public CareSystemParameter CareSystemParameter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CareSystemParameter.LastModified = DateTime.Now;
            _context.CareSystemParameters.Add(CareSystemParameter);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}