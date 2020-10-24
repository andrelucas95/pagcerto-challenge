using System.Threading.Tasks;

namespace api.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}