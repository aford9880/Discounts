using Microsoft.EntityFrameworkCore;
using MTSDiscount.Core.Models;

namespace MTSDiscount.Core.Infrastructure {
    public class AppDbContext : DbContext {
        // Через параметр options в конструктор контекста данных будут передаваться настройки контекста
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            // с помощью вызова Database.EnsureCreated() по определению моделей будет создаваться база данных(если она отсутствует)
            //Database.EnsureCreated();
        }
        public DbSet<Discount> Discounts { get; set; }        
    }
}
