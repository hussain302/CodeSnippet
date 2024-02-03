using CodeSnippet.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSnippet.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasKey(u => u.Id); 

        builder.Property(u => u.Username)
               .HasMaxLength(50)   
               .IsRequired();

        builder.Property(u => u.PasswordHash)
               .HasMaxLength(maxLength:int.MaxValue)
               .IsRequired();

        builder.Property(u => u.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.MiddleName)
               .HasMaxLength(50);
        
        builder.Property(u => u.LastName)
               .HasMaxLength(50)
               .IsRequired();
        
        builder.Property(u => u.Email)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(u => u.CreatedAt)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(u => u.UpdatedAt)
               .IsRequired()
               .HasColumnType("datetime");
        
        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey(u => u.RoleId);

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.StreetAddress)
                    .HasColumnName("StreetAddress")
                   .HasMaxLength(70);

            address.Property(a => a.City)
                   .HasColumnName("City")
                   .HasMaxLength(25);

            address.Property(a => a.State)
                   .HasColumnName("State")
                   .HasMaxLength(25);
            
            address.Property(a => a.Country)
                   .HasColumnName("Country")
                   .HasMaxLength(25);

            address.Property(a => a.ZipCode)
                   .HasColumnName("ZipCode")
                   .HasMaxLength(10);
        });
    }
}