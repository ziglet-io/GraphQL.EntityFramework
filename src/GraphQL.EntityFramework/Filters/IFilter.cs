using System.Threading.Tasks;

namespace GraphQL.EntityFramework
{
    /// <summary>
    /// Interface for a class that implements the <see cref="Filter{TEntity}"/> and <see cref="AsyncFilter{TEntity}"/>
    /// delegates.
    ///
    /// This class is a convenience for stateful filters such as those that require a database context and are injected
    /// by the Dependency Injection framework usually as Scoped.
    /// </summary>
    public interface IFilter<TEntity> where TEntity : class
    {
        public delegate bool Filter(object userContext, TEntity input);

        public delegate Task<bool> AsyncFilter(object userContext, TEntity input);

    }
}