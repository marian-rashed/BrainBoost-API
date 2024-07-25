using System.Linq.Expressions;

namespace BrainBoost_API.Repositories.Inplementation
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null);

        void add(T entity);
        void update(T entity);
       
        void remove(T entity);
        void removeRange(IEnumerable<T> entities);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter, string? includeProps = null);
    }
}
