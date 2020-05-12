﻿using Mercury.DAL.Data;
using Mercury.DAL.Helpers;
using Mercury.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.DAL.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<T> GetWithThrow(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                throw new InvalidOperationException();
            }

            return entity;
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(predicate);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllWithPaging<U>(QueryDate<T, U> queryData)
        {
            var objects = _context.Set<T>().AsQueryable();

            if(queryData.Includes != null)
            {
                foreach(var include in queryData.Includes)
                {
                    objects = objects.Include(include);
                }
            }

            if(queryData.Conditions != null)
            {
                foreach (var condition in queryData.Conditions)
                {
                    objects = objects.Where(condition);
                }
            }

            if (queryData.SortBy != null)
            {
                objects = objects.OrderBy(queryData.SortBy);
            }

            int skip = (queryData.CurrentPage - 1) * queryData.ItemsPerPage;

            objects = objects.Skip(skip)
                .Take(queryData.ItemsPerPage);

            return await objects.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        
        public void AddRange(IEnumerable<T> entities)
        {
            if(entities != null)
            {
                _context.Set<T>().AddRange(entities);
            }
        }

        public async Task<T> AddWithResult(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);

            return result.Entity;
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                _context.Set<T>().RemoveRange(entities);
            }
        }
    }
}
