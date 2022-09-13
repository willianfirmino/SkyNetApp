
using Microsoft.EntityFrameworkCore;
using SkiNetBackend.Entities;
using SkiNetBackend.Interfaces;

namespace SkiNetBackend.Data;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;
    public GenericRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListallAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
