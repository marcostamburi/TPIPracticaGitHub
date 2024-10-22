using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T>  _dbSet;

        public BaseRepository  (ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T? Add(T entity) 
        { 
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(T entity)
        { 
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity) 
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T? GetById(int id) 
        { 
            return _dbSet.Find(id);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
