namespace KPO2.Data.Repositories
{
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Get(long id);
        Task<T[]> GetAll();
        Task<T> Add(T entity);
        Task<T> Edit(T entity);
        Task<T> Remove(long id);
    }
}