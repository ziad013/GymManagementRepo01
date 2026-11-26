using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC01.Contexts;
using MVC01.Entities;
using MVC01.Repositories.Classes;
using MVC01.Repositories.Interfaces;
using MVC01.ViewModels;

namespace MVC01.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMembershipRepository _membershipRepository;

        // GymDbContext dbContext = new GymDbContext();
        // private readonly IPlanRepository _planRepository = new PlanRepository();
        public PlanController(IPlanRepository planRepository,IMembershipRepository membershipRepository)
        {
            _planRepository = planRepository;
            _membershipRepository = membershipRepository;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            //var plans = await dbContext.Plans.ToListAsync();
            var plans = await _planRepository.GetAllAsync(ct);
            var plansViewModels = plans.Select(p => new PlanViewModel()
            {
                Id = p.Id,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Name = p.Name,
                IsActive = p.IsActive,
                Price = p.Price
            });
            return View(plansViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id,CancellationToken ct)
        {
            var plan = await _planRepository.GetPlanByIdAsync(id,ct);

            if (plan == null || !plan.IsActive)  
            {
                TempData["ErrorMessage"] = "This Plan Can Not Be Edited";
                return RedirectToAction(nameof(Index));
            }
            var HasCurrentMembershipAsync = await  _membershipRepository.AnyAsync(m => m.PlanId == id && m.EndDate > DateTime.Now,ct);

            if (HasCurrentMembershipAsync)
            {
                TempData["ErrorMessage"] = "This Plan Can Not Be Edited";
                return RedirectToAction(nameof(Index));
            }

            var planViewModel = new UpdatePlanViewModel()
    {
        PlanName = plan.Name,
        Description = plan.Description,
        DurationDays = plan.DurationDays,
        Price = plan.Price
    };
               return View(planViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,UpdatePlanViewModel UpdatedPlan,CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return View(UpdatedPlan);
            }
            var plan = await _planRepository.GetPlanByIdAsync(id, ct);

            if (plan == null || !plan.IsActive)
            {
                TempData["ErrorMessage"] = "This Plan Can Not Be Edited";
                return RedirectToAction(nameof(Index));
            }
            var HasCurrentMembershipAsync = await _membershipRepository.AnyAsync(m => m.PlanId == id && m.EndDate > DateTime.Now,ct);

            if (HasCurrentMembershipAsync)
            {
                TempData["ErrorMessage"] = "This Plan Can Not Be Edited";
                return RedirectToAction(nameof(Index));
            }

            plan.Description = UpdatedPlan.Description;
            plan.Price = UpdatedPlan.Price;
            plan.DurationDays = UpdatedPlan.DurationDays;
            plan.UpdatedAt = DateTime.Now;

            var result = await _planRepository.UpdateAsync() > 0;
            if (result)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Plan Fail to Update");
                return View(UpdatedPlan);
            }
        }

    }
}
