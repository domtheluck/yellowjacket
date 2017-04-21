using System.Collections.Generic;
using System.Threading.Tasks;
using YellowJacket.Dashboard.Models.Agent;

namespace YellowJacket.Dashboard.Repositories
{
    public interface IAgentRepository
    {
        #region Public Methods

        void Add(AgentModel entity);

        /// <summary>
        /// Gets all agents from the repository.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AgentModel>> GetAll();

        /// <summary>
        /// Finds an agent by its key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><see cref="AgentModel"/>.</returns>
        AgentModel Find(long key);

        void Remove(long key);

        void Update(AgentModel item);

        #endregion
    }
}
