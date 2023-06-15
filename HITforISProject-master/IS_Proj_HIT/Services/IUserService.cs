using System.Threading.Tasks;

namespace IS_Proj_HIT.Services
{
    public interface IUserService
    {
        Task<int> GetUserIdByAspNetUserIdAsync(string id);
    }
}

