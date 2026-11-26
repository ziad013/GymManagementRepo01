using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC01.Entities;

namespace MVC01.Configurations
{
    public class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);

            builder.Property(x => x.Email)
                   .HasColumnType("varchar(100)")
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.ToTable(T =>
            {
                T.HasCheckConstraint(
                    "CK_GymUser_Email",
                    "Email LIKE '_%@_%._%'"
                );

                T.HasCheckConstraint(
                    "CK_GymUser_Phone", "LEN(Phone) = 11 And ( Phone LIKE '010%' OR Phone LIKE '011%' OR Phone LIKE '012%' OR Phone LIKE '015%')"
                );
            });

            builder.ComplexProperty(x => x.Address);
        }
    }
}
