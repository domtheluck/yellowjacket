using System.Collections.Generic;
using System.Threading.Tasks;
using YellowJacket.Dashboard.Entities.Agent;

namespace YellowJacket.Dashboard.Repositories
{
    public interface IAgentRepository
    {
        #region Public Methods

        void Add(AgentEntity entity);

        /// <summary>
        /// Gets all agents from the repository.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AgentEntity>> GetAll();

        /// <summary>
        /// Finds an agent by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="AgentEntity"/>.</returns>
        AgentEntity Find(string id);

        void Remove(string id);

        void Update(AgentEntity item);

        #endregion
    }
}
