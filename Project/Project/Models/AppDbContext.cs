using Microsoft.EntityFrameworkCore;
using Project.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Torneio> Torneios { get; set; }
    public DbSet<Battle> Battles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Torneios)
            .WithMany(t => t.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserTorneio",
                j => j.HasOne<Torneio>().WithMany().HasForeignKey("TorneioId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));

        modelBuilder.Entity<Torneio>()
            .HasKey(t => t.TorneioId);

        modelBuilder.Entity<Torneio>()
            .HasOne(t => t.Batalha)
            .WithMany()
            .HasForeignKey("BatalhaId");

        modelBuilder.Entity<Battle>()
            .HasKey(b => b.Jogada);
    }
}