using Checats.Persistence.Configurations;
using Checats.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checats.Persistence;

/// <summary>
/// Контекст базы данных CheCats
/// </summary>
/// <param name="options"></param>
public class ChecatsDbContext(DbContextOptions<ChecatsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CommentaryConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CommentaryEntity> Commentaries { get; set; }
}