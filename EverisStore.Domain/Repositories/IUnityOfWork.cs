using System.Threading.Tasks;

namespace EverisStore.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
