using Microsoft.EntityFrameworkCore;
using MVC01.Contexts;
using MVC01.Entities;
using MVC01.Repositories.Interfaces;

namespace MVC01.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //private readonly GymDbContext _dbContext = new GymDbContext(); //DI
        public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Plans.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Plan?> GetPlanByIdAsync(int id, CancellationToken ct = default)
        {
           //return await _dbContext.Plans.FindAsync(new object[] {id}, ct);
           return await _dbContext.Plans.FindAsync([id],cancellationToken: ct);
        }

        public async Task<int> UpdateAsync(CancellationToken ct = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                return 0;
            }
        }
    }
}
