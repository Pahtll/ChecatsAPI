using Checats.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checats.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
{
    private const int TitleMaxLength = 100;
    private const int ContentMaxLength = 10000;
    
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Title)
            .HasMaxLength(TitleMaxLength)
            .IsRequired();
        
        builder
            .Property(p => p.Content)
            .HasMaxLength(ContentMaxLength)
            .IsRequired();

        builder
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts);

        builder
            .HasMany(p => p.Commentaries)
            .WithOne(c => c.Post);
    }
}