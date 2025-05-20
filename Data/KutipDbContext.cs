using Microsoft.EntityFrameworkCore;
using KutipWeb.Models;

namespace KutipWeb.Data
{
    public class KutipDbContext : DbContext
    {
        public KutipDbContext(DbContextOptions<KutipDbContext> options) : base(options) 
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bin> Bins { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Collector> Collectors { get; set; }
        public virtual DbSet<Pickup> Pickups { get; set; }
        public virtual DbSet<Models.Route> Routes { get; set; }

    }
}
