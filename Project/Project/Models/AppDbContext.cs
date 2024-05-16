using Microsoft.EntityFrameworkCore;
using Project;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Torneio> Torneios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}