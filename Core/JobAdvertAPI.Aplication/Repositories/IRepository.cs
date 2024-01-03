using JobAdvertAPI.Domain.Entities.common;
using Microsoft.EntityFrameworkCore;

namespace JobAdvertAPI.Aplication.Repositories;

public interface IRepository<T> where T : BaseEntity //IRepository  cllasını oluşturduk ve
                                                     //T tipinde BaseEntity sınıfından kalıtım aldık
{
    DbSet<T> Table { get; } //DbSet<T> tipinde Table adında bir property oluşturduk
}
