﻿using Commerce.Contracts.Repositories;
using Commerce.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Commerce.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual void Commit()
        {
            context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        //public virtual void Delete(object id)
        //{
        //    TEntity entity = null;//dbSet.Find(id);
        //    Delete(entity);
        //}
        public abstract void Delete(object id);

        public virtual void Dispose()
        {
            context.Dispose();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<TEntity> GetAll(object filter)
        {
            return null; //need to override in order to implement specific filtering.

        }

        //public virtual TEntity GetById(object id)
        //{
        //    return null;// dbSet.Find(id);
        //}
        public abstract TEntity GetById(object id);

        public virtual TEntity GetFullObject(object id)
        {
            return null; //need to override in order to implement specific filtering.

        }

        public virtual IQueryable<TEntity> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
        {
            return dbSet.Skip(skip).Take(top);
            //return null; //need to override in order to implement specific filtering and ordering
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}