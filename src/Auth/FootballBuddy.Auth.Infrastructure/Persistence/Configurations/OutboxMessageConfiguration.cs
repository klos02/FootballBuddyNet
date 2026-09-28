using FootballBuddy.Auth.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBuddy.Auth.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Type)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Payload)
            .IsRequired();
        
        builder.HasIndex(message => message.OccurredOn)
            .HasFilter("\"ProcessedOn\" IS NULL");
    }
}