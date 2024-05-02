using DMSMVC.Models.Entities;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T type);
        Task<IList<T>> GetAllAsync();
        void Delete(T type);
        Task<T> GetAsync(Expression<Func<T, bool>> exp);


        //void SetUp();






    }
}
