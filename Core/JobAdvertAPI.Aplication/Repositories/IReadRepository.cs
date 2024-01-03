using JobAdvertAPI.Domain.Entities.common;
using System.Linq.Expressions;

namespace JobAdvertAPI.Aplication.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity // IReadRepository  classını oluşturduk
                                                                          // ve T tipinde BaseEntity sınıfından kalıtım aldık
{
    IQueryable<T> GetAll(bool tracking=true);// IQueryable<T> tipinde GetAll adında bir method oluşturduk
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);// IQueryable<T> tipinde GetWhere
                                                                                   // adında bir method oluşturduk
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);// Task<T> tipinde GetSingleAsync
                                                                                   // adında bir method oluşturduk
    Task<T> GetByIdAsync(int id, bool tracking = true);// Task<T> tipinde GetByIdAsync
                                                       // adında bir method oluşturduk
}
