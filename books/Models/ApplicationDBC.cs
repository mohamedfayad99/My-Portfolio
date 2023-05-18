using Microsoft.EntityFrameworkCore;

namespace books.Models
{
    public class ApplicationDBC  :DbContext
    {
     //   private ApplicationDBC _db;

        public ApplicationDBC(DbContextOptions<ApplicationDBC> options):base(options)    
        {
          //  _db= new ApplicationDBC(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<PortfolioItem> PortfolioItems { get; set; }
        public  virtual DbSet<Contactme> ContactmeItems { get; set; }
    }
}
