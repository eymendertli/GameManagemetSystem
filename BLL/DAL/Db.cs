using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public class Db : DbContext
{
    public DbSet<Player> Players { get; set; }
    
    public DbSet<GameAccount> GameAccounts { get; set; }

    public Db(DbContextOptions<Db> options) : base(options)
    {
    }
}