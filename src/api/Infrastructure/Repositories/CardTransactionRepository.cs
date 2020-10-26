using api.Models.EntityModel;
using api.Models.EntityModel.Core;

namespace api.Infrastructure.Repositories
{
    public class CardTransactionRepository : ICardTransactionRepository
    {
        private readonly PaymentDbContext _context;

        public CardTransactionRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(CardTransaction cardTransaction)
        {
            _context.Add(cardTransaction);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}