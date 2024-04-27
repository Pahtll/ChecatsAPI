using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checats.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int UsernameMaxLength = 30;
    private const int EmailMaxLength = 40;

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Username)
            .HasMaxLength(UsernameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasMaxLength(EmailMaxLength)
            .IsRequired();

        builder
            .Property(u => u.UserRole)
            .HasDefaultValue(UserRole.User)
            .IsRequired();

        builder
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author);

        builder
            .HasMany(u => u.Commentaries)
            .WithOne(p => p.Author);

        builder
            .Property(u => u.ProfilePicture);
    }
}