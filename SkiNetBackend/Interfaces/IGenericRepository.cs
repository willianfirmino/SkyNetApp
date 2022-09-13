using SkiNetBackend.Entities;

namespace SkiNetBackend.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListallAsync();
}
