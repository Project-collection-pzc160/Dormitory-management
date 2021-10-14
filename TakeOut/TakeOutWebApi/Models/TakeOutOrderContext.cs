using Microsoft.EntityFrameworkCore;

namespace DormManagementApi.Models
{
    public class TakeOutOrderContext : DbContext
    {
        public TakeOutOrderContext(DbContextOptions<TakeOutOrderContext> options)
          : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }

        public DbSet<TakeOutOrder> TakeOutOrders { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
