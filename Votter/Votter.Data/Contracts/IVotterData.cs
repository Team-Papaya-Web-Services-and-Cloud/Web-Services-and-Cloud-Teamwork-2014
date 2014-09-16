namespace Votter.Data.Contracts
{
    using System;
    using System.Linq;

    public interface IVotterData : IDisposable
    {
        // IGenericRepository<Model> Models { get; }

        int SaveChanges();
    }
}