using Checats.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checats.Persistence.Configurations;

public class CommentaryConfiguration : IEntityTypeConfiguration<CommentaryEntity>
{
    private const int ContentMaxLength = 1500;
    
    public void Configure(EntityTypeBuilder<CommentaryEntity> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .Property(c => c.Content)
            .HasMaxLength(ContentMaxLength)
            .IsRequired();

        builder
            .HasOne(c => c.Author)
            .WithMany(u => u.Commentaries);
    }
}