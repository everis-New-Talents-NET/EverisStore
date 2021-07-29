using EverisStore.Domain.DomainObjects;
using System;

namespace EverisStore.Domain.Repositories
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
