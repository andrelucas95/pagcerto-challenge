using System.Threading.Tasks;
using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure
{
    public class PaymentDbContext : DbContext, IUnitOfWork
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) 
            : base(options) { }

        public DbSet<CardTransaction> CardTransctions { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Anticipation> Anticipations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}