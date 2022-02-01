using ClimbingLocationApi.Models;
using System.Threading.Tasks;

namespace ClimbingLocationApi
{
    public interface ISitewatchCacher
    {
        public Task<Mob> GetCurrentPos();
    }
}
