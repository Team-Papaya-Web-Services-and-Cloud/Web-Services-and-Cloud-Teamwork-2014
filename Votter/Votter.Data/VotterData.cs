﻿namespace Votter.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Votter.Data.Contracts;
    using Votter.Data.Repositories;

    public class VotterData : IVotterData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public VotterData()
            : this(new ApplicationDbContext())
        {
        }

        public VotterData(DbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}