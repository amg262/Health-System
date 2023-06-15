using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Entities.Data;
using Microsoft.AspNetCore.Identity;

namespace IS_Proj_HIT.Services
{
    class UserService : IUserService
     {
         private WCTCHealthSystemContext _context;
         private UserManager<IdentityUser> _userManager;
     
         public UserService(WCTCHealthSystemContext context) => _context = context; 
     
         public async Task<int> GetUserIdByAspNetUserIdAsync(string id) => _context.UserTables
             .Where(x => x.AspNetUsersId == id)
             .Select(x => x.UserId)
             .FirstOrDefault();
     }
}

