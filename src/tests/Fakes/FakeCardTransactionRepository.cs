using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Infrastructure;
using api.Models.EntityModel;
using api.Models.EntityModel.Core;
using Microsoft.EntityFrameworkCore;

namespace tests.Fakes
{
    public class FakeCardTransactionRepository : ICardTransactionRepository
    {

       private readonly PaymentDbContext _context;

        public FakeCardTransactionRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(CardTransaction cardTransaction)
        {
            _context.Add(cardTransaction);
        }

        public async Task<List<CardTransaction>> ListByNsus(List<int> nsus)
        {
            return await _context.CardTransctions
                .Include(ct => ct.Installments)
                .Where(ct => nsus.Contains(ct.Nsu))
                .ToListAsync();
        }

        public async Task<Anticipation> FindAnticipation()
        {
            return await _context.Anticipations
            .Include(a => a.CardTransactions)
            .ThenInclude(ct => ct.Installments)
            .Where(a => a.AnalysisFinalizedAt == null)
            .FirstOrDefaultAsync();
        }

        public void Add(Anticipation anticipation)
        {
            _context.Add(anticipation);
        }

        public void UpdateAnticipation(Anticipation anticipation)
        {
            _context.Update(anticipation);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}