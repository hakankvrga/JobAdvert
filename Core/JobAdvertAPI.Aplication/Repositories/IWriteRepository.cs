using JobAdvertAPI.Domain.Entities.common;

namespace JobAdvertAPI.Aplication.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity// IWriteRepository  classını oluşturduk ve
                                                                          // T tipinde BaseEntity sınıfından kalıtım aldık
{
    Task<bool> AddAsync(T model);// Task<bool> tipinde AddAsync
                                 // adında bir method oluşturduk
    Task<bool> AddRangeAsync(List<T> datas);// Task<bool> tipinde
                                            // AddRangeAsync adında bir method oluşturduk
    bool Remove(T model);// bool tipinde Remove
                         // adında bir method oluşturduk
    bool RemoveRange(List<T> datas);// bool tipinde RemoveRange
                                    // adında bir method oluşturduk
    Task<bool> RemoveAsync(int id);// Task<bool> tipinde RemoveAsync
                                   // adında bir method oluşturduk
    bool Update(T model);
    Task<int> SaveAsync();
}
