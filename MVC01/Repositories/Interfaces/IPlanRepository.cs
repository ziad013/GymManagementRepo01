using MVC01.Entities;

namespace MVC01.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default);
        Task<Plan?> GetPlanByIdAsync(int id , CancellationToken ct = default);
        Task<int> UpdateAsync(CancellationToken ct = default);
    }
} 
