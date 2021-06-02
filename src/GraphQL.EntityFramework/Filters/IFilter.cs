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
        /// <summary>
        /// Return true if the system should return the entity. False if not.
        /// </summary>
        /// <param name="userContext">User context from the caller. For example in an ASP.NET application should be <see cref="UserContext"/> or similar.</param>
        /// <param name="input">The <see cref="TEntity"/> that will be output if the filter succeeds.</param>
        /// <returns></returns>
        public bool Filter(object userContext, TEntity input);

        /// <summary>
        /// Return true if the system should return the entity. False if not.
        /// </summary>
        /// <param name="userContext">User context from the caller. For example in an ASP.NET application should be <see cref="UserContext"/> or similar.</param>
        /// <param name="input">The <see cref="TEntity"/> that will be output if the filter succeeds.</param>
        /// <returns></returns>
        public Task<bool> AsyncFilter(object userContext, TEntity input);

    }
}