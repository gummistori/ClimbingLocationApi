using ClimbingLocationApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClimbingLocationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly ISitewatchCacher sitewatchCacher;
        public LocationController(ISitewatchCacher sitewatchCacher)
        {
            this.sitewatchCacher = sitewatchCacher;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<Mob> Get()
        {
            return await sitewatchCacher.GetCurrentPos();

        }
    }
}
