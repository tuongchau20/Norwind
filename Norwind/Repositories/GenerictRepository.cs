using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Norwind.DTO;
using Norwind.Data;
using Norwind.Helpers;
using Norwind.Models;

namespace Norwind.Repositories
{
    public class GenerictRepository<T> : IGenerictRepository<T> where T : BaseModelDTO
    {
        private readonly NorthwindContext _context;
        private readonly ILoggerManager _logger;

        public GenerictRepository(NorthwindContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Create(T entity)
        {
            _logger.LogInfo("GenerictRepository: Create");
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = "User";
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = "User";
            _context.Set<T>().Add(entity);
            this.Save();
        }

        public void Delete(T entity)
        {
            _logger.LogInfo("GenerictRepository: Delete");
            _context.Set<T>().Remove(entity);
            this.Save();
        }

        public void DeleteById(int id)
        {
            _logger.LogInfo("GenerictRepository: DeleteById");
            T element = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(element);
            this.Save();
        }

        public IEnumerable<T> GetAll()
        {
            _logger.LogInfo("GenerictRepository: GetAll");
            return _context.Set<T>().AsQueryable();
        }

        public T GetById(int Id)
        {
            _logger.LogInfo("GenerictRepository: GetById");
            return _context.Set<T>().Find(Id);
        }

        public void Update(T entity)
        {
            _logger.LogInfo("GenerictRepository: Update");
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = "User";
            _context.Set<T>().Update(entity);
            this.Save();
        }

        public void Save()
        {
            _logger.LogInfo("GenerictRepository: Save");
            _context.SaveChanges();
        }

        public IQueryable<T> FindAll()
        {
            _logger.LogInfo("GenerictRepository: FindAll");
            return _context.Set<T>().AsNoTracking();
        }


    }
}