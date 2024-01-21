using CodeSnippet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippet.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(r => r.Description)
               .HasMaxLength(1000)
               .IsRequired();
    }
}
