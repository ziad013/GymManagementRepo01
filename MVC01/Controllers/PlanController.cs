using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC01.Contexts;

namespace MVC01.Controllers
{
    public class PlanController : Controller
    {
       private GymDbContext dbContext = new GymDbContext();
        public async Task<IActionResult> Index()
        {
            var plans = await dbContext.Plans.ToListAsync();
            return View(plans);
        }

    }
}
