using Microsoft.EntityFrameworkCore;

namespace Ordering.Database {
    public class OrderContext : DbContext {
        
        
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbOrderLine> OrderLines { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Stroopwafels.db");
        }
        
    }
}