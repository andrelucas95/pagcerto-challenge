using System.Threading.Tasks;
using api.Infrastructure;

namespace api.Models.EntityModel.Core
{
    public interface ICardTransactionRepository : IRepository<CardTransaction>
    {
        void Add(CardTransaction cardTransaction);
    }
}