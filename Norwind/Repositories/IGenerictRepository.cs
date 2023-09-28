using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Norwind.DTO;
using Norwind.Models;

namespace Norwind.Repositories
{
    public interface IGenerictRepository<T> where T : BaseModelDTO
    {
        void Create(T entity);
        void Update(T entity);
        public IEnumerable<T> GetAll();
        T GetById(int Id);
        void Delete(T entity);
        void DeleteById(int id);
        void Save();
        IQueryable<T> FindAll();
        //IEnumerable<T> GetPaging(PagingParameters pageParameters);


    }
}