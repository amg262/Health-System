using System.Threading.Tasks;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IS_Proj_HIT
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public class DeleteModelProgram : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public DeleteModelProgram(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entities.Program Program { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Program = await _context.Programs.FirstOrDefaultAsync(m => m.ProgramId == id);

            if (Program == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Program = await _context.Programs.FindAsync(id);

            if (Program != null)
            {
                _context.Programs.Remove(Program);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
