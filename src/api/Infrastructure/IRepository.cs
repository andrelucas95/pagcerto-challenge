using System;
using api.Models.EntityModel.Core;

namespace api.Infrastructure
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}