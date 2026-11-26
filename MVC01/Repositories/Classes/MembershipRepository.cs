using Microsoft.EntityFrameworkCore;
using MVC01.Contexts;
using MVC01.Entities;
using MVC01.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MVC01.Repositories.Classes
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AnyAsync(Expression<Func<Membership, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbContext.Memberships.AnyAsync(predicate,ct);
        }
    }
}
