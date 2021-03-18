using Microsoft.EntityFrameworkCore;

namespace WebProg_Projekat_2021.Models
{
    public class Context : DbContext
    {
        public DbSet<Parking> Parkinzi { get; set; }
        public DbSet<Mesto> Mesta  { get; set; }
        public DbSet<Vozilo> Vozila  { get; set; }
        public Context(DbContextOptions options) : base(options)
        {
            
        }
    }
}