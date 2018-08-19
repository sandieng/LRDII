using System.Linq;

namespace LRDII.Services
{
    public interface ILrdiiRepository<T> where T : class
    {
        bool Save(T entity);
        IQueryable<T> GetAll();
        T GetById(int? id);
        void Update(T sharePrice);
        void Delete(T share);
    }
}
