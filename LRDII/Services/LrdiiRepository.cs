using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRDII.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LRDII.Services
{
    public class LrdiiRepository<T> : ILrdiiRepository<T> where T : class
    {
        private LrdiiDbContext _context;
        private DbSet<T> entity;

        public LrdiiRepository(LrdiiDbContext context)
        {
            _context = context;
            entity = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return entity.AsQueryable();
        }

        public T GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public bool Save(T entity)
        {
            _context.Add(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
