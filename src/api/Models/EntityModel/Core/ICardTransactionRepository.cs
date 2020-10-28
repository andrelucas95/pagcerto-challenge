using System.Collections.Generic;
using System.Threading.Tasks;
using api.Infrastructure;

namespace api.Models.EntityModel.Core
{
    public interface ICardTransactionRepository : IRepository<CardTransaction>
    {
        void Add(CardTransaction cardTransaction);
        void Add(Anticipation anticipation);
        Task<List<CardTransaction>> ListByNsus(List<int> nsus);
        Task<Anticipation> FindAnticipation();
    }
}