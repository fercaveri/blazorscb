using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using NHibernate.Exceptions;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;

namespace SurrealCB.Data.Repository
{
    public interface IRepository : IDisposable
    {
        void Insert<T>(T obj, object id, bool failIfExists = true) where T : class;
        Task InsertAsync<T>(T obj, object id, bool failIfExists = true) where T : class;
        T Save<T>(T obj) where T : class;
        Task<T> SaveAsync<T>(T obj) where T : class;
        Task<ICollection<T>> SaveCollectionAsync<T>(ICollection<T> obj) where T : class;
        Task<ICollection<T>> UpdateCollectionAsync<T>(ICollection<T> oldList, ICollection<T> newList) where T : class;
        T Load<T>(object id);
        Task<T> LoadAsync<T>(object id);
        void Delete<T>(T obj) where T : class;
        Task DeleteAsync<T>(T obj) where T : class;
        IQueryable<T> Query<T>();
        ISession GetSession();
        NHibernate.Stat.IStatistics GetDbStatistics();
    }

    public class NHibernateRepository : IRepository
    {
        public NHibernateRepository(IRepositoryConfiguration config)
        {
            this.config = config;
        }

        protected IRepositoryConfiguration config;
        protected ISession session;

        public ISession Session
        {
            get
            {
                if (this.session == null)
                {
                    this.session = this.config.SessionFactory.OpenSession();
                }

                return this.session;
            }
        }

        public void Insert<T>(T obj, object id, bool failIfExists = true) where T : class
        {
            try
            {
                this.Session.Save(obj, id);
                this.Session.Flush();
            }
            catch (GenericADOException)
            {
                if (failIfExists)
                {
                    throw;
                }
            }
        }

        public async Task InsertAsync<T>(T obj, object id, bool failIfExists = true) where T : class
        {
            try
            {
                await this.Session.SaveAsync(obj, id);
                await this.Session.FlushAsync();
            }
            catch (GenericADOException)
            {
                if (failIfExists)
                {
                    throw;
                }
            }
        }

        public IQueryable<T> Query<T>()
        {
            return this.Session.Query<T>();
        }

        public void Dispose()
        {
            if (this.session != null)
            {
                this.session.Dispose();
            }
        }

        public T Load<T>(object id)
        {
            return this.Session.Load<T>(id);
        }

        public Task<T> LoadAsync<T>(object id)
        {
            return this.Session.LoadAsync<T>(id);
        }

        public virtual T Save<T>(T obj) where T : class
        {
            var rv = this.Session.Merge<T>(obj);
            this.Session.Flush();
            return rv;
        }

        public virtual async Task<T> SaveAsync<T>(T obj) where T : class
        {
            var rv = await this.Session.MergeAsync<T>(obj);
            await this.Session.FlushAsync();
            return rv;
        }

        public virtual async Task<ICollection<T>> SaveCollectionAsync<T>(ICollection<T> list) where T : class
        {
            ICollection<T> ret = new List<T>();
            using (var transaction = this.Session.BeginTransaction())
            {
                foreach (var item in list)
                {
                    ret.Add(await this.session.MergeAsync(item));
                }
                await transaction.CommitAsync();
            }
            return ret;
        }

        public virtual async Task<ICollection<T>> UpdateCollectionAsync<T>(ICollection<T> oldList, ICollection<T> newList) where T : class
        {
            List<T> ret = new List<T>();
            using (var transaction = this.Session.BeginTransaction())
            {
                foreach (var item in oldList)
                {
                    await session.DeleteAsync(item);
                }
                foreach (var item in newList)
                {
                    ret.Add(await this.session.MergeAsync(item));
                }
                await transaction.CommitAsync();
            }
            return ret;
        }

        public void Delete<T>(T obj) where T : class
        {
            this.session.Delete(obj);
            this.Session.Flush();
        }

        public async Task DeleteAsync<T>(T obj) where T : class
        {
            await this.session.DeleteAsync(obj);
            await this.Session.FlushAsync();
        }

        public ISession GetSession()
        {
            return this.Session;
        }

        public NHibernate.Stat.IStatistics GetDbStatistics()
        {
            return this.config.SessionFactory.Statistics;
        }
    }
}