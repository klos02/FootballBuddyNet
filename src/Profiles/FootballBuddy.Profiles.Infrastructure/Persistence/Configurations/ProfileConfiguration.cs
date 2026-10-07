using FootballBuddy.Profiles.Domain.Aggregates;
using FootballBuddy.Shared.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBuddy.Profiles.Infrastructure.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(profile => profile.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new UserId(value));
        
        builder.Property(profile => profile.DisplayName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(profile => profile.PreferredPosition)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired(false);

        builder.OwnsMany(profile => profile.SecondaryPositions, assignment =>
        {
            assignment.ToTable("ProfileSecondaryPositions");

            assignment.WithOwner().HasForeignKey("ProfileId");

            assignment.Property<UserId>("ProfileId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value));

            assignment.Property(position => position.Position)
                .HasConversion<string>()
                .HasMaxLength(30)
                .ValueGeneratedNever()
                .IsRequired();

            assignment.HasKey("ProfileId", "Position");
        });

        builder.Navigation(profile => profile.SecondaryPositions)
            .HasField("_secondaryPositions")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(profile => profile.DomainEvents);
    }
}
