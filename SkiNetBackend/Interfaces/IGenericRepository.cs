using SkiNetBackend.Entities;
using SkiNetBackend.Specifications;

namespace SkiNetBackend.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListallAsync();
    Task<T> GetEntityWitSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
}
