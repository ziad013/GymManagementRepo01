using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC01.Entities;

namespace MVC01.Configurations
{
    public class MembershipConfigurations : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
           builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Member)
                  .WithMany(x => x.MemberSubscriptions)
                  .HasForeignKey(x => x.MemberId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Plan)
                   .WithMany(x => x.PlanSubscriptions)
                   .HasForeignKey(x => x.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
