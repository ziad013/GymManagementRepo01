using MVC01.Entities;
using System.Linq.Expressions;

namespace MVC01.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate , CancellationToken ct = default);
    }
}
