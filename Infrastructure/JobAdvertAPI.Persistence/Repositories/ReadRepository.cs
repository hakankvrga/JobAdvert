using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities.common;
using JobAdvertAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobAdvertAPI.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity// ReadRepository  classını oluşturduk
                                                                        // ve T tipinde BaseEntity sınıfından kalıtım aldık
{
    private readonly JobAdvertContext _context;//  tanımladık ve JobAdvertContext tipinde bir nesne oluşturduk
                                               //  ve _context değişkenine atadık

    public ReadRepository(JobAdvertContext context) // ReadRepository  classının constructor methodunu oluşturduk
                                                    // ve JobAdvertContext tipinde bir parametre aldık
    {
        _context = context;
    }
    #region GetAll
    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query= Table.AsQueryable();
        if (!tracking)
           query= query.AsNoTracking();
        return query;
    }
    #endregion 


    #region GetWhere
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query= Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
    #endregion

    #region GetSingleAsync
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return  await query.FirstOrDefaultAsync(method);
    }
#endregion
    public async Task<T> GetByIdAsync(int id, bool tracking = true) 
    
    {
        var query= Table.AsQueryable(); // var tipinde query adında bir değişken
                                        // tanımladık ve Table değişkenini atadık
        if (!tracking) // tracking değişkeni true ise
            query = query.AsNoTracking(); // query değişkenine AsNoTracking methodunu atadık
        return await query.FirstOrDefaultAsync(data => data.Id == id); // bu şekilde GetByIdAsync
                                                                       // methodunu oluşturduk
    }
}
