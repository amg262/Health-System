using System.Collections.Generic;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IS_Proj_HIT
{
    public class IndexModelPcaCommentType : PageModel
    {
        private readonly WCTCHealthSystemContext _context;

        public IndexModelPcaCommentType(WCTCHealthSystemContext context)
        {
            _context = context;
        }

        public IList<PcacommentType> PcaCommentType { get;set; }

        public async Task OnGetAsync()
        {
            PcaCommentType = await _context.PcacommentTypes.ToListAsync();
        }
    }
}
